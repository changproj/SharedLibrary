Imports CompanyMasterCore.DataEntity
Imports DataAccess
Imports Helper
Imports Helper.ApplicationTools
Imports Validation

Namespace DataEntityMgr
    Public Class CompanySystemMgr
        Inherits DataEntityMgrBase(Of CompanySystem)

#Region "Enum"
        Public Enum Rules
            CoID_StringRequired
            'CoID_IsExist
            CoID_IsCoExist
            SysID_StringRequired
            SysID_IsExist
            SysID_IsSysExist
            DbName_StringRequired
            ServerName_StringRequired
            ConnectionString_StringRequired
        End Enum
#End Region

#Region "Field"
        Private _includeCompanyList As New List(Of String)
#End Region

#Region "Constructor"
        Public Sub New(ByVal connstr As String)
            MyBase.New(connstr, New CompanySystem)
        End Sub
        Public Sub New(ByVal connstr As String, ByVal companySystem As CompanySystem)
            MyBase.New(connstr, companySystem)
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
            DataEntity.AddBusinessRules(CompanySystem.Properties.CoID.ToString, AddressOf Validation.CommonRules.StringRequired)
            DataEntity.AddBusinessRules(CompanySystem.Properties.SysID.ToString, AddressOf Validation.CommonRules.StringRequired)
            DataEntity.AddBusinessRules(CompanySystem.Properties.CoID.ToString, AddressOf IsCoExist)
            DataEntity.AddBusinessRules(CompanySystem.Properties.SysID.ToString, AddressOf IsSysExist)
            DataEntity.AddBusinessRules(CompanySystem.Properties.SysID.ToString, AddressOf IsExist)
            DataEntity.AddBusinessRules(CompanySystem.Properties.DbName.ToString, AddressOf Validation.CommonRules.StringRequired)
            DataEntity.AddBusinessRules(CompanySystem.Properties.ServerName.ToString, AddressOf Validation.CommonRules.StringRequired)
            DataEntity.AddBusinessRules(CompanySystem.Properties.ConnectionString.ToString, AddressOf Validation.CommonRules.StringRequired)
        End Sub
        Public Function IsExist(ByVal target As Object, ByVal e As RuleArgs) As Boolean
            Dim mMe As New CompanySystemMgr(ConnStr)
            mMe.SetDataEntity(New CompanySystem(New KeyValueHelper(CompanySystem.Properties.CoID.ToString, DataEntity.CoID),
                                                New KeyValueHelper(CompanySystem.Properties.SysID.ToString, DataEntity.SysID))).
            Fetch(New MgrArgs(New OptionList(enumIsLike.equal)))
            If mMe.DataEntityList.Any Then
                Dim mObj As CompanySystem = CType(mMe.DataEntityList.FirstOrDefault, CompanySystem)
                If (DataEntity.ObjectState = EnumObjectState.Added) Then
                    If TypeName(target) = TypeName(Me.DataEntity) Then e.Description = String.Format("'{0}' already exists.", DataEntity.SysID)    'other caller will bypass this and make use of True and False
                    Return False
                Else
                    Return True
                End If
            Else
                Return True
            End If
        End Function
        Public Function IsCoExist(ByVal target As Object, ByVal e As RuleArgs) As Boolean
            Dim mMe As New CompanyMasterMgr(ConnStr)
            Dim mIsExist As Boolean = mMe.SetDataEntity(New KeyValueHelper(CompanyMaster.Properties.ID.ToString, DataEntity.CoID)).
                                          IsIDExist(Me, New RuleArgs(CompanyMaster.Properties.ID.ToString))
            If mIsExist Then    'True means does not exist
                e.Description = String.Format("'{0}' does not exist.", DataEntity.CoID)
                Return False
            Else                'False means exists
                Return True
            End If
        End Function
        Public Function IsSysExist(ByVal target As Object, ByVal e As RuleArgs) As Boolean
            Dim mMe As New AppSystemMgr(ConnStr)
            Dim mIsExist As Boolean = mMe.SetDataEntity(New KeyValueHelper(AppSystem.Properties.ID.ToString, DataEntity.SysID)).
                                          IsExist(Me, New RuleArgs(AppSystem.Properties.ID.ToString))
            If mIsExist Then    'True means does not exist
                e.Description = String.Format("'{0}' does not exist.", DataEntity.SysID)
                Return False
            Else                'False means exists
                Return True
            End If
        End Function
#End Region


#Region "Overrides"
        Public Overloads Function SetDataEntity() As CompanySystemMgr
            MyBase.SetDataEntity(New CompanySystem)
            DataEntity.ObjectState = EnumObjectState.Added
            Return Me
        End Function
        Public Overloads Function SetDataEntity(ByVal ParamArray vkp() As KeyValueHelper) As CompanySystemMgr
            SetDataEntity()
            SetPropertyValue(vkp)
            Return Me
        End Function
#End Region

#Region "Overrides"
        Public Overrides Function Add() As Object
            With DataBaseBuilder
                .CreateCommandParameters(CompanySystem.Properties.CoID.ToString, DataEntity.CoID)
                .CreateCommandParameters(CompanySystem.Properties.SysID.ToString, DataEntity.SysID)
                .CreateCommandParameters(CompanySystem.Properties.DbName.ToString, DataEntity.DbName)
                .CreateCommandParameters(CompanySystem.Properties.ServerName.ToString, DataEntity.ServerName)
                .CreateCommandParameters(CompanySystem.Properties.ConnectionString.ToString, DataEntity.ConnectionString)
                .CreateCommandParameters(CompanySystem.Properties.ServerPath.ToString, DataEntity.ServerPath)
                .CreateCommandParameters(CompanySystem.Properties.BackupPath1.ToString, DataEntity.BackupPath1)
                .CreateCommandParameters(CompanySystem.Properties.BackupPath2.ToString, DataEntity.BackupPath2)
                .CreateCommandParameters(CompanySystem.Properties.ExportPath1.ToString, DataEntity.ExportPath1)
                .CreateCommandParameters(CompanySystem.Properties.ExportPath2.ToString, DataEntity.ExportPath2)
                .CreateCommandParameters(CompanySystem.Properties.ExportPath3.ToString, DataEntity.ExportPath3)
                .CreateCommandParameters(CompanySystem.Properties.Active.ToString, DataEntity.Active)
                .CreateCommandParameters(CompanySystem.Properties.CreatedBy.ToString, DataEntity.CreatedBy)
                .CreateCommandParameters(CompanySystem.Properties.CreatedOn.ToString, DataEntity.CreatedOn)
                .CreateCommandParameters(CompanySystem.Properties.EditedBy.ToString, DataEntity.EditedBy)
                .CreateCommandParameters(CompanySystem.Properties.EditedOn.ToString, DataEntity.EditedOn)
            End With
            Dim mCount As Integer = 0
            Try
                mCount = DataBaseBuilder.SaveChanges(CompanySystem.StoreProcedures.spCompanySystem_Insert.ToString, CommandType.StoredProcedure)
                If DataBaseBuilder.ErrorMsg IsNot Nothing Then
                    Throw New Exception(DataBaseBuilder.ErrorMsg)
                Else
                    DataEntity.ObjectState = EnumObjectState.Unchanged
                End If
            Catch e As System.Exception
                DataEntity.ErrorMsg = e.Message
            End Try
            Return mCount
        End Function
        Public Overrides Async Function AddAsync() As Task
            With DataBaseBuilder
                .CreateCommandParameters(CompanySystem.Properties.CoID.ToString, DataEntity.CoID)
                .CreateCommandParameters(CompanySystem.Properties.SysID.ToString, DataEntity.SysID)
                .CreateCommandParameters(CompanySystem.Properties.DbName.ToString, DataEntity.DbName)
                .CreateCommandParameters(CompanySystem.Properties.ServerName.ToString, DataEntity.ServerName)
                .CreateCommandParameters(CompanySystem.Properties.ConnectionString.ToString, DataEntity.ConnectionString)
                .CreateCommandParameters(CompanySystem.Properties.ServerPath.ToString, DataEntity.ServerPath)
                .CreateCommandParameters(CompanySystem.Properties.BackupPath1.ToString, DataEntity.BackupPath1)
                .CreateCommandParameters(CompanySystem.Properties.BackupPath2.ToString, DataEntity.BackupPath2)
                .CreateCommandParameters(CompanySystem.Properties.ExportPath1.ToString, DataEntity.ExportPath1)
                .CreateCommandParameters(CompanySystem.Properties.ExportPath2.ToString, DataEntity.ExportPath2)
                .CreateCommandParameters(CompanySystem.Properties.ExportPath3.ToString, DataEntity.ExportPath3)
                .CreateCommandParameters(CompanySystem.Properties.Active.ToString, DataEntity.Active)
                .CreateCommandParameters(CompanySystem.Properties.CreatedBy.ToString, DataEntity.CreatedBy)
                .CreateCommandParameters(CompanySystem.Properties.CreatedOn.ToString, DataEntity.CreatedOn)
                .CreateCommandParameters(CompanySystem.Properties.EditedBy.ToString, DataEntity.EditedBy)
                .CreateCommandParameters(CompanySystem.Properties.EditedOn.ToString, DataEntity.EditedOn)
            End With
            Try
                Await DataBaseBuilder.SaveChangesAsync(CompanySystem.StoreProcedures.spCompanySystem_Insert.ToString, CommandType.StoredProcedure)
                If DataBaseBuilder.ErrorMsg IsNot Nothing Then
                    Throw New Exception(DataBaseBuilder.ErrorMsg)
                Else
                    DataEntity.ObjectState = EnumObjectState.Unchanged
                End If
            Catch e As System.Exception
                DataEntity.ErrorMsg = e.Message
            End Try
        End Function
        Public Overrides Function Update() As Object
            With DataBaseBuilder
                .CreateCommandParameters(CompanySystem.Properties.AutoID.ToString, DataEntity.AutoID)
                .CreateCommandParameters(CompanySystem.Properties.CoID.ToString, DataEntity.CoID)
                .CreateCommandParameters(CompanySystem.Properties.SysID.ToString, DataEntity.SysID)
                .CreateCommandParameters(CompanySystem.Properties.DbName.ToString, DataEntity.DbName)
                .CreateCommandParameters(CompanySystem.Properties.ServerName.ToString, DataEntity.ServerName)
                .CreateCommandParameters(CompanySystem.Properties.ConnectionString.ToString, DataEntity.ConnectionString)
                .CreateCommandParameters(CompanySystem.Properties.ServerPath.ToString, DataEntity.ServerPath)
                .CreateCommandParameters(CompanySystem.Properties.BackupPath1.ToString, DataEntity.BackupPath1)
                .CreateCommandParameters(CompanySystem.Properties.BackupPath2.ToString, DataEntity.BackupPath2)
                .CreateCommandParameters(CompanySystem.Properties.ExportPath1.ToString, DataEntity.ExportPath1)
                .CreateCommandParameters(CompanySystem.Properties.ExportPath2.ToString, DataEntity.ExportPath2)
                .CreateCommandParameters(CompanySystem.Properties.ExportPath3.ToString, DataEntity.ExportPath3)
                .CreateCommandParameters(CompanySystem.Properties.Active.ToString, DataEntity.Active)
                .CreateCommandParameters(CompanySystem.Properties.CreatedBy.ToString, DataEntity.CreatedBy)
                .CreateCommandParameters(CompanySystem.Properties.CreatedOn.ToString, DataEntity.CreatedOn)
                .CreateCommandParameters(CompanySystem.Properties.EditedBy.ToString, DataEntity.EditedBy)
                .CreateCommandParameters(CompanySystem.Properties.EditedOn.ToString, DataEntity.EditedOn)
            End With
            Dim mCount As Integer = 0
            Try
                mCount = DataBaseBuilder.SaveChanges(CompanySystem.StoreProcedures.spCompanySystem_Update.ToString, CommandType.StoredProcedure)
                If DataBaseBuilder.ErrorMsg IsNot Nothing Then
                    Throw New Exception(DataBaseBuilder.ErrorMsg)
                Else
                    DataEntity.ObjectState = EnumObjectState.Unchanged
                End If
            Catch e As System.Exception
                DataEntity.ErrorMsg = e.Message
            End Try
            Return mCount
        End Function
        Public Overrides Async Function UpdateAsync() As Task
            With DataBaseBuilder
                .CreateCommandParameters(CompanySystem.Properties.AutoID.ToString, DataEntity.AutoID)
                .CreateCommandParameters(CompanySystem.Properties.CoID.ToString, DataEntity.CoID)
                .CreateCommandParameters(CompanySystem.Properties.SysID.ToString, DataEntity.SysID)
                .CreateCommandParameters(CompanySystem.Properties.DbName.ToString, DataEntity.DbName)
                .CreateCommandParameters(CompanySystem.Properties.ServerName.ToString, DataEntity.ServerName)
                .CreateCommandParameters(CompanySystem.Properties.ConnectionString.ToString, DataEntity.ConnectionString)
                .CreateCommandParameters(CompanySystem.Properties.ServerPath.ToString, DataEntity.ServerPath)
                .CreateCommandParameters(CompanySystem.Properties.BackupPath1.ToString, DataEntity.BackupPath1)
                .CreateCommandParameters(CompanySystem.Properties.BackupPath2.ToString, DataEntity.BackupPath2)
                .CreateCommandParameters(CompanySystem.Properties.ExportPath1.ToString, DataEntity.ExportPath1)
                .CreateCommandParameters(CompanySystem.Properties.ExportPath2.ToString, DataEntity.ExportPath2)
                .CreateCommandParameters(CompanySystem.Properties.ExportPath3.ToString, DataEntity.ExportPath3)
                .CreateCommandParameters(CompanySystem.Properties.Active.ToString, DataEntity.Active)
                .CreateCommandParameters(CompanySystem.Properties.CreatedBy.ToString, DataEntity.CreatedBy)
                .CreateCommandParameters(CompanySystem.Properties.CreatedOn.ToString, DataEntity.CreatedOn)
                .CreateCommandParameters(CompanySystem.Properties.EditedBy.ToString, DataEntity.EditedBy)
                .CreateCommandParameters(CompanySystem.Properties.EditedOn.ToString, DataEntity.EditedOn)
            End With
            Try
                Await DataBaseBuilder.SaveChangesAsync(CompanySystem.StoreProcedures.spCompanySystem_Update.ToString, CommandType.StoredProcedure)
                If DataBaseBuilder.ErrorMsg IsNot Nothing Then
                    Throw New Exception(DataBaseBuilder.ErrorMsg)
                Else
                    DataEntity.ObjectState = EnumObjectState.Unchanged
                End If
            Catch e As System.Exception
                DataEntity.ErrorMsg = e.Message
            End Try
        End Function
        Public Overrides Function Delete() As Object
            Dim mProcRetValue As IDataParameter
            With DataBaseBuilder
                .CreateCommandParameters(CompanySystem.Properties.AutoID.ToString, DataEntity.AutoID)
                mProcRetValue = .CreateCommandParametersExplicit("ErrMsg", DbType.String, ParameterDirection.Output)
            End With
            Dim mCount As Integer = 0
            Try
                mCount = DataBaseBuilder.SaveChanges(CompanySystem.StoreProcedures.spCompanySystem_Delete.ToString, CommandType.StoredProcedure)
                If DataBaseBuilder.ErrorMsg IsNot Nothing Then
                    Throw New Exception(DataBaseBuilder.ErrorMsg)
                End If
                If mCount < 0 Then
                    DataEntity.ErrorMsg = mProcRetValue.Value    'return error
                End If
            Catch e As System.Exception
                DataEntity.ErrorMsg = e.Message
            End Try
            Return mCount
        End Function
        Public Overrides Async Function DeleteAsync() As Task
            Dim mProcRetValue As IDataParameter
            With DataBaseBuilder
                .CreateCommandParameters(CompanySystem.Properties.AutoID.ToString, DataEntity.AutoID)
                mProcRetValue = .CreateCommandParametersExplicit("ErrMsg", DbType.String, ParameterDirection.Output)
            End With
            Try
                Dim i As Integer = Await DataBaseBuilder.SaveChangesAsync(CompanySystem.StoreProcedures.spCompanySystem_Delete.ToString, CommandType.StoredProcedure)
                If DataBaseBuilder.ErrorMsg IsNot Nothing Then
                    Throw New Exception(DataBaseBuilder.ErrorMsg)
                End If
                If i < 0 Then
                    DataEntity.ErrorMsg = mProcRetValue.Value    'return error
                End If
            Catch e As System.Exception
                DataEntity.ErrorMsg = e.Message
            End Try
        End Function
        Public Overrides Function Fetch(ByVal DataBusinessParams As MgrArgs) As IDataEntityMgr(Of CompanySystem)
            Dim objcol As New List(Of CompanySystem)
            Dim obj As CompanySystem
            Try
                With DataBaseBuilder
                    .CreateCommandParameters(CompanySystem.Properties.AutoID.ToString, DataEntity.AutoID)
                    .CreateCommandParameters(CompanySystem.Properties.CoID.ToString, DataEntity.CoID)
                    .CreateCommandParameters(CompanySystem.Properties.SysID.ToString, DataEntity.SysID)
                    .CreateCommandParameters(CompanySystem.Properties.DbName.ToString, DataEntity.DbName)
                    .CreateCommandParameters(CompanySystem.Properties.ServerName.ToString, DataEntity.ServerName)
                    .CreateCommandParameters(CompanySystem.Properties.ConnectionString.ToString, DataEntity.ConnectionString)
                    .CreateCommandParameters(CompanySystem.Properties.ServerPath.ToString, DataEntity.ServerPath)
                    .CreateCommandParameters(CompanySystem.Properties.BackupPath1.ToString, DataEntity.BackupPath1)
                    .CreateCommandParameters(CompanySystem.Properties.BackupPath2.ToString, DataEntity.BackupPath2)
                    .CreateCommandParameters(CompanySystem.Properties.ExportPath1.ToString, DataEntity.ExportPath1)
                    .CreateCommandParameters(CompanySystem.Properties.ExportPath2.ToString, DataEntity.ExportPath2)
                    .CreateCommandParameters(CompanySystem.Properties.ExportPath3.ToString, DataEntity.ExportPath3)
                    .CreateCommandParameters(CompanySystem.Properties.Active.ToString, DataEntity.Active)
                    .CreateCommandParameters("SortField", DataBusinessParams.GetOrderBy)
                    .CreateCommandParameters("IsLike", DataBusinessParams.OptionList.IsLike)
                End With

                Using mDataReader As IDataReader = DataBaseBuilder.GetdataReader(CompanySystem.StoreProcedures.spCompanySystem_Get.ToString)
                    If DataBaseBuilder.ErrorMsg IsNot Nothing Then
                        Throw New Exception(DataBaseBuilder.ErrorMsg)
                    End If

                    While mDataReader.Read
                        obj = New CompanySystem
                        With obj
                            .AutoID = SafeField(mDataReader(CompanySystem.DbFields.Cs_AutoID.ToString))
                            .CoID = SafeField(mDataReader(CompanySystem.DbFields.Cs_CoID.ToString))
                            .SysID = SafeField(mDataReader(CompanySystem.DbFields.Cs_SysID.ToString))
                            .DbName = SafeField(mDataReader(CompanySystem.DbFields.Cs_DbName.ToString))
                            .ServerName = SafeField(mDataReader(CompanySystem.DbFields.Cs_ServerName.ToString))
                            .ConnectionString = SafeField(mDataReader(CompanySystem.DbFields.Cs_ConnectionString.ToString))
                            .ServerPath = SafeField(mDataReader(CompanySystem.DbFields.Cs_ServerPath.ToString))
                            .BackupPath1 = SafeField(mDataReader(CompanySystem.DbFields.Cs_BackupPath1.ToString))
                            .BackupPath2 = SafeField(mDataReader(CompanySystem.DbFields.Cs_BackupPath2.ToString))
                            .ExportPath1 = SafeField(mDataReader(CompanySystem.DbFields.Cs_ExportPath1.ToString))
                            .ExportPath2 = SafeField(mDataReader(CompanySystem.DbFields.Cs_ExportPath2.ToString))
                            .ExportPath3 = SafeField(mDataReader(CompanySystem.DbFields.Cs_ExportPath3.ToString))
                            .Active = SafeField(mDataReader(CompanySystem.DbFields.Cs_Active.ToString))
                            .CreatedBy = SafeField(mDataReader(CompanySystem.DbFields.Cs_CreatedBy.ToString))
                            .CreatedOn = SafeField(mDataReader(CompanySystem.DbFields.Cs_CreatedOn.ToString))
                            .EditedBy = SafeField(mDataReader(CompanySystem.DbFields.Cs_EditedBy.ToString))
                            .EditedOn = SafeField(mDataReader(CompanySystem.DbFields.Cs_EditedOn.ToString))
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
        Public Overrides Async Function FetchAsync(ByVal DataBusinessParams As MgrArgs) As Task(Of IDataEntityMgr(Of CompanySystem))
            Dim objcol As New List(Of CompanySystem)
            Dim obj As CompanySystem
            Try
                With DataBaseBuilder
                    .CreateCommandParameters(CompanySystem.Properties.AutoID.ToString, DataEntity.AutoID)
                    .CreateCommandParameters(CompanySystem.Properties.CoID.ToString, DataEntity.CoID)
                    .CreateCommandParameters(CompanySystem.Properties.SysID.ToString, DataEntity.SysID)
                    .CreateCommandParameters(CompanySystem.Properties.DbName.ToString, DataEntity.DbName)
                    .CreateCommandParameters(CompanySystem.Properties.ServerName.ToString, DataEntity.ServerName)
                    .CreateCommandParameters(CompanySystem.Properties.ConnectionString.ToString, DataEntity.ConnectionString)
                    .CreateCommandParameters(CompanySystem.Properties.ServerPath.ToString, DataEntity.ServerPath)
                    .CreateCommandParameters(CompanySystem.Properties.BackupPath1.ToString, DataEntity.BackupPath1)
                    .CreateCommandParameters(CompanySystem.Properties.BackupPath2.ToString, DataEntity.BackupPath2)
                    .CreateCommandParameters(CompanySystem.Properties.ExportPath1.ToString, DataEntity.ExportPath1)
                    .CreateCommandParameters(CompanySystem.Properties.ExportPath2.ToString, DataEntity.ExportPath2)
                    .CreateCommandParameters(CompanySystem.Properties.ExportPath3.ToString, DataEntity.ExportPath3)
                    .CreateCommandParameters(CompanySystem.Properties.Active.ToString, DataEntity.Active)
                    .CreateCommandParameters("SortField", DataBusinessParams.GetOrderBy)
                    .CreateCommandParameters("IsLike", DataBusinessParams.OptionList.IsLike)
                End With

                Using mDataReader As IDataReader = Await DataBaseBuilder.GetdataReaderAsync(CompanySystem.StoreProcedures.spCompanySystem_Get.ToString)
                    If DataBaseBuilder.ErrorMsg IsNot Nothing Then
                        Throw New Exception(DataBaseBuilder.ErrorMsg)
                    End If

                    While mDataReader.Read
                        obj = New CompanySystem
                        With obj
                            .AutoID = SafeField(mDataReader(CompanySystem.DbFields.Cs_AutoID.ToString))
                            .CoID = SafeField(mDataReader(CompanySystem.DbFields.Cs_CoID.ToString))
                            .SysID = SafeField(mDataReader(CompanySystem.DbFields.Cs_SysID.ToString))
                            .DbName = SafeField(mDataReader(CompanySystem.DbFields.Cs_DbName.ToString))
                            .ServerName = SafeField(mDataReader(CompanySystem.DbFields.Cs_ServerName.ToString))
                            .ConnectionString = SafeField(mDataReader(CompanySystem.DbFields.Cs_ConnectionString.ToString))
                            .ServerPath = SafeField(mDataReader(CompanySystem.DbFields.Cs_ServerPath.ToString))
                            .BackupPath1 = SafeField(mDataReader(CompanySystem.DbFields.Cs_BackupPath1.ToString))
                            .BackupPath2 = SafeField(mDataReader(CompanySystem.DbFields.Cs_BackupPath2.ToString))
                            .ExportPath1 = SafeField(mDataReader(CompanySystem.DbFields.Cs_ExportPath1.ToString))
                            .ExportPath2 = SafeField(mDataReader(CompanySystem.DbFields.Cs_ExportPath2.ToString))
                            .ExportPath3 = SafeField(mDataReader(CompanySystem.DbFields.Cs_ExportPath3.ToString))
                            .Active = SafeField(mDataReader(CompanySystem.DbFields.Cs_Active.ToString))
                            .CreatedBy = SafeField(mDataReader(CompanySystem.DbFields.Cs_CreatedBy.ToString))
                            .CreatedOn = SafeField(mDataReader(CompanySystem.DbFields.Cs_CreatedOn.ToString))
                            .EditedBy = SafeField(mDataReader(CompanySystem.DbFields.Cs_EditedBy.ToString))
                            .EditedOn = SafeField(mDataReader(CompanySystem.DbFields.Cs_EditedOn.ToString))
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
        Public Overrides Function Save() As IDataEntityMgr(Of CompanySystem)
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
        Public Overrides Async Function SaveAsync() As Task(Of IDataEntityMgr(Of CompanySystem))
            Select Case DataEntity.ObjectState
                Case EnumObjectState.Added, EnumObjectState.Modified
                    DataEntity.CheckAllRules()
                    If DataEntity.IsValid = False Then DataEntity.ErrorMsg = DataEntity.BrokenRulesCollection.ToString
            End Select
            Select Case DataEntity.ObjectState
                Case EnumObjectState.Added
                    Await AddAsync()
                Case EnumObjectState.Modified
                    Await UpdateAsync()
                Case EnumObjectState.Deleted
                    Await DeleteAsync()
            End Select
            Await MyBase.SaveAsync()
            Return Me
        End Function
#End Region


    End Class
End Namespace
