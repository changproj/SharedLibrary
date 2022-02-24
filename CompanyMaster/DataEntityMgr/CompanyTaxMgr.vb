Imports DataAccess
Imports Helper
Imports Helper.ApplicationTools
Imports Validation
Imports CompanyMasterCore.DataEntity

Namespace DataEntityMgr
    Public Class CompanyTaxMgr
        Inherits DataEntityMgrBase(Of CompanyTax)

#Region "Enum"
        Public Enum Rules
            CoID_StringRequired
            CoID_IsCoExist
            CoID_IsExist
        End Enum
#End Region

#Region "Field"
        Private _includeCompanyList As New List(Of String)
#End Region

#Region "Constructor"
        Public Sub New(ByVal connstr As String)
            MyBase.New(connstr, New CompanyTax)
        End Sub
        Public Sub New(ByVal connstr As String, ByVal companyTax As CompanyTax)
            MyBase.New(connstr, companyTax)
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
            DataEntity.AddBusinessRules(CompanyTax.Properties.CoID.ToString, AddressOf Validation.CommonRules.StringRequired)
            DataEntity.AddBusinessRules(CompanyTax.Properties.CoID.ToString, AddressOf IsCoExist)
            DataEntity.AddBusinessRules(CompanyTax.Properties.CoID.ToString, AddressOf IsExist)
        End Sub
        Public Function IsExist(ByVal target As Object, ByVal e As RuleArgs) As Boolean
            Dim mMe As New CompanyTaxMgr(ConnStr)
            mMe.SetDataEntity(New CompanyTax(New KeyValueHelper(CompanyTax.Properties.CoID.ToString, DataEntity.CoID))).
            Fetch(New MgrArgs(New OptionList(enumIsLike.equal)))
            If mMe.DataEntityList.Any Then
                Dim mObj As CompanyTax = CType(mMe.DataEntityList.FirstOrDefault, CompanyTax)
                If (DataEntity.ObjectState = EnumObjectState.Added) Then
                    If TypeName(target) = TypeName(Me.DataEntity) Then e.Description = String.Format("'{0}' already exists.", DataEntity.CoID)    'other caller will bypass this and make use of True and False
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
#End Region


#Region "Overrides"
        Public Overloads Function SetDataEntity() As CompanyTaxMgr
            MyBase.SetDataEntity(New CompanyTax)
            DataEntity.ObjectState = EnumObjectState.Added
            Return Me
        End Function
        Public Overloads Function SetDataEntity(ByVal ParamArray vkp() As KeyValueHelper) As CompanyTaxMgr
            SetDataEntity()
            SetPropertyValue(vkp)
            Return Me
        End Function
#End Region

#Region "Overrides"
        Public Overrides Sub Add()
            With DataBaseBuilder
                .CreateCommandParameters(CompanyTax.Properties.CoID.ToString, DataEntity.CoID)
                .CreateCommandParameters(CompanyTax.Properties.GSTNo.ToString, DataEntity.GSTNo)
                .CreateCommandParameters(CompanyTax.Properties.SalesTaxNo.ToString, DataEntity.SalesTaxNo)
                .CreateCommandParameters(CompanyTax.Properties.ServiceTaxNo.ToString, DataEntity.ServiceTaxNo)
                .CreateCommandParameters(CompanyTax.Properties.CreatedBy.ToString, DataEntity.CreatedBy)
                .CreateCommandParameters(CompanyTax.Properties.CreatedOn.ToString, DataEntity.CreatedOn)
                .CreateCommandParameters(CompanyTax.Properties.EditedBy.ToString, DataEntity.EditedBy)
                .CreateCommandParameters(CompanyTax.Properties.EditedOn.ToString, DataEntity.EditedOn)
            End With
            Try
                DataBaseBuilder.SaveChanges(CompanyTax.StoreProcedures.spCompanyTax_Insert.ToString, CommandType.StoredProcedure)
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
                .CreateCommandParameters(CompanyTax.Properties.CoID.ToString, DataEntity.CoID)
                .CreateCommandParameters(CompanyTax.Properties.GSTNo.ToString, DataEntity.GSTNo)
                .CreateCommandParameters(CompanyTax.Properties.SalesTaxNo.ToString, DataEntity.SalesTaxNo)
                .CreateCommandParameters(CompanyTax.Properties.ServiceTaxNo.ToString, DataEntity.ServiceTaxNo)
                .CreateCommandParameters(CompanyTax.Properties.CreatedBy.ToString, DataEntity.CreatedBy)
                .CreateCommandParameters(CompanyTax.Properties.CreatedOn.ToString, DataEntity.CreatedOn)
                .CreateCommandParameters(CompanyTax.Properties.EditedBy.ToString, DataEntity.EditedBy)
                .CreateCommandParameters(CompanyTax.Properties.EditedOn.ToString, DataEntity.EditedOn)
            End With
            Try
                DataBaseBuilder.SaveChanges(CompanyTax.StoreProcedures.spCompanyTax_Update.ToString, CommandType.StoredProcedure)
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
                .CreateCommandParameters(CompanyTax.Properties.CoID.ToString, DataEntity.CoID)
                mProcRetValue = .CreateCommandParametersExplicit("ErrMsg", DbType.String, ParameterDirection.Output)
            End With
            Try
                Dim i As Integer = DataBaseBuilder.SaveChanges(CompanyTax.StoreProcedures.spCompanyTax_Delete.ToString, CommandType.StoredProcedure)
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
        Public Overrides Function Fetch(ByVal DataBusinessParams As MgrArgs) As IDataEntityMgr(Of CompanyTax)
            Dim objcol As New List(Of CompanyTax)
            Dim obj As CompanyTax
            Try
                With DataBaseBuilder
                    .CreateCommandParameters(CompanyTax.Properties.CoID.ToString, DataEntity.CoID)
                    .CreateCommandParameters(CompanyTax.Properties.GSTNo.ToString, DataEntity.GSTNo)
                    .CreateCommandParameters(CompanyTax.Properties.SalesTaxNo.ToString, DataEntity.SalesTaxNo)
                    .CreateCommandParameters(CompanyTax.Properties.ServiceTaxNo.ToString, DataEntity.ServiceTaxNo)
                    .CreateCommandParameters("SortField", DataBusinessParams.GetOrderBy)
                    .CreateCommandParameters("IsLike", DataBusinessParams.OptionList.IsLike)
                End With

                Using mDataReader As IDataReader = DataBaseBuilder.GetdataReader(CompanyTax.StoreProcedures.spCompanyTax_Get.ToString)
                    If DataBaseBuilder.ErrorMsg IsNot Nothing Then
                        Throw New Exception(DataBaseBuilder.ErrorMsg)
                    End If

                    While mDataReader.Read
                        obj = New CompanyTax
                        With obj
                            .CoID = SafeField(mDataReader(CompanyTax.DbFields.Ct_CoID.ToString))
                            .GSTNo = SafeField(mDataReader(CompanyTax.DbFields.Ct_GSTNo.ToString))
                            .SalesTaxNo = SafeField(mDataReader(CompanyTax.DbFields.Ct_SalesTaxNo.ToString))
                            .ServiceTaxNo = SafeField(mDataReader(CompanyTax.DbFields.Ct_ServiceTaxNo.ToString))
                            .CreatedBy = SafeField(mDataReader(CompanyTax.DbFields.Ct_CreatedBy.ToString))
                            .CreatedOn = SafeField(mDataReader(CompanyTax.DbFields.Ct_CreatedOn.ToString))
                            .EditedBy = SafeField(mDataReader(CompanyTax.DbFields.Ct_EditedBy.ToString))
                            .EditedOn = SafeField(mDataReader(CompanyTax.DbFields.Ct_EditedOn.ToString))
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
        Public Overrides Function Save() As IDataEntityMgr(Of CompanyTax)
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
