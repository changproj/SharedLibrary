Imports Helper
Imports Helper.ApplicationTools
Imports Helper.DataEntity
Imports Validation


Public Class AppSystem
        Inherits DataEntityBase

        Private _ID As String
        Private _Name As String
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
            spAppSystem_Get
            spAppSystem_Insert
            spAppSystem_Update
            spAppSystem_Delete
        End Enum
        Shadows Enum DbFields
            Sy_ID               'T 
            Sy_Name             'T 
            Sy_CreatedBy        'T 
            Sy_CreatedOn        'T 
            Sy_EditedBy         'T 
            Sy_EditedOn         'T 
        End Enum
        Shadows Enum Properties
            ID                  'T 
            Name                'T 
            CreatedBy           'T 
            CreatedOn           'T 
            EditedBy            'T 
            EditedOn            'T                            
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

