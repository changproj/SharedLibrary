If Exists (Select * From sys.objects Where type = 'P' And name = 'spCompanyMaster_Get')
Drop Procedure spCompanyMaster_Get
Go
Create  PROCEDURE [dbo].[spCompanyMaster_Get]
	 @ID				nvarchar(50)	=	Null
    ,@Name				nvarchar(200)	=	Null
    ,@RegNo				nvarchar(50)	=	Null
	,@Branch			nvarchar(50)	=	Null
    ,@Address			nvarchar(255)	=	Null
    ,@City				nvarchar(100)	=	Null
    ,@Postcode			nvarchar(5)		=	Null
    ,@State				nvarchar(30)	=	Null
    ,@Country			nvarchar(50)	=	Null
    ,@Tel				nvarchar(50)	=	Null
    ,@Fax				nvarchar(50)	=	Null
    ,@Email				nvarchar(50)	=	Null
    ,@CoGroup			nvarchar(50)	=	Null
	,@Level				int				=	Null
	,@HasChildrenOf		int				=	Null
	,@Active			bit				=	Null
    ,@CreatedBy			nvarchar(10)	=	Null
    ,@CreatedOn			datetime		=	Null
    ,@EditedBy			nvarchar(10)	=	Null
    ,@EditedOn			datetime		=	Null
	,@GSTNo				nvarchar(50)	=	Null
	,@SalesTaxNo		nvarchar(50)	=	Null
	,@ServiceTaxNo		nvarchar(50)	=	Null
	,@SysID				nvarchar(50)	=	Null
	,@SortField			nvarchar(100)	=	Null 
	,@IsLike 			Int				=   Null
	,@SelectionText		nvarchar(1000)	=   Null
AS

Begin

Declare  @sql		Nvarchar(Max)
		,@sqlParams Nvarchar(Max)
		,@IsSST		Bit
		
Set @sqlParams = '
					 @ID				nvarchar(50)	
					,@Name				nvarchar(200)	
					,@RegNo				nvarchar(50)	
					,@Branch			nvarchar(50)	
					,@Address			nvarchar(255)	
					,@City				nvarchar(100)	
					,@Postcode			nvarchar(5)		
					,@State				nvarchar(30)	
					,@Country			nvarchar(50)	
					,@Tel				nvarchar(50)	
					,@Fax				nvarchar(50)	
					,@Email				nvarchar(50)	
					,@CoGroup			nvarchar(50)	
					,@Level				int				
					,@HasChildrenOf		int				
					,@Active			bit				
					,@CreatedBy			nvarchar(10)	
					,@CreatedOn			datetime		
					,@EditedBy			nvarchar(10)	
					,@EditedOn			datetime		
					,@GSTNo				nvarchar(50)	
					,@SalesTaxNo		nvarchar(50)	
					,@ServiceTaxNo		nvarchar(50)	
					,@SysID				nvarchar(50)	
					,@SortField			nvarchar(100)	
					,@IsLike 			Int				
					,@SelectionText		nvarchar(1000)	
				'

;With
[Co0] (
	 ID				
	,CoGroup		
	,Level			
)
As
(
Select 
	 Co_ID				
	,Co_CoGroup	
	,0				
From [Company]
Where Co_CoGroup Is Null
Union All
Select 
	 [a].Co_ID				
	,[a].Co_CoGroup
	,[b].Level + 1				
FROM		[Company] [a]
INNER JOIN	[Co0]	  [b]
ON			[a].Co_CoGroup = [b].ID
)
,
[Co1] (ID, CoGroup, ID2) As (
	Select
		  ID
		 ,CoGroup
		 ,Case When CoGroup Is Not Null Then CoGroup Else ID End 
	From [Co0]
)
,
[Co2] (ID2, CoGroup, HasChildrenOf) As (
	Select
		 ID2
		,CoGroup
		,Count(ID2)
	From [Co1]
	Group By ID2,CoGroup
	Having CoGroup Is Not Null
)
,
[Co3] As (
	Select
		 [a].ID				
		,[a].CoGroup		
		,[a].Level			As Co_Level
		,[b].HasChildrenOf	As Co_HasChildrenOf
	From	  [Co0] [a]
	Left Join [Co2] [b]
	On		  [a].ID = [b].ID2	
)
,
[Ct0] As (
	Select
		 [a].*
		,[b].Ct_GSTNo			As Co_GSTNo
		,[b].Ct_SalesTaxNo		As Co_SalesTaxNo
		,[b].Ct_ServiceTaxNo	As Co_ServiceTaxNo
	From	  [Co3]			[a]
	Left Join [CompanyTax]	[b]
	On		  [a].ID = [b].Ct_CoID
)
,
[Cs0] As (
	Select
		 [a].ID
		,[a].Co_Level
	    ,[a].Co_HasChildrenOf
		,[a].Co_GSTNo
		,[a].Co_SalesTaxNo
		,[a].Co_ServiceTaxNo
		,[b].Cs_DbName				As Co_DbName
		,[b].Cs_ServerName			As Co_ServerName
		,[b].Cs_ConnectionString	As Co_ConnectionString
		,[b].Cs_ServerPath			As Co_ServerPath
		,[b].Cs_BackupPath1			As Co_BackupPath1
		,[b].Cs_BackupPath2			As Co_BackupPath2
		,[b].Cs_ExportPath1			As Co_ExportPath1
		,[b].Cs_ExportPath2			As Co_ExportPath2
		,[b].Cs_ExportPath3			As Co_ExportPath3
		,[b].Cs_Active				As Co_IsSysActive
		,[b].Cs_SysID					As Co_SysID
	From	  [Ct0]				[a]
	Left Join [CompanySystem]	[b]		/* [a] repeats if [b] has multiple same CoID */
	On		  [a].ID = [b].Cs_CoID
	Where	  1 = Case When @SysID Is Null			Then 1 
					   When @SysID = [b].Cs_SysID	Then 1 
													Else 0 End
)
,
[Co4] As (
	Select
	 [a].Co_ID				
	,[a].Co_Name				
	,[a].Co_RegNo				
	,[a].Co_Branch
	,[a].Co_Address			
	,[a].Co_City				
	,[a].Co_Postcode			
	,[a].Co_State				
	,[a].Co_Country			
	,[a].Co_Tel				
	,[a].Co_Fax				
	,[a].Co_Email				
	,[a].Co_CoGroup
	,[b].Co_Level 
	,Isnull([b].Co_HasChildrenOf, 0) As Co_HasChildrenOf
	,[a].Co_Active	
	,[a].Co_CreatedBy			
	,[a].Co_CreatedOn			
	,[a].Co_EditedBy			
	,[a].Co_EditedOn
	,[b].Co_GSTNo
	,[b].Co_SalesTaxNo
	,[b].Co_ServiceTaxNo
	,[b].Co_DbName
	,[b].Co_ServerName
	,[b].Co_ConnectionString
	,[b].Co_ServerPath
	,[b].Co_BackupPath1
	,[b].Co_BackupPath2
	,[b].Co_ExportPath1
	,[b].Co_ExportPath2
	,[b].Co_ExportPath3
	,[b].Co_IsSysActive
	,[b].Co_SysID
	From [Company] [a]
	Join [Cs0]	   [b]
	On	 [a].Co_ID = [b].ID	
)
Select * Into #tbl
From [Co4]


Set @sql = 
'
	Select 
		 *
	From #tbl Where (1=1)
'

If @ID Is Not Null
	Set @sql = @sql + ' And Co_ID ' + dbo.fnGetIsLike(@ID, IsNull(@IsLike, 0))

If @Name Is Not Null
	Set @sql = @sql + ' And Co_Name ' + dbo.fnGetIsLike(@Name, IsNull(@IsLike, 0))
        
If @RegNo Is Not Null
	Set @sql = @sql + ' And Co_RegNo ' + dbo.fnGetIsLike(@RegNo, IsNull(@IsLike, 0))

If @Branch Is Not Null
	Set @sql = @sql + ' And Co_Branch ' + dbo.fnGetIsLike(@Branch, IsNull(@IsLike, 0))
	
If @Address Is Not Null
	Set @sql = @sql + ' And Co_Address ' + dbo.fnGetIsLike(@Address, IsNull(@IsLike, 0))

If @City Is Not Null
	Set @sql = @sql + ' And Co_City ' + dbo.fnGetIsLike(@City, IsNull(@IsLike, 0))

If @Postcode Is Not Null
	Set @sql = @sql + ' And Co_Postcode ' + dbo.fnGetIsLike(@Postcode, IsNull(@IsLike, 0))

If @State Is Not Null
	Set @sql = @sql + ' And Co_State ' + dbo.fnGetIsLike(@State, IsNull(@IsLike, 0))

If @Country Is Not Null
	Set @sql = @sql + ' And Co_Country ' + dbo.fnGetIsLike(@Country, IsNull(@IsLike, 0))

If @Tel Is Not Null
	Set @sql = @sql + ' And Co_Tel ' + dbo.fnGetIsLike(@Tel, IsNull(@IsLike, 0))

If @Fax Is Not Null
	Set @sql = @sql + ' And Co_Fax ' + dbo.fnGetIsLike(@Fax, IsNull(@IsLike, 0))

If @Email Is Not Null
	Set @sql = @sql + ' And Co_Email ' + dbo.fnGetIsLike(@Email, IsNull(@IsLike, 0))
       
If @CoGroup Is Not Null
	Set @sql = @sql + ' And Co_CoGroup ' + dbo.fnGetIsLike(@CoGroup, IsNull(@IsLike, 0))
        
If @Level Is Not Null
	Set @sql = @sql + ' And Co_Level = ' + Cast(@Level as nvarchar)

If @HasChildrenOf Is Not Null
	Set @sql = @sql + ' And Co_HasChildrenOf = ' + Cast(@HasChildrenOf as nvarchar)

If @Active Is Not Null
	Set @sql = @sql + ' And Co_Active = ' + Cast(@Active as nvarchar)
	
If @CreatedBy Is Not Null
	Set @sql = @sql + ' And Co_CreatedBy ' + dbo.fnGetIsLike(@CreatedBy, IsNull(@IsLike, 0))
	
If @CreatedOn Is Not Null
    Set @sql = @sql + ' And DateAdd(dd,0,Datediff(dd,0,Co_CreatedOn)) >= ' 
					+ ' ''' + Cast(@CreatedOn as nvarchar)+ ''''
					+ ' And DateAdd(dd,0,Datediff(dd,0,Co_CreatedOn)) <= ' 
					+ ' ''' + Cast(@CreatedOn as nvarchar) + ''''
							  
If @EditedBy Is Not Null
	Set @sql = @sql + ' And Co_EditedBy ' + dbo.fnGetIsLike(@EditedBy, IsNull(@IsLike, 0))
	
If @EditedOn Is Not Null
    Set @sql = @sql + ' And DateAdd(dd,0,Datediff(dd,0,Co_EditedOn)) >= ' 
					+ ' ''' + Cast(@EditedOn as nvarchar)+ ''''
					+ ' And DateAdd(dd,0,Datediff(dd,0,Co_EditedOn)) <= ' 
					+ ' ''' + Cast(@EditedOn as nvarchar) + ''''

If @GSTNo Is Not Null
	Set @sql = @sql + ' And Co_GSTNo ' + dbo.fnGetIsLike(@GSTNo, IsNull(@IsLike, 0))

If @SalesTaxNo Is Not Null
	Set @sql = @sql + ' And Co_SalesTaxNo ' + dbo.fnGetIsLike(@SalesTaxNo, IsNull(@IsLike, 0))

If @ServiceTaxNo Is Not Null
	Set @sql = @sql + ' And Co_ServiceTaxNo ' + dbo.fnGetIsLike(@ServiceTaxNo, IsNull(@IsLike, 0))


If @SelectionText Is Not Null
	Set @sql = @sql + @SelectionText


If @SortField Is Not NULL
    Set @SQL = @SQL + @SortField


print @sql

Exec sp_executesql  @sql, @sqlParams,	 @ID				=	@ID			
										,@Name				=	@Name			
										,@RegNo				=	@RegNo			
										,@Branch			=	@Branch		
										,@Address			=	@Address		
										,@City				=	@City			
										,@Postcode			=	@Postcode		
										,@State				=	@State			
										,@Country			=	@Country		
										,@Tel				=	@Tel			
										,@Fax				=	@Fax			
										,@Email				=	@Email			
										,@CoGroup			=	@CoGroup		
										,@Level				=	@Level			
										,@HasChildrenOf		=	@HasChildrenOf	
										,@Active			=	@Active		
										,@CreatedBy			=	@CreatedBy		
										,@CreatedOn			=	@CreatedOn		
										,@EditedBy			=	@EditedBy		
										,@EditedOn			=	@EditedOn		
										,@GSTNo				=	@GSTNo			
										,@SalesTaxNo		=	@SalesTaxNo	
										,@ServiceTaxNo		=	@ServiceTaxNo	
										,@SysID				=	@SysID	
										,@SortField			=	@SortField		
										,@IsLike 			=	@IsLike 	
										,@SelectionText		=   @SelectionText		


Drop Table #tbl

END

/*

Exec [spCompanyMaster_Get]		 @ID				=	null
								,@Name				=	null
								,@RegNo				=	null
								,@Branch			=	null
								,@Address			=	null
								,@City				=	null
								,@Postcode			=	null
								,@State				=	null
								,@Country			=	null
								,@Tel				=	null
								,@Fax				=	null
								,@Email				=	null
								,@CoGroup			=	null
								,@Level				=	null
								,@HasChildrenOf		=	null
								,@Active			=	null
								,@CreatedBy			=	null
								,@CreatedOn			=	null
								,@EditedBy			=	null
								,@EditedOn			=	null
								,@GSTNo				=	null
								,@SalesTaxNo		=	null
								,@ServiceTaxNo		=	null
								,@SysID				=	'ACC'
								,@SortField			=	null
								,@IsLike 			=	null
								,@SelectionText		=   'And (1=1) And (Co_ID = ''W1'')'



*/

