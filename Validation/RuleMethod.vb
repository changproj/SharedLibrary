Namespace Validation


    Friend Class RuleMethod

#Region "Field"
        Private _target As Object
        Private _handler As RuleHandler
        Private _ruleName As String = ""
        Private _args As RuleArgs
#End Region

#Region "Constructor"
        Public Sub New(ByVal target As Object, ByVal handler As RuleHandler, ByVal propertyName As String)
            _target = target
            _handler = handler
            _args = New RuleArgs(propertyName)
            _ruleName = _handler.Method.Name & "!" & _args.ToString
        End Sub
        Public Sub New(ByVal target As Object, ByVal handler As RuleHandler, ByVal args As RuleArgs)
            _target = target
            _handler = handler
            _args = args
            _ruleName = _handler.Method.Name & "!" & _args.ToString
        End Sub
#End Region

#Region "Property"
        Public ReadOnly Property Handler() As RuleHandler
            Get
                Return _handler
            End Get
        End Property
        Public ReadOnly Property RuleName() As String
            Get
                Return _ruleName
            End Get
        End Property
        Public ReadOnly Property RuleArgs() As RuleArgs
            Get
                Return _args
            End Get
        End Property
#End Region

#Region "Method"
        Public Function Invoke() As Boolean
            Return _handler.Invoke(_target, _args)
        End Function
        Public Overrides Function ToString() As String
            Return _ruleName
        End Function
#End Region

    End Class

End Namespace