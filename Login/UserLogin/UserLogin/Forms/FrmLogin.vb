Imports System.Drawing
Imports System.Windows.Forms
Imports Aga.Controls.Tree
Imports CompanyMasterCore.DataEntity
Imports CompanyMasterCore.DataEntityMgr
Imports Helper
Imports Helper.ApplicationTools

Public Class FrmLogin

#Region "Fields"
    Private _image As Image
    Private _connectionStringCoMaster As String
    Private _connectionStringCo As String
    Private _systemID As EnumSystem
    Private _includeCO As String
    Private _showMonth As Boolean
    Private _selectedCompanyMaster As CompanyMaster
    Private _selectedMonth As Object
    Private _handlerGetMonths As funcGetMonths
    Private _handlerValidateUser As funcValidateUser
#End Region

#Region "Delegate"
    Public Delegate Function funcGetMonths() As List(Of Object)
    Public Delegate Function funcValidateUser(ByVal userID As String, ByVal userPassword As String) As Boolean
#End Region

#Region "Event"
    Event CloseForms()
    Event ClearGlobalVar()
#End Region


#Region "Property"
    Public Sub New()
        InitializeComponent()
    End Sub
    Public Property Image() As Image
        Get
            Return _image
        End Get
        Set(ByVal value As Image)
            _image = value
        End Set
    End Property
    Public Property ConnectionStringCoMaster() As String
        Get
            Return _connectionStringCoMaster
        End Get
        Set(ByVal value As String)
            _connectionStringCoMaster = value
        End Set
    End Property
    Public ReadOnly Property ConnectionStringCo() As String
        Get
            Return _connectionStringCo
        End Get
    End Property
    Public Property SystemID() As EnumSystem
        Get
            Return _systemID
        End Get
        Set(ByVal value As EnumSystem)
            _systemID = value
        End Set
    End Property
    Public Property IncludeCO() As String
        Get
            Return _includeCO
        End Get
        Set(ByVal value As String)
            _includeCO = value
        End Set
    End Property
    Public Property ShowMonth() As Boolean
        Get
            Return _showMonth
        End Get
        Set(ByVal value As Boolean)
            _showMonth = value
        End Set
    End Property
    Public ReadOnly Property SelectedCompanyMaster() As CompanyMaster
        Get
            Return _selectedCompanyMaster
        End Get
    End Property
    Public ReadOnly Property SelectedMonth() As Object
        Get
            Return _selectedMonth
        End Get
    End Property
    Public Property HandlerGetMonths() As funcGetMonths
        Get
            Return _handlerGetMonths
        End Get
        Set(ByVal value As funcGetMonths)
            _handlerGetMonths = value
        End Set
    End Property
    Public Property HandlerValidateUser() As funcValidateUser
        Get
            Return _handlerValidateUser
        End Get
        Set(ByVal value As funcValidateUser)
            _handlerValidateUser = value
        End Set
    End Property
#End Region

#Region "Form"
    Private Sub FrmLogin_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.A AndAlso e.Control Then
            txtUser.Text = "admin"
            txtPass.Text = "123"
            Me.AcceptButton = btnLogin
        End If
    End Sub
    Private Sub FrmLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Change image
        'Me.pictureBox1.Image = IIf(ApplicationVariables.SYSTEMID = EnumSystem.ACC.ToString, My.Resources.Login_Blue, My.Resources.Login_Green)

        If Me.Image IsNot Nothing Then
            Me.pictureBox1.Image = Me.Image
        Else
            Me.pictureBox1.Image = My.Resources.Login_Green
        End If

        RaiseEvent CloseForms()         'close forms in mdi
        RaiseEvent ClearGlobalVar()     'clear global variables

        'form
        AddHandlers(Me)
        Me.CenterToScreen()
        Me.FormBorderStyle = FormBorderStyle.None

        'control
        cboCompany.MaxLength = 2
        EnableControl(False)
        NotUpperCaseControl = New Control() {txtPass}
        txtUser.CharacterCasing = CharacterCasing.Upper
        lblCaseSensitive.Visible = False
        txtUser.Text = String.Empty
        txtPass.Text = String.Empty
        btnLoading.Visible = False

        cboMonth.Visible = Me.ShowMonth
        lblMonth.Visible = Me.ShowMonth

        'get company
        GetCompanyMasterList()

        Me.KeyPreview = True

    End Sub
#End Region

#Region "Function"
    Private Sub GetCompanyMasterList()
        Try

            Dim mCompanyMasterMgr As New CompanyMasterMgr(Me.ConnectionStringCoMaster)
            mCompanyMasterMgr.SetDataEntity(New CompanyMaster(New KeyValueHelper(CompanyMaster.Properties.Active.ToString, True),
                                                          New KeyValueHelper(CompanyMaster.Properties.SysID.ToString, Me.SystemID.ToString)))
            If Not String.IsNullOrEmpty(Me.IncludeCO) Then mCompanyMasterMgr.IncludeCompanyList.AddRange(Me.IncludeCO.Split(","))
            cboCompany.DisplayMember = CompanyMaster.Properties.ID.ToString
            cboCompany.ValueMember = CompanyMaster.Properties.Name.ToString
            cboCompany.DataSource = mCompanyMasterMgr.Fetch(New MgrArgs(New OptionList(enumIsLike.like))).DataEntityList
            'If cboCompany.Items.Count > 0 Then cboCompany.SelectedIndex = -1 : cboCompany.SelectedIndex = 0
            Me.cboCompany.Focus()
        Catch ex As Exception
            ApplicationTools.ErrorMsg(ex.Message)
        End Try
    End Sub
    Private Function checkInput() As Boolean
        If String.IsNullOrEmpty(Me.txtUser.Text.Trim) Then
            ApplicationTools.InfoMsg("Please enter a valid User ID.")
            ApplicationTools.HLField(txtUser)
            Return False
        End If
        If String.IsNullOrEmpty(Me.txtPass.Text) Then
            ApplicationTools.InfoMsg("Please enter a valid Password.")
            ApplicationTools.HLField(txtPass)
            Return False
        End If
        Return True
    End Function
    Private Sub EnableControl(ByVal enable As Boolean)
        txtUser.Enabled = enable
        txtPass.Enabled = enable
        btnLogin.Enabled = enable
    End Sub
    Private Function GetCoIndex() As Integer
        Return cboCompany.FindStringExact(cboCompany.Text.Trim)
    End Function
    Private Function GetAccMonthIndex() As Integer
        Return cboMonth.FindStringExact(cboMonth.Text.Trim)
    End Function
    '    Private Sub mTimer_Tick(sender As Object, e As EventArgs) Handles mTimer.Tick
    '        btnLoading.Text = String.Format("Checking changes from Previous Financial Year ...{0}({1} Sec)", vbNewLine, DateTime.Now.Subtract(mElapseStartTime).TotalSeconds.ToString("0"))
    '        Application.DoEvents()
    '    End Sub
#End Region

#Region "Textbox & Button & ComboBox"
    Private Sub txtPass_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPass.GotFocus
        lblCaseSensitive.Visible = True
    End Sub
    Private Sub txtPass_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPass.LostFocus
        lblCaseSensitive.Visible = False
    End Sub
    Private Sub btnLogin_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLogin.Click
        Try
            If checkInput() Then
                If (Me.HandlerValidateUser.Invoke(txtUser.Text.Trim, txtPass.Text.Trim)) Then
                    cboCompany.SelectedIndex = GetCoIndex()
                    If cboCompany.SelectedIndex < 0 Then Throw New Exception("Please select a valid Company.")
                    Me._selectedCompanyMaster = cboCompany.SelectedItem  'will be used for setting Application Variable in calling form

                    If Me.ShowMonth Then
                        cboMonth.SelectedIndex = GetAccMonthIndex()
                        If cboMonth.SelectedIndex < 0 Then Throw New Exception("Please select a valid Month.")
                        Me._selectedMonth = cboMonth.SelectedItem  'will be used for setting Application Variable in calling form
                    End If

                    Me.DialogResult = DialogResult.OK
                Else
                    ApplicationTools.ErrorMsg("Invalid User.")
                    ApplicationTools.HLField(txtUser)
                End If
            End If
        Catch ex As Exception
            ApplicationTools.ErrorMsg(ex.Message)
        End Try
    End Sub
    Private Sub cboCompany_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCompany.TextChanged
        Me.cboCompany.SelectedIndex = GetCoIndex()  'get the index when Co ID is found
        If Me.cboCompany.SelectedIndex = -1 Then
            cboMonth.Enabled = False
            EnableControl(False)
        End If
    End Sub
    Private Sub cboCompany_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboCompany.KeyPress
        e.KeyChar = ApplicationTools.ChangeCase(e.KeyChar)
    End Sub
    Private Sub cboCompany_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCompany.SelectedIndexChanged
        If Not cboCompany.SelectedItem Is Nothing Then
            lblCoName.Text = cboCompany.SelectedValue.ToString
            Dim companyMaster As CompanyMaster = CType(cboCompany.SelectedItem, CompanyMaster)
            Dim serverName As String = companyMaster.ServerName
            Dim connectionString As String = companyMaster.ConnectionString
            If (String.IsNullOrEmpty(serverName) = False) AndAlso (String.IsNullOrEmpty(connectionString) = False) Then
                _connectionStringCo = String.Format(connectionString, serverName, companyMaster.DbName)   'set Application Variable
                If Me.ShowMonth Then
                    cboMonth.DataSource = Me.HandlerGetMonths.Invoke
                    cboMonth.Enabled = True
                Else
                    EnableControl(True)
                    ApplicationTools.HLField(txtUser)
                End If
            Else
                EnableControl(False)
            End If
        End If
    End Sub
    Private Sub cboAccMonth_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboMonth.TextChanged
        Me.cboMonth.SelectedIndex = GetAccMonthIndex() 'get the index when AccMonth is found
        If Me.cboMonth.SelectedIndex = -1 Then
            EnableControl(False)
        End If
    End Sub
    Private Sub cboAccMonth_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboMonth.SelectedIndexChanged
        EnableControl(True)
        ApplicationTools.HLField(txtUser)
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub
    Private Sub btnFindCo_Click(sender As Object, e As EventArgs) Handles btnFindCo.Click
        Try

            'Initial CompanyMaster List to be passed into EnGeneral
            Dim mCompanyMasterMgr As New CompanyMasterMgr(Me.ConnectionStringCoMaster)
            Dim mOrderByList As New OrderByList
            mOrderByList.Add(New KeyValueHelper(CompanyMaster.DbFields.Co_ID.ToString, enumSortDirection.Asc))
            If Not String.IsNullOrEmpty(Me.IncludeCO) Then mCompanyMasterMgr.IncludeCompanyList.AddRange(Me.IncludeCO.Split(","))
            mCompanyMasterMgr.SetDataEntity(New CompanyMaster(New KeyValueHelper(CompanyMaster.Properties.Active.ToString, True),
                                            New KeyValueHelper(CompanyMaster.Properties.SysID.ToString, Me.SystemID.ToString))).
                              Fetch(New MgrArgs(New OptionList(enumIsLike.like), mOrderByList))


            Dim mEnquiryForm As New EnGeneral()
            mEnquiryForm.SetFormName = "Companies"
            'mEnquiryForm.DynamicType = GetType(CompanyMaster.Properties)                        'Ctrl + D
            mEnquiryForm.IncludeFieldList.Add(CompanyMaster.Properties.ID.ToString)             'Init dgv
            mEnquiryForm.IncludeFieldList.Add(CompanyMaster.Properties.Name.ToString)           'Init dgv
            mEnquiryForm.PropertyDescriptionList = GetPropertyDescriptions(Of CompanyMaster)()  'Init dgv
            mEnquiryForm.DataEntityList.AddRange(mCompanyMasterMgr.DataEntityList)              'Data to load dgv
            mEnquiryForm.ShowDialog()
            If mEnquiryForm.DialogResult = DialogResult.OK Then
                If mEnquiryForm.SelectedDataEntity.Any Then
                    cboCompany.Text = mEnquiryForm.SelectedDataEntity.FirstOrDefault.ID
                    lblCoName.Text = mEnquiryForm.SelectedDataEntity.FirstOrDefault.Name
                    txtUser.Focus()
                End If
            End If
        Catch ex As Exception
            ApplicationTools.ErrorMsg(ex.Message)
        End Try
    End Sub

    Private Sub FrmLogin_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        cboCompany.Focus()
    End Sub
#End Region




End Class