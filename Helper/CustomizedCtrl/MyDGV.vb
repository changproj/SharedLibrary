Imports System.Windows.Forms

Public Class MyDGV
    Inherits DataGridView

    Protected Overrides Function ProcessDialogKey(ByVal keyData As Keys) As Boolean
        Dim key As Keys = keyData And Keys.KeyCode

        If key = Keys.Enter Then
            MyBase.OnKeyDown(New KeyEventArgs(keyData))
            Return True
        Else
            Return MyBase.ProcessDialogKey(keyData)
        End If
    End Function
End Class
