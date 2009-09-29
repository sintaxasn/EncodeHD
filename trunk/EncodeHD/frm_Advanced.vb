Public Class frm_Advanced

    Private Sub frm_Advanced_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        sub_DebugMessage()
        sub_DebugMessage("* Advanced Form Load Events *")

        tbx_FFmpegFlags.Text = str_SettingsUIAdvancedFFmpegFlags

        If bln_SettingsUIAdvancedFFmpegFlagsChecked Then
            cbx_FFmpegFlags.Checked = True
            tbx_FFmpegFlags.Enabled = True
        Else
            cbx_FFmpegFlags.Checked = False
            tbx_FFmpegFlags.Enabled = False
        End If

        Dim int_PreferredLanguageIndex As Integer = cbx_PreferredAudioLanguage.FindString(str_SettingsUIAdvancedPreferredAudioLanguage)
        sub_DebugMessage(str_SettingsUIAdvancedPreferredAudioLanguage & ": " & int_PreferredLanguageIndex.ToString)
        cbx_PreferredAudioLanguage.SelectedIndex = int_PreferredLanguageIndex

        cbx_SoftSubs.Checked = bln_SettingsUIAdvancedSoftSubsChecked

    End Sub

    Private Sub cbx_FFmpegFlags_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbx_FFmpegFlags.CheckedChanged

        sub_DebugMessage()
        sub_DebugMessage("* FFmpeg Flags Check Changed *")

        If cbx_FFmpegFlags.Checked Then
            tbx_FFmpegFlags.Enabled = True
        Else
            tbx_FFmpegFlags.Enabled = False
        End If

    End Sub

    Private Sub cbx_SoftSubs_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbx_SoftSubs.CheckedChanged

        sub_DebugMessage()
        sub_DebugMessage("* SoftSubs Check Changed *")

    End Sub

    Private Sub lnk_ViewLogFiles_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnk_ViewLogFiles.LinkClicked

        sub_DebugMessage()
        sub_DebugMessage("* View Log Files Link Click Events *")

        Try
            Process.Start(str_LogFolder)
        Catch ex As Exception
            sub_DebugMessage("Error while trying to open Log Folder: " & ex.Message)
        End Try
    End Sub

    Private Sub btn_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Close.Click

        sub_DebugMessage()
        sub_DebugMessage("* Close Button Click Events *")

        str_SettingsUIAdvancedFFmpegFlags = tbx_FFmpegFlags.Text
        bln_SettingsUIAdvancedFFmpegFlagsChecked = cbx_FFmpegFlags.Checked
        str_SettingsUIAdvancedPreferredAudioLanguage = cbx_PreferredAudioLanguage.Text
        bln_SettingsUIAdvancedSoftSubsChecked = cbx_SoftSubs.Checked

        Me.Close()

    End Sub

End Class