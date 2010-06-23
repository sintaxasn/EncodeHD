Imports System.IO
Imports System.Threading

Public Class cls_MediaEncoder

#Region "Declarations"

    ' FFmpeg defaults
    Private FFMPEG_OPTIONS_COMMENT As String = "-comment ""Created using " & My.Resources.App_Title & " " & fvi_AppVersion.FileVersion & " - " & My.Resources.App_Company & """"
    Private Const FFMPEG_OPTIONS_OVERWRITEEXISTINGFILES As String = "-y"
    Private Const FFMPEG_OPTIONS_COPY As String = "-threads 0"
    Private Const FFMPEG_OPTIONS_H264 As String = "-flags +loop " & _
                                                    "-cmp +chroma " & _
                                                    "-partitions +parti4x4+partp8x8+partb8x8 " & _
                                                    "-rc_eq 'blurCplx^(1-qComp)' " & _
                                                    "-me_method hex " & _
                                                    "-subq 6 " & _
                                                    "-me_range 16 " & _
                                                    "-g 250 " & _
                                                    "-keyint_min 25 " & _
                                                    "-sc_threshold 40 " & _
                                                    "-i_qfactor 0.71 " & _
                                                    "-b_strategy 1 " & _
                                                    "-qcomp 0.6 " & _
                                                    "-qmin 10 " & _
                                                    "-qmax 51 " & _
                                                    "-qdiff 4 " & _
                                                    "-directpred 1 " & _
                                                    "-flags2 +fastpskip " & _
                                                    "-threads 0"

    Private Const FFMPEG_OPTIONS_MPEG4 As String = "-threads 8"

    ' -qmin 10 x264 recommended, 20 visualhub
    ' -subq 5 x264 recommended, 6 other source

    Public Const FFMPEG_CODEC_VIDEO_COPY As String = "copy"
    Public Const FFMPEG_CODEC_VIDEO_H264 As String = "libx264"
    Public Const FFMPEG_CODEC_VIDEO_MPEG4 As String = "mpeg4"
    Public Const FFMPEG_CODEC_VIDEO_AVI As String = "libxvid"
    Public Const FFMPEG_CODEC_AUDIO_AAC As String = "libfaac"
    ' Public Const FFMPEG_CODEC_AUDIO_AAC As String = "aac -strict experimental"
    Public Const FFMPEG_CODEC_AUDIO_AC3 As String = "ac3"
    Public Const FFMPEG_CODEC_AUDIO_MP3 As String = "libmp3lame"

    ' Setup the thread objects
    Private _thread As Thread
    Private _process As Process
    Private _processPriority As ProcessPriorityClass = Nothing

    Private _int_ProcessExitCode As Integer = 0
    Private _bln_ProcessCancelled As Boolean = False
    Private _bln_ProcessFailed As Boolean = False
    Private _str_Progress As String = Nothing
    Private _int_PercentComplete As Integer = Nothing

    ' Set the encoding application variables
    Private _str_AppExecutable As String = "ffmpeg.exe"
    Private _str_AppArguments As String = Nothing
    Private _bln_EncodingVideoStreamDetected As Boolean = False
    Private _int_EncodingTimeDuration As Integer = Nothing
    Private _int_EncodingTimeCurrentPosition As Integer = Nothing
    Private _dtm_EncodingTimeStart As Date
    Private _int_EncodingZeroFileSizeCount As Integer = 0

    ' Set the video variables
    Private _str_EncoderVideoCodec As String = Nothing
    Private _bln_EncoderVideoCopy As Boolean = False
    Private _int_EncoderVideoDuration As Integer = 0
    Private _int_EncoderVideoBitRate As Integer = 0
    Private _int_EncoderVideoWidth As Integer = 0
    Private _int_EncoderVideoHeight As Integer = 0
    Private _int_EncoderVideoPadding_Top As Integer = 0
    Private _int_EncoderVideoPadding_Bottom As Integer = 0
    Private _dbl_EncoderVideoFPS As Double = 0
    Private _int_EncoderVideoH264ProfileLevel As Integer = 0
    Private _bln_EncoderVideoH264ProfileLowComplexity As Boolean = False

    ' Set the audio variables
    Private _int_EncoderAudioStream As Integer = 0
    Private _str_EncoderAudioCodecs As String() = Nothing
    Private _int_EncoderAudioBitRate As Integer = Nothing
    Private _int_EncoderAudioSampleRate As Integer = Nothing
    Private _int_EncoderAudioChannels As Integer = Nothing

    Private _str_EncoderAdvancedFlags As String = Nothing
    Private _str_EncoderInputFile As String = Nothing
    Private _str_EncoderOutputFolder As String = Nothing
    Private _str_EncoderOutputFile As String = Nothing
    Private _str_EncoderOutputFileExtension As String = Nothing

#End Region

#Region "Properties"

    Public ReadOnly Property ExitCode() As Integer
        Get
            Return _int_ProcessExitCode
        End Get
    End Property

    Public Property VideoCodec() As String
        Get
            Return _str_EncoderVideoCodec
        End Get
        Set(ByVal value As String)
            _str_EncoderVideoCodec = value
        End Set
    End Property

    Public Property VideoDuration() As Integer
        Get
            Return _int_EncoderVideoDuration
        End Get
        Set(ByVal value As Integer)
            _int_EncoderVideoDuration = value
        End Set
    End Property

    Public Property VideoBitrate() As Integer
        Get
            Return _int_EncoderVideoBitRate
        End Get
        Set(ByVal value As Integer)
            _int_EncoderVideoBitRate = value
        End Set
    End Property

    Public Property VideoFPS() As Double
        Get
            Return _dbl_EncoderVideoFPS
        End Get
        Set(ByVal value As Double)
            _dbl_EncoderVideoFPS = value
        End Set
    End Property

    Public Property VideoHeight() As Integer
        Get
            Return _int_EncoderVideoHeight
        End Get
        Set(ByVal value As Integer)
            _int_EncoderVideoHeight = value
        End Set
    End Property

    Public Property VideoWidth() As Integer
        Get
            Return _int_EncoderVideoWidth
        End Get
        Set(ByVal value As Integer)
            _int_EncoderVideoWidth = value
        End Set
    End Property

    Public Property VideoPadding_Top() As Integer
        Get
            Return _int_EncoderVideoPadding_Top
        End Get
        Set(ByVal value As Integer)
            _int_EncoderVideoPadding_Top = value
        End Set
    End Property

    Public Property VideoPadding_Bottom() As Integer
        Get
            Return _int_EncoderVideoPadding_Bottom
        End Get
        Set(ByVal value As Integer)
            _int_EncoderVideoPadding_Bottom = value
        End Set
    End Property

    Public Property VideoH264ProfileLevel() As Integer
        Get
            Return _int_EncoderVideoH264ProfileLevel
        End Get
        Set(ByVal value As Integer)
            _int_EncoderVideoH264ProfileLevel = value
        End Set
    End Property

    Public Property VideoH264ProfileLowComplexity() As Boolean
        Get
            Return _bln_EncoderVideoH264ProfileLowComplexity
        End Get
        Set(ByVal value As Boolean)
            _bln_EncoderVideoH264ProfileLowComplexity = value
        End Set
    End Property

    Public Property AudioStream() As Integer
        Get
            Return _int_EncoderAudioStream
        End Get
        Set(ByVal value As Integer)
            _int_EncoderAudioStream = value
        End Set
    End Property

    Public Property AudioCodecs() As String()
        Get
            Return _str_EncoderAudioCodecs
        End Get
        Set(ByVal value As String())
            _str_EncoderAudioCodecs = value
        End Set
    End Property

    Public Property AudioBitRate() As Integer
        Get
            Return _int_EncoderAudioBitRate
        End Get
        Set(ByVal value As Integer)
            _int_EncoderAudioBitRate = value
        End Set
    End Property
    Public Property AudioSampleRate As Integer
        Get
            Return _int_EncoderAudioSampleRate
        End Get
        Set(ByVal value As Integer)
            _int_EncoderAudioSampleRate = value
        End Set
    End Property

    Public Property InputFile() As String
        Get
            Return _str_EncoderInputFile
        End Get
        Set(ByVal value As String)
            _str_EncoderInputFile = value
        End Set
    End Property

    Public Property OutputFolder() As String
        Get
            Return _str_EncoderOutputFolder
        End Get
        Set(ByVal value As String)
            _str_EncoderOutputFolder = value
        End Set
    End Property

    Public Property OutputFile() As String
        Get
            Return _str_EncoderOutputFile
        End Get
        Set(ByVal value As String)
            _str_EncoderOutputFile = value
        End Set
    End Property

    Public Property OutputFileExtension() As String
        Get
            Return _str_EncoderOutputFileExtension
        End Get
        Set(ByVal value As String)
            _str_EncoderOutputFileExtension = value
        End Set
    End Property

    Public Property EncoderAdvancedFlags() As String
        Get
            Return _str_EncoderAdvancedFlags
        End Get
        Set(ByVal value As String)
            _str_EncoderAdvancedFlags = value
        End Set
    End Property

    Public Property ProcessPriority() As ProcessPriorityClass
        Get
            Return _processPriority
        End Get
        Set(ByVal value As ProcessPriorityClass)
            _processPriority = value
        End Set
    End Property

    Public ReadOnly Property ProcessCancelled() As Boolean
        Get
            Return _bln_ProcessCancelled
        End Get
    End Property

    Public ReadOnly Property ProcessFailed() As Boolean
        Get
            Return _bln_ProcessFailed
        End Get
    End Property

    Public ReadOnly Property Progress() As String
        Get
            Return _str_Progress
        End Get
    End Property

    Public ReadOnly Property PercentComplete() As Integer
        Get
            Return _int_PercentComplete
        End Get
    End Property

    Public ReadOnly Property TimeRemaining() As Double
        Get
            Return _func_GetTimeRemaining()
        End Get
    End Property


#End Region

#Region "Events"

    Public Event ProgressUpdate()
    Public Event Finished()

#End Region

#Region "Subroutines"

    Public Sub SpinUp()

        Try

            If _processPriority = Nothing Then
                sub_DebugMessage("Setting default process priority...")
                _processPriority = ProcessPriorityClass.Normal
            End If

            sub_DebugMessage("Starting Thread...")
            Dim threadStart As New ThreadStart(AddressOf Me._Start)
            _thread = New Thread(threadStart)
            _thread.IsBackground = True
            _thread.Start()

        Catch ex As Exception

        End Try

    End Sub

    Public Sub SpinDown()

        ' Cleanup

        Try

            sub_DebugMessage("Ending Thread...")

            If Not _process Is Nothing Then
                If Not _process.HasExited Then
                    _process.Kill()
                End If
                _process.Close()
            End If

            _process = Nothing
            _thread = Nothing

        Catch ex As Exception

        End Try

    End Sub

    Public Sub Abort()

        Try

            sub_DebugMessage("Ending Thread...")

            If Not _process Is Nothing Then
                If Not _process.HasExited Then
                    _process.Kill()
                End If
                _process.Close()
            End If

            _process = Nothing
            _thread = Nothing

            ' Return Cancelled Error Code
            _int_ProcessExitCode = 999

        Catch ex As Exception

        End Try

    End Sub

    Private Sub _Start()

        Dim _str_EncoderInputFileNoExt As String = Nothing

        ' Reset Encoding Status
        _bln_ProcessCancelled = False
        _int_EncodingZeroFileSizeCount = 0

        Try

            ' Determine the output file name
            sub_DebugMessage("Determining output filename...")
            Dim _int_OutputFileAdditionalNumber As Integer = 0
            Dim _bln_OutputFileOk As Boolean = False
            ' Verify the file has an extension
            If Path.GetExtension(_str_EncoderInputFile) = Nothing Then Throw New Exception("Input file has no extension")
            ' Check for the existance of our file
            Dim _str_OutputFileTemp As String = Nothing
            If _str_EncoderOutputFolder = Nothing Then
                ' Start in the Input folder
                _str_OutputFileTemp = Path.Combine(Path.GetDirectoryName(_str_EncoderInputFile), Path.GetFileName(Path.ChangeExtension(_str_EncoderInputFile, _str_EncoderOutputFileExtension.ToLower)))
            Else
                ' Start in the specified Output folder
                _str_OutputFileTemp = Path.Combine(_str_EncoderOutputFolder, Path.GetFileName(Path.ChangeExtension(_str_EncoderInputFile, _str_EncoderOutputFileExtension.ToLower)))
            End If
            ' Check if the Output file has already been set
            If _str_EncoderOutputFile = Nothing Then
                ' Loop through until we find a valid filename to use
                Do
                    sub_DebugMessage("Checking for existing of file: " & _str_OutputFileTemp)
                    If My.Computer.FileSystem.FileExists(_str_OutputFileTemp) Then
                        sub_DebugMessage(vbTab & "File already exists!")
                        _int_OutputFileAdditionalNumber += 1
                        _str_OutputFileTemp = Path.Combine(Path.GetDirectoryName(_str_OutputFileTemp), Path.GetFileNameWithoutExtension(_str_OutputFileTemp) & "-" & _int_OutputFileAdditionalNumber & Path.GetExtension(_str_OutputFileTemp))
                    Else
                        sub_DebugMessage("File not found. Using this as our output filename")
                        _str_EncoderOutputFile = _str_OutputFileTemp
                        _bln_OutputFileOk = True
                    End If
                Loop Until _bln_OutputFileOk
            Else
                ' Use the new filename, but modify the path as required
                If _str_EncoderOutputFolder = Nothing Then
                    ' Start in the Input folder
                    _str_OutputFileTemp = Path.Combine(Path.GetDirectoryName(_str_EncoderInputFile), Path.GetFileName(Path.ChangeExtension(_str_EncoderOutputFile, _str_EncoderOutputFileExtension.ToLower)))
                Else
                    ' Start in the specified Output folder
                    _str_OutputFileTemp = Path.Combine(_str_EncoderOutputFolder, Path.GetFileName(Path.ChangeExtension(_str_EncoderOutputFile, _str_EncoderOutputFileExtension.ToLower)))
                End If
                _str_EncoderOutputFile = _str_OutputFileTemp
                sub_DebugMessage("Output file: " & _str_EncoderOutputFile)
            End If

        Catch ex As Exception
            sub_DebugMessage("Output filename Error: " & ex.Message, True, MsgBoxStyle.Critical, True)
            Exit Sub
        End Try

        Try
            ' Set up FFmpeg parameters
            sub_DebugMessage("Setting up " & _str_AppExecutable & " parameters...")

            ' Set up the initial arguments
            ' _str_AppArguments = _str_AppArguments & FFMPEG_OPTIONS_COMMENT
            _str_AppArguments = _str_AppArguments & FFMPEG_OPTIONS_OVERWRITEEXISTINGFILES

            ' Add each input file to the command-line
            _str_AppArguments = _str_AppArguments & " -i """ & _str_EncoderInputFile & """"

            ' Set up the video mapping (if the audio mapping is non-standard)
            If _int_EncoderAudioStream <> 0 And _int_EncoderAudioStream <> 99 Then
                _str_AppArguments = _str_AppArguments & " -map 0:0"
            End If

            ' Determine the relevant Video Codec
            Select Case _str_EncoderVideoCodec
                Case "COPY"
                    _str_AppArguments = _str_AppArguments & " -vcodec " & cls_MediaEncoder.FFMPEG_CODEC_VIDEO_COPY
                    _str_AppArguments = _str_AppArguments & " " & FFMPEG_OPTIONS_COPY
                    _bln_EncoderVideoCopy = True
                Case "H264"
                    _str_AppArguments = _str_AppArguments & " -vcodec " & cls_MediaEncoder.FFMPEG_CODEC_VIDEO_H264
                    ' Disable CABAC for Low Complexity Baseline Profile
                    If _bln_EncoderVideoH264ProfileLowComplexity Then
                        _str_AppArguments = _str_AppArguments & " -coder 0"
                    Else
                        _str_AppArguments = _str_AppArguments & " -coder 1"
                    End If
                    _str_AppArguments = _str_AppArguments & " " & FFMPEG_OPTIONS_H264
                    _str_AppArguments = _str_AppArguments & " -level " & _int_EncoderVideoH264ProfileLevel
                Case "MPEG4"
                    _str_AppArguments = _str_AppArguments & " -vcodec " & cls_MediaEncoder.FFMPEG_CODEC_VIDEO_MPEG4
                    _str_AppArguments = _str_AppArguments & " " & FFMPEG_OPTIONS_MPEG4
            End Select

            ' Set up additional parameters if re-encoding
            If Not _bln_EncoderVideoCopy Then
                ' _str_AppArguments = _str_AppArguments & " -b " & _int_EncoderVideoBitRate & "k" & " -bt " & _int_EncoderVideoBitRate & "k"
                _str_AppArguments = _str_AppArguments & " -b " & _int_EncoderVideoBitRate & "k"
                _str_AppArguments = _str_AppArguments & " -minrate " & _int_EncoderVideoBitRate & "k"
                _str_AppArguments = _str_AppArguments & " -maxrate " & _int_EncoderVideoBitRate & "k"
                _str_AppArguments = _str_AppArguments & " -bufsize " & _int_EncoderVideoBitRate & "k"
                _str_AppArguments = _str_AppArguments & " -s " & _int_EncoderVideoWidth & "x" & _int_EncoderVideoHeight
                ' _str_AppArguments = _str_AppArguments & " -aspect " & _int_EncoderVideoWidth & ":" & _int_EncoderVideoHeight
                _str_AppArguments = _str_AppArguments & " -r " & _dbl_EncoderVideoFPS.ToString.Replace(strLocaleDecimal, ".")
                If Not _int_EncoderVideoPadding_Top = 0 Then
                    _str_AppArguments = _str_AppArguments & " -padtop " & _int_EncoderVideoPadding_Top
                End If
                If Not _int_EncoderVideoPadding_Bottom = 0 Then
                    _str_AppArguments = _str_AppArguments & " -padbottom " & _int_EncoderVideoPadding_Bottom
                End If
            End If

            ' If there's no audio stream, specify FFMpeg Audio removal flags, just in case
            If _int_EncoderAudioStream = 99 Then

                _str_AppArguments = _str_AppArguments & " -an"
                ' Add the output file name immediately
                _str_AppArguments = _str_AppArguments & " """ & _str_EncoderOutputFile & """"

            Else
                sub_DebugMessage("Setting up Audio parameters...")
                Dim _int_EncoderAudioStreamIndex As Integer = 0
                For Each _str_EncoderAudioCodec As String In _str_EncoderAudioCodecs
                    _int_EncoderAudioStreamIndex = _int_EncoderAudioStreamIndex + 1

                    ' Determine the relevant Audio Codec
                    Select Case _str_EncoderAudioCodec
                        Case "AAC"
                            ' Set up the audio stream mapping (if the audio stream is nonstandard)
                            If _int_EncoderAudioStream <> 0 Then
                                _str_AppArguments = _str_AppArguments & " -map 0." & _int_EncoderAudioStream + 1 & ":0"
                            End If
                            ' Copy the audio stream if the bitrate was too low
                            If _int_EncoderAudioBitRate = 0 Then
                                _str_AppArguments = _str_AppArguments & " -acodec copy"
                            Else
                                _str_AppArguments = _str_AppArguments & " -acodec " & cls_MediaEncoder.FFMPEG_CODEC_AUDIO_AAC
                                ' _str_AppArguments = _str_AppArguments & " -ac 2"
                                _str_AppArguments = _str_AppArguments & " -ar " & _int_EncoderAudioSampleRate
                                _str_AppArguments = _str_AppArguments & " -ab " & _int_EncoderAudioBitRate & "k"
                            End If
                        Case "AC3"
                            ' Set up the audio stream mapping (if the audio stream is nonstandard)
                            If _int_EncoderAudioStream <> 0 Then
                                _str_AppArguments = _str_AppArguments & " -map 0:" & _int_EncoderAudioStream + 1
                            End If
                            _str_AppArguments = _str_AppArguments & " -acodec " & cls_MediaEncoder.FFMPEG_CODEC_AUDIO_AC3
                            ' TODO - Fix 2 channel audio issue
                            _str_AppArguments = _str_AppArguments & " -ac 2"
                            _str_AppArguments = _str_AppArguments & " -ar " & _int_EncoderAudioSampleRate & "k"
                            ' Ensure the bitrate is at least 128kb
                            If _int_EncoderAudioBitRate < 128 Then
                                _str_AppArguments = _str_AppArguments & " -ab 128k"
                            Else
                                _str_AppArguments = _str_AppArguments & " -ab " & _int_EncoderAudioBitRate & "k"
                            End If

                    End Select

                    ' If not the first audio stream, the specify as a new audio stream
                    If Not _str_EncoderAudioCodecs(0) = _str_EncoderAudioCodec Then
                        _str_AppArguments = _str_AppArguments & " -newaudio"
                    Else
                        ' Add the output file name after the first audio stream
                        _str_AppArguments = _str_AppArguments & " """ & _str_EncoderOutputFile & """"
                    End If

                Next

            End If


            ' Add and replace the encoding arguments
            If Not _str_EncoderAdvancedFlags = Nothing Then
                sub_DebugMessage("Modifying arguements with advanced FFmpeg flags: " & _str_EncoderAdvancedFlags)
                _str_AppArguments = _func_ReplaceEncoderArguments(_str_AppArguments, _str_EncoderAdvancedFlags)
            End If

            ' Setup and configure the new process
            sub_DebugMessage("Starting encoding process...")

            ' Output full process and arguments to logfile
            sub_DebugMessage()
            sub_DebugMessage(str_AppFolder & "\" & _str_AppExecutable & " " & _str_AppArguments)
            sub_DebugMessage()

            ' Process Start
            _process = New Process
            Dim _processInfo As New ProcessStartInfo(str_AppFolder & "\" & _str_AppExecutable, _str_AppArguments)
            _processInfo.WorkingDirectory = str_AppFolder
            _processInfo.UseShellExecute = False
            _processInfo.RedirectStandardError = True
            _processInfo.CreateNoWindow = True
            _processInfo.WindowStyle = ProcessWindowStyle.Normal

            _process.StartInfo = _processInfo

            ' Start the Encoding Process
            _process.Start()
            _process.PriorityClass = _processPriority

            ' Set the starting time
            _dtm_EncodingTimeStart = Now

            ' Monitor the progress
            Dim strProgress As String = Nothing
            Do Until _process.StandardError.EndOfStream
                strProgress = _process.StandardError.ReadLine
                _sub_CheckProgress(strProgress)
            Loop

            ' End the process
            _int_ProcessExitCode = _process.ExitCode
            _process.Close()

            ' Verify the exit code is 0
            If Not _int_ProcessExitCode = 0 Then
                Throw New Exception(strProgress & " (Exit Code: " & _int_ProcessExitCode & ")")
            End If

        Catch exNullReference As NullReferenceException
            _bln_ProcessCancelled = True
        Catch ex As Exception
            sub_DebugMessage(_str_AppExecutable & " Error while encoding '" & _str_EncoderInputFile & "'. " & vbCr & vbCr & ex.Message, True, MsgBoxStyle.Critical, True)
            _bln_ProcessFailed = True
        Finally
            ' If we haven't forcefully cancelled, then raise events
            If Not _bln_ProcessCancelled Then
                RaiseEvent Finished()
            End If
        End Try

    End Sub

    Private Sub _sub_CheckProgress(ByVal strProgress As String)

        If bln_EncodingInProgress Then

            _str_Progress = strProgress

            ' Verify we've got a video stream
            If strProgress.ToUpper.Contains("STREAM") And strProgress.ToUpper.Contains("VIDEO") Then
                _bln_EncodingVideoStreamDetected = True
            End If
            ' Wait until we're mapping the streams to verify
            If strProgress.ToUpper.Contains("STREAM") And strProgress.ToUpper.Contains("MAPPING") Then
                If Not _bln_EncodingVideoStreamDetected Then
                    Throw New Exception("No Video Stream was detected")
                End If
            End If

            ' Get the current time position and percent complete in encoding and display as output
            If strProgress.ToUpper.Contains("TIME=") Then
                Dim _str_EncodingTime As String = strProgress.Trim.Substring(strProgress.ToUpper.IndexOf("TIME=") + 5)
                _str_EncodingTime = _str_EncodingTime.Remove(_str_EncodingTime.IndexOf(" "), _str_EncodingTime.Length - _str_EncodingTime.IndexOf(" "))
                _int_EncodingTimeCurrentPosition = CInt(FormatNumber(_str_EncodingTime.Replace(".", strLocaleDecimal), 0))
                _int_PercentComplete = CInt(_int_EncodingTimeCurrentPosition / _int_EncoderVideoDuration * 100)
            End If

            ' Get the current file size (so we can determine if the file is encoding properly)
            If strProgress.ToUpper.Contains("SIZE=") And Not strProgress.ToUpper.Contains("LIBX264 @") Then
                Dim _str_EncodingSize As String = strProgress.Trim.Substring(strProgress.ToUpper.IndexOf("SIZE=") + 5, 8)
                Dim _int_encodingSize As Integer = CInt(_str_EncodingSize.ToUpper.Trim.Replace("KB", Nothing).Replace(".", Nothing))
                If _int_encodingSize = 0 Then
                    _int_EncodingZeroFileSizeCount = _int_EncodingZeroFileSizeCount + 1
                End If
                ' After 20 checks, fail
                If _int_EncodingZeroFileSizeCount > 20 Then
                    Throw New Exception("Encoding has started but the filesize is not increasing. This may be as a result of an unsupported video or audio codec.")
                End If
            End If

            ' Display all progress information
            sub_DebugMessage(strProgress)

            ' If FFmpeg has encountered an error, throw a new exception
            'If strProgress.ToUpper.Contains("ERROR") Or strProgress.ToUpper.Contains("NULL @ ") Then
            '    Throw New Exception(strProgress)
            'End If

            RaiseEvent ProgressUpdate()

        End If

    End Sub

#End Region

#Region "Functions"

    Private Function _func_GetTimeRemaining() As Double
        If Not _int_PercentComplete = 0 Then
            Dim _etaTimeDiff = (Now - _dtm_EncodingTimeStart).TotalSeconds
            Dim _etaTimeForPercent = _etaTimeDiff / _int_PercentComplete
            Dim _etaTimeTotal = _etaTimeForPercent * 100
            Dim _etaTimeRemaining = Math.Round((_etaTimeTotal - _etaTimeDiff) / 60, 2)
            Return _etaTimeRemaining
        End If
        Return 0
    End Function

    Private Function _func_ReplaceEncoderArguments(ByVal str_EncoderArguments As String, ByVal str_ReplacementEncoderArguments As String) As String

        ' Divide our existing items into new arrays
        Dim _arr_EncoderArguments As New ArrayList
        For Each value As String In str_EncoderArguments.Split(CChar(" "))
            _arr_EncoderArguments.Add(value)
        Next
        Dim _arr_EncoderAdvancedFlags As New ArrayList
        For Each value As String In str_ReplacementEncoderArguments.Split(CChar(" "))
            _arr_EncoderAdvancedFlags.Add(value)
        Next

        ' Set up our output array and clone from the existing
        Dim _arr_EncoderNewList As New ArrayList
        _arr_EncoderNewList = CType(_arr_EncoderArguments.Clone, ArrayList)

        ' Parse through the replacement list, and replace any flags that are necessary
        For Each _str_EncoderAdvancedFlagsTemp As String In _arr_EncoderAdvancedFlags
            For Each _str_EncoderArgumentsTemp As String In _arr_EncoderArguments
                If _str_EncoderAdvancedFlagsTemp = _str_EncoderArgumentsTemp Then
                    Dim _int_EncoderAdvancedFlagsTempIndex As Integer = _arr_EncoderAdvancedFlags.IndexOf(_str_EncoderAdvancedFlagsTemp) + 1
                    Dim _int_EncoderArgumentsTempIndex As Integer = _arr_EncoderArguments.IndexOf(_str_EncoderArgumentsTemp) + 1
                    _arr_EncoderNewList(_int_EncoderArgumentsTempIndex) = _arr_EncoderAdvancedFlags(_int_EncoderAdvancedFlagsTempIndex)
                End If
            Next
        Next

        ' Insert additional flags, ensuring there are no duplicates
        Dim _bln_2PartFlagWritten As Boolean = False
        For _int_EncoderAdvancedFlagsTemp As Integer = 0 To (_arr_EncoderAdvancedFlags.Count - 1)
            Dim _bln_2PartFlag As Boolean = True
            If Not _arr_EncoderNewList.Contains(_arr_EncoderAdvancedFlags(_int_EncoderAdvancedFlagsTemp)) Then
                ' Verify this is a 2 part flag (ie, -br 5000k)
                If Not _int_EncoderAdvancedFlagsTemp = _arr_EncoderAdvancedFlags.Count - 1 Then
                    If _arr_EncoderAdvancedFlags(_int_EncoderAdvancedFlagsTemp + 1).ToString.StartsWith("-") Then
                        _bln_2PartFlag = False
                    End If
                Else
                    _bln_2PartFlag = False
                End If
                ' If this is a 2 part flag, write both parts
                If _bln_2PartFlag Then
                    _arr_EncoderNewList.Add(CStr(_arr_EncoderAdvancedFlags(_int_EncoderAdvancedFlagsTemp)) & " " & CStr(_arr_EncoderAdvancedFlags(_int_EncoderAdvancedFlagsTemp + 1)))
                    _bln_2PartFlagWritten = True
                    ' Otherwise, just write the one
                Else
                    ' Skip writing the flag if it is the 2nd part of a 2 part flag
                    If _bln_2PartFlagWritten Then
                        _bln_2PartFlagWritten = False
                    Else
                        _arr_EncoderNewList.Add(_arr_EncoderAdvancedFlags(_int_EncoderAdvancedFlagsTemp))
                    End If
                End If
            End If
        Next

        ' Return the new list of arguments
        _func_ReplaceEncoderArguments = Join(_arr_EncoderNewList.ToArray, " ")

    End Function

#End Region

End Class
