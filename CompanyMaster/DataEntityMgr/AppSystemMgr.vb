Imports Helper
Imports Helper.ApplicationTools
Imports Validation

Namespace DataEntityMgr
    Public Class AppSystemMgr
        Inherits DataEntityMgrBase(Of AppSystem)

#Region "Enum"
        Public Enum Rules
            ID_StringRequired
            ID_IsExist
            Name_StringRequired
        End Enum
#End Region

#Region "Field"
        Private _includeCompanyList As New List(Of String)
#End Region

#Region "Constructor"
        Public Sub New(ByVal connstr As String)
            MyBase.New(connstr, New AppSystem)
        End Sub
        Public Sub New(ByVal connstr As String, ByVal appSystem As AppSystem)
            MyBase.New(connstr, appSystem)
        End Sub
        Public Sub New(ByVal connstr As String, ByVal ParamArray vkp() As KeyValueHelper)
            Me.New(connstr)
            SetPropertyValue(vkp)
        End Sub
#End Region

#Region "RuleHandler"
        Public Overrides Sub EngageRules(Optional ByVal args As Dictionary(Of String, RuleArgs) = Nothing)
            DataEntity.RemoveAllRules()
            AddBusinessRules(args)
        End Sub
        Public Overrides Sub AddBusinessRules(Optional ByVal args As Dictionary(Of String, RuleArgs) = Nothing)
            DataEntity.AddBusinessRules(AppSystem.Properties.ID.ToString, AddressOf Validation.CommonRules.StringRequired)
            DataEntity.AddBusinessRules(AppSystem.Properties.ID.ToString, AddressOf IsExist)
            DataEntity.AddBusinessRules(AppSystem.Properties.Name.ToString, AddressOf Validation.CommonRules.StringRequired)
        End Sub
        Public Function IsExist(ByVal target As Object, ByVal e As RuleArgs) As Boolean
            Dim mMe As New AppSystemMgr(ConnStr)
            mMe.SetDataEntity(New AppSystem(New KeyValueHelper(AppSystem.Properties.ID.ToString, DataEntity.ID))).
            Fetch(New MgrArgs(New OptionList(enumIsLike.equal)))
            If mMe.DataEntityList.Any Then
                Dim mObj As AppSystem = CType(mMe.DataEntityList.FirstOrDefault, AppSystem)
                If (DataEntity.ObjectState = EnumObjectState.Added) Then
                    If TypeName(target) = TypeName(Me.DataEntity) Then e.Description = String.Format("'{0}' already exists.", DataEntity.ID)    'other caller will bypass this and make use of True and False
                    Return False
                Else
                    Return True
                End If
            Else
                Return True
            End If
        End Function
#End Region


#Region "Overrides"
        Public Overloads Function SetDataEntity() As AppSystemMgr
            MyBase.SetDataEntity(New AppSystem)
            DataEntity.ObjectState = EnumObjectState.Added
            Return Me
        End Function
        Public Overloads Function SetDataEntity(ByVal ParamArray vkp() As KeyValueHelper) As AppSystemMgr
            SetDataEntity()
            SetPropertyValue(vkp)
            Return Me
        End Function
#End Region

#Region "Overrides"
        Public Overrides Sub Add()
            With DataBaseBuilder
                .CreateCommandParameters(AppSystem.Properties.ID.ToString, DataEntity.ID)
                .CreateCommandParameters(AppSystem.Properties.Name.ToString, DataEntity.Name)
                .CreateCommandParameters(AppSystem.Properties.CreatedBy.ToString, DataEntity.CreatedBy)
                .CreateCommandParameters(AppSystem.Properties.CreatedOn.ToString, DataEntity.CreatedOn)
                .CreateCommandParameters(AppSystem.Properties.EditedBy.ToString, DataEntity.EditedBy)
                .CreateCommandParameters(AppSystem.Properties.EditedOn.ToString, DataEntity.EditedOn)
            End With
            Try
                DataBaseBuilder.SaveChanges(AppSystem.StoreProcedures.spAppSystem_Insert.ToString, CommandType.StoredProcedure)
                If DataBaseBuilder.ErrorMsg IsNot Nothing Then
                    Throw New Exception(DataBaseBuilder.ErrorMsg)
                Else
                    DataEntity.ObjectState = EnumObjectState.Unchanged
                End If
            Catch e As System.Exception
                DataEntity.ErrorMsg = e.Message
            End Try
        End Sub
        Public Overrides Sub Update()
            With DataBaseBuilder
                .CreateCommandParameters(AppSystem.Properties.ID.ToString, DataEntity.ID)
                .CreateCommandParameters(AppSystem.Properties.Name.ToString, DataEntity.Name)
                .CreateCommandParameters(AppSystem.Properties.CreatedBy.ToString, DataEntity.CreatedBy)
                .CreateCommandParameters(AppSystem.Properties.CreatedOn.ToString, DataEntity.CreatedOn)
                .CreateCommandParameters(AppSystem.Properties.EditedBy.ToString, DataEntity.EditedBy)
                .CreateCommandParameters(AppSystem.Properties.EditedOn.ToString, DataEntity.EditedOn)
            End With
            Try
                DataBaseBuilder.SaveChanges(AppSystem.StoreProcedures.spAppSystem_Update.ToString, CommandType.StoredProcedure)
                If DataBaseBuilder.ErrorMsg IsNot Nothing Then
                    Throw New Exception(DataBaseBuilder.ErrorMsg)
                Else
                    DataEntity.ObjectState = EnumObjectState.Unchanged
                End If
            Catch e As System.Exception
                DataEntity.ErrorMsg = e.Message
            End Try
        End Sub
        Public Overrides Sub Delete()
            Dim mProcRetValue As IDataParameter
            With DataBaseBuilder
                .CreateCommandParameters(AppSystem.Properties.ID.ToString, DataEntity.ID)
                mProcRetValue = .CreateCommandParametersExplicit("ErrMsg", DbType.String, ParameterDirection.Output)
            End With
            Try
                Dim i As Integer = DataBaseBuilder.SaveChanges(AppSystem.StoreProcedures.spAppSystem_Delete.ToString, CommandType.StoredProcedure)
                If DataBaseBuilder.ErrorMsg IsNot Nothing Then
                    Throw New Exception(DataBaseBuilder.ErrorMsg)
                End If
                If i < 0 Then
                    DataEntity.ErrorMsg = mProcRetValue.Value    'return error
                End If
            Catch e As System.Exception
                DataEntity.ErrorMsg = e.Message
            End Try
        End Sub
        Public Overrides Function Fetch(ByVal DataBusinessParams As MgrArgs) As IDataEntityMgr(Of AppSystem)
            Dim objcol As New List(Of AppSystem)
            Dim obj As AppSystem
            Try
                With DataBaseBuilder
                    .CreateCommandParameters(AppSystem.Properties.ID.ToString, DataEntity.ID)
                    .CreateCommandParameters(AppSystem.Properties.Name.ToString, DataEntity.Name)
                    .CreateCommandParameters("SortField", DataBusinessParams.GetOrderBy)
                    .CreateCommandParameters("IsLike", DataBusinessParams.OptionList.IsLike)
                End With

                Using mDataReader As IDataReader = DataBaseBuilder.GetdataReader(AppSystem.StoreProcedures.spAppSystem_Get.ToString)
                    If DataBaseBuilder.ErrorMsg IsNot Nothing Then
                        Throw New Exception(DataBaseBuilder.ErrorMsg)
                    End If

                    While mDataReader.Read
                        obj = New AppSystem
                        With obj
                            .ID = SafeField(mDataReader(AppSystem.DbFields.Sy_ID.ToString))
                            .Name = SafeField(mDataReader(AppSystem.DbFields.Sy_Name.ToString))
                            .CreatedBy = SafeField(mDataReader(AppSystem.DbFields.Sy_CreatedBy.ToString))
                            .CreatedOn = SafeField(mDataReader(AppSystem.DbFields.Sy_CreatedOn.ToString))
                            .EditedBy = SafeField(mDataReader(AppSystem.DbFields.Sy_EditedBy.ToString))
                            .EditedOn = SafeField(mDataReader(AppSystem.DbFields.Sy_EditedOn.ToString))
                            .ObjectState = EnumObjectState.Unchanged
                        End With
                        objcol.Add(obj)
                    End While
                End Using
                SetDataEntityList(objcol)
            Catch ex As Exception
                DataEntity.ErrorMsg = ex.Message
                Throw New Exception(ex.Message)
            End Try
            Return Me
        End Function
        Public Overrides Function Save() As IDataEntityMgr(Of AppSystem)
            Select Case DataEntity.ObjectState
                Case EnumObjectState.Added, EnumObjectState.Modified
                    DataEntity.CheckAllRules()
                    If DataEntity.IsValid = False Then DataEntity.ErrorMsg = DataEntity.BrokenRulesCollection.ToString
            End Select
            Select Case DataEntity.ObjectState
                Case EnumObjectState.Added
                    Add()
                Case EnumObjectState.Modified
                    Update()
                Case EnumObjectState.Deleted
                    Delete()
            End Select
            MyBase.Save()
            Return Me
        End Function
#End Region


    End Class
End Namespace