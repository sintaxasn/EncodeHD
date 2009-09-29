Imports System.IO
Imports System.Threading

Public Class cls_MediaTagger

#Region "Declarations"

    ' Setup the thread objects
    Private _thread As Thread
    Private _process As Process

    Private _int_ProcessExitCode As Integer = 0
    Private _bln_ProcessCancelled As Boolean = False
    Private _str_Progress As String = Nothing
    Private _int_PercentComplete As Integer = Nothing

    ' Set the MP4 Tagging application variables
    Private _str_AppExecutable As String = "AtomicParsley.Exe"
    Private _str_InputFile As String = Nothing
    Private _str_OutputFileiUUID As String = Nothing

    Private arrSupportedCoverArtFormats() As String = {"PNG", "JPG"}
    Private _str_OutputFileArtwork As String = Nothing

#End Region

#Region "Properties"

    Public Property InputFile() As String
        Get
            Return _str_InputFile
        End Get
        Set(ByVal value As String)
            _str_InputFile = value
        End Set
    End Property

    Public Property OutputFileUUID() As String
        Get
            Return _str_OutputFileiUUID
        End Get
        Set(ByVal value As String)
            _str_OutputFileiUUID = value
        End Set
    End Property

    Public ReadOnly Property ExitCode() As Integer
        Get
            Return _int_ProcessExitCode
        End Get
    End Property

    Public ReadOnly Property ProcessCancelled() As Boolean
        Get
            Return _bln_ProcessCancelled
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

#End Region

#Region "Events"

    Public Event ProgressUpdate()
    Public Event Finished()

#End Region

#Region "Subroutines"

    Public Sub SpinUp()

        Try

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

        Try

            ' Set up the basic arguments
            Dim _str_AppArgumentsTemp As String = Nothing

            ' Check to see if artwork exists for the file
            For Each _str_CoverArtFormat As String In arrSupportedCoverArtFormats
                If My.Computer.FileSystem.FileExists(Path.ChangeExtension(_str_InputFile, _str_CoverArtFormat)) Then
                    _str_OutputFileArtwork = Path.ChangeExtension(_str_InputFile, _str_CoverArtFormat)
                End If
            Next

            ' If Artwork is found, and we're not running in Stitch mode...
            If Not _str_OutputFileArtwork = Nothing And Not bln_SettingsSessionStitchMode Then
                sub_DebugMessage("Cover Art found: " & _str_OutputFileArtwork & " and will be tagged...")
                _str_AppArgumentsTemp = " --artwork """ & _str_OutputFileArtwork & """"
            End If

            ' If output is targetted at the iPod...
            If Not _str_OutputFileiUUID = Nothing Then
                sub_DebugMessage("iPod Atom will be tagged...")
                _str_AppArgumentsTemp = _str_AppArgumentsTemp & " --DeepScan --iPod-uuid " & _str_OutputFileiUUID
            End If

            ' If we have no arguments, do nothing
            If _str_AppArgumentsTemp = Nothing Then
                sub_DebugMessage("No tagging operations to perform. Skipping...")
                Exit Sub
            End If

            ' Finalise the arguments
            Dim _str_AppArguments As String = """" & _str_InputFile & """ " & _str_AppArgumentsTemp & " --overWrite"

            ' Output full process and arguments to logfile
            sub_DebugMessage()
            sub_DebugMessage(str_AppFolder & "\" & _str_AppExecutable & " " & _str_AppArguments)
            sub_DebugMessage()

            ' Process Start
            _process = New Process
            Dim _processInfo As New ProcessStartInfo(str_AppFolder & "\" & _str_AppExecutable, _str_AppArguments)
            _processInfo.WorkingDirectory = str_AppFolder
            _processInfo.UseShellExecute = False
            _processInfo.RedirectStandardOutput = True
            _processInfo.CreateNoWindow = True
            _processInfo.WindowStyle = ProcessWindowStyle.Normal

            _process.StartInfo = _processInfo

            ' Start the Process
            _process.Start()

            ' Monitor the progress
            Dim strProgress As String = Nothing
            Do Until _process.StandardOutput.EndOfStream
                strProgress = _process.StandardOutput.ReadLine
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
            sub_DebugMessage(_str_AppExecutable & " Error: " & ex.Message, True, MsgBoxStyle.Critical, True)
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

            ' Display all progress information
            sub_DebugMessage(strProgress)

            RaiseEvent ProgressUpdate()

        End If

    End Sub

#End Region

End Class
