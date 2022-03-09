Module Exercise
    Delegate Function Multiplication(ByVal value As Double) As Double

    Public Function Area(ByVal Side As Double) As Double
        Return 6 * Side * Side
    End Function

    Public Function Volume(ByVal Side As Double) As Double
        Return Side * Side * Side
    End Function

    Public Function Main() As Integer

        Dim Side As Double = 46.95
        Dim AreaDefinition As Multiplication
        Dim VolDefinition As Multiplication

        AreaDefinition = New Multiplication(AddressOf Area)
        VolDefinition = New Multiplication(AddressOf Volume)

        MsgBox("Cube Calculation" & vbCrLf &
               "Side: " & vbTab & CStr(Side) & vbCrLf &
               "Area: " & vbTab & CStr(AreaDefinition.Invoke(Side)) & vbCrLf &
               "Volume: " & vbTab & CStr(VolDefinition.Invoke(Side)))

        Return 0
    End Function

End Module