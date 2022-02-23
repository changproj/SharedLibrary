Namespace Validation



    Public MustInherit Class ReadOnlyBindingList(Of C)
        Inherits ComponentModel.BindingList(Of C)

#Region "Field"
        Private _isReadOnly As Boolean = True
#End Region

#Region "Constructor"
        Protected Sub New()
            AllowEdit = False
            AllowRemove = False
            AllowNew = False
        End Sub
#End Region

#Region "Property"
        Public Property IsReadOnly() As Boolean
            Get
                Return _isReadOnly
            End Get
            Protected Set(ByVal value As Boolean)
                _isReadOnly = value
            End Set
        End Property
#End Region

#Region "Method"
        Protected Overrides Sub ClearItems()
            If Not IsReadOnly Then
                Dim mOldValue As Boolean = AllowRemove
                AllowRemove = True
                MyBase.ClearItems()
                AllowRemove = mOldValue
            Else
                Throw New NotSupportedException("Clear is an invalid operation")
            End If
        End Sub
        Protected Overrides Function AddNewCore() As Object
            If Not IsReadOnly Then
                Return MyBase.AddNewCore()
            Else
                Throw New NotSupportedException("Insert is an invalid operation")
            End If
        End Function
        Protected Overrides Sub InsertItem(ByVal index As Integer, ByVal item As C)
            If Not IsReadOnly Then
                MyBase.InsertItem(index, item)
            Else
                Throw New NotSupportedException("Insert is an invalid operation")
            End If
        End Sub
        Protected Overrides Sub RemoveItem(ByVal index As Integer)
            If Not IsReadOnly Then
                Dim mOldValue As Boolean = AllowRemove
                AllowRemove = True
                MyBase.RemoveItem(index)
                AllowRemove = mOldValue
            Else
                Throw New NotSupportedException("Remove is an invalid operation")
            End If
        End Sub
        Protected Overrides Sub SetItem(ByVal index As Integer, ByVal item As C)
            If Not IsReadOnly Then
                MyBase.SetItem(index, item)
            Else
                Throw New NotSupportedException("Changing an element is an invalid operation")
            End If
        End Sub
#End Region

    End Class

End Namespace
