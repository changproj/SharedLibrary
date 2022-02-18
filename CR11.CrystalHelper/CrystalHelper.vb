Imports CrystalDecisions.Windows.Forms
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient
Imports Helper.ApplicationTools
Imports Helper

Public Class CrystalHelper

#Region "Variable"
    Private mRptDocument As ReportDocument
    Private mRptPath, mRptName As String
#End Region

#Region "New"
    Public Sub New()
        mRptDocument = New ReportDocument
    End Sub
    Public Sub New(ByVal reportDocument As ReportDocument)
        mRptDocument = reportDocument
    End Sub
#End Region

#Region "Property"
    Public ReadOnly Property Report() As ReportDocument
        Get
            Return mRptDocument
        End Get
    End Property
    Public Property ReportPath() As String
        Get
            Return mRptPath
        End Get
        Set(ByVal value As String)
            mRptPath = value
        End Set
    End Property
    Public Property ReportName() As String
        Get
            Return mRptName
        End Get
        Set(ByVal value As String)
            mRptName = value
        End Set
    End Property
    Public ReadOnly Property ReportFullPath() As String
        Get
            Return mRptPath + mRptName + ".rpt"
        End Get
    End Property
#End Region

#Region "Open"
    Public Sub OpenReport(ByVal RpPath As String,
                      ByVal RpName As String,
                      ByVal RpConn As String,
                      Optional ByVal RpODBC As String = Nothing)
        If mRptDocument.IsLoaded Then mRptDocument.Close()
        mRptPath = RpPath
        mRptName = RpName
        mRptDocument.Load(ReportFullPath)
        Dim mRpODBC As String = CEmpty(RpODBC)
        Dim mRpSQL As New SqlConnectionStringBuilder(RpConn)
        Dim mConnInfo As New ConnectionInfo
        With mConnInfo
            .Type = ConnectionInfoType.SQL
            .ServerName = IIf(mRpODBC = "", mRpSQL.DataSource, mRpODBC)
            .DatabaseName = mRpSQL.InitialCatalog
            .IntegratedSecurity = mRpSQL.IntegratedSecurity
            If Not .IntegratedSecurity Then
                .UserID = mRpSQL.UserID
                .Password = mRpSQL.Password
            End If
        End With
        AssignTableConnections(mConnInfo)
    End Sub
    '# add
    Public Sub OpenReportOLEDb(ByVal RpPath As String,
                      ByVal RpName As String,
                      ByVal RpConn As String,
                      Optional ByVal RpPassword As String = Nothing,
                      Optional ByVal RpOtherDbInfo As KeyValueHelper = Nothing)
        If mRptDocument.IsLoaded Then mRptDocument.Close()
        mRptPath = RpPath
        mRptName = RpName
        mRptDocument.Load(ReportFullPath)
        AssignTableConnections(RpConn, RpPassword, RpOtherDbInfo)
    End Sub
    Public Sub OpenReport(ByVal RpPath As String,
                      ByVal RpName As String,
                      ByVal RpDataSource As Object)
        If mRptDocument.IsLoaded Then mRptDocument.Close()
        mRptPath = RpPath
        mRptName = RpName
        mRptDocument.Load(ReportFullPath)
        mRptDocument.SetDataSource(RpDataSource)
    End Sub
#End Region

#Region "Internal Function"
    Private Function ValidateIsLoaded() As Boolean
        If Not mRptDocument.IsLoaded Then Throw New InvalidOperationException("The report is not open.")
        Return mRptDocument.IsLoaded
    End Function
    Private Sub AssignTableConnection(ByVal table As Engine.Table, ByVal connection As ConnectionInfo)
        With table
            .LogOnInfo.ConnectionInfo = connection
            .ApplyLogOnInfo(.LogOnInfo)
        End With
    End Sub
    '# add
    Private Sub AssignTableConnection(ByVal table As Engine.Table, ByVal tableLogOn As TableLogOnInfo) 'mdb
        With table
            .ApplyLogOnInfo(.LogOnInfo)
        End With
    End Sub
#End Region

#Region "Function"
    Public Sub SetViewerSource(ByVal crv As CrystalReportViewer)
        crv.ReportSource = Report
    End Sub
    Public Sub AssignTableConnections(ByVal connectionInfo As ConnectionInfo)
        If Not ValidateIsLoaded() Then Exit Sub
        For Each table As Table In mRptDocument.Database.Tables
            AssignTableConnection(table, connectionInfo)
        Next
        For Each section As Section In mRptDocument.ReportDefinition.Sections
            For Each obj As ReportObject In section.ReportObjects
                If (obj.Kind = ReportObjectKind.SubreportObject) Then
                    Dim subReport As SubreportObject = obj
                    Dim subDocument As ReportDocument = subReport.OpenSubreport(subReport.SubreportName)
                    For Each table As Table In subDocument.Database.Tables
                        AssignTableConnection(table, connectionInfo)
                    Next
                End If
            Next
        Next
    End Sub
    '# add
    Public Sub AssignTableConnections(ByVal serverName As String, ByVal password As String, ByVal rpOtherDbInfo As KeyValueHelper) 'mdb
        If Not ValidateIsLoaded() Then Exit Sub
        For Each table As Table In mRptDocument.Database.Tables
            Dim logonInfo As TableLogOnInfo = New TableLogOnInfo()
            logonInfo = table.LogOnInfo
            If table.Name.ToUpper = rpOtherDbInfo?.Key.ToUpper Then
                logonInfo.ConnectionInfo.ServerName = rpOtherDbInfo.Value
            Else
                logonInfo.ConnectionInfo.ServerName = serverName
            End If
            'logonInfo.ConnectionInfo.UserID = "Admin"
            'logonInfo.ConnectionInfo.Password = password

            'If Not password Is Nothing Then logonInfo.ConnectionInfo.LogonProperties.Add(New NameValuePair2("Jet OLEDB:Database Password", password))
            If Not password Is Nothing Then logonInfo.ConnectionInfo.Password = String.Format("{0}", password)
            AssignTableConnection(table, logonInfo)
        Next
        For Each section As Section In mRptDocument.ReportDefinition.Sections
            For Each obj As ReportObject In section.ReportObjects
                If (obj.Kind = ReportObjectKind.SubreportObject) Then
                    Dim subReport As SubreportObject = obj
                    Dim subDocument As ReportDocument = subReport.OpenSubreport(subReport.SubreportName)
                    For Each table As Table In subDocument.Database.Tables
                        Dim logonInfo As TableLogOnInfo = New TableLogOnInfo()
                        logonInfo = table.LogOnInfo
                        If table.Name.ToUpper = rpOtherDbInfo?.Key.ToUpper Then
                            logonInfo.ConnectionInfo.ServerName = rpOtherDbInfo.Value
                        Else
                            logonInfo.ConnectionInfo.ServerName = serverName
                        End If
                        'logonInfo.ConnectionInfo.UserID = "Admin"
                        'logonInfo.ConnectionInfo.Password = password

                        'If Not password Is Nothing Then logonInfo.ConnectionInfo.LogonProperties.Add(New NameValuePair2("Jet OLEDB:Database Password", password))
                        If Not password Is Nothing Then logonInfo.ConnectionInfo.Password = String.Format("{0}", password)
                        AssignTableConnection(table, logonInfo)
                    Next
                End If
            Next
        Next
    End Sub
    Public Sub CloseReport()
        If Not ValidateIsLoaded() Then Exit Sub
        mRptDocument.Close()
    End Sub
    Public Sub Export(ByVal fileName As String, ByVal exportType As ExportFormatType)
        If Not ValidateIsLoaded() Then Exit Sub
        Dim DestinationOptions As New DiskFileDestinationOptions()
        DestinationOptions.DiskFileName = fileName
        With mRptDocument.ExportOptions
            .ExportFormatType = exportType
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportDestinationOptions = DestinationOptions
        End With
        mRptDocument.Export()
    End Sub
    Public Function HasReportParameter(ByVal paramName As String) As Boolean
        If Not ValidateIsLoaded() Then Return False
        For Each parameter As ParameterFieldDefinition In mRptDocument.DataDefinition.ParameterFields
            If parameter.Name.Equals(paramName, StringComparison.CurrentCultureIgnoreCase) Then
                Return True
            End If
        Next
        Return False
    End Function
    Public Function GetParamsAsHTML() As String
        If Not ValidateIsLoaded() Then Return ""
        Dim result As String = ""
        Dim description, valueText As String
        Dim value As ParameterDiscreteValue
        For Each parameter As ParameterFieldDefinition In mRptDocument.DataDefinition.ParameterFields
            Try
                description = parameter.PromptText
                If (TypeOf parameter.CurrentValues(0) Is ParameterDiscreteValue) Then
                    value = parameter.CurrentValues(0)
                    If description.StartsWith("Enter ") Then description = description.Remove(0, 6)
                    valueText = value.Value.ToString
                Else
                    valueText = "[not supported param type]"
                End If
                result = result + "<b>" + description + "</b> " + valueText + "<br />"
            Catch ex As System.Exception
            End Try
        Next
        Return result
    End Function
    Public Sub PurgeSavedData()
        If Not ValidateIsLoaded() Then Exit Sub
        With mRptDocument
            If Not .HasSavedData Then Exit Sub
            .ReportOptions.EnableSaveDataWithReport = False
        End With
    End Sub
    Public Sub SetReportParameter(ByVal paramName As String, ByVal paramValue As Object)
        If paramValue Is Nothing Then Exit Sub
        Dim paramFound As Boolean
        For Each parameter As ParameterFieldDefinition In mRptDocument.DataDefinition.ParameterFields
            If Not parameter.Name.Equals(paramName, StringComparison.CurrentCultureIgnoreCase) OrElse
            parameter.ReportName <> String.Empty Then Continue For
            paramFound = True
            Dim newValue As New ParameterDiscreteValue
            newValue.Value = paramValue
            With parameter
                .CurrentValues.Clear()
                .CurrentValues.Add(newValue)
                .ApplyCurrentValues(.CurrentValues)
            End With
            Exit For
        Next
        If Not paramFound Then Throw New ArgumentException("Parameter [" + paramName + "] not found in the report.")
    End Sub
#End Region

End Class





'Imports System
'Imports System.IO
'Imports CrystalDecisions.CrystalReports.Engine
'Imports CrystalDecisions.Shared

'Public Class CrystalHelper

'    Public Shared Sub AssignTableConnections(ByVal reportDocument As ReportDocument, ByVal connectionInfo As ConnectionInfo)
'        If Not reportDocument.IsLoaded Then
'            Throw New InvalidOperationException("The report is not open.")
'        End If
'        ' Assign the connection info to all tables in the main report
'        For Each table As Table In reportDocument.Database.Tables
'            AssignTableConnection(table, connectionInfo)
'        Next
'        ' Assign the connection info to all tables in subreports
'        For Each section As CrystalDecisions.CrystalReports.Engine.Section In reportDocument.ReportDefinition.Sections
'            ' In each section we need to loop through all the reporting objects
'            For Each reportObject As ReportObject In section.ReportObjects
'                If (reportObject.Kind = ReportObjectKind.SubreportObject) Then
'                    'TODO: Warning!!! continue If

'                    Dim subReport As SubreportObject = CType(reportObject, SubreportObject)
'                    Dim subDocument As ReportDocument = subReport.OpenSubreport(subReport.SubreportName)
'                    For Each table As Table In subDocument.Database.Tables
'                        AssignTableConnection(table, connectionInfo)
'                    Next
'                End If
'                'If (reportObject.Kind <> ReportObjectKind.SubreportObject) Then
'                '    'TODO: Warning!!! continue If
'                'End If
'                'Dim subReport As SubreportObject = CType(reportObject, SubreportObject)
'                'Dim subDocument As ReportDocument = subReport.OpenSubreport(subReport.SubreportName)
'                'For Each table As Table In subDocument.Database.Tables
'                '    AssignTableConnection(table, connectionInfo)
'                'Next
'            Next
'        Next
'    End Sub

'    ''' <summary>
'    ''' Close opened report, otherwise throw exception
'    ''' </summary>
'    ''' <param name="reportDocument">Report instance</param>
'    Public Shared Sub CloseReport(ByVal reportDocument As ReportDocument)
'        If Not reportDocument.IsLoaded Then
'            Throw New InvalidOperationException("The report is not open.")
'        End If
'        reportDocument.Close()
'    End Sub

'    ''' <summary>
'    ''' Export report to file of specified format
'    ''' </summary>
'    ''' <param name="reportDocument">Report instance</param>
'    ''' <param name="fileName">Export File name</param>
'    ''' <param name="exportFormatType">Export Format type</param>
'    Public Shared Sub Export(ByVal reportDocument As ReportDocument, ByVal fileName As String, ByVal exportFormatType As ExportFormatType)
'        If Not reportDocument.IsLoaded Then
'            Throw New InvalidOperationException("The report is not open.")
'        End If
'        Dim exportOptions As ExportOptions = reportDocument.ExportOptions
'        ' Set export format type and Options
'        exportOptions.ExportFormatType = exportFormatType
'        ' Set destination type and file name
'        exportOptions.ExportDestinationType = ExportDestinationType.DiskFile
'        exportOptions.ExportDestinationOptions = New DiskFileDestinationOptions
'        CType(exportOptions.ExportDestinationOptions, DiskFileDestinationOptions).DiskFileName = fileName
'        reportDocument.Export()
'    End Sub

'    ''' <summary>
'    ''' Checks if specified parameter is present in the report
'    ''' </summary>
'    ''' <param name="reportDocument">Report instance</param>
'    ''' <param name="paramName">Parameter Name</param>
'    ''' <returns>Returns <b>true</b> if parameter is present in the report</returns>
'    Public Shared Function HasReportParameter(ByVal reportDocument As ReportDocument, ByVal paramName As String) As Boolean
'        If Not reportDocument.IsLoaded Then
'            Throw New InvalidOperationException("The report is not open.")
'        End If
'        For Each parameter As ParameterFieldDefinition In reportDocument.DataDefinition.ParameterFields
'            If parameter.Name.Equals(paramName, StringComparison.CurrentCultureIgnoreCase) Then
'                Return True
'            End If
'        Next
'        Return False
'    End Function

'    ''' <summary>
'    ''' Function returns list of parameters descriptions with associated values
'    ''' for specified report instance in the form of HTML code block which
'    ''' then can be used in the report to display current parameter values
'    ''' </summary>
'    ''' <param name="reportDocument">Report instance</param>
'    ''' <returns>List of parameter with values as HTML</returns>
'    Public Shared Function GetReportParamsAsText(ByVal reportDocument As ReportDocument) As String
'        If Not reportDocument.IsLoaded Then
'            Throw New InvalidOperationException("The report is not open.")
'        End If
'        Dim result As String = ""
'        Dim description As String
'        Dim valueText As String
'        Dim value As ParameterDiscreteValue
'        For Each parameter As ParameterFieldDefinition In reportDocument.DataDefinition.ParameterFields
'            Try

'                'If parameter.CurrentValues.IsNoValue Then
'                '    'TODO: Warning!!! continue If
'                'End If
'                description = parameter.PromptText
'                If (TypeOf parameter.CurrentValues(0) Is ParameterDiscreteValue) Then
'                    value = CType(parameter.CurrentValues(0), ParameterDiscreteValue)
'                    If description.StartsWith("Enter ") Then
'                        description = description.Remove(0, 6)
'                    End If
'                    valueText = value.Value.ToString
'                Else
'                    valueText = "[not supported param type]"
'                End If
'                result = (result + ("<b>" _
'                                + (description + ("</b> " _
'                                + (valueText + "<br />")))))

'            Catch ex As System.Exception
'                ' Ignore any errors
'            End Try
'        Next
'        Return result
'    End Function

'    ''' <summary>
'    ''' Load report and prepare database connection using provided Server, Database Names, and login info
'    ''' </summary>
'    ''' <param name="reportDocument">Report instance</param>
'    ''' <param name="reportFile">Report File Name</param>
'    ''' <param name="type">Type of database connection used</param>
'    ''' <param name="serverName">Database server name</param>
'    ''' <param name="databaseName">Database name</param>
'    ''' <param name="integratedSecurity">Use integrated security</param>
'    ''' <param name="userName">Database User Name</param>
'    ''' <param name="password">Database User Password</param>
'    Public Overloads Shared Sub OpenReport(ByVal reportDocument As ReportDocument,
'                                               ByVal reportFile As String,
'                                               ByVal type As ConnectionInfoType,
'                                               ByVal serverName As String,
'                                               ByVal databaseName As String,
'                                               ByVal integratedSecurity As Boolean,
'                                               ByVal userName As String,
'                                               ByVal password As String,
'                                               Optional ByVal instancename As String = Nothing)
'        ' Close report if it was opened before
'        If reportDocument.IsLoaded Then
'            reportDocument.Close()
'        End If
'        ' Load report file
'        reportDocument.Load(reportFile)
'        Dim connection As ConnectionInfo = New ConnectionInfo
'        ' Prepare connection info for the report
'        connection.Type = type 'ConnectionInfoType
'        'connection.ServerName = serverName
'        If instancename IsNot Nothing Then
'            connection.ServerName = instancename
'        End If
'        connection.DatabaseName = databaseName
'        connection.IntegratedSecurity = integratedSecurity
'        If Not integratedSecurity Then
'            connection.UserID = userName
'            connection.Password = password
'        End If
'        AssignTableConnections(reportDocument, connection)
'    End Sub

'    ''' <summary>
'    ''' Load report and prepare database connection using user credential
'    ''' </summary>
'    ''' <param name="reportDocument">Report instance</param>
'    ''' <param name="reportFile">Report File Name</param>
'    ''' <param name="type">Type of database connection used</param>
'    ''' <param name="serverName">Database server name</param>
'    ''' <param name="databaseName">Database name</param>
'    ''' <param name="userName">Database User Name</param>
'    ''' <param name="password">Database User Password</param>
'    Public Overloads Shared Sub OpenReport(ByVal reportDocument As ReportDocument, ByVal reportFile As String, ByVal type As ConnectionInfoType, ByVal serverName As String, ByVal databaseName As String, ByVal userName As String, ByVal password As String)
'        CrystalHelper.OpenReport(reportDocument, reportFile, type, serverName, databaseName, False, userName, password)
'    End Sub

'    ''' <summary>
'    ''' Load report and prepare database connection using integrated security context
'    ''' </summary>
'    ''' <param name="reportDocument">Report instance</param>
'    ''' <param name="reportFile">Report File Name</param>
'    ''' <param name="type">Type of database connection used</param>
'    ''' <param name="serverName">Database server name</param>
'    ''' <param name="databaseName">Database name</param>
'    Public Overloads Shared Sub OpenReport(ByVal reportDocument As ReportDocument, ByVal reportFile As String, ByVal type As ConnectionInfoType, ByVal serverName As String, ByVal databaseName As String)
'        CrystalHelper.OpenReport(reportDocument, reportFile, type, serverName, databaseName, True, String.Empty, String.Empty)
'    End Sub



'    ''' <summary>
'    ''' Purges saved report data if present
'    ''' </summary>
'    ''' <param name="reportDocument">Report instance</param>
'    Public Shared Sub PurgeSavedData(ByVal reportDocument As ReportDocument)
'        If Not reportDocument.IsLoaded Then
'            Throw New InvalidOperationException("The report is not open.")
'        End If
'        ' No saved data present, nothing to be done
'        If Not reportDocument.HasSavedData Then
'            Return
'        End If
'        ' Purge saved data associated with report
'        reportDocument.ReportOptions.EnableSaveDataWithReport = False
'    End Sub

'    ''' <summary>
'    ''' Set specified report parameter
'    ''' </summary>
'    ''' <param name="reportDocument">Report instance</param>
'    ''' <param name="paramName">Parameter Name</param>
'    ''' <param name="paramValue">Parameter Value</param>
'    Public Shared Sub SetReportParameter(ByVal reportDocument As ReportDocument, ByVal paramName As String, ByVal paramValue As Object)
'        ' Nothing to be set here
'        ' Use ClearReportParameter instead
'        If (paramValue Is Nothing) Then
'            Return
'        End If
'        Dim currentValues As CrystalDecisions.Shared.ParameterValues
'        Dim newValue As CrystalDecisions.Shared.ParameterDiscreteValue
'        Dim paramFound As Boolean = False
'        For Each parameter As ParameterFieldDefinition In reportDocument.DataDefinition.ParameterFields
'            If Not parameter.Name.Equals(paramName, StringComparison.CurrentCultureIgnoreCase) Then
'                'TODO: Warning!!! continue If
'            Else



'                ' List contains all report parameters, including one from subreports
'                ' Ignore any subreport parameters, should be set from main report
'                If (parameter.ReportName <> String.Empty) Then
'                    'TODO: Warning!!! continue If
'                End If
'                paramFound = True
'                ' Get the current value for the parameter
'                currentValues = parameter.CurrentValues
'                ' Clear any prior values
'                currentValues.Clear()
'                ' Create a value object for Crystal reports and assign the specified value.
'                newValue = New CrystalDecisions.Shared.ParameterDiscreteValue
'                newValue.Value = paramValue
'                ' Now add the new value to the values collection and apply the 
'                ' collection to the report.
'                currentValues.Add(newValue)
'                parameter.ApplyCurrentValues(currentValues)
'                Exit For
'            End If
'        Next
'        If Not paramFound Then
'            Throw New ArgumentException(("Parameter [" _
'                                + (paramName + "] not found in the report.")))
'        End If
'    End Sub

'    '''' <summary>
'    '''' Set specified report parameter
'    '''' </summary>
'    '''' <param name="reportDocument">Report instance</param>
'    '''' <param name="paramName">Parameter Name</param>
'    '''' <param name="paramValue">Parameter Value</param>
'    'Public Shared Sub SetReportParameter(ByVal reportDocument As ReportDocument, ByVal paramName As String, ByVal paramValue As Object)
'    '    ' Nothing to be set here
'    '    ' Use ClearReportParameter instead
'    '    If (paramValue Is Nothing) Then
'    '        Return
'    '    End If
'    '    Dim currentValues As CrystalDecisions.Shared.ParameterValues
'    '    Dim newValue As CrystalDecisions.Shared.ParameterDiscreteValue
'    '    Dim paramFound As Boolean = False
'    '    For Each parameter As ParameterFieldDefinition In ReportDocument.DataDefinition.ParameterFields
'    '        If Not parameter.Name.Equals(paramName, StringComparison.CurrentCultureIgnoreCase) Then
'    '            'TODO: Warning!!! continue If
'    '        End If
'    '        ' List contains all report parameters, including one from subreports
'    '        ' Ignore any subreport parameters, should be set from main report
'    '        If (parameter.ReportName <> String.Empty) Then
'    '            'TODO: Warning!!! continue If
'    '        End If
'    '        paramFound = True
'    '        ' Get the current value for the parameter
'    '        currentValues = parameter.CurrentValues
'    '        ' Clear any prior values
'    '        currentValues.Clear()
'    '        ' Create a value object for Crystal reports and assign the specified value.
'    '        newValue = New CrystalDecisions.Shared.ParameterDiscreteValue
'    '        newValue.Value = paramValue
'    '        ' Now add the new value to the values collection and apply the 
'    '        ' collection to the report.
'    '        currentValues.Add(newValue)
'    '        parameter.ApplyCurrentValues(currentValues)
'    '        Exit For
'    '    Next
'    '    If Not paramFound Then
'    '        Throw New ArgumentException(("Parameter [" _
'    '                        + (paramName + "] not found in the report.")))
'    '    End If
'    'End Sub

'    Public Shared Sub OpenPDF(ByVal Rel As ReportDocument)
'        Dim stream As MemoryStream = CType(Rel.ExportToStream(ExportFormatType.PortableDocFormat), MemoryStream)
'        'HttpContext.Current.Response.Clear()
'        'HttpContext.Current.Response.Buffer = True
'        'HttpContext.Current.Response.ContentType = "application/pdf"
'        'HttpContext.Current.Response.BinaryWrite(stream.ToArray)
'        'HttpContext.Current.Response.End()
'    End Sub

'    Public Shared Sub OpenCRViewer(ByVal CRV As CrystalDecisions.Windows.Forms.CrystalReportViewer, ByVal Rel As ReportDocument)

'        'CrystalReportViewer1.BackColor = Drawing.Color.AliceBlue
'        'CRV.SeparatePages = False
'        'CRV.DisplayGroupTree = False
'        'CRV.HasPageNavigationButtons = True
'        'CRV.HasCrystalLogo = False
'        'CRV.HasDrillUpButton = False
'        'CRV.HasSearchButton = False
'        'CRV.HasViewList = False
'        'CRV.HasToggleGroupTreeButton = False
'        'CRV.HasZoomFactorList = False

'        'rptviewer.EnableDatabaseLogonPrompt = False

'        CRV.ReportSource = Rel
'        'CRV.DataBind()
'    End Sub



'#Region "Internal functions"

'    Private Shared Sub AssignTableConnection(ByVal table As CrystalDecisions.CrystalReports.Engine.Table, ByVal connection As ConnectionInfo)
'        ' Cache the logon info block
'        Dim logOnInfo As TableLogOnInfo = table.LogOnInfo
'        ' Set the connection
'        logOnInfo.ConnectionInfo = connection
'        ' Apply the connection to the table!
'        table.ApplyLogOnInfo(logOnInfo)
'        'table.Location = connection.ServerName + "." + connection.DatabaseName + ".dbo." + table.Location.Substring(table.Location.LastIndexOf(".") + 1)
'    End Sub
'#End Region
'End Class

