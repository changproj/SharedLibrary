<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MtnBase
    Inherits GeneralAcc.WinFormBase

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Protected Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MtnBase))
        Me.lblEditedOn = New System.Windows.Forms.Label()
        Me.gbxSearch = New System.Windows.Forms.GroupBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.dgvSearch = New System.Windows.Forms.DataGridView()
        Me.btnLoading = New System.Windows.Forms.Button()
        Me.BindingNavigator1 = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripTextBox1 = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton4 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.lblFiltered = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.cboSearch = New System.Windows.Forms.ComboBox()
        Me.gbxDetail = New System.Windows.Forms.GroupBox()
        Me.lblEditedBy = New System.Windows.Forms.Label()
        Me.lblCreatedOn = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsbtnNew = New System.Windows.Forms.ToolStripButton()
        Me.tsbtnEdit = New System.Windows.Forms.ToolStripButton()
        Me.tsbtnDelete = New System.Windows.Forms.ToolStripButton()
        Me.tsbtnPrint = New System.Windows.Forms.ToolStripButton()
        Me.tsbtnSave = New System.Windows.Forms.ToolStripButton()
        Me.tsbtnCancel = New System.Windows.Forms.ToolStripButton()
        Me.tsbtnExit = New System.Windows.Forms.ToolStripButton()
        Me.lblCreatedBy = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.gbxSearch.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.dgvSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingNavigator1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BindingNavigator1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblEditedOn
        '
        Me.lblEditedOn.Location = New System.Drawing.Point(364, 42)
        Me.lblEditedOn.Name = "lblEditedOn"
        Me.lblEditedOn.Size = New System.Drawing.Size(189, 23)
        Me.lblEditedOn.TabIndex = 7
        '
        'gbxSearch
        '
        Me.gbxSearch.Controls.Add(Me.Panel1)
        Me.gbxSearch.Controls.Add(Me.lblFiltered)
        Me.gbxSearch.Controls.Add(Me.txtSearch)
        Me.gbxSearch.Controls.Add(Me.cboSearch)
        Me.gbxSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbxSearch.Location = New System.Drawing.Point(12, 28)
        Me.gbxSearch.Name = "gbxSearch"
        Me.gbxSearch.Padding = New System.Windows.Forms.Padding(10)
        Me.gbxSearch.Size = New System.Drawing.Size(475, 519)
        Me.gbxSearch.TabIndex = 33
        Me.gbxSearch.TabStop = False
        Me.gbxSearch.Text = "Search"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnLoading)
        Me.Panel1.Controls.Add(Me.dgvSearch)
        Me.Panel1.Controls.Add(Me.BindingNavigator1)
        Me.Panel1.Location = New System.Drawing.Point(7, 45)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(464, 467)
        Me.Panel1.TabIndex = 43
        '
        'dgvSearch
        '
        Me.dgvSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSearch.Location = New System.Drawing.Point(3, 7)
        Me.dgvSearch.Name = "dgvSearch"
        Me.dgvSearch.Size = New System.Drawing.Size(457, 429)
        Me.dgvSearch.TabIndex = 41
        '
        'btnLoading
        '
        Me.btnLoading.Image = Global.GeneralAcc.My.Resources.Resources.LoadingProgress
        Me.btnLoading.Location = New System.Drawing.Point(150, 106)
        Me.btnLoading.Name = "btnLoading"
        Me.btnLoading.Size = New System.Drawing.Size(136, 123)
        Me.btnLoading.TabIndex = 44
        Me.btnLoading.UseVisualStyleBackColor = True
        '
        'BindingNavigator1
        '
        Me.BindingNavigator1.AddNewItem = Nothing
        Me.BindingNavigator1.AutoSize = False
        Me.BindingNavigator1.CountItem = Me.ToolStripLabel1
        Me.BindingNavigator1.DeleteItem = Nothing
        Me.BindingNavigator1.Dock = System.Windows.Forms.DockStyle.None
        Me.BindingNavigator1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.BindingNavigator1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1, Me.ToolStripButton2, Me.ToolStripSeparator1, Me.ToolStripTextBox1, Me.ToolStripLabel1, Me.ToolStripSeparator2, Me.ToolStripButton3, Me.ToolStripButton4, Me.ToolStripSeparator3})
        Me.BindingNavigator1.Location = New System.Drawing.Point(3, 439)
        Me.BindingNavigator1.MoveFirstItem = Me.ToolStripButton1
        Me.BindingNavigator1.MoveLastItem = Me.ToolStripButton4
        Me.BindingNavigator1.MoveNextItem = Me.ToolStripButton3
        Me.BindingNavigator1.MovePreviousItem = Me.ToolStripButton2
        Me.BindingNavigator1.Name = "BindingNavigator1"
        Me.BindingNavigator1.PositionItem = Me.ToolStripTextBox1
        Me.BindingNavigator1.Size = New System.Drawing.Size(232, 25)
        Me.BindingNavigator1.TabIndex = 42
        Me.BindingNavigator1.Text = "BindingNavigator1"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(35, 22)
        Me.ToolStripLabel1.Text = "of {0}"
        Me.ToolStripLabel1.ToolTipText = "Total number of items"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton1.Size = New System.Drawing.Size(28, 22)
        Me.ToolStripButton1.Text = "Move first"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton2.Size = New System.Drawing.Size(28, 22)
        Me.ToolStripButton2.Text = "Move previous"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripTextBox1
        '
        Me.ToolStripTextBox1.AccessibleName = "Position"
        Me.ToolStripTextBox1.AutoSize = False
        Me.ToolStripTextBox1.Name = "ToolStripTextBox1"
        Me.ToolStripTextBox1.Size = New System.Drawing.Size(50, 23)
        Me.ToolStripTextBox1.Text = "0"
        Me.ToolStripTextBox1.ToolTipText = "Current position"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton3.Image = CType(resources.GetObject("ToolStripButton3.Image"), System.Drawing.Image)
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton3.Size = New System.Drawing.Size(28, 22)
        Me.ToolStripButton3.Text = "Move next"
        '
        'ToolStripButton4
        '
        Me.ToolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton4.Image = CType(resources.GetObject("ToolStripButton4.Image"), System.Drawing.Image)
        Me.ToolStripButton4.Name = "ToolStripButton4"
        Me.ToolStripButton4.RightToLeftAutoMirrorImage = True
        Me.ToolStripButton4.Size = New System.Drawing.Size(28, 22)
        Me.ToolStripButton4.Text = "Move last"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'lblFiltered
        '
        Me.lblFiltered.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFiltered.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblFiltered.Location = New System.Drawing.Point(73, 1)
        Me.lblFiltered.Name = "lblFiltered"
        Me.lblFiltered.Size = New System.Drawing.Size(69, 13)
        Me.lblFiltered.TabIndex = 20
        Me.lblFiltered.Text = "(Filtered)"
        '
        'txtSearch
        '
        Me.txtSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.Location = New System.Drawing.Point(124, 19)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(284, 20)
        Me.txtSearch.TabIndex = 0
        '
        'cboSearch
        '
        Me.cboSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboSearch.FormattingEnabled = True
        Me.cboSearch.Location = New System.Drawing.Point(10, 19)
        Me.cboSearch.Name = "cboSearch"
        Me.cboSearch.Size = New System.Drawing.Size(108, 21)
        Me.cboSearch.TabIndex = 0
        '
        'gbxDetail
        '
        Me.gbxDetail.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbxDetail.Location = New System.Drawing.Point(496, 29)
        Me.gbxDetail.Name = "gbxDetail"
        Me.gbxDetail.Size = New System.Drawing.Size(623, 459)
        Me.gbxDetail.TabIndex = 34
        Me.gbxDetail.TabStop = False
        Me.gbxDetail.Text = "Details"
        '
        'lblEditedBy
        '
        Me.lblEditedBy.Location = New System.Drawing.Point(362, 16)
        Me.lblEditedBy.Name = "lblEditedBy"
        Me.lblEditedBy.Size = New System.Drawing.Size(199, 23)
        Me.lblEditedBy.TabIndex = 6
        '
        'lblCreatedOn
        '
        Me.lblCreatedOn.Location = New System.Drawing.Point(87, 42)
        Me.lblCreatedOn.Name = "lblCreatedOn"
        Me.lblCreatedOn.Size = New System.Drawing.Size(204, 23)
        Me.lblCreatedOn.TabIndex = 5
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbtnNew, Me.tsbtnEdit, Me.tsbtnDelete, Me.tsbtnPrint, Me.tsbtnSave, Me.tsbtnCancel, Me.tsbtnExit})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1132, 31)
        Me.ToolStrip1.TabIndex = 35
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'tsbtnNew
        '
        Me.tsbtnNew.Image = Global.GeneralAcc.My.Resources.Resources.Add
        Me.tsbtnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtnNew.Margin = New System.Windows.Forms.Padding(5, 1, 5, 2)
        Me.tsbtnNew.Name = "tsbtnNew"
        Me.tsbtnNew.Size = New System.Drawing.Size(59, 28)
        Me.tsbtnNew.Text = "New"
        '
        'tsbtnEdit
        '
        Me.tsbtnEdit.Image = Global.GeneralAcc.My.Resources.Resources.Edit
        Me.tsbtnEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtnEdit.Margin = New System.Windows.Forms.Padding(5, 1, 5, 2)
        Me.tsbtnEdit.Name = "tsbtnEdit"
        Me.tsbtnEdit.Size = New System.Drawing.Size(55, 28)
        Me.tsbtnEdit.Text = "Edit"
        '
        'tsbtnDelete
        '
        Me.tsbtnDelete.Image = Global.GeneralAcc.My.Resources.Resources.Delete
        Me.tsbtnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtnDelete.Margin = New System.Windows.Forms.Padding(5, 1, 5, 2)
        Me.tsbtnDelete.Name = "tsbtnDelete"
        Me.tsbtnDelete.Size = New System.Drawing.Size(68, 28)
        Me.tsbtnDelete.Text = "Delete"
        '
        'tsbtnPrint
        '
        Me.tsbtnPrint.Image = Global.GeneralAcc.My.Resources.Resources.Print
        Me.tsbtnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtnPrint.Margin = New System.Windows.Forms.Padding(5, 1, 5, 2)
        Me.tsbtnPrint.Name = "tsbtnPrint"
        Me.tsbtnPrint.Size = New System.Drawing.Size(60, 28)
        Me.tsbtnPrint.Text = "Print"
        '
        'tsbtnSave
        '
        Me.tsbtnSave.Image = Global.GeneralAcc.My.Resources.Resources.Save
        Me.tsbtnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtnSave.Margin = New System.Windows.Forms.Padding(5, 1, 5, 2)
        Me.tsbtnSave.Name = "tsbtnSave"
        Me.tsbtnSave.Size = New System.Drawing.Size(59, 28)
        Me.tsbtnSave.Text = "Save"
        '
        'tsbtnCancel
        '
        Me.tsbtnCancel.Image = Global.GeneralAcc.My.Resources.Resources.Cancel
        Me.tsbtnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtnCancel.Margin = New System.Windows.Forms.Padding(5, 1, 5, 2)
        Me.tsbtnCancel.Name = "tsbtnCancel"
        Me.tsbtnCancel.Size = New System.Drawing.Size(71, 28)
        Me.tsbtnCancel.Text = "Cancel"
        '
        'tsbtnExit
        '
        Me.tsbtnExit.Image = Global.GeneralAcc.My.Resources.Resources._Exit
        Me.tsbtnExit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtnExit.Margin = New System.Windows.Forms.Padding(5, 1, 5, 2)
        Me.tsbtnExit.Name = "tsbtnExit"
        Me.tsbtnExit.Size = New System.Drawing.Size(53, 28)
        Me.tsbtnExit.Text = "Exit"
        '
        'lblCreatedBy
        '
        Me.lblCreatedBy.Location = New System.Drawing.Point(87, 16)
        Me.lblCreatedBy.Name = "lblCreatedBy"
        Me.lblCreatedBy.Size = New System.Drawing.Size(204, 23)
        Me.lblCreatedBy.TabIndex = 4
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(304, 45)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(54, 13)
        Me.Label14.TabIndex = 3
        Me.Label14.Text = "Edited On"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(304, 16)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(52, 13)
        Me.Label12.TabIndex = 2
        Me.Label12.Text = "Edited By"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(22, 45)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(61, 13)
        Me.Label10.TabIndex = 1
        Me.Label10.Text = "Created On"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(22, 16)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(59, 13)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "Created By"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblEditedOn)
        Me.GroupBox1.Controls.Add(Me.lblEditedBy)
        Me.GroupBox1.Controls.Add(Me.lblCreatedOn)
        Me.GroupBox1.Controls.Add(Me.lblCreatedBy)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Location = New System.Drawing.Point(496, 485)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(623, 62)
        Me.GroupBox1.TabIndex = 36
        Me.GroupBox1.TabStop = False
        '
        'MtnBase
        '
        Me.ClientSize = New System.Drawing.Size(1132, 556)
        Me.Controls.Add(Me.gbxSearch)
        Me.Controls.Add(Me.gbxDetail)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "MtnBase"
        Me.gbxSearch.ResumeLayout(False)
        Me.gbxSearch.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        CType(Me.dgvSearch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingNavigator1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BindingNavigator1.ResumeLayout(False)
        Me.BindingNavigator1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Protected WithEvents lblEditedOn As Label
    Protected WithEvents gbxSearch As GroupBox
    Protected WithEvents lblFiltered As Label
    Protected WithEvents txtSearch As TextBox
    Protected WithEvents cboSearch As ComboBox
    Protected WithEvents gbxDetail As GroupBox
    Protected WithEvents lblEditedBy As Label
    Protected WithEvents lblCreatedOn As Label
    Protected WithEvents ToolStrip1 As ToolStrip
    Protected WithEvents tsbtnNew As ToolStripButton
    Protected WithEvents tsbtnEdit As ToolStripButton
    Protected WithEvents tsbtnDelete As ToolStripButton
    Protected WithEvents tsbtnPrint As ToolStripButton
    Protected WithEvents tsbtnSave As ToolStripButton
    Protected WithEvents tsbtnCancel As ToolStripButton
    Protected WithEvents tsbtnExit As ToolStripButton
    Protected WithEvents lblCreatedBy As Label
    Protected WithEvents Label14 As Label
    Protected WithEvents Label12 As Label
    Protected WithEvents Label10 As Label
    Protected WithEvents Label8 As Label
    Protected WithEvents GroupBox1 As GroupBox
    Protected WithEvents Panel1 As Panel
    Protected WithEvents BindingNavigator1 As BindingNavigator
    Protected WithEvents ToolStripLabel1 As ToolStripLabel
    Protected WithEvents ToolStripButton1 As ToolStripButton
    Protected WithEvents ToolStripButton2 As ToolStripButton
    Protected WithEvents ToolStripSeparator1 As ToolStripSeparator
    Protected WithEvents ToolStripTextBox1 As ToolStripTextBox
    Protected WithEvents ToolStripSeparator2 As ToolStripSeparator
    Protected WithEvents ToolStripButton3 As ToolStripButton
    Protected WithEvents ToolStripButton4 As ToolStripButton
    Protected WithEvents ToolStripSeparator3 As ToolStripSeparator
    Protected WithEvents dgvSearch As DataGridView
    Protected WithEvents btnLoading As Button
    Private components As System.ComponentModel.IContainer
End Class
