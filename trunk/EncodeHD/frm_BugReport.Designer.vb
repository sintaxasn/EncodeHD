<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_BugReport
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_BugReport))
        Me.lbl_Email = New System.Windows.Forms.Label
        Me.tbx_CustomerEmailAddress = New System.Windows.Forms.TextBox
        Me.lbl_ExtraInfo = New System.Windows.Forms.Label
        Me.tbx_ExtraInfo = New System.Windows.Forms.TextBox
        Me.btn_SendBugReport = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'lbl_Email
        '
        Me.lbl_Email.AutoSize = True
        Me.lbl_Email.Location = New System.Drawing.Point(12, 13)
        Me.lbl_Email.Name = "lbl_Email"
        Me.lbl_Email.Size = New System.Drawing.Size(148, 13)
        Me.lbl_Email.TabIndex = 0
        Me.lbl_Email.Text = "Your Email Address (required):"
        '
        'tbx_CustomerEmailAddress
        '
        Me.tbx_CustomerEmailAddress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbx_CustomerEmailAddress.Location = New System.Drawing.Point(167, 10)
        Me.tbx_CustomerEmailAddress.Name = "tbx_CustomerEmailAddress"
        Me.tbx_CustomerEmailAddress.Size = New System.Drawing.Size(173, 20)
        Me.tbx_CustomerEmailAddress.TabIndex = 1
        '
        'lbl_ExtraInfo
        '
        Me.lbl_ExtraInfo.Location = New System.Drawing.Point(12, 34)
        Me.lbl_ExtraInfo.Name = "lbl_ExtraInfo"
        Me.lbl_ExtraInfo.Size = New System.Drawing.Size(458, 35)
        Me.lbl_ExtraInfo.TabIndex = 2
        Me.lbl_ExtraInfo.Text = "The following logging information will be included as part of the report. Please " & _
            "feel free to edit out any information you do not want sent:"
        '
        'tbx_ExtraInfo
        '
        Me.tbx_ExtraInfo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbx_ExtraInfo.Location = New System.Drawing.Point(15, 72)
        Me.tbx_ExtraInfo.Multiline = True
        Me.tbx_ExtraInfo.Name = "tbx_ExtraInfo"
        Me.tbx_ExtraInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.tbx_ExtraInfo.Size = New System.Drawing.Size(455, 188)
        Me.tbx_ExtraInfo.TabIndex = 3
        '
        'btn_SendBugReport
        '
        Me.btn_SendBugReport.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_SendBugReport.Location = New System.Drawing.Point(346, 8)
        Me.btn_SendBugReport.Name = "btn_SendBugReport"
        Me.btn_SendBugReport.Size = New System.Drawing.Size(124, 23)
        Me.btn_SendBugReport.TabIndex = 4
        Me.btn_SendBugReport.Text = "&Send Bug Report"
        Me.btn_SendBugReport.UseVisualStyleBackColor = True
        '
        'frm_BugReport
        '
        Me.AcceptButton = Me.btn_SendBugReport
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(482, 270)
        Me.Controls.Add(Me.btn_SendBugReport)
        Me.Controls.Add(Me.tbx_ExtraInfo)
        Me.Controls.Add(Me.lbl_ExtraInfo)
        Me.Controls.Add(Me.tbx_CustomerEmailAddress)
        Me.Controls.Add(Me.lbl_Email)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_BugReport"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "EncodeHD Bug Reporting"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lbl_Email As System.Windows.Forms.Label
    Friend WithEvents tbx_CustomerEmailAddress As System.Windows.Forms.TextBox
    Friend WithEvents lbl_ExtraInfo As System.Windows.Forms.Label
    Friend WithEvents tbx_ExtraInfo As System.Windows.Forms.TextBox
    Friend WithEvents btn_SendBugReport As System.Windows.Forms.Button
End Class
