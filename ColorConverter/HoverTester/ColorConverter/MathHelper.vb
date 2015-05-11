Public Class MathHelper

    Public Shared Function Min(ParamArray values() As Double) As Double
        If (values IsNot Nothing) Then
            Dim minResult As Double = Double.MaxValue

            For Each v As Double In values
                If (v < minResult) Then
                    minResult = v
                End If
            Next

            Return (minResult)
        End If

        Return (Double.MaxValue)
    End Function

    Public Shared Function Max(ParamArray values() As Double) As Double
        If (values IsNot Nothing) Then
            Dim maxResult As Double = Byte.MinValue

            For Each v As Double In values
                If (v > maxResult) Then
                    maxResult = v
                End If
            Next

            Return (maxResult)
        End If

        Return (Double.MinValue)
    End Function

End Class