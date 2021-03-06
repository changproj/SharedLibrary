Imports System.Text.RegularExpressions
Imports System.Windows.Forms

Public Class ApplicationTools

#Region "Enum"
#Region "Winform"
    Public Enum FormState
        Load = 1
        [New] = 2
        Edit = 3
        Delete = 4
        Save = 6
        Cancel = 7
        Nil = 8
    End Enum
#End Region

#Region "RpReport"
    Public Enum SelectionType
        [And]
        [Or]
        [Nil]
    End Enum
    Public Enum BracketType
        Curve
        Flat
        Round
    End Enum
    Public Enum QuoteType
        [Single]
        [Double]
        Star
    End Enum
    Public Enum FilterType
        Nil
        Equal
        EqualString
        NotEqual
        EndWith
        StartWith
        [Like]
        [In]
        NotIn
        Greater
        GreaterOrEqual
        Smaller
        SmallerOrEqual
    End Enum
#End Region

#Region "DataEntityMgr"
    Public Enum enumIsLike
        equal = 0
        [like] = 1
        notequal = 2
        notlike = 3
        greater = 4
        lesser = 5
        greaterequal = 6
        lesserequal = 7
    End Enum
    Public Enum enumSortDirection
        Asc = 0
        Desc = 1
    End Enum
#End Region

#Region "DataEntity"
    Public Enum EnumObjectState
        Unchanged
        Added
        Deleted
        Modified
    End Enum
#End Region

#Region "Import File"
    Enum FileType
        Nil
        Text
        Xml
        Mdb
        Server
        Excel
    End Enum
    Enum FileExtension
        txt
        xml
        mdb
        xls
        xlsx
    End Enum
    Enum FileFormat
        Nil
        FixedLength
        Delimiter
        Mixed
    End Enum
    Enum FolderAccessState
        Exist
        NotExist
        NotAllowed
    End Enum
#End Region

    '#Region "Is Planation"
    '    Enum CompanyType
    '        PL
    '        INS
    '        SC
    '    End Enum
    '#End Region

#Region ".ini"
#Region "Variable"
    Public Shared cIniHelper As New IniHelper
    Public Shared cFileHelper As New FileHelper
#End Region
#Region "Enum"
    Enum SectionIni
        [Default]
    End Enum
    'Enum KeyIni
    '    Connection
    '    HasBatch
    '    ApplicationName
    '    SystemID
    '    NetworkTextFilePool
    '    IncludeCo
    '    ODBCName
    '    NetworkPath
    '    LocalPath
    'End Enum
    Enum CmdArgs
        Ini
    End Enum
#End Region

#End Region
#End Region

#Region "Msgbox"
    Public Shared Sub Msg(ByVal msg As String,
                  ByVal title As String,
                  ByVal msgButton As MessageBoxButtons,
                  ByVal msgIcon As MessageBoxIcon,
                  ByVal control As Control,
                  ByVal isHighlight As Boolean)
        MessageBox.Show(msg, title, msgButton, msgIcon)
        If (control IsNot Nothing) AndAlso (isHighlight) Then
            HLField(control)
        End If
    End Sub
    Public Shared Sub InfoMsg(ByVal txt As String)
        MessageBox.Show(txt, "Information:", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Public Shared Sub ErrorMsg(ByVal txt As String)
        MessageBox.Show(txt, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub
    Public Shared Function QuestionMsg(ByVal txt As String) As Boolean
        If (MessageBox.Show(txt, "Question:", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
            Return True
        Else
            Return False
        End If
    End Function
#End Region

#Region "ComboBox"
    Public Shared Sub ListComboBox(ByVal cboBox As ComboBox, ByVal ArrayName As List(Of KeyValueHelper))
        cboBox.DataSource = ArrayName
        cboBox.ValueMember = "Key"
        cboBox.DisplayMember = "Value"

        If cboBox.Items.Count > 0 Then cboBox.SelectedIndex = 0
        cboBox.Focus()
    End Sub
#End Region

#Region "SetToolTip"
    Public Shared Sub SetToolTip(ByVal ctrl As Control, ByVal msg As String, Optional ByVal title As String = "")
        Dim buttonToolTip As New ToolTip()
        buttonToolTip.ToolTipTitle = title
        buttonToolTip.UseFading = True
        buttonToolTip.UseAnimation = True
        buttonToolTip.IsBalloon = True
        buttonToolTip.ShowAlways = True
        buttonToolTip.AutoPopDelay = 3000
        buttonToolTip.InitialDelay = 1000
        buttonToolTip.ReshowDelay = 500
        buttonToolTip.IsBalloon = True
        buttonToolTip.SetToolTip(ctrl, msg)
    End Sub
#End Region

#Region "SafeValue"
    Public Shared Function SafeString(ByVal val As Object, Optional ByVal defaultVal As Object = 0) As String
        If String.IsNullOrEmpty(val) Then
            val = defaultVal
        Else
            val = CStr(val).Trim
        End If
        Return val
    End Function
    Public Shared Function SafeInt16(ByVal val As Object, Optional ByVal defaultVal As Object = 0) As Int16
        Dim m As Integer
        If String.IsNullOrEmpty(val) Then
            val = defaultVal
        End If
        If Int16.TryParse(val, m) Then
            Return m
        Else
            Return 0
        End If
    End Function
    Public Shared Function SafeInt32(ByVal val As Object, Optional ByVal defaultVal As Object = 0) As Int32
        Dim m As Integer
        If String.IsNullOrEmpty(val) Then
            val = defaultVal
        End If
        If Int32.TryParse(val, m) Then
            Return m
        Else
            Return 0
        End If
    End Function
    Public Shared Function SafeInt64(ByVal val As Object, Optional ByVal defaultVal As Object = 0) As Int64
        Dim m As Integer
        If String.IsNullOrEmpty(val) Then
            val = defaultVal
        End If
        If Int64.TryParse(val, m) Then
            Return m
        Else
            Return 0
        End If
    End Function
    Public Shared Function SafeDecimal(ByVal val As Object, Optional ByVal defaultVal As Object = Nothing) As Decimal
        Dim m As Integer
        If String.IsNullOrEmpty(val) Then
            val = defaultVal
        End If
        If Decimal.TryParse(val, m) Then
            Return val
        Else
            Return 0
        End If
    End Function
    Public Shared Function SafeBoolean(ByVal val As Object, Optional ByVal defaultVal As Object = Nothing) As Boolean
        Dim m As Integer
        If String.IsNullOrEmpty(val) Then
            val = defaultVal
        End If
        If Boolean.TryParse(val, m) Then
            Return val
        Else
            Return False
        End If
    End Function

    Public Shared Function SafeDate(ByVal val As Object, Optional ByVal defaultVal As Object = Nothing) As Object
        If String.IsNullOrEmpty(val) Then
            val = defaultVal
        End If
        Dim mDate As DateTime
        If DateTime.TryParse(val, mDate) Then
            Return mDate
        Else
            Return Nothing
        End If
    End Function
    Public Shared Function SafeObject(ByVal val As Object, Optional ByVal defaultVal As Object = Nothing) As Object
        If String.IsNullOrEmpty(val) Then
            val = defaultVal
        End If
        Return val
    End Function

    Public Shared Function ApplyFormat(ByVal val As Object, Optional ByVal format As String = "") As Object
        If val Is Nothing Then Return Nothing
        If val.GetType.Equals(GetType(System.Decimal)) OrElse
            val.GetType.Equals(GetType(System.Int16)) OrElse
            val.GetType.Equals(GetType(System.Int32)) OrElse
            val.GetType.Equals(GetType(System.Int64)) Then
            If String.IsNullOrEmpty(format) Then format = "#,##0.00;(#,##0.00)"
            Return Decimal.Parse(val).ToString(format)
        End If
        Return Nothing
    End Function

    'database to DataEntity
    Public Shared Function SafeValue(ByVal val As Object, Optional ByVal defaultVal As Object = Nothing) As Object
        If val Is Nothing Then Return defaultVal
        If val.GetType.Equals(GetType(System.String)) Then
            Return SafeString(val, defaultVal)
        End If
        If val.GetType.Equals(GetType(System.Decimal)) Then
            Return SafeDecimal(val, defaultVal)
        End If
        If val.GetType.Equals(GetType(System.Int16)) Then
            Return SafeInt16(val, defaultVal)
        End If
        If val.GetType.Equals(GetType(System.Int32)) Then
            Return SafeInt32(val, defaultVal)
        End If
        If val.GetType.Equals(GetType(System.Int64)) Then
            Return SafeInt64(val, defaultVal)
        End If
        If val.GetType.Equals(GetType(System.Boolean)) Then
            Return SafeBoolean(val, defaultVal)
        End If
        If val.GetType.Equals(GetType(System.DateTime)) Then
            Return SafeDate(val, defaultVal)
        End If
        Return Nothing
    End Function

    Public Shared Function SafeCheckbox(ByVal val As Object, Optional ByVal defaultVal As CheckState = CheckState.Indeterminate) As CheckState
        If val Is Nothing Then Return defaultVal
        Select Case CType(val, Boolean)
            Case True
                Return CheckState.Checked
            Case False
                Return CheckState.Unchecked
        End Select
        Return Nothing
    End Function
    Public Shared Function SafeCheckbox(ByVal val As CheckState, Optional ByVal defaultVal As CheckState = CheckState.Indeterminate) As Boolean
        Select Case CType(val, Boolean)
            Case CheckState.Checked
                Return True
            Case CheckState.Unchecked
                Return False
            Case CheckState.Indeterminate
                Return Nothing
        End Select
        Return Nothing
    End Function

    Public Shared Function SafeComboBox(ByVal e As ComboBox, Optional ByVal isSelectedValue As Boolean = True) As Object
        If e.SelectedItem Is Nothing Then
            Return 0
        Else
            If isSelectedValue Then
                Return e.SelectedValue
            Else
                Return e.Text
            End If
        End If
    End Function
    Public Shared Function SafePath(ByVal path As String, Optional ByVal defaultVal As Object = Nothing) As String
        If String.IsNullOrEmpty(path) Then Return defaultVal
        Return path.TrimEnd("\") + "\"
    End Function



    'make sure colName is not empty
    Public Shared Function SafeField(ByVal dr As DataRow, ByVal colName As String) As Object
        If Not dr.IsNull(colName) Then
            Return dr.Item(colName)
        Else
            Return Nothing
        End If
    End Function
    Public Shared Function SafeField(ByVal dr As Object) As Object
        If dr Is Nothing Then Return DBNull.Value
        If Not dr.Equals(DBNull.Value) Then
            Return dr
        Else
            Return Nothing
        End If
    End Function
#End Region

#Region "Others"
    Public Shared Sub HLField(ByVal myCtl As Control)
        If TypeOf myCtl Is TextBox Then
            myCtl.Focus()
            CType(myCtl, TextBox).Select(0, myCtl.Text.Length)
            Exit Sub
        End If
        If TypeOf myCtl Is MaskedTextBox Then
            myCtl.Focus()
            CType(myCtl, MaskedTextBox).Select(0, myCtl.Text.Length)
            Exit Sub
        End If
        If TypeOf myCtl Is WaterMarkTextBox Then
            myCtl.Focus()
            CType(myCtl, WaterMarkTextBox).Select(0, myCtl.Text.Length)
            Exit Sub
        End If
        If TypeOf myCtl Is ComboBox Then
            myCtl.Focus()
            CType(myCtl, ComboBox).Select(0, myCtl.Text.Length)
            Exit Sub
        End If
        If TypeOf myCtl Is DateTimePicker Then
            myCtl.Focus()
            Exit Sub
        End If
    End Sub
    Public Shared Function ChangeCase(ByVal KeyChar As Char) As Char
        Dim InputStr As Char

        If KeyChar >= "a" And KeyChar <= "z" Then
            InputStr = UCase(KeyChar)
        Else
            InputStr = KeyChar
        End If
        Return InputStr
    End Function
    'Public Shared Function GetImage(ByVal appPath As String, ByVal pictName As String) As Image
    '    Return Image.FromFile(String.Format("{0}\resources\{1}", appPath, pictName))
    'End Function
    Public Shared Function RoundCents(ByVal TotalCost As Decimal) As Decimal
        Return Math.Round(TotalCost / 0.05, 0) * 0.05
    End Function
#End Region

#Region "Watermark"
    Public Shared Function isMatch(ByVal regexpress As String, ByVal val As String) As Boolean
        Dim regex As Regex = New Regex(regexpress)
        Dim match As Match = regex.Match(UCase(val))
        If match.Success Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function GetRegEx(ByVal accFormat As String, ByVal inputLength As Integer) As String

        Dim inputChar As Char()
        inputChar = accFormat.ToCharArray

        Dim str As String = ""
        Select Case inputChar(inputLength)
            Case "A"
                str = "[A-Z]{1}"
            Case "9"
                str = "[0-9]{1}"
            Case "-"
                str = "-{1}"
        End Select
        Return str
    End Function
    Public Shared Function ValidKeypress(ByVal wm As TextBox, ByVal regexp As String, ByVal accFormat As String, ByVal e As KeyPressEventArgs, ByVal inputLength As Integer, Optional ByVal delimiter As String = "-") As List(Of String)
        Dim li As New List(Of String)
        Dim accRegexp As String = regexp '"[0-9]{3}-[0-9]{4}-[0-9]{6}-[0-9]{2}"

        If (e.KeyChar = ControlChars.Back) Then
            e.Handled = False
        Else
            If wm.Text.Length = accFormat.Length AndAlso wm.SelectionLength > 0 Then
                Dim a As Integer
                If wm.SelectionStart + wm.SelectionLength > wm.Text.Length Then
                    a = wm.SelectionStart - wm.SelectionLength
                ElseIf wm.SelectionLength = wm.Text.Length Then
                    a = 0
                Else
                    a = wm.SelectionStart
                End If
                If wm.Text.Length <> accFormat.Length Then wm.Text = wm.Text.Remove(a, wm.SelectionLength)
                wm.SelectionStart = a
                inputLength = wm.Text.Length + 1
            End If

            If accFormat.Length < inputLength Then
                e.Handled = True
            Else

                If isMatch(GetRegEx(accFormat, wm.SelectionStart), e.KeyChar) Then
                    Dim i As Integer = wm.SelectionStart
                    wm.Text = wm.Text.Insert(i, UCase(e.KeyChar))
                    wm.SelectionStart = i + 1

                    If accFormat.Length > inputLength AndAlso accFormat.Chars(wm.Text.Length) = delimiter Then
                        Dim b As Integer = wm.SelectionStart
                        wm.AppendText(delimiter)
                        If b < (accFormat.LastIndexOf(delimiter, inputLength)) Then
                            wm.SelectionStart = b
                        End If
                    End If

                    e.Handled = True

                    If accFormat.Length <= inputLength Then
                        If isMatch(accRegexp, wm.Text) Then
                            'MsgBox("ok")
                        Else
                            'MsgBox("not ok")
                            Debug.Print("format not ok")
                        End If
                    End If

                    Return Nothing

                Else
                    'MsgBox("wrong")
                    li.Add("Wrong")
                    e.Handled = True


                End If
            End If
        End If

        Return li

    End Function

#End Region

#Region "GetPropertyValue"
    Public Shared Function GetPropertyValue(ByVal obj As Object, ByVal PropName As String) As Object
        Dim objType As Type = obj.GetType()
        Dim pInfo As System.Reflection.PropertyInfo = objType.GetProperty(PropName)
        If pInfo Is Nothing Then Return Nothing

        If GetType(IList).IsAssignableFrom(pInfo.PropertyType) AndAlso pInfo.PropertyType.IsGenericType Then Return Nothing 'skip if it is list of object

        Dim PropValue As Object = pInfo.GetValue(obj, Reflection.BindingFlags.GetProperty, Nothing, Nothing, Nothing)

        Return PropValue
    End Function

#End Region

#Region "Valid Input Type"
    Public Shared Function CheckNumeric(ByVal KeyChar As Char, ByVal IStr As String) As Char
        Dim InputStr As Char
        If (KeyChar >= "0" And KeyChar <= "9") Or KeyChar = "." Or Asc(KeyChar) = 8 Or Asc(KeyChar) = 13 Then
            If KeyChar = "." And InStr(IStr, ".") <> 0 Then
                InputStr = Nothing
            Else
                InputStr = KeyChar
            End If
        Else
            InputStr = Nothing
        End If
        Return InputStr
    End Function

    Public Shared Function CheckIntOnly(ByVal KeyChar As Char, ByVal IStr As String) As Char
        Dim InputStr As Char
        If (KeyChar >= "0" And KeyChar <= "0") Or (KeyChar >= "1" And KeyChar <= "9") Or Asc(KeyChar) = 8 Or Asc(KeyChar) = 13 Then
            If KeyChar = "." And InStr(IStr, ".") <> 0 Then
                InputStr = Nothing
            Else
                InputStr = KeyChar
            End If
        Else
            InputStr = Nothing
        End If
        Return InputStr
    End Function
    Public Shared Function CheckAlphabetOnly(ByVal KeyChar As Char, ByVal IStr As String) As Char
        Dim InputStr As Char
        If (KeyChar >= "A" And KeyChar <= "Z") Or (KeyChar >= "a" And KeyChar <= "z") Or Asc(KeyChar) = 8 Or Asc(KeyChar) = 13 Then
            InputStr = KeyChar
        Else
            InputStr = Nothing
        End If
        Return InputStr
    End Function

    Public Function IsInteger(ByVal Data As String) As Boolean
        If Data.Trim = "" Then
            IsInteger = False
        Else
            If Data.Trim <> "" Then
                If Information.IsNumeric(Data & ".0e0") = True Then
                    IsInteger = True
                Else
                    IsInteger = False
                End If
            End If
        End If
    End Function
#End Region

#Region "Self Defined (Cast)"
    Public Shared Function CNameSave(ByVal Obj As Object, ByVal PropName As Object,
                      ByVal CallType As CallType, ByVal ParamArray Args() As Object) As Object
        Try
            If Obj Is Nothing Then Return Nothing
            If CEmpty(PropName.ToString) = "" Then Return Nothing
            If Obj.GetType().GetProperty(PropName.ToString) Is Nothing Then Return Nothing
            Return CallByName(Obj, PropName.ToString, CallType, Args)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function CEmpty(ByVal str As String) As String
        If str Is Nothing Then Return String.Empty
        Return str.ToString.Trim
    End Function
    Public Shared Function CBracket(ByVal str As String, Optional ByVal bracketType As BracketType = BracketType.Round,
    Optional ByVal hideOnPrefix As Boolean = False, Optional ByVal hideOnSuffix As Boolean = False) As String
        Dim prefix As String = "", suffix As String = ""
        If Not hideOnPrefix Then
            Select Case bracketType
                Case BracketType.Curve
                    prefix = "{"
                Case BracketType.Flat
                    prefix = "["
                Case BracketType.Round
                    prefix = "("
            End Select
        End If
        If Not hideOnSuffix Then
            Select Case bracketType
                Case BracketType.Curve
                    suffix = "}"
                Case BracketType.Flat
                    suffix = "]"
                Case BracketType.Round
                    suffix = ")"
            End Select
        End If
        Return prefix + CEmpty(str) + suffix
    End Function
    Public Shared Function CQuote(ByVal str As String, Optional ByVal quoteType As QuoteType = QuoteType.Single,
    Optional ByVal hideOnPrefix As Boolean = False, Optional ByVal hideOnSuffix As Boolean = False) As String
        Dim prefix As String = "", suffix As String = ""
        If Not hideOnPrefix Then
            Select Case quoteType
                Case QuoteType.Single
                    prefix = "'"
                Case QuoteType.Double
                    prefix = """"
                Case QuoteType.Star
                    prefix = "*"
            End Select
        End If
        If Not hideOnSuffix Then
            Select Case quoteType
                Case QuoteType.Single
                    suffix = "'"
                Case QuoteType.Double
                    suffix = """"
                Case QuoteType.Star
                    suffix = "*"
            End Select
        End If
        Return prefix + CEmpty(str) + suffix
    End Function
    Public Shared Function CEqual(ByVal obj1 As Object, ByVal obj2 As Object) As Boolean
        If obj1 Is Nothing And obj2 Is Nothing Then Return True
        If (obj1 Is Nothing) <> (obj2 Is Nothing) Then Return False
        If Not obj1.Equals(obj2) Then Return False
        Return True
    End Function
    Public Shared Function CSpace(ByVal str As String, Optional ByVal Front As Boolean = True, Optional ByVal Rear As Boolean = True) As String
        str = CEmpty(str)
        If Front Then str = " " + str
        If Rear Then str += " "
        Return str
    End Function
    Public Shared Sub DoNothing()
    End Sub
#End Region

#Region "PC Name"
    Public Shared Function GetComputerName() As String
        'Dim ComputerName As String
        'ComputerName = System.Net.Dns.GetHostName
        'Return ComputerName

        Return System.Environment.MachineName
    End Function
#End Region


End Class
