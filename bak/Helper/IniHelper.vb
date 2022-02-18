Imports System.Text
Imports System.Text.RegularExpressions
Imports Helper.ApplicationTools
Imports Helper.KeyValueHelper

Public Class IniHelper

#Region "API"
    Declare Auto Function GetPrivateProfileString Lib "kernel32" (
        ByVal section As String, ByVal key As String,
        ByVal [default] As String, ByVal builder As StringBuilder,
        ByVal size As Integer, ByVal path As String) As Integer
    Declare Auto Function WritePrivateProfileString Lib "kernel32" (
        ByVal section As String, ByVal key As String,
        ByVal value As String, ByVal path As String) As Long
    Declare Auto Function GetPrivateProfileSection Lib "kernel32" (
        ByVal section As String, ByVal buffer As Byte(),
        ByVal size As Integer, ByVal FileName As String) As Integer
#End Region

#Region "Function"
    Function IsExistsKey(ByVal path As String, ByVal section As String, ByVal key As String) As Boolean
        Return ReadValue(path, section, key).Length > 0
    End Function
    Function ReadValue(ByVal Path As String, ByVal Section As Object, ByVal Key As Object) As String
        Dim mStr As New StringBuilder(500)
        GetPrivateProfileString(Section.ToString, Key.ToString, "", mStr, mStr.Capacity, Path)
        Return CEmpty(mStr.ToString)
    End Function
    Sub WriteValue(ByVal path As String, ByVal section As String, ByVal key As String, ByVal value As String)
        WritePrivateProfileString(section, key, CSpace(value, , False), path)
    End Sub
    Sub DeleteKey(ByVal path As String, ByVal section As String, ByVal key As String)
        WriteValue(path, section, key, Nothing)
    End Sub
    Sub DeleteSection(ByVal path As String, ByVal section As String)
        WriteValue(path, section, Nothing, Nothing)
    End Sub
    Function ReadPairs(ByVal file As String, ByVal section As String) As KeyValueHelperList
        Try
            Dim mLst As New KeyValueHelperList
            Dim mBuf As Byte() = New Byte(2047) {}
            GetPrivateProfileSection(section, mBuf, 2048, file)
            Dim mStrs As New List(Of String)
            Dim mBdr As New StringBuilder
            Dim mBrk As Integer = 0
            For mIdx As Integer = 0 To mBuf.Length
                Select Case True
                    Case mIdx = mBuf.Length, mBrk >= 2
                        Dim mStr As String = CEmpty(mBdr.ToString)
                        If mStr = "" Then Exit Select
                        mStrs.Add(mBdr.ToString)
                        mBdr.Length = 0
                        mBrk = 0
                        Continue For
                End Select
                Select Case True
                    Case mIdx = mBuf.Length
                    Case ChrW(mBuf(mIdx)) = ControlChars.NullChar
                        mBrk += 1
                    Case Else
                        mBrk = 0
                        mBdr.Append(ChrW(mBuf(mIdx)))
                End Select
            Next
            For Each mLine As String In mStrs
                Dim mMatch As Match = Regex.Match(mLine, "^([^=]+)\s*=\s*(.*)")
                With mMatch
                    If .Success = True Then
                        Dim mKey As String = CEmpty(.Groups(1).Value)
                        Dim mVal As String = CEmpty(.Groups(2).Value)
                        mLst.Add(New KeyValueHelper(mKey, mVal))
                    End If
                    If .Success = False Then
                        mLst.Add(New KeyValueHelper(mLine, ""))
                    End If
                End With
            Next
            Return mLst
        Catch ex As Exception
            Dim mLst As New KeyValueHelperList
            Return mLst
        End Try
    End Function
    Function ReadDirts(ByVal file As String, ByVal section As String) As Dictionary(Of String, String)
        Try
            Dim mLst As New Dictionary(Of String, String)
            Dim mBuf As Byte() = New Byte(2047) {}
            GetPrivateProfileSection(section, mBuf, 2048, file)
            Dim mStrs As New List(Of String)
            Dim mBdr As New StringBuilder
            Dim mBrk As Integer = 0
            For mIdx As Integer = 0 To mBuf.Length
                Select Case True
                    Case mIdx = mBuf.Length, mBrk >= 2
                        Dim mStr As String = CEmpty(mBdr.ToString)
                        If mStr = "" Then Exit Select
                        mStrs.Add(mBdr.ToString)
                        mBdr.Length = 0
                        mBrk = 0
                        Continue For
                End Select
                Select Case True
                    Case mIdx = mBuf.Length
                    Case ChrW(mBuf(mIdx)) = ControlChars.NullChar
                        mBrk += 1
                    Case Else
                        mBrk = 0
                        mBdr.Append(ChrW(mBuf(mIdx)))
                End Select
            Next
            For Each mLine As String In mStrs
                Dim mMatch As Match = Regex.Match(mLine, "^([^=]+)\s*=\s*(.*)")
                With mMatch
                    If .Success = True Then
                        Dim mKey As String = CEmpty(.Groups(1).Value)
                        Dim mVal As String = CEmpty(.Groups(2).Value)
                        mLst.Add(mKey, mVal)
                    End If
                    If .Success = False Then
                        mLst.Add(mLine, "")
                    End If
                End With
            Next
            Return mLst
        Catch ex As Exception
            Dim mLst As New Dictionary(Of String, String)
            Return mLst
        End Try
    End Function
#End Region
End Class
