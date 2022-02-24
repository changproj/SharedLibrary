
Imports System.Data.Odbc
Imports System.Threading.Tasks

Namespace DataAccess

    Public Class OdbcBuilder
        Inherits DatabaseBuilder
        Private _connection As OdbcConnection
        Private _command As OdbcCommand
        Private _dataAdapter As OdbcDataAdapter
        Private _dataReader As OdbcDataReader

#Region "Property"
        Public Sub New(connectionString As String)
            MyBase.New(connectionString)
            _providerType = ProviderType.Odbc
            _command = New OdbcCommand()
        End Sub
#End Region

#Region "CreateCommandParameters"
        Public Overrides Sub ClearParameters()
            _command.Parameters.Clear()
        End Sub
        Public Overrides Sub DisposeCommand()
            _command.Dispose()
        End Sub
        Public Overrides Sub CreateCommandParameters(ByVal parameterName As String, ByVal value As Object)
            _command.Parameters.Add(New OdbcParameter(String.Format("@{0}", parameterName), value))
        End Sub
        Public Overrides Sub CreateCommandParameters(ByVal parameterName As String, ByVal value As Object, ByVal fieldType As DbType, ByVal TypeName As String)
        End Sub

        Public Overrides Sub CreateCommandParametersExplicit(ByVal parameterName As String, ByVal value As Object, ByVal parameterDirection As ParameterDirection, ByVal fieldSize As Integer)
            Dim mParameterInputOutput As OdbcParameter = Nothing
            mParameterInputOutput = New OdbcParameter With {.ParameterName = String.Format("@{0}", parameterName, value),
                                                                           .Direction = parameterDirection,
                                                                           .Size = fieldSize}
        End Sub

        Public Overrides Function CreateCommandParametersExplicit(ByVal parameterName As String, ByVal fieldType As DbType, ByVal parameterDirection As ParameterDirection) As IDataParameter
            Dim mOutputParameter As OdbcParameter = Nothing
            Dim mParameterValue As Object = Nothing
            mOutputParameter = New OdbcParameter
            mOutputParameter.ParameterName = String.Format("@{0}", parameterName)
            CType(mOutputParameter, OdbcParameter).OdbcType = fieldType
            mOutputParameter.Direction = parameterDirection
            mOutputParameter.Size = 10
            _command.Parameters.Add(mOutputParameter)
            Return mOutputParameter
        End Function
#End Region

#Region "Get Parameter"
        Public Overrides Function GetCommandParameter(ByVal parameterName As String) As IDataParameter
            Return _command.Parameters.Item(String.Format("@{0}", parameterName))
        End Function
#End Region

#Region "SaveChanges"
        Public Overrides Function SaveChanges(ByVal query As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As Integer
            Using _connection = New OdbcConnection(_globalConnectionString)
                Dim mTransaction As OdbcTransaction
                _connection.Open()
                mTransaction = _connection.BeginTransaction()
                Try
                    _command.Connection = _connection
                    _command.CommandText = query
                    _command.CommandType = commandType
                    _command.CommandTimeout = 0
                    _command.Transaction = mTransaction
                    _command.CommandTimeout = 0
                    Return _command.ExecuteNonQuery
                    _errMsg = Nothing
                    mTransaction.Commit()
                Catch ex As Exception
                    _errMsg = ex.Message
                    mTransaction.Rollback()
                Finally
                    ClearParameters()
                    DisposeCommand()
                End Try
            End Using
        End Function
        'added
        Public Overrides Function SaveChanges(ByVal queryList As List(Of String)) As Integer    'no parameters
            Using _connection = New OdbcConnection(_globalConnectionString)
                Dim mTransaction As OdbcTransaction
                Dim mRowAffected As Integer = 0
                _connection.Open()
                mTransaction = _connection.BeginTransaction()
                Dim mCommand = New OdbcCommand()
                Try
                    For Each mSQL As String In queryList
                        mCommand.Connection = _connection
                        mCommand.CommandText = mSQL
                        mCommand.CommandType = CommandType.Text
                        mCommand.CommandTimeout = 0
                        mCommand.Transaction = mTransaction
                        mCommand.CommandTimeout = 0
                        mRowAffected = mCommand.ExecuteNonQuery
                    Next
                    _errMsg = Nothing
                    mTransaction.Commit()
                    Return mRowAffected
                Catch ex As Exception
                    _errMsg = ex.Message
                    mTransaction.Rollback()
                Finally
                    mCommand.Parameters.Clear()
                    mCommand.Dispose()
                End Try
            End Using
        End Function
#End Region

#Region "SaveChangesAsync"
        Public Overrides Async Function SaveChangesAsync(ByVal query As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As Task(Of Integer)
            Using _connection = New OdbcConnection(_globalConnectionString)
                Dim mTransaction As OdbcTransaction
                _connection.Open()
                mTransaction = _connection.BeginTransaction()
                Try
                    _command.Connection = _connection
                    _command.CommandText = query
                    _command.CommandType = commandType
                    _command.CommandTimeout = 0
                    _command.Transaction = mTransaction
                    _command.CommandTimeout = 0

                    'Long Process
                    Dim mTask As Task(Of Integer) = New Task(Of Integer)(Function() _command.ExecuteNonQuery)
                    mTask.Start()
                    Return Await mTask

                    _errMsg = Nothing
                    mTransaction.Commit()
                Catch ex As Exception
                    _errMsg = ex.Message
                    mTransaction.Rollback()
                    Return 0
                Finally
                    ClearParameters()
                    DisposeCommand()
                End Try
            End Using
        End Function
        'added
        Public Overrides Async Function SaveChangesAsync(ByVal queryList As List(Of String)) As Task(Of Integer)    'no parameters
            Using _connection = New OdbcConnection(_globalConnectionString)
                Dim mTransaction As OdbcTransaction
                Dim mRowAffected As Integer = 0
                _connection.Open()
                mTransaction = _connection.BeginTransaction()
                Dim mCommand = New OdbcCommand()
                Try
                    For Each mSQL As String In queryList
                        mCommand.Connection = _connection
                        mCommand.CommandText = mSQL
                        mCommand.CommandType = CommandType.Text
                        mCommand.CommandTimeout = 0
                        mCommand.Transaction = mTransaction
                        mCommand.CommandTimeout = 0

                        'Long Process
                        Dim mTask As Task(Of Integer) = New Task(Of Integer)(Function() mCommand.ExecuteNonQuery)
                        mTask.Start()
                        mRowAffected = Await mTask

                        Return mRowAffected
                    Next
                    _errMsg = Nothing
                    mTransaction.Commit()
                    Return mRowAffected
                Catch ex As Exception
                    _errMsg = ex.Message
                    mTransaction.Rollback()
                    Return 0
                Finally
                    mCommand.Parameters.Clear()
                    mCommand.Dispose()
                End Try
            End Using
        End Function
#End Region

#Region "GetScalarValue"
        Public Overrides Function GetScalarValue(ByVal scalarQuery As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As Object
            Using _connection = New OdbcConnection(_globalConnectionString)
                Try
                    _command.Connection = _connection
                    _command.CommandText = scalarQuery
                    _command.CommandType = commandType
                    _command.CommandTimeout = 0
                    _connection.Open()
                    Return _command.ExecuteScalar
                    _errMsg = Nothing
                Catch ex As Exception
                    _errMsg = ex.Message
                Finally
                    ClearParameters()
                    DisposeCommand()
                End Try
                Return Nothing
            End Using
        End Function
        Public Overrides Function GetScalarValue(Of T)(ByVal scalarQuery As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As T
            Dim mValue As T
            Using _connection = New OdbcConnection(_globalConnectionString)
                Try
                    _command.Connection = _connection
                    _command.CommandText = scalarQuery
                    _command.CommandType = commandType
                    _command.CommandTimeout = 0
                    _connection.Open()
                    mValue = CType(_command.ExecuteScalar, T)
                    _errMsg = Nothing
                Catch ex As Exception
                    _errMsg = ex.Message
                Finally
                    ClearParameters()
                    DisposeCommand()
                End Try
                Return mValue
            End Using
        End Function
#End Region

#Region "GetScalarValueAsync"
        Public Overrides Async Function GetScalarValueAsync(ByVal scalarQuery As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As Task(Of Object)
            Using _connection = New OdbcConnection(_globalConnectionString)
                Try
                    _command.Connection = _connection
                    _command.CommandText = scalarQuery
                    _command.CommandType = commandType
                    _command.CommandTimeout = 0
                    _connection.Open()

                    'Long Process
                    Dim mTask As Task(Of Object) = New Task(Of Object)(Function() _command.ExecuteScalar)
                    mTask.Start()
                    Return Await mTask

                    _errMsg = Nothing
                Catch ex As Exception
                    _errMsg = ex.Message
                Finally
                    ClearParameters()
                    DisposeCommand()
                End Try
                Return Nothing
            End Using
        End Function
        Public Overrides Async Function GetScalarValueAsync(Of T)(ByVal scalarQuery As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As Task(Of T)
            Dim mValue As T
            Using _connection = New OdbcConnection(_globalConnectionString)
                Try
                    _command.Connection = _connection
                    _command.CommandText = scalarQuery
                    _command.CommandType = commandType
                    _command.CommandTimeout = 0
                    _connection.Open()

                    'Long Process
                    Dim mTask As Task(Of Object) = New Task(Of Object)(Function() CType(_command.ExecuteScalar, T))
                    mTask.Start()
                    mValue = Await mTask

                    _errMsg = Nothing
                Catch ex As Exception
                    _errMsg = ex.Message
                Finally
                    ClearParameters()
                    DisposeCommand()
                End Try
                Return mValue
            End Using
        End Function
#End Region

#Region "GetdataReader"
        Public Overrides Function GetdataReader(ByVal parameterName As String, ByVal query As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As List(Of Object)
            Dim mList As New List(Of Object)
            Using _connection = New OdbcConnection(_globalConnectionString)
                Try
                    _command.Connection = _connection
                    _command.CommandText = query
                    _command.CommandType = commandType
                    _command.CommandTimeout = 0
                    _connection.Open()
                    Using _dataReader = _command.ExecuteReader
                        While (_dataReader.Read)
                            mList.Add(_dataReader(parameterName))
                        End While
                    End Using
                    _errMsg = Nothing
                Catch ex As Exception
                    _errMsg = ex.Message
                Finally
                    ClearParameters()
                    DisposeCommand()
                End Try
                Return mList
            End Using
        End Function
        Public Overrides Function GetdataReader(ByVal query As Object, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As List(Of Object)
            Dim mList As New List(Of Object)

            Using _connection = New OdbcConnection(_globalConnectionString)
                Try
                    _command.Connection = _connection
                    _command.CommandText = query
                    _command.CommandType = commandType
                    _command.CommandTimeout = 0
                    _connection.Open()
                    Using _dataReader = _command.ExecuteReader
                        While (_dataReader.Read)
                            Dim mObject(_dataReader.FieldCount) As Object
                            Dim mFieldFilledCount As Integer = _dataReader.GetValues(mObject)
                            mList.Add(mObject)
                        End While
                    End Using
                    _errMsg = Nothing
                Catch ex As Exception
                    _errMsg = ex.Message
                Finally
                    ClearParameters()
                    DisposeCommand()
                End Try
                Return mList
            End Using
        End Function
        Public Overrides Function GetdataReader(ByVal query As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As IDataReader
            Try
                _connection = New OdbcConnection(_globalConnectionString)
                _command.Connection = _connection
                _command.CommandText = query
                _command.CommandType = commandType
                _command.CommandTimeout = 0
                _connection.Open()
                _dataReader = _command.ExecuteReader(CommandBehavior.CloseConnection)
                _errMsg = Nothing
            Catch ex As Exception
                _errMsg = ex.Message
            Finally
                ClearParameters()
                DisposeCommand()
            End Try
            Return _dataReader
        End Function
#End Region

#Region "GetdataReaderAsync"
        Public Overrides Async Function GetdataReaderAsync(ByVal parameterName As String, ByVal query As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As Task(Of List(Of Object))
            Dim mList As New List(Of Object)
            Using _connection = New OdbcConnection(_globalConnectionString)
                Try
                    _command.Connection = _connection
                    _command.CommandText = query
                    _command.CommandType = commandType
                    _command.CommandTimeout = 0
                    _connection.Open()

                    'Long Process
                    Dim mTask As Task(Of OdbcDataReader) = New Task(Of OdbcDataReader)(Function() _command.ExecuteReader)
                    mTask.Start()

                    Using _dataReader = Await mTask
                        While (_dataReader.Read)
                            mList.Add(_dataReader(parameterName))
                        End While
                    End Using
                    _errMsg = Nothing
                Catch ex As Exception
                    _errMsg = ex.Message
                Finally
                    ClearParameters()
                    DisposeCommand()
                End Try
                Return mList
            End Using
        End Function
        Public Overrides Async Function GetdataReaderAsync(ByVal query As Object, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As Task(Of List(Of Object))
            Dim mList As New List(Of Object)

            Using _connection = New OdbcConnection(_globalConnectionString)
                Try
                    _command.Connection = _connection
                    _command.CommandText = query
                    _command.CommandType = commandType
                    _command.CommandTimeout = 0
                    _connection.Open()

                    'Long Process
                    Dim mTask As Task(Of OdbcDataReader) = New Task(Of OdbcDataReader)(Function() _command.ExecuteReader)
                    mTask.Start()

                    Using _dataReader = Await mTask
                        While (_dataReader.Read)
                            Dim mObject(_dataReader.FieldCount) As Object
                            Dim mFieldFilledCount As Integer = _dataReader.GetValues(mObject)
                            mList.Add(mObject)
                        End While
                    End Using
                    _errMsg = Nothing
                Catch ex As Exception
                    _errMsg = ex.Message
                Finally
                    ClearParameters()
                    DisposeCommand()
                End Try
                Return mList
            End Using
        End Function
        Public Overrides Async Function GetdataReaderAsync(ByVal query As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As Task(Of IDataReader)
            Try
                _connection = New OdbcConnection(_globalConnectionString)
                _command.Connection = _connection
                _command.CommandText = query
                _command.CommandType = commandType
                _command.CommandTimeout = 0
                _connection.Open()

                'Long Process
                Dim mTask As Task(Of OdbcDataReader) = New Task(Of OdbcDataReader)(Function() _command.ExecuteReader(CommandBehavior.CloseConnection))
                mTask.Start()

                _dataReader = Await mTask
                _errMsg = Nothing
            Catch ex As Exception
                _errMsg = ex.Message
            Finally
                ClearParameters()
                DisposeCommand()
            End Try
            Return _dataReader
        End Function
#End Region

#Region "GetDataTable"
        Public Overrides Function GetDataTable(ByVal tableIndex As Integer, ByVal selectQuery As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As DataTable
            Using _connection = New OdbcConnection(_globalConnectionString)
                Using _dataAdapter = New OdbcDataAdapter
                    Try
                        _command.Connection = _connection
                        _command.CommandText = selectQuery
                        _command.CommandType = commandType
                        _command.CommandTimeout = 0
                        _dataAdapter.SelectCommand = _command
                        _connection.Open()
                        _dataSet = New DataSet
                        _dataAdapter.Fill(_dataSet)
                        _errMsg = Nothing
                    Catch ex As Exception
                        _errMsg = ex.Message
                    Finally
                        ClearParameters()
                        DisposeCommand()
                    End Try
                    CheckForRow(_dataSet.Tables(0).Rows.Count)
                    Return _dataSet.Tables(tableIndex)
                End Using
            End Using
        End Function
        Public Overrides Function GetDataTable(ByVal selectQuery As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As DataTable
            Using _connection = New OdbcConnection(_globalConnectionString)
                Dim mTransaction As OdbcTransaction
                _connection.Open()
                mTransaction = _connection.BeginTransaction()
                Using _dataAdapter = New OdbcDataAdapter
                    Try
                        _command.Connection = _connection
                        _command.CommandText = selectQuery
                        _command.CommandType = commandType
                        _command.CommandTimeout = 0
                        _command.Transaction = mTransaction
                        _dataAdapter.SelectCommand = _command
                        _dataSet = New DataSet
                        _dataAdapter.Fill(_dataSet)
                        _errMsg = Nothing
                        CheckForRow(_dataSet.Tables(0).Rows.Count)
                        mTransaction.Commit()
                        Return _dataSet.Tables(0)
                    Catch ex As Exception
                        _errMsg = ex.Message
                        mTransaction.Rollback()
                    Finally
                        ClearParameters()
                        DisposeCommand()
                    End Try
                    Return Nothing
                End Using
            End Using
        End Function
#End Region

#Region "GetDataTableAsync"
        Public Overrides Async Function GetDataTableAsync(ByVal tableIndex As Integer, ByVal selectQuery As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As Task(Of DataTable)
            Using _connection = New OdbcConnection(_globalConnectionString)
                Using _dataAdapter = New OdbcDataAdapter
                    Try
                        _command.Connection = _connection
                        _command.CommandText = selectQuery
                        _command.CommandType = commandType
                        _command.CommandTimeout = 0
                        _dataAdapter.SelectCommand = _command
                        _connection.Open()
                        _dataSet = New DataSet

                        'Long Process
                        Dim mTask As Task = New Task(Function() _dataAdapter.Fill(_dataSet))
                        mTask.Start()
                        Await mTask

                        _errMsg = Nothing
                    Catch ex As Exception
                        _errMsg = ex.Message
                    Finally
                        ClearParameters()
                        DisposeCommand()
                    End Try
                    CheckForRow(_dataSet.Tables(0).Rows.Count)
                    Return _dataSet.Tables(tableIndex)
                End Using
            End Using
        End Function
        Public Overrides Async Function GetDataTableAsync(ByVal selectQuery As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As Task(Of DataTable)
            Using _connection = New OdbcConnection(_globalConnectionString)
                Dim mTransaction As OdbcTransaction
                _connection.Open()
                mTransaction = _connection.BeginTransaction()
                Using _dataAdapter = New OdbcDataAdapter
                    Try
                        _command.Connection = _connection
                        _command.CommandText = selectQuery
                        _command.CommandType = commandType
                        _command.CommandTimeout = 0
                        _command.Transaction = mTransaction
                        _dataAdapter.SelectCommand = _command
                        _dataSet = New DataSet

                        'Long Process
                        Dim mTask As Task = New Task(Function() _dataAdapter.Fill(_dataSet))
                        mTask.Start()
                        Await mTask

                        _errMsg = Nothing
                        CheckForRow(_dataSet.Tables(0).Rows.Count)
                        mTransaction.Commit()
                        Return _dataSet.Tables(0)
                    Catch ex As Exception
                        _errMsg = ex.Message
                        mTransaction.Rollback()
                    Finally
                        ClearParameters()
                        DisposeCommand()
                    End Try
                    Return Nothing
                End Using
            End Using
        End Function
#End Region

#Region "GetDataSet"
        Public Overrides Function GetDataSet(ByVal selectQuery As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As DataSet
            Using _connection = New OdbcConnection(_globalConnectionString)
                Dim mTransaction As OdbcTransaction
                _connection.Open()
                mTransaction = _connection.BeginTransaction()
                Using _dataAdapter = New OdbcDataAdapter
                    Try
                        _command.Connection = _connection
                        _command.CommandText = selectQuery
                        _command.CommandType = commandType
                        _command.CommandTimeout = 0
                        _command.Transaction = mTransaction
                        _dataAdapter.SelectCommand = _command
                        _dataSet = New DataSet
                        _dataAdapter.Fill(_dataSet)
                        _errMsg = Nothing
                        mTransaction.Commit()
                    Catch ex As Exception
                        _errMsg = ex.Message
                        mTransaction.Rollback()
                    Finally
                        ClearParameters()
                        DisposeCommand()
                    End Try
                    Return _dataSet
                End Using
            End Using
        End Function
#End Region

#Region "GetDataSetAsync"
        Public Overrides Async Function GetDataSetAsync(ByVal selectQuery As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As Task(Of DataSet)
            Using _connection = New OdbcConnection(_globalConnectionString)
                Dim mTransaction As OdbcTransaction
                _connection.Open()
                mTransaction = _connection.BeginTransaction()
                Using _dataAdapter = New OdbcDataAdapter
                    Try
                        _command.Connection = _connection
                        _command.CommandText = selectQuery
                        _command.CommandType = commandType
                        _command.CommandTimeout = 0
                        _command.Transaction = mTransaction
                        _dataAdapter.SelectCommand = _command
                        _dataSet = New DataSet

                        'Long Process
                        Dim mTask As Task = New Task(Function() _dataAdapter.Fill(_dataSet))
                        mTask.Start()
                        Await mTask

                        _errMsg = Nothing
                        mTransaction.Commit()
                    Catch ex As Exception
                        _errMsg = ex.Message
                        mTransaction.Rollback()
                    Finally
                        ClearParameters()
                        DisposeCommand()
                    End Try
                    Return _dataSet
                End Using
            End Using
        End Function
#End Region

    End Class


End Namespace

