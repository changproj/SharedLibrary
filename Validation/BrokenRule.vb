Namespace Validation



    Public Class BrokenRule

#Region "Field"
        Private _ruleName As String
        Private _description As String
        Private _property As String
#End Region

#Region "Constructor"
        Friend Sub New(ByVal rule As RuleMethod)
            _ruleName = rule.RuleName
            _description = rule.RuleArgs.Description
            _property = rule.RuleArgs.PropertyName
        End Sub
#End Region

#Region "Property"
        Public ReadOnly Property RuleName() As String
            Get
                Return _ruleName
            End Get
        End Property
        Public ReadOnly Property Description() As String
            Get
                Return _description
            End Get
        End Property
        Public ReadOnly Property [Property]() As String
            Get
                Return _property
            End Get
        End Property
#End Region
    End Class


End Namespace