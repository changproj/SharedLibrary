Imports Helper.ApplicationTools

Public Class FieldHelper
    Inherits KeyValueHelper

#Region "New"
    Public Sub New(ByVal key As Object, ByVal value As Object)
        MyBase.New(key.ToString, value.ToString)
    End Sub
#End Region

#Region "Property"
    Public ReadOnly Property ToText(Optional ByVal bracketType As BracketType = BracketType.Curve) As String
        Get
            If Key <> "" Then
                Return CBracket(Key + "." + Value, bracketType)
            Else
                Return CBracket(Value, bracketType)
            End If
        End Get
    End Property
#End Region

End Class
