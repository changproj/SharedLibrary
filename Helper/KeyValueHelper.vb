
Public Class KeyValueHelper

#Region "Variable"
    Private mKey As Object
    Private mValue As Object
#End Region

#Region "New"
    Public Sub New(ByVal key As Object, ByVal value As Object)
        mKey = key
        mValue = value
    End Sub
#End Region

#Region "Property"
    Public Overridable ReadOnly Property Key() As Object
        Get
            Return mKey
        End Get
    End Property
    Public Overridable ReadOnly Property Value() As Object
        Get
            Return mValue
        End Get
    End Property
#End Region

End Class


<Serializable()>
Public Class KeyValueHelperList
    Inherits List(Of KeyValueHelper)

#Region "New"
    Public Sub New()
        MyBase.New()
    End Sub
#End Region

End Class
