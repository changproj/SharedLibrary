Imports System.IO
Imports DataEntity
Imports DataEntityMgr
Imports GeneralAcc.Module
Imports Helper
Imports Helper.ApplicationTools
Imports RpViewer

Public Class ImportExportBase
#Region "Fields"
    Protected _formName As String = String.Empty
    Protected _connstr As String = String.Empty
    Protected _exportFileName As String = String.Empty
    Protected _exportFileExtension As String = String.Empty
    Protected _exportDestination As String = String.Empty
    Protected _docRangeSummary As String = String.Empty
    Protected _totalTransaction As Int16 = 0
    Protected _textFileMgr As TextFileMgr
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
            Me.Text = String.Format("{0}", _formName)
            Return Me.Text
        End Get
        Set(ByVal value As String)
            _formName = value
        End Set
    End Property
    Protected ReadOnly Property TextFileMgr() As TextFileMgr
        Get
            If _textFileMgr Is Nothing Then
                _textFileMgr = New TextFileMgr(cCompanyMaster, cCompany, cAccMonth)
            End If
            Return _textFileMgr
        End Get
    End Property
    Protected ReadOnly Property ExportFileNameFull() As String
        Get
            Return String.Format("{0}{1}", SafePath(ExportDestination), ExportFileName)
        End Get
    End Property
    Protected Overridable Property ExportFileName() As String
        Get
            Return _exportFileName
        End Get
        Set(ByVal value As String)
            _exportFileName = value
        End Set
    End Property
    Protected Overridable Property ExportFileExtension() As String
        Get
            Return _exportFileExtension
        End Get
        Set(ByVal value As String)
            _exportFileExtension = value
        End Set
    End Property
    Protected Property ExportDestination() As String
        Get
            Return _exportDestination
        End Get
        Set(ByVal value As String)
            _exportDestination = value
        End Set
    End Property
    Protected Property DocRangeSummary() As String
        Get
            Return _docRangeSummary
        End Get
        Set(ByVal value As String)
            _docRangeSummary = value
        End Set
    End Property
    Protected Property TotalTransaction() As Int16
        Get
            Return _totalTransaction
        End Get
        Set(ByVal value As Int16)
            _totalTransaction = value
        End Set
    End Property
#End Region


#Region "Overrides"
    Protected Overrides Sub InitControls()
        'GetSettings(cCompany.CompanyGroup, cCompanyMaster.ID, cCompanyMaster.SysID, Me.Name)
        'Me.Text = FormName
        'PictureBox1.Image = My.Resources.HQBranch
        'txtAccMonth.MaxLength = 6
        'txtAccMonth.Text = cAccMonth.ID
        'btnExport.Enabled = False
        ''InitComboBox()

        'Dim mDestination As String = cCompanyMaster.ExportPath1
        'If Not String.IsNullOrEmpty(mDestination) Then
        '    Select Case TextFileMgr.IsExistOrNotAllowed(mDestination)
        '        Case FolderAccessState.Exist
        '            TxtDestAddress.Text = mDestination
        '            TxtDestAddress.Enabled = False
        '        Case FolderAccessState.NotExist
        '            TxtDestAddress.Text = String.Format("Destination '{0}' does not exist.", mDestination)
        '            btnExport.Enabled = False
        '            Return
        '        Case FolderAccessState.NotAllowed
        '            TxtDestAddress.Text = String.Format("Destination '{0}' has permission issue.", mDestination)
        '            btnExport.Enabled = False
        '            Return
        '    End Select
        'Else
        '    TxtDestAddress.Text = SafePath(Environment.GetFolderPath(Environment.SpecialFolder.Desktop))
        'End If
        'btnExport.Enabled = True
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

        End If
    End Sub
    'called after Enter keypress
    Protected Overrides Function ValidateControl(ByVal ctrl As Control, Optional ByVal isSuppressErrMsg As Boolean = False) As Boolean
        Dim mListErr As List(Of KeyValueHelper) = CheckData(ctrl)
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
    Protected Sub MtnBase_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AddHandlers(Me)
        InitControls()
        TxtDestAddress.TabStop = False
        txtFileName.TabStop = False
        StartPosition = FormStartPosition.CenterScreen
    End Sub
#End Region

#Region "Checkbox"
    Protected Overridable Sub cbxCreateFolder_CheckedChanged(sender As Object, e As EventArgs) Handles cbxCreateFolder.CheckedChanged
        Dim mFolderName As String = String.Format("{0}\{1}\{2}\{3}\",
                                                        FormName,
                                                        cCompany.ID,
                                                        cCompany.Branch,
                                                        txtAccMonth.Text.Trim)
        ExportDestination = SafePath(ExportDestination)
        If cbxCreateFolder.Checked Then
            ExportDestination += mFolderName
        Else
            ExportDestination = ExportDestination.Replace(mFolderName, String.Empty)
        End If
        Directory.CreateDirectory(ExportDestination)
        TxtDestAddress.Text = ExportDestination
    End Sub

#End Region

#Region "ComboBox"
    Protected Shadows Sub InitComboBox(ByVal kvpList As List(Of KeyValueHelper))
        ListComboBox(cboFileType, kvpList)
    End Sub
    Private Sub cboFileType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboFileType.SelectedIndexChanged
        If cboFileType IsNot Nothing Then
            ExportFileExtension = String.Format(".{0}", cboFileType.SelectedItem.key)
            txtFileName.Text = ExportFileName
        End If
    End Sub
#End Region

#Region "Textbox"
    Protected Sub txtAccMonth_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAccMonth.KeyPress
        e.KeyChar = CheckIntOnly(e.KeyChar, txtAccMonth.Text)
    End Sub
    Protected Sub txtAccMonth_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAccMonth.TextChanged
        btnExport.Enabled = (txtAccMonth.Text.Length = txtAccMonth.MaxLength)
        txtFileName.Text = ExportFileName
    End Sub
    Protected Sub Txt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtDestAddress.KeyPress, txtFileName.KeyPress
        e.Handled = True
    End Sub
#End Region

#Region "Button"
    Protected Sub BtnBrowseDestination_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseDestination.Click
        Dim mFolderBrowserDialog As New FolderBrowserDialog
        lblStatus.Text = String.Empty
        If mFolderBrowserDialog.ShowDialog() = DialogResult.OK Then
            ExportDestination = mFolderBrowserDialog.SelectedPath
        Else
            If String.IsNullOrEmpty(TxtDestAddress.Text) Then
                ExportDestination = SafePath(Environment.GetFolderPath(Environment.SpecialFolder.Desktop))
            Else
                ExportDestination = TxtDestAddress.Text
            End If
        End If
        cbxCreateFolder_CheckedChanged(Nothing, Nothing)
        btnExport.Enabled = True
        TxtDestAddress.Text = ExportDestination
    End Sub
    Protected Overridable Sub BtnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Export()
    End Sub
    Protected Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Protected Overridable Sub Export()
    End Sub
#End Region

#Region "TextFileMgr"
#End Region


#Region "Print"
    Protected Overridable Sub Print(fn As String, notran As Int16, docno As String)
        'Dim rpt As RpReport

        'Try
        '    'FilterValue()
        '    Dim _inputam As String = ""
        '    If Me.txtAccMonth.Text.Trim.Length > 0 Then
        '        _inputam = Me.GetInputAM(_connstr, Me.txtAccMonth.Text.Trim)
        '    Else
        '        _inputam = cAccMonth.ID
        '    End If

        '    rpt = RpReport.GetReportViewer(_connstr, String.Format("{0}{1}", RpPath, "SSTTransactionSummary.rpt"), cCompany.InstanceName)
        '    With rpt
        '        .Report.DataDefinition.FormulaFields("Company").Text = String.Format("'{0} ({1})'", cCompany.Name, cCompany.RegNo)
        '        .Report.DataDefinition.FormulaFields("GSTRegNo").Text = String.Format("'{0}'", cCompany.TaxNo)
        '        If Not String.IsNullOrEmpty(cCompany.ServiceTaxNo) Then .Report.DataDefinition.FormulaFields("SSTRegNo").Text = String.Format("'{0}'", cCompany.ServiceTaxNo)
        '        .Report.DataDefinition.FormulaFields("SystemName").Text = String.Format("'{0}'", My.Settings.ApplicationName)
        '        .Report.DataDefinition.FormulaFields("Branch").Text = String.Format("'({0})'", cCompany.Branch)
        '        .Report.DataDefinition.FormulaFields("DisplayAccMth").Text = String.Format("'{0}'", Me.txtAccMonth.Text.Trim)
        '        .Report.DataDefinition.FormulaFields("COID").Text = String.Format("'{0}'", cCompany.ID)
        '        .Report.DataDefinition.FormulaFields("DOCNO").Text = String.Format("'{0}'", docno)
        '        .Report.DataDefinition.FormulaFields("NOTRAN").Text = String.Format("'{0}'", notran)
        '        .Report.DataDefinition.FormulaFields("FILEPATH").Text = String.Format("'{0}'", fn)
        '        .Report.DataDefinition.FormulaFields("Title").Text = String.Format("'{0}'", "AR")

        '        .Show()
        '    End With
        'Catch ex As Exception
        '    MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Error Message")
        'End Try



        'Dim mRpReport As New RpReport(APPRPPATH, ReportNames.TC.ToString, CONNSTR, "") 'cCompany.InstanceName)
        'mRpReport.AddParameter(
        '    New KeyValueHelper("@InputAccMonth", "201801"),
        '    New KeyValueHelper("@ID", Nothing),
        '    New KeyValueHelper("@Description", Nothing),
        '    New KeyValueHelper("@IsActive", Nothing),
        '    New KeyValueHelper("@IsPrimary", Nothing),
        '    New KeyValueHelper("@IsDiffMonth", Nothing),
        '    New KeyValueHelper("@NoDoc", Nothing),
        '    New KeyValueHelper("@NotAllow", Nothing),
        '    New KeyValueHelper("@AllowAR", Nothing),
        '    New KeyValueHelper("@AllowAP", Nothing),
        '    New KeyValueHelper("@ChequeInput", Nothing),
        '    New KeyValueHelper("@DisplayID", Nothing),
        '    New KeyValueHelper("@SortField", Nothing),
        '    New KeyValueHelper("@IsLike", Nothing)
        ')
        'mRpReport.AddFilter(
        '    String.Format("{0} = {1}", "Company Name", cCompany.Name)
        ')
        'mRpReport.AddFormula(
        '    New KeyValueHelper("Company", cCompany.Name),
        '    New KeyValueHelper("SystemName", APPNAME),
        '    New KeyValueHelper("CoID", cCompany.ID),
        '    New KeyValueHelper("Branch", cCompany.Branch),
        '    New KeyValueHelper("RunDate", Now.ToShortDateString),
        '    New KeyValueHelper("TaxNoText", mRpReport.FilterText), 'New KeyValueHelper("TaxNoText", cCompanyMaster.ServiceTaxNo),
        '    New KeyValueHelper("AsAt", String.Format("{0}/{1}", cAccMonth.ID.Substring(4, 2), cAccMonth.ID.Substring(0, 4)))
        ')
        ''mRpReport.AddSelection(
        ''    New SelectionHelper("spTC_Get;1", TC.DbFields.Tc_ID.ToString, "'D*'", FilterType.Like, SelectionType.And)
        ''    )
        'mRpReport.AddSelection(
        '    New SelectionHelper(New CompareHelper(New FieldHelper("spTC_Get;1", "Tc_ID").ToText, FilterType.Like, "'A*'")),
        '    New SelectionHelper(New CompareHelper(New FieldHelper("spTC_Get;1", "Tc_Description").ToText, FilterType.Like, "'*Voucher*'"), SelectionType.And)
        '    )
        'mRpReport.SetSelectionFormula()

        'mRpReport.Show()
    End Sub










#End Region

End Class
