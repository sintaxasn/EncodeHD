Imports System.IO
Imports System.Net

Public Class frm_Main

    ' Set up the classes with events
    Private WithEvents MediaEncoder As cls_MediaEncoder
    Private WithEvents MediaSubtitler As cls_MediaSubtitler
    Private WithEvents MediaTagger As cls_MediaTagger
    Private WithEvents MediaSplitter As cls_MediaSplitter
    Private EncoderParameters As New cls_MediaEncoderParameters

    ' Declarations
    Private que_FilesToProcess As Queue
    Private que_FilesToStitch As Queue

#Region "Form Events"

    ' Form Loading
    Private Sub frm_Main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        sub_AppInitialise()

        sub_DebugMessage()
        sub_DebugMessage("* Form Startup Events *")

        bln_AppStartup = True

    End Sub

    ' Form Shown
    Private Sub frm_Main_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Shown

        sub_DebugMessage()
        sub_DebugMessage("* Form Shown Events *")

        ' Set the initial column width in relation to the listview
        clm_Files.Width = listView.Width - 4

        ' Validate Components
        sub_InitValidateComponents()

        ' Check if this is the first run - Removed since it was mainly a mechanism to get people to donate, which they didn't :\
        ' sub_InitCheckFirstRun()

        ' Parse the command-line
        sub_InitParseCommandLine()

        ' Set up form values
        cbx_H264Encoding.Checked = bln_SettingsUIH264EncodingChecked
        cbx_StreamCopy.Checked = bln_SettingsUIStreamCopyChecked
        cbx_TVOutput.Checked = bln_SettingsUIOutputForTVChecked
        cbx_AC3Passthrough.Checked = bln_SettingsUIAC3PassthroughChecked
        cbx_ConversionDevice.SelectedIndex = int_SettingsUIConversionDevice
        If Not str_SettingsUIOutputFolder = Nothing Then
            cbx_OutputFolder.Checked = True
            toolTip.SetToolTip(cbx_OutputFolder, "Output Folder: " & str_SettingsUIOutputFolder)
        End If
        cbx_AutoSplit4GB.Checked = bln_SettingsUIAutoSplitChecked

        ' Reset form to defaults
        sub_SupportUpdateFormControls("DEFAULT")

        ' Form Startup Events have complete
        bln_AppStartup = False

        ' Automatic Mode
        If bln_SettingsSessionAutoMode Then
            btn_StartStop.PerformClick()
        End If

    End Sub

    ' Form Closing
    Private Sub frm_Main_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing

        sub_DebugMessage()
        sub_DebugMessage("* Form Closing Events *")

        ' Transfer form values for saving
        bln_SettingsUIH264EncodingChecked = cbx_H264Encoding.Checked
        bln_SettingsUIStreamCopyChecked = cbx_StreamCopy.Checked
        bln_SettingsUIOutputForTVChecked = cbx_TVOutput.Checked
        bln_SettingsUIAC3PassthroughChecked = cbx_AC3Passthrough.Checked
        int_SettingsUIConversionDevice = cbx_ConversionDevice.SelectedIndex

        'Exit the application
        sub_AppShutdown(CInt(My.Resources.ExitCode_Success))

    End Sub

    ' Form Resizing
    Private Sub frm_Main_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize

        ' Maintain the column width in relation to the listview
        clm_Files.Width = listView.Width - 4

    End Sub

    ' Form Drag / Drop
    Private Sub frm_Main_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragDrop, listView.DragDrop

        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then

            ' Add the files to the list
            Dim arrfiles As String() = CType(e.Data.GetData("FileDrop"), String())
            For Each strFile In arrfiles
                ' Get the file extension
                Dim strExtension As String = Path.GetExtension(strFile)
                ' Check if it is supported
                Dim blnFileSupported As Boolean = False
                For Each strFormat As String In arrSupportedMediaFormats
                    If strFormat.ToUpper = strExtension.ToUpper Then
                        blnFileSupported = True
                    End If
                Next
                ' If yes, add the file, otherwise ignore
                If blnFileSupported Then
                    sub_DebugMessage("Adding File: " & strFile)
                    listView.Items.Add(strFile)
                Else
                    sub_DebugMessage("Ignoring File: " & strFile)
                End If

            Next

        End If

    End Sub

    ' Form Drag Enter
    Private Sub frm_Main_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragEnter, listView.DragEnter

        ' If a file is being dragged onto the form, change the mouse icon
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If

    End Sub

#End Region

#Region "Button Events"

    ' Add File(s) Button Click
    Private Sub btn_AddFiles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_AddFiles.Click

        sub_DebugMessage()
        sub_DebugMessage("* Add Files *")

        ' Display Selection Dialog
        dlg_OpenFile.Filter = "Supported Video Files|*" & Join(arrSupportedMediaFormats, ";*") & "|" & "All Files|*.*"
        If dlg_OpenFile.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            For Each strFile As String In dlg_OpenFile.FileNames
                listView.Items.Add(strFile)
                sub_DebugMessage("Adding File: " & strFile)
            Next
        Else
            Exit Sub
        End If

    End Sub

    ' Remove File(s) Button Click
    Private Sub btn_RemoveFiles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_RemoveFiles.Click

        sub_DebugMessage()
        sub_DebugMessage("* Remove Files *")

        ' For each item selected...
        For Each item As ListViewItem In listView.SelectedItems
            ' Remove it from the ListView
            listView.Items.Remove(item)
            sub_DebugMessage("Removing File: " & item.Text)
        Next

    End Sub

    ' Clear All Button Click
    Private Sub button_ClearAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_ClearAll.Click

        sub_DebugMessage()
        sub_DebugMessage("* Clear All *")

        ' Clear the ListView
        listView.Items.Clear()

    End Sub

    ' Advanced Button Click
    Private Sub btn_Advanced_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Advanced.Click

        frm_Advanced.ShowDialog()

    End Sub

    ' Start / Stop Button Click
    Private Sub btn_StartStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_StartStop.Click

        sub_DebugMessage()
        sub_DebugMessage("* Start / Stop Encoding *")

        If bln_EncodingInProgress Then
            If Not MediaEncoder Is Nothing Then
                ' Force the encoding to finish
                sub_EncodingAbort()
            End If
        Else
            ' Verify the listbox is populated
            If listView.Items.Count = 0 Then Exit Sub

            ' If running in Stitch Mode...
            If bln_SettingsSessionStitchMode Then
                ' Verify we have more than 1 file
                If listView.Items.Count < 2 Then
                    sub_DebugMessage("Stitching requires at least two files", True, MsgBoxStyle.Exclamation)
                    Exit Sub
                End If
                ' If no Stitch file has been specified, prompt for one
                If str_SettingsSessionStitchFile = Nothing Then
                    str_SettingsSessionStitchFile = InputBox("Please enter the output filename for the stitched file", "Stitched Output Filename", "StitchedFile")
                    If Not str_SettingsSessionStitchFile = Nothing Then
                        sub_DebugMessage("Stitched Output Filename: " & str_SettingsSessionStitchFile)
                    Else
                        sub_DebugMessage("No filename was entered", True, MsgBoxStyle.Exclamation)
                        Exit Sub
                    End If

                End If
            End If

            ' Prepare the queues
            que_FilesToProcess = New Queue
            que_FilesToStitch = New Queue

            ' Queue each file in the list
            For Each item As ListViewItem In listView.Items
                Dim strFile As String = item.Text
                que_FilesToProcess.Enqueue(strFile)
            Next

            ' Start encoding the files
            sub_EncodeFile(CStr(que_FilesToProcess.Dequeue))
        End If

    End Sub

#End Region

#Region "UI State Change Events"

    ' Conversion Type Combobox Change
    Private Sub cbx_ConversionType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbx_ConversionDevice.SelectedIndexChanged

        sub_DebugMessage()
        sub_DebugMessage("* Conversion Type ComboBox Index Changed *")

        Select Case cbx_ConversionDevice.SelectedItem.ToString
            Case "Apple TV"
                EncoderParameters.Output_Device = cls_MediaEncoderParameters.DEVICE_NAME_APPLETV
            Case "BlackBerry (8100) Pearl"
                EncoderParameters.Output_Device = cls_MediaEncoderParameters.DEVICE_NAME_BLACKBERRY8100
            Case "BlackBerry (8200) Kickstart"
                EncoderParameters.Output_Device = cls_MediaEncoderParameters.DEVICE_NAME_BLACKBERRY8200
            Case "BlackBerry (8300) Curve"
                EncoderParameters.Output_Device = cls_MediaEncoderParameters.DEVICE_NAME_BLACKBERRY8300
            Case "BlackBerry (8700) Electron"
                EncoderParameters.Output_Device = cls_MediaEncoderParameters.DEVICE_NAME_BLACKBERRY8700
            Case "BlackBerry (8800) Indigo"
                EncoderParameters.Output_Device = cls_MediaEncoderParameters.DEVICE_NAME_BLACKBERRY8800
            Case "BlackBerry (8900) Javelin"
                EncoderParameters.Output_Device = cls_MediaEncoderParameters.DEVICE_NAME_BLACKBERRY8900
            Case "BlackBerry (9000) Bold"
                EncoderParameters.Output_Device = cls_MediaEncoderParameters.DEVICE_NAME_BLACKBERRY9000
            Case "BlackBerry (9500) Storm"
                EncoderParameters.Output_Device = cls_MediaEncoderParameters.DEVICE_NAME_BLACKBERRY9500
            Case "HTC Desire"
                EncoderParameters.Output_Device = cls_MediaEncoderParameters.DEVICE_NAME_HTCDESIRE
            Case "HTC EVO 4G"
                EncoderParameters.Output_Device = cls_MediaEncoderParameters.DEVICE_NAME_HTCEVO4G
            Case "iPad"
                EncoderParameters.Output_Device = cls_MediaEncoderParameters.DEVICE_NAME_IPAD
            Case "iPhone"
                EncoderParameters.Output_Device = cls_MediaEncoderParameters.DEVICE_NAME_IPHONE
            Case "iPhone 4"
                EncoderParameters.Output_Device = cls_MediaEncoderParameters.DEVICE_NAME_IPHONE4
            Case "iPod 5G"
                EncoderParameters.Output_Device = cls_MediaEncoderParameters.DEVICE_NAME_IPOD5G
            Case "iPod Classic"
                EncoderParameters.Output_Device = cls_MediaEncoderParameters.DEVICE_NAME_IPODCLASSIC
            Case "iPod Nano"
                EncoderParameters.Output_Device = cls_MediaEncoderParameters.DEVICE_NAME_IPODNANO
            Case "iPod Touch"
                EncoderParameters.Output_Device = cls_MediaEncoderParameters.DEVICE_NAME_IPODTOUCH
            Case "Nexus One"
                EncoderParameters.Output_Device = cls_MediaEncoderParameters.DEVICE_NAME_NEXUSONE
            Case "Nokia E71"
                EncoderParameters.Output_Device = cls_MediaEncoderParameters.DEVICE_NAME_NOKIAE71
            Case "Nokia N900"
                EncoderParameters.Output_Device = cls_MediaEncoderParameters.DEVICE_NAME_NOKIAN900
            Case "PlayStation 3"
                EncoderParameters.Output_Device = cls_MediaEncoderParameters.DEVICE_NAME_PLAYSTATION3
            Case "PSP"
                EncoderParameters.Output_Device = cls_MediaEncoderParameters.DEVICE_NAME_PSP
            Case "T-Mobile G1"
                EncoderParameters.Output_Device = cls_MediaEncoderParameters.DEVICE_NAME_TMOBILEG1
            Case "WD TV"
                EncoderParameters.Output_Device = cls_MediaEncoderParameters.DEVICE_NAME_WESTERNDIGITALDV
            Case "Xbox 360"
                EncoderParameters.Output_Device = cls_MediaEncoderParameters.DEVICE_NAME_XBOX360
            Case "Youtube HD"
                EncoderParameters.Output_Device = cls_MediaEncoderParameters.DEVICE_NAME_YOUTUBEHD
            Case "Zune"
                EncoderParameters.Output_Device = cls_MediaEncoderParameters.DEVICE_NAME_ZUNE
            Case "ZuneHD"
                EncoderParameters.Output_Device = cls_MediaEncoderParameters.DEVICE_NAME_ZUNEHD
        End Select

        sub_DebugMessage("Output Device: " & EncoderParameters.Output_Device)

        ' If H.264 Encoding is not available, disable the Checkbox option
        If EncoderParameters.Output_Video_Codec_H264Supported Then
            sub_DebugMessage("H.264 Encoding is supported on this device type")
            cbx_H264Encoding.Enabled = True
            cbx_StreamCopy.Enabled = True
        Else
            sub_DebugMessage("H.264 Encoding is not supported on this device type")
            cbx_H264Encoding.Enabled = False
            cbx_StreamCopy.Enabled = False
        End If

        ' If Formatting for TV is not available, disable the Checkbox option
        If EncoderParameters.Output_Video_Codec_TVOutputSupported Then
            sub_DebugMessage("Formatting for TV is supported on this device type")
            cbx_TVOutput.Enabled = True
        Else
            sub_DebugMessage("Formatting for TV is not supported on this device type")
            cbx_TVOutput.Enabled = False
        End If

        ' If AC3Passthrough is not available, disable the Checkbox option
        If EncoderParameters.Output_Audio_Codec_AC3PassThroughSupported Then
            sub_DebugMessage("AC3 Passthrough is supported on this device type")
            cbx_AC3Passthrough.Enabled = True
        Else
            sub_DebugMessage("AC3 Passthrough is not supported on this device type")
            cbx_AC3Passthrough.Enabled = False
        End If

    End Sub

    ' Output Folder Checkbox Change
    Private Sub cbx_OutputFolder_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbx_OutputFolder.CheckedChanged

        sub_DebugMessage()
        sub_DebugMessage("* Specific Output Folder Check Changed *")

        If cbx_OutputFolder.Checked And str_SettingsUIOutputFolder = Nothing Then
            ' If the  folder to create files in was selected successfully, set the path
            If dlg_OpenFolder.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                str_SettingsUIOutputFolder = dlg_OpenFolder.SelectedPath
                toolTip.SetToolTip(cbx_OutputFolder, "Output Folder: " & str_SettingsUIOutputFolder)
                sub_DebugMessage("Output Folder Enabled: " & str_SettingsUIOutputFolder)
            Else
                cbx_OutputFolder.Checked = False
                sub_DebugMessage("Output Folder Cancelled")
                Exit Sub
            End If
        ElseIf Not cbx_OutputFolder.Checked Then
            str_SettingsUIOutputFolder = Nothing
            toolTip.SetToolTip(cbx_OutputFolder, Nothing)
            sub_DebugMessage("Output Folder Disabled")
        End If

    End Sub

    ' AutoSplit Checkbox Change
    Private Sub cbx_AutoSplit4GB_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbx_AutoSplit4GB.CheckedChanged

        sub_DebugMessage()
        sub_DebugMessage("* AutoSplit Check Changed *")

        If cbx_AutoSplit4GB.Checked Then
            bln_SettingsUIAutoSplitChecked = True
            sub_DebugMessage("AutoSplit Enabled")
        Else
            bln_SettingsUIAutoSplitChecked = False
            sub_DebugMessage("AutoSplit Disabled")
        End If

    End Sub

    ' Update Form Controls
    Private Sub sub_SupportUpdateFormControls(ByVal strLayout As String)

        sub_DebugMessage()
        sub_DebugMessage("* Update Form Controls *")

        Select Case UCase(strLayout)
            Case "DEFAULT"
                sub_DebugMessage("Resetting form to default values...")
                pgb_Progress.Visible = False
                pgb_Progress.Value = 0
                btn_AddFiles.Enabled = True
                btn_RemoveFiles.Enabled = True
                btn_ClearAll.Enabled = True
                cbx_ConversionDevice.Enabled = True
                cbx_OutputFolder.Enabled = True
                cbx_AutoSplit4GB.Enabled = True
                listView.AllowDrop = True
                btn_Advanced.Enabled = True
                btn_StartStop.Text = "&Start"
                toolStripStatusLabel.Text = ""
                If EncoderParameters.Output_Video_Codec_H264Supported Then
                    cbx_H264Encoding.Enabled = True
                    cbx_StreamCopy.Enabled = True
                Else
                    cbx_H264Encoding.Checked = False
                    cbx_H264Encoding.Enabled = False
                    cbx_StreamCopy.Enabled = False
                End If
                If EncoderParameters.Output_Video_Codec_TVOutputSupported Then
                    cbx_TVOutput.Enabled = True
                Else
                    cbx_TVOutput.Checked = False
                    cbx_TVOutput.Enabled = False
                End If
                If EncoderParameters.Output_Audio_Codec_AC3PassThroughSupported Then
                    cbx_AC3Passthrough.Enabled = True
                Else
                    cbx_AC3Passthrough.Checked = False
                    cbx_AC3Passthrough.Enabled = False
                End If

            Case "ENCODING"
                sub_DebugMessage("Setting up form for Encoding...")
                pgb_Progress.Visible = True
                btn_AddFiles.Enabled = False
                btn_RemoveFiles.Enabled = False
                btn_ClearAll.Enabled = False
                cbx_ConversionDevice.Enabled = False
                cbx_H264Encoding.Enabled = False
                cbx_StreamCopy.Enabled = False
                cbx_TVOutput.Enabled = False
                cbx_AC3Passthrough.Enabled = False
                cbx_OutputFolder.Enabled = False
                cbx_AutoSplit4GB.Enabled = False
                listView.AllowDrop = False
                btn_Advanced.Enabled = False
                btn_StartStop.Text = "&Stop"
                toolStripStatusLabel.Text = "Preparing to encode..."

        End Select

        ' Refresh the form
        Application.DoEvents()

    End Sub

#End Region

#Region "Media Encoder Class Handled Events"

    Private Sub sub_MediaEncoderProgressMonitor() Handles MediaEncoder.ProgressUpdate

        Try

            ' Handle thread invoking so that the UI can be updated cross-thread
            If Me.InvokeRequired Then
                Me.Invoke(New MethodInvoker(AddressOf sub_MediaEncoderProgressMonitor))
            Else

                ' If the progress hasn't changed, then don't update it
                If pgb_Progress.Value = MediaEncoder.PercentComplete And MediaEncoder.PercentComplete <> 0 Then
                    Exit Sub
                    ' Verify the percent complete hasn't gone over 100%
                ElseIf MediaEncoder.PercentComplete <= 100 Then
                    ' Update the progress bar
                    pgb_Progress.Value = MediaEncoder.PercentComplete
                    ' Find the current file number in the list
                    Dim int_CurrentFileNumber As Integer = listView.FindItemWithText(MediaEncoder.InputFile).Index + 1
                    If MediaEncoder.TimeRemaining = 0 Then ' If the Time Remaining is under 0, we don't need to estimate
                        toolStripStatusLabel.Text = "Encoding File " & int_CurrentFileNumber & " of " & listView.Items.Count & " ... " & MediaEncoder.PercentComplete & "% Complete"
                    ElseIf MediaEncoder.TimeRemaining < 1 Then ' If the Time Remaining is under 1 minute, format for seconds remaining
                        toolStripStatusLabel.Text = "Encoding File " & int_CurrentFileNumber & " of " & listView.Items.Count & " ... " & MediaEncoder.PercentComplete & "% Complete. Time Remaining: " & FormatNumber((MediaEncoder.TimeRemaining * 60), 0) & " seconds"
                    ElseIf MediaEncoder.TimeRemaining >= 60 Then ' If the Time Remaining is equal/over 1 hour, format for hours and minutes remaining
                        toolStripStatusLabel.Text = "Encoding File " & int_CurrentFileNumber & " of " & listView.Items.Count & " ... " & MediaEncoder.PercentComplete & "% Complete. Time Remaining: " & FormatNumber((MediaEncoder.TimeRemaining / 60), 0) & " hour(s)" & FormatNumber((MediaEncoder.TimeRemaining Mod 60), 2, TriState.False) & " minute(s)"
                    Else ' Otherwise format for minutes remaining
                        toolStripStatusLabel.Text = "Encoding File " & int_CurrentFileNumber & " of " & listView.Items.Count & " ... " & MediaEncoder.PercentComplete & "% Complete. Time Remaining: " & FormatNumber(MediaEncoder.TimeRemaining, 0) & " minute(s)"
                    End If
                End If

            End If

        Catch ex As Exception
            MediaEncoder.Abort()
            sub_DebugMessage(ex.Message, True)
            Exit Sub
        End Try

    End Sub

    Private Sub sub_MediaEncoderFinished() Handles MediaEncoder.Finished

        Dim bln_ActionFailed As Boolean = False

        ' Handle thread invoking so that the UI can be updated cross-thread
        If Me.InvokeRequired Then
            Me.Invoke(New MethodInvoker(AddressOf sub_MediaEncoderFinished))
        Else

            sub_DebugMessage()
            sub_DebugMessage("* Finish Encoding *")

            Try

                sub_DebugMessage("Spinning down Encoder Class...")
                MediaEncoder.SpinDown()

                ' Queue the output file for stitching later
                que_FilesToStitch.Enqueue(MediaEncoder.OutputFile)

                ' Start File Subtitling
                sub_SubtitleFile(MediaEncoder.InputFile, MediaEncoder.OutputFile)

            Catch ex As Exception
                sub_DebugMessage(ex.Message, True, MsgBoxStyle.Exclamation, True)
                bln_ActionFailed = True
            Finally
                ' Clear the queue if encoding has been cancelled
                If MediaEncoder.ProcessCancelled Or MediaEncoder.ProcessFailed Or bln_ActionFailed Then
                    que_FilesToProcess.Clear()
                    que_FilesToStitch.Clear()
                End If
            End Try
        End If

    End Sub

#End Region

#Region "Media Subtitler Class Handled Events"

    Private Sub sub_MediaSubtitlerProgressMonitor() Handles MediaSubtitler.ProgressUpdate

        Try

            ' Handle thread invoking so that the UI can be updated cross-thread
            If Me.InvokeRequired Then
                Me.Invoke(New MethodInvoker(AddressOf sub_MediaSubtitlerProgressMonitor))
            Else

                ' Update the progress bar and Status text
                pgb_Progress.Value = MediaSubtitler.PercentComplete
                toolStripStatusLabel.Text = "Merging Softsubs..."
                Application.DoEvents()

            End If

        Catch ex As Exception
            MediaSubtitler.Abort()
            sub_DebugMessage(ex.Message, True)
            Exit Sub
        End Try

    End Sub

    Private Sub sub_MediaSubtitlerFinished() Handles MediaSubtitler.Finished

        ' Handle thread invoking so that the UI can be updated cross-thread
        If Me.InvokeRequired Then
            Me.Invoke(New MethodInvoker(AddressOf sub_MediaSubtitlerFinished))
        Else

            sub_DebugMessage()
            sub_DebugMessage("* Finish Merging Softsubs *")

            Try

                sub_DebugMessage("Spinning down Subtitling Class...")
                MediaSubtitler.SpinDown()

                ' Start File Tagging
                sub_TagFile(MediaEncoder.OutputFile)

            Catch ex As Exception
                sub_DebugMessage(ex.Message, True, MsgBoxStyle.Exclamation, True)

            Finally
                ' Clear the queue if encoding has been cancelled
                If MediaSubtitler.ProcessCancelled Then
                    que_FilesToProcess.Clear()
                    que_FilesToStitch.Clear()
                End If

            End Try
        End If

    End Sub

#End Region

#Region "Media Tagger Class Handled Events"

    Private Sub sub_MediaTaggerProgressMonitor() Handles MediaTagger.ProgressUpdate

        Try

            ' Handle thread invoking so that the UI can be updated cross-thread
            If Me.InvokeRequired Then
                Me.Invoke(New MethodInvoker(AddressOf sub_MediaTaggerProgressMonitor))
            Else

                ' Update the progress bar and Status text
                pgb_Progress.Value = MediaTagger.PercentComplete
                toolStripStatusLabel.Text = "Tagging File..."
                Application.DoEvents()

            End If

        Catch ex As Exception
            MediaTagger.Abort()
            sub_DebugMessage(ex.Message, True)
            Exit Sub
        End Try

    End Sub

    Private Sub sub_MediaTaggerFinished() Handles MediaTagger.Finished

        ' Handle thread invoking so that the UI can be updated cross-thread
        If Me.InvokeRequired Then
            Me.Invoke(New MethodInvoker(AddressOf sub_MediaTaggerFinished))
        Else

            sub_DebugMessage()
            sub_DebugMessage("* Finish Tagging *")

            Try

                sub_DebugMessage("Spinning down Tagging Class...")
                MediaTagger.SpinDown()

                ' Start File Splitting
                sub_SplitFile(MediaEncoder.OutputFile)

            Catch ex As Exception
                sub_DebugMessage(ex.Message, True, MsgBoxStyle.Exclamation, True)

            Finally
                ' Clear the queue if encoding has been cancelled
                If MediaTagger.ProcessCancelled Then
                    que_FilesToProcess.Clear()
                    que_FilesToStitch.Clear()
                End If

            End Try
        End If

    End Sub

#End Region

#Region "Media Splitter Class Handled Events"

    Private Sub sub_MediaSplitterProgressMonitor() Handles MediaSplitter.ProgressUpdate

        Try

            ' Handle thread invoking so that the UI can be updated cross-thread
            If Me.InvokeRequired Then
                Me.Invoke(New MethodInvoker(AddressOf sub_MediaSplitterProgressMonitor))
            Else

                ' Update the progress bar and Status text
                Application.DoEvents()

                ' If there's no progress yet, just display Status
                If MediaSplitter.PercentComplete = 0 Then
                    toolStripStatusLabel.Text = "Splitting File ..."
                    ' If the progress hasn't changed, then don't update it
                ElseIf pgb_Progress.Value = MediaSplitter.PercentComplete Then
                    Exit Sub
                    ' Verify the percent complete hasn't gone over 100%
                ElseIf MediaSplitter.PercentComplete <= 100 Then
                    ' Update the progress bar
                    pgb_Progress.Value = MediaSplitter.PercentComplete
                    ' Display the status and percent complete
                    toolStripStatusLabel.Text = "Splitting File ... " & MediaSplitter.PercentComplete & "% Complete"
                End If

            End If


        Catch ex As Exception
            MediaSplitter.Abort()
            sub_DebugMessage(ex.Message, True)
            Exit Sub
        End Try

    End Sub

    Private Sub sub_MediaSplitterFinished() Handles MediaSplitter.Finished

        ' Handle thread invoking so that the UI can be updated cross-thread
        If Me.InvokeRequired Then
            Me.Invoke(New MethodInvoker(AddressOf sub_MediaSplitterFinished))
        Else

            sub_DebugMessage()
            sub_DebugMessage("* Finish Splitting *")

            Try

                sub_DebugMessage("Spinning down Splitter Class...")
                MediaSplitter.SpinDown()

            Catch ex As Exception
                sub_DebugMessage(ex.Message, True, MsgBoxStyle.Exclamation, True)

            Finally
                ' Clear the queue if encoding has been cancelled
                If MediaSplitter.ProcessCancelled Then
                    que_FilesToProcess.Clear()
                    que_FilesToStitch.Clear()
                Else
                    ' Display Snarl Message
                    sub_SnarlMessage(Path.GetFileName(MediaEncoder.OutputFile) & " finished encoding for " & cbx_ConversionDevice.Text & " at " & FormatDateTime(DateTime.Now, DateFormat.ShortTime))
                End If


                ' Check if there's more in the queue to encode
                If Not que_FilesToProcess Is Nothing And que_FilesToProcess.Count > 0 Then
                    ' Start encoding the next file
                    sub_DebugMessage("Processing next file in the queue...")
                    sub_EncodeFile(CStr(que_FilesToProcess.Dequeue))
                Else
                    ' Stitch Videos
                    If bln_SettingsSessionStitchMode Then
                        For Each file As String In que_FilesToStitch
                            sub_DebugMessage("Stitch File: " & file)
                        Next
                    End If

                    ' Cleanup
                    que_FilesToProcess = Nothing
                    ' Restore the form controls
                    bln_EncodingInProgress = False
                    sub_SupportUpdateFormControls("DEFAULT")
                    If bln_SettingsSessionQuitOnFinishMode Then
                        ' If Quit On Finish is enabled, close the form and initialise shutdown
                        Me.Close()
                    ElseIf bln_SettingsSessionShutdownOnFinishMode Then
                        ' If Shutdown On Finish is enabled, Initiate a system shutdown
                        Process.Start("Shutdown.Exe", "/S /T 0 /C ""Shutdown initiated by EncodeHD""")
                    End If
                End If
            End Try
        End If

    End Sub


#End Region

    ' Validate against required components
    Private Sub sub_InitValidateComponents()

        sub_DebugMessage()
        sub_DebugMessage("* Validate Components *")

        Try

            ' Check for each component listed in the array
            For Each str_Component In arr_RequiredComponents
                sub_DebugMessage("Checking for component: " & str_Component)

                ' If the component is not found, throw an exception
                If Not My.Computer.FileSystem.FileExists(str_AppFolder & "\" & str_Component) Then
                    Throw New Exception("Unable to find required component: " & str_Component)
                End If
            Next

            '' Check for each component listed in the array
            'For Each str_Component In arr_RequiredNonGPLComponents
            '    sub_DebugMessage("Checking for Non-GPL component: " & str_Component)

            '    ' If the component is not found, throw an exception
            '    If Not My.Computer.FileSystem.FileExists(str_AppFolder & "\" & str_Component) Then
            '        Throw New Exception("Unable to find required Non-GPL component: " & str_Component & ". You need to download this component separately so that " & My.Resources.App_Title & " can comply with the GPL licensing terms. Please visit the " & My.Resources.App_Title & " website for more information")
            '    End If
            'Next

        Catch ex As Exception
            ' Display error and exit the application
            sub_DebugMessage(ex.Message, True)
            sub_AppShutdown(CInt(My.Resources.ExitCode_ComponentError))
        End Try

        sub_DebugMessage("All components found and valid")

    End Sub

    ' Parse the Command-line
    Private Sub sub_InitParseCommandLine()

        sub_DebugMessage()
        sub_DebugMessage("* Parse Command Line *")

        Try

            Dim col_CommandLineArguments As System.Collections.ObjectModel.ReadOnlyCollection(Of String) = My.Application.CommandLineArgs
            Dim int_Count As Integer

            ' If no arguments, continue without processing
            If Not col_CommandLineArguments.Count > 0 Then
                sub_DebugMessage("No command-line parameters specified")
                Exit Sub
            End If

            ' Enumerate all arguments
            For int_Count = 0 To col_CommandLineArguments.Count - 1
                sub_DebugMessage("Checking: " & col_CommandLineArguments(int_Count).ToUpper)
                Dim str_CommandLineMatchTemp As String = Nothing

                ' Add Files to Queue
                str_CommandLineMatchTemp = "/I:"
                If col_CommandLineArguments(int_Count).ToUpper.Contains(str_CommandLineMatchTemp) Then
                    sub_DebugMessage("Match: Add Files to Queue")

                    ' Get the Filename
                    Dim strFile As String = col_CommandLineArguments(int_Count).Remove(0, str_CommandLineMatchTemp.Length)
                    ' Get the file extension
                    Dim strExtension As String = Path.GetExtension(strFile)
                    ' Check if it is supported
                    Dim blnFileSupported As Boolean = False
                    For Each strFormat As String In arrSupportedMediaFormats
                        If strFormat.ToUpper = strExtension.ToUpper Then
                            blnFileSupported = True
                        End If
                    Next

                    ' If yes, add the file, otherwise ignore
                    If blnFileSupported Then
                        sub_DebugMessage("Adding File: " & strFile)
                        listView.Items.Add(strFile)
                    Else
                        sub_DebugMessage("Ignoring File: " & strFile)
                    End If
                    Continue For
                End If

                ' Output File Name
                str_CommandLineMatchTemp = "/O:"
                If col_CommandLineArguments(int_Count).ToUpper.Contains(str_CommandLineMatchTemp) Then
                    sub_DebugMessage("Match: Set Output File / Folder Name")

                    Dim str_OutputFileTemp As String = col_CommandLineArguments(int_Count).Remove(0, str_CommandLineMatchTemp.Length)

                    ' Get Output Directory
                    If Not Path.GetDirectoryName(Path.GetFullPath(str_OutputFileTemp)) = Nothing And str_OutputFileTemp.Contains(Path.DirectorySeparatorChar) Then
                        Dim str_OutputFileTemp2 As String = Path.GetDirectoryName(Path.GetFullPath(str_OutputFileTemp) & Path.DirectorySeparatorChar)
                        ' Workaround for handling files and folders
                        If Path.HasExtension(str_OutputFileTemp2) Then
                            str_SettingsUIOutputFolder = Path.GetDirectoryName(Path.GetFullPath(str_OutputFileTemp))
                        Else
                            str_SettingsUIOutputFolder = str_OutputFileTemp2
                        End If
                        sub_DebugMessage("Output Folder: " & str_SettingsUIOutputFolder)
                    End If

                    ' Get Output File
                    If Path.HasExtension(str_OutputFileTemp) Then
                        If bln_SettingsSessionStitchMode Or listView.Items.Count <= 1 Then
                            ' Set the output file, removing the folder
                            str_SettingsSessionOutputFile = Path.GetFileName(str_OutputFileTemp)
                            sub_DebugMessage("Output File: " & str_SettingsSessionOutputFile)
                        Else
                            sub_DebugMessage("You cannot specify the output filename unless stitching all videos together or only encoding one file", True, MsgBoxStyle.Exclamation, False)
                            Exit Sub
                        End If
                    End If
                    Continue For
                End If

                ' Automatic Mode
                str_CommandLineMatchTemp = "/AUTO"
                If col_CommandLineArguments(int_Count).ToUpper.Contains(str_CommandLineMatchTemp) Then
                    sub_DebugMessage("Match: Automatic Mode")

                    ' Ensure we have files to encode
                    If listView.Items.Count >= 1 Then
                        bln_SettingsSessionAutoMode = True
                    Else
                        sub_DebugMessage("Automatic Mode requires at least one input file be specified", True, MsgBoxStyle.Exclamation, False)
                    End If
                    Continue For
                End If

                ' Quit On Finish
                str_CommandLineMatchTemp = "/QOF"
                If col_CommandLineArguments(int_Count).ToUpper.Contains(str_CommandLineMatchTemp) Then
                    sub_DebugMessage("Match: Quit On Finish")
                    bln_SettingsSessionQuitOnFinishMode = True
                    Continue For
                End If

                ' Shutdown On Finish
                str_CommandLineMatchTemp = "/SOF"
                If col_CommandLineArguments(int_Count).ToUpper.Contains(str_CommandLineMatchTemp) Then
                    sub_DebugMessage("Match: Shutdown On Finish")
                    bln_SettingsSessionQuitOnFinishMode = True
                    Continue For
                End If

                ' Video Stitch Mode
                str_CommandLineMatchTemp = "/STITCH"
                If col_CommandLineArguments(int_Count).ToUpper.Contains(str_CommandLineMatchTemp) Then
                    sub_DebugMessage("Match: Video Stitch Mode")
                    bln_SettingsSessionStitchMode = True
                    Continue For
                End If

                ' Low Priority Encoding Mode
                str_CommandLineMatchTemp = "/LOWPRIORITY"
                If col_CommandLineArguments(int_Count).ToUpper.Contains(str_CommandLineMatchTemp) Then
                    sub_DebugMessage("Match: Low Priority Encoding Mode")
                    str_SettingsSessionPriority = "Low"
                    Continue For
                End If

                ' High Priority Encoding Mode
                str_CommandLineMatchTemp = "/HIGHPRIORITY"
                If col_CommandLineArguments(int_Count).ToUpper.Contains(str_CommandLineMatchTemp) Then
                    sub_DebugMessage("Match: High Priority Encoding Mode")
                    str_SettingsSessionPriority = "High"
                    Continue For
                End If

                ' H264 On/Off
                str_CommandLineMatchTemp = "/H264:"
                If col_CommandLineArguments(int_Count).ToUpper.Contains(str_CommandLineMatchTemp) Then
                    sub_DebugMessage("Match: Changing H264 Setting")

                    ' Get the Parameter
                    Dim strYN As String = col_CommandLineArguments(int_Count).Remove(0, str_CommandLineMatchTemp.Length)

                    ' If yes/no, change setting. Otherwise, Ignore.
                    If strYN.ToUpper = "Y" Then
                        sub_DebugMessage("Setting H264 - ON")
                        bln_SettingsUIH264EncodingChecked = True
                    ElseIf strYN.ToUpper = "N" Then
                        sub_DebugMessage("Setting H264 - OFF")
                        bln_SettingsUIH264EncodingChecked = False
                    Else
                        sub_DebugMessage("Invalid H264 Parameter - Ignoring")
                    End If
                    Continue For
                End If

                ' For TV On/Off
                str_CommandLineMatchTemp = "/TV:"
                If col_CommandLineArguments(int_Count).ToUpper.Contains(str_CommandLineMatchTemp) Then
                    sub_DebugMessage("Match: Changing TV Setting")

                    ' Get the Parameter
                    Dim strYN As String = col_CommandLineArguments(int_Count).Remove(0, str_CommandLineMatchTemp.Length)

                    ' If yes/no, change setting. Otherwise, Ignore.
                    If strYN.ToUpper = "Y" Then
                        sub_DebugMessage("Setting TV - ON")
                        bln_SettingsUIOutputForTVChecked = True
                    ElseIf strYN.ToUpper = "N" Then
                        sub_DebugMessage("Setting TV - OFF")
                        bln_SettingsUIOutputForTVChecked = False
                    Else
                        sub_DebugMessage("Invalid TV Parameter - Ignoring")
                    End If
                    Continue For
                End If

                ' Split Video On/Off
                str_CommandLineMatchTemp = "/SPLIT:"
                If col_CommandLineArguments(int_Count).ToUpper.Contains(str_CommandLineMatchTemp) Then
                    sub_DebugMessage("Match: Changing Split Setting")

                    ' Get the Parameter
                    Dim strYN As String = col_CommandLineArguments(int_Count).Remove(0, str_CommandLineMatchTemp.Length)

                    ' If yes/no, change setting. Otherwise, Ignore.
                    If strYN.ToUpper = "Y" Then
                        sub_DebugMessage("Setting Split - ON")
                        bln_SettingsUIAutoSplitChecked = True
                    ElseIf strYN.ToUpper = "N" Then
                        sub_DebugMessage("Setting Split - OFF")
                        bln_SettingsUIAutoSplitChecked = False
                    Else
                        sub_DebugMessage("Invalid Split Parameter - Ignoring")
                    End If
                    Continue For
                End If

                ' AC3 Passthrough On/Off
                str_CommandLineMatchTemp = "/AC3:"
                If col_CommandLineArguments(int_Count).ToUpper.Contains(str_CommandLineMatchTemp) Then
                    sub_DebugMessage("Match: Changing AC3 Setting")

                    ' Get the Parameter
                    Dim strYN As String = col_CommandLineArguments(int_Count).Remove(0, str_CommandLineMatchTemp.Length)

                    ' If yes/no, change setting. Otherwise, Ignore.
                    If strYN.ToUpper = "Y" Then
                        sub_DebugMessage("Setting AC3 - ON")
                        bln_SettingsUIAC3PassthroughChecked = True
                    ElseIf strYN.ToUpper = "N" Then
                        sub_DebugMessage("Setting AC3 - OFF")
                        bln_SettingsUIAC3PassthroughChecked = False
                    Else
                        sub_DebugMessage("Invalid AC3 Parameter - Ignoring")
                    End If
                    Continue For
                End If

                ' Set Conversion Device
                str_CommandLineMatchTemp = "/CD:"
                If col_CommandLineArguments(int_Count).ToUpper.Contains(str_CommandLineMatchTemp) Then
                    sub_DebugMessage("Match: Changing Conversion Device Setting")

                    ' Get the Parameter
                    Dim strCD As String = col_CommandLineArguments(int_Count).Remove(0, str_CommandLineMatchTemp.Length)

                    ' Check if valid conversion device number
                    If Convert.ToInt32(strCD) <= cbx_ConversionDevice.Items.Count Then
                        sub_DebugMessage("Setting Conversion Device - " + strCD)
                        int_SettingsUIConversionDevice = Convert.ToInt32(strCD)
                    Else ' Invalid Conversion Device
                        sub_DebugMessage("Invalid Conversion Device Parameter - " + strCD + " was supplied.")
                    End If
                    Continue For
                End If

                sub_DebugMessage()

            Next
        Catch ex As Exception
            sub_DebugMessage(ex.Message, True, MsgBoxStyle.Exclamation, True)
        End Try

    End Sub

    ' Start Encoding a File
    Private Sub sub_EncodeFile(ByVal strInputFile As String)

        sub_DebugMessage()
        sub_DebugMessage("* Start Encoding *")

        ' Determine File Properties
        Dim bln_FileValid As Boolean = CBool(func_DetermineFileProperties(strInputFile))

        If bln_FileValid Then

            bln_EncodingInProgress = True
            sub_SupportUpdateFormControls("ENCODING")

            Try

                sub_DebugMessage("Initialising Encoder class...")

                ' Initialise the class
                MediaEncoder = New cls_MediaEncoder

                ' Defaults
                MediaEncoder.OutputFileExtension = EncoderParameters.Output_Video_FileExtension
                MediaEncoder.VideoCodec = EncoderParameters.Output_Video_Codec
                MediaEncoder.AudioCodecs = EncoderParameters.Output_Audio_Codecs
                MediaEncoder.AudioStream = EncoderParameters.Output_Audio_Stream

                ' Set up Duration
                MediaEncoder.VideoDuration = EncoderParameters.Output_Video_Duration

                ' Set up BitRates
                MediaEncoder.VideoBitrate = EncoderParameters.Output_Video_BitRate
                MediaEncoder.AudioBitRate = EncoderParameters.Output_Audio_BitRate

                MediaEncoder.AudioSampleRate = EncoderParameters.Output_Audio_SampleRate

                ' Set up FrameRate
                MediaEncoder.VideoFPS = EncoderParameters.Output_Video_FPS

                ' Set up Resolution
                MediaEncoder.VideoHeight = EncoderParameters.Output_Video_Height
                MediaEncoder.VideoWidth = EncoderParameters.Output_Video_Width

                ' Set up Padding
                MediaEncoder.VideoPadding_Top = EncoderParameters.Output_Video_Padding_Top
                MediaEncoder.VideoPadding_Bottom = EncoderParameters.Output_Video_Padding_Bottom

                ' Set up H.264 Profile Level
                MediaEncoder.VideoH264ProfileLevel = EncoderParameters.Output_Video_H264_Profile_Level
                MediaEncoder.VideoH264ProfileLowComplexity = EncoderParameters.Output_Video_H264_Profile_LowComplexity

                ' Set any Advanced FFmpeg Flags
                If bln_SettingsUIAdvancedFFmpegFlagsChecked Then
                    sub_DebugMessage("Setting Advanced FFmpeg Flags...")
                    MediaEncoder.EncoderAdvancedFlags = str_SettingsUIAdvancedFFmpegFlags
                End If

                ' Add the file
                sub_DebugMessage("Setting Input File...")
                MediaEncoder.InputFile = strInputFile

                ' Set the Output Folder
                If Not str_SettingsUIOutputFolder = Nothing Then
                    sub_DebugMessage("Setting Output Folder...")
                    MediaEncoder.OutputFolder = str_SettingsUIOutputFolder
                End If

                ' Set the Output File
                If Not str_SettingsSessionOutputFile = Nothing Then
                    sub_DebugMessage("Setting Output File...")
                    MediaEncoder.OutputFile = str_SettingsSessionOutputFile
                End If

                ' Set the Encoding Priority
                Select Case str_SettingsSessionPriority.ToUpper
                    Case "HIGH"
                        sub_DebugMessage("Setting Encoder Priority: High")
                        MediaEncoder.ProcessPriority = ProcessPriorityClass.High
                    Case "LOW"
                        sub_DebugMessage("Setting Encoder Priority: Low")
                        MediaEncoder.ProcessPriority = ProcessPriorityClass.Idle
                    Case Else
                        sub_DebugMessage("Setting Encoder Priority: Default")
                        MediaEncoder.ProcessPriority = ProcessPriorityClass.Normal
                End Select

                ' Start Encoding
                sub_DebugMessage("Spinning up Encoder...")
                MediaEncoder.SpinUp()

            Catch ex As Exception
                MediaEncoder.Abort()
                sub_DebugMessage(ex.Message, True, MsgBoxStyle.Exclamation, True)
                Exit Sub
            End Try

        End If

    End Sub

    ' Start Subtitling a File
    Private Sub sub_SubtitleFile(ByVal strInputFile As String, ByVal strOutputFile As String)

        sub_DebugMessage()
        sub_DebugMessage("* Start Subtitling *")

        Try

            sub_DebugMessage("Initialising Subtitling class...")

            ' Initialise the class
            MediaSubtitler = New cls_MediaSubtitler

            ' Set up the input file
            MediaSubtitler.InputFile = strInputFile
            MediaSubtitler.OutputFile = strOutputFile

            ' Start Encoding
            sub_DebugMessage("Spinning up Subtitler...")
            MediaSubtitler.SpinUp()

        Catch ex As Exception
            MediaSubtitler.Abort()
            sub_DebugMessage(ex.Message, True, MsgBoxStyle.Exclamation, True)
            Exit Sub
        End Try

    End Sub

    ' Start Tagging a File
    Private Sub sub_TagFile(ByVal strInputFile As String)

        sub_DebugMessage()
        sub_DebugMessage("* Start Tagging *")

        Try

            sub_DebugMessage("Initialising Tagging class...")

            ' Initialise the class
            MediaTagger = New cls_MediaTagger

            ' Set up the input file
            MediaTagger.InputFile = strInputFile

            ' Set the UUID to be tagged if required
            If Not EncoderParameters.Output_Video_H264_Profile_UUID = Nothing Then
                MediaTagger.OutputFileUUID = EncoderParameters.Output_Video_H264_Profile_UUID
            End If

            ' Start Encoding
            sub_DebugMessage("Spinning up Tagger...")
            MediaTagger.SpinUp()

        Catch ex As Exception
            MediaTagger.Abort()
            sub_DebugMessage(ex.Message, True, MsgBoxStyle.Exclamation, True)
            Exit Sub
        End Try

    End Sub

    ' Start Splitting a File
    Private Sub sub_SplitFile(ByVal strInputFile As String)

        sub_DebugMessage()
        sub_DebugMessage("* Start Splitting *")

        Try

            sub_DebugMessage("Initialising Splitter class...")

            ' Initialise the class
            MediaSplitter = New cls_MediaSplitter

            MediaSplitter.InputFile = strInputFile

            ' Start Encoding
            sub_DebugMessage("Spinning up Splitter...")
            MediaSplitter.SpinUp()

        Catch ex As Exception
            MediaSplitter.Abort()
            sub_DebugMessage(ex.Message, True, MsgBoxStyle.Exclamation, True)
            Exit Sub
        End Try

    End Sub

    ' Abort Encoding
    Private Sub sub_EncodingAbort()

        ' Handle thread invoking so that the UI can be updated cross-thread
        If Me.InvokeRequired Then
            Me.Invoke(New MethodInvoker(AddressOf sub_MediaEncoderFinished))
        Else

            sub_DebugMessage()
            sub_DebugMessage("* Abort *")

            Try

                sub_DebugMessage("Aborting Classes...")
                If Not MediaEncoder Is Nothing Then
                    MediaEncoder.Abort()
                End If
                If Not MediaSubtitler Is Nothing Then
                    MediaSubtitler.Abort()
                End If
                If Not MediaSplitter Is Nothing Then
                    MediaSplitter.Abort()
                End If
                If Not MediaTagger Is Nothing Then
                    MediaTagger.Abort()
                End If

                ' And make sure to cancel any automation
                bln_SettingsSessionAutoMode = False
                bln_SettingsSessionQuitOnFinishMode = False

            Catch ex As Exception
                sub_DebugMessage(ex.Message, True)

            Finally
                ' Cleanup
                que_FilesToProcess = Nothing
                ' Restore the form controls
                bln_EncodingInProgress = False
                sub_SupportUpdateFormControls("DEFAULT")
            End Try
        End If

    End Sub

    ' Determine File Properties
    Private Function func_DetermineFileProperties(ByVal strInputFile As String) As Boolean

        sub_DebugMessage()
        sub_DebugMessage("* Determine File Properties *")

        ' Reset Everything
        Dim intInputAudioStreamCount As Integer = 0
        Dim intInputVideoStreamCount As Integer = 0
        Dim intInputTextStreamCount As Integer = 0
        Dim strInputContainerFormat As String = Nothing
        Dim intInputContainerBitRate As Integer = 0

        Dim intAudioStream As Integer = 0
        Dim intVideoStream As Integer = 0

        Dim strInputVideoCodec As String = Nothing
        Dim intInputVideoDuration As Integer = 0
        Dim intInputVideoHeight As Integer = 0
        Dim intInputVideoWidth As Integer = 0
        Dim dblInputVideoFPS As Double = 0
        Dim dblInputVideoAspectRatio As Double = 0
        Dim intInputVideoBitrate As Integer = Nothing
        Dim strInputAudioCodec As String = Nothing
        Dim intInputAudioBitRate As Integer = 0
        Dim intInputAudioChannels As Integer = 0
        Dim intInputAudioSampleRate As Integer = 0

        Try

            ' Instantiate MediaInfo DLL
            sub_DebugMessage("Initiating MediaInfo...")
            Dim MediaInfo As New cls_MediaInfo

            ' Open the file
            sub_DebugMessage("Opening file to determine properties...")
            MediaInfo.Open(strInputFile)

            ' Retrieve the stream details
            sub_DebugMessage()

            ' Video Streams
            If Not MediaInfo.Get_(StreamKind.General, 0, "VideoCount") = Nothing Or Not MediaInfo.Get_(StreamKind.General, 0, "VideoCount") = "" Then
                intInputVideoStreamCount = CInt(MediaInfo.Get_(StreamKind.General, 0, "VideoCount"))
            Else
                Throw New Exception("Each file requires at least one video stream to encode")
            End If
            sub_DebugMessage("Video Streams: " & intInputVideoStreamCount)

            ' Audio Streams
            If Not MediaInfo.Get_(StreamKind.General, 0, "AudioCount") = Nothing Or Not MediaInfo.Get_(StreamKind.General, 0, "AudioCount") = "" Then
                intInputAudioStreamCount = CInt(MediaInfo.Get_(StreamKind.General, 0, "AudioCount"))
            Else
                sub_DebugMessage("WARNING: No Audio Streams detected")
                'Throw New Exception("Each file requires at least one audio stream to encode")
            End If
            sub_DebugMessage("Audio Streams: " & intInputAudioStreamCount)

            ' Text Streams
            If Not MediaInfo.Get_(StreamKind.General, 0, "TextCount") = Nothing Or Not MediaInfo.Get_(StreamKind.General, 0, "TextCount") = "" Then
                intInputTextStreamCount = CInt(MediaInfo.Get_(StreamKind.General, 0, "TextCount"))
            End If
            sub_DebugMessage("Text Streams: " & intInputTextStreamCount)

            ' Container Format
            If Not MediaInfo.Get_(StreamKind.General, 0, "Format") = Nothing Then
                strInputContainerFormat = MediaInfo.Get_(StreamKind.General, 0, "Format")
            End If
            sub_DebugMessage("Container Format: " & strInputContainerFormat)

            ' Video Duration
            If Not MediaInfo.Get_(StreamKind.General, 0, "PlayTime") = Nothing Or Not MediaInfo.Get_(StreamKind.General, 0, "PlayTime") = "" Then
                ' Prevent against invalid PlayTime being returned from MediaInfo
                Dim strInputVideoDurationTemp As String = MediaInfo.Get_(StreamKind.General, 0, "PlayTime")
                If strInputVideoDurationTemp.Contains("/") Then strInputVideoDurationTemp = strInputVideoDurationTemp.Substring(0, strInputVideoDurationTemp.IndexOf("/") - 1)
                strInputVideoDurationTemp = strInputVideoDurationTemp.Replace(".", strLocaleDecimal)
                intInputVideoDuration = CInt(CDbl(strInputVideoDurationTemp) / 1000)
            Else
                Throw New Exception("Unable to determine the video duration")
            End If
            sub_DebugMessage("Video Duration: " & intInputVideoDuration)

            ' Get Video Stream Details
            sub_DebugMessage()

            ' Video Codec
            If Not MediaInfo.Get_(StreamKind.Visual, intVideoStream, "Codec") = Nothing Then
                strInputVideoCodec = MediaInfo.Get_(StreamKind.Visual, intVideoStream, "Codec")
            End If
            sub_DebugMessage("Video Codec: " & strInputVideoCodec)

            ' Video BitRate
            If Not MediaInfo.Get_(StreamKind.Visual, intVideoStream, "BitRate") = Nothing Or Not MediaInfo.Get_(StreamKind.Visual, intVideoStream, "BitRate") = "" Then
                intInputVideoBitrate = CInt(CDbl(MediaInfo.Get_(StreamKind.Visual, intVideoStream, "BitRate")) / 1024)
            Else
                ' Make sure we get a bitrate, so fall back on the nominal bitrate
                If Not MediaInfo.Get_(StreamKind.Visual, intVideoStream, "BitRate_Nominal") = Nothing Or Not MediaInfo.Get_(StreamKind.Visual, intVideoStream, "BitRate_Nominal") = "" Then
                    sub_DebugMessage("Video Birate not found. Falling back on nominal bitrate...")
                    intInputVideoBitrate = CInt(CInt(MediaInfo.Get_(StreamKind.Visual, intVideoStream, "BitRate_Nominal")) / 1024)
                    ' Try falling back on the Overall bitrate, extracting a default of 128kb for audio
                ElseIf Not MediaInfo.Get_(StreamKind.General, 0, "BitRate") = Nothing Or Not MediaInfo.Get_(StreamKind.General, 0, "BitRate") = "" Then
                    sub_DebugMessage("Video Birate not found. Falling back on overall bitrate...")
                    intInputVideoBitrate = CInt(CInt(MediaInfo.Get_(StreamKind.General, 0, "BitRate")) / 1024) - 128
                Else
                    Throw New Exception("Unable to determine the video bitrate")
                End If
            End If
            sub_DebugMessage("Video BitRate: " & intInputVideoBitrate)

            ' Video Height
            If Not MediaInfo.Get_(StreamKind.Visual, intVideoStream, "Height") = Nothing Or Not MediaInfo.Get_(StreamKind.Visual, intVideoStream, "Height") = "" Then
                intInputVideoHeight = CInt(MediaInfo.Get_(StreamKind.Visual, intVideoStream, "Height"))
            Else
                Throw New Exception("Unable to determine the video resolution height")
            End If
            sub_DebugMessage("Video Height: " & intInputVideoHeight)

            ' Video Width
            If Not MediaInfo.Get_(StreamKind.Visual, intVideoStream, "Width") = Nothing Or Not MediaInfo.Get_(StreamKind.Visual, intVideoStream, "Width") = "" Then
                intInputVideoWidth = CInt(MediaInfo.Get_(StreamKind.Visual, intVideoStream, "Width"))
            Else
                Throw New Exception("Unable to determine the video resolution width")
            End If
            sub_DebugMessage("Video Width: " & intInputVideoWidth)

            ' Video Framerate
            If Not MediaInfo.Get_(StreamKind.Visual, intVideoStream, "FrameRate") = Nothing Or Not MediaInfo.Get_(StreamKind.Visual, intVideoStream, "FrameRate") = "" Then
                dblInputVideoFPS = CDbl(MediaInfo.Get_(StreamKind.Visual, intVideoStream, "FrameRate").Replace(".", strLocaleDecimal))
                ' Adjust framerate as necessary
                If dblInputVideoFPS = 23976 Then dblInputVideoFPS = 23.976
            Else
                Throw New Exception("Unable to determine the video framerate")
            End If
            sub_DebugMessage("Video FPS: " & dblInputVideoFPS)

            ' Video Aspect Ratio
            If Not MediaInfo.Get_(StreamKind.Visual, intVideoStream, "AspectRatio") = Nothing Or Not MediaInfo.Get_(StreamKind.Visual, intVideoStream, "AspectRatio") = "" Then
                dblInputVideoAspectRatio = CDbl(MediaInfo.Get_(StreamKind.Visual, intVideoStream, "AspectRatio").Replace(".", strLocaleDecimal))
            Else
                Throw New Exception("Unable to determine the video aspect ratio")
            End If
            sub_DebugMessage("Video Aspect Ratio: " & dblInputVideoAspectRatio)

            ' Get Audio Stream Details
            sub_DebugMessage()

            ' Determine what audio stream we want to use
            If Not intInputAudioStreamCount = 0 Then
                Dim blnAudioStreamMatch As Boolean = False
                For intAudioStreamCheck As Integer = 0 To intInputAudioStreamCount - 1
                    ' Search for a language match
                    If Not MediaInfo.Get_(StreamKind.Audio, intAudioStreamCheck, "Language") = Nothing Then
                        If str_SettingsUIAdvancedPreferredAudioLanguage.ToUpper.Contains(MediaInfo.Get_(StreamKind.Audio, intAudioStreamCheck, "Language").ToUpper) Then
                            sub_DebugMessage("Match for Preferred Audio Language (" & str_SettingsUIAdvancedPreferredAudioLanguage & ") at #" & intAudioStreamCheck & ": " & MediaInfo.Get_(StreamKind.Audio, intAudioStreamCheck, "Language"))
                            blnAudioStreamMatch = True
                            intAudioStream = intAudioStreamCheck
                            Exit For
                        End If
                    End If
                Next
                If Not blnAudioStreamMatch Then
                    For intAudioStreamCheck As Integer = 0 To intInputAudioStreamCount - 1
                        ' Search for a language match
                        If MediaInfo.Get_(StreamKind.Audio, intAudioStreamCheck, "Language") = Nothing Then
                            sub_DebugMessage("No match for Preferred Audio Language. Defaulting to audio stream with no language marker at #" & intAudioStreamCheck)
                            blnAudioStreamMatch = True
                            intAudioStream = intAudioStreamCheck
                            Exit For
                        End If
                    Next
                End If
                If Not blnAudioStreamMatch Then
                    sub_DebugMessage("No match for Preferred Audio Language. Defaulting to first audio stream")
                End If

                ' Audio Codec
                If Not MediaInfo.Get_(StreamKind.Audio, intAudioStream, "Codec") = Nothing Then
                    strInputAudioCodec = MediaInfo.Get_(StreamKind.Audio, intAudioStream, "Codec")
                End If
                sub_DebugMessage("Audio Codec: " & strInputAudioCodec)

                ' Audio BitRate
                If Not MediaInfo.Get_(StreamKind.Audio, intAudioStream, "BitRate") = Nothing Or Not MediaInfo.Get_(StreamKind.Audio, intAudioStream, "BitRate") = "" Then
                    intInputAudioBitRate = CInt(CDbl(MediaInfo.Get_(StreamKind.Audio, intAudioStream, "BitRate")) / 1024)
                Else
                    If Not MediaInfo.Get_(StreamKind.Audio, intAudioStream, "BitRate_Nominal") = Nothing Or Not MediaInfo.Get_(StreamKind.General, 0, "BitRate_Nominal") = "" Then
                        ' Make sure we get a bitrate, so default to 128kb for audio
                        sub_DebugMessage("Audio Birate not found. Falling back on nominal bitrate...")
                        intInputAudioBitRate = CInt(CInt(MediaInfo.Get_(StreamKind.Audio, intAudioStream, "BitRate_Nominal")) / 1024)
                        ' Try falling back on the Overall bitrate, extracting a default of 128kb for audio
                    ElseIf Not MediaInfo.Get_(StreamKind.General, 0, "BitRate") = Nothing Or Not MediaInfo.Get_(StreamKind.General, 0, "BitRate") = "" Then
                        sub_DebugMessage("Audio Birate not found. Falling back on overall bitrate...")
                        intInputAudioBitRate = 128
                    Else
                        Throw New Exception("Unable to determine the audio bitrate")
                    End If
                End If

                sub_DebugMessage("Audio BitRate: " & intInputAudioBitRate)

                ' Audio Channels
                If Not MediaInfo.Get_(StreamKind.Audio, intAudioStream, "Channel(s)") = Nothing Or Not MediaInfo.Get_(StreamKind.Audio, intAudioStream, "Channel(s)") = "" Then
                    intInputAudioChannels = CInt(MediaInfo.Get_(StreamKind.Audio, intAudioStream, "Channel(s)"))
                Else
                    Throw New Exception("Unable to determine the number of audio channels")
                End If
                sub_DebugMessage("Audio Channels: " & intInputAudioChannels)

                ' Audio Sample Rate
                If Not MediaInfo.Get_(StreamKind.Audio, intAudioStream, "SamplingRate") = Nothing Or Not MediaInfo.Get_(StreamKind.Audio, intAudioStream, "SamplingRate") = "" Then
                    intInputAudioSampleRate = CInt(MediaInfo.Get_(StreamKind.Audio, intAudioStream, "SamplingRate"))
                Else
                    Throw New Exception("Unable to determine the audio sample rate")
                End If
                sub_DebugMessage("Audio Sample Rate: " & intInputAudioSampleRate)
            Else
                ' Set the audio stream value to 99 so we can use to identify files without audio
                intAudioStream = 99
            End If

            ' Pass the parameters to the EncoderParameters class
            sub_DebugMessage()
            sub_DebugMessage("Passing input file information through filtering framework...")
            EncoderParameters.Input_Video_Codec = strInputVideoCodec
            EncoderParameters.Input_Video_Duration = intInputVideoDuration
            EncoderParameters.Input_Video_BitRate = intInputVideoBitrate
            EncoderParameters.Input_Video_Height = intInputVideoHeight
            EncoderParameters.Input_Video_Width = intInputVideoWidth
            EncoderParameters.Input_Video_FPS = dblInputVideoFPS
            EncoderParameters.Input_Video_AspectRatio = dblInputVideoAspectRatio
            EncoderParameters.Input_Audio_Stream = intAudioStream
            EncoderParameters.Input_Audio_Codec = strInputAudioCodec
            EncoderParameters.Input_Audio_BitRate = intInputAudioBitRate
            EncoderParameters.Input_Audio_Channels = intInputAudioChannels
            EncoderParameters.Input_Audio_SampleRate = intInputAudioSampleRate

            ' UI options
            sub_DebugMessage("Passing UI selections through filtering framework...")
            EncoderParameters.Output_Video_Codec_H264Enabled = cbx_H264Encoding.Checked
            EncoderParameters.Output_Video_Codec_StreamCopyEnabled = cbx_StreamCopy.Checked
            EncoderParameters.Output_Video_Resolution_OutputForTV = cbx_TVOutput.Checked
            EncoderParameters.Output_Audio_Codec_AC3PassThroughEnabled = cbx_AC3Passthrough.Checked

            ' Configure the output parameters
            sub_DebugMessage("Generating validated output parameters...")
            EncoderParameters.ConfigureOutputParameters()

        Catch ex As Exception
            sub_DebugMessage(ex.Message & ". It's possible that the file you are attempting to read is corrupt", True, MsgBoxStyle.Exclamation)
            Return False
        End Try

        Return True

    End Function

End Class



