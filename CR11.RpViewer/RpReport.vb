Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports Helper
Imports Helper.ApplicationTools
Imports System.Windows.Forms
Imports System.Drawing.Printing
Imports CrystalDecisions.Windows.Forms
Imports CR11.CrystalHelper.CrystalHelper


Public Class RpReport
    Private _CrysHelper As CrystalHelper.CrystalHelper
    Private _FilterBuilder As FilterBuilder
    Private _SelBuilder As SelectionBuilder


#Region "New"
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
        _CrysHelper = New CrystalHelper.CrystalHelper
        _FilterBuilder = New FilterBuilder
        _SelBuilder = New SelectionBuilder

        If RpViewerConfigOpt IsNot Nothing Then
            With crViewer
                .ShowCloseButton = If(RpViewerConfigOpt.ShowCloseButton.HasValue, RpViewerConfigOpt.ShowCloseButton.Value, True)
                .ShowExportButton = If(RpViewerConfigOpt.ShowExportButton.HasValue, RpViewerConfigOpt.ShowExportButton, True)
                .ShowGotoPageButton = If(RpViewerConfigOpt.ShowGotoPageButton.HasValue, RpViewerConfigOpt.ShowGotoPageButton, True)
                .ShowPageNavigateButtons = If(RpViewerConfigOpt.ShowPageNavigateButtons.HasValue, RpViewerConfigOpt.ShowPageNavigateButtons.Value, True)
                .ShowPrintButton = If(RpViewerConfigOpt.ShowPrintButton.HasValue, RpViewerConfigOpt.ShowPrintButton.Value, True)
                .ShowRefreshButton = If(RpViewerConfigOpt.ShowRefreshButton.HasValue, RpViewerConfigOpt.ShowRefreshButton, True)
                .ShowTextSearchButton = If(RpViewerConfigOpt.ShowTextSearchButton.HasValue, RpViewerConfigOpt.ShowTextSearchButton.Value, True)
                .ShowZoomButton = If(RpViewerConfigOpt.ShowZoomButton.HasValue, RpViewerConfigOpt.ShowZoomButton.Value, True)
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
            Report.Subreports.Item(name.ToString + ".rpt").DataDefinition.FormulaFields(kvh.Key.ToString).Text = CEmpty(kvh.Value)
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
            .PrintToPrinter(CntCopy, False, 0, 0)
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



    Private Sub crViewer_Drill(source As Object, e As DrillEventArgs) Handles crViewer.Drill

    End Sub
#End Region

End Class


Public Class ReportViewerConfigOptions

#Region "Fields"
    Private _ShowCloseButton As Boolean?
    Private _ShowExportButton As Boolean?
    Private _ShowGotoPageButton As Boolean?
    Private _ShowPageNavigateButtons As Boolean?
    Private _ShowPrintButton As Boolean?
    Private _ShowRefreshButton As Boolean?
    Private _ShowTextSearchButton As Boolean?
    Private _ShowZoomButton As Boolean?
    'Private _ShowCopyButton As Boolean?            'not supported in 10.2.3600.0
    'Private _ShowGroupTreeButton As Boolean?       'not supported in 10.2.3600.0
    'Private _ShowLogo As Boolean?                  'not supported in 10.2.3600.0
    'Private _ShowParameterPanelButton As Boolean?  'not supported in 10.2.3600.0
    'Private _PanelType As PanelType                'not supported in 10.2.3600.0    
#End Region

#Region "New"
    Public Sub New()
    End Sub
#End Region

#Region "Properties"

#End Region
    Public Property ShowCloseButton() As Boolean?
        Get
            Return _ShowCloseButton
        End Get
        Set(value As Boolean?)
            _ShowCloseButton = value
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

    Public Property ShowPageNavigateButtons() As Boolean?
        Get
            Return _ShowPageNavigateButtons
        End Get
        Set(value As Boolean?)
            _ShowPageNavigateButtons = value
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
    'Public Property ShowCopyButton() As Boolean?
    '    Get
    '        Return _ShowCopyButton
    '    End Get
    '    Set(value As Boolean?)
    '        _ShowCopyButton = value
    '    End Set
    'End Property
    'Public Property ShowGroupTreeButton() As Boolean?
    '    Get
    '        Return _ShowGroupTreeButton
    '    End Get
    '    Set(value As Boolean?)
    '        _ShowGroupTreeButton = value
    '    End Set
    'End Property
    'Public Property ShowLogo() As Boolean?
    '    Get
    '        Return _ShowLogo
    '    End Get
    '    Set(value As Boolean?)
    '        _ShowLogo = value
    '    End Set
    'End Property
    'Public Property ShowParameterPanelButton() As Boolean?
    '    Get
    '        Return _ShowParameterPanelButton
    '    End Get
    '    Set(value As Boolean?)
    '        _ShowParameterPanelButton = value
    '    End Set
    'End Property
    'Public Property PanelType() As PanelType
    '    Get
    '        Return _PanelType
    '    End Get
    '    Set(value As PanelType)
    '        _PanelType = value
    '    End Set
    'End Property
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



