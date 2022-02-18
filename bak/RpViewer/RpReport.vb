Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Windows.Forms
Imports CrystalDecisions.Shared
Imports Helper
Imports Helper.ApplicationTools
Imports System.Windows.Forms
Imports System.Drawing.Printing

#Region "Enum"
Public Enum PanelType
    None
    Grouptree
    Parameterpanel
End Enum
#End Region

Public Class RpReport

#Region "Fields"
    Private _CrysHelper As CrystalHelper
    Private _FilterBuilder As FilterBuilder
    Private _SelBuilder As SelectionBuilder
#End Region

#Region "New"
    'Public Sub New(ByVal RpName As ReportNames)
    '    Me.New(MdConfigCore.CrystalRpPath, RpName.ToString,
    '           MdConfigCore.SQLConnStr, CSuppress(MdConfigCore.ODBCName, True))
    'End Sub
    Public Sub New(ByVal RpPath As String,
                   ByVal RpName As String,
                   ByVal RpConn As String,
                   Optional ByVal RpODBC As String = Nothing,
                   Optional ByVal RpViewerConfigOpt As ReportViewerConfigOptions = Nothing)
        Initialize(RpViewerConfigOpt)

        With _CrysHelper
            .OpenReport(RpPath, RpName, RpConn, RpODBC)
            .SetViewerSource(crViewer)
        End With
    End Sub

    Private Sub Initialize(RpViewerConfigOpt As ReportViewerConfigOptions)
        InitializeComponent()
        _CrysHelper = New CrystalHelper
        _FilterBuilder = New FilterBuilder
        _SelBuilder = New SelectionBuilder

        If RpViewerConfigOpt IsNot Nothing Then
            With crViewer
                .ShowCloseButton = If(RpViewerConfigOpt.ShowCloseButton.HasValue, RpViewerConfigOpt.ShowCloseButton.Value, True)
                .ShowCopyButton = If(RpViewerConfigOpt.ShowCopyButton.HasValue, RpViewerConfigOpt.ShowCopyButton.Value, True)
                .ShowExportButton = If(RpViewerConfigOpt.ShowExportButton.HasValue, RpViewerConfigOpt.ShowExportButton, True)
                .ShowGotoPageButton = If(RpViewerConfigOpt.ShowGotoPageButton.HasValue, RpViewerConfigOpt.ShowGotoPageButton, True)
                .ShowGroupTreeButton = If(RpViewerConfigOpt.ShowGroupTreeButton.HasValue, RpViewerConfigOpt.ShowGroupTreeButton, True)
                .ShowLogo = If(RpViewerConfigOpt.ShowLogo.HasValue, RpViewerConfigOpt.ShowLogo.Value, True)
                .ShowPageNavigateButtons = If(RpViewerConfigOpt.ShowPageNavigateButtons.HasValue, RpViewerConfigOpt.ShowPageNavigateButtons.Value, True)
                .ShowParameterPanelButton = If(RpViewerConfigOpt.ShowParameterPanelButton.HasValue, RpViewerConfigOpt.ShowParameterPanelButton.Value, True)
                .ShowPrintButton = If(RpViewerConfigOpt.ShowPrintButton.HasValue, RpViewerConfigOpt.ShowPrintButton.Value, True)
                .ShowRefreshButton = If(RpViewerConfigOpt.ShowRefreshButton.HasValue, RpViewerConfigOpt.ShowRefreshButton, True)
                .ShowTextSearchButton = If(RpViewerConfigOpt.ShowTextSearchButton.HasValue, RpViewerConfigOpt.ShowTextSearchButton.Value, True)
                .ShowZoomButton = If(RpViewerConfigOpt.ShowZoomButton.HasValue, RpViewerConfigOpt.ShowZoomButton.Value, True)

                Select Case RpViewerConfigOpt.PanelType
                    Case PanelType.None
                        .ToolPanelView = ToolPanelViewType.None
                    Case PanelType.Grouptree
                        .ToolPanelView = ToolPanelViewType.GroupTree
                    Case PanelType.Parameterpanel
                        .ToolPanelView = ToolPanelViewType.ParameterPanel
                End Select
            End With
        End If
    End Sub
    '# add
    Public Sub New(ByVal RpPath As String,
                   ByVal RpName As String,
                   ByVal RpConn As String,
                   Optional ByVal RpViewerConfigOpt As ReportViewerConfigOptions = Nothing,
                   Optional ByVal RpPassword As String = Nothing,
                   Optional ByVal RpOtherDbInfo As KeyValueHelper = Nothing)
        Initialize(RpViewerConfigOpt)

        With _CrysHelper
            .OpenReportOLEDb(RpPath, RpName, RpConn, RpPassword, RpOtherDbInfo)
            .SetViewerSource(crViewer)
        End With
    End Sub
#End Region

#Region "Property"
    Public ReadOnly Property Report() As ReportDocument
        Get
            Return _CrysHelper.Report
        End Get
    End Property
    Public ReadOnly Property FilterText() As String
        Get
            Return _FilterBuilder.ToText
        End Get
    End Property
    Public ReadOnly Property SelectionText() As String
        Get
            Return _SelBuilder.ToText
        End Get
    End Property
#End Region

#Region "Load"
    Sub mFormLoad(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        With _CrysHelper
            .SetViewerSource(crViewer)
        End With
    End Sub
    Sub mFormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs) Handles Me.FormClosed
        CloseReport()
    End Sub
#End Region


#Region "Function"
    Public Sub AddFormulaRaw(ByVal ParamArray vkp() As KeyValueHelper)
        For Each kvh As KeyValueHelper In vkp
            Report.DataDefinition.FormulaFields(kvh.Key.ToString).Text = CEmpty(kvh.Value)
        Next
    End Sub
    Public Sub AddFormula(ByVal ParamArray vkp() As KeyValueHelper)
        For Each kvh As KeyValueHelper In vkp
            Report.DataDefinition.FormulaFields(kvh.Key.ToString).Text = CQuote(CEmpty(kvh.Value))
        Next
    End Sub
    Public Sub AddFormulaSubReport(ByVal name As String, ByVal ParamArray vkp() As KeyValueHelper)
        For Each kvh As KeyValueHelper In vkp
            Report.Subreports.Item(name + ".rpt").DataDefinition.FormulaFields(kvh.Key.ToString).Text = CEmpty(kvh.Value)
        Next
    End Sub
    Public Sub AddSelection(ByVal ParamArray vkp() As SelectionHelper)
        _SelBuilder.Append(vkp)
    End Sub
    Public Sub AddSelection(ByVal vkpList As List(Of SelectionHelper))
        _SelBuilder.Append(vkpList.ToArray)
    End Sub
    Public Sub AddFilter(ByVal ParamArray text() As String)
        _FilterBuilder.Append(text)
    End Sub
    Public Sub SetSelectionFormula()
        Report.DataDefinition.RecordSelectionFormula = _SelBuilder.ToText
    End Sub
    Public Sub AddParameter(ByVal ParamArray param() As KeyValueHelper)
        For Each p As KeyValueHelper In param
            AddParameter(p)
        Next
    End Sub
    Private Sub AddParameter(ByVal param As KeyValueHelper)
        Dim mFieldDefs As ParameterFieldDefinitions
        Dim mFieldDef As ParameterFieldDefinition
        Dim mVal As ParameterValues
        Dim mDiscVal As ParameterDiscreteValue

        ''Get the collection of parameters from the report
        mFieldDefs = Report.DataDefinition.ParameterFields

        mDiscVal = Nothing

        ''Access the specified parameter from the collection
        mFieldDef = mFieldDefs(param.Key)

        ''Get the current values from the parameter field.  At this point
        ''there are zero values set.
        mVal = mFieldDef.CurrentValues

        'Set the current values for the parameter field
        mDiscVal = New CrystalDecisions.Shared.ParameterDiscreteValue
        mDiscVal.Value = param.Value  '1st current value

        'Add the first current value for the parameter field
        mVal.Add(mDiscVal)

        'All current parameter values must be applied for the parameter field.
        mFieldDef.ApplyCurrentValues(mVal)

    End Sub
#End Region

#Region "Process"
    Public Sub Print(ByVal Printer As String, ByVal CntCopy As Integer)
        With Report
            .PrintOptions.PrinterName = IIf(Not CEmpty(Printer) = "", CEmpty(Printer), CEmpty(New PrinterSettings().PrinterName))
            Try
                .PrintToPrinter(CntCopy, False, 0, 0)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End With
    End Sub
    Public Sub Export(ByVal Format As ExportFormatType, ByVal Path As String)
        With Report
            .ExportToDisk(Format, Path)
        End With
    End Sub
    Public Sub CloseReport()
        Try
            If Report Is Nothing Then Exit Sub
            If Not Report.IsLoaded Then Exit Sub
            Report.Close()
        Catch ex As Exception
            DoNothing()
        End Try
    End Sub

#End Region

End Class


Public Class ReportViewerConfigOptions

#Region "Fields"
    Private _ShowCloseButton As Boolean?
    Private _ShowCopyButton As Boolean?
    Private _ShowExportButton As Boolean?
    Private _ShowGotoPageButton As Boolean?
    Private _ShowGroupTreeButton As Boolean?
    Private _ShowLogo As Boolean?
    Private _ShowPageNavigateButtons As Boolean?
    Private _ShowParameterPanelButton As Boolean?
    Private _ShowPrintButton As Boolean?
    Private _ShowRefreshButton As Boolean?
    Private _ShowTextSearchButton As Boolean?
    Private _ShowZoomButton As Boolean?
    Private _PanelType As PanelType
#End Region

#Region "New"
    Public Sub New()
    End Sub
#End Region

#Region "Properties"
    Public Property ShowCloseButton() As Boolean?
        Get
            Return _ShowCloseButton
        End Get
        Set(value As Boolean?)
            _ShowCloseButton = value
        End Set
    End Property
    Public Property ShowCopyButton() As Boolean?
        Get
            Return _ShowCopyButton
        End Get
        Set(value As Boolean?)
            _ShowCopyButton = value
        End Set
    End Property
    Public Property ShowExportButton() As Boolean?
        Get
            Return _ShowExportButton
        End Get
        Set(value As Boolean?)
            _ShowExportButton = value
        End Set
    End Property
    Public Property ShowGotoPageButton() As Boolean?
        Get
            Return _ShowGotoPageButton
        End Get
        Set(value As Boolean?)
            _ShowGotoPageButton = value
        End Set
    End Property
    Public Property ShowGroupTreeButton() As Boolean?
        Get
            Return _ShowGroupTreeButton
        End Get
        Set(value As Boolean?)
            _ShowGroupTreeButton = value
        End Set
    End Property
    Public Property ShowLogo() As Boolean?
        Get
            Return _ShowLogo
        End Get
        Set(value As Boolean?)
            _ShowLogo = value
        End Set
    End Property
    Public Property ShowPageNavigateButtons() As Boolean?
        Get
            Return _ShowPageNavigateButtons
        End Get
        Set(value As Boolean?)
            _ShowPageNavigateButtons = value
        End Set
    End Property
    Public Property ShowParameterPanelButton() As Boolean?
        Get
            Return _ShowParameterPanelButton
        End Get
        Set(value As Boolean?)
            _ShowParameterPanelButton = value
        End Set
    End Property
    Public Property ShowPrintButton() As Boolean?
        Get
            Return _ShowPrintButton
        End Get
        Set(value As Boolean?)
            _ShowPrintButton = value
        End Set
    End Property
    Public Property ShowRefreshButton() As Boolean?
        Get
            Return _ShowRefreshButton
        End Get
        Set(value As Boolean?)
            _ShowRefreshButton = value
        End Set
    End Property
    Public Property ShowTextSearchButton() As Boolean?
        Get
            Return _ShowTextSearchButton
        End Get
        Set(value As Boolean?)
            _ShowTextSearchButton = value
        End Set
    End Property
    Public Property ShowZoomButton() As Boolean?
        Get
            Return _ShowZoomButton
        End Get
        Set(value As Boolean?)
            _ShowZoomButton = value
        End Set
    End Property
    Public Property PanelType() As PanelType
        Get
            Return _PanelType
        End Get
        Set(value As PanelType)
            _PanelType = value
        End Set
    End Property
#End Region
End Class

#Region "Winform sample"
'Protected Overrides Sub tsbtnPrint_Click(sender As Object, e As EventArgs)
'    MyBase.tsbtnPrint_Click(sender, e)
'    Dim mRpReport As New RpReport(AppRpPath, ReportNames.TC.ToString, ConnStr, "") 'cCompany.InstanceName)
'    mRpReport.AddParameter(
'            New KeyValueHelper("@InputAccMonth", "201801"),
'            New KeyValueHelper("@ID", Nothing),
'            New KeyValueHelper("@Description", Nothing),
'            New KeyValueHelper("@IsActive", Nothing),
'            New KeyValueHelper("@IsPrimary", Nothing),
'            New KeyValueHelper("@IsDiffMonth", Nothing),
'            New KeyValueHelper("@NoDoc", Nothing),
'            New KeyValueHelper("@NotAllow", Nothing),
'            New KeyValueHelper("@AllowAR", Nothing),
'            New KeyValueHelper("@AllowAP", Nothing),
'            New KeyValueHelper("@ChequeInput", Nothing),
'            New KeyValueHelper("@DisplayID", Nothing),
'            New KeyValueHelper("@SortField", Nothing),
'            New KeyValueHelper("@IsLike", Nothing)
'        )
'    mRpReport.AddFilter(
'            String.Format("{0} = {1}", "Company Name", cCompany.Name)
'        )
'    mRpReport.AddFormula(
'            New KeyValueHelper("Company", cCompany.Name),
'            New KeyValueHelper("SystemName", AppName),
'            New KeyValueHelper("CoID", cCompany.ID),
'            New KeyValueHelper("Branch", cCompany.Branch),
'            New KeyValueHelper("RunDate", Now.ToShortDateString),
'            New KeyValueHelper("TaxNoText", mRpReport.FilterText), 'New KeyValueHelper("TaxNoText", cCompanyMaster.ServiceTaxNo),
'            New KeyValueHelper("AsAt", String.Format("{0}/{1}", cAccMonth.ID.Substring(4, 2), cAccMonth.ID.Substring(0, 4)))
'        )
'   mRpReport.AddFormulaRaw(
'                New KeyValueHelper("ShowDetail", String.Format("{0}", IIf(optSummary.Checked, False, True))),
'                New KeyValueHelper("ShowAging", String.Format("{0}", cbxShowAging.Checked)),
'                New KeyValueHelper("ShowVoucherNo", String.Format("{0}", cbxShowSNNo.Checked)),
'                New KeyValueHelper("ShowParticulars", String.Format("{0}", cbxShowParticulars.Checked)),
'                New KeyValueHelper("ShowPrintDate", String.Format("{0}", cbxShowPrintDate.Checked))
'            )
'    'mRpReport.AddSelection(
'    '    New SelectionHelper("spTC_Get;1", TC.DbFields.Tc_ID.ToString, "'D*'", FilterType.Like, SelectionType.And)
'    '    )
'    mRpReport.AddSelection(
'            New SelectionHelper(New CompareHelper(New FieldHelper("spTC_Get;1", "Tc_ID").ToText, FilterType.Like, "'A*'")),
'            New SelectionHelper(New CompareHelper(New FieldHelper("spTC_Get;1", "Tc_Description").ToText, FilterType.Like, "'*Voucher*'"), SelectionType.And)
'            )
'    mRpReport.SetSelectionFormula()

'    mRpReport.Show()
'End Sub
#End Region


