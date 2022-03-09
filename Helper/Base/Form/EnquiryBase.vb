Imports System.Drawing
Imports System.Windows.Forms
Imports Helper.ApplicationTools
Imports Helper

Public Class EnquiryBase

#Region "Fields"
    Protected _formName As String = ""
    Protected _connstr As String = ""
    Protected _currentTotal As String = "0"
    Protected _Total As String = "0"
    Protected _dataEntityMgr As Object
    Protected WithEvents _bs As New BindingSource
    Protected _isDataEntityListLoaded As Boolean = False
    Protected _showSelectButton As Boolean
    Protected _sortingDirection As enumSortDirection
    Protected _disableControlSetProperty As Boolean = False
    Protected _includeFieldList As New List(Of String)
    Protected _bypassTextChangedList As List(Of Control)
#End Region

#Region "Property"
    Public Sub New()
        InitializeComponent()
    End Sub
    Public Sub New(ByVal connstr As String)
        Me.New
        _connstr = connstr
    End Sub
    Protected Property FormName() As String
        Get
            Me.Text = String.Format("{0} Enquiry", _formName)
            Return Me.Text
        End Get
        Set(ByVal value As String)
            _formName = value
        End Set
    End Property
    Protected ReadOnly Property DataEntityBindingSource() As BindingSource
        Get
            Return _bs
        End Get
    End Property
    Protected ReadOnly Property IsDataEntityListLoaded() As Boolean
        Get
            Return _isDataEntityListLoaded
        End Get
    End Property
    Protected Property CurrentTotal() As String
        Get
            Return _currentTotal
        End Get
        Set(value As String)
            _currentTotal = value
        End Set
    End Property
    Protected Property Total() As String
        Get
            Return _Total
        End Get
        Set(value As String)
            _Total = value
        End Set
    End Property
    Protected ReadOnly Property ShowTotal() As String
        Get
            Return String.Format("{0} / {1}", _currentTotal, _Total)
        End Get
    End Property
    Public Property ShowSelectButton() As Boolean
        Get
            Return _showSelectButton
        End Get
        Set(ByVal value As Boolean)
            _showSelectButton = value
        End Set
    End Property
    Public Property SortingDirection() As enumSortDirection
        Get
            Return _sortingDirection
        End Get
        Set(ByVal value As enumSortDirection)
            _sortingDirection = value
        End Set
    End Property
    Public Property DisableControlSetProperty() As Boolean
        Get
            Return _disableControlSetProperty
        End Get
        Set(ByVal value As Boolean)
            _disableControlSetProperty = value
        End Set
    End Property
    Public ReadOnly Property IncludeFieldList As List(Of String)
        Get
            Return _includeFieldList
        End Get
    End Property
    Public ReadOnly Property BypassTextChangedList As List(Of Control)
        Get
            If _bypassTextChangedList Is Nothing Then
                _bypassTextChangedList = New List(Of Control)
            End If
            Return _bypassTextChangedList
        End Get
    End Property

#End Region

#Region "Overridable"
    Protected Overridable Sub SelectClick()
    End Sub
#End Region

#Region "Overrides"
    Protected Overrides Sub InitControls()
        Me.KeyPreview = True
        btnLoading.Visible = False
        lblFiltered.Visible = False
        Me.Text = FormName
        'GetSettings(cCompany.CompanyGroup, cCompanyMaster.ID, cCompanyMaster.SysID, Me.Name)
    End Sub
    Protected Overrides Sub LastControlAction()
        If TypeOf (LastControl) Is TextBox OrElse
            TypeOf (LastControl) Is ComboBox OrElse
            TypeOf (LastControl) Is DateTimePicker OrElse
            TypeOf (LastControl) Is NumericUpDown OrElse
            TypeOf (LastControl) Is WaterMarkTextBox OrElse
            TypeOf (LastControl) Is CheckBox Then

        End If
    End Sub
#End Region

#Region "Form State"
    Private Sub MtnBase_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AddHandlers(Me)
        MyBase.InitDGV(dgvSearch)
        InitControls()
        StartPosition = FormStartPosition.CenterScreen
    End Sub

#End Region

#Region "ComboBox"

#End Region

#Region "Search"

#End Region

#Region "Button"
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Close()
    End Sub
    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        SelectClick()
    End Sub
#End Region

#Region "Datagridview"
    Protected Overridable Sub LoadDGVData(Optional isDirty As Boolean = False)
        If isDirty Then
            lblFiltered.Visible = True
        Else
            lblFiltered.Visible = False
        End If
    End Sub
    Protected Overrides Sub InitSearchDGV(ByVal dgv As DataGridView, Optional ByVal isEnquiry As Boolean = False)
    End Sub
    Protected Sub dgvSearch_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles dgvSearch.CellFormatting
        If e.Value Is Nothing Then Return
        Select Case e.Value.GetType.Name
            Case GetType(Decimal).Name
                e.CellStyle.Format = "#,##0.00;(#,##0.00)"
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                e.CellStyle.ForeColor = If(e.Value >= 0, e.CellStyle.ForeColor, Color.Red)
            Case GetType(Boolean).Name
                If e.Value Then
                    e.Value = "Yes"
                Else
                    e.Value = "No"
                End If
            Case GetType(Date).Name
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        End Select
    End Sub
    Protected Sub dataGridView1_CellPainting(ByVal sender As Object, ByVal e As DataGridViewCellPaintingEventArgs) Handles dgvSearch.CellPainting
        'If (e.Value Is Nothing) Then Return

        'Dim s As SizeF = e.Graphics.MeasureString(e.Value.ToString, dgvSearch.Font)

        'If (s.Width > dgvSearch.Columns(e.ColumnIndex).Width) Then
        '    Dim backColorBrush As Brush = New SolidBrush(e.CellStyle.BackColor)
        '    Dim gridBrush As Brush = New SolidBrush(Me.dgvSearch.GridColor)
        '    If e.RowIndex > -1 Then
        '        Dim rect As Rectangle = New Rectangle(e.CellBounds.Location, New Size(e.CellBounds.Width - 1, e.CellBounds.Height - 1))
        '        If dgvSearch.CurrentRow.Index = e.RowIndex Then
        '            e.Graphics.FillRectangle(New SolidBrush(dgvSearch.DefaultCellStyle.SelectionBackColor), rect)
        '            e.Graphics.DrawString(e.Value.ToString, dgvSearch.Font, Brushes.White, rect, StringFormat.GenericDefault)
        '        Else
        '            e.Graphics.FillRectangle(backColorBrush, rect)
        '            e.Graphics.DrawString(e.Value.ToString, dgvSearch.Font, Brushes.Black, rect, StringFormat.GenericDefault)

        '        End If

        '        e.Graphics.DrawLine(New Pen(dgvSearch.GridColor, 0.5), New Point(e.CellBounds.X, e.CellBounds.Y + e.CellBounds.Height - 1),
        '           New Point(e.CellBounds.X + e.CellBounds.Width, e.CellBounds.Y + e.CellBounds.Height - 1))
        '        e.Graphics.DrawLine(New Pen(dgvSearch.GridColor, 0.5), New Point(e.CellBounds.X + e.CellBounds.Width - 1, e.CellBounds.Y),
        '        New Point(e.CellBounds.X + e.CellBounds.Width - 1, e.CellBounds.Y + e.CellBounds.Height - 1))

        '        Dim h As Integer = CType((s.Height * Math.Ceiling((s.Width / dgvSearch.Columns(e.ColumnIndex).Width))), Integer)
        '        If dgvSearch.Rows(e.RowIndex).Height <= h Then dgvSearch.Rows(e.RowIndex).Height = CType((s.Height * Math.Ceiling((s.Width / dgvSearch.Columns(e.ColumnIndex).Width))), Integer)
        '        e.Handled = True
        '    End If
        'End If
    End Sub
    Protected Sub dgvSearch_DoubleClick(sender As Object, e As EventArgs) Handles dgvSearch.DoubleClick
        SelectClick()
    End Sub

    Protected Sub dgvSearch_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvSearch.ColumnHeaderMouseClick
        dgvSearch.Columns(e.ColumnIndex).SortMode = DataGridViewColumnSortMode.Programmatic
        If DataEntityBindingSource.Count = 0 Then Return
        Dim mQuery = From sortedListItem In CType(DataEntityBindingSource.List, IEnumerable(Of Object))
        Select Case SortingDirection
            Case enumSortDirection.Asc
                mQuery = mQuery.OrderBy(Function(o) o.GetType.GetProperty(dgvSearch.Columns(e.ColumnIndex).DataPropertyName).GetValue(o, Nothing))
                SortingDirection = enumSortDirection.Desc
            Case enumSortDirection.Desc
                mQuery = mQuery.OrderByDescending(Function(o) o.GetType.GetProperty(dgvSearch.Columns(e.ColumnIndex).DataPropertyName).GetValue(o, Nothing))
                SortingDirection = enumSortDirection.Asc
        End Select
        DataEntityBindingSource.DataSource = Nothing
        DataEntityBindingSource.DataSource = mQuery
    End Sub
#End Region

#Region "Function"
    Protected Sub SetValueDisableControl(ByVal control As Control, ByVal value As Object)
        BypassTextChangedList.Add(control)
        Select Case True
            Case TypeOf (control) Is TextBox
                control.Text = value
                control.Enabled = DisableControlSetProperty
            Case TypeOf (control) Is WaterMarkTextBox
                control.Text = value
                control.Enabled = DisableControlSetProperty
            Case TypeOf (control) Is CheckBox
                CType(control, CheckBox).CheckState = SafeCheckbox(value)
                control.Enabled = DisableControlSetProperty
        End Select
    End Sub
    Protected Function IsInTheList(ByVal target As Control) As Boolean
        If BypassTextChangedList.Count = 0 Then Return False
        For Each ctrl As Control In BypassTextChangedList
            If target.Name = ctrl.Name Then Return True
        Next
        Return False
    End Function
#End Region


End Class
