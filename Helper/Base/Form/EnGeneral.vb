Imports System.Drawing
Imports System.Windows.Forms
Imports Aga.Controls.Tree

Public Class EnGeneral


#Region "Enum"
#End Region

#Region "Fields"
    Private _selectedDataEntity As List(Of Object)
    Private _dataEntityList As New List(Of Object)
    Private _propertyDescriptionList As New Dictionary(Of String, String)
    Private _dynamicType As System.Type
#End Region

#Region "Property"
    Public Sub New()
        InitializeComponent()
    End Sub
    Public WriteOnly Property SetFormName() As String
        Set(ByVal value As String)
            FormName = value
        End Set
    End Property
    Public ReadOnly Property SelectedDataEntity() As List(Of Object)
        Get
            If _selectedDataEntity Is Nothing Then
                _selectedDataEntity = New List(Of Object)
            End If
            Return _selectedDataEntity
        End Get
    End Property
    Public Property DynamicType() As System.Type    'Ctrl + D
        Get
            Return _dynamicType
        End Get
        Set(ByVal value As System.Type)
            _dynamicType = value
        End Set
    End Property
    Public Property DataEntityList() As List(Of Object) 'Load
        Get
            Return _dataEntityList
        End Get
        Set(ByVal value As List(Of Object))
            _dataEntityList = value
        End Set
    End Property
    Public Property PropertyDescriptionList() As Dictionary(Of String, String)  'Init dgv
        Get
            Return _propertyDescriptionList
        End Get
        Set(ByVal value As Dictionary(Of String, String))
            _propertyDescriptionList = value
        End Set
    End Property
#End Region


#Region "Overrides"
    Protected Overrides Sub InitControls()
        MyBase.InitControls()
        Panel1.Location = New Point(12, 12)
        Me.Height = Panel1.Height + 80
        btnExit.Visible = False
        'Panel1.Height = Me.Height - 70
        'Panel1.Width = Me.Width - 70
        'dgvSearch.Width = 1500
        'dgvSearch.Width = Panel1.Width - 30
    End Sub
#End Region

#Region "Form State"
    Private Sub EnAccNoGL_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        KeyControl = Nothing
        LastControl = Nothing
        dgvSearch.MultiSelect = ShowSelectButton
        btnSelect.Visible = dgvSearch.MultiSelect
        LoadDGVData()
        SetTotal()
    End Sub
    Public Sub Form1_KeyUp(sender As Object, e As KeyEventArgs) Handles MyBase.KeyUp
        If e.KeyCode = Keys.D AndAlso e.Control Then
            'If DynamicType Is Nothing Then Return
            'Dim mFrm As New FrmGlobalVar()
            'mFrm.GetDataEntityValues(DynamicType, dgvSearch.CurrentRow.DataBoundItem)
            'mFrm.ShowDialog()
            Dim mFrm As New FmQuickWatch
            mFrm.Watch(dgvSearch.CurrentRow.DataBoundItem)
        End If
    End Sub
#End Region


#Region "ComboBox"
#End Region

#Region "Search"
    Private Sub txtBox_TextChanged(sender As Object, e As EventArgs)
    End Sub
    Protected Overrides Sub SelectClick()
        SelectedDataEntity.Clear()
        For Each row As DataGridViewRow In dgvSearch.SelectedRows
            SelectedDataEntity.Add(row.DataBoundItem)
        Next
        DialogResult = DialogResult.OK
    End Sub
    Protected Overrides Sub LoadDGVData(Optional isDirty As Boolean = False)
        DataEntityBindingSource.DataSource = DataEntityList
        dgvSearch.DataSource = DataEntityBindingSource
        Total = DataEntityList.Count
        CurrentTotal = DataEntityBindingSource.Count
        SetTotal()
    End Sub
    Protected Overrides Sub InitSearchDGV(ByVal dgv As DataGridView, Optional ByVal isEnquiry As Boolean = False)
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
        For Each prop As String In IncludeFieldList
            With dgv
                Dim mNewColIndex As Integer
                mNewColIndex = .Columns.Add(prop, PropertyDescriptionList.Item(prop))
                .Columns(mNewColIndex).DataPropertyName = prop
            End With
        Next
    End Sub
#End Region


#Region "Function"
    Private Sub SetTotal()
        lblTotal.Text = ShowTotal
    End Sub

#End Region
End Class

