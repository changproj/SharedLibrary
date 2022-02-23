Imports System.ComponentModel
Imports System.Reflection
Imports Helper
Imports Helper.ApplicationTools
Imports Validation

Namespace DataEntity
    Public MustInherit Class DataEntityBase
        Implements IDataEntity
        Implements INotifyPropertyChanged

#Region "Field"

#End Region

#Region "Event"
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
#End Region

#Region "Constructor"
        Public Sub New()
        End Sub
        Public Sub New(ByVal ParamArray vkp() As KeyValueHelper)
            Dim _field As FieldInfo
            For Each itm As KeyValueHelper In vkp
                _field = Me.GetType.BaseType.GetField("_" & itm.Key, BindingFlags.Instance Or BindingFlags.NonPublic)
                If _field IsNot Nothing Then _field.SetValue(Me, itm.Value)
            Next
            For Each itm As KeyValueHelper In vkp
                _field = Me.GetType.GetField("_" & itm.Key, BindingFlags.Instance Or BindingFlags.NonPublic)
                If _field IsNot Nothing Then _field.SetValue(Me, itm.Value)
            Next

            'ObjectState = EnumObjectState.Added
        End Sub
#End Region

#Region "Implements"
#End Region

#Region "Field/ Property Mapping"
        Enum StoreProcedures
            Empty
        End Enum
        Enum DbFields
            Empty
        End Enum
        Enum Properties
            Empty
        End Enum
#End Region

#Region "Clone"
        Public Function Clone(Of T)(originalObject As T, cloneObject As T) As T
            For Each p As System.Reflection.PropertyInfo In originalObject.GetType().GetProperties()
                If p.CanRead AndAlso cloneObject.GetType().GetProperty(p.Name).CanWrite Then
                    cloneObject.GetType().GetProperty(p.Name).SetValue(cloneObject, p.GetValue(originalObject, Nothing), Nothing)
                    'Public Sub SetValue(obj As Object, value As Object) in 4.5.2
                    'cloneObject.GetType().GetProperty(p.Name).SetValue(cloneObject, p.GetValue(originalObject, Nothing))
                End If
            Next
            Return cloneObject
        End Function
#End Region

#Region "Object State"
        Private _objectState As EnumObjectState
        Public Property ObjectState() As EnumObjectState
            Get
                Return _objectState
            End Get
            Set(ByVal value As EnumObjectState)
                _objectState = value
            End Set
        End Property
        Public ReadOnly Property IsDirty() As Boolean
            Get
                Return ObjectState <> EnumObjectState.Unchanged
            End Get
        End Property
        Protected Sub DataStateChanged(ByVal propertyName As String)
            DataStateChanged(EnumObjectState.Modified)
        End Sub
        Protected Sub DataStateChanged()
            DataStateChanged(CType(Nothing, String))
        End Sub
        Protected Sub DataStateChanged(ByVal dataState As EnumObjectState)
            DataStateChanged(dataState, Nothing)
        End Sub
        Protected Sub DataStateChanged(ByVal dataState As EnumObjectState, ByVal propertyName As String)
            If Not String.IsNullOrEmpty(propertyName) Then
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
            End If
            ' Only change the ObjectState if we're unchanged
            If Me.ObjectState = EnumObjectState.Unchanged And
                    dataState = EnumObjectState.Modified Then
                Me.ObjectState = dataState
            Else
                If dataState = EnumObjectState.Deleted Then
                    Me.ObjectState = EnumObjectState.Deleted
                End If
            End If

            Validationrules.CheckRules(propertyName)
        End Sub
#End Region

#Region "Validation"
        Private _validationRules As ValidationRules
        Protected ReadOnly Property Validationrules() As ValidationRules
            Get
                If _validationRules Is Nothing Then
                    _validationRules = New ValidationRules(Me)
                End If
                Return _validationRules
            End Get
        End Property
        Public Overridable ReadOnly Property IsValid() As Boolean
            Get
                Return Validationrules.IsValid
            End Get
        End Property
        Public Overridable ReadOnly Property BrokenRulesCollection() As BrokenRulesCollection
            Get
                Return Validationrules.GetBrokenRules
            End Get
        End Property
        Public MustOverride Sub AddBusinessRules(ByVal prop As String, ByVal func As RuleHandler)
        Public MustOverride Sub AddBusinessRules(ByVal args As RuleArgs, ByVal func As RuleHandler)
        Public MustOverride Sub AddBusinessRules(ByVal funcList As Dictionary(Of String, RuleHandler))
        Public MustOverride Sub RemoveAllRules()
        Public MustOverride Sub CheckRules(ByVal propertyName As String)
        Public MustOverride Sub CheckAllRules()
#End Region

#Region "Run Time Error"
        Protected _errorMsg As String                 '+
        Public Property ErrorMsg() As String
            Get
                Return _errorMsg
            End Get
            Set(ByVal value As String)
                If Not CEqual(_errorMsg, value) Then
                    _errorMsg = value
                    DataStateChanged(EnumObjectState.Modified, "ErrorMsg")
                End If
            End Set
        End Property
        Public ReadOnly Property HasError() As Boolean
            Get
                Return SafeString(_errorMsg, String.Empty).Trim.Length > 0
            End Get
        End Property
#End Region
    End Class

#Region "Descriptive Property Name"
    Public Class PropertyDescriptionAttribute
        Inherits Attribute

        Private _key As String
        Private _value As String

        Public Sub New(ByVal key As String, Optional ByVal value As String = Nothing)
            _key = key

            If value Is Nothing Then
                _value = key
            Else
                _value = value
            End If
        End Sub
        Public Property Key() As String
            Get
                Return _key
            End Get
            Set(value As String)
                _key = value
            End Set
        End Property
        Public Property Value() As String
            Get
                Return _value
            End Get
            Set(value As String)
                _value = value
            End Set
        End Property
    End Class
#End Region

End Namespace

