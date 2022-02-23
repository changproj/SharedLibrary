Namespace Validation

    Public Class ValidationException
        Inherits Exception

#Region "Constructor"
        Public Sub New(ByVal message As String)
            MyBase.New(message)
        End Sub
        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context)
        End Sub
#End Region

#Region "Property"
        Public Sub New(ByVal message As String, ByVal innerException As Exception)
            MyBase.New(message, innerException)
        End Sub
#End Region

    End Class

End Namespace
