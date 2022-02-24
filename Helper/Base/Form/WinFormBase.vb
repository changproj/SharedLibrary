Imports System.Drawing
Imports System.Windows.Forms
Imports Helper.DataEntity

Public Class WinFormBase

#Region "Enum"
    Public Enum EnumFormState
        [NewDoc]
        [Add]
        [Edit]
        [Delete]
        [Save]
        [Cancel]
        [DocCancelled]
        [Confirmed]
    End Enum
#End Region

#Region "Event"
    Public Event FormFlagChanged(ByVal objState As EnumFormState)
    Public Event RecordSaved()
#End Region

#Region "Fields"
    Private _keyControl As Control() = Nothing
    Private _focusControl As Control = Nothing
    Private _lastControl As Control = Nothing
    Private _skipTabControl As Control() = Nothing
    Private _notUpperCaseControl As Control() = Nothing
    Protected _PropertyDescription As New Dictionary(Of String, String)
    Private _status As EnumFormState
    Private _settings As IEnumerable(Of Object)
    Protected _IsLoadTheme As Nullable(Of Boolean) = False
#End Region

#Region "Properties"
    Public Sub New()
        AddFormFlagHandlers()
        InitializeComponent()
    End Sub
    Protected ReadOnly Property FormState() As EnumFormState
        Get
            Return _status
        End Get
    End Property
    Protected Property KeyControl() As Control()
        Get
            Return _keyControl
        End Get
        Set(ByVal value As Control())
            _keyControl = value
        End Set
    End Property
    Protected Property FocusControl() As Control
        Get
            Return _focusControl
        End Get
        Set(ByVal value As Control)
            _focusControl = value
        End Set
    End Property
    Protected Property LastControl() As Control
        Get
            Return _lastControl
        End Get
        Set(ByVal value As Control)
            _lastControl = value
        End Set
    End Property
    Protected Property SkipTabControl() As Control()
        Get
            Return _skipTabControl
        End Get
        Set(ByVal value As Control())
            _skipTabControl = value
        End Set
    End Property
    Protected Property NotUpperCaseControl() As Control()
        Get
            Return _notUpperCaseControl
        End Get
        Set(ByVal value As Control())
            _notUpperCaseControl = value
        End Set
    End Property
    Public ReadOnly Property Settings As IEnumerable(Of Object)
        Get
            Return _settings
        End Get
    End Property
    Public Property IsLoadTheme() As Nullable(Of Boolean)
        Get
            Return _IsLoadTheme
        End Get
        Set(ByVal value As Nullable(Of Boolean))
            _IsLoadTheme = value
        End Set
    End Property
#End Region

#Region "Overridable"
    Protected Overridable Sub InitControls()
    End Sub
    Protected Overridable Sub [NewDoc]()
    End Sub
    Protected Overridable Sub [New]()
    End Sub
    Protected Overridable Sub Edit()
    End Sub
    Protected Overridable Sub Delete()
    End Sub
    Protected Overridable Sub Cancel()
    End Sub
    Protected Overridable Sub Save()
    End Sub
    Protected Overridable Sub DocCancelled()
    End Sub
    Protected Overridable Sub Confirmed()
    End Sub
    Protected Overridable Sub OnFormFlagChanged(ByVal objState As EnumFormState)
        RaiseEvent FormFlagChanged(objState)
    End Sub
    Protected Overridable Sub ClearGBItem(ByVal MyGB As Control)
        For Each ctrl As Control In MyGB.Controls
            If TypeOf ctrl Is TextBox Then
                If Not TypeOf (ctrl.Parent) Is UpDownBase Then
                    ctrl.Text = ""
                End If
            End If
            If TypeOf ctrl Is CheckBox Then
                CType(ctrl, CheckBox).Checked = False
            End If
            If TypeOf ctrl Is DateTimePicker Then
                If Now >= CType(ctrl, DateTimePicker).MinDate AndAlso Now <= CType(ctrl, DateTimePicker).MaxDate Then
                    ctrl.Text = Now
                End If
            End If
            If TypeOf ctrl Is ComboBox Then
                Dim mCb As ComboBox = CType(ctrl, ComboBox)
                If mCb.Items.Count > 0 Then mCb.SelectedIndex = 0
            End If
            If TypeOf ctrl Is NumericUpDown Then
                CType(ctrl, NumericUpDown).Value = 0
            End If
            If ctrl.HasChildren Then
                ClearGBItem(ctrl)
            End If
        Next
        InitControls()
    End Sub
    Protected Overridable Sub LastControlAction()
    End Sub
    Protected Overridable Sub ListComboBox(ByVal cboBox As ComboBox, ByVal ArrayName As List(Of KeyValueHelper))
        cboBox.DataSource = ArrayName
        cboBox.DisplayMember = "Value"
        cboBox.ValueMember = "Key"
        If cboBox.Items.Count > 0 Then cboBox.SelectedIndex = 0
    End Sub
    Protected Overridable Function IsDataValid(ByVal listErr As List(Of KeyValueHelper), ByVal Optional isSuppressErrMsg As Boolean = False) As Boolean
        Dim mErrMsg As String = ""
        If listErr.Any Then
            For Each kvh As KeyValueHelper In listErr
                mErrMsg += IIf(mErrMsg = "", "", Chr(13)) + kvh.Value
            Next
            If isSuppressErrMsg = False Then
                ApplicationTools.ErrorMsg(mErrMsg)
            End If

            Return False
        End If
        Return True
    End Function
    Protected Overridable Function ValidateControl(ByVal ctrl As Control, ByVal Optional isSuppressErrMsg As Boolean = False) As Boolean
        Return Nothing
    End Function
    Protected Overridable Sub InitDGV(ByVal dgv As DataGridView, ByVal Optional initSearch As Boolean = True)
        If dgv Is Nothing Then Exit Sub
        With dgv
            .AutoGenerateColumns = False
            .BackgroundColor = Color.LightGray
            .BorderStyle = BorderStyle.Fixed3D
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            '.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            .ShowCellToolTips = False
            .AllowUserToOrderColumns = True
            .ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .ReadOnly = True
            .DefaultCellStyle.SelectionBackColor = Color.FromArgb(107, 103, 102)
            .DefaultCellStyle.SelectionForeColor = Color.White
            .RowHeadersDefaultCellStyle.SelectionBackColor = Color.Empty
            .RowsDefaultCellStyle.BackColor = Color.White
            .AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray
            .ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            .ColumnHeadersDefaultCellStyle.BackColor = Color.Black
            .RowHeadersDefaultCellStyle.BackColor = Color.Black

            Dim mFont As New Font(.DefaultCellStyle.Font.FontFamily, 9, FontStyle.Regular)
            .Font = mFont
            '.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
            '.DefaultCellStyle.WrapMode = DataGridViewTriState.True

            .DataSource = Nothing
            .Columns.Clear()
        End With

        If initSearch Then InitSearchDGV(dgv)

        'AddHandler dgv.CellPainting, AddressOf DGVCellPainting
    End Sub
    Protected Overridable Sub InitSearchDGV(ByVal dgv As DataGridView, Optional ByVal isEnquiry As Boolean = False)
    End Sub

#End Region

#Region "Methods"
    Private Sub InitFields()
        _focusControl = Nothing
        _lastControl = Nothing
        _skipTabControl = Nothing
        _notUpperCaseControl = Nothing
    End Sub
    Private Sub AddFormFlagHandlers()
        RemoveFormFlagChangedHandler()
        AddFormFlagChangedHandler()
    End Sub
    Private Sub AddFormFlagChangedHandler()
        AddHandler Me.FormFlagChanged, AddressOf FormStateChanged
    End Sub
    Private Sub RemoveFormFlagChangedHandler()
        RemoveHandler Me.FormFlagChanged, AddressOf FormStateChanged
    End Sub
    Private Sub FormStateChanged(ByVal objState As EnumFormState)
        _status = objState
        Select Case objState
            Case EnumFormState.Cancel
                Cancel()
            Case EnumFormState.[NewDoc]
                [NewDoc]()
            Case EnumFormState.Add
                [New]()
            Case EnumFormState.Edit
                Edit()
            Case EnumFormState.Delete
                Delete()
            Case EnumFormState.Save
                Save()
            Case EnumFormState.DocCancelled
                DocCancelled()
            Case EnumFormState.Confirmed
                Confirmed()
        End Select
        SetFocus(objState)
    End Sub
    Private Sub SetFocus(ByVal objState As EnumFormState)
        Select Case objState
            Case EnumFormState.Cancel
                If KeyControl IsNot Nothing Then
                    EnabledControls(KeyControl, True)
                End If
            Case EnumFormState.Add
                If KeyControl IsNot Nothing Then
                    EnabledControls(KeyControl, True)
                    KeyControl(0).Focus()
                End If
            Case EnumFormState.Edit
                If KeyControl IsNot Nothing Then
                    EnabledControls(KeyControl, False)
                    If ActiveControl IsNot Nothing Then
                        If TypeOf (ActiveControl) Is TextBox Then
                            Dim mTb As TextBox = CType(ActiveControl, TextBox)
                            mTb.Select(mTb.Text.Length, mTb.Text.Length)
                        ElseIf TypeOf (ActiveControl) Is NumericUpDown Then
                            Dim mTb As NumericUpDown = CType(ActiveControl, NumericUpDown)
                            mTb.Select(0, mTb.Text.Length)
                        End If
                    End If
                End If
            Case Else
        End Select
    End Sub
    Private Sub EnabledControls(ByVal Controls As Control(), ByVal Enabled As Boolean)
        For Each ctrl As Control In Controls
            If ctrl IsNot Nothing Then ctrl.Enabled = Enabled
        Next
    End Sub
    Protected Sub AddHandlers(ByVal ctl As Control)
        For Each i As Control In ctl.Controls
            AddHandler i.GotFocus, AddressOf GotFocusBackColor
            AddHandler i.LostFocus, AddressOf LostFocusBackColor
            AddHandler i.KeyPress, AddressOf ControlKeyPress
            If i.HasChildren Then
                AddHandlers(i)
            End If
        Next
    End Sub
    Private Sub LostFocusBackColor(ByVal sender As Object, ByVal e As System.EventArgs)
        If TypeOf sender Is TextBox Then
            If CType(sender, TextBox).Name.ToString <> "txtSearchText" Then
                CType(sender, TextBox).BackColor = Drawing.Color.White
            End If
        End If
        If TypeOf sender Is ComboBox Then
            CType(sender, ComboBox).BackColor = Drawing.Color.White
        End If
        If TypeOf sender Is MaskedTextBox Then
            CType(sender, MaskedTextBox).BackColor = Drawing.Color.White
        End If
        If TypeOf sender Is WaterMarkTextBox Then
            CType(sender, WaterMarkTextBox).BackColor = Drawing.Color.White
        End If
        If TypeOf sender Is NumericUpDown Then
            CType(sender, NumericUpDown).BackColor = Drawing.Color.White
        End If
    End Sub
    Private Sub GotFocusBackColor(ByVal sender As Object, ByVal e As System.EventArgs)
        If TypeOf sender Is TextBox Then
            CType(sender, TextBox).Text = CType(sender, TextBox).Text.Trim
            CType(sender, TextBox).BackColor = Color.LightGray
        End If
        If TypeOf sender Is ComboBox Then
            CType(sender, ComboBox).BackColor = Color.LightGray
        End If
        If TypeOf sender Is MaskedTextBox Then
            CType(sender, MaskedTextBox).BackColor = Color.LightGray
        End If
        If TypeOf sender Is WaterMarkTextBox Then
            CType(sender, WaterMarkTextBox).BackColor = Color.LightGray
        End If
        If TypeOf sender Is NumericUpDown Then
            CType(sender, NumericUpDown).BackColor = Color.LightGray
        End If
        'If TypeOf sender Is CheckBox Then
        '    CType(sender, CheckBox).ForeColor = Color.LightGray
        'End If
    End Sub


    Private Sub ControlKeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
        If e.Handled AndAlso Not TypeOf sender Is WaterMarkTextBox Then Exit Sub
        If IsLowerCaseControl(sender) = False Then e.KeyChar = UCase(e.KeyChar)
        If e.KeyChar = Chr(13) Then
            If sender.Name = "" Then Return
            If ((TypeOf sender Is TextBox AndAlso CType(sender, TextBox).Multiline = False) Or
                 TypeOf sender Is WaterMarkTextBox Or
                 TypeOf sender Is NumericUpDown Or
                 TypeOf sender Is ComboBox Or
                 TypeOf sender Is DateTimePicker Or
                 TypeOf sender Is Button Or
                 TypeOf sender Is NumericUpDown Or
                 TypeOf sender Is CheckBox) And
                 IsInTheList(sender, SkipTabControl) = False _
            Then
                SelectNextControl(sender, True, True, True, True)
            End If
            If ValidateControl(sender) = False Then Return
            If (LastControl IsNot Nothing) AndAlso (LastControl.Name = sender.name) Then
                LastControlAction()
                Return
            End If
        End If
    End Sub
    Private Function IsInTheList(ByVal target As Control, ctrls As Control()) As Boolean
        If ctrls Is Nothing Then Return False
        For Each ctrl As Control In ctrls
            If target.Name = ctrl.Name Then Return True
        Next
        Return False
    End Function
    Private Function IsLowerCaseControl(ctrl As Control) As Boolean
        If NotUpperCaseControl Is Nothing Then Return False
        For Each c As Control In NotUpperCaseControl
            If ctrl.Name = c.Name Then Return True
        Next
        Return False
    End Function
    Protected Function GetPropertyDescriptions(Of T)() As Dictionary(Of String, String)
        Dim properties = GetType(T).GetProperties(Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Public)
        For Each Prop As Reflection.PropertyInfo In properties
            Dim attributes = DirectCast(Prop.GetCustomAttributes(GetType(PropertyDescriptionAttribute), False), PropertyDescriptionAttribute())
            If attributes.Length > 0 Then
                If _PropertyDescription.ContainsKey(attributes(0).Key) = False Then
                    _PropertyDescription.Add(attributes(0).Key, attributes(0).Value)
                End If
            End If
        Next
        Return _PropertyDescription
    End Function
    Protected Function ReplacePropNameWithDescription(Of T)(ByVal propName As String, ByVal errorMsg As String) As String
        errorMsg = errorMsg.Replace(propName, GetPropertyDescriptions(Of T)().Item(propName))
        Return errorMsg
    End Function

    'Protected Function GetDisplayName(Of T)() As Dictionary(Of String, String)
    '    Dim properties = GetType(T).GetProperties(Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Public)
    '    For Each Prop As Reflection.PropertyInfo In properties
    '        Dim attributes = DirectCast(Prop.GetCustomAttributes(GetType(DisplayAttribute), False), DisplayAttribute())
    '        If attributes.Any Then
    '            If _PropertyDescription.ContainsKey(Prop.Name) = False Then
    '                _PropertyDescription.Add(Prop.Name, attributes(0).Name)
    '            End If
    '        End If
    '    Next
    '    Return _PropertyDescription
    'End Function
    'Protected Function ReplacePropNameWithDisplayName(Of T)(ByVal propName As String, ByVal errorMsg As String) As String
    '    errorMsg = errorMsg.Replace(propName, GetDisplayName(Of T)().Item(propName))
    '    Return errorMsg
    'End Function
    Protected Function CheckSetting(Of T)(ByVal key As String) As Object
        If Settings Is Nothing Then Return Nothing
        Dim mObj As Object = Settings.Where(Function(a) a.ckey = key).FirstOrDefault
        If mObj IsNot Nothing Then
            Dim mType As Type = GetType(T)
            Select Case 1 = 1
                Case mType.Equals(GetType(Boolean))
                    Return IIf(mObj.cValue = 0, False, True)
                Case mType.Equals(GetType(String))
                    Return mObj.cValue
            End Select
        End If
        Return Nothing
    End Function
    'Protected Sub GetSettings(ByVal coGrp As String, ByVal coID As String, ByVal coSys As String, ByVal formName As String, ByVal Optional key As String = Nothing)
    '    'Dim mGeneralMgr As New GeneralMgr(AuthenticationMgr.GetCoMasterConnStr)
    '    Dim mGeneralMgr As New GeneralMgr(ApplicationVariables.SQLCONNSTR)
    '    Dim mParameters As New List(Of KeyValueHelper)
    '    mParameters.Add(New KeyValueHelper("CoGrp", coGrp))
    '    mParameters.Add(New KeyValueHelper("CoID", coID))
    '    mParameters.Add(New KeyValueHelper("CoSys", coSys))
    '    mParameters.Add(New KeyValueHelper("Form", formName))
    '    If key IsNot Nothing Then mParameters.Add(New KeyValueHelper("Key", key))
    '    Dim mSql As String = String.Format("spSetting")
    '    Dim mObjectList As List(Of Object) = mGeneralMgr.GetdataReader(mSql, mParameters)
    '    If Not String.IsNullOrEmpty(mGeneralMgr.ErrorMsg) Then
    '        Throw New Exception(mGeneralMgr.ErrorMsg)
    '    End If
    '    Dim mAnonymousList = (From itm In mObjectList
    '                          Select cCoGrp = itm(0),
    '                                 cCoID = itm(1),
    '                                 cCoSys = itm(2),
    '                                 cForm = itm(3),
    '                                 cKey = itm(4),
    '                                 cValue = itm(5)
    '                       ).ToList
    '    _settings = mAnonymousList
    'End Sub
#End Region

#Region "Theme"
    Private Sub WinFormBase_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Me.IsLoadTheme.HasValue AndAlso Me.IsLoadTheme.Value Then LoadTheme(Me)
    End Sub
    Protected Sub LoadTheme(ctrl As Object)
        If ThemeColor.PrimaryColor.IsEmpty OrElse ThemeColor.SecondaryColor.IsEmpty Then Return
        For Each btns As Control In ctrl.Controls
            If btns.GetType() = GetType(Button) Then
                Dim btn As Button = CType(btns, Button)
                btn.BackColor = ThemeColor.PrimaryColor
                btn.ForeColor = Color.White
                btn.FlatAppearance.BorderColor = ThemeColor.SecondaryColor
                btn.FlatAppearance.BorderSize = 1
                btn.FlatStyle = FlatStyle.Flat
            End If

            If btns.GetType() = GetType(Label) Then
                Dim btn As Label = CType(btns, Label)
                btn.ForeColor = ThemeColor.SecondaryColor
                'btn.AutoSize = True
            End If

            'If btns.GetType() = GetType(DataGridView) Then
            '    Dim btn As DataGridView = CType(btns, DataGridView)
            '    btn.AlternatingRowsDefaultCellStyle.BackColor = ThemeColor.SecondaryColor
            'End If

            If btns.HasChildren Then
                LoadTheme(btns)
            End If
        Next
        'Label4.ForeColor = ThemeColor.SecondaryColor
        'Label5.ForeColor = ThemeColor.PrimaryColor
    End Sub


#End Region

End Class

