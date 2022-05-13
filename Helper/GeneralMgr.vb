Imports System.ComponentModel
Imports DataAccess

Namespace DataEntityMgr

    Public Class GeneralMgr


#Region "Enum"
#End Region

#Region "Fields"
        Private _connStr As String = String.Empty
        Private _errorMsg As String = String.Empty
        Protected _dataEntity As Object
        Protected _dataEntityList As IEnumerable(Of Object)
        Protected _databaseBuilder As DatabaseBuilder
#End Region

#Region "Event"
        Public WithEvents _backgroundWorker As BackgroundWorker
#End Region

#Region "Constructor"
        Public Sub New(ByVal connstr As String)
            _connStr = connstr
        End Sub
#End Region

#Region "Property "
        Public Property ConnStr() As String
            Get
                Return Me._connStr
            End Get
            Set(ByVal value As String)
                Me._connStr = value
            End Set
        End Property
        Public Property ErrorMsg() As String
            Get
                Return _errorMsg
            End Get
            Set(ByVal value As String)
                _errorMsg = value
            End Set
        End Property
        Public Property DataEntity() As Object
            Get
                Return _dataEntity
            End Get
            Set(value As Object)
                _dataEntity = value
            End Set
        End Property
        Public Property DataEntityList() As IEnumerable(Of Object)
            Get
                Return _dataEntityList
            End Get
            Set(value As IEnumerable(Of Object))
                _dataEntityList = value
            End Set
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
#End Region

#Region "Method"
        Public Function GetDataTable(ByVal sql As String) As DataTable
            Try
                Dim mDatabaseBuilder As DatabaseBuilder = New SqlClientBuilder(ConnStr)
                Dim mDataTable As DataTable = mDatabaseBuilder.GetDataTable(sql, CommandType.Text)
                If Not String.IsNullOrEmpty(mDatabaseBuilder.ErrorMsg) Then
                    ErrorMsg = mDatabaseBuilder.ErrorMsg
                End If
                Return mDataTable
            Catch ex As Exception
                ErrorMsg = ex.Message
            End Try
            Return Nothing
        End Function
        Public Function GetDataTable(ByVal sql As String, ByVal parameters As List(Of KeyValueHelper), Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As DataTable
            Try
                Dim mDatabaseBuilder As DatabaseBuilder = New SqlClientBuilder(ConnStr)
                If parameters IsNot Nothing Then
                    For Each param As KeyValueHelper In parameters
                        mDatabaseBuilder.CreateCommandParameters(param.Key, param.Value)
                    Next
                End If
                Dim mDataTable As DataTable = mDatabaseBuilder.GetDataTable(sql, commandType)
                If Not String.IsNullOrEmpty(mDatabaseBuilder.ErrorMsg) Then
                    ErrorMsg = mDatabaseBuilder.ErrorMsg
                End If
                Return mDataTable
            Catch ex As Exception
                ErrorMsg = ex.Message
            End Try
            Return Nothing
        End Function
        Public Async Function GetDataTableAsync(ByVal sql As String, ByVal parameters As List(Of KeyValueHelper), Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As Threading.Tasks.Task(Of DataTable)
            Try
                Dim mDatabaseBuilder As DatabaseBuilder = New SqlClientBuilder(ConnStr)
                If parameters IsNot Nothing Then
                    For Each param As KeyValueHelper In parameters
                        mDatabaseBuilder.CreateCommandParameters(param.Key, param.Value)
                    Next
                End If

                'Long process
                Dim mTask As Task(Of DataTable) = New Task(Of DataTable)(Function() mDatabaseBuilder.GetDataTable(sql, commandType))
                mTask.Start()
                Using mDataTable As DataTable = Await mTask
                    If Not String.IsNullOrEmpty(mDatabaseBuilder.ErrorMsg) Then
                        ErrorMsg = mDatabaseBuilder.ErrorMsg
                    End If
                    Return mDataTable
                End Using
            Catch ex As Exception
                ErrorMsg = ex.Message
            End Try
            Return Nothing
        End Function
        Public Function GetScalarValue(ByVal sql As String, Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As Object
            Try
                Dim mDatabaseBuilder As DatabaseBuilder = New SqlClientBuilder(ConnStr)
                Dim mObject As Object = mDatabaseBuilder.GetScalarValue(sql, commandType)
                If Not String.IsNullOrEmpty(mDatabaseBuilder.ErrorMsg) Then
                    ErrorMsg = mDatabaseBuilder.ErrorMsg
                End If
                Return mObject
            Catch ex As Exception
                ErrorMsg = ex.Message
            End Try
            Return Nothing
        End Function
        Public Function GetScalarValue(ByVal sql As String, ByVal parameters As List(Of KeyValueHelper), Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As Object
            Try
                Dim mDatabaseBuilder As DatabaseBuilder = New SqlClientBuilder(ConnStr)
                If parameters IsNot Nothing Then
                    For Each param As KeyValueHelper In parameters
                        mDatabaseBuilder.CreateCommandParameters(param.Key, param.Value)
                    Next
                End If
                Dim mObject As Object = mDatabaseBuilder.GetScalarValue(sql, commandType)
                If Not String.IsNullOrEmpty(mDatabaseBuilder.ErrorMsg) Then
                    ErrorMsg = mDatabaseBuilder.ErrorMsg
                End If
                Return mObject
            Catch ex As Exception
                ErrorMsg = ex.Message
            End Try
            Return Nothing
        End Function
        Public Function GetdataReader(ByVal sql As String) As IDataReader
            Try
                Dim mDatabaseBuilder As DatabaseBuilder = New SqlClientBuilder(ConnStr)
                Dim mDataReader As IDataReader = mDatabaseBuilder.GetdataReader(sql, CommandType.Text)
                If Not String.IsNullOrEmpty(mDatabaseBuilder.ErrorMsg) Then
                    ErrorMsg = mDatabaseBuilder.ErrorMsg
                End If
                Return mDataReader
            Catch ex As Exception
                ErrorMsg = ex.Message
            End Try
            Return Nothing
        End Function
        Public Function GetdataReader(ByVal sql As Object, ByVal parameters As List(Of KeyValueHelper), Optional ByVal commandType As CommandType = CommandType.StoredProcedure) As List(Of Object)
            Try
                Dim mDatabaseBuilder As DatabaseBuilder = New SqlClientBuilder(ConnStr)
                If parameters IsNot Nothing Then
                    For Each param As KeyValueHelper In parameters
                        mDatabaseBuilder.CreateCommandParameters(param.Key, param.Value)
                    Next
                End If
                Dim mObjectList As List(Of Object) = mDatabaseBuilder.GetdataReader(sql, commandType)
                If Not String.IsNullOrEmpty(mDatabaseBuilder.ErrorMsg) Then
                    ErrorMsg = mDatabaseBuilder.ErrorMsg
                End If
                Return mObjectList
            Catch ex As Exception
                ErrorMsg = ex.Message
            End Try
            Return Nothing
        End Function
        Public Function GetDataSet(ByVal sql As String) As DataSet
            Try
                Dim mDatabaseBuilder As DatabaseBuilder = New SqlClientBuilder(ConnStr)
                Dim mDataSet As DataSet = mDatabaseBuilder.GetDataSet(sql, CommandType.Text)
                If Not String.IsNullOrEmpty(mDatabaseBuilder.ErrorMsg) Then
                    ErrorMsg = mDatabaseBuilder.ErrorMsg
                End If
                Return mDataSet
            Catch ex As Exception
                ErrorMsg = ex.Message
            End Try
            Return Nothing
        End Function
#End Region

    End Class

End Namespace

