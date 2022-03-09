Imports System.Windows.Forms
Imports Helper.ApplicationTools
Imports UserLogin

Public Class MDIMain


    Private WithEvents mUserLogin As UserLogin.FrmLogin

    Private Sub MDIMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Me.AfterHousekeeping()


        mUserLogin = New UserLogin.FrmLogin
        mUserLogin.Image = New Bitmap(My.Resources.Login_Blue)
        mUserLogin.ConnectionStringCoMaster = "Data Source=it-changfl;Initial Catalog=AccCo2_New;User Id=GA;Password=GACT2019;Connect Timeout=0"
        mUserLogin.SystemID = EnumSystem.ACC
        mUserLogin.IncludeCO = "98,PP,"
        mUserLogin.ShowMonth = True

        AddHandler mUserLogin.CloseForms, AddressOf CloseForms
        AddHandler mUserLogin.ClearGlobalVar, AddressOf ClearGlobalVar
        AddHandler mUserLogin.GetMonths, AddressOf GetAccMonth
        'AddHandler mUserLogin.ValidateUser, AddressOf ValidateUser


        If mUserLogin.ShowDialog = DialogResult.OK Then
            Me.Enabled = True
        Else
            End
        End If

    End Sub

    Public Sub CloseForms()
        For Each mdichild As Form In Me.MdiChildren
            mdichild.Close()
        Next
    End Sub

    Public Sub ClearGlobalVar()
        'cUser = Nothing
        'cTempUser = Nothing
        'cCompanyMaster = Nothing
        'cAccMonth = Nothing
        'cCompany = Nothing
        'cAccSetting = Nothing

        'CONNSTR = String.Empty
        'APPRPPATH = String.Empty
        'APPPATH = String.Empty

        'TEXTFILEPOOL = String.Empty
        'TEXTFILEPOOL_REGNO = String.Empty
        'IMPORT_FOLDER = "Import"
        'EXPORT_FOLDER = "Export"
        'LEDGER_FOLDER = "Ledger"
        'EXPORTACCOUNTBAK_FOLDER = "ExportAccountBak"
        'DBBAK_FOLDER = "DbBak"
        'FINAL_FOLDER = "Final"

        'TEXTFILEPOOL_REGNO_IMPORT = String.Empty
        'TEXTFILEPOOL_REGNO_EXPORT = String.Empty
        'TEXTFILEPOOL_REGNO_LEDGER = String.Empty
        'TEXTFILEPOOL_REGNO_EXPORTACCOUNTBAK = String.Empty
        'TEXTFILEPOOL_REGNO_DBBAK = String.Empty
        'TEXTFILEPOOL_REGNO_FINAL = String.Empty
        'TEXTFILEPOOL_SERVERPATH = String.Empty
    End Sub
    Private Sub GetAccMonth()

        mUserLogin.delGetMonths(Function() New List(Of Object) From {"01", "02"})


        'Dim optionList As New OptionList(enumIsLike.equal)
        'Dim orderByList As OrderByList = New OrderByList()
        'orderByList.Add(New KeyValueHelper(AccMonth.DbFields.Am_ID.ToString, enumSortDirection.Desc))
        'cboAccMonth.DataSource = New AccMonthMgr(CONNSTR).SetDataEntity(New AccMonth With {.Closed = False}).Fetch(New MgrArgs(optionList, orderByList)).DataEntityList
        'cboAccMonth.DisplayMember = AccMonth.Properties.ID.ToString
        'cboAccMonth.ValueMember = AccMonth.Properties.ID.ToString
        'If cboAccMonth.Items.Count > 0 Then cboAccMonth.SelectedIndex = -1 : cboAccMonth.SelectedIndex = 0
    End Sub
    Private Sub ValidateUser()
        mUserLogin.delValidateUser(AddressOf ValidatingUser)
    End Sub

    Private Function ValidatingUser(ByVal userID As String, ByVal userPassword As String) As Boolean
        If userID = "ADMIN" AndAlso userPassword = "123" Then
            Return True
        Else
            Return False
        End If


        '                Dim mUser As User
        '                mUser = New User(New KeyValueHelper(User.Properties.ID.ToString, Me.txtUser.Text.Trim),
        '                                 New KeyValueHelper(User.Properties.Password.ToString, Me.txtPass.Text.Trim))
        '                Dim userMgr As UserMgr = New UserMgr(CONNSTR).SetDataEntity(mUser)
        '                userMgr.Fetch(New MgrArgs(New OptionList(enumIsLike.equal)))
        '                If Not userMgr.DataEntityList.Any Then
        '                    ApplicationTools.ErrorMsg("Invalid User ID or Password.")
        '                    Exit Sub
        '                End If

        '                If userMgr.DataEntityList.Count > 0 Then
        '                    mUser = userMgr.DataEntityList.FirstOrDefault
        '                    If (mUser.Password <> txtPass.Text.Trim) Then
        '                        ApplicationTools.ErrorMsg("Invalid Password.")
        '                        ApplicationTools.HLField(txtPass)
        '                    Else
        '                        cboCompany.SelectedIndex = GetCoIndex()
        '                        If cboCompany.SelectedIndex < 0 Then Exit Sub

        '                        cboAccMonth.SelectedIndex = GetAccMonthIndex()
        '                        If cboAccMonth.SelectedIndex < 0 Then Exit Sub

        '                        cUser = mUser        'set Application Variable
        '                        cTempUser = mUser    'set Application Variable
        '                        cCompanyMaster = cboCompany.SelectedItem  'set Application Variable


        '                        'company
        '                        Dim coMgr As New CompanyMgr(CONNSTR)
        '                        Dim coList As List(Of Company) = coMgr.SetDataEntity(New Company(New KeyValueHelper(Company.Properties.ID.ToString, cCompanyMaster.ID))).
        '                                            Fetch(New MgrArgs(New OptionList(enumIsLike.equal))).DataEntityList
        '                        If (coList IsNot Nothing) And (coList.Count > 0) Then
        '                            cCompany = coList(0)        'set Application Variable
        '                        Else
        '                            ApplicationTools.ErrorMsg("Company is not set correctly.")
        '                            Exit Sub
        '                        End If
        '                        'coMgr = Nothing
        '                        coList = Nothing


        '                        'account setting
        '                        Dim mAccountSettingMgr As New AccSettingMgr(CONNSTR)
        '                        mAccountSettingMgr.SetDataEntity(New KeyValueHelper(AccSetting.Properties.ID.ToString, cCompany.CompanyGroup)).
        '                                           Fetch(New MgrArgs(New OptionList(enumIsLike.equal)))
        '                        If mAccountSettingMgr.DataEntityList.Any Then
        '                            cAccSetting = mAccountSettingMgr.DataEntityList.FirstOrDefault     'set Application Variable
        '                        Else
        '                            ApplicationTools.ErrorMsg("Account Setting is not set correctly.")
        '                            Exit Sub
        '                        End If
        '                        mAccountSettingMgr = Nothing


        '                        'Account Month
        '                        cAccMonth = cboAccMonth.SelectedItem        'set Application Variable

        '                        'Report Period
        '                        RptPeriod = cAccMonth.ID                    'set Application Variable

        '                        'AppPath & AppRpPath
        '                        APPPATH = GetAppPath()                      'set Application Variable
        '                        APPRPPATH = APPPATH + IIf(APPPATH.Substring(APPPATH.Length - 1) = "\", "Rp\", "\Rp\")            'set Application Variable
        '                        'If cCompany.IsPlantation Then APPRPPATH += "Plantation\"
        '                        APPNAME = ApplicationVariables.APPLICATIONNAME


        '                        'search correct company folder in filepool
        '                        If (IsExistTextFilePool()) = False Then
        '                            ApplicationTools.ErrorMsg(String.Format("Ops, It seems there is a folder path problem with {0}, please contact System Administrator.", ApplicationVariables.NETWORKTEXTFILEPOOL))
        '                            Exit Sub
        '                        Else
        '                            Try
        '                                SetTextFilePool()  'set Application Variable
        '                            Catch ex As Exception
        '                                ApplicationTools.ErrorMsg(String.Format("{0}{1}{2}", ex.Message, Chr(13), "Ops, It seems there is network permission problem, please contact System Administrator."))
        '                                Exit Sub
        '                            End Try
        '                        End If


        '                        'Auto Housekeeping from Last Year for non ACC & Co_IsBF = True
        '                        If cCompanyMaster.SysID <> "ACC" AndAlso cCompany.IsBF Then
        '                            btnLoading.Visible = True
        '                            mElapseStartTime = DateTime.Now
        '                            mTimer.Start()

        '                            Dim mAccMonthMgr As New AccMonthMgr(CONNSTR)
        '                            Dim mHasDiff As Boolean = Await mAccMonthMgr.CompareClosingOpening(cAccMonth.ID)
        '                            If mHasDiff Then
        '                                btnLoading.Text = "Housekeeping from Previous Financial Year ..."
        '                                Dim mErrorMsg As String = Await mAccMonthMgr.Housekeeping(cAccMonth.LFYEndPeriod, cCompanyMaster.SysID)   'auto housekeeping on LFYEndPeriod
        '                                If Not String.IsNullOrEmpty(mErrorMsg) Then
        '                                    ErrorMsg(mErrorMsg)
        '                                    Return
        '                                End If
        '                                mTimer.Stop()
        '                                btnLoading.Text = String.Empty
        '                                btnLoading.Visible = False

        '                                InfoMsg("Auto Housekeeping is completed.")
        '                            End If
        '                        End If


        '                        'Get Settings
        '                        GetSettings(cCompany.CompanyGroup, cCompanyMaster.ID, cCompanyMaster.SysID, Me.Name)


        '                        'Export ACC transaction to txt as backup
        '                        Dim mNeedCurrentMonthTransactionBackup As Boolean? = CheckSetting(Of Boolean)(EnumSettings.NeedCurrentMonthTransactionBackup.ToString)
        '                        If mNeedCurrentMonthTransactionBackup.HasValue = False OrElse mNeedCurrentMonthTransactionBackup.Value = True Then
        '                            Dim mProToTextFile As New ProToTextFile(CONNSTR)
        '                            Await mProToTextFile.ExportDataToTextFile(TEXTFILEPOOL_REGNO_CURRENTMONTHTRANSACTIONBAK,
        '                                            String.Format("{0}.{1}", TextBackupFilename, "txt"),
        '                                            FileType.Text)
        '                        End If


        '                        Me.DialogResult = DialogResult.OK
        '                        'MDIMain.Show()

        '                    End If
    End Function
End Class
