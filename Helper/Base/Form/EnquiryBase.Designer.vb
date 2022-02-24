<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EnquiryBase
    Inherits GeneralAcc.WinFormBase

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EnquiryBase))
        Me.gbxSearch = New System.Windows.Forms.GroupBox()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.lblFiltered = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.btnLoading = New System.Windows.Forms.Button()
        Me.dgvSearch = New System.Windows.Forms.DataGridView()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.gbxSearch.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.dgvSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gbxSearch
        '
        Me.gbxSearch.Controls.Add(Me.lblTotal)
        Me.gbxSearch.Controls.Add(Me.lblFiltered)
        Me.gbxSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbxSearch.Location = New System.Drawing.Point(12, 12)
        Me.gbxSearch.Name = "gbxSearch"
        Me.gbxSearch.Padding = New System.Windows.Forms.Padding(10)
        Me.gbxSearch.Size = New System.Drawing.Size(1000, 90)
        Me.gbxSearch.TabIndex = 34
        Me.gbxSearch.TabStop = False
        Me.gbxSearch.Text = "Search"
        '
        'lblTotal
        '
        Me.lblTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.ForeColor = System.Drawing.Color.Black
        Me.lblTotal.Location = New System.Drawing.Point(160, 0)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(136, 23)
        Me.lblTotal.TabIndex = 82
        Me.lblTotal.Text = "Total"
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
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
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.btnSelect)
        Me.Panel1.Controls.Add(Me.btnLoading)
        Me.Panel1.Controls.Add(Me.dgvSearch)
        Me.Panel1.Location = New System.Drawing.Point(12, 108)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1164, 425)
        Me.Panel1.TabIndex = 43
        '
        'btnSelect
        '
        Me.btnSelect.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSelect.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSelect.Location = New System.Drawing.Point(25, 372)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(136, 34)
        Me.btnSelect.TabIndex = 82
        Me.btnSelect.TabStop = False
        Me.btnSelect.Text = "Select"
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'btnLoading
        '
        Me.btnLoading.Image = Global.GeneralAcc.My.Resources.Resources.LoadingProgress
        Me.btnLoading.Location = New System.Drawing.Point(505, 97)
        Me.btnLoading.Name = "btnLoading"
        Me.btnLoading.Size = New System.Drawing.Size(136, 123)
        Me.btnLoading.TabIndex = 44
        Me.btnLoading.UseVisualStyleBackColor = True
        '
        'dgvSearch
        '
        Me.dgvSearch.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSearch.Location = New System.Drawing.Point(16, 14)
        Me.dgvSearch.Name = "dgvSearch"
        Me.dgvSearch.Size = New System.Drawing.Size(1130, 402)
        Me.dgvSearch.TabIndex = 41
        '
        'btnExit
        '
        Me.btnExit.Image = CType(resources.GetObject("btnExit.Image"), System.Drawing.Image)
        Me.btnExit.Location = New System.Drawing.Point(1108, 13)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(68, 66)
        Me.btnExit.TabIndex = 81
        Me.btnExit.Text = "Exit"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'EnquiryBase
        '
        Me.ClientSize = New System.Drawing.Size(1188, 545)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.gbxSearch)
        Me.Name = "EnquiryBase"
        Me.gbxSearch.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.dgvSearch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Protected WithEvents gbxSearch As GroupBox
    Protected WithEvents Panel1 As Panel
    Protected WithEvents btnLoading As Button
    Protected WithEvents dgvSearch As DataGridView
    Protected WithEvents lblFiltered As Label
    Protected WithEvents btnExit As Button
    Protected WithEvents lblTotal As Label
    Protected WithEvents btnSelect As Button
End Class
