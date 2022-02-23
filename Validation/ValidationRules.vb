
Namespace Validation


    Public Class ValidationRules

#Region "Field"
        Private mTarget As Object
        Private mBrokenRules As BrokenRulesCollection
        Private mRulesList As Dictionary(Of String, List(Of RuleMethod))
#End Region

#Region "Constructor"
        Public Sub New(ByVal businessObject As Object)
            mTarget = businessObject
        End Sub
#End Region

#Region "Property"
        Private ReadOnly Property BrokenRulesList() As BrokenRulesCollection
            Get
                If mBrokenRules Is Nothing Then
                    mBrokenRules = New BrokenRulesCollection
                End If
                Return mBrokenRules
            End Get
        End Property
        Private ReadOnly Property RulesList() As Generic.Dictionary(Of String, List(Of RuleMethod))
            Get
                If mRulesList Is Nothing Then
                    mRulesList = New Dictionary(Of String, List(Of RuleMethod))
                End If
                Return mRulesList
            End Get
        End Property
#End Region


#Region " Adding Rules "
        Public Sub AddRule(ByVal handler As RuleHandler, ByVal propertyName As String)
            ' get the list of rules for the property
            Dim list As List(Of RuleMethod) = GetRulesForProperty(propertyName)

            ' we have the list, add our new rule
            list.Add(New RuleMethod(mTarget, handler, propertyName))
        End Sub

        Public Sub AddRule(ByVal handler As RuleHandler, ByVal args As RuleArgs)
            ' get the list of rules for the property
            Dim list As List(Of RuleMethod) = GetRulesForProperty(args.PropertyName)

            ' we have the list, add our new rule
            list.Add(New RuleMethod(mTarget, handler, args))
        End Sub
        Private Function GetRulesForProperty(ByVal propertyName As String) As List(Of RuleMethod)
            ' get the list (if any) from the dictionary
            Dim list As List(Of RuleMethod) = Nothing
            If RulesList.ContainsKey(propertyName) Then
                list = RulesList.Item(propertyName)
            End If

            If list Is Nothing Then
                ' there is no list for this name - create one
                list = New List(Of RuleMethod)
                RulesList.Add(propertyName, list)
            End If
            Return list
        End Function
#End Region

#Region " Remove Rules "
        Public Sub RemoveAllRules()
            RulesList.Clear()
        End Sub
#End Region

#Region " Checking Rules "
        Public Sub CheckRules(ByVal propertyName As String)
            Dim list As List(Of RuleMethod)
            ' get the list of rules to check
            If RulesList.ContainsKey(propertyName) Then
                list = RulesList.Item(propertyName)
                If list Is Nothing Then Exit Sub

                ' now check the rules
                For Each mRule As RuleMethod In list
                    If mRule.Invoke() Then
                        BrokenRulesList.Remove(mRule)
                    Else
                        BrokenRulesList.Add(mRule)
                    End If
                Next
            End If
        End Sub
        Public Sub CheckRules()
            ' get the rules for each rule name
            For Each mProperty As KeyValuePair(Of String, List(Of RuleMethod)) In RulesList
                ' now check the rules
                For Each rule As RuleMethod In mProperty.Value
                    If rule.Invoke() Then
                        BrokenRulesList.Remove(rule)
                    Else
                        BrokenRulesList.Add(rule)
                    End If
                Next
            Next
        End Sub
#End Region

#Region " Status retrieval "
        Public ReadOnly Property IsValid() As Boolean
            Get
                Return BrokenRulesList.Count = 0
            End Get
        End Property
        Public Function GetBrokenRules() As BrokenRulesCollection
            Return BrokenRulesList
        End Function
#End Region

    End Class

End Namespace

