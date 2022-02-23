Imports DataEntity
Imports DataEntityMgr
Imports GeneralAcc.Module
Imports Helper
Imports Helper.ApplicationTools
Imports System.ComponentModel
Imports System.Linq
Imports Validation

Public Class MtnBase


#Region "Fields"
    Protected _formName As String = ""
    Protected _connstr As String = ""
    Protected _dataEntityMgr As Object
    Protected WithEvents _bs As New BindingSource
#End Region

#Region "Property"
    Public Sub New()
        InitializeComponent()
    End Sub
    Public Sub New(ByVal connstr As String)
        Me.New
        _connstr = connstr
    End Sub

    'Sub SetSomething(Of C)(ByVal Data As C)
    '    Dim mMgr As DataEntityMgrBase(Of C) = _dataEntityMgr
    '    mMgr.SetDataEntity(Data)
    'End Sub

    Protected ReadOnly Property DataEntityBindingSource() As BindingSource
        Get
            Return _bs
        End Get
    End Property
    Protected Property FormName() As String
        Get
            Me.Text = String.Format("{0} Maintenance", _formName)
            Return Me.Text
        End Get
        Set(ByVal value As String)
            _formName = value
        End Set
    End Property
#End Region

#Region "Overrides"
    Protected Overrides Sub InitControls()
        btnLoading.Visible = False
        Me.KeyPreview = True
        'GetSettings(cCompany.CompanyGroup, cCompanyMaster.ID, cCompanyMaster.SysID, Me.Name)
    End Sub
    Protected Overrides Sub OnFormFlagChanged(ByVal objState As EnumFormState)
        MyBase.OnFormFlagChanged(objState)
    End Sub
    Protected Overrides Sub Cancel()
        Me.Text = FormName

        tsbtnSave.Text = "Save"
        ToolBarState(True, False)
        GroupBoxState(True, False)

        Me.BindingNavigator1.Enabled = True
        If DataEntityBindingSource.Count > 0 Then
            DataEntityBindingSource_PositionChanged(Nothing, Nothing)
        Else
            ClearGBItem(gbxDetail)
        End If

        tsbtnExit.Enabled = True
        Me.BindingNavigator1.Enabled = True

        txtSearch.Focus()
        dgvSearch.PerformLayout()
    End Sub
    Protected Overrides Sub [New]()
        Me.Text = String.Format("{0} - (ADDNEW)", FormName)
        Me.tsbtnExit.Enabled = False
        Me.BindingNavigator1.Enabled = False

        tsbtnSave.Text = "Save"
        ToolBarState(False, True)
        ClearGBItem(gbxDetail)
        GroupBoxState(False, True)
    End Sub
    Protected Overrides Sub Edit()
        Me.Text = String.Format("{0} - (EDIT)", FormName)
        Me.tsbtnExit.Enabled = False
        Me.BindingNavigator1.Enabled = False

        tsbtnSave.Text = "Save"
        ToolBarState(False, True)
        GroupBoxState(False, True)
    End Sub
    Protected Overrides Sub Delete()
        Me.Text = String.Format("{0} - (DELETE)", FormName)
        Me.tsbtnExit.Enabled = False
        Me.BindingNavigator1.Enabled = False

        tsbtnSave.Text = "Delete"
        ToolBarState(False, True)
        GroupBoxState(False, False)

        txtSearch.Focus()
    End Sub
    Protected Overrides Sub LastControlAction()
        If TypeOf (LastControl) Is TextBox OrElse
            TypeOf (LastControl) Is ComboBox OrElse
            TypeOf (LastControl) Is DateTimePicker OrElse
            TypeOf (LastControl) Is NumericUpDown OrElse
            TypeOf (LastControl) Is WaterMarkTextBox OrElse
            TypeOf (LastControl) Is CheckBox Then
            tsbtnSave.PerformClick()
        End If
    End Sub
    'called after Enter keypress
    Protected Overrides Function ValidateControl(ByVal ctrl As Control, Optional ByVal isSuppressErrMsg As Boolean = False) As Boolean
        Dim mListErr As List(Of KeyValueHelper) = CheckData(ctrl)
        If IsDataValid(mListErr) = False Then
            mListErr(0).Key.focus()
            HLField(mListErr(0).Key)
            Return False
        End If
        Return True
    End Function
#End Region

#Region "Form State"
    Private Sub MtnBase_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AddHandlers(Me)
        InitControls()
        MyBase.InitDGV(dgvSearch)
    End Sub
    Protected Sub ToolBarState(ByVal AddEditDeleteState As Boolean, ByVal SaveCancelState As Boolean)
        tsbtnNew.Enabled = False
        tsbtnEdit.Enabled = False
        tsbtnDelete.Enabled = False
        tsbtnPrint.Enabled = False

        Dim mUrMgr As UserRoleMgr = New UserRoleMgr(_connstr)
        Dim mUrList As List(Of UserRole) = mUrMgr.SetDataEntity(New UserRole(New KeyValueHelper(User.Properties.Role.ToString, cUser.Role))).
            Fetch(New MgrArgs(New OptionList(enumIsLike.equal))).DataEntityList
        If (mUrList.Count > 0) Then
            Dim mUr As UserRole = mUrList(0)
            tsbtnNew.Enabled = IIf(mUr.AddNew, AddEditDeleteState, False)
            If dgvSearch.RowCount > 0 Then
                tsbtnEdit.Enabled = IIf(mUr.Edit, AddEditDeleteState, False)
                tsbtnDelete.Enabled = IIf(mUr.Delete, AddEditDeleteState, False)
                tsbtnPrint.Enabled = IIf(mUr.Print, AddEditDeleteState, False)
            End If
        End If
        tsbtnSave.Enabled = SaveCancelState
        tsbtnCancel.Enabled = SaveCancelState
    End Sub
    Protected Sub GroupBoxState(ByVal SearchState As Boolean, ByVal DetailState As Boolean)
        gbxDetail.Enabled = DetailState
        gbxSearch.Enabled = SearchState
    End Sub
#End Region

#Region "ComboBox"
    Protected Overridable Sub InitComboBox(ByVal kvh As List(Of KeyValueHelper), Optional ByVal cbo As ComboBox = Nothing)
        'Dim mTableList As New List(Of KeyValueHelper)
        'For Each itm As KeyValueHelper In kvh
        '    mTableList.Add(New KeyValueHelper(itm.Key, itm.Value))
        'Next

        If cbo Is Nothing Then
            cbo = cboSearch
        End If
        ListComboBox(cbo, kvh)
        'mTableList = Nothing
    End Sub
#End Region

#Region "Search"
    Protected Overridable Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        LoadDGVData()
    End Sub
    Private Sub dataGridView1_CellPainting(ByVal sender As Object, ByVal e As DataGridViewCellPaintingEventArgs)
        If (e.Value Is Nothing) Then
            Return
        End If
        Dim s As SizeF = e.Graphics.MeasureString(e.Value.ToString, dgvSearch.Font)

        If (s.Width > dgvSearch.Columns(e.ColumnIndex).Width) Then
            Dim backColorBrush As Brush = New SolidBrush(e.CellStyle.BackColor)
            Dim gridBrush As Brush = New SolidBrush(Me.dgvSearch.GridColor)
            If e.RowIndex > -1 Then
                Dim rect As Rectangle = New Rectangle(e.CellBounds.Location, New Size(e.CellBounds.Width - 1, e.CellBounds.Height - 1))
                If dgvSearch.CurrentRow.Index = e.RowIndex Then
                    e.Graphics.FillRectangle(New SolidBrush(dgvSearch.DefaultCellStyle.SelectionBackColor), rect)
                    e.Graphics.DrawString(e.Value.ToString, dgvSearch.Font, Brushes.White, rect, StringFormat.GenericDefault)
                Else
                    e.Graphics.FillRectangle(backColorBrush, rect)
                    e.Graphics.DrawString(e.Value.ToString, dgvSearch.Font, Brushes.Black, rect, StringFormat.GenericDefault)

                End If

                e.Graphics.DrawLine(New Pen(dgvSearch.GridColor, 0.5), New Point(e.CellBounds.X, e.CellBounds.Y + e.CellBounds.Height - 1),
                   New Point(e.CellBounds.X + e.CellBounds.Width, e.CellBounds.Y + e.CellBounds.Height - 1))
                e.Graphics.DrawLine(New Pen(dgvSearch.GridColor, 0.5), New Point(e.CellBounds.X + e.CellBounds.Width - 1, e.CellBounds.Y),
                New Point(e.CellBounds.X + e.CellBounds.Width - 1, e.CellBounds.Y + e.CellBounds.Height - 1))

                Dim h As Integer = CType((s.Height * Math.Ceiling((s.Width / dgvSearch.Columns(e.ColumnIndex).Width))), Integer)
                If dgvSearch.Rows(e.RowIndex).Height <= h Then dgvSearch.Rows(e.RowIndex).Height = CType((s.Height * Math.Ceiling((s.Width / dgvSearch.Columns(e.ColumnIndex).Width))), Integer)
                e.Handled = True
            End If
        End If
    End Sub
    Private Sub dgvSearch_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles dgvSearch.CellFormatting
        Dim mColumnValue As Boolean
        If e.Value IsNot Nothing AndAlso Boolean.TryParse(e.Value.ToString, mColumnValue) Then
            If mColumnValue Then
                e.Value = "Yes"
            Else
                e.Value = "No"
            End If
        End If
    End Sub
    Protected Overrides Sub InitSearchDGV(ByVal dgv As DataGridView, Optional ByVal isEnquiry As Boolean = False)
    End Sub
    Protected Overridable Sub LoadDGVData(Optional isDirty As Boolean = False)
        If cboSearch.SelectedIndex < 0 Then Exit Sub
        If txtSearch.Text = "" Then
            lblFiltered.Visible = False
        Else
            lblFiltered.Visible = True
        End If
    End Sub
    Protected Overridable Sub LoadDetails(ByVal dataEntity As Object)
    End Sub
    Private Sub DataEntityBindingSource_DataSourceChanged(sender As Object, e As EventArgs) Handles _bs.DataSourceChanged
        DataEntityBindingSource_PositionChanged(Nothing, Nothing)
    End Sub
    Protected Overridable Sub DataEntityBindingSource_PositionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _bs.PositionChanged
        If DataEntityBindingSource.Count > 0 Then
            LoadDetails(DataEntityBindingSource.Current)
        End If
    End Sub
#End Region

#Region "Tools Strip Button"
    Protected Overridable Sub tsbtnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbtnNew.Click
        OnFormFlagChanged(EnumFormState.Add)
    End Sub
    Protected Overridable Sub tsbtnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbtnEdit.Click
        OnFormFlagChanged(EnumFormState.Edit)
    End Sub
    Protected Overridable Sub tsbtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbtnDelete.Click
        OnFormFlagChanged(EnumFormState.Delete)
    End Sub
    Protected Overridable Sub tsbtnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbtnCancel.Click
        OnFormFlagChanged(EnumFormState.Cancel)
    End Sub
    Protected Overridable Sub tsbtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbtnPrint.Click
    End Sub
    Protected Overridable Sub tsbtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbtnExit.Click
        Me.Close()
    End Sub
    Protected Overridable Sub tsbtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbtnSave.Click
        Dim mListErr As New List(Of KeyValueHelper)
        If MyBase.FormState <> EnumFormState.Delete Then
            mListErr = CheckData()
        End If
        If IsDataValid(mListErr) = False Then
            mListErr(0).Key.focus()
            HLField(mListErr(0).Key)
        Else
            If ConfirmBeforeSave() = DialogResult.Yes Then
                SaveRecord()
                OnFormFlagChanged(EnumFormState.Cancel)
            Else
                ActiveControl.Focus()
            End If
        End If
    End Sub
#End Region

#Region "Save Function"
    Protected Overridable Sub SaveRecord()
    End Sub
    Protected Overridable Function ConfirmBeforeSave() As DialogResult
        Dim mMyResponse As DialogResult
        If MyBase.FormState = EnumFormState.Edit Or MyBase.FormState = EnumFormState.Add Then
            mMyResponse = MessageBox.Show("Confirm to Save?", "Continue", MessageBoxButtons.YesNo,
                                                                      MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

        ElseIf MyBase.FormState = EnumFormState.Delete Then
            mMyResponse = MessageBox.Show("Confirm to delete?", "Continue", MessageBoxButtons.YesNo,
                                                                                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

        End If
        Return mMyResponse '= Windows.Forms.DialogResult.Yes
    End Function
    Protected Overridable Function CheckData(Optional ByVal ctrl As Object = Nothing) As List(Of KeyValueHelper)
        Return Nothing
    End Function
#End Region



End Class