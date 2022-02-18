Imports Helper.ApplicationTools

Public Class CompareHelper
    Inherits KeyValueHelper

#Region "Variable"
    Private mCompareType As FilterType
    Private mToShortDate As Boolean
#End Region

#Region "New"
    Public Sub New(ByVal key As String,
                    ByVal compareType As FilterType,
                    ByVal value As String,
                    Optional ByVal ToShortDate As Boolean = False)
        MyBase.New(key, value)
        mCompareType = compareType
        mToShortDate = ToShortDate
    End Sub
#End Region

#Region "Property"
    Public ReadOnly Property CompareType() As FilterType
        Get
            Return mCompareType
        End Get
    End Property
    Public Shadows ReadOnly Property Key() As String
        Get
            Return MyBase.Key
        End Get
    End Property
    Public Shadows ReadOnly Property Value() As String
        Get
            Return MyBase.Value
        End Get
    End Property
    Public ReadOnly Property CompareText() As String
        Get
            Return GetCompareText()
        End Get
    End Property
    Public ReadOnly Property ToText() As String
        Get
            Return GetSelectionText()
        End Get
    End Property
#End Region

#Region "Function"
    Public Function GetSelectionText() As String
        Dim str As String = ""
        Dim mKey As String = Key
        If mToShortDate Then mKey = "Date" + CBracket(mKey)
        str += mKey
        str += GetCompareText()
        str += Value
        Return str
    End Function
    Private Function GetCompareText() As String
        Dim str As String = ""
        Select Case mCompareType
            Case FilterType.Equal
                str = "="
            Case FilterType.NotEqual
                str = "<>"
            Case FilterType.Greater
                str = ">"
            Case FilterType.GreaterOrEqual
                str = ">="
            Case FilterType.Smaller
                str = "<"
            Case FilterType.SmallerOrEqual
                str = "<="
            Case FilterType.Like, FilterType.In
                str = mCompareType.ToString
            Case FilterType.Nil
                str = String.Empty
        End Select
        Return CSpace(str)
    End Function
#End Region

End Class
