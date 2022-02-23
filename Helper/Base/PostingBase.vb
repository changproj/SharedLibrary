Imports DataEntity
Imports DataEntityMgr
Imports GeneralAcc.Module
Imports Helper
Imports Helper.ApplicationTools
Imports System.ComponentModel
Imports System.Linq
Imports Validation
Imports DataBusiness

Public Class PostingBase

#Region "Enum"
    Protected Enum ColName

        TP = 0
        ACCNO = 1
        ACCDESC = 2
        RELATEDDOCNO = 3
        TAXCODE = 4
        PARTICULARS = 5
        DRCR = 6
        AMTF = 7
        AMTL = 8
        PAYMODE = 9

        'Visible = False
        ID = 10
        RELATEDDOCDATE = 11
        VOUCHERPREFIX = 12
        VOUCHERNO = 13
        VOUCHERSUFFIX = 14
        VOUCHERDATE = 15
        DOCNO = 16
        DOCDATE = 17
        TC = 18
        CURRENCY = 19
        EXCHRATE = 20
        SUBACCNO = 21
        CHEQUENO = 22
        BATCHID = 23
        FROMSYSTEM = 24
        FILENAME = 25
        REMARK = 26
        UNIQUENO = 27
        CREATEDBY = 28
        CREATEDON = 29
        EDITEDBY = 30
        EDITEDON = 31
        PERIOD = 32
        CO = 33
        BRANCH = 34
        ISTRANDIRTY = 35
        PS = 36
        MSIC = 37
        GROUP = 38
        ISAUTO = 39
        RESERVED1 = 40
        RESERVED2 = 41
        YEAR = 42
        DISPLAYTC = 43
        SUBACCDESC = 44
        SUPPCUSTNO = 45
        SUPPCUSTNAME = 46
        ISCANCELLED = 47
        PRINTCOUNT = 48
        BATCHIDCOUNT = 49
    End Enum
    Public Enum PaymentMode
        CASH = 0
        CHEQUE = 1
        EBANKING = 2
    End Enum
    Protected Enum ARAPColName
        ID = 0
        ACCNO = 1
        INVOICENO = 2 'RELATEDDOCNO
        FINDINVOICENO = 3
        TAXCODE = 4
        INVOICEDATE = 5
        AMOUNTL = 6
        BANK = 7
        PAYTYPE = 8
        CHEQUENO = 9
        FINDCHEQUENO = 10
        PARTICULARS = 11
        TP = 12
        DRCR = 13
    End Enum
    Protected Enum GLMCColName
        ID = 0
        ACCNO = 1
        REFNO = 2
        ACCDESC = 3
        FINDACCNO = 4
        AMOUNTL = 5
        BANK = 6
        PAYTYPE = 7
        CHEQUENO = 8
        FINDCHEQUENO = 9
        PARTICULARS = 10
        TP = 11
        DRCR = 12
    End Enum
    Protected Enum TranGLTableName
        TranGL
        TranGLCancel
        TranGLTemp
    End Enum
#End Region

#Region "Fields"
    Protected _formName As String = ""
    Protected _connstr As String = ""
    Protected _dataEntityMgr As Object
    Protected _tc As TC
    Protected _voucherSettingList As List(Of VoucherSetting)
    Private _skipValidateControl As Control() = Nothing
#End Region

#Region "Property"
    Public Sub New()
        InitializeComponent()
    End Sub
    Public Sub New(ByVal connstr As String, ByVal tc As String)
        Me.New
        _connstr = connstr
        _tc = GetTC(tc)
        _voucherSettingList = GetVoucherSettingList(tc)
    End Sub
    Protected Property FormName() As String
        Get
            Return String.Format("Posting of {0} ({1})", TC.Description, TC.ID)
        End Get
        Set(ByVal value As String)
            _formName = value
        End Set
    End Property
    Public ReadOnly Property GetTC(ByVal tcid As String) As TC
        Get
            If _tc Is Nothing Then
                Dim mTcMgr As New TCMgr(_connstr)
                _tc = mTcMgr.SetDataEntity(New TC(New KeyValueHelper(TC.Properties.InputAccMonth.ToString, cAccMonth.ID),
                                                  New KeyValueHelper(TC.Properties.ID.ToString, tcid))).
                             Fetch(New MgrArgs(New OptionList(enumIsLike.like))).DataEntityList.FirstOrDefault
            End If
            Return _tc
        End Get
    End Property
    Public ReadOnly Property TC() As TC
        Get
            Return _tc
        End Get
    End Property
    Public ReadOnly Property GetVoucherSettingList(ByVal tcid As String) As List(Of VoucherSetting)
        Get
            If _voucherSettingList Is Nothing Then
                Dim mVoucherSettingMgr As New VoucherSettingMgr(_connstr)
                mVoucherSettingMgr.SetDataEntity(New VoucherSetting(New KeyValueHelper(VoucherSetting.Properties.InputAccMonth.ToString, cAccMonth.ID),
                                                                    New KeyValueHelper(VoucherSetting.Properties.ID.ToString, tcid)))
                _voucherSettingList = mVoucherSettingMgr.FetchVoucherPrefix(New MgrArgs(New OptionList(enumIsLike.like))).DataEntityList

            End If
            Return _voucherSettingList
        End Get
    End Property
    Public ReadOnly Property VoucherSettingList() As List(Of VoucherSetting)
        Get
            Return _voucherSettingList
        End Get
    End Property
    Public ReadOnly Property CurrencyList() As List(Of KeyValueHelper)
        Get
            Dim mCurrency As New List(Of KeyValueHelper)
            mCurrency.Add(New KeyValueHelper("MYR", "MYR"))
            mCurrency.Add(New KeyValueHelper("USD", "USD"))
            Return mCurrency
        End Get
    End Property
    Protected Property SkipValidateControl() As Control()
        Get
            Return _skipValidateControl
        End Get
        Set(ByVal value As Control())
            _skipValidateControl = value
        End Set
    End Property
    'Protected Property KeyControl() As Control()
    '    Get
    '        Return _keyControl
    '    End Get
    '    Set(ByVal value As Control())
    '        _keyControl = value
    '    End Set
    'End Property
    'Public ReadOnly Property VoucherSetting(ByVal tcid As String) As 
    '    Get
    '        If _tc Is Nothing Then
    '            Dim mTcMgr As New TCMgr(_connstr)
    '            _tc = mTcMgr.SetDataEntity(New TC(New KeyValueHelper(TC.Properties.InputAccMonth.ToString, cAccMonth.ID),
    '                                              New KeyValueHelper(TC.Properties.ID.ToString, tcid))).
    '                         Fetch(New MgrArgs(New OptionList(enumIsLike.like))).DataEntityList(0)
    '        End If
    '        Return _tc
    '    End Get
    'End Property
#End Region

#Region "Overrides"
    Protected Overridable Shadows Sub InitControls()
        'GetSettings(cCompany.CompanyGroup, cCompanyMaster.ID, cCompanyMaster.SysID, Me.Name)

    End Sub
    Protected Overridable Sub EnableChildForm()
    End Sub
    Protected Overrides Sub OnFormFlagChanged(ByVal objState As EnumFormState)
        MyBase.OnFormFlagChanged(objState)
        EnableChildForm()
    End Sub
    Protected Overrides Sub Cancel()
        Me.Text = FormName + " - New"
        ButtonState(True, False, False)
    End Sub
    Protected Overrides Sub [New]()
        Me.Text = FormName + " - New"
        ButtonState(True, False, False)
    End Sub
    Protected Overrides Sub Edit()
        Me.Text = FormName + " - Edit"
        ButtonState(AddEditDeleteState:=True, SaveCancelState:=True, printpreview:=True)
    End Sub
    Protected Overrides Sub Save()
        Me.Text = FormName + " - Saving"
        ButtonState(False, False, False)
    End Sub
    Protected Overrides Sub DocCancelled()
        Me.Text = FormName + " - Cancelled"
        ButtonState(True, False, True)
        btnCancel.Enabled = True
    End Sub
    Protected Overrides Sub Confirmed()
        Me.Text = FormName + " - Confirmed"
        ButtonState(False, False, True)
        btnCancel.Enabled = True
    End Sub
    Protected Overrides Sub LastControlAction()
        If TypeOf (LastControl) Is TextBox OrElse
            TypeOf (LastControl) Is ComboBox OrElse
            TypeOf (LastControl) Is DateTimePicker OrElse
            TypeOf (LastControl) Is NumericUpDown OrElse
            TypeOf (LastControl) Is WaterMarkTextBox OrElse
            TypeOf (LastControl) Is CheckBox Then
            'tsbtnSave.PerformClick()
        End If
    End Sub
    'called after Enter keypress
    Protected Overrides Function ValidateControl(ByVal ctrl As Control, Optional ByVal isSuppressErrMsg As Boolean = False) As Boolean
        'LostFocus in PostingRT handled validating
        If SkipValidateControl?.Contains(ctrl) Then Return True

        Dim mListErr As List(Of KeyValueHelper) = CheckData(ctrl)
        If IsDataValid(mListErr) = False Then
            mListErr(0).Key.focus()
            HLField(mListErr(0).Key)
            Return False
        End If
        Return True
    End Function
    Protected Overrides Sub InitSearchDGV(ByVal dgv As DataGridView, Optional ByVal isEnquiry As Boolean = False)
    End Sub
#End Region

#Region "Overridable"
    Protected Overridable Sub Amount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        '---if textbox is empty and user pressed a decimal char---
        If CType(sender, TextBox).Text = String.Empty AndAlso
                       e.KeyChar = ChrW(Keys.Decimal) Then
            e.Handled = True
            Return
        End If
        If CType(sender, TextBox).Text.Contains(Chr(46)) AndAlso
                        e.KeyChar = Chr(46) Then
            CType(sender, TextBox).Text = "0."
            CType(sender, TextBox).SelectionStart = CType(sender, TextBox).Text.Length
            e.Handled = True
            Return
        End If
        '---if the key pressed is not a valid decimal number---
        If (Not (Char.IsDigit(e.KeyChar) OrElse
                    Char.IsControl(e.KeyChar) OrElse
                        (e.KeyChar = Chr(46)))) Then
            e.Handled = True
        End If
    End Sub
#End Region

#Region "Form State"
    Private Sub PostingBase_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AddHandlers(Me)
        InitControls()
        Me.KeyPreview = True
    End Sub
#End Region

#Region "ComboBox"
    Protected Overridable Sub InitComboBox(ByVal kvh As List(Of KeyValueHelper), Optional ByVal cbo As ComboBox = Nothing)
        'If cbo Is Nothing Then
        '    cbo = cboSearch
        'End If
        'ListComboBox(cbo, kvh)
    End Sub
    Protected Sub PostingBase_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        'Hide()
        'Close()
    End Sub
    Protected Overridable Sub ButtonState(ByVal AddEditDeleteState As Boolean, ByVal SaveCancelState As Boolean, ByVal printpreview As Boolean)
    End Sub
#End Region

#Region "Button"
    'Protected Overridable Sub tsbtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbtnSave.Click
    '    Dim mListErr As New List(Of KeyValueHelper)
    '    If MyBase.FormState <> EnumFormState.Delete Then
    '        mListErr = CheckData()
    '    End If
    '    If IsDataValid(mListErr) = False Then
    '        mListErr(0).Key.focus()
    '        HLField(mListErr(0).Key)
    '    Else
    '        If ConfirmBeforeSave() = DialogResult.Yes Then
    '            SaveRecord()
    '            OnFormFlagChanged(EnumFormState.Cancel)
    '        Else
    '            ActiveControl.Focus()
    '        End If
    '    End If
    'End Sub
#End Region

#Region "Save Function"
    Protected Overridable Sub SaveRecord()
    End Sub
    Protected Overridable Function ConfirmBeforeSave() As DialogResult
        Dim mMyResponse As DialogResult
        If MyBase.FormState = EnumFormState.Edit Or MyBase.FormState = EnumFormState.Add Then
            mMyResponse = MessageBox.Show("Confirm to Save?", "Continue", MessageBoxButtons.YesNo,
                                                                      MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

        ElseIf MyBase.FormState = EnumFormState.Delete Then
            mMyResponse = MessageBox.Show("Confirm to delete?", "Continue", MessageBoxButtons.YesNo,
                                                                                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

        End If
        Return mMyResponse '= Windows.Forms.DialogResult.Yes
    End Function
    Protected Overridable Function CheckData(Optional ByVal ctrl As Object = Nothing) As List(Of KeyValueHelper)
        Return Nothing
    End Function

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

#End Region

End Class
