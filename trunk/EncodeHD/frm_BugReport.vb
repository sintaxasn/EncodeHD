' Imports FogBugz
Imports System.Text.RegularExpressions


Public Class frm_BugReport

    ' Form Loading Events
    Private Sub frm_BugReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ' Populate the Extra Info text box with the logging info we've collected so far
        'tbx_ExtraInfo.Text = str_BugzScoutExtraInfo

    End Sub

    ' Send Bug Report Button Click
    Private Sub btn_SendBugReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_SendBugReport.Click

        ' Verify the email address
        Dim rgx_Pattern As String = "^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
        Dim rgx_EmailMatch As Match = Regex.Match(tbx_CustomerEmailAddress.Text, rgx_Pattern)
        If Not rgx_EmailMatch.Success Then
            MessageBox.Show("The supplied email address is not valid", My.Resources.App_Title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        Else
            ' If the email address is okay, let's send the bug report
            Try
                '' Trim invalid characters from the description
                'str_BugzScoutDescription = str_BugzScoutDescription.Replace("#", "")
                'str_BugzScoutDescription = str_BugzScoutDescription.Replace("'", "")
                'str_BugzScoutDescription = str_BugzScoutDescription.Replace("""", "")
                'str_BugzScoutDescription = str_BugzScoutDescription.Replace("@", "")
                'str_BugzScoutDescription = str_BugzScoutDescription.Replace("(", "")
                'str_BugzScoutDescription = str_BugzScoutDescription.Replace(")", "")

                '' Default Bug Reporting Info
                'Dim fbz_BugReport As New BugReport(str_BugzScoutURL, str_BugzScoutUser)

                'fbz_BugReport.Project = str_BugzScoutProject
                'fbz_BugReport.Area = str_BugzScoutArea
                'fbz_BugReport.DefaultMsg = str_BugzScoutDefaultReturnMessage
                'fbz_BugReport.ForceNewBug = bln_BugzScoutForceNewBug

                '' Dynamic Info
                'fbz_BugReport.Description = str_BugzScoutDescription
                'fbz_BugReport.ExtraInformation = tbx_ExtraInfo.Text
                'fbz_BugReport.Email = tbx_CustomerEmailAddress.Text

                '' Disable form items
                'tbx_CustomerEmailAddress.Enabled = False
                'tbx_ExtraInfo.Enabled = False
                'btn_SendBugReport.Text = "Sending..."
                'btn_SendBugReport.Enabled = False

                '' Submit the bug
                'Dim str_BugReportResult As String = fbz_BugReport.Submit()

                ' Display the confirmation message
                'MessageBox.Show(str_BugReportResult, My.Resources.App_Title, MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Close the Bug Report Dialog Box
                Me.Close()

            Catch ex As Exception

                ' Display an error if it occurs
                MessageBox.Show(ex.Message, My.Resources.App_Title, MessageBoxButtons.OK, MessageBoxIcon.Error)

                ' Re-Enable form items so we can retry
                tbx_CustomerEmailAddress.Enabled = True
                tbx_ExtraInfo.Enabled = True
                btn_SendBugReport.Text = "&Send Bug Report"
                btn_SendBugReport.Enabled = True

            End Try

        End If

    End Sub

End Class
