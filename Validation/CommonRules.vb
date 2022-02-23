Imports System.Text.RegularExpressions


Namespace Validation

    Public Module CommonRules

#Region " StringRequired "
        Public Function StringRequired(ByVal target As Object, ByVal e As RuleArgs) As Boolean
            Dim mValue As String = CStr(CallByName(target, e.PropertyName, CallType.Get))
            If Len(mValue?.Trim) = 0 Then
                e.Description = String.Format("{0} is required.", e.PropertyName)
                Return False
            Else
                Return True
            End If
        End Function
#End Region


#Region " DateRequired "

        Public Function DateRequired(ByVal target As Object, ByVal e As RuleArgs) As Boolean
            Dim mValue As String = CStr(CallByName(target, e.PropertyName, CallType.Get))
            If IsDate(mValue) = False Then
                e.Description =
                  String.Format("{0} is required.", e.PropertyName)
                Return False
            Else
                Return True
            End If
        End Function

#End Region

#Region " NumberRequired "

        Public Function NumberRequired(ByVal target As Object, ByVal e As RuleArgs) As Boolean
            Dim mValue As String = CStr(CallByName(target, e.PropertyName, CallType.Get))
            If IsNumeric(mValue) = False Then
                e.Description =
                  String.Format("{0} is required.", e.PropertyName)
                Return False
            Else
                Return True
            End If
        End Function

#End Region

#Region " StringMaxLength "
        Public Function StringMaxLength(ByVal target As Object, ByVal e As RuleArgs) As Boolean
            Dim mMax As Integer = DirectCast(e, MaxLengthRuleArgs).MaxLength
            If Len(CallByName(target, e.PropertyName, CallType.Get).ToString) > mMax Then
                e.Description = String.Format("{0} can not exceed {1} characters.", e.PropertyName, mMax)
                Return False
            Else
                Return True
            End If
        End Function

        Public Class MaxLengthRuleArgs
            Inherits RuleArgs

            Private mMaxLength As Integer
            Public Sub New(ByVal propertyName As String, ByVal maxLength As Integer)
                MyBase.New(propertyName)
                mMaxLength = maxLength
            End Sub
            Public ReadOnly Property MaxLength() As Integer
                Get
                    Return mMaxLength
                End Get
            End Property
            Public Overrides Function ToString() As String
                Return MyBase.ToString & "!" & mMaxLength.ToString
            End Function
        End Class
#End Region

#Region " IntegerMaxValue "

        Public Function IntegerMaxValue(ByVal target As Object, ByVal e As RuleArgs) As Boolean
            Dim max As Integer = CType(e, IntegerMaxValueRuleArgs).MaxValue
            Dim value As Integer = CType(CallByName(target, e.PropertyName, CallType.Get), Integer)
            If value > max Then
                e.Description = String.Format("{0} can not exceed {1}.", e.PropertyName, max.ToString)
                Return False
            Else
                Return True
            End If
        End Function

        Public Class IntegerMaxValueRuleArgs
            Inherits RuleArgs

            Private mMaxValue As Integer
            Public Sub New(ByVal propertyName As String, ByVal maxValue As Integer)
                MyBase.New(propertyName)
                mMaxValue = maxValue
            End Sub
            Public ReadOnly Property MaxValue() As Integer
                Get
                    Return mMaxValue
                End Get
            End Property
            Public Overrides Function ToString() As String
                Return MyBase.ToString & "!" & mMaxValue.ToString
            End Function

        End Class

#End Region

#Region " IntegerMinValue "

        Public Function IntegerMinValue(ByVal target As Object, ByVal e As RuleArgs) As Boolean
            Dim min As Integer = CType(e, IntegerMinValueRuleArgs).MinValue
            Dim value As Integer = CType(CallByName(target, e.PropertyName, CallType.Get), Integer)
            If value < min Then
                e.Description = String.Format("{0} can not be less than {1}.", e.PropertyName, min.ToString)
                Return False
            Else
                Return True
            End If
        End Function

        Public Class IntegerMinValueRuleArgs
            Inherits RuleArgs

            Private mMinValue As Integer
            Public Sub New(ByVal propertyName As String, ByVal minValue As Integer)
                MyBase.New(propertyName)
                mMinValue = minValue
            End Sub
            Public ReadOnly Property MinValue() As Integer
                Get
                    Return mMinValue
                End Get
            End Property
            Public Overrides Function ToString() As String
                Return MyBase.ToString & "!" & mMinValue.ToString
            End Function

        End Class

#End Region

#Region " MaxValue "

        Public Function MaxValue(Of T)(ByVal target As Object, ByVal e As RuleArgs) As Boolean

            Dim mMax As Object = CType(e, MaxValueRuleArgs(Of T)).MaxValue
            Dim mValue As Object = CallByName(target, e.PropertyName, CallType.Get)
            Dim mResult As Boolean
            Dim mType As Type = GetType(T)
            If mType.IsPrimitive Then
                If mType.Equals(GetType(Integer)) Then
                    mResult = (CInt(mValue) <= CInt(mMax))

                ElseIf mType.Equals(GetType(Boolean)) Then
                    mResult = (CBool(mValue) <= CBool(mMax))

                ElseIf mType.Equals(GetType(Single)) Then
                    mResult = (CSng(mValue) <= CSng(mMax))

                ElseIf mType.Equals(GetType(Double)) Then
                    mResult = (CDbl(mValue) <= CDbl(mMax))

                ElseIf mType.Equals(GetType(Byte)) Then
                    mResult = (CByte(mValue) <= CByte(mMax))

                ElseIf mType.Equals(GetType(Char)) Then
                    mResult = (CChar(mValue) <= CChar(mMax))

                ElseIf mType.Equals(GetType(Short)) Then
                    mResult = (CShort(mValue) <= CShort(mMax))

                ElseIf mType.Equals(GetType(Long)) Then
                    mResult = (CLng(mValue) <= CLng(mMax))

                ElseIf mType.Equals(GetType(UShort)) Then
                    mResult = (CUShort(mValue) <= CUShort(mMax))

                ElseIf mType.Equals(GetType(UInteger)) Then
                    mResult = (CUInt(mValue) <= CUInt(mMax))

                ElseIf mType.Equals(GetType(ULong)) Then
                    mResult = (CULng(mValue) <= CULng(mMax))

                ElseIf mType.Equals(GetType(SByte)) Then
                    mResult = (CSByte(mValue) <= CSByte(mMax))
                Else
                    Throw New ArgumentException("Type parameter must be a primitive type.")
                End If
            Else  ' not primitive
                Throw New ArgumentException("Type parameter must be a primitive type.")
            End If

            If Not mResult Then
                e.Description = String.Format("{0} can not exceed {1}.", e.PropertyName, mMax.ToString)
                Return False
            Else
                Return True
            End If
        End Function

        Public Class MaxValueRuleArgs(Of T)
            Inherits RuleArgs

            Private mMaxValue As T
            Public Sub New(ByVal propertyName As String, ByVal maxValue As T)
                MyBase.New(propertyName)
                mMaxValue = maxValue
            End Sub
            Public ReadOnly Property MaxValue() As T
                Get
                    Return mMaxValue
                End Get
            End Property
            Public Overrides Function ToString() As String
                Return MyBase.ToString & "!" & mMaxValue.ToString
            End Function
        End Class

#End Region

#Region " MinValue "

        Public Function MinValue(Of T)(ByVal target As Object, ByVal e As RuleArgs) As Boolean
            Dim mMin As Object = CType(e, MinValueRuleArgs(Of T)).MinValue
            Dim mValue As Object = CallByName(target, e.PropertyName, CallType.Get)
            Dim mResult As Boolean
            Dim mType As Type = GetType(T)
            If mType.IsPrimitive Then
                If mType.Equals(GetType(Integer)) Then
                    mResult = (CInt(mValue) >= CInt(mMin))

                ElseIf mType.Equals(GetType(Boolean)) Then
                    mResult = (CBool(mValue) >= CBool(mMin))

                ElseIf mType.Equals(GetType(Single)) Then
                    mResult = (CSng(mValue) >= CSng(mMin))

                ElseIf mType.Equals(GetType(Double)) Then
                    mResult = (CDbl(mValue) >= CDbl(mMin))

                ElseIf mType.Equals(GetType(Byte)) Then
                    mResult = (CByte(mValue) >= CByte(mMin))

                ElseIf mType.Equals(GetType(Char)) Then
                    mResult = (CChar(mValue) >= CChar(mMin))

                ElseIf mType.Equals(GetType(Short)) Then
                    mResult = (CShort(mValue) >= CShort(mMin))

                ElseIf mType.Equals(GetType(Long)) Then
                    mResult = (CLng(mValue) >= CLng(mMin))

                ElseIf mType.Equals(GetType(UShort)) Then
                    mResult = (CUShort(mValue) >= CUShort(mMin))

                ElseIf mType.Equals(GetType(UInteger)) Then
                    mResult = (CUInt(mValue) >= CUInt(mMin))

                ElseIf mType.Equals(GetType(ULong)) Then
                    mResult = (CULng(mValue) >= CULng(mMin))

                ElseIf mType.Equals(GetType(SByte)) Then
                    mResult = (CSByte(mValue) >= CSByte(mMin))
                Else
                    Throw New ArgumentException("Type parameter must be a primitive type.")
                End If
            Else  ' not primitive
                Throw New ArgumentException("Type parameter must be a primitive type.")
            End If

            If Not mResult Then
                e.Description = String.Format("{0} can not be less than {1}.", e.PropertyName, mMin.ToString)
                Return False
            Else
                Return True
            End If

        End Function

        Public Class MinValueRuleArgs(Of T)
            Inherits RuleArgs

            Private mMinValue As T
            Public Sub New(ByVal propertyName As String, ByVal minValue As T)
                MyBase.New(propertyName)
                mMinValue = minValue
            End Sub
            Public ReadOnly Property MinValue() As T
                Get
                    Return mMinValue
                End Get
            End Property
            Public Overrides Function ToString() As String
                Return MyBase.ToString & "!" & mMinValue.ToString
            End Function

        End Class

#End Region

#Region " RegEx "

        Public Function RegExMatch(ByVal target As Object, ByVal e As RuleArgs) As Boolean
            Dim mRegEx As Regex = DirectCast(e, RegExRuleArgs).RegEx
            If Not mRegEx.IsMatch(CallByName(target, e.PropertyName, CallType.Get).ToString) Then
                e.Description = String.Format("{0} does not match regular expression.", e.PropertyName)
                Return False
            Else
                Return True
            End If
        End Function

        Public Enum RegExPatterns
            Email
        End Enum

        Public Class RegExRuleArgs
            Inherits RuleArgs

            Private mRegEx As Regex
            Public Sub New(ByVal propertyName As String, ByVal pattern As RegExPatterns)
                MyBase.New(propertyName)
                mRegEx = New Regex(GetPattern(pattern))
            End Sub
            Public Sub New(ByVal propertyName As String, ByVal pattern As String)
                MyBase.New(propertyName)
                mRegEx = New Regex(pattern)
            End Sub
            Public Sub New(ByVal propertyName As String, ByVal regEx As Regex)
                MyBase.New(propertyName)
                mRegEx = regEx
            End Sub
            Public ReadOnly Property RegEx() As Regex
                Get
                    Return mRegEx
                End Get
            End Property
            Public Overrides Function ToString() As String
                Return MyBase.ToString & "!" & mRegEx.ToString
            End Function
            Public Shared Function GetPattern(ByVal pattern As RegExPatterns) As String
                Select Case pattern
                    'Case RegExPatterns.SSN
                    '    Return "^\d{3}-\d{2}-\d{4}$"

                    Case RegExPatterns.Email
                        Return "\b[A-Z0-9._%-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b"

                    Case Else
                        Return ""
                End Select
            End Function

        End Class

#End Region


    End Module

End Namespace
