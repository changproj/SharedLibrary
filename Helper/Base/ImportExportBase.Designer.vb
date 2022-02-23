<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ImportExportBase
    Inherits GeneralAcc.WinFormBase

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.txtAccMonth = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtFileName = New System.Windows.Forms.TextBox()
        Me.btnBrowseDestination = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtDestAddress = New System.Windows.Forms.TextBox()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.cboFileType = New System.Windows.Forms.ComboBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.gbOption = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cbxCreateFolder = New System.Windows.Forms.CheckBox()
        Me.gbAccMonth = New System.Windows.Forms.GroupBox()
        Me.btnLoading = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbOption.SuspendLayout()
        Me.gbAccMonth.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtAccMonth
        '
        Me.txtAccMonth.Location = New System.Drawing.Point(77, 28)
        Me.txtAccMonth.MaxLength = 6
        Me.txtAccMonth.Name = "txtAccMonth"
        Me.txtAccMonth.Size = New System.Drawing.Size(55, 20)
        Me.txtAccMonth.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(3, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 23)
        Me.Label1.TabIndex = 141
        Me.Label1.Text = "Acc Month "
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(14, 118)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(306, 24)
        Me.ProgressBar1.TabIndex = 136
        '
        'lblStatus
        '
        Me.lblStatus.Location = New System.Drawing.Point(12, 268)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(539, 39)
        Me.lblStatus.TabIndex = 137
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtFileName)
        Me.GroupBox1.Controls.Add(Me.btnBrowseDestination)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.TxtDestAddress)
        Me.GroupBox1.Location = New System.Drawing.Point(15, 148)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(539, 117)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Path"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(12, 79)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(71, 23)
        Me.Label4.TabIndex = 143
        Me.Label4.Text = "File Name"
        '
        'txtFileName
        '
        Me.txtFileName.Location = New System.Drawing.Point(89, 79)
        Me.txtFileName.MaxLength = 6
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.Size = New System.Drawing.Size(407, 20)
        Me.txtFileName.TabIndex = 142
        '
        'btnBrowseDestination
        '
        Me.btnBrowseDestination.Location = New System.Drawing.Point(502, 22)
        Me.btnBrowseDestination.Name = "btnBrowseDestination"
        Me.btnBrowseDestination.Size = New System.Drawing.Size(31, 21)
        Me.btnBrowseDestination.TabIndex = 1
        Me.btnBrowseDestination.Text = "...."
        Me.btnBrowseDestination.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(12, 27)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 23)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Destination"
        '
        'TxtDestAddress
        '
        Me.TxtDestAddress.Location = New System.Drawing.Point(89, 23)
        Me.TxtDestAddress.Multiline = True
        Me.TxtDestAddress.Name = "TxtDestAddress"
        Me.TxtDestAddress.Size = New System.Drawing.Size(407, 50)
        Me.TxtDestAddress.TabIndex = 3
        '
        'btnExit
        '
        Me.btnExit.Image = Global.GeneralAcc.My.Resources.Resources.Close
        Me.btnExit.Location = New System.Drawing.Point(86, 310)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(68, 68)
        Me.btnExit.TabIndex = 3
        Me.btnExit.Text = "Exit"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(336, 16)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(217, 126)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 138
        Me.PictureBox1.TabStop = False
        '
        'btnExport
        '
        Me.btnExport.BackColor = System.Drawing.SystemColors.Control
        Me.btnExport.Image = Global.GeneralAcc.My.Resources.Resources.Export
        Me.btnExport.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnExport.Location = New System.Drawing.Point(15, 310)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(70, 68)
        Me.btnExport.TabIndex = 2
        Me.btnExport.Text = "Export"
        Me.btnExport.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnExport.UseVisualStyleBackColor = False
        '
        'cboFileType
        '
        Me.cboFileType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFileType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboFileType.FormattingEnabled = True
        Me.cboFileType.Location = New System.Drawing.Point(73, 28)
        Me.cboFileType.Name = "cboFileType"
        Me.cboFileType.Size = New System.Drawing.Size(69, 21)
        Me.cboFileType.TabIndex = 143
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(8, 31)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(50, 13)
        Me.Label16.TabIndex = 144
        Me.Label16.Text = "File Type"
        '
        'gbOption
        '
        Me.gbOption.Controls.Add(Me.Label3)
        Me.gbOption.Controls.Add(Me.cbxCreateFolder)
        Me.gbOption.Controls.Add(Me.cboFileType)
        Me.gbOption.Controls.Add(Me.Label16)
        Me.gbOption.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbOption.Location = New System.Drawing.Point(14, 12)
        Me.gbOption.Name = "gbOption"
        Me.gbOption.Size = New System.Drawing.Size(150, 91)
        Me.gbOption.TabIndex = 4
        Me.gbOption.TabStop = False
        Me.gbOption.Text = "Option"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(9, 62)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(86, 15)
        Me.Label3.TabIndex = 148
        Me.Label3.Text = "Create Folder"
        '
        'cbxCreateFolder
        '
        Me.cbxCreateFolder.AutoSize = True
        Me.cbxCreateFolder.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbxCreateFolder.Location = New System.Drawing.Point(125, 63)
        Me.cbxCreateFolder.Name = "cbxCreateFolder"
        Me.cbxCreateFolder.Size = New System.Drawing.Size(15, 14)
        Me.cbxCreateFolder.TabIndex = 147
        Me.cbxCreateFolder.TabStop = False
        Me.cbxCreateFolder.UseVisualStyleBackColor = True
        '
        'gbAccMonth
        '
        Me.gbAccMonth.Controls.Add(Me.txtAccMonth)
        Me.gbAccMonth.Controls.Add(Me.Label1)
        Me.gbAccMonth.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbAccMonth.Location = New System.Drawing.Point(170, 12)
        Me.gbAccMonth.Name = "gbAccMonth"
        Me.gbAccMonth.Size = New System.Drawing.Size(150, 91)
        Me.gbAccMonth.TabIndex = 0
        Me.gbAccMonth.TabStop = False
        '
        'btnLoading
        '
        Me.btnLoading.Image = Global.GeneralAcc.My.Resources.Resources.LoadingProgress
        Me.btnLoading.Location = New System.Drawing.Point(218, 138)
        Me.btnLoading.Name = "btnLoading"
        Me.btnLoading.Size = New System.Drawing.Size(136, 123)
        Me.btnLoading.TabIndex = 140
        Me.btnLoading.UseVisualStyleBackColor = True
        '
        'ImportExportBase
        '
        Me.ClientSize = New System.Drawing.Size(572, 398)
        Me.Controls.Add(Me.btnLoading)
        Me.Controls.Add(Me.gbAccMonth)
        Me.Controls.Add(Me.gbOption)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.btnExport)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "ImportExportBase"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbOption.ResumeLayout(False)
        Me.gbOption.PerformLayout()
        Me.gbAccMonth.ResumeLayout(False)
        Me.gbAccMonth.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Protected WithEvents txtAccMonth As TextBox
    Protected WithEvents Label1 As Label
    Protected WithEvents btnExit As Button
    Protected WithEvents PictureBox1 As PictureBox
    Protected WithEvents ProgressBar1 As ProgressBar
    Protected WithEvents lblStatus As Label
    Protected WithEvents btnExport As Button
    Protected WithEvents GroupBox1 As GroupBox
    Protected WithEvents btnBrowseDestination As Button
    Protected WithEvents Label2 As Label
    Protected WithEvents TxtDestAddress As TextBox
    Protected WithEvents cboFileType As ComboBox
    Protected WithEvents Label16 As Label
    Protected WithEvents gbOption As GroupBox
    Friend WithEvents cbxCreateFolder As CheckBox
    Protected WithEvents Label3 As Label
    Protected WithEvents gbAccMonth As GroupBox
    Protected WithEvents Label4 As Label
    Protected WithEvents txtFileName As TextBox
    Protected WithEvents btnLoading As Button
End Class
