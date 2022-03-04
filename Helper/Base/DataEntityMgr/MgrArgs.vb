Imports Helper
Imports Helper.ApplicationTools

Public Class MgrArgs
    Private _optionList As OptionList
    Private _orderByList As OrderByList

    Public Sub New(Optional optionList As OptionList = Nothing, Optional orderByList As OrderByList = Nothing)
        _optionList = optionList
        _orderByList = orderByList
    End Sub

    Public Property OptionList() As OptionList
        Get
            Return Me._optionList
        End Get
        Set(ByVal value As OptionList)
            Me._optionList = value
        End Set
    End Property

    Public Property OrderbyList() As OrderByList
        Get
            Return Me._orderByList
        End Get
        Set(ByVal value As OrderByList)
            Me._orderByList = value
        End Set
    End Property

    Public Function GetOrderBy() As String
        Dim result As String = Nothing
        If _orderByList Is Nothing Then Return result
        For Each item As KeyValueHelper In _orderByList.OrderByList
            If result IsNot Nothing Then result += ","
            result += String.Format("{0} {1}", item.Key, item.Value)
        Next
        Return $"Order By {result}"
    End Function
End Class

Public Class OrderByList
    Private _orderByList As List(Of KeyValueHelper)

    Public Sub New()
        _orderByList = New List(Of KeyValueHelper)
    End Sub

    Public Sub Add(orderByItem As KeyValueHelper)
        _orderByList.Add(orderByItem)
    End Sub

    Public Property OrderByList() As List(Of KeyValueHelper)
        Get
            Return Me._orderByList
        End Get
        Set(ByVal value As List(Of KeyValueHelper))
            Me._orderByList = value
        End Set
    End Property
End Class

Public Class OptionList
    Private _isLike As enumIsLike
    Private _isDistinct As Boolean = False
    Private _SelBuilder As SelectionBuilder


    Public Sub New(Optional isLike As enumIsLike = enumIsLike.like)
        _isLike = isLike
        _SelBuilder = New SelectionBuilder
    End Sub
    Public Property IsLike() As enumIsLike
        Get
            Return Me._isLike
        End Get
        Set(ByVal value As enumIsLike)
            Me._isLike = value
        End Set
    End Property
    Public Property IsDistinct() As Boolean
        Get
            Return Me._isDistinct
        End Get
        Set(ByVal value As Boolean)
            Me._isDistinct = value
        End Set
    End Property
    Public ReadOnly Property SelectionText(ByVal Optional selectionType As SelectionType = SelectionType.And) As String
        Get
            Dim mSelection As String = String.Empty
            Select Case selectionType
                Case SelectionType.And
                    mSelection = " And"
                Case SelectionType.Or
                    mSelection = " Or"
                Case SelectionType.Nil
            End Select
            Return $"{mSelection}{If(selectionType <> SelectionType.Nil, Space(1), String.Empty)}{_SelBuilder.ToText}"
        End Get
    End Property
    Public Sub AddSelection(ByVal ParamArray vkp() As SelectionHelper)
        _SelBuilder.Append(vkp)
    End Sub
    Public Sub AddSelection(ByVal vkpList As List(Of SelectionHelper))
        _SelBuilder.Append(vkpList.ToArray)
    End Sub
End Class
