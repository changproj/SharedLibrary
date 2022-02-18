Imports Helper.ApplicationTools


Public Class TextBuilder

#Region "Variable"
    Protected _AppendCount As Integer
    Protected _Default, _Selection As String
    Protected _BuildList As New List(Of String)
#End Region

#Region "New"
    Public Sub New(ByVal defaultText As String)
        _Default = CEmpty(defaultText)
    End Sub
#End Region

#Region "Property"
    Public Overridable ReadOnly Property BuildList() As List(Of String)
        Get
            Return _BuildList
        End Get
    End Property
    Public ReadOnly Property ToText() As String
        Get
            Return GetText()
        End Get
    End Property
#End Region

#Region "Function"
    Protected Overridable Function GetText() As String
        If _BuildList.Count = 0 Then Return ""
        _Selection = _Default
        For Each str As String In _BuildList
            _Selection += str
        Next
        Return _Selection
    End Function
#End Region

#Region "Builder"
    Public Sub Clear()
        BuildList.Clear()
    End Sub
    Public Overridable Sub Append(ByVal ParamArray text() As String)
        For Each str As String In text
            _BuildList.Add(str.Trim)
        Next
    End Sub
#End Region

End Class

Public Class FilterBuilder
    Inherits TextBuilder

#Region "New"
    Public Sub New()
        MyBase.New("Filter By |")
    End Sub
#End Region

#Region "Function"
    Protected Overrides Function GetText() As String
        If _BuildList.Count = 0 Then Return ""
        _Selection = _Default
        _AppendCount = 0
        For Each str As String In _BuildList
            _Selection += str + IIf(_AppendCount = _BuildList.Count - 1, "", "| ")
            _AppendCount += 1
        Next
        Return CBracket(_Selection)
    End Function
#End Region

End Class

Public Class SelectionBuilder
    Inherits TextBuilder

#Region "Variable"
    Shadows _BuildList As New List(Of SelectionHelper)
#End Region

#Region "New"
    Public Sub New()
        MyBase.New("(1=1)")
    End Sub
#End Region

#Region "Property"
    Public Shadows ReadOnly Property BuildList() As List(Of SelectionHelper)
        Get
            Return _BuildList
        End Get
    End Property
#End Region

#Region "Function"
    Protected Overrides Function GetText() As String
        _Selection = _Default
        For Each selHelper As SelectionHelper In _BuildList
            _Selection += selHelper.ToText
        Next
        Return _Selection
    End Function
#End Region

#Region "Builder"
    Public Shadows Sub Append(ByVal ParamArray vkp() As SelectionHelper)
        _AppendCount = 0
        For Each selHelper As SelectionHelper In vkp
            With selHelper
                If _AppendCount = 0 Then .FirstSelection = True
                If _AppendCount = vkp.Length - 1 Then .LastSelection = True
            End With
            _BuildList.Add(selHelper)
            _AppendCount += 1
        Next
    End Sub
#End Region

End Class

Public Class SelectionHelper
    Inherits KeyValueHelper

#Region "Variable"
    Private _First, _Last As Boolean
#End Region

#Region "New"
    Public Sub New(ByVal vkp As CompareHelper, Optional ByVal selectType As SelectionType = SelectionType.And)
        MyBase.New(selectType, vkp) 'And    F1 = V1
    End Sub
    Public Sub New(ByVal tableName As Object, ByVal fieldName As Object, ByVal fieldValue As String,
    Optional ByVal compareType As FilterType = FilterType.Equal, Optional ByVal selectType As SelectionType = SelectionType.And,
    Optional ByVal ToShortDate As Boolean = False)
        MyBase.New(selectType, New CompareHelper(New FieldHelper(tableName, fieldName).ToText, compareType, fieldValue, ToShortDate))
    End Sub
#End Region

#Region "Property"
    Public Shadows ReadOnly Property Key() As SelectionType
        Get
            Return MyBase.Key
        End Get
    End Property
    Public Shadows ReadOnly Property Value() As CompareHelper
        Get
            Return MyBase.Value
        End Get
    End Property
    Public ReadOnly Property ToText() As String
        Get
            Return GetSelectionText()
        End Get
    End Property
    Public Property FirstSelection() As Boolean
        Get
            Return _First
        End Get
        Set(ByVal value As Boolean)
            _First = value
        End Set
    End Property
    Public Property LastSelection() As Boolean
        Get
            Return _Last
        End Get
        Set(ByVal value As Boolean)
            _Last = value
        End Set
    End Property
#End Region

#Region "Function"
    Public Function GetSelectionText() As String
        Dim str As String = ""
        str += CSpace(Key.ToString)
        str += CBracket(Value.ToText, , Not FirstSelection, Not LastSelection)
        If LastSelection Then str += vbNewLine
        Return str
    End Function
#End Region

End Class