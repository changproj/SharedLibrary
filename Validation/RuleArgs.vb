
Namespace Validation


    Public Class RuleArgs
        Private _propertyName As String
        Private _description As String

#Region "Constructor"
        Public Sub New(ByVal propertyName As String)
            _propertyName = propertyName
        End Sub
#End Region

#Region "Property"
        Public ReadOnly Property PropertyName() As String
            Get
                Return _propertyName
            End Get
        End Property
        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal Value As String)
                _description = Value
            End Set
        End Property
#End Region

#Region "Method"
        Public Overrides Function ToString() As String
            Return _propertyName
        End Function
#End Region
    End Class

End Namespace