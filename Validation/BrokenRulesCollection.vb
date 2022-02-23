Namespace Validation


    Public Class BrokenRulesCollection
        Inherits ReadOnlyBindingList(Of BrokenRule)

#Region "Field"
        Friend Sub New()
            ' limit creation to this assembly
        End Sub
#End Region

#Region "Method"
        Friend Overloads Sub Add(ByVal rule As RuleMethod)
            Remove(rule)
            IsReadOnly = False
            Add(New BrokenRule(rule))
            IsReadOnly = True
        End Sub

        Friend Overloads Sub Remove(ByVal rule As RuleMethod)
            IsReadOnly = False
            For index As Integer = 0 To Count - 1
                If Me(index).RuleName = rule.RuleName Then
                    RemoveAt(index)
                    Exit For
                End If
            Next
            IsReadOnly = True
        End Sub
        Public Function GetFirstBrokenRule(ByVal [property] As String) As BrokenRule
            For Each item As BrokenRule In Me
                If item.Property = [property] Then
                    Return item
                End If
            Next
            Return Nothing
        End Function
        Public Overrides Function ToString() As String
            Dim mResult As New System.Text.StringBuilder()
            Dim mItem As BrokenRule
            Dim mFirst As Boolean = True

            For Each mItem In Me
                If mFirst Then
                    mFirst = False
                Else
                    mResult.Append(Environment.NewLine)
                End If
                mResult.Append(mItem.Description)
            Next
            Return mResult.ToString
        End Function
#End Region

    End Class

End Namespace