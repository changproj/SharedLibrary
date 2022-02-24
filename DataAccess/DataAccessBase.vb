Imports System.Data.Odbc
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Threading.Tasks

Namespace DataAccess

    Public Class DataAccessBase

#Region "Enum"
        Public Enum ProviderType
            SqlClient
            OleDb
            Odbc
        End Enum
#End Region

#Region "Field"
        Private _connection As IDbConnection
        Private _command As IDbCommand
        Private _dataAdapter As IDataAdapter
        Private _dataReader As IDataReader
        Private _dataSet As DataSet
        Private _providerType As ProviderType
        Private _globalConnectionString As String
        Private _hasRow As Boolean
        Private _errMsg As String = Nothing
#End Region

#Region "Property"
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
#End Region

#Region "InitializeDataAccess"
        Public Sub CastProvider(ByVal providerType As ProviderType, ByVal connectionString As String, Optional ByVal query As String = Nothing)
            _providerType = providerType
            _globalConnectionString = connectionString
            _dataSet = New DataSet

            Select Case providerType
                Case ProviderType.SqlClient
                    _connection = New SqlConnection(_globalConnectionString)
                    _command = New SqlCommand(query, _connection)
                    _dataAdapter = New SqlDataAdapter
                Case ProviderType.OleDb
                    _connection = New OleDbConnection(_globalConnectionString)
                    _command = New OleDbCommand(query, _connection)
                    _dataAdapter = New OleDbDataAdapter
                Case ProviderType.Odbc
                    _connection = New OdbcConnection(_globalConnectionString)
                    _command = New OdbcCommand(query, _connection)
                    _dataAdapter = New OdbcDataAdapter
            End Select
        End Sub
        Public Sub InitializeDataAccess(ByVal providerType As ProviderType, ByVal connectionString As String, ByVal query As String)
            CastProvider(providerType, connectionString, query)
        End Sub
        Public Sub InitializeDataAccess(ByVal providerType As ProviderType, ByVal connectionString As String)
            CastProvider(providerType, connectionString)
        End Sub
        Public Sub InitializeDataAccess(ByVal query As String, ByVal providerType As ProviderType)
            CastProvider(providerType, GlobalConnectionString, query)
        End Sub
        Public Sub InitializeDataAccess(ByVal providerType As ProviderType)
            CastProvider(providerType, GlobalConnectionString)
        End Sub
#End Region

#Region "CreateCommandParameters"
        Public Sub CreateCommandParameters(ByVal parameterName As String, ByVal value As Object)
            Select Case Provider
                Case ProviderType.SqlClient
                    _command.Parameters.Add(New SqlParameter(String.Format("@{0}", parameterName), value))
                Case ProviderType.OleDb
                    _command.Parameters.Add(New OleDbParameter(String.Format("@{0}", parameterName), value))
                Case ProviderType.Odbc
                    _command.Parameters.Add(New OdbcParameter(String.Format("@{0}", parameterName), value))
            End Select
        End Sub
        Public Sub CreateCommandParametersExplicit(ByVal parameterName As String, ByVal value As Object, ByVal parameterDirection As ParameterDirection, ByVal fieldSize As Integer)
            Dim mParameterInputOutput As IDbDataParameter = Nothing
            Select Case Provider
                Case ProviderType.SqlClient
                    mParameterInputOutput = New SqlParameter With {.ParameterName = String.Format("@{0}", parameterName, value),
                                                                   .Direction = parameterDirection,
                                                                   .Size = fieldSize}
                Case ProviderType.OleDb
                    mParameterInputOutput = New OleDbParameter With {.ParameterName = String.Format("@{0}", parameterName, value),
                                                                   .Direction = parameterDirection,
                                                                   .Size = fieldSize}
                Case ProviderType.Odbc
                    mParameterInputOutput = New OdbcParameter With {.ParameterName = String.Format("@{0}", parameterName, value),
                                                                   .Direction = parameterDirection,
                                                                   .Size = fieldSize}
            End Select
        End Sub
        Public Function CreateCommandParametersExplicit(ByVal parameterName As String,
                                                        ByVal fieldType As DbType,
                                                        ByVal parameterDirection As ParameterDirection) As IDataParameter
            Dim mOutputParameter As IDbDataParameter = Nothing
            Dim mParameterValue As Object = Nothing
            Select Case Provider
                Case ProviderType.SqlClient
                    mOutputParameter = New SqlParameter
                    mOutputParameter.ParameterName = String.Format("@{0}", parameterName)
                    CType(mOutputParameter, SqlParameter).SqlDbType = fieldType
                    mOutputParameter.Direction = parameterDirection
                    mOutputParameter.Size = 10
                    _command.Parameters.Add(mOutputParameter)
                Case ProviderType.OleDb
                Case ProviderType.Odbc
            End Select
            Return mOutputParameter
        End Function
#End Region

#Region "SaveChanges"
        Public Function SaveChanges(ByVal query As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As Integer
            _command.CommandText = query
            _command.CommandType = commandType
            Using (_connection)
                Try
                    _connection.Open()
                    Return _command.ExecuteNonQuery
                Catch ex As Exception
                    _errMsg = ex.Message
                Finally
                    _connection.Close()
                    _connection.Dispose()
                    _command.Dispose()
                End Try
            End Using
        End Function
        Public Function SaveChanges(ByVal commandType As CommandType) As Integer
            _command.CommandType = commandType
            Using (_connection)
                Try
                    _connection.Open()
                    Return _command.ExecuteNonQuery
                Catch ex As Exception
                    _errMsg = ex.Message
                Finally
                    _connection.Close()
                    _connection.Dispose()
                    _command.Dispose()
                End Try
            End Using
        End Function
        Public Function SaveChanges(ByVal query As String) As Integer
            _command.CommandText = query
            Using (_connection)
                Try
                    _connection.Open()
                    Return _command.ExecuteNonQuery
                Catch ex As Exception
                    _errMsg = ex.Message
                Finally
                    _connection.Close()
                    _connection.Dispose()
                    _command.Dispose()
                End Try
            End Using
        End Function
        Public Function SaveChanges() As Integer
            Using (_connection)
                Try
                    _connection.Open()
                    Return _command.ExecuteNonQuery
                Catch ex As Exception
                    _errMsg = ex.Message
                Finally
                    _connection.Close()
                    _connection.Dispose()
                    _command.Dispose()
                End Try
            End Using
        End Function
#End Region

#Region "SaveChangesAsync"
        Public Async Function SaveChangesAsync(ByVal query As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As Task(Of Integer)
            _command.CommandText = query
            _command.CommandType = commandType
            Using (_connection)
                Try
                    _connection.Open()

                    'Long process
                    Dim mTask As Task(Of Integer) = New Task(Of Integer)(Function() _command.ExecuteNonQuery)
                    mTask.Start()
                    Return Await mTask

                Catch ex As Exception
                    _errMsg = ex.Message
                    Return 0
                Finally
                    _connection.Close()
                    _connection.Dispose()
                    _command.Dispose()
                End Try
            End Using
        End Function
        Public Async Function SaveChangesAsync(ByVal commandType As CommandType) As Task(Of Integer)
            _command.CommandType = commandType
            Using (_connection)
                Try
                    _connection.Open()

                    'Long process
                    Dim mTask As Task(Of Integer) = New Task(Of Integer)(Function() _command.ExecuteNonQuery)
                    mTask.Start()
                    Return Await mTask

                Catch ex As Exception
                    _errMsg = ex.Message
                    Return 0
                Finally
                    _connection.Close()
                    _connection.Dispose()
                    _command.Dispose()
                End Try
            End Using
        End Function
        Public Async Function SaveChangesAsync(ByVal query As String) As Task(Of Integer)
            _command.CommandText = query
            Using (_connection)
                Try
                    _connection.Open()

                    'Long process
                    Dim mTask As Task(Of Integer) = New Task(Of Integer)(Function() _command.ExecuteNonQuery)
                    mTask.Start()
                    Return Await mTask

                Catch ex As Exception
                    _errMsg = ex.Message
                    Return 0
                Finally
                    _connection.Close()
                    _connection.Dispose()
                    _command.Dispose()
                End Try
            End Using
        End Function
        Public Async Function SaveChangesAsync() As Task(Of Integer)
            Using (_connection)
                Try
                    _connection.Open()

                    'Long process
                    Dim mTask As Task(Of Integer) = New Task(Of Integer)(Function() _command.ExecuteNonQuery)
                    mTask.Start()
                    Return Await mTask

                Catch ex As Exception
                    _errMsg = ex.Message
                    Return 0
                Finally
                    _connection.Close()
                    _connection.Dispose()
                    _command.Dispose()
                End Try
            End Using
        End Function
#End Region

#Region "GetScalarValue"
        Public Function GetScalarValue(ByVal scalarQuery As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As Object
            _command.CommandText = scalarQuery
            _command.CommandType = commandType
            Using (_connection)
                Try
                    _connection.Open()
                    Return _command.ExecuteScalar
                Catch ex As Exception
                    _errMsg = ex.Message
                Finally
                    _connection.Close()
                    _connection.Dispose()
                    _command.Dispose()
                End Try
            End Using
            Return Nothing
        End Function
        Public Function GetScalarValue(Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As Object
            _command.CommandType = commandType
            Using (_connection)
                Try
                    _connection.Open()
                    Return _command.ExecuteScalar
                Catch ex As Exception
                    _errMsg = ex.Message
                Finally
                    _connection.Close()
                    _connection.Dispose()
                    _command.Dispose()
                End Try
            End Using
            Return Nothing
        End Function
        Public Function GetScalarValue(Of T)(ByVal commandType As CommandType) As T
            _command.CommandType = commandType
            Dim mValue As T
            Try
                Using (_connection)
                    _connection.Open()
                    mValue = CType(_command.ExecuteScalar, T)
                End Using
            Catch ex As Exception
                _errMsg = ex.Message
            End Try
            Return mValue
        End Function
#End Region

#Region "GetScalarValueAsync"
        Public Async Function GetScalarValueAsync(ByVal scalarQuery As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As Task(Of Object)
            _command.CommandText = scalarQuery
            _command.CommandType = commandType
            Using (_connection)
                Try
                    _connection.Open()

                    'Long Process
                    Dim mTask As Task(Of Object) = New Task(Of Object)(Function() _command.ExecuteScalar)
                    mTask.Start()
                    Return Await mTask

                Catch ex As Exception
                    _errMsg = ex.Message
                    Return Nothing
                Finally
                    _connection.Close()
                    _connection.Dispose()
                    _command.Dispose()
                End Try
            End Using
            Return Nothing
        End Function
        Public Async Function GetScalarValueAsync(Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As Task(Of Object)
            _command.CommandType = commandType
            Using (_connection)
                Try
                    _connection.Open()

                    'Long Process
                    Dim mTask As Task(Of Object) = New Task(Of Object)(Function() _command.ExecuteScalar)
                    mTask.Start()
                    Return Await mTask

                Catch ex As Exception
                    _errMsg = ex.Message
                    Return Nothing
                Finally
                    _connection.Close()
                    _connection.Dispose()
                    _command.Dispose()
                End Try
            End Using
            Return Nothing
        End Function
        Public Async Function GetScalarValueAsync(Of T)(ByVal commandType As CommandType) As Task(Of T)
            _command.CommandType = commandType
            Dim mValue As T
            Try
                Using (_connection)
                    _connection.Open()

                    'Long Process
                    Dim mTask As Task(Of T) = New Task(Of T)(Function() CType(_command.ExecuteScalar, T))
                    mTask.Start()
                    mValue = Await mTask
                    Return mValue

                End Using
            Catch ex As Exception
                _errMsg = ex.Message
                Return Nothing
            End Try
            Return mValue
        End Function
#End Region

#Region "GetdataReader"
        Public Function GetdataReader(ByVal parameterName As String, ByVal query As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As List(Of Object)
            Dim mList As New List(Of Object)
            _command.CommandText = query
            _command.CommandType = commandType
            Using (_connection)
                Try
                    _connection.Open()
                    _dataReader = _command.ExecuteReader
                    While (_dataReader.Read)
                        mList.Add(_dataReader(parameterName))
                    End While
                Catch ex As Exception
                    _errMsg = ex.Message
                Finally
                    _connection.Close()
                    _connection.Dispose()
                    _command.Dispose()
                End Try
            End Using
            Return mList
        End Function
        Public Function GetdataReader(ByVal parameterName As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As List(Of Object)
            Dim mList As New List(Of Object)
            _command.CommandType = commandType
            Using (_connection)
                Try
                    _connection.Open()
                    _command.CommandType = CommandType.StoredProcedure
                    _dataReader = _command.ExecuteReader
                    While (_dataReader.Read)
                        mList.Add(_dataReader(parameterName))
                    End While
                Catch ex As Exception
                    _errMsg = ex.Message
                Finally
                    _connection.Close()
                    _connection.Dispose()
                    _command.Dispose()
                End Try
            End Using
            Return mList
        End Function
        Public Function GetdataReader(Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As IDataReader
            _command.CommandType = commandType
            Try
                _connection.Open()
                _dataReader = _command.ExecuteReader(CommandBehavior.CloseConnection)
            Catch ex As Exception
                _errMsg = ex.Message
                _connection.Close()
                _connection.Dispose()
                _command.Dispose()
            Finally
            End Try
            Return _dataReader
        End Function
#End Region

#Region "GetdataReaderAsync"
        Public Async Function GetdataReaderAsync(ByVal parameterName As String, ByVal query As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As Task(Of List(Of Object))
            Dim mList As New List(Of Object)
            _command.CommandText = query
            _command.CommandType = commandType
            Using (_connection)
                Try
                    _connection.Open()

                    'Long Process
                    Dim mTask As Task(Of List(Of Object)) = New Task(Of List(Of Object))(Function() _command.ExecuteReader)
                    mTask.Start()
                    _dataReader = Await mTask

                    While (_dataReader.Read)
                        mList.Add(_dataReader(parameterName))
                    End While
                Catch ex As Exception
                    _errMsg = ex.Message
                Finally
                    _connection.Close()
                    _connection.Dispose()
                    _command.Dispose()
                End Try
            End Using
            Return mList
        End Function
        Public Async Function GetdataReaderAsync(ByVal parameterName As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As Task(Of List(Of Object))
            Dim mList As New List(Of Object)
            _command.CommandType = commandType
            Using (_connection)
                Try
                    _connection.Open()
                    _command.CommandType = CommandType.StoredProcedure

                    'Long Process
                    Dim mTask As Task(Of List(Of Object)) = New Task(Of List(Of Object))(Function() _command.ExecuteReader)
                    mTask.Start()
                    _dataReader = Await mTask

                    While (_dataReader.Read)
                        mList.Add(_dataReader(parameterName))
                    End While
                Catch ex As Exception
                    _errMsg = ex.Message
                Finally
                    _connection.Close()
                    _connection.Dispose()
                    _command.Dispose()
                End Try
            End Using
            Return mList
        End Function
        Public Async Function GetdataReaderAsync(Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As Task(Of IDataReader)
            _command.CommandType = commandType
            Try
                _connection.Open()
                'Long Process
                Dim mTask As Task(Of IDataReader) = New Task(Of IDataReader)(Function() _command.ExecuteReader(CommandBehavior.CloseConnection))
                mTask.Start()
                _dataReader = Await mTask

            Catch ex As Exception
                _errMsg = ex.Message
                _connection.Close()
                _connection.Dispose()
                _command.Dispose()
            Finally
            End Try
            Return _dataReader
        End Function
#End Region

#Region "GetDataTable"
        Public Function GetDataTable(ByVal tableIndex As Integer, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As DataTable
            _command.CommandType = commandType
            Select Case (1 = 1)
                Case TypeOf (_dataAdapter) Is SqlDataAdapter
                    CType(_dataAdapter, SqlDataAdapter).SelectCommand = _command
                Case TypeOf (_dataAdapter) Is OleDbDataAdapter
                    CType(_dataAdapter, OleDbDataAdapter).SelectCommand = _command
                Case TypeOf (_dataAdapter) Is OdbcDataAdapter
                    CType(_dataAdapter, OdbcDataAdapter).SelectCommand = _command
            End Select
            Try
                _dataAdapter.Fill(_dataSet)
            Catch ex As Exception
                _errMsg = ex.Message
            End Try
            Return _dataSet.Tables(tableIndex)
        End Function
        Public Function GetDataTable(ByVal selectQuery As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As DataTable
            _command.CommandText = selectQuery
            _command.CommandType = commandType
            Select Case (1 = 1)
                Case TypeOf (_dataAdapter) Is SqlDataAdapter
                    CType(_dataAdapter, SqlDataAdapter).SelectCommand = _command
                Case TypeOf (_dataAdapter) Is OleDbDataAdapter
                    CType(_dataAdapter, OleDbDataAdapter).SelectCommand = _command
                Case TypeOf (_dataAdapter) Is OdbcDataAdapter
                    CType(_dataAdapter, OdbcDataAdapter).SelectCommand = _command
            End Select
            Try
                _dataAdapter.Fill(_dataSet)
            Catch ex As Exception
                _errMsg = ex.Message
            End Try
            CheckForRow(_dataSet.Tables(0).Rows.Count)
            Return _dataSet.Tables(0)
        End Function
        Public Function GetDataTable(ByVal tableIndex As Integer, ByVal selectQuery As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As DataTable
            _command.CommandText = selectQuery
            _command.CommandType = commandType
            Select Case (1 = 1)
                Case TypeOf (_dataAdapter) Is SqlDataAdapter
                    CType(_dataAdapter, SqlDataAdapter).SelectCommand = _command
                Case TypeOf (_dataAdapter) Is OleDbDataAdapter
                    CType(_dataAdapter, OleDbDataAdapter).SelectCommand = _command
                Case TypeOf (_dataAdapter) Is OdbcDataAdapter
                    CType(_dataAdapter, OdbcDataAdapter).SelectCommand = _command
            End Select
            Try
                _dataAdapter.Fill(_dataSet)
            Catch ex As Exception
                _errMsg = ex.Message
            End Try
            CheckForRow(_dataSet.Tables(0).Rows.Count)
            Return _dataSet.Tables(tableIndex)
        End Function
#End Region

#Region "GetDataTableAsync"
        Public Async Function GetDataTableAsync(ByVal tableIndex As Integer, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As Task(Of DataTable)
            _command.CommandType = commandType
            Select Case (1 = 1)
                Case TypeOf (_dataAdapter) Is SqlDataAdapter
                    CType(_dataAdapter, SqlDataAdapter).SelectCommand = _command
                Case TypeOf (_dataAdapter) Is OleDbDataAdapter
                    CType(_dataAdapter, OleDbDataAdapter).SelectCommand = _command
                Case TypeOf (_dataAdapter) Is OdbcDataAdapter
                    CType(_dataAdapter, OdbcDataAdapter).SelectCommand = _command
            End Select
            Try

                'Long Process
                Dim mTask As Task = New Task(Function() _dataAdapter.Fill(_dataSet))
                mTask.Start()
                Await mTask

            Catch ex As Exception
                _errMsg = ex.Message
            End Try
            Return _dataSet.Tables(tableIndex)
        End Function
        Public Async Function GetDataTableAsync(ByVal selectQuery As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As Task(Of DataTable)
            _command.CommandText = selectQuery
            _command.CommandType = commandType
            Select Case (1 = 1)
                Case TypeOf (_dataAdapter) Is SqlDataAdapter
                    CType(_dataAdapter, SqlDataAdapter).SelectCommand = _command
                Case TypeOf (_dataAdapter) Is OleDbDataAdapter
                    CType(_dataAdapter, OleDbDataAdapter).SelectCommand = _command
                Case TypeOf (_dataAdapter) Is OdbcDataAdapter
                    CType(_dataAdapter, OdbcDataAdapter).SelectCommand = _command
            End Select
            Try
                'Long Process
                Dim mTask As Task = New Task(Function() _dataAdapter.Fill(_dataSet))
                mTask.Start()
                Await mTask

            Catch ex As Exception
                _errMsg = ex.Message
            End Try
            CheckForRow(_dataSet.Tables(0).Rows.Count)
            Return _dataSet.Tables(0)
        End Function
        Public Async Function GetDataTableAsync(ByVal tableIndex As Integer, ByVal selectQuery As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As Task(Of DataTable)
            _command.CommandText = selectQuery
            _command.CommandType = commandType
            Select Case (1 = 1)
                Case TypeOf (_dataAdapter) Is SqlDataAdapter
                    CType(_dataAdapter, SqlDataAdapter).SelectCommand = _command
                Case TypeOf (_dataAdapter) Is OleDbDataAdapter
                    CType(_dataAdapter, OleDbDataAdapter).SelectCommand = _command
                Case TypeOf (_dataAdapter) Is OdbcDataAdapter
                    CType(_dataAdapter, OdbcDataAdapter).SelectCommand = _command
            End Select
            Try
                'Long Process
                Dim mTask As Task = New Task(Function() _dataAdapter.Fill(_dataSet))
                mTask.Start()
                Await mTask

            Catch ex As Exception
                _errMsg = ex.Message
            End Try
            CheckForRow(_dataSet.Tables(0).Rows.Count)
            Return _dataSet.Tables(tableIndex)
        End Function
#End Region

#Region "GetDataSet"
        Public Function GetDataSet(ByVal selectQuery As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As DataSet
            _command.CommandText = selectQuery
            _command.CommandType = commandType
            Select Case (1 = 1)
                Case TypeOf (_dataAdapter) Is SqlDataAdapter
                    CType(_dataAdapter, SqlDataAdapter).SelectCommand = _command
                Case TypeOf (_dataAdapter) Is OleDbDataAdapter
                    CType(_dataAdapter, OleDbDataAdapter).SelectCommand = _command
                Case TypeOf (_dataAdapter) Is OdbcDataAdapter
                    CType(_dataAdapter, OdbcDataAdapter).SelectCommand = _command
            End Select
            Try
                _dataAdapter.Fill(_dataSet)
            Catch ex As Exception
                _errMsg = ex.Message
            End Try
            Return _dataSet
        End Function
        Public Function GetDataSet(Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As DataSet
            _command.CommandType = commandType
            Select Case (1 = 1)
                Case TypeOf (_dataAdapter) Is SqlDataAdapter
                    CType(_dataAdapter, SqlDataAdapter).SelectCommand = _command
                Case TypeOf (_dataAdapter) Is OleDbDataAdapter
                    CType(_dataAdapter, OleDbDataAdapter).SelectCommand = _command
                Case TypeOf (_dataAdapter) Is OdbcDataAdapter
                    CType(_dataAdapter, OdbcDataAdapter).SelectCommand = _command
            End Select
            Try
                _dataAdapter.Fill(_dataSet)
            Catch ex As Exception
                _errMsg = ex.Message
            End Try
            Return _dataSet
        End Function
#End Region

#Region "GetDataSetAsync"
        Public Async Function GetDataSetAsync(ByVal selectQuery As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As Task(Of DataSet)
            _command.CommandText = selectQuery
            _command.CommandType = commandType
            Select Case (1 = 1)
                Case TypeOf (_dataAdapter) Is SqlDataAdapter
                    CType(_dataAdapter, SqlDataAdapter).SelectCommand = _command
                Case TypeOf (_dataAdapter) Is OleDbDataAdapter
                    CType(_dataAdapter, OleDbDataAdapter).SelectCommand = _command
                Case TypeOf (_dataAdapter) Is OdbcDataAdapter
                    CType(_dataAdapter, OdbcDataAdapter).SelectCommand = _command
            End Select
            Try
                'Long Process
                Dim mTask As Task = New Task(Function() _dataAdapter.Fill(_dataSet))
                mTask.Start()
                Await mTask

            Catch ex As Exception
                _errMsg = ex.Message
            End Try
            Return _dataSet
        End Function
        Public Async Function GetDataSetAsync(Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As Task(Of DataSet)
            _command.CommandType = commandType
            Select Case (1 = 1)
                Case TypeOf (_dataAdapter) Is SqlDataAdapter
                    CType(_dataAdapter, SqlDataAdapter).SelectCommand = _command
                Case TypeOf (_dataAdapter) Is OleDbDataAdapter
                    CType(_dataAdapter, OleDbDataAdapter).SelectCommand = _command
                Case TypeOf (_dataAdapter) Is OdbcDataAdapter
                    CType(_dataAdapter, OdbcDataAdapter).SelectCommand = _command
            End Select
            Try
                'Long Process
                Dim mTask As Task = New Task(Function() _dataAdapter.Fill(_dataSet))
                mTask.Start()
                Await mTask

            Catch ex As Exception
                _errMsg = ex.Message
            End Try
            Return _dataSet
        End Function
#End Region

#Region "CheckForRow"
        Private Sub CheckForRow(ByVal rowCount As Integer)
            If rowCount > 0 Then
                _hasRow = True
            Else
                _hasRow = False
            End If
        End Sub
#End Region


    End Class

End Namespace