Imports CompanyMasterCore.DataEntity
Imports DataAccess
Imports DataAccess.DatabaseBuilder
Imports Helper
Imports Helper.ApplicationTools
Imports Validation

Namespace DataEntityMgr
    Public Class CompanyMasterMgr
        Inherits DataEntityMgrBase(Of CompanyMaster)

#Region "Enum"
        Public Enum Rules
            ID_StringRequired
            ID_IsIDExist
            Name_StringRequired
            RegNo_IsRegNoExist
            CoGroup_IsCoGroupValid
        End Enum
#End Region

#Region "Field"
        Private _includeCompanyList As New List(Of String)
#End Region

#Region "Constructor"
        Public Sub New(ByVal connstr As String, ByVal Optional ProviderType As ProviderType = ProviderType.SqlClient)
            MyBase.New(connstr, New CompanyMaster, ProviderType)
        End Sub
        Public Sub New(ByVal connstr As String, ByVal companyMaster As CompanyMaster)
            MyBase.New(connstr, companyMaster)
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
            DataEntity.AddBusinessRules(CompanyMaster.Properties.ID.ToString, AddressOf Validation.CommonRules.StringRequired)
            DataEntity.AddBusinessRules(CompanyMaster.Properties.ID.ToString, AddressOf IsIDExist)
            DataEntity.AddBusinessRules(CompanyMaster.Properties.Name.ToString, AddressOf Validation.CommonRules.StringRequired)
            DataEntity.AddBusinessRules(CompanyMaster.Properties.RegNo.ToString, AddressOf IsRegNoExist)
            DataEntity.AddBusinessRules(CompanyMaster.Properties.CoGroup.ToString, AddressOf IsCoGroupValid)
        End Sub
        Public Function IsIDExist(ByVal target As Object, ByVal e As RuleArgs) As Boolean
            Dim mMe As New CompanyMasterMgr(ConnStr)
            mMe.SetDataEntity(New CompanyMaster(New KeyValueHelper(CompanyMaster.Properties.ID.ToString, DataEntity.ID))).
            Fetch(New MgrArgs(New OptionList(enumIsLike.equal)))
            If mMe.DataEntityList.Any Then
                Dim mObj As CompanyMaster = CType(mMe.DataEntityList.FirstOrDefault, CompanyMaster)
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
        Public Function IsRegNoExist(ByVal target As Object, ByVal e As RuleArgs) As Boolean
            Dim mMe As New CompanyMasterMgr(ConnStr)
            mMe.SetDataEntity(New CompanyMaster(New KeyValueHelper(CompanyMaster.Properties.RegNo.ToString, DataEntity.RegNo))).
            Fetch(New MgrArgs(New OptionList(enumIsLike.equal)))
            If mMe.DataEntityList.Any Then
                If (DataEntity.ObjectState = EnumObjectState.Added) Then
                    e.Description = String.Format("'{0}' already exists.", DataEntity.RegNo)    'other caller will bypass this and make use of True and False
                    Return False
                End If
                If mMe.DataEntityList.Any(Function(a) a.ID = DataEntity.ID) Then
                    Return True     'regno belongs to this record
                Else
                    e.Description = String.Format("'{0}' already exists.", DataEntity.RegNo)    'other caller will bypass this and make use of True and False
                    Return False
                End If
            Else
                Return True     'does not exist
            End If
        End Function
        Public Function IsCoGroupValid(ByVal target As Object, ByVal e As RuleArgs) As Boolean
            Dim mMe As New CompanyMasterMgr(ConnStr)
            mMe.SetDataEntity(New CompanyMaster(New KeyValueHelper(CompanyMaster.Properties.ID.ToString, DataEntity.CoGroup))).
            Fetch(New MgrArgs(New OptionList(enumIsLike.equal)))
            If mMe.DataEntityList.Any Then
                If DataEntity.ID = DataEntity.CoGroup Then
                    e.Description = String.Format("Company Group cannot be own self.")
                    Return False
                Else
                    Return True
                End If
            Else
                e.Description = String.Format("'{0}' does not exist.", DataEntity.CoGroup)
                Return False
            End If
        End Function
#End Region

#Region "Properties"
        Public ReadOnly Property IncludeCompanyList() As List(Of String)
            Get
                Return _includeCompanyList
            End Get
        End Property
#End Region

#Region "Overrides"
        Public Overloads Function SetDataEntity() As CompanyMasterMgr
            MyBase.SetDataEntity(New CompanyMaster)
            DataEntity.ObjectState = EnumObjectState.Added
            Return Me
        End Function
        Public Overloads Function SetDataEntity(ByVal ParamArray vkp() As KeyValueHelper) As CompanyMasterMgr
            SetDataEntity()
            SetPropertyValue(vkp)
            Return Me
        End Function
#End Region

#Region "Overrides"
        Public Overrides Sub Add()
            With DataBaseBuilder
                .CreateCommandParameters(CompanyMaster.Properties.ID.ToString, DataEntity.ID)
                .CreateCommandParameters(CompanyMaster.Properties.Name.ToString, DataEntity.Name)
                .CreateCommandParameters(CompanyMaster.Properties.RegNo.ToString, DataEntity.RegNo)
                .CreateCommandParameters(CompanyMaster.Properties.Branch.ToString, DataEntity.Branch)
                .CreateCommandParameters(CompanyMaster.Properties.Address.ToString, DataEntity.Address)
                .CreateCommandParameters(CompanyMaster.Properties.City.ToString, DataEntity.City)
                .CreateCommandParameters(CompanyMaster.Properties.Postcode.ToString, DataEntity.Postcode)
                .CreateCommandParameters(CompanyMaster.Properties.State.ToString, DataEntity.State)
                .CreateCommandParameters(CompanyMaster.Properties.Country.ToString, DataEntity.Country)
                .CreateCommandParameters(CompanyMaster.Properties.Tel.ToString, DataEntity.Tel)
                .CreateCommandParameters(CompanyMaster.Properties.Fax.ToString, DataEntity.Fax)
                .CreateCommandParameters(CompanyMaster.Properties.Email.ToString, DataEntity.Email)
                .CreateCommandParameters(CompanyMaster.Properties.Active.ToString, DataEntity.Active)
                .CreateCommandParameters(CompanyMaster.Properties.CoGroup.ToString, DataEntity.CoGroup)
                .CreateCommandParameters(CompanyMaster.Properties.CreatedBy.ToString, DataEntity.CreatedBy)
                .CreateCommandParameters(CompanyMaster.Properties.CreatedOn.ToString, DataEntity.CreatedOn)
                .CreateCommandParameters(CompanyMaster.Properties.EditedBy.ToString, DataEntity.EditedBy)
                .CreateCommandParameters(CompanyMaster.Properties.EditedOn.ToString, DataEntity.EditedOn)
            End With
            Try
                DataBaseBuilder.SaveChanges(CompanyMaster.StoreProcedures.spCompanyMaster_Insert.ToString, CommandType.StoredProcedure)
                If DataBaseBuilder.ErrorMsg IsNot Nothing Then
                    Throw New Exception(DataBaseBuilder.ErrorMsg)
                Else
                    DataEntity.ObjectState = EnumObjectState.Unchanged
                End If
            Catch e As System.Exception
                DataEntity.ErrorMsg = e.Message
            End Try
        End Sub
        Public Overrides Async Function AddAsync() As Task
            With DataBaseBuilder
                .CreateCommandParameters(CompanyMaster.Properties.ID.ToString, DataEntity.ID)
                .CreateCommandParameters(CompanyMaster.Properties.Name.ToString, DataEntity.Name)
                .CreateCommandParameters(CompanyMaster.Properties.RegNo.ToString, DataEntity.RegNo)
                .CreateCommandParameters(CompanyMaster.Properties.Branch.ToString, DataEntity.Branch)
                .CreateCommandParameters(CompanyMaster.Properties.Address.ToString, DataEntity.Address)
                .CreateCommandParameters(CompanyMaster.Properties.City.ToString, DataEntity.City)
                .CreateCommandParameters(CompanyMaster.Properties.Postcode.ToString, DataEntity.Postcode)
                .CreateCommandParameters(CompanyMaster.Properties.State.ToString, DataEntity.State)
                .CreateCommandParameters(CompanyMaster.Properties.Country.ToString, DataEntity.Country)
                .CreateCommandParameters(CompanyMaster.Properties.Tel.ToString, DataEntity.Tel)
                .CreateCommandParameters(CompanyMaster.Properties.Fax.ToString, DataEntity.Fax)
                .CreateCommandParameters(CompanyMaster.Properties.Email.ToString, DataEntity.Email)
                .CreateCommandParameters(CompanyMaster.Properties.Active.ToString, DataEntity.Active)
                .CreateCommandParameters(CompanyMaster.Properties.CoGroup.ToString, DataEntity.CoGroup)
                .CreateCommandParameters(CompanyMaster.Properties.CreatedBy.ToString, DataEntity.CreatedBy)
                .CreateCommandParameters(CompanyMaster.Properties.CreatedOn.ToString, DataEntity.CreatedOn)
                .CreateCommandParameters(CompanyMaster.Properties.EditedBy.ToString, DataEntity.EditedBy)
                .CreateCommandParameters(CompanyMaster.Properties.EditedOn.ToString, DataEntity.EditedOn)
            End With
            Try
                Await DataBaseBuilder.SaveChangesAsync(CompanyMaster.StoreProcedures.spCompanyMaster_Insert.ToString, CommandType.StoredProcedure)
                If DataBaseBuilder.ErrorMsg IsNot Nothing Then
                    Throw New Exception(DataBaseBuilder.ErrorMsg)
                Else
                    DataEntity.ObjectState = EnumObjectState.Unchanged
                End If
            Catch e As System.Exception
                DataEntity.ErrorMsg = e.Message
            End Try
        End Function
        Public Overrides Sub Update()
            With DataBaseBuilder
                .CreateCommandParameters(CompanyMaster.Properties.ID.ToString, DataEntity.ID)
                .CreateCommandParameters(CompanyMaster.Properties.Name.ToString, DataEntity.Name)
                .CreateCommandParameters(CompanyMaster.Properties.RegNo.ToString, DataEntity.RegNo)
                .CreateCommandParameters(CompanyMaster.Properties.Branch.ToString, DataEntity.Branch)
                .CreateCommandParameters(CompanyMaster.Properties.Address.ToString, DataEntity.Address)
                .CreateCommandParameters(CompanyMaster.Properties.City.ToString, DataEntity.City)
                .CreateCommandParameters(CompanyMaster.Properties.Postcode.ToString, DataEntity.Postcode)
                .CreateCommandParameters(CompanyMaster.Properties.State.ToString, DataEntity.State)
                .CreateCommandParameters(CompanyMaster.Properties.Country.ToString, DataEntity.Country)
                .CreateCommandParameters(CompanyMaster.Properties.Tel.ToString, DataEntity.Tel)
                .CreateCommandParameters(CompanyMaster.Properties.Fax.ToString, DataEntity.Fax)
                .CreateCommandParameters(CompanyMaster.Properties.Email.ToString, DataEntity.Email)
                .CreateCommandParameters(CompanyMaster.Properties.Active.ToString, DataEntity.Active)
                .CreateCommandParameters(CompanyMaster.Properties.CoGroup.ToString, DataEntity.CoGroup)
                .CreateCommandParameters(CompanyMaster.Properties.CreatedBy.ToString, DataEntity.CreatedBy)
                .CreateCommandParameters(CompanyMaster.Properties.CreatedOn.ToString, DataEntity.CreatedOn)
                .CreateCommandParameters(CompanyMaster.Properties.EditedBy.ToString, DataEntity.EditedBy)
                .CreateCommandParameters(CompanyMaster.Properties.EditedOn.ToString, DataEntity.EditedOn)
            End With
            Try
                DataBaseBuilder.SaveChanges(CompanyMaster.StoreProcedures.spCompanyMaster_Update.ToString, CommandType.StoredProcedure)
                If DataBaseBuilder.ErrorMsg IsNot Nothing Then
                    Throw New Exception(DataBaseBuilder.ErrorMsg)
                Else
                    DataEntity.ObjectState = EnumObjectState.Unchanged
                End If
            Catch e As System.Exception
                DataEntity.ErrorMsg = e.Message
            End Try
        End Sub
        Public Overrides Async Function UpdateAsync() As Task
            With DataBaseBuilder
                .CreateCommandParameters(CompanyMaster.Properties.ID.ToString, DataEntity.ID)
                .CreateCommandParameters(CompanyMaster.Properties.Name.ToString, DataEntity.Name)
                .CreateCommandParameters(CompanyMaster.Properties.RegNo.ToString, DataEntity.RegNo)
                .CreateCommandParameters(CompanyMaster.Properties.Branch.ToString, DataEntity.Branch)
                .CreateCommandParameters(CompanyMaster.Properties.Address.ToString, DataEntity.Address)
                .CreateCommandParameters(CompanyMaster.Properties.City.ToString, DataEntity.City)
                .CreateCommandParameters(CompanyMaster.Properties.Postcode.ToString, DataEntity.Postcode)
                .CreateCommandParameters(CompanyMaster.Properties.State.ToString, DataEntity.State)
                .CreateCommandParameters(CompanyMaster.Properties.Country.ToString, DataEntity.Country)
                .CreateCommandParameters(CompanyMaster.Properties.Tel.ToString, DataEntity.Tel)
                .CreateCommandParameters(CompanyMaster.Properties.Fax.ToString, DataEntity.Fax)
                .CreateCommandParameters(CompanyMaster.Properties.Email.ToString, DataEntity.Email)
                .CreateCommandParameters(CompanyMaster.Properties.Active.ToString, DataEntity.Active)
                .CreateCommandParameters(CompanyMaster.Properties.CoGroup.ToString, DataEntity.CoGroup)
                .CreateCommandParameters(CompanyMaster.Properties.CreatedBy.ToString, DataEntity.CreatedBy)
                .CreateCommandParameters(CompanyMaster.Properties.CreatedOn.ToString, DataEntity.CreatedOn)
                .CreateCommandParameters(CompanyMaster.Properties.EditedBy.ToString, DataEntity.EditedBy)
                .CreateCommandParameters(CompanyMaster.Properties.EditedOn.ToString, DataEntity.EditedOn)
            End With
            Try
                Await DataBaseBuilder.SaveChangesAsync(CompanyMaster.StoreProcedures.spCompanyMaster_Update.ToString, CommandType.StoredProcedure)
                If DataBaseBuilder.ErrorMsg IsNot Nothing Then
                    Throw New Exception(DataBaseBuilder.ErrorMsg)
                Else
                    DataEntity.ObjectState = EnumObjectState.Unchanged
                End If
            Catch e As System.Exception
                DataEntity.ErrorMsg = e.Message
            End Try
        End Function
        Public Overrides Sub Delete()
            Dim mProcRetValue As IDataParameter
            With DataBaseBuilder
                .CreateCommandParameters(CompanyMaster.Properties.ID.ToString, DataEntity.ID)
                mProcRetValue = .CreateCommandParametersExplicit("ErrMsg", DbType.String, ParameterDirection.Output)
            End With
            Try
                Dim i As Integer = DataBaseBuilder.SaveChanges(CompanyMaster.StoreProcedures.spCompanyMaster_Delete.ToString, CommandType.StoredProcedure)
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
        Public Overrides Async Function DeleteAsync() As Task
            Dim mProcRetValue As IDataParameter
            With DataBaseBuilder
                .CreateCommandParameters(CompanyMaster.Properties.ID.ToString, DataEntity.ID)
                mProcRetValue = .CreateCommandParametersExplicit("ErrMsg", DbType.String, ParameterDirection.Output)
            End With
            Try
                Dim i As Integer = Await DataBaseBuilder.SaveChangesAsync(CompanyMaster.StoreProcedures.spCompanyMaster_Delete.ToString, CommandType.StoredProcedure)
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
        Public Overrides Function Fetch(ByVal DataBusinessParams As MgrArgs) As IDataEntityMgr(Of CompanyMaster)
            Dim objcol As New List(Of CompanyMaster)
            Dim obj As CompanyMaster
            Try
                With DataBaseBuilder
                    .CreateCommandParameters(CompanyMaster.Properties.ID.ToString, DataEntity.ID)
                    .CreateCommandParameters(CompanyMaster.Properties.Name.ToString, DataEntity.Name)
                    .CreateCommandParameters(CompanyMaster.Properties.RegNo.ToString, DataEntity.RegNo)
                    .CreateCommandParameters(CompanyMaster.Properties.Branch.ToString, DataEntity.Branch)
                    .CreateCommandParameters(CompanyMaster.Properties.Address.ToString, DataEntity.Address)
                    .CreateCommandParameters(CompanyMaster.Properties.City.ToString, DataEntity.City)
                    .CreateCommandParameters(CompanyMaster.Properties.Postcode.ToString, DataEntity.Postcode)
                    .CreateCommandParameters(CompanyMaster.Properties.State.ToString, DataEntity.State)
                    .CreateCommandParameters(CompanyMaster.Properties.Country.ToString, DataEntity.Country)
                    .CreateCommandParameters(CompanyMaster.Properties.Tel.ToString, DataEntity.Tel)
                    .CreateCommandParameters(CompanyMaster.Properties.Fax.ToString, DataEntity.Fax)
                    .CreateCommandParameters(CompanyMaster.Properties.Email.ToString, DataEntity.Email)
                    .CreateCommandParameters(CompanyMaster.Properties.CoGroup.ToString, DataEntity.CoGroup)
                    .CreateCommandParameters(CompanyMaster.Properties.Level.ToString, DataEntity.Level)
                    .CreateCommandParameters(CompanyMaster.Properties.HasChildrenOf.ToString, DataEntity.HasChildrenOf)
                    .CreateCommandParameters(CompanyMaster.Properties.Active.ToString, DataEntity.Active)
                    .CreateCommandParameters(CompanyMaster.Properties.CreatedBy.ToString, DataEntity.CreatedBy)
                    .CreateCommandParameters(CompanyMaster.Properties.CreatedOn.ToString, DataEntity.CreatedOn)
                    .CreateCommandParameters(CompanyMaster.Properties.EditedBy.ToString, DataEntity.EditedBy)
                    .CreateCommandParameters(CompanyMaster.Properties.EditedOn.ToString, DataEntity.EditedOn)
                    .CreateCommandParameters(CompanyMaster.Properties.GSTNo.ToString, DataEntity.GSTNo)
                    .CreateCommandParameters(CompanyMaster.Properties.SalesTaxNo.ToString, DataEntity.SalesTaxNo)
                    .CreateCommandParameters(CompanyMaster.Properties.ServiceTaxNo.ToString, DataEntity.ServiceTaxNo)
                    .CreateCommandParameters(CompanyMaster.Properties.SysID.ToString, DataEntity.SysID)         'Fixed value to be passed from FrmLogin
                    .CreateCommandParameters("SortField", DataBusinessParams.GetOrderBy)
                    .CreateCommandParameters("IsLike", DataBusinessParams.OptionList.IsLike)
                End With

                Using mDataReader As IDataReader = DataBaseBuilder.GetdataReader(CompanyMaster.StoreProcedures.spCompanyMaster_Get.ToString)
                    If DataBaseBuilder.ErrorMsg IsNot Nothing Then
                        Throw New Exception(DataBaseBuilder.ErrorMsg)
                    End If

                    While mDataReader.Read
                        obj = New CompanyMaster
                        With obj
                            .ID = SafeField(mDataReader(CompanyMaster.DbFields.Co_ID.ToString))
                            .Name = SafeField(mDataReader(CompanyMaster.DbFields.Co_Name.ToString))
                            .RegNo = SafeField(mDataReader(CompanyMaster.DbFields.Co_RegNo.ToString))
                            .Branch = SafeField(mDataReader(CompanyMaster.DbFields.Co_Branch.ToString))
                            .Address = SafeField(mDataReader(CompanyMaster.DbFields.Co_Address.ToString))
                            .City = SafeField(mDataReader(CompanyMaster.DbFields.Co_City.ToString))
                            .Postcode = SafeField(mDataReader(CompanyMaster.DbFields.Co_Postcode.ToString))
                            .State = SafeField(mDataReader(CompanyMaster.DbFields.Co_State.ToString))
                            .Country = SafeField(mDataReader(CompanyMaster.DbFields.Co_Country.ToString))
                            .Tel = SafeField(mDataReader(CompanyMaster.DbFields.Co_Tel.ToString))
                            .Fax = SafeField(mDataReader(CompanyMaster.DbFields.Co_Fax.ToString))
                            .Email = SafeField(mDataReader(CompanyMaster.DbFields.Co_Email.ToString))
                            .CoGroup = SafeField(mDataReader(CompanyMaster.DbFields.Co_CoGroup.ToString))
                            .Level = SafeField(mDataReader(CompanyMaster.DbFields.Co_Level.ToString))
                            .HasChildrenOf = SafeField(mDataReader(CompanyMaster.DbFields.Co_HasChildrenOf.ToString))
                            .Active = SafeField(mDataReader(CompanyMaster.DbFields.Co_Active.ToString))
                            .CreatedBy = SafeField(mDataReader(CompanyMaster.DbFields.Co_CreatedBy.ToString))
                            .CreatedOn = SafeField(mDataReader(CompanyMaster.DbFields.Co_CreatedOn.ToString))
                            .EditedBy = SafeField(mDataReader(CompanyMaster.DbFields.Co_EditedBy.ToString))
                            .EditedOn = SafeField(mDataReader(CompanyMaster.DbFields.Co_EditedOn.ToString))
                            .GSTNo = SafeField(mDataReader(CompanyMaster.DbFields.Co_GSTNo.ToString))
                            .SalesTaxNo = SafeField(mDataReader(CompanyMaster.DbFields.Co_SalesTaxNo.ToString))
                            .ServiceTaxNo = SafeField(mDataReader(CompanyMaster.DbFields.Co_ServiceTaxNo.ToString))
                            .DbName = SafeField(mDataReader(CompanyMaster.DbFields.Co_DbName.ToString))
                            .ServerName = SafeField(mDataReader(CompanyMaster.DbFields.Co_ServerName.ToString))
                            .ConnectionString = SafeField(mDataReader(CompanyMaster.DbFields.Co_ConnectionString.ToString))
                            .ServerPath = SafeField(mDataReader(CompanyMaster.DbFields.Co_ServerPath.ToString))
                            .BackupPath1 = SafeField(mDataReader(CompanyMaster.DbFields.Co_BackupPath1.ToString))
                            .BackupPath2 = SafeField(mDataReader(CompanyMaster.DbFields.Co_BackupPath2.ToString))
                            .ExportPath1 = SafeField(mDataReader(CompanyMaster.DbFields.Co_ExportPath1.ToString))
                            .ExportPath2 = SafeField(mDataReader(CompanyMaster.DbFields.Co_ExportPath2.ToString))
                            .ExportPath3 = SafeField(mDataReader(CompanyMaster.DbFields.Co_ExportPath3.ToString))
                            .IsSysActive = SafeField(mDataReader(CompanyMaster.DbFields.Co_IsSysActive.ToString))
                            .SysID = SafeField(mDataReader(CompanyMaster.DbFields.Co_SysID.ToString))
                            .ObjectState = EnumObjectState.Unchanged

                            If IncludeCompanyList.Any Then
                                If IncludeCompanyList.Exists(Function(a) a = .ID) = False Then Continue While
                            End If
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
        Public Overrides Async Function FetchAsync(ByVal DataBusinessParams As MgrArgs) As Task(Of IDataEntityMgr(Of CompanyMaster))
            Dim objcol As New List(Of CompanyMaster)
            Dim obj As CompanyMaster
            Try
                With DataBaseBuilder
                    .CreateCommandParameters(CompanyMaster.Properties.ID.ToString, DataEntity.ID)
                    .CreateCommandParameters(CompanyMaster.Properties.Name.ToString, DataEntity.Name)
                    .CreateCommandParameters(CompanyMaster.Properties.RegNo.ToString, DataEntity.RegNo)
                    .CreateCommandParameters(CompanyMaster.Properties.Branch.ToString, DataEntity.Branch)
                    .CreateCommandParameters(CompanyMaster.Properties.Address.ToString, DataEntity.Address)
                    .CreateCommandParameters(CompanyMaster.Properties.City.ToString, DataEntity.City)
                    .CreateCommandParameters(CompanyMaster.Properties.Postcode.ToString, DataEntity.Postcode)
                    .CreateCommandParameters(CompanyMaster.Properties.State.ToString, DataEntity.State)
                    .CreateCommandParameters(CompanyMaster.Properties.Country.ToString, DataEntity.Country)
                    .CreateCommandParameters(CompanyMaster.Properties.Tel.ToString, DataEntity.Tel)
                    .CreateCommandParameters(CompanyMaster.Properties.Fax.ToString, DataEntity.Fax)
                    .CreateCommandParameters(CompanyMaster.Properties.Email.ToString, DataEntity.Email)
                    .CreateCommandParameters(CompanyMaster.Properties.CoGroup.ToString, DataEntity.CoGroup)
                    .CreateCommandParameters(CompanyMaster.Properties.Level.ToString, DataEntity.Level)
                    .CreateCommandParameters(CompanyMaster.Properties.HasChildrenOf.ToString, DataEntity.HasChildrenOf)
                    .CreateCommandParameters(CompanyMaster.Properties.Active.ToString, DataEntity.Active)
                    .CreateCommandParameters(CompanyMaster.Properties.CreatedBy.ToString, DataEntity.CreatedBy)
                    .CreateCommandParameters(CompanyMaster.Properties.CreatedOn.ToString, DataEntity.CreatedOn)
                    .CreateCommandParameters(CompanyMaster.Properties.EditedBy.ToString, DataEntity.EditedBy)
                    .CreateCommandParameters(CompanyMaster.Properties.EditedOn.ToString, DataEntity.EditedOn)
                    .CreateCommandParameters(CompanyMaster.Properties.GSTNo.ToString, DataEntity.GSTNo)
                    .CreateCommandParameters(CompanyMaster.Properties.SalesTaxNo.ToString, DataEntity.SalesTaxNo)
                    .CreateCommandParameters(CompanyMaster.Properties.ServiceTaxNo.ToString, DataEntity.ServiceTaxNo)
                    .CreateCommandParameters(CompanyMaster.Properties.SysID.ToString, DataEntity.SysID)         'Fixed value to be passed from FrmLogin
                    .CreateCommandParameters("SortField", DataBusinessParams.GetOrderBy)
                    .CreateCommandParameters("IsLike", DataBusinessParams.OptionList.IsLike)
                End With

                Using mDataReader As IDataReader = Await DataBaseBuilder.GetdataReaderAsync(CompanyMaster.StoreProcedures.spCompanyMaster_Get.ToString)
                    If DataBaseBuilder.ErrorMsg IsNot Nothing Then
                        Throw New Exception(DataBaseBuilder.ErrorMsg)
                    End If

                    While mDataReader.Read
                        obj = New CompanyMaster
                        With obj
                            .ID = SafeField(mDataReader(CompanyMaster.DbFields.Co_ID.ToString))
                            .Name = SafeField(mDataReader(CompanyMaster.DbFields.Co_Name.ToString))
                            .RegNo = SafeField(mDataReader(CompanyMaster.DbFields.Co_RegNo.ToString))
                            .Branch = SafeField(mDataReader(CompanyMaster.DbFields.Co_Branch.ToString))
                            .Address = SafeField(mDataReader(CompanyMaster.DbFields.Co_Address.ToString))
                            .City = SafeField(mDataReader(CompanyMaster.DbFields.Co_City.ToString))
                            .Postcode = SafeField(mDataReader(CompanyMaster.DbFields.Co_Postcode.ToString))
                            .State = SafeField(mDataReader(CompanyMaster.DbFields.Co_State.ToString))
                            .Country = SafeField(mDataReader(CompanyMaster.DbFields.Co_Country.ToString))
                            .Tel = SafeField(mDataReader(CompanyMaster.DbFields.Co_Tel.ToString))
                            .Fax = SafeField(mDataReader(CompanyMaster.DbFields.Co_Fax.ToString))
                            .Email = SafeField(mDataReader(CompanyMaster.DbFields.Co_Email.ToString))
                            .CoGroup = SafeField(mDataReader(CompanyMaster.DbFields.Co_CoGroup.ToString))
                            .Level = SafeField(mDataReader(CompanyMaster.DbFields.Co_Level.ToString))
                            .HasChildrenOf = SafeField(mDataReader(CompanyMaster.DbFields.Co_HasChildrenOf.ToString))
                            .Active = SafeField(mDataReader(CompanyMaster.DbFields.Co_Active.ToString))
                            .CreatedBy = SafeField(mDataReader(CompanyMaster.DbFields.Co_CreatedBy.ToString))
                            .CreatedOn = SafeField(mDataReader(CompanyMaster.DbFields.Co_CreatedOn.ToString))
                            .EditedBy = SafeField(mDataReader(CompanyMaster.DbFields.Co_EditedBy.ToString))
                            .EditedOn = SafeField(mDataReader(CompanyMaster.DbFields.Co_EditedOn.ToString))
                            .GSTNo = SafeField(mDataReader(CompanyMaster.DbFields.Co_GSTNo.ToString))
                            .SalesTaxNo = SafeField(mDataReader(CompanyMaster.DbFields.Co_SalesTaxNo.ToString))
                            .ServiceTaxNo = SafeField(mDataReader(CompanyMaster.DbFields.Co_ServiceTaxNo.ToString))
                            .DbName = SafeField(mDataReader(CompanyMaster.DbFields.Co_DbName.ToString))
                            .ServerName = SafeField(mDataReader(CompanyMaster.DbFields.Co_ServerName.ToString))
                            .ConnectionString = SafeField(mDataReader(CompanyMaster.DbFields.Co_ConnectionString.ToString))
                            .ServerPath = SafeField(mDataReader(CompanyMaster.DbFields.Co_ServerPath.ToString))
                            .BackupPath1 = SafeField(mDataReader(CompanyMaster.DbFields.Co_BackupPath1.ToString))
                            .BackupPath2 = SafeField(mDataReader(CompanyMaster.DbFields.Co_BackupPath2.ToString))
                            .ExportPath1 = SafeField(mDataReader(CompanyMaster.DbFields.Co_ExportPath1.ToString))
                            .ExportPath2 = SafeField(mDataReader(CompanyMaster.DbFields.Co_ExportPath2.ToString))
                            .ExportPath3 = SafeField(mDataReader(CompanyMaster.DbFields.Co_ExportPath3.ToString))
                            .IsSysActive = SafeField(mDataReader(CompanyMaster.DbFields.Co_IsSysActive.ToString))
                            .SysID = SafeField(mDataReader(CompanyMaster.DbFields.Co_SysID.ToString))
                            .ObjectState = EnumObjectState.Unchanged

                            If IncludeCompanyList.Any Then
                                If IncludeCompanyList.Exists(Function(a) a = .ID) = False Then Continue While
                            End If
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
        Public Overrides Function Save() As IDataEntityMgr(Of CompanyMaster)
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