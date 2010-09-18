<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Main
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Main))
        Me.btn_StartStop = New System.Windows.Forms.Button()
        Me.cbx_ConversionDevice = New System.Windows.Forms.ComboBox()
        Me.dlg_OpenFile = New System.Windows.Forms.OpenFileDialog()
        Me.pgb_Progress = New System.Windows.Forms.ProgressBar()
        Me.btn_ClearAll = New System.Windows.Forms.Button()
        Me.btn_RemoveFiles = New System.Windows.Forms.Button()
        Me.btn_AddFiles = New System.Windows.Forms.Button()
        Me.cbx_H264Encoding = New System.Windows.Forms.CheckBox()
        Me.cbx_AC3Passthrough = New System.Windows.Forms.CheckBox()
        Me.cbx_AutoSplit4GB = New System.Windows.Forms.CheckBox()
        Me.cbx_OutputFolder = New System.Windows.Forms.CheckBox()
        Me.pbx_ImageTitleMiddle = New System.Windows.Forms.PictureBox()
        Me.pbx_ImageTitleLeft = New System.Windows.Forms.PictureBox()
        Me.pbx_ImageTitleRight = New System.Windows.Forms.PictureBox()
        Me.dlg_OpenFolder = New System.Windows.Forms.FolderBrowserDialog()
        Me.toolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.cbx_TVOutput = New System.Windows.Forms.CheckBox()
        Me.btn_Advanced = New System.Windows.Forms.Button()
        Me.statusStrip = New System.Windows.Forms.StatusStrip()
        Me.toolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.listView = New EncodeHD.cmp_ListViewPlus(Me.components)
        Me.clm_Files = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.cbx_StreamCopy = New System.Windows.Forms.CheckBox()
        CType(Me.pbx_ImageTitleMiddle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbx_ImageTitleLeft, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbx_ImageTitleRight, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.statusStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'btn_StartStop
        '
        Me.btn_StartStop.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_StartStop.Location = New System.Drawing.Point(431, 227)
        Me.btn_StartStop.Name = "btn_StartStop"
        Me.btn_StartStop.Size = New System.Drawing.Size(75, 22)
        Me.btn_StartStop.TabIndex = 0
        Me.btn_StartStop.Text = "&Start"
        Me.btn_StartStop.UseVisualStyleBackColor = True
        '
        'cbx_ConversionDevice
        '
        Me.cbx_ConversionDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbx_ConversionDevice.FormattingEnabled = True
        Me.cbx_ConversionDevice.Items.AddRange(New Object() {"Apple TV", "BlackBerry (8100) Pearl", "BlackBerry (8200) Kickstart", "BlackBerry (8300) Curve", "BlackBerry (8700) Electron", "BlackBerry (8800) Indigo", "BlackBerry (8900) Javelin", "BlackBerry (9000) Bold", "BlackBerry (9500) Storm", "HTC Desire", "HTC EVO 4G", "iPad", "iPhone", "iPhone 4", "iPod 5G", "iPod Classic", "iPod Nano", "iPod Touch", "Nexus One", "Nokia E71", "Nokia N900", "PlayStation 3", "PSP", "T-Mobile G1", "WD TV", "Youtube HD", "Xbox 360", "Zune", "ZuneHD"})
        Me.cbx_ConversionDevice.Location = New System.Drawing.Point(12, 37)
        Me.cbx_ConversionDevice.Name = "cbx_ConversionDevice"
        Me.cbx_ConversionDevice.Size = New System.Drawing.Size(146, 21)
        Me.cbx_ConversionDevice.TabIndex = 1
        Me.toolTip.SetToolTip(Me.cbx_ConversionDevice, "Select the device profile you want to encode video to")
        '
        'dlg_OpenFile
        '
        Me.dlg_OpenFile.Multiselect = True
        Me.dlg_OpenFile.Title = "Select file(s) for encoding"
        '
        'pgb_Progress
        '
        Me.pgb_Progress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pgb_Progress.Location = New System.Drawing.Point(12, 227)
        Me.pgb_Progress.Name = "pgb_Progress"
        Me.pgb_Progress.Size = New System.Drawing.Size(413, 22)
        Me.pgb_Progress.TabIndex = 5
        Me.pgb_Progress.Visible = False
        '
        'btn_ClearAll
        '
        Me.btn_ClearAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_ClearAll.FlatAppearance.BorderSize = 0
        Me.btn_ClearAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!)
        Me.btn_ClearAll.Location = New System.Drawing.Point(466, 65)
        Me.btn_ClearAll.Name = "btn_ClearAll"
        Me.btn_ClearAll.Size = New System.Drawing.Size(52, 23)
        Me.btn_ClearAll.TabIndex = 41
        Me.btn_ClearAll.Text = "Clear All"
        Me.btn_ClearAll.UseVisualStyleBackColor = True
        '
        'btn_RemoveFiles
        '
        Me.btn_RemoveFiles.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_RemoveFiles.FlatAppearance.BorderSize = 0
        Me.btn_RemoveFiles.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.btn_RemoveFiles.Location = New System.Drawing.Point(441, 65)
        Me.btn_RemoveFiles.Name = "btn_RemoveFiles"
        Me.btn_RemoveFiles.Size = New System.Drawing.Size(26, 23)
        Me.btn_RemoveFiles.TabIndex = 40
        Me.btn_RemoveFiles.Text = "-"
        Me.btn_RemoveFiles.UseVisualStyleBackColor = True
        '
        'btn_AddFiles
        '
        Me.btn_AddFiles.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_AddFiles.FlatAppearance.BorderSize = 0
        Me.btn_AddFiles.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.btn_AddFiles.Location = New System.Drawing.Point(416, 65)
        Me.btn_AddFiles.Name = "btn_AddFiles"
        Me.btn_AddFiles.Size = New System.Drawing.Size(26, 23)
        Me.btn_AddFiles.TabIndex = 39
        Me.btn_AddFiles.Text = "+"
        Me.btn_AddFiles.UseVisualStyleBackColor = True
        '
        'cbx_H264Encoding
        '
        Me.cbx_H264Encoding.AutoSize = True
        Me.cbx_H264Encoding.Location = New System.Drawing.Point(164, 39)
        Me.cbx_H264Encoding.Name = "cbx_H264Encoding"
        Me.cbx_H264Encoding.Size = New System.Drawing.Size(55, 17)
        Me.cbx_H264Encoding.TabIndex = 44
        Me.cbx_H264Encoding.Text = "&H.264"
        Me.toolTip.SetToolTip(Me.cbx_H264Encoding, "H.264 encoding provides better video quality and smaller file sizes at the expens" & _
                "e of taking longer to encode. It's recommended that you leave this enabled unles" & _
                "s it is grayed out by default.")
        Me.cbx_H264Encoding.UseVisualStyleBackColor = True
        '
        'cbx_AC3Passthrough
        '
        Me.cbx_AC3Passthrough.AutoSize = True
        Me.cbx_AC3Passthrough.Location = New System.Drawing.Point(398, 39)
        Me.cbx_AC3Passthrough.Name = "cbx_AC3Passthrough"
        Me.cbx_AC3Passthrough.Size = New System.Drawing.Size(108, 17)
        Me.cbx_AC3Passthrough.TabIndex = 54
        Me.cbx_AC3Passthrough.Text = "A&C3 Passthrough"
        Me.toolTip.SetToolTip(Me.cbx_AC3Passthrough, "AC3 Passthrough will, when possible, add an additional AC3 audio stream (ie, 5.1 " & _
                "audio) to the output file as well as the standard 2 channel AAC stream that is c" & _
                "reated.")
        Me.cbx_AC3Passthrough.UseVisualStyleBackColor = True
        '
        'cbx_AutoSplit4GB
        '
        Me.cbx_AutoSplit4GB.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cbx_AutoSplit4GB.AutoSize = True
        Me.cbx_AutoSplit4GB.Location = New System.Drawing.Point(158, 203)
        Me.cbx_AutoSplit4GB.Name = "cbx_AutoSplit4GB"
        Me.cbx_AutoSplit4GB.Size = New System.Drawing.Size(101, 17)
        Me.cbx_AutoSplit4GB.TabIndex = 52
        Me.cbx_AutoSplit4GB.Text = "AutoSplit > 4GB"
        Me.cbx_AutoSplit4GB.UseVisualStyleBackColor = True
        '
        'cbx_OutputFolder
        '
        Me.cbx_OutputFolder.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cbx_OutputFolder.AutoSize = True
        Me.cbx_OutputFolder.Location = New System.Drawing.Point(12, 203)
        Me.cbx_OutputFolder.Name = "cbx_OutputFolder"
        Me.cbx_OutputFolder.Size = New System.Drawing.Size(128, 17)
        Me.cbx_OutputFolder.TabIndex = 51
        Me.cbx_OutputFolder.Text = "S&pecify Output Folder"
        Me.cbx_OutputFolder.UseVisualStyleBackColor = True
        '
        'pbx_ImageTitleMiddle
        '
        Me.pbx_ImageTitleMiddle.BackgroundImage = Global.EncodeHD.My.Resources.Resources.imageTitleMiddle
        Me.pbx_ImageTitleMiddle.Dock = System.Windows.Forms.DockStyle.Top
        Me.pbx_ImageTitleMiddle.Location = New System.Drawing.Point(0, 0)
        Me.pbx_ImageTitleMiddle.Name = "pbx_ImageTitleMiddle"
        Me.pbx_ImageTitleMiddle.Size = New System.Drawing.Size(518, 31)
        Me.pbx_ImageTitleMiddle.TabIndex = 58
        Me.pbx_ImageTitleMiddle.TabStop = False
        '
        'pbx_ImageTitleLeft
        '
        Me.pbx_ImageTitleLeft.Image = Global.EncodeHD.My.Resources.Resources.imageTitleLeft
        Me.pbx_ImageTitleLeft.Location = New System.Drawing.Point(0, 0)
        Me.pbx_ImageTitleLeft.Name = "pbx_ImageTitleLeft"
        Me.pbx_ImageTitleLeft.Size = New System.Drawing.Size(295, 31)
        Me.pbx_ImageTitleLeft.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pbx_ImageTitleLeft.TabIndex = 59
        Me.pbx_ImageTitleLeft.TabStop = False
        '
        'pbx_ImageTitleRight
        '
        Me.pbx_ImageTitleRight.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbx_ImageTitleRight.Image = Global.EncodeHD.My.Resources.Resources.imageTitleRight
        Me.pbx_ImageTitleRight.Location = New System.Drawing.Point(223, 0)
        Me.pbx_ImageTitleRight.Name = "pbx_ImageTitleRight"
        Me.pbx_ImageTitleRight.Size = New System.Drawing.Size(295, 31)
        Me.pbx_ImageTitleRight.TabIndex = 60
        Me.pbx_ImageTitleRight.TabStop = False
        '
        'cbx_TVOutput
        '
        Me.cbx_TVOutput.AutoSize = True
        Me.cbx_TVOutput.Location = New System.Drawing.Point(317, 39)
        Me.cbx_TVOutput.Name = "cbx_TVOutput"
        Me.cbx_TVOutput.Size = New System.Drawing.Size(75, 17)
        Me.cbx_TVOutput.TabIndex = 63
        Me.cbx_TVOutput.Text = "&TV Output"
        Me.toolTip.SetToolTip(Me.cbx_TVOutput, "Certain devices (such as the iPod) can be connected to a TV for output. Select th" & _
                "is option to format the output video for best display on a TV.")
        Me.cbx_TVOutput.UseVisualStyleBackColor = True
        '
        'btn_Advanced
        '
        Me.btn_Advanced.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Advanced.Location = New System.Drawing.Point(431, 198)
        Me.btn_Advanced.Name = "btn_Advanced"
        Me.btn_Advanced.Size = New System.Drawing.Size(75, 22)
        Me.btn_Advanced.TabIndex = 64
        Me.btn_Advanced.Text = "&Advanced"
        Me.toolTip.SetToolTip(Me.btn_Advanced, "Starts / Stops Encoding")
        Me.btn_Advanced.UseVisualStyleBackColor = True
        '
        'statusStrip
        '
        Me.statusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.toolStripStatusLabel})
        Me.statusStrip.Location = New System.Drawing.Point(0, 254)
        Me.statusStrip.Name = "statusStrip"
        Me.statusStrip.Size = New System.Drawing.Size(518, 22)
        Me.statusStrip.TabIndex = 65
        Me.statusStrip.Text = "statusStrip"
        '
        'toolStripStatusLabel
        '
        Me.toolStripStatusLabel.Name = "toolStripStatusLabel"
        Me.toolStripStatusLabel.Size = New System.Drawing.Size(0, 17)
        '
        'listView
        '
        Me.listView.AllowDrop = True
        Me.listView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.listView.BackColor = System.Drawing.SystemColors.Window
        Me.listView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.clm_Files})
        Me.listView.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.5!)
        Me.listView.FullRowSelect = True
        Me.listView.GridLines = True
        Me.listView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.listView.Location = New System.Drawing.Point(-1, 64)
        Me.listView.Name = "listView"
        Me.listView.Size = New System.Drawing.Size(520, 129)
        Me.listView.TabIndex = 7
        Me.listView.UseCompatibleStateImageBehavior = False
        Me.listView.View = System.Windows.Forms.View.Details
        '
        'clm_Files
        '
        Me.clm_Files.Text = "Drop video files below"
        Me.clm_Files.Width = 516
        '
        'cbx_StreamCopy
        '
        Me.cbx_StreamCopy.AutoSize = True
        Me.cbx_StreamCopy.Location = New System.Drawing.Point(225, 39)
        Me.cbx_StreamCopy.Name = "cbx_StreamCopy"
        Me.cbx_StreamCopy.Size = New System.Drawing.Size(86, 17)
        Me.cbx_StreamCopy.TabIndex = 66
        Me.cbx_StreamCopy.Text = "&Stream Copy"
        Me.toolTip.SetToolTip(Me.cbx_StreamCopy, resources.GetString("cbx_StreamCopy.ToolTip"))
        Me.cbx_StreamCopy.UseVisualStyleBackColor = True
        '
        'frm_Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(518, 276)
        Me.Controls.Add(Me.cbx_StreamCopy)
        Me.Controls.Add(Me.statusStrip)
        Me.Controls.Add(Me.btn_Advanced)
        Me.Controls.Add(Me.cbx_TVOutput)
        Me.Controls.Add(Me.pbx_ImageTitleLeft)
        Me.Controls.Add(Me.pbx_ImageTitleRight)
        Me.Controls.Add(Me.pbx_ImageTitleMiddle)
        Me.Controls.Add(Me.cbx_AC3Passthrough)
        Me.Controls.Add(Me.cbx_AutoSplit4GB)
        Me.Controls.Add(Me.cbx_OutputFolder)
        Me.Controls.Add(Me.cbx_H264Encoding)
        Me.Controls.Add(Me.btn_ClearAll)
        Me.Controls.Add(Me.btn_RemoveFiles)
        Me.Controls.Add(Me.btn_AddFiles)
        Me.Controls.Add(Me.listView)
        Me.Controls.Add(Me.pgb_Progress)
        Me.Controls.Add(Me.cbx_ConversionDevice)
        Me.Controls.Add(Me.btn_StartStop)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(534, 300)
        Me.Name = "frm_Main"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "EncodeHD"
        CType(Me.pbx_ImageTitleMiddle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbx_ImageTitleLeft, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbx_ImageTitleRight, System.ComponentModel.ISupportInitialize).EndInit()
        Me.statusStrip.ResumeLayout(False)
        Me.statusStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btn_StartStop As System.Windows.Forms.Button
    Friend WithEvents cbx_ConversionDevice As System.Windows.Forms.ComboBox
    Friend WithEvents dlg_OpenFile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents pgb_Progress As System.Windows.Forms.ProgressBar
    Friend WithEvents listView As cmp_ListViewPlus
    Friend WithEvents clm_Files As System.Windows.Forms.ColumnHeader
    Friend WithEvents btn_ClearAll As System.Windows.Forms.Button
    Friend WithEvents btn_RemoveFiles As System.Windows.Forms.Button
    Friend WithEvents btn_AddFiles As System.Windows.Forms.Button
    Friend WithEvents cbx_H264Encoding As System.Windows.Forms.CheckBox
    Friend WithEvents cbx_AC3Passthrough As System.Windows.Forms.CheckBox
    Friend WithEvents cbx_AutoSplit4GB As System.Windows.Forms.CheckBox
    Friend WithEvents cbx_OutputFolder As System.Windows.Forms.CheckBox
    Friend WithEvents pbx_ImageTitleMiddle As System.Windows.Forms.PictureBox
    Friend WithEvents pbx_ImageTitleLeft As System.Windows.Forms.PictureBox
    Friend WithEvents pbx_ImageTitleRight As System.Windows.Forms.PictureBox
    Friend WithEvents dlg_OpenFolder As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents toolTip As System.Windows.Forms.ToolTip
    Friend WithEvents cbx_TVOutput As System.Windows.Forms.CheckBox
    Friend WithEvents btn_Advanced As System.Windows.Forms.Button
    Friend WithEvents statusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents toolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents cbx_StreamCopy As System.Windows.Forms.CheckBox

End Class
