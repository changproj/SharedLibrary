Module basEncrypt
    Public Const intMaxLen = 10
    Public Const key = "paymt2003"

    Public Function Encrypt(key As String, DataString As String) As String
        Dim i As Integer
        Dim position As Integer
        Dim Temp As String

        Temp$ = ""
        For i% = 1 To Len(DataString$)
            position% = (i% Mod Len(key$)) + 1
            'Temp$ = Temp$ + Chr$(Asc(Mid$(DataString$, i%, 1)) Xor Asc(Mid$(key$, position%, 1)))
            Temp$ = Temp$ + Chr(Asc(Mid$(DataString$, i%, 1)) Xor Asc(Mid$(key$, position%, 1)))
        Next i%
        Encrypt = Temp

    End Function

    Function GetPass(strPassSave As String) As String

        Dim intPos As Integer
        Dim chrSearch As String
        Dim strPlainText As String

        chrSearch = " "
        strPlainText = Encrypt(key, strPassSave)

        'Loop
        'Cut the space
        intPos = InStr(strPlainText, chrSearch)
        If intPos <> 0 Then
            strPlainText = Left(strPlainText, intPos - 1)
        End If
        GetPass = strPlainText

    End Function

    Function LetPass(strPasswd As String)
        Dim intCount As Integer
        Dim intLen As String

        If Len(strPasswd) < intMaxLen Then
            intLen = Len(strPasswd)
            For intCount = intLen To intMaxLen
                strPasswd = strPasswd & " "
            Next intCount
        End If
        LetPass = Encrypt(key, strPasswd)
    End Function

End Module
