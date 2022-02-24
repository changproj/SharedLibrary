Imports System.ComponentModel
Imports System.Reflection
Imports DataAccess
Imports Validation

Public MustInherit Class DataEntityMgrBase(Of T)
    Implements IDataEntityMgr(Of T)

#Region "Field"
    Private _connstr As String = String.Empty
    Protected _dataEntity As T
    Protected _dataEntityList As List(Of T)
    Protected _databaseBuilder As IDatabaseBuilder
#End Region

#Region "Event"
    Public Delegate Sub DataEntitySavedEventHandler(target As T)
    Public Event DataEntitySaved As IDataEntityMgr(Of T).DataEntitySavedEventHandler Implements IDataEntityMgr(Of T).DataEntitySaved
    Public WithEvents _backgroundWorker As BackgroundWorker
#End Region

#Region "Constructor"
    Public Sub New(ByVal connstr As String, ByVal dataEntity As T)
        _connstr = connstr
        _dataEntity = dataEntity
        _dataEntityList = New List(Of T)
        '_databaseBuilder = New SqlClientBuilder(connstr) 'assign New SqlClientBuilder(connstr) in children
    End Sub
#End Region

#Region "Property"
    Public Property ConnStr() As String Implements IDataEntityMgr(Of T).ConnStr
        Get
            Return _connstr
        End Get
        Set(ByVal value As String)
            _connstr = value
        End Set
    End Property

    Public ReadOnly Property DataEntity() As T Implements IDataEntityMgr(Of T).DataEntity
        Get
            Return _dataEntity
        End Get
    End Property
    Public ReadOnly Property DataEntityList() As List(Of T) Implements IDataEntityMgr(Of T).DataEntityList
        Get
            Return _dataEntityList
        End Get
    End Property
    Public ReadOnly Property BackgroundWorker() As BackgroundWorker
        Get
            If _backgroundWorker Is Nothing Then
                _backgroundWorker = New BackgroundWorker
                _backgroundWorker.WorkerSupportsCancellation = True
                _backgroundWorker.WorkerReportsProgress = True
            End If
            Return _backgroundWorker
        End Get
    End Property
    Public Property DataBaseBuilder() As DatabaseBuilder
        Get
            Return _databaseBuilder
        End Get
        Set(ByVal value As DatabaseBuilder)
            _databaseBuilder = value
        End Set
    End Property
#End Region

#Region "Method"
    Protected Sub SetPropertyValue(ByVal ParamArray vkp() As KeyValueHelper)
        Dim _field As FieldInfo
        For Each itm As KeyValueHelper In vkp
            _field = DataEntity.GetType.GetField("_" & itm.Key, BindingFlags.Instance Or BindingFlags.NonPublic)
            If _field IsNot Nothing Then _field.SetValue(DataEntity, itm.Value)
        Next
    End Sub

    Public Function SetDataEntity(ByVal dataEntity As T) As IDataEntityMgr(Of T)
        _dataEntity = dataEntity
        Return Me
    End Function
    Public Function SetDataEntityList(ByVal dataEntityList As List(Of T)) As IDataEntityMgr(Of T)
        _dataEntityList = dataEntityList
        Return Me
    End Function

#End Region

#Region "MustOverride"
    Public MustOverride Sub Add()
    Public MustOverride Sub Update()
    Public MustOverride Sub Delete()
    Public MustOverride Function Fetch(dataBusinessParams As MgrArgs) As IDataEntityMgr(Of T) Implements IDataEntityMgr(Of T).Fetch
    Public MustOverride Sub AddBusinessRules(Optional ByVal args As Dictionary(Of String, RuleArgs) = Nothing)
    Public MustOverride Sub EngageRules(Optional ByVal args As Dictionary(Of String, RuleArgs) = Nothing)
#End Region

#Region "Overridable"
    Public Overridable Function Save() As IDataEntityMgr(Of T) Implements IDataEntityMgr(Of T).Save
        RaiseEvent DataEntitySaved(DataEntity)
        Return Nothing
    End Function
    Public Overridable Function FetchAsync(dataBausinessParams As MgrArgs) As Task(Of IDataEntityMgr(Of T)) Implements IDataEntityMgr(Of T).FetchAsync
        Return Nothing
    End Function
    Public Overridable Sub AddAsync()
    End Sub
    Public Overridable Sub UpdateAsync()
    End Sub
    Public Overridable Sub DeleteAsync()
    End Sub
    'Public Overridable Function FetchMaster(dataBusinessParams As MgrArgs) As IDataEntityMgr(Of T) Implements IDataEntityMgr(Of T).FetchMaster
    '    Return Nothing
    'End Function
#End Region



End Class
