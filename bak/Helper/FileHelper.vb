Imports System.IO
Imports System.Linq
Imports System.Text.RegularExpressions
Imports Helper.ApplicationTools


Public Class FileHelper

    Public Shared Function GetFiles(ByVal path As String, Optional searchPatterns As String() = Nothing) As List(Of String)
        Dim mFileList As New List(Of String)
        Dim mSearchPatterns As String() = searchPatterns
        Try
            If mSearchPatterns Is Nothing Then mSearchPatterns(0) = "*.txt"
            For i As Integer = 0 To mSearchPatterns.Length - 1
                Debug.Print(mSearchPatterns(i))
                mFileList.AddRange(Directory.GetFiles(path, mSearchPatterns(i), SearchOption.TopDirectoryOnly))
            Next
        Catch ex As Exception
        End Try
        Return mFileList.Distinct.ToList
    End Function
    Public Shared Function GetFilesRecursive(ByVal initial As String, Optional searchstring As String = "") As List(Of String)
        Dim result As New List(Of String)
        Dim dir As String = initial
        Dim arrSearch As String()
        Try
            arrSearch = Split(searchstring, "|")
            If arrSearch(0) = "" Then
                arrSearch(0) = "*.txt"
            End If

            For i As Integer = 0 To arrSearch.Length - 1
                result.AddRange(Directory.GetFiles(dir, arrSearch(i), SearchOption.TopDirectoryOnly))
            Next

        Catch ex As Exception
        End Try
        Return result
    End Function
    Function ExistFile(ByVal path As String,
                        Optional ByVal name As String = "",
                        Optional ByVal extention As String = "") As Boolean
        Dim mPath As String = CEmpty(path) + CEmpty(name) + CEmpty(extention)
        If CEmpty(mPath) = "" Then Return False
        Return File.Exists(mPath)
    End Function
    Public Shared Function GetCoRegNo(ByVal folder As String, ByVal regNo As String) As String
        Dim mFolderList As String() = Directory.GetDirectories(folder)
        Try
            For Each foldr As String In mFolderList
                Dim mFolder As New DirectoryInfo(foldr)
                Dim mFolderInfo As String() = Split(mFolder.Name, ".")
                If mFolderInfo(0) = regNo Then
                    Return foldr
                End If
            Next
        Catch excpt As System.Exception
            Debug.WriteLine(excpt.Message)
        End Try
        Return Nothing
    End Function
    Public Shared Function FetchTextFiles(ByVal initial As String) As List(Of String)
        Dim result As New List(Of String)
        Try
            ' Add all immediate file paths
            result.AddRange(Directory.GetFiles(initial, "*.txt"))
            Return result
        Catch ex As Exception
        End Try
        Return result
    End Function
    Public Shared Function GetExe(ByVal initial As String) As List(Of String)
        Dim result As New List(Of String)
        Dim stack As New Stack(Of String)
        stack.Push(initial)
        Do While (stack.Count > 0)
            Dim dir As String = stack.Pop
            Try
                result.AddRange(Directory.GetFiles(dir, "*.lnk"))
            Catch ex As Exception
            End Try
        Loop

        Return result
    End Function
    Public Shared Function IsExistOrNotAllowed(ByVal path As String) As FolderAccessState
        Dim mDirectoryInfo As DirectoryInfo = New DirectoryInfo(path)
        Dim mIPPattern = "\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}"
        Dim mMatchInfo = Regex.Match(mDirectoryInfo.FullName, mIPPattern)
        Dim mIPOutput As String
        If mMatchInfo.Success Then
            mIPOutput = mMatchInfo.Value
        Else
            mIPOutput = String.Empty
        End If
        If Not mIPOutput = String.Empty AndAlso My.Computer.Network.Ping(mIPOutput) = False Then
            Return FolderAccessState.NotExist
        End If
        If (Not mDirectoryInfo.Exists) Then
            Try
                If mDirectoryInfo.Attributes = -1 Then
                    Return FolderAccessState.NotExist
                End If
            Catch ex As Exception
                Return FolderAccessState.NotAllowed
            End Try
        End If
        Return FolderAccessState.Exist
    End Function



    'Public Shared Function SearchDirectory(ByVal sDir As String, ByVal regNo As String) As String


    '    Dim x As String = Nothing

    '    Try
    '        For Each d As String In Directory.GetDirectories(sDir)

    '            Dim dir As New DirectoryInfo(d)
    '            Dim info As String() = Split(dir.Name, ".")
    '            If info(0) = regNo Then
    '                x = d
    '                Exit For
    '            End If

    '            x = SearchDirectory(d, regNo)
    '        Next

    '    Catch excpt As System.Exception
    '        Debug.WriteLine(excpt.Message)
    '    End Try

    '    Return x

    'End Function



    '' <summary>
    '' This method starts at the specified directory, and traverses all subdirectories.
    '' It returns a List of those directories.
    '' </summary>
    'Public Shared Function GetFilesRecursive(ByVal initial As String) As List(Of String)
    '    ' This list stores the results.
    '    Dim result As New List(Of String)

    '    ' This stack stores the directories to process.
    '    Dim stack As New Stack(Of String)

    '    ' Add the initial directory
    '    stack.Push(initial)

    '    ' Continue processing for each stacked directory
    '    Do While (stack.Count > 0)
    '        ' Get top directory string
    '        Dim dir As String = stack.Pop
    '        Try
    '            ' Add all immediate file paths
    '            result.AddRange(Directory.GetFiles(dir,  "*.txt", SearchOption.TopDirectoryOnly))


    '            ' Loop through all subdirectories and add them to the stack.
    '            'Dim directoryName As String
    '            'For Each directoryName In Directory.GetDirectories(dir)
    '            '    stack.Push(directoryName)
    '            'Next

    '        Catch ex As Exception
    '        End Try
    '    Loop

    '    ' Return the list
    '    Return result
    'End Function





End Class
