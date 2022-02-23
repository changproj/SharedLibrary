Imports DataEntity
Imports DataEntityMgr
Imports GeneralAcc.Module
Imports Helper
Imports Helper.ApplicationTools

Public Class ReportBase


#Region "Fields"
    Protected _formName As String = ""
    Protected _connstr As String = ""
    Protected _dataEntityMgr As Object
#End Region


#Region "Property"
    Public Sub New()
        InitializeComponent()
    End Sub
    Public Sub New(ByVal connstr As String)
        Me.New
        _connstr = connstr
    End Sub

    'Sub SetSomething(Of C)(ByVal Data As C)
    '    Dim mMgr As DataEntityMgrBase(Of C) = _dataEntityMgr
    '    mMgr.SetDataEntity(Data)
    'End Sub

    Protected Property FormName() As String
        Get
            Me.Text = String.Format("{0} Report", _formName)
            Return Me.Text
        End Get
        Set(ByVal value As String)
            _formName = value
        End Set
    End Property
#End Region


#Region "Overrides"
    Protected Overrides Sub InitControls()
        txtAccMth.MaxLength = 6
        txtAccMth.Text = RptPeriod
        dtpRunDate.Value = RptRunDate
        cbxShowPrintDate.Checked = RptShowPrintDate
        'GetSettings(cCompany.CompanyGroup, cCompanyMaster.ID, cCompanyMaster.SysID, Me.Name)
    End Sub
    Protected Overrides Sub OnFormFlagChanged(ByVal objState As EnumFormState)
        MyBase.OnFormFlagChanged(objState)
    End Sub
    Protected Overrides Sub Cancel()
        Me.Text = FormName
        ClearGBItem(Me)
    End Sub
    Protected Overrides Sub LastControlAction()
        If TypeOf (LastControl) Is TextBox OrElse
            TypeOf (LastControl) Is ComboBox OrElse
            TypeOf (LastControl) Is DateTimePicker OrElse
            TypeOf (LastControl) Is NumericUpDown OrElse
            TypeOf (LastControl) Is WaterMarkTextBox OrElse
            TypeOf (LastControl) Is CheckBox Then
            btnPreview.PerformClick()
        End If
    End Sub
    'called after Enter keypress
    Protected Overrides Function ValidateControl(ByVal ctrl As Control, Optional ByVal isSuppressErrMsg As Boolean = False) As Boolean
        Dim mListErr As List(Of KeyValueHelper) = CheckData(ctrl)
        If mListErr Is Nothing Then Return True
        If IsDataValid(mListErr) = False Then
            mListErr(0).Key.focus()
            HLField(mListErr(0).Key)
            Return False
        End If
        Return True
    End Function
    Protected Overridable Function CheckData(Optional ByVal ctrl As Object = Nothing) As List(Of KeyValueHelper)
        Return Nothing
    End Function
#End Region


#Region "Form State"
    Private Sub ReportBase_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AddHandlers(Me)
        InitControls()
        StartPosition = FormStartPosition.CenterScreen
        Me.Text = FormName
    End Sub
#End Region


#Region "ComboBox"
    Protected Overridable Sub InitComboBox(ByVal kvh As List(Of KeyValueHelper), Optional ByVal cbo As ComboBox = Nothing)
        ListComboBox(cbo, kvh)
    End Sub
#End Region

#Region "Button"
    Protected Overridable Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
    End Sub
    Protected Overridable Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub txtAccMth_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAccMth.KeyPress
        e.KeyChar = CheckIntOnly(e.KeyChar, txtAccMth.Text)
    End Sub
#End Region
End Class
