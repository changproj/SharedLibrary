Imports Helper
Imports Helper.ApplicationTools
Imports Helper.DataEntity
Imports Validation
Namespace DataEntity

    Public Class CompanySystem
        Inherits DataEntityBase

        Private _AutoID As Int64?
        Private _CoID As String
        Private _SysID As String
        Private _DbName As String
        Private _ServerName As String
        Private _ConnectionString As String
        Private _ServerPath As String
        Private _BackupPath1 As String
        Private _BackupPath2 As String
        Private _ExportPath1 As String
        Private _ExportPath2 As String
        Private _ExportPath3 As String
        Private _Active As Boolean?
        Private _CreatedBy As String
        Private _CreatedOn As Date?
        Private _EditedBy As String
        Private _EditedOn As Date?

        Public Sub New()
            MyBase.New()
        End Sub
        Public Sub New(ByVal ParamArray vkp() As KeyValueHelper)
            MyBase.New(vkp)
        End Sub

#Region "Overrides"
        Public Overrides Sub AddBusinessRules(ByVal prop As String, ByVal func As RuleHandler)
            Validationrules.AddRule(func, prop)
        End Sub
        Public Overrides Sub AddBusinessRules(ByVal args As RuleArgs, ByVal func As RuleHandler)
            Validationrules.AddRule(func, args)
        End Sub
        Public Overrides Sub AddBusinessRules(ByVal funcList As Dictionary(Of String, RuleHandler))
            For Each key As String In funcList.Keys
                Dim mValue As RuleHandler = funcList(key)
                AddBusinessRules(key, mValue)
            Next
        End Sub
        Public Overrides Sub RemoveAllRules()
            Validationrules.RemoveAllRules()
        End Sub
        Public Overrides Sub CheckRules(ByVal propertyName As String)
            Validationrules.CheckRules(propertyName)
        End Sub
        Public Overrides Sub CheckAllRules()
            Validationrules.CheckRules()
        End Sub
#End Region

#Region "Field/ Property Mapping"
        Shadows Enum StoreProcedures
            spCompanySystem_Get
            spCompanySystem_Insert
            spCompanySystem_Update
            spCompanySystem_Delete
        End Enum
        Shadows Enum DbFields
            Cs_AutoID               'T
            Cs_CoID                 'T
            Cs_SysID                'T
            Cs_DbName               'T
            Cs_ServerName           'T
            Cs_ConnectionString     'T
            Cs_ServerPath           'T
            Cs_BackupPath1          'T
            Cs_BackupPath2          'T
            Cs_ExportPath1          'T
            Cs_ExportPath2          'T
            Cs_ExportPath3          'T
            Cs_Active               'T
            Cs_CreatedBy            'T 
            Cs_CreatedOn            'T 
            Cs_EditedBy             'T 
            Cs_EditedOn             'T 
        End Enum
        Shadows Enum Properties
            AutoID                  'T
            CoID                    'T
            SysID                   'T
            DbName                  'T
            ServerName              'T
            ConnectionString        'T
            ServerPath              'T
            BackupPath1             'T
            BackupPath2             'T
            ExportPath1             'T
            ExportPath2             'T
            ExportPath3             'T
            Active                  'T
            CreatedBy               'T 
            CreatedOn               'T 
            EditedBy                'T 
            EditedOn                'T                            
        End Enum
#End Region

#Region "Property"
        <PropertyDescriptionAttribute("AutoID")>
        Public Property AutoID() As Int64?
            Get
                Return _AutoID
            End Get
            Set(ByVal value As Int64?)
                If Not CEqual(_AutoID, value) Then
                    _AutoID = value
                    DataStateChanged(EnumObjectState.Modified, Properties.AutoID.ToString)
                End If
            End Set
        End Property
        <PropertyDescription("CoID", "Company ID")>
        Public Property CoID() As String
            Get
                Return _CoID
            End Get
            Set(ByVal value As String)
                If Not CEqual(_CoID, value) Then
                    _CoID = value
                    DataStateChanged(EnumObjectState.Modified, Properties.CoID.ToString)
                End If
            End Set
        End Property
        <PropertyDescription("SysID", "System ID")>
        Public Property SysID() As String
            Get
                Return _SysID
            End Get
            Set(ByVal value As String)
                If Not CEqual(_SysID, value) Then
                    _SysID = value
                    DataStateChanged(EnumObjectState.Modified, Properties.SysID.ToString)
                End If
            End Set
        End Property
        <PropertyDescription("DbName", "Db Name")>
        Public Property DbName() As String
            Get
                Return _DbName
            End Get
            Set(ByVal value As String)
                If Not CEqual(_DbName, value) Then
                    _DbName = value
                    DataStateChanged(EnumObjectState.Modified, Properties.DbName.ToString)
                End If
            End Set
        End Property
        <PropertyDescription("ServerName", "Server Name")>
        Public Property ServerName() As String
            Get
                Return _ServerName
            End Get
            Set(ByVal value As String)
                If Not CEqual(_ServerName, value) Then
                    _ServerName = value
                    DataStateChanged(EnumObjectState.Modified, Properties.ServerName.ToString)
                End If
            End Set
        End Property
        <PropertyDescription("ConnectionString", "Connection String")>
        Public Property ConnectionString() As String
            Get
                Return _ConnectionString
            End Get
            Set(ByVal value As String)
                If Not CEqual(_ConnectionString, value) Then
                    _ConnectionString = value
                    DataStateChanged(EnumObjectState.Modified, Properties.ConnectionString.ToString)
                End If
            End Set
        End Property
        <PropertyDescription("ServerPath", "Server Path")>
        Public Property ServerPath() As String
            Get
                Return _ServerPath
            End Get
            Set(ByVal value As String)
                If Not CEqual(_ServerPath, value) Then
                    _ServerPath = value
                    DataStateChanged(EnumObjectState.Modified, Properties.ServerPath.ToString)
                End If
            End Set
        End Property
        <PropertyDescription("BackupPath1", "Backup Path 1")>
        Public Property BackupPath1() As String
            Get
                Return _BackupPath1
            End Get
            Set(ByVal value As String)
                If Not CEqual(_BackupPath1, value) Then
                    _BackupPath1 = value
                    DataStateChanged(EnumObjectState.Modified, Properties.BackupPath1.ToString)
                End If
            End Set
        End Property
        <PropertyDescription("BackupPath2", "Backup Path 2")>
        Public Property BackupPath2() As String
            Get
                Return _BackupPath2
            End Get
            Set(ByVal value As String)
                If Not CEqual(_BackupPath2, value) Then
                    _BackupPath2 = value
                    DataStateChanged(EnumObjectState.Modified, Properties.BackupPath2.ToString)
                End If
            End Set
        End Property
        <PropertyDescription("ExportPath1", "Export Path 1")>
        Public Property ExportPath1() As String
            Get
                Return _ExportPath1
            End Get
            Set(ByVal value As String)
                If Not CEqual(_ExportPath1, value) Then
                    _ExportPath1 = value
                    DataStateChanged(EnumObjectState.Modified, Properties.ExportPath1.ToString)
                End If
            End Set
        End Property
        <PropertyDescription("ExportPath2", "Export Path 2")>
        Public Property ExportPath2() As String
            Get
                Return _ExportPath2
            End Get
            Set(ByVal value As String)
                If Not CEqual(_ExportPath2, value) Then
                    _ExportPath2 = value
                    DataStateChanged(EnumObjectState.Modified, Properties.ExportPath2.ToString)
                End If
            End Set
        End Property
        <PropertyDescription("ExportPath3", "Export Path 3")>
        Public Property ExportPath3() As String
            Get
                Return _ExportPath3
            End Get
            Set(ByVal value As String)
                If Not CEqual(_ExportPath3, value) Then
                    _ExportPath3 = value
                    DataStateChanged(EnumObjectState.Modified, Properties.ExportPath3.ToString)
                End If
            End Set
        End Property
        <PropertyDescriptionAttribute("Active")>
        Public Property Active() As Boolean?
            Get
                Return _Active
            End Get
            Set(ByVal value As Boolean?)
                If Not CEqual(_Active, value) Then
                    _Active = value
                    DataStateChanged(EnumObjectState.Modified, Properties.Active.ToString)
                End If
            End Set
        End Property
        <PropertyDescription("CreatedBy", "Created By")>
        Public Property CreatedBy() As String
            Get
                Return _CreatedBy
            End Get
            Set(ByVal value As String)
                If Not CEqual(_CreatedBy, value) Then
                    _CreatedBy = value
                    DataStateChanged(EnumObjectState.Modified, Properties.CreatedBy.ToString)
                End If
            End Set
        End Property
        <PropertyDescription("CreatedOn", "Created On")>
        Public Property CreatedOn() As Date?
            Get
                Return _CreatedOn
            End Get
            Set(ByVal value As Date?)
                If Not CEqual(_CreatedOn, value) Then
                    _CreatedOn = value
                    DataStateChanged(EnumObjectState.Modified, Properties.CreatedOn.ToString)
                End If
            End Set
        End Property
        <PropertyDescription("EditedBy", "Edited By")>
        Public Property EditedBy() As String
            Get
                Return _EditedBy
            End Get
            Set(ByVal value As String)
                If Not CEqual(_EditedBy, value) Then
                    _EditedBy = value
                    DataStateChanged(EnumObjectState.Modified, Properties.EditedBy.ToString)
                End If
            End Set
        End Property
        <PropertyDescription("EditedOn", "Edited On")>
        Public Property EditedOn() As Date?
            Get
                Return _EditedOn
            End Get
            Set(ByVal value As Date?)
                If Not CEqual(_EditedOn, value) Then
                    _EditedOn = value
                    DataStateChanged(EnumObjectState.Modified, Properties.EditedOn.ToString)
                End If
            End Set
        End Property
#End Region
    End Class
End Namespace
