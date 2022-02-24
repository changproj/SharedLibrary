
Imports System.Threading.Tasks

Namespace DataAccess

    Public MustInherit Class DatabaseBuilder
        Implements IDatabaseBuilder

#Region "Enum"
        Public Enum ProviderType
            SqlClient
            OleDb
            Odbc
        End Enum
#End Region

#Region "Field"

        Protected _dataSet As DataSet
        Protected _providerType As ProviderType
        Protected _globalConnectionString As String
        Protected _hasRow As Boolean
        Protected _errMsg As String = Nothing
        Protected _hasCompleted As Boolean = False
#End Region

#Region "Property"
        Public Sub New(connectionString As String)
            _globalConnectionString = connectionString
        End Sub
        Public Property GlobalConnectionString() As String
            Get
                Return _globalConnectionString
            End Get
            Set(ByVal value As String)
                _globalConnectionString = value
            End Set
        End Property
        Public Property Provider() As ProviderType
            Get
                Return _providerType
            End Get
            Set(ByVal value As ProviderType)
                _providerType = value
            End Set
        End Property
        Public ReadOnly Property HasRow() As Boolean
            Get
                Return _hasRow
            End Get
        End Property
        Public Property ErrorMsg() As String
            Get
                Return _errMsg
            End Get
            Set(ByVal value As String)
                _errMsg = value
            End Set
        End Property
        Public Property DatabaseProvider()
            Get
                Return _providerType
            End Get
            Set(value)
                _providerType = value
            End Set
        End Property
        Public ReadOnly Property HasCompleted() As Boolean
            Get
                Return _hasCompleted
            End Get
        End Property
#End Region

#Region "CreateCommandParameters"
        Public MustOverride Sub ClearParameters()
        Public MustOverride Sub DisposeCommand()
        Public MustOverride Sub CreateCommandParameters(ByVal parameterName As String, ByVal value As Object)
        Public MustOverride Sub CreateCommandParameters(ByVal parameterName As String, ByVal value As Object, ByVal fieldType As DbType, ByVal TypeName As String)
        Public MustOverride Sub CreateCommandParametersExplicit(ByVal parameterName As String, ByVal value As Object, ByVal parameterDirection As ParameterDirection, ByVal fieldSize As Integer)
        Public MustOverride Function CreateCommandParametersExplicit(ByVal parameterName As String, ByVal fieldType As DbType, ByVal parameterDirection As ParameterDirection) As IDataParameter
#End Region

#Region "GetCommandParameter"
        Public MustOverride Function GetCommandParameter(ByVal parameterName As String) As IDataParameter
#End Region

#Region "SaveChanges"
        Public MustOverride Function SaveChanges(ByVal query As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As Integer
        Public MustOverride Function SaveChanges(ByVal queryList As List(Of String)) As Integer     'added
#End Region

#Region "SaveChangesAsync"
        Public MustOverride Async Function SaveChangesAsync(ByVal query As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As Task(Of Integer)
        Public MustOverride Async Function SaveChangesAsync(ByVal queryList As List(Of String)) As Task(Of Integer)
#End Region

#Region "GetScalarValue"
        Public MustOverride Function GetScalarValue(ByVal scalarQuery As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As Object
        Public MustOverride Function GetScalarValue(Of T)(ByVal scalarQuery As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As T
#End Region

#Region "GetScalarValueAsync"
        Public MustOverride Async Function GetScalarValueAsync(ByVal scalarQuery As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As Task(Of Object)
        Public MustOverride Async Function GetScalarValueAsync(Of T)(ByVal scalarQuery As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As Task(Of T)
#End Region

#Region "GetdataReader"
        Public MustOverride Function GetdataReader(ByVal parameterName As String, ByVal query As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As List(Of Object)
        Public MustOverride Function GetdataReader(ByVal query As Object, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As List(Of Object)
        Public MustOverride Function GetdataReader(ByVal query As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As IDataReader
#End Region

#Region "GetdataReaderAsync"
        Public MustOverride Async Function GetdataReaderAsync(ByVal parameterName As String, ByVal query As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As Task(Of List(Of Object))
        Public MustOverride Async Function GetdataReaderAsync(ByVal query As Object, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As Task(Of List(Of Object))
        Public MustOverride Async Function GetdataReaderAsync(ByVal query As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As Task(Of IDataReader)
#End Region

#Region "GetDataTable"
        Public MustOverride Function GetDataTable(ByVal selectQuery As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As DataTable
        Public MustOverride Function GetDataTable(ByVal tableIndex As Integer, ByVal selectQuery As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As DataTable
#End Region

#Region "GetDataTableAsync"
        Public MustOverride Async Function GetDataTableAsync(ByVal selectQuery As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As Task(Of DataTable)
        Public MustOverride Async Function GetDataTableAsync(ByVal tableIndex As Integer, ByVal selectQuery As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As Task(Of DataTable)
#End Region

#Region "GetDataSet"
        Public MustOverride Function GetDataSet(ByVal selectQuery As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As DataSet
#End Region

#Region "GetDataSetAsync"
        Public MustOverride Async Function GetDataSetAsync(ByVal selectQuery As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As Task(Of DataSet)
#End Region

#Region "Others"
        Protected Sub CheckForRow(ByVal rowCount As Integer)
            If rowCount > 0 Then
                _hasRow = True
            Else
                _hasRow = False
            End If
        End Sub
        Public Overrides Function ToString() As String
            Return String.Format("{0}", DatabaseProvider.ToString)
        End Function
#End Region

    End Class
End Namespace
