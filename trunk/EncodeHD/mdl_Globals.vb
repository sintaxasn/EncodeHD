Imports System.IO
Imports Snarl

Module mdl_Globals

    ' Locale Settings
    Public strLocaleDecimal As String = Mid(CStr(11 / 10), 2, 1)
    Public strLocaleComma As String = Chr(90 - Asc(strLocaleDecimal))

    ' OS Information
    Public str_OSFullName As String = My.Computer.Info.OSFullName
    Public str_OSVersion As String = My.Computer.Info.OSVersion
    Public str_OSArchitecture As String = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE")

    ' Application Information
    Public arr_RequiredComponents() As String = {"ffmpeg.exe", "mediainfo.dll", "snarlconnector.dll", "atomicparsley.exe", "mp4box.exe", "js32.dll", "zlib1.dll"}
    Public arr_RequiredNonGPLComponents() As String = {"libfaac.dll"}

    Public str_AppFolder As String = My.Application.Info.DirectoryPath
    Public str_LogFolder As String = Path.Combine(My.Computer.FileSystem.SpecialDirectories.MyDocuments, "EncodeHD Log Files")
    Public str_LogFile As String = Path.Combine(str_LogFolder, My.Resources.App_Title & "_" & Date.Today.Year & Right("0" & Date.Today.Month, 2) & Right("0" & Date.Today.Day, 2) & ".Log")

    Public bln_SnarlInstalledAndWorking As Boolean = False
    Public str_SnarlIconPath As String = Path.Combine(str_AppFolder, My.Resources.App_Title & ".ICO")

    Public fvi_AppVersion As FileVersionInfo = FileVersionInfo.GetVersionInfo(Application.ExecutablePath)

    Public bln_AppStartup As Boolean = False

    ' Supported Formats List
    Public arrSupportedMediaFormats() As String = {".ASF", ".AVI", ".DIVX", ".DVR-MS", ".FLV", ".M2V", ".M4V", ".MKV", ".MOV", ".MP4", ".MPG", ".MPEG", ".MTS", ".M2T", ".M2TS", ".OGM", ".OGG", ".RM", ".RMVB", ".TS", ".VOB", ".WMV", ".XVID"}

    ' Object Setup
    Public obj_LogFile As System.IO.TextWriter

    ' Get Settings from .Exe.Settings file
    Public bln_SettingsDebugMode As Boolean = My.Settings.SettingsDebugMode
    Public bln_SettingsUIH264EncodingChecked As Boolean = My.Settings.SettingsUIH264EncodingChecked
    Public bln_SettingsUIOutputForTVChecked As Boolean = My.Settings.SettingsUIOutputForTVChecked
    Public bln_SettingsUIAC3PassthroughChecked As Boolean = My.Settings.SettingsUIAC3PassthroughChecked
    Public int_SettingsUIConversionDevice As Integer = My.Settings.SettingsUIConversionDevice
    Public str_SettingsUIOutputFolder As String = My.Settings.SettingsUIOutputFolder
    Public bln_SettingsUIAutoSplitChecked As Boolean = My.Settings.SettingsUIAutoSplitChecked
    Public bln_SettingsUIAdvancedFFmpegFlagsChecked As Boolean = My.Settings.SettingsUIAdvancedFFmpegFlagsChecked
    Public str_SettingsUIAdvancedFFmpegFlags As String = My.Settings.SettingsUIAdvancedFFmpegFlags
    Public str_SettingsUIAdvancedPreferredAudioLanguage As String = My.Settings.SettingsUIAdvancedPreferredAudioLanguage
    Public bln_SettingsUIAdvancedSoftSubsChecked As Boolean = My.Settings.SettingsUIAdvancedSoftSubsChecked

    ' Global Variables
    Public bln_EncodingInProgress As Boolean = False

    ' Session Settings
    Public bln_SettingsSessionAutoMode As Boolean = False
    Public bln_SettingsSessionStitchMode As Boolean = False
    Public str_SettingsSessionStitchFile As String = Nothing
    Public bln_SettingsSessionQuitOnFinishMode As Boolean = False
    Public bln_SettingsSessionShutdownOnFinishMode As Boolean = False
    Public str_SettingsSessionOutputFile As String = Nothing
    Public str_SettingsSessionPriority As String = "Normal"

    Public Sub sub_DebugMessage(Optional ByVal str_DebugMessage As String = Nothing, Optional ByVal bln_DisplayError As Boolean = False, Optional ByVal mbs_Style As MsgBoxStyle = MsgBoxStyle.Information, Optional ByVal bln_BugReport As Boolean = False)

        ' If we are to display an error message...
        If bln_DisplayError = True Then
            MsgBox(str_DebugMessage, CType(mbs_Style + MsgBoxStyle.OkOnly + MsgBoxStyle.MsgBoxSetForeground, MsgBoxStyle), My.Resources.App_Title)
            ' And make sure to cancel any automation
            bln_SettingsSessionAutoMode = False
            bln_SettingsSessionQuitOnFinishMode = False
        End If

        ' Write to the external logfile if running in Debug Mode
        If bln_SettingsDebugMode And Not obj_LogFile Is Nothing Then
            obj_LogFile.WriteLine(str_DebugMessage)
            obj_LogFile.Flush()
        Else
            Exit Sub
        End If

        ' Output to the Console
        Console.WriteLine(str_DebugMessage)

    End Sub

    Public Sub sub_SnarlRegister(ByVal str_RegType As String)

        ' Initialise Defaults
        Dim int_IntPtr As Integer = 0
        Dim int_SnarlTimeout As Integer = 10
        Dim ptr_SnarlClient As New IntPtr(int_IntPtr)

        Dim int_SnarlReturn As M_RESULT = Nothing

        Select Case str_RegType.ToUpper
            Case "REGISTER"
                int_SnarlReturn = SnarlConnector.RegisterConfig(ptr_SnarlClient, "EncodeHD", WindowsMessage.WM_ACTIVATEAPP, Path.Combine(str_AppFolder, My.Resources.App_Title & ".PNG"))
            Case "UNREGISTER"
                int_SnarlReturn = SnarlConnector.RevokeConfig(ptr_SnarlClient)
        End Select


    End Sub

    Public Sub sub_SnarlMessage(ByVal strMessage As String)

        ' Initialise Defaults
        Dim int_IntPtr As Integer = 0
        Dim int_SnarlTimeout As Integer = 10
        Dim ptr_SnarlClient As New IntPtr(int_IntPtr)

        ' Display Snarl Message
        Dim int_SnarlReturn As Integer = SnarlConnector.ShowMessage(My.Resources.App_Title, strMessage, int_SnarlTimeout, str_SnarlIconPath, ptr_SnarlClient, WindowsMessage.WM_ACTIVATEAPP)

    End Sub

    Public Sub sub_AppInitialise()

        sub_DebugMessage()
        sub_DebugMessage("* Application Initialisation *")

        ' Create the logfile if in Debug mode, otherwise, just output to the console...
        If bln_SettingsDebugMode Then
            sub_DebugMessage("Running in Debug Mode")
            Try
                ' Verify the Log Folder Exists, if not, create it
                If Not My.Computer.FileSystem.DirectoryExists(str_LogFolder) Then
                    My.Computer.FileSystem.CreateDirectory(str_LogFolder)
                End If

                ' Connect to the logfile
                Try
                    sub_DebugMessage("Initialising logfile...")
                    obj_LogFile = My.Computer.FileSystem.OpenTextFileWriter(str_LogFile, True)
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try

            Catch ex As Exception
                MessageBox.Show("Unable to create Debug log file: " & ex.Message & ". Debugging switched off", My.Resources.App_Title, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                bln_SettingsDebugMode = False
            End Try
        End If

        ' Register Snarl
        sub_SnarlRegister("Register")

        sub_DebugMessage("----------------------------------------------------------------------------------")
        sub_DebugMessage(My.Resources.App_Title & " " & fvi_AppVersion.FileVersion & " - " & My.Resources.App_Company)
        frm_Main.Text = My.Resources.App_Title & " " & fvi_AppVersion.FileVersion

        sub_DebugMessage()
        sub_DebugMessage("* Debug Info *")
        sub_DebugMessage("OS Full Name: " & str_OSFullName)
        sub_DebugMessage("OS Version: " & str_OSVersion)
        sub_DebugMessage("OS Architecture: " & str_OSArchitecture)
        sub_DebugMessage("Regional Decimal: " & strLocaleDecimal)
        sub_DebugMessage("Regional Comma: " & strLocaleComma)

    End Sub

    Public Sub sub_AppShutdown(ByVal int_ExitCode As Integer)

        sub_DebugMessage()
        sub_DebugMessage("* Application Shutdown *")

        ' Save Settings
        sub_DebugMessage("Saving Settings...")
        My.Settings.SettingsUIH264EncodingChecked = bln_SettingsUIH264EncodingChecked
        My.Settings.SettingsUIOutputForTVChecked = bln_SettingsUIOutputForTVChecked
        My.Settings.SettingsUIAC3PassthroughChecked = bln_SettingsUIAC3PassthroughChecked
        My.Settings.SettingsUIConversionDevice = int_SettingsUIConversionDevice
        My.Settings.SettingsUIOutputFolder = str_SettingsUIOutputFolder
        My.Settings.SettingsUIAutoSplitChecked = bln_SettingsUIAutoSplitChecked
        My.Settings.SettingsUIAdvancedFFmpegFlagsChecked = bln_SettingsUIAdvancedFFmpegFlagsChecked
        My.Settings.SettingsUIAdvancedFFmpegFlags = str_SettingsUIAdvancedFFmpegFlags
        My.Settings.SettingsUIAdvancedPreferredAudioLanguage = str_SettingsUIAdvancedPreferredAudioLanguage
        My.Settings.Save()

        ' Close the logfile
        If bln_SettingsDebugMode Then
            sub_DebugMessage("Closing Logfile...")
            sub_DebugMessage()
            obj_LogFile.Close()
            obj_LogFile = Nothing
        End If

        ' UnRegister Snarl
        sub_SnarlRegister("UnRegister")

        sub_DebugMessage("Exiting Application with Exit Code: " & int_ExitCode, False, CType(True, MsgBoxStyle))
        System.Environment.Exit(int_ExitCode)

    End Sub

End Module


