Imports System.Windows.Forms

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmLogin
    Inherits Helper.WinFormBase

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmLogin))
        Me.btnLoading = New System.Windows.Forms.Button()
        Me.cboMonth = New System.Windows.Forms.ComboBox()
        Me.lblMonth = New System.Windows.Forms.Label()
        Me.lblCaseSensitive = New System.Windows.Forms.Label()
        Me.lblCoName = New System.Windows.Forms.Label()
        Me.cboCompany = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnLogin = New System.Windows.Forms.Button()
        Me.txtPass = New System.Windows.Forms.TextBox()
        Me.label2 = New System.Windows.Forms.Label()
        Me.txtUser = New System.Windows.Forms.TextBox()
        Me.label1 = New System.Windows.Forms.Label()
        Me.btnFindCo = New System.Windows.Forms.Button()
        Me.pictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnLoading
        '
        Me.btnLoading.Image = CType(resources.GetObject("btnLoading.Image"), System.Drawing.Image)
        Me.btnLoading.Location = New System.Drawing.Point(183, 92)
        Me.btnLoading.Name = "btnLoading"
        Me.btnLoading.Size = New System.Drawing.Size(136, 123)
        Me.btnLoading.TabIndex = 121
        Me.btnLoading.UseVisualStyleBackColor = True
        '
        'cboMonth
        '
        Me.cboMonth.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboMonth.FormattingEnabled = True
        Me.cboMonth.Location = New System.Drawing.Point(101, 130)
        Me.cboMonth.Name = "cboMonth"
        Me.cboMonth.Size = New System.Drawing.Size(84, 23)
        Me.cboMonth.TabIndex = 109
        '
        'lblMonth
        '
        Me.lblMonth.AutoSize = True
        Me.lblMonth.Location = New System.Drawing.Point(12, 135)
        Me.lblMonth.Name = "lblMonth"
        Me.lblMonth.Size = New System.Drawing.Size(43, 13)
        Me.lblMonth.TabIndex = 120
        Me.lblMonth.Text = "Month :"
        '
        'lblCaseSensitive
        '
        Me.lblCaseSensitive.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCaseSensitive.ForeColor = System.Drawing.Color.Red
        Me.lblCaseSensitive.Location = New System.Drawing.Point(100, 214)
        Me.lblCaseSensitive.Name = "lblCaseSensitive"
        Me.lblCaseSensitive.Size = New System.Drawing.Size(84, 13)
        Me.lblCaseSensitive.TabIndex = 118
        Me.lblCaseSensitive.Text = "(case sensitive)"
        '
        'lblCoName
        '
        Me.lblCoName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCoName.ForeColor = System.Drawing.Color.Blue
        Me.lblCoName.Location = New System.Drawing.Point(188, 104)
        Me.lblCoName.Name = "lblCoName"
        Me.lblCoName.Size = New System.Drawing.Size(324, 21)
        Me.lblCoName.TabIndex = 117
        Me.lblCoName.Text = "Company Name"
        '
        'cboCompany
        '
        Me.cboCompany.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCompany.FormattingEnabled = True
        Me.cboCompany.Location = New System.Drawing.Point(101, 98)
        Me.cboCompany.Name = "cboCompany"
        Me.cboCompany.Size = New System.Drawing.Size(51, 23)
        Me.cboCompany.TabIndex = 108
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 103)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(57, 13)
        Me.Label3.TabIndex = 116
        Me.Label3.Text = "Company :"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(96, 254)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 25)
        Me.btnCancel.TabIndex = 113
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnLogin
        '
        Me.btnLogin.Location = New System.Drawing.Point(15, 254)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Size = New System.Drawing.Size(75, 25)
        Me.btnLogin.TabIndex = 112
        Me.btnLogin.Text = "Login"
        Me.btnLogin.UseVisualStyleBackColor = True
        '
        'txtPass
        '
        Me.txtPass.Location = New System.Drawing.Point(101, 192)
        Me.txtPass.Name = "txtPass"
        Me.txtPass.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPass.Size = New System.Drawing.Size(101, 20)
        Me.txtPass.TabIndex = 111
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(12, 196)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(59, 13)
        Me.label2.TabIndex = 115
        Me.label2.Text = "Password :"
        '
        'txtUser
        '
        Me.txtUser.Location = New System.Drawing.Point(101, 163)
        Me.txtUser.Name = "txtUser"
        Me.txtUser.Size = New System.Drawing.Size(101, 20)
        Me.txtUser.TabIndex = 110
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(12, 167)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(49, 13)
        Me.label1.TabIndex = 114
        Me.label1.Text = "User ID :"
        '
        'btnFindCo
        '
        Me.btnFindCo.Image = CType(resources.GetObject("btnFindCo.Image"), System.Drawing.Image)
        Me.btnFindCo.Location = New System.Drawing.Point(155, 99)
        Me.btnFindCo.Name = "btnFindCo"
        Me.btnFindCo.Size = New System.Drawing.Size(28, 20)
        Me.btnFindCo.TabIndex = 119
        Me.btnFindCo.TabStop = False
        Me.btnFindCo.UseVisualStyleBackColor = True
        '
        'pictureBox1
        '
        Me.pictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.pictureBox1.Name = "pictureBox1"
        Me.pictureBox1.Size = New System.Drawing.Size(504, 82)
        Me.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pictureBox1.TabIndex = 16
        Me.pictureBox1.TabStop = False
        '
        'FrmLogin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(504, 306)
        Me.Controls.Add(Me.btnLoading)
        Me.Controls.Add(Me.cboMonth)
        Me.Controls.Add(Me.lblMonth)
        Me.Controls.Add(Me.btnFindCo)
        Me.Controls.Add(Me.lblCaseSensitive)
        Me.Controls.Add(Me.lblCoName)
        Me.Controls.Add(Me.cboCompany)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnLogin)
        Me.Controls.Add(Me.txtPass)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.txtUser)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.pictureBox1)
        Me.Name = "FrmLogin"
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private WithEvents pictureBox1 As PictureBox
    Protected WithEvents btnLoading As Button
    Friend WithEvents cboMonth As ComboBox
    Private WithEvents lblMonth As Label
    Friend WithEvents btnFindCo As Button
    Private WithEvents lblCaseSensitive As Label
    Private WithEvents lblCoName As Label
    Friend WithEvents cboCompany As ComboBox
    Private WithEvents Label3 As Label
    Private WithEvents btnCancel As Button
    Private WithEvents btnLogin As Button
    Private WithEvents txtPass As TextBox
    Private WithEvents label2 As Label
    Private WithEvents txtUser As TextBox
    Private WithEvents label1 As Label
End Class
