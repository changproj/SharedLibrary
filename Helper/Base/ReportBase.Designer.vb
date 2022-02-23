<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReportBase
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
    Protected components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Protected Sub InitializeComponent()
        Me.gbxAccMonth = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtAccMth = New System.Windows.Forms.TextBox()
        Me.dtpRunDate = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.gbxRpType = New System.Windows.Forms.GroupBox()
        Me.optSummary = New System.Windows.Forms.RadioButton()
        Me.optDetail = New System.Windows.Forms.RadioButton()
        Me.gbxOption1 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtT = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtF = New System.Windows.Forms.TextBox()
        Me.optS = New System.Windows.Forms.RadioButton()
        Me.optA = New System.Windows.Forms.RadioButton()
        Me.gbxOption3 = New System.Windows.Forms.GroupBox()
        Me.cbxShowSummary = New System.Windows.Forms.CheckBox()
        Me.cbxShowPrintDate = New System.Windows.Forms.CheckBox()
        Me.cbxSeparatePage = New System.Windows.Forms.CheckBox()
        Me.cbxShowPage = New System.Windows.Forms.CheckBox()
        Me.gbxSelectAccNo = New System.Windows.Forms.GroupBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lbxAccNo = New System.Windows.Forms.ListBox()
        Me.gbxAccNo = New System.Windows.Forms.GroupBox()
        Me.WmAccNoT = New Helper.WaterMarkTextBox()
        Me.WmAccNoF = New Helper.WaterMarkTextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.optAccNoS = New System.Windows.Forms.RadioButton()
        Me.optAccNoA = New System.Windows.Forms.RadioButton()
        Me.gbxOption2 = New System.Windows.Forms.GroupBox()
        Me.cboCbx = New System.Windows.Forms.ComboBox()
        Me.optCbxS = New System.Windows.Forms.RadioButton()
        Me.optCbxA = New System.Windows.Forms.RadioButton()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.gbxAccMonth.SuspendLayout()
        Me.gbxRpType.SuspendLayout()
        Me.gbxOption1.SuspendLayout()
        Me.gbxOption3.SuspendLayout()
        Me.gbxSelectAccNo.SuspendLayout()
        Me.gbxAccNo.SuspendLayout()
        Me.gbxOption2.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbxAccMonth
        '
        Me.gbxAccMonth.Controls.Add(Me.Label1)
        Me.gbxAccMonth.Controls.Add(Me.txtAccMth)
        Me.gbxAccMonth.Controls.Add(Me.dtpRunDate)
        Me.gbxAccMonth.Controls.Add(Me.Label2)
        Me.gbxAccMonth.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbxAccMonth.Location = New System.Drawing.Point(12, 88)
        Me.gbxAccMonth.Name = "gbxAccMonth"
        Me.gbxAccMonth.Size = New System.Drawing.Size(274, 94)
        Me.gbxAccMonth.TabIndex = 0
        Me.gbxAccMonth.TabStop = False
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(7, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(91, 13)
        Me.Label1.TabIndex = 76
        Me.Label1.Text = "Account Month"
        '
        'txtAccMth
        '
        Me.txtAccMth.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAccMth.Location = New System.Drawing.Point(111, 23)
        Me.txtAccMth.Name = "txtAccMth"
        Me.txtAccMth.Size = New System.Drawing.Size(80, 20)
        Me.txtAccMth.TabIndex = 0
        '
        'dtpRunDate
        '
        Me.dtpRunDate.Checked = False
        Me.dtpRunDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpRunDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpRunDate.Location = New System.Drawing.Point(111, 54)
        Me.dtpRunDate.Name = "dtpRunDate"
        Me.dtpRunDate.Size = New System.Drawing.Size(127, 20)
        Me.dtpRunDate.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(7, 54)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(91, 13)
        Me.Label2.TabIndex = 106
        Me.Label2.Text = "Run Date"
        '
        'btnExit
        '
        Me.btnExit.Image = Global.GeneralAcc.My.Resources.Resources.Close
        Me.btnExit.Location = New System.Drawing.Point(91, 226)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(73, 79)
        Me.btnExit.TabIndex = 113
        Me.btnExit.Text = "Exit"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnPreview
        '
        Me.btnPreview.Image = Global.GeneralAcc.My.Resources.Resources.Preview
        Me.btnPreview.Location = New System.Drawing.Point(12, 226)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(73, 79)
        Me.btnPreview.TabIndex = 112
        Me.btnPreview.Text = "Preview"
        Me.btnPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnPreview.UseVisualStyleBackColor = True
        '
        'gbxRpType
        '
        Me.gbxRpType.Controls.Add(Me.optSummary)
        Me.gbxRpType.Controls.Add(Me.optDetail)
        Me.gbxRpType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbxRpType.Location = New System.Drawing.Point(12, 12)
        Me.gbxRpType.Name = "gbxRpType"
        Me.gbxRpType.Size = New System.Drawing.Size(274, 67)
        Me.gbxRpType.TabIndex = 1
        Me.gbxRpType.TabStop = False
        Me.gbxRpType.Text = "Select Type"
        '
        'optSummary
        '
        Me.optSummary.AutoSize = True
        Me.optSummary.Location = New System.Drawing.Point(10, 28)
        Me.optSummary.Name = "optSummary"
        Me.optSummary.Size = New System.Drawing.Size(68, 17)
        Me.optSummary.TabIndex = 89
        Me.optSummary.Text = "Summary"
        Me.optSummary.UseVisualStyleBackColor = True
        '
        'optDetail
        '
        Me.optDetail.AutoSize = True
        Me.optDetail.Checked = True
        Me.optDetail.Location = New System.Drawing.Point(131, 28)
        Me.optDetail.Name = "optDetail"
        Me.optDetail.Size = New System.Drawing.Size(52, 17)
        Me.optDetail.TabIndex = 90
        Me.optDetail.TabStop = True
        Me.optDetail.Text = "Detail"
        Me.optDetail.UseVisualStyleBackColor = True
        '
        'gbxOption1
        '
        Me.gbxOption1.Controls.Add(Me.Label4)
        Me.gbxOption1.Controls.Add(Me.txtT)
        Me.gbxOption1.Controls.Add(Me.Label3)
        Me.gbxOption1.Controls.Add(Me.txtF)
        Me.gbxOption1.Controls.Add(Me.optS)
        Me.gbxOption1.Controls.Add(Me.optA)
        Me.gbxOption1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbxOption1.Location = New System.Drawing.Point(302, 12)
        Me.gbxOption1.Name = "gbxOption1"
        Me.gbxOption1.Size = New System.Drawing.Size(243, 97)
        Me.gbxOption1.TabIndex = 3
        Me.gbxOption1.TabStop = False
        Me.gbxOption1.Text = "Option 1"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(111, 70)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(20, 13)
        Me.Label4.TabIndex = 79
        Me.Label4.Text = "To"
        '
        'txtT
        '
        Me.txtT.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtT.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtT.Location = New System.Drawing.Point(147, 67)
        Me.txtT.Name = "txtT"
        Me.txtT.Size = New System.Drawing.Size(90, 20)
        Me.txtT.TabIndex = 78
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(111, 48)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(30, 13)
        Me.Label3.TabIndex = 77
        Me.Label3.Text = "From"
        '
        'txtF
        '
        Me.txtF.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtF.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtF.Location = New System.Drawing.Point(147, 41)
        Me.txtF.Name = "txtF"
        Me.txtF.Size = New System.Drawing.Size(90, 20)
        Me.txtF.TabIndex = 76
        '
        'optS
        '
        Me.optS.AutoSize = True
        Me.optS.Location = New System.Drawing.Point(7, 46)
        Me.optS.Name = "optS"
        Me.optS.Size = New System.Drawing.Size(55, 17)
        Me.optS.TabIndex = 11
        Me.optS.Text = "Select"
        Me.optS.UseVisualStyleBackColor = True
        '
        'optA
        '
        Me.optA.AutoSize = True
        Me.optA.Checked = True
        Me.optA.Location = New System.Drawing.Point(8, 23)
        Me.optA.Name = "optA"
        Me.optA.Size = New System.Drawing.Size(39, 17)
        Me.optA.TabIndex = 10
        Me.optA.TabStop = True
        Me.optA.Text = "All "
        Me.optA.UseVisualStyleBackColor = True
        '
        'gbxOption3
        '
        Me.gbxOption3.Controls.Add(Me.cbxShowSummary)
        Me.gbxOption3.Controls.Add(Me.cbxShowPrintDate)
        Me.gbxOption3.Controls.Add(Me.cbxSeparatePage)
        Me.gbxOption3.Controls.Add(Me.cbxShowPage)
        Me.gbxOption3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbxOption3.Location = New System.Drawing.Point(554, 130)
        Me.gbxOption3.Name = "gbxOption3"
        Me.gbxOption3.Size = New System.Drawing.Size(192, 152)
        Me.gbxOption3.TabIndex = 6
        Me.gbxOption3.TabStop = False
        Me.gbxOption3.Text = "Option"
        '
        'cbxShowSummary
        '
        Me.cbxShowSummary.AutoSize = True
        Me.cbxShowSummary.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbxShowSummary.Location = New System.Drawing.Point(18, 101)
        Me.cbxShowSummary.Name = "cbxShowSummary"
        Me.cbxShowSummary.Size = New System.Drawing.Size(99, 17)
        Me.cbxShowSummary.TabIndex = 117
        Me.cbxShowSummary.TabStop = False
        Me.cbxShowSummary.Text = "Show Summary"
        Me.cbxShowSummary.UseVisualStyleBackColor = True
        '
        'cbxShowPrintDate
        '
        Me.cbxShowPrintDate.AutoSize = True
        Me.cbxShowPrintDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbxShowPrintDate.Location = New System.Drawing.Point(18, 78)
        Me.cbxShowPrintDate.Name = "cbxShowPrintDate"
        Me.cbxShowPrintDate.Size = New System.Drawing.Size(103, 17)
        Me.cbxShowPrintDate.TabIndex = 114
        Me.cbxShowPrintDate.TabStop = False
        Me.cbxShowPrintDate.Text = "Show Print Date"
        Me.cbxShowPrintDate.UseVisualStyleBackColor = True
        '
        'cbxSeparatePage
        '
        Me.cbxSeparatePage.AutoSize = True
        Me.cbxSeparatePage.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbxSeparatePage.Location = New System.Drawing.Point(18, 32)
        Me.cbxSeparatePage.Name = "cbxSeparatePage"
        Me.cbxSeparatePage.Size = New System.Drawing.Size(127, 17)
        Me.cbxSeparatePage.TabIndex = 113
        Me.cbxSeparatePage.TabStop = False
        Me.cbxSeparatePage.Text = "Show Separate Page"
        Me.cbxSeparatePage.UseVisualStyleBackColor = True
        '
        'cbxShowPage
        '
        Me.cbxShowPage.AutoSize = True
        Me.cbxShowPage.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbxShowPage.Location = New System.Drawing.Point(18, 55)
        Me.cbxShowPage.Name = "cbxShowPage"
        Me.cbxShowPage.Size = New System.Drawing.Size(98, 17)
        Me.cbxShowPage.TabIndex = 112
        Me.cbxShowPage.TabStop = False
        Me.cbxShowPage.Text = "Show Page No"
        Me.cbxShowPage.UseVisualStyleBackColor = True
        '
        'gbxSelectAccNo
        '
        Me.gbxSelectAccNo.Controls.Add(Me.Label9)
        Me.gbxSelectAccNo.Controls.Add(Me.lbxAccNo)
        Me.gbxSelectAccNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbxSelectAccNo.Location = New System.Drawing.Point(302, 130)
        Me.gbxSelectAccNo.Name = "gbxSelectAccNo"
        Me.gbxSelectAccNo.Size = New System.Drawing.Size(218, 217)
        Me.gbxSelectAccNo.TabIndex = 4
        Me.gbxSelectAccNo.TabStop = False
        Me.gbxSelectAccNo.Text = "Selected A/C No"
        '
        'Label9
        '
        Me.Label9.ForeColor = System.Drawing.Color.Blue
        Me.Label9.Location = New System.Drawing.Point(53, 198)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(165, 16)
        Me.Label9.TabIndex = 79
        Me.Label9.Text = "Double-click A/C No. to remove"
        '
        'lbxAccNo
        '
        Me.lbxAccNo.FormattingEnabled = True
        Me.lbxAccNo.Location = New System.Drawing.Point(56, 47)
        Me.lbxAccNo.Name = "lbxAccNo"
        Me.lbxAccNo.Size = New System.Drawing.Size(138, 147)
        Me.lbxAccNo.TabIndex = 67
        '
        'gbxAccNo
        '
        Me.gbxAccNo.Controls.Add(Me.WmAccNoT)
        Me.gbxAccNo.Controls.Add(Me.WmAccNoF)
        Me.gbxAccNo.Controls.Add(Me.Label5)
        Me.gbxAccNo.Controls.Add(Me.Label6)
        Me.gbxAccNo.Controls.Add(Me.optAccNoS)
        Me.gbxAccNo.Controls.Add(Me.optAccNoA)
        Me.gbxAccNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbxAccNo.Location = New System.Drawing.Point(75, 375)
        Me.gbxAccNo.Name = "gbxAccNo"
        Me.gbxAccNo.Size = New System.Drawing.Size(323, 117)
        Me.gbxAccNo.TabIndex = 2
        Me.gbxAccNo.TabStop = False
        Me.gbxAccNo.Text = "A/C No"
        '
        'WmAccNoT
        '
        Me.WmAccNoT.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.WmAccNoT.Location = New System.Drawing.Point(117, 72)
        Me.WmAccNoT.Name = "WmAccNoT"
        Me.WmAccNoT.Size = New System.Drawing.Size(183, 20)
        Me.WmAccNoT.TabIndex = 108
        Me.WmAccNoT.WaterMarkColor = System.Drawing.Color.Gray
        Me.WmAccNoT.WaterMarkText = "Water Mark"
        '
        'WmAccNoF
        '
        Me.WmAccNoF.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.WmAccNoF.Location = New System.Drawing.Point(117, 46)
        Me.WmAccNoF.Name = "WmAccNoF"
        Me.WmAccNoF.Size = New System.Drawing.Size(183, 20)
        Me.WmAccNoF.TabIndex = 107
        Me.WmAccNoF.WaterMarkColor = System.Drawing.Color.Gray
        Me.WmAccNoF.WaterMarkText = "Water Mark"
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(78, 47)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(33, 23)
        Me.Label5.TabIndex = 18
        Me.Label5.Text = "From"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(82, 72)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(26, 23)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "to"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'optAccNoS
        '
        Me.optAccNoS.AutoSize = True
        Me.optAccNoS.Location = New System.Drawing.Point(7, 46)
        Me.optAccNoS.Name = "optAccNoS"
        Me.optAccNoS.Size = New System.Drawing.Size(55, 17)
        Me.optAccNoS.TabIndex = 11
        Me.optAccNoS.Text = "Select"
        Me.optAccNoS.UseVisualStyleBackColor = True
        '
        'optAccNoA
        '
        Me.optAccNoA.AutoSize = True
        Me.optAccNoA.Checked = True
        Me.optAccNoA.Location = New System.Drawing.Point(8, 23)
        Me.optAccNoA.Name = "optAccNoA"
        Me.optAccNoA.Size = New System.Drawing.Size(36, 17)
        Me.optAccNoA.TabIndex = 10
        Me.optAccNoA.TabStop = True
        Me.optAccNoA.Text = "All"
        Me.optAccNoA.UseVisualStyleBackColor = True
        '
        'gbxOption2
        '
        Me.gbxOption2.Controls.Add(Me.cboCbx)
        Me.gbxOption2.Controls.Add(Me.optCbxS)
        Me.gbxOption2.Controls.Add(Me.optCbxA)
        Me.gbxOption2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbxOption2.Location = New System.Drawing.Point(457, 398)
        Me.gbxOption2.Name = "gbxOption2"
        Me.gbxOption2.Size = New System.Drawing.Size(218, 82)
        Me.gbxOption2.TabIndex = 5
        Me.gbxOption2.TabStop = False
        Me.gbxOption2.Text = "Option 2"
        '
        'cboCbx
        '
        Me.cboCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCbx.FormattingEnabled = True
        Me.cboCbx.Location = New System.Drawing.Point(85, 42)
        Me.cboCbx.Name = "cboCbx"
        Me.cboCbx.Size = New System.Drawing.Size(124, 21)
        Me.cboCbx.TabIndex = 12
        '
        'optCbxS
        '
        Me.optCbxS.AutoSize = True
        Me.optCbxS.Location = New System.Drawing.Point(7, 46)
        Me.optCbxS.Name = "optCbxS"
        Me.optCbxS.Size = New System.Drawing.Size(55, 17)
        Me.optCbxS.TabIndex = 11
        Me.optCbxS.Text = "Select"
        Me.optCbxS.UseVisualStyleBackColor = True
        '
        'optCbxA
        '
        Me.optCbxA.AutoSize = True
        Me.optCbxA.Checked = True
        Me.optCbxA.Location = New System.Drawing.Point(8, 23)
        Me.optCbxA.Name = "optCbxA"
        Me.optCbxA.Size = New System.Drawing.Size(36, 17)
        Me.optCbxA.TabIndex = 10
        Me.optCbxA.TabStop = True
        Me.optCbxA.Text = "All"
        Me.optCbxA.UseVisualStyleBackColor = True
        '
        'btnPrint
        '
        Me.btnPrint.Image = Global.GeneralAcc.My.Resources.Resources.Print2
        Me.btnPrint.Location = New System.Drawing.Point(170, 226)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(73, 79)
        Me.btnPrint.TabIndex = 126
        Me.btnPrint.Text = "Print"
        Me.btnPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'ReportBase
        '
        Me.ClientSize = New System.Drawing.Size(789, 544)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.gbxOption2)
        Me.Controls.Add(Me.gbxAccNo)
        Me.Controls.Add(Me.gbxSelectAccNo)
        Me.Controls.Add(Me.gbxOption3)
        Me.Controls.Add(Me.gbxOption1)
        Me.Controls.Add(Me.gbxRpType)
        Me.Controls.Add(Me.gbxAccMonth)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnPreview)
        Me.Name = "ReportBase"
        Me.gbxAccMonth.ResumeLayout(False)
        Me.gbxAccMonth.PerformLayout()
        Me.gbxRpType.ResumeLayout(False)
        Me.gbxRpType.PerformLayout()
        Me.gbxOption1.ResumeLayout(False)
        Me.gbxOption1.PerformLayout()
        Me.gbxOption3.ResumeLayout(False)
        Me.gbxOption3.PerformLayout()
        Me.gbxSelectAccNo.ResumeLayout(False)
        Me.gbxAccNo.ResumeLayout(False)
        Me.gbxAccNo.PerformLayout()
        Me.gbxOption2.ResumeLayout(False)
        Me.gbxOption2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Protected WithEvents gbxAccMonth As GroupBox
    Protected WithEvents Label1 As Label
    Protected WithEvents txtAccMth As TextBox
    Protected WithEvents dtpRunDate As DateTimePicker
    Protected WithEvents Label2 As Label
    Protected WithEvents btnExit As Button
    Protected WithEvents btnPreview As Button
    Protected WithEvents gbxRpType As GroupBox
    Protected WithEvents optSummary As RadioButton
    Protected WithEvents optDetail As RadioButton
    Protected WithEvents gbxOption1 As GroupBox
    Protected WithEvents Label4 As Label
    Protected WithEvents txtT As TextBox
    Protected WithEvents Label3 As Label
    Protected WithEvents txtF As TextBox
    Protected WithEvents optS As RadioButton
    Protected WithEvents optA As RadioButton
    Protected WithEvents gbxOption3 As GroupBox
    Protected WithEvents cbxShowSummary As CheckBox
    Protected WithEvents cbxShowPrintDate As CheckBox
    Protected WithEvents cbxSeparatePage As CheckBox
    Protected WithEvents cbxShowPage As CheckBox
    Protected WithEvents gbxSelectAccNo As GroupBox
    Protected WithEvents Label9 As Label
    Protected WithEvents lbxAccNo As ListBox
    Protected WithEvents gbxAccNo As GroupBox
    Protected WithEvents Label5 As Label
    Protected WithEvents Label6 As Label
    Protected WithEvents optAccNoS As RadioButton
    Protected WithEvents optAccNoA As RadioButton
    Protected WithEvents WmAccNoT As Helper.WaterMarkTextBox
    Protected WithEvents WmAccNoF As Helper.WaterMarkTextBox
    Protected WithEvents gbxOption2 As GroupBox
    Protected WithEvents cboCbx As ComboBox
    Protected WithEvents optCbxS As RadioButton
    Protected WithEvents optCbxA As RadioButton
    Protected WithEvents btnPrint As Button
End Class
