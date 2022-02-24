Imports Helper
Imports Helper.ApplicationTools
Imports Helper.DataEntity
Imports Validation
Namespace DataEntity

    Public Class CompanyTax
        Inherits DataEntityBase

        Private _CoID As String
        Private _GSTNo As String
        Private _SalesTaxNo As String
        Private _ServiceTaxNo As String
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
            spCompanyTax_Get
            spCompanyTax_Insert
            spCompanyTax_Update
            spCompanyTax_Delete
        End Enum
        Shadows Enum DbFields
            Ct_CoID                 'T 
            Ct_GSTNo                'T 
            Ct_SalesTaxNo           'T 
            Ct_ServiceTaxNo         'T 
            Ct_CreatedBy            'T 
            Ct_CreatedOn            'T 
            Ct_EditedBy             'T 
            Ct_EditedOn             'T 
        End Enum
        Shadows Enum Properties
            CoID                    'T 
            GSTNo                   'T 
            SalesTaxNo              'T 
            ServiceTaxNo            'T 
            CreatedBy               'T 
            CreatedOn               'T 
            EditedBy                'T 
            EditedOn                'T                            
        End Enum
#End Region

#Region "Property"
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
