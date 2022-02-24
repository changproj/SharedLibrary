Imports DataAccess

Public Interface IDataEntityMgr(Of T)
    Inherits IDatabaseBuilder

#Region "Delegate"
    Delegate Sub DataEntitySavedEventHandler(target As T)
#End Region

#Region "Event"
    Event DataEntitySaved As DataEntitySavedEventHandler
#End Region

#Region "Property"
    Property ConnStr() As String
    ReadOnly Property DataEntity() As T
    ReadOnly Property DataEntityList() As List(Of T)
#End Region

#Region "Method"
    Function Fetch(ByVal dataBausinessParams As MgrArgs) As IDataEntityMgr(Of T)
    'Function FetchMaster(ByVal dataBausinessParams As MgrArgs) As IDataEntityMgr(Of T)
    Function FetchAsync(ByVal dataBausinessParams As MgrArgs) As Task(Of IDataEntityMgr(Of T))
    Function Save() As IDataEntityMgr(Of T)

#End Region

End Interface
