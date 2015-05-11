Imports System.Windows.Media

' http://easyrgb.com/index.php?X=MATH&H=22#text22
Public Class HSVColor

    <DebuggerBrowsable(DebuggerBrowsableState.Never)>
    Private _H As Double = 0
    Public Property H As Double
        Get
            Return (Me._H)
        End Get
        Set(value As Double)
            Me._H = value
        End Set
    End Property

    <DebuggerBrowsable(DebuggerBrowsableState.Never)>
    Private _S As Double = 0
    Public Property S As Double
        Get
            Return (Me._S)
        End Get
        Set(value As Double)
            Me._S = value
        End Set
    End Property

    <DebuggerBrowsable(DebuggerBrowsableState.Never)>
    Private _V As Double = 0
    Public Property V As Double
        Get
            Return (Me._V)
        End Get
        Set(value As Double)
            Me._V = value
        End Set
    End Property

    Public Sub New(h As Double, s As Double, v As Double)
        Me._H = h
        Me._S = s
        Me._V = v
    End Sub

    Public Shared Function FromColor(color As Color) As HSVColor
        Dim r As Double = color.R / 255
        Dim g As Double = color.G / 255
        Dim b As Double = color.B / 255
        Dim min As Double = MathHelper.Min(r, g, b)
        Dim max As Double = MathHelper.Max(r, g, b)

        If ((Double.MaxValue > min) AndAlso (Double.MinValue < max)) Then
            Dim deltaMax As Double = (max - min)
            Dim h As Double = 0
            Dim s As Double = 0
            Dim v As Double = 0

            v = max

            If (deltaMax = 0) Then
                h = 0
                s = 0

            Else
                s = deltaMax / max

                Dim deltaR As Double = (((max - r) / 6) + (deltaMax / 2)) / deltaMax
                Dim deltaG As Double = (((max - g) / 6) + (deltaMax / 2)) / deltaMax
                Dim deltaB As Double = (((max - b) / 6) + (deltaMax / 2)) / deltaMax

                If (r = max) Then
                    h = deltaB - deltaG

                ElseIf (g = max) Then
                    h = (1 / 3) + deltaR - deltaB

                ElseIf (b = max) Then
                    h = (2 / 3) + deltaG - deltaR
                End If

                If (h < 0) Then h += 1
                If (h > 1) Then h -= 1
            End If

            Return (New HSVColor(h, s, v))
        End If

        Return (Nothing)
    End Function

    Public Function ToColor() As Color
        Dim r As Double = 0
        Dim g As Double = 0
        Dim b As Double = 0

        If (Me.S = 0) Then
            r = Me.V * 255
            g = Me.V * 255
            b = Me.V * 255

        Else
            Dim valH As Double = Me.H * 6

            If (valH = 6) Then H = 0

            Dim i As Integer = CInt(valH)
            Dim val1 As Double = Me.V * (1 - Me.S)
            Dim val2 As Double = Me.V * (1 - Me.S * (valH - i))
            Dim val3 As Double = Me.V * (1 - Me.S * (1 - (valH - i)))

            If (i = 0) Then
                r = Me.V
                g = val3
                b = val1
            ElseIf (i = 1) Then
                r = val2
                g = Me.V
                b = val1

            ElseIf (i = 2) Then
                r = val1
                g = Me.V
                b = val3

            ElseIf (i = 3) Then
                r = val1
                g = val2
                b = Me.V

            ElseIf (i = 4) Then
                r = val3
                g = val1
                b = Me.V

            Else
                r = Me.V
                g = val1
                b = val2
            End If

            r *= 255
            g *= 255
            b *= 255
        End If

        Return (Color.FromArgb(255, r, g, b))
    End Function

End Class