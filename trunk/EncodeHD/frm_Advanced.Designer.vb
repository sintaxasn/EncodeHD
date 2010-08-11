<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Advanced
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Advanced))
        Me.cbx_FFmpegFlags = New System.Windows.Forms.CheckBox()
        Me.tbx_FFmpegFlags = New System.Windows.Forms.TextBox()
        Me.btn_Close = New System.Windows.Forms.Button()
        Me.toolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.cbx_PreferredAudioLanguage = New System.Windows.Forms.ComboBox()
        Me.lbl_PreferredAudioLanguage = New System.Windows.Forms.Label()
        Me.cbx_SoftSubs = New System.Windows.Forms.CheckBox()
        Me.lnk_ViewLogFiles = New System.Windows.Forms.LinkLabel()
        Me.SuspendLayout()
        '
        'cbx_FFmpegFlags
        '
        Me.cbx_FFmpegFlags.AutoSize = True
        Me.cbx_FFmpegFlags.Location = New System.Drawing.Point(12, 14)
        Me.cbx_FFmpegFlags.Name = "cbx_FFmpegFlags"
        Me.cbx_FFmpegFlags.Size = New System.Drawing.Size(95, 17)
        Me.cbx_FFmpegFlags.TabIndex = 0
        Me.cbx_FFmpegFlags.Text = "FFmpeg Flags:"
        Me.cbx_FFmpegFlags.UseVisualStyleBackColor = True
        '
        'tbx_FFmpegFlags
        '
        Me.tbx_FFmpegFlags.Location = New System.Drawing.Point(113, 12)
        Me.tbx_FFmpegFlags.Name = "tbx_FFmpegFlags"
        Me.tbx_FFmpegFlags.Size = New System.Drawing.Size(352, 20)
        Me.tbx_FFmpegFlags.TabIndex = 1
        '
        'btn_Close
        '
        Me.btn_Close.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Close.Location = New System.Drawing.Point(390, 69)
        Me.btn_Close.Name = "btn_Close"
        Me.btn_Close.Size = New System.Drawing.Size(75, 23)
        Me.btn_Close.TabIndex = 2
        Me.btn_Close.Text = "&Close"
        Me.btn_Close.UseVisualStyleBackColor = True
        '
        'cbx_PreferredAudioLanguage
        '
        Me.cbx_PreferredAudioLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbx_PreferredAudioLanguage.FormattingEnabled = True
        Me.cbx_PreferredAudioLanguage.Items.AddRange(New Object() {"Chinese", "Danish", "Dutch", "English", "Finnish", "French", "German", "Greek", "Hebrew", "Italian", "Japanese", "Russian", "Spanish", "Swedish"})
        Me.cbx_PreferredAudioLanguage.Location = New System.Drawing.Point(119, 38)
        Me.cbx_PreferredAudioLanguage.Name = "cbx_PreferredAudioLanguage"
        Me.cbx_PreferredAudioLanguage.Size = New System.Drawing.Size(135, 21)
        Me.cbx_PreferredAudioLanguage.TabIndex = 5
        Me.toolTip.SetToolTip(Me.cbx_PreferredAudioLanguage, resources.GetString("cbx_PreferredAudioLanguage.ToolTip"))
        '
        'lbl_PreferredAudioLanguage
        '
        Me.lbl_PreferredAudioLanguage.AutoSize = True
        Me.lbl_PreferredAudioLanguage.Location = New System.Drawing.Point(9, 41)
        Me.lbl_PreferredAudioLanguage.Name = "lbl_PreferredAudioLanguage"
        Me.lbl_PreferredAudioLanguage.Size = New System.Drawing.Size(104, 13)
        Me.lbl_PreferredAudioLanguage.TabIndex = 4
        Me.lbl_PreferredAudioLanguage.Text = "Preferred Language:"
        Me.toolTip.SetToolTip(Me.lbl_PreferredAudioLanguage, "Where possible, this language will be used as the default Audio / Subtitle langua" & _
                "ge")
        '
        'cbx_SoftSubs
        '
        Me.cbx_SoftSubs.AutoSize = True
        Me.cbx_SoftSubs.Location = New System.Drawing.Point(295, 40)
        Me.cbx_SoftSubs.Name = "cbx_SoftSubs"
        Me.cbx_SoftSubs.Size = New System.Drawing.Size(170, 17)
        Me.cbx_SoftSubs.TabIndex = 7
        Me.cbx_SoftSubs.Text = "Automatically Merge Soft-Subs"
        Me.toolTip.SetToolTip(Me.cbx_SoftSubs, "When a matching SRT or SUB subtitle file is found with the original video, this w" & _
                "ill be merged into the resulting output file")
        Me.cbx_SoftSubs.UseVisualStyleBackColor = True
        '
        'lnk_ViewLogFiles
        '
        Me.lnk_ViewLogFiles.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.lnk_ViewLogFiles.AutoSize = True
        Me.lnk_ViewLogFiles.Location = New System.Drawing.Point(9, 74)
        Me.lnk_ViewLogFiles.Name = "lnk_ViewLogFiles"
        Me.lnk_ViewLogFiles.Size = New System.Drawing.Size(137, 13)
        Me.lnk_ViewLogFiles.TabIndex = 3
        Me.lnk_ViewLogFiles.TabStop = True
        Me.lnk_ViewLogFiles.Text = "Click here to View Log Files"
        '
        'frm_Advanced
        '
        Me.AcceptButton = Me.btn_Close
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(477, 104)
        Me.ControlBox = False
        Me.Controls.Add(Me.cbx_SoftSubs)
        Me.Controls.Add(Me.cbx_PreferredAudioLanguage)
        Me.Controls.Add(Me.lbl_PreferredAudioLanguage)
        Me.Controls.Add(Me.lnk_ViewLogFiles)
        Me.Controls.Add(Me.btn_Close)
        Me.Controls.Add(Me.tbx_FFmpegFlags)
        Me.Controls.Add(Me.cbx_FFmpegFlags)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_Advanced"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Advanced Settings"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cbx_FFmpegFlags As System.Windows.Forms.CheckBox
    Friend WithEvents tbx_FFmpegFlags As System.Windows.Forms.TextBox
    Friend WithEvents btn_Close As System.Windows.Forms.Button
    Friend WithEvents toolTip As System.Windows.Forms.ToolTip
    Friend WithEvents lnk_ViewLogFiles As System.Windows.Forms.LinkLabel
    Friend WithEvents lbl_PreferredAudioLanguage As System.Windows.Forms.Label
    Friend WithEvents cbx_PreferredAudioLanguage As System.Windows.Forms.ComboBox

    Friend WithEvents cbx_SoftSubs As System.Windows.Forms.CheckBox
End Class
