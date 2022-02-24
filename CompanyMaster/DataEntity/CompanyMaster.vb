Imports Helper
Imports Helper.ApplicationTools
Imports Helper.DataEntity
Imports Validation
Namespace DataEntity

    Public Class CompanyMaster
        Inherits DataEntityBase

        Private _ID As String
        Private _Name As String
        Private _RegNo As String
        Private _Branch As String
        Private _Address As String
        Private _City As String
        Private _Postcode As String
        Private _State As String
        Private _Country As String
        Private _Tel As String
        Private _Fax As String
        Private _Email As String
        Private _CoGroup As String
        Private _Level As Integer?
        Private _HasChildrenOf As Integer?
        Private _Active As Boolean?
        Private _CreatedBy As String
        Private _CreatedOn As Date?
        Private _EditedBy As String
        Private _EditedOn As Date?
        Private _GSTNo As String
        Private _SalesTaxNo As String
        Private _ServiceTaxNo As String
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
        Private _IsSysActive As Boolean?

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
            spCompanyMaster_Get
            spCompanyMaster_Insert
            spCompanyMaster_Update
            spCompanyMaster_Delete
        End Enum
        Shadows Enum DbFields
            Co_ID               'T Company, CompanySystem, CompanyTax
            Co_Name             'T Company
            Co_RegNo            'T Company
            Co_Branch           'T Company
            Co_Address          'T Company
            Co_City             'T Company
            Co_Postcode         'T Company
            Co_State            'T Company
            Co_Country          'T Company
            Co_Tel              'T Company
            Co_Fax              'T Company
            Co_Email            'T Company
            Co_CoGroup          'T Company
            Co_Level
            Co_HasChildrenOf
            Co_Active           'T Company
            Co_CreatedBy        'T Company
            Co_CreatedOn        'T Company
            Co_EditedBy         'T Company
            Co_EditedOn         'T Company
            Co_GSTNo            'T CompanyTax
            Co_SalesTaxNo       'T CompanyTax
            Co_ServiceTaxNo     'T CompanyTax
            Co_DbName           'T CompanySystem
            Co_ServerName       'T CompanySystem
            Co_ConnectionString 'T CompanySystem
            Co_ServerPath       'T CompanySystem
            Co_BackupPath1      'T CompanySystem
            Co_BackupPath2      'T CompanySystem
            Co_ExportPath1      'T CompanySystem
            Co_ExportPath2      'T CompanySystem
            Co_ExportPath3      'T CompanySystem
            Co_IsSysActive      'T CompanySystem
            Co_SysID            'T CompanySystem
        End Enum
        Shadows Enum Properties
            ID                  'T Company, CompanySystem, CompanyTax
            Name                'T Company
            RegNo               'T Company
            Branch              'T Company
            Address             'T Company
            City                'T Company
            Postcode            'T Company
            State               'T Company
            Country             'T Company
            Tel                 'T Company
            Fax                 'T Company
            Email               'T Company
            CoGroup             'T Company
            Level
            HasChildrenOf
            Active              'T Company
            CreatedBy           'T Company
            CreatedOn           'T Company
            EditedBy            'T Company
            EditedOn            'T Company
            GSTNo               'T CompanyTax
            SalesTaxNo          'T CompanyTax
            ServiceTaxNo        'T CompanyTax
            SysID               'used as a @parameter in Mgr (no DbFields of this) 
            DbName              'T CompanySystem
            ServerName          'T CompanySystem
            ConnectionString    'T CompanySystem
            ServerPath          'T CompanySystem
            BackupPath1         'T CompanySystem
            BackupPath2         'T CompanySystem
            ExportPath1         'T CompanySystem
            ExportPath2         'T CompanySystem
            ExportPath3         'T CompanySystem
            IsSysActive         'T CompanySystem
        End Enum
#End Region

#Region "Property"
        <PropertyDescription("ID")>
        Public Property ID() As String
            Get
                Return _ID
            End Get
            Set(ByVal value As String)
                If Not CEqual(_ID, value) Then
                    _ID = value
                    DataStateChanged(EnumObjectState.Modified, Properties.ID.ToString)
                End If
            End Set
        End Property
        <PropertyDescription("Name")>
        Public Property Name() As String
            Get
                Return _Name
            End Get
            Set(ByVal value As String)
                If Not CEqual(_Name, value) Then
                    _Name = value
                    DataStateChanged(EnumObjectState.Modified, Properties.Name.ToString)
                End If
            End Set
        End Property
        <PropertyDescription("RegNo", "Reg No.")>
        Public Property RegNo() As String
            Get
                Return _RegNo
            End Get
            Set(ByVal value As String)
                If Not CEqual(_RegNo, value) Then
                    _RegNo = value
                    DataStateChanged(EnumObjectState.Modified, Properties.RegNo.ToString)
                End If
            End Set
        End Property
        <PropertyDescription("Branch")>
        Public Property Branch() As String
            Get
                Return _Branch
            End Get
            Set(ByVal value As String)
                If Not CEqual(_Branch, value) Then
                    _Branch = value
                    DataStateChanged(EnumObjectState.Modified, Properties.Branch.ToString)
                End If
            End Set
        End Property
        <PropertyDescription("Address")>
        Public Property Address() As String
            Get
                Return _Address
            End Get
            Set(ByVal value As String)
                If Not CEqual(_Address, value) Then
                    _Address = value
                    DataStateChanged(EnumObjectState.Modified, Properties.Address.ToString)
                End If
            End Set
        End Property
        <PropertyDescription("City")>
        Public Property City() As String
            Get
                Return _City
            End Get
            Set(ByVal value As String)
                If Not CEqual(_City, value) Then
                    _City = value
                    DataStateChanged(EnumObjectState.Modified, Properties.City.ToString)
                End If
            End Set
        End Property
        <PropertyDescription("Postcode", "Post Code")>
        Public Property Postcode() As String
            Get
                Return _Postcode
            End Get
            Set(ByVal value As String)
                If Not CEqual(_Postcode, value) Then
                    _Postcode = value
                    DataStateChanged(EnumObjectState.Modified, Properties.Postcode.ToString)
                End If
            End Set
        End Property
        <PropertyDescription("State")>
        Public Property State() As String
            Get
                Return _State
            End Get
            Set(ByVal value As String)
                If Not CEqual(_State, value) Then
                    _State = value
                    DataStateChanged(EnumObjectState.Modified, Properties.State.ToString)
                End If
            End Set
        End Property
        <PropertyDescription("Country")>
        Public Property Country() As String
            Get
                Return _Country
            End Get
            Set(ByVal value As String)
                If Not CEqual(_Country, value) Then
                    _Country = value
                    DataStateChanged(EnumObjectState.Modified, Properties.Country.ToString)
                End If
            End Set
        End Property
        <PropertyDescription("Telephone")>
        Public Property Tel() As String
            Get
                Return _Tel
            End Get
            Set(ByVal value As String)
                If Not CEqual(_Tel, value) Then
                    _Tel = value
                    DataStateChanged(EnumObjectState.Modified, Properties.Tel.ToString)
                End If
            End Set
        End Property
        <PropertyDescription("Fax")>
        Public Property Fax() As String
            Get
                Return _Fax
            End Get
            Set(ByVal value As String)
                If Not CEqual(_Fax, value) Then
                    _Fax = value
                    DataStateChanged(EnumObjectState.Modified, Properties.Fax.ToString)
                End If
            End Set
        End Property
        <PropertyDescription("Email")>
        Public Property Email() As String
            Get
                Return _Email
            End Get
            Set(ByVal value As String)
                If Not CEqual(_Email, value) Then
                    _Email = value
                    DataStateChanged(EnumObjectState.Modified, Properties.Email.ToString)
                End If
            End Set
        End Property
        <PropertyDescription("CoGroup", "Co. Group")>
        Public Property CoGroup() As String
            Get
                Return _CoGroup
            End Get
            Set(ByVal value As String)
                If Not CEqual(_CoGroup, value) Then
                    _CoGroup = value
                    DataStateChanged(EnumObjectState.Modified, Properties.CoGroup.ToString)
                End If
            End Set
        End Property
        <PropertyDescription("Level")>
        Public Property Level() As Integer?
            Get
                Return _Level
            End Get
            Set(ByVal value As Integer?)
                If Not CEqual(_Level, value) Then
                    _Level = value
                    DataStateChanged(EnumObjectState.Modified, Properties.Level.ToString)
                End If
            End Set
        End Property
        <PropertyDescription("HasChildrenOf", "Has Children Of")>
        Public Property HasChildrenOf() As Integer?
            Get
                Return _HasChildrenOf
            End Get
            Set(ByVal value As Integer?)
                If Not CEqual(_HasChildrenOf, value) Then
                    _HasChildrenOf = value
                    DataStateChanged(EnumObjectState.Modified, Properties.HasChildrenOf.ToString)
                End If
            End Set
        End Property
        <PropertyDescription("Active")>
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
        <PropertyDescription("GSTNo", "GST No.")>
        Public Property GSTNo() As String
            Get
                Return _GSTNo
            End Get
            Set(ByVal value As String)
                If Not CEqual(_GSTNo, value) Then
                    _GSTNo = value
                    DataStateChanged(EnumObjectState.Modified, Properties.GSTNo.ToString)
                End If
            End Set
        End Property
        <PropertyDescription("SalesTaxNo", "Sales Tax No.")>
        Public Property SalesTaxNo() As String
            Get
                Return _SalesTaxNo
            End Get
            Set(ByVal value As String)
                If Not CEqual(_SalesTaxNo, value) Then
                    _SalesTaxNo = value
                    DataStateChanged(EnumObjectState.Modified, Properties.SalesTaxNo.ToString)
                End If
            End Set
        End Property
        <PropertyDescription("ServiceTaxNo", "Service Tax No.")>
        Public Property ServiceTaxNo() As String
            Get
                Return _ServiceTaxNo
            End Get
            Set(ByVal value As String)
                If Not CEqual(_ServiceTaxNo, value) Then
                    _ServiceTaxNo = value
                    DataStateChanged(EnumObjectState.Modified, Properties.ServiceTaxNo.ToString)
                End If
            End Set
        End Property
        <PropertyDescription("SysID")>
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
        <PropertyDescription("BackupPath1")>
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
        <PropertyDescription("BackupPath2")>
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
        <PropertyDescription("ExportPath1")>
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
        <PropertyDescription("ExportPath2")>
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
        <PropertyDescription("ExportPath3")>
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
        <PropertyDescription("IsSysActive")>
        Public Property IsSysActive() As Boolean?
            Get
                Return _IsSysActive
            End Get
            Set(ByVal value As Boolean?)
                If Not CEqual(_IsSysActive, value) Then
                    _IsSysActive = value
                    DataStateChanged(EnumObjectState.Modified, Properties.IsSysActive.ToString)
                End If
            End Set
        End Property
#End Region
    End Class
End Namespace

