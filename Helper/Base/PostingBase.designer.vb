<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PostingBase
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PostingBase))
        Me.pnButton = New System.Windows.Forms.Panel()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnDeleteDoc = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.btnCancelDoc = New System.Windows.Forms.Button()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.pnButton.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnButton
        '
        Me.pnButton.Controls.Add(Me.btnSave)
        Me.pnButton.Controls.Add(Me.btnCancel)
        Me.pnButton.Controls.Add(Me.btnExit)
        Me.pnButton.Controls.Add(Me.btnDeleteDoc)
        Me.pnButton.Controls.Add(Me.btnNew)
        Me.pnButton.Controls.Add(Me.btnPreview)
        Me.pnButton.Controls.Add(Me.btnCancelDoc)
        Me.pnButton.Controls.Add(Me.btnPrint)
        Me.pnButton.Location = New System.Drawing.Point(44, 125)
        Me.pnButton.Name = "pnButton"
        Me.pnButton.Size = New System.Drawing.Size(610, 73)
        Me.pnButton.TabIndex = 0
        '
        'btnSave
        '
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.Location = New System.Drawing.Point(86, 3)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 66)
        Me.btnSave.TabIndex = 75
        Me.btnSave.Text = "Save"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.Location = New System.Drawing.Point(160, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(68, 66)
        Me.btnCancel.TabIndex = 76
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Image = CType(resources.GetObject("btnExit.Image"), System.Drawing.Image)
        Me.btnExit.Location = New System.Drawing.Point(234, 3)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(68, 66)
        Me.btnExit.TabIndex = 80
        Me.btnExit.Text = "Exit"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnDeleteDoc
        '
        Me.btnDeleteDoc.Image = CType(resources.GetObject("btnDeleteDoc.Image"), System.Drawing.Image)
        Me.btnDeleteDoc.Location = New System.Drawing.Point(459, 3)
        Me.btnDeleteDoc.Name = "btnDeleteDoc"
        Me.btnDeleteDoc.Size = New System.Drawing.Size(69, 66)
        Me.btnDeleteDoc.TabIndex = 77
        Me.btnDeleteDoc.Text = "Delete Doc"
        Me.btnDeleteDoc.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnDeleteDoc.UseVisualStyleBackColor = True
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.Location = New System.Drawing.Point(12, 3)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(68, 66)
        Me.btnNew.TabIndex = 79
        Me.btnNew.Text = "New"
        Me.btnNew.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnNew.UseVisualStyleBackColor = True
        '
        'btnPreview
        '
        Me.btnPreview.Image = CType(resources.GetObject("btnPreview.Image"), System.Drawing.Image)
        Me.btnPreview.Location = New System.Drawing.Point(309, 3)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(69, 66)
        Me.btnPreview.TabIndex = 78
        Me.btnPreview.Text = "Preview"
        Me.btnPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnPreview.UseVisualStyleBackColor = True
        '
        'btnCancelDoc
        '
        Me.btnCancelDoc.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancelDoc.ForeColor = System.Drawing.Color.Blue
        Me.btnCancelDoc.Image = CType(resources.GetObject("btnCancelDoc.Image"), System.Drawing.Image)
        Me.btnCancelDoc.Location = New System.Drawing.Point(534, 0)
        Me.btnCancelDoc.Name = "btnCancelDoc"
        Me.btnCancelDoc.Size = New System.Drawing.Size(69, 66)
        Me.btnCancelDoc.TabIndex = 81
        Me.btnCancelDoc.Text = "Cancel Doc"
        Me.btnCancelDoc.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCancelDoc.UseVisualStyleBackColor = True
        '
        'btnPrint
        '
        Me.btnPrint.Image = Global.GeneralAcc.My.Resources.Resources.Print2
        Me.btnPrint.Location = New System.Drawing.Point(384, 3)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(69, 66)
        Me.btnPrint.TabIndex = 82
        Me.btnPrint.Text = "Print"
        Me.btnPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'PostingBase
        '
        Me.ClientSize = New System.Drawing.Size(822, 600)
        Me.Controls.Add(Me.pnButton)
        Me.Name = "PostingBase"
        Me.pnButton.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Protected WithEvents pnButton As Panel
    Protected WithEvents btnSave As Button
    Protected WithEvents btnCancel As Button
    Protected WithEvents btnExit As Button
    Protected WithEvents btnDeleteDoc As Button
    Protected WithEvents btnNew As Button
    Protected WithEvents btnPreview As Button
    Protected WithEvents btnCancelDoc As Button
    Protected WithEvents btnPrint As Button
End Class
