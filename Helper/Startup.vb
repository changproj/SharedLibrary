Imports Helper.ApplicationTools

Public Class Startup

    Private _IsConfigIni As Boolean

#Region "Delegate"
    'Public Delegate Function funcConfigIni() As Boolean
#End Region

#Region "Event"
    Event OnConfigIni(ByVal FileName As String)
#End Region

#Region "Property"
    Public ReadOnly Property IsConfigIni() As Boolean
        Get
            Return _IsConfigIni
        End Get
    End Property
#End Region

#Region "Function"
    Sub LoadIni(Optional ByVal FileName As String = Nothing)
        Dim mFileIni As String = FileIni(FileName)
        RaiseEvent OnConfigIni(FileName)
        'Dim mConfigIni As Boolean = ConfigIni(mFileIni)
        'Dim mConfigIni As Boolean = Me.IsConfigIni
    End Sub
    Function FileIni(ByVal FileName As String) As String
        Select Case True
            Case CEmpty(FileName) = ""
                Return ""
            Case cFileHelper.ExistFile(FileName)
                Return CEmpty(FileName)
                'Case cFileHelper.ExistFile(CONFIGFILE)
                '    Return CEmpty(CONFIGFILE)
            Case Else
                Return ""
        End Select
    End Function

#End Region

#Region "Calling From Outside This Class"
    'Public Sub delConfigIni(ByVal funcConfigIni As funcConfigIni)
    '    _IsConfigIni = funcConfigIni.Invoke()
    'End Sub
#End Region

#Region "Argument"
    Sub LoadArguments()
        Dim mArgs As String() = System.Environment.GetCommandLineArgs()
        If mArgs.Length = 0 Then Exit Sub
        For mIdx As Integer = 0 To mArgs.Length - 1
            Dim mArg As String = CEmpty(mArgs(mIdx))
            Select Case True
                Case mArg = ""
                Case String.Equals(mArg, String.Format("-{0}", CmdArgs.Ini.ToString), 1)
                    If mIdx + 1 >= mArgs.Length Then Exit Select
                    LoadIni(mArgs(mIdx + 1))
            End Select
        Next
    End Sub
#End Region
End Class
