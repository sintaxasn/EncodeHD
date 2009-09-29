Imports System.IO
Imports System.Threading

Public Class cls_MediaSplitter

#Region "Declarations"

    ' Setup the thread objects
    Private _thread As Thread
    Private _process As Process

    Private _int_ProcessExitCode As Integer = 0
    Private _bln_ProcessCancelled As Boolean = False
    Private _str_Progress As String = Nothing
    Private _int_PercentComplete As Integer = Nothing

    ' Set the MP4 Splitting application variables
    Private _str_AppExecutable As String = "MP4Box.Exe"
    Private _str_InputFile As String = Nothing

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

            ' If AutoSplit is enabled
            If bln_SettingsUIAutoSplitChecked Then

                ' Process Start
                sub_DebugMessage("")
                sub_DebugMessage("Checking filesize for AutoSplit...")

                If My.Computer.FileSystem.GetFileInfo(_str_InputFile).Length > 4294967296 Then

                    sub_DebugMessage("File is larger than 4GB (" & My.Computer.FileSystem.GetFileInfo(_str_InputFile).Length & "). Splitting...")

                    Dim _str_AppArguments As String = " -splits 4000000 """ & _str_InputFile & """"

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

                    ' Delete the original input file
                    sub_DebugMessage("Removing Temporary File: " & _str_InputFile)
                    My.Computer.FileSystem.DeleteFile(_str_InputFile)

                Else
                    sub_DebugMessage("No need to split this file")
                End If

            Else
                sub_DebugMessage("Splitting has been disabled")
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

            ' Get the current percent complete in splitting and display as output
            If strProgress.ToUpper.Contains("SPLITTING") And strProgress.ToUpper.Contains("| (") Then
                Dim _int_PercentComplete As String = strProgress.Trim.Substring(strProgress.ToUpper.IndexOf("| (") + 3)
                _int_PercentComplete = _int_PercentComplete.Replace("/", "")
            End If

            ' Display all progress information
            sub_DebugMessage(strProgress)

            RaiseEvent ProgressUpdate()

        End If

    End Sub

#End Region

End Class
