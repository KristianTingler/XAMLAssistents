Public Class XAMLAssistent
    Inherits FrameworkElement

#Region "Show Hover Property"

    Public Shared ReadOnly ShowHoverProperty As DependencyProperty = DependencyProperty.RegisterAttached("ShowHover", GetType(Boolean), GetType(XAMLAssistent), New PropertyMetadata(False, AddressOf ShowHoverPropertyChanged))

    Public Shared Sub SetShowHover(o As DependencyObject, value As Boolean)
        o.SetValue(ShowHoverProperty, value)
    End Sub

    Public Shared Function GetShowHover(o As DependencyObject) As Boolean
        Return (CType(o.GetValue(ShowHoverProperty), Boolean))
    End Function

#End Region

#Region "Original Color Property"

    Public Shared ReadOnly OriginalColorProperty As DependencyProperty = DependencyProperty.Register("OriginalColor", GetType(Color), GetType(XAMLAssistent))

    Public Shared Sub SetOriginalColor(o As DependencyObject, value As Color)
        o.SetValue(OriginalColorProperty, value)
    End Sub

    Public Shared Function GetOriginalColor(o As DependencyObject) As Color
        Return (CType(o.GetValue(OriginalColorProperty), Color))
    End Function

#End Region

    Private Shared Sub ShowHoverPropertyChanged(s As DependencyObject, e As DependencyPropertyChangedEventArgs)
        If ((s IsNot Nothing) AndAlso (TypeOf s Is Border)) Then
            Dim b As Border = CType(s, Border)

            AddHandler b.MouseEnter, AddressOf Border_MouseEnter
            AddHandler b.MouseLeave, AddressOf Border_MouseLeave
        End If
    End Sub

    Private Shared Sub Border_MouseEnter(sender As Object, e As MouseEventArgs)
        Dim b As Border = CType(sender, Border)

        If ((b.Background IsNot Nothing) AndAlso (TypeOf b.Background Is SolidColorBrush)) Then
            Dim originalColor As Color = CType(b.Background, SolidColorBrush).Color
            Dim hsvColor As HSVColor = hsvColor.FromColor(originalColor)

            hsvColor.V += 0.1

            b.SetValue(OriginalColorProperty, originalColor)
            b.Background = New SolidColorBrush(hsvColor.ToColor())
        End If
    End Sub

    Private Shared Sub Border_MouseLeave(sender As Object, e As MouseEventArgs)
        Dim b As Border = CType(sender, Border)

        If ((b.Background IsNot Nothing) AndAlso (TypeOf b.Background Is SolidColorBrush)) Then
            Dim obj As Object = b.GetValue(OriginalColorProperty)

            If ((obj IsNot Nothing) AndAlso (TypeOf obj Is Color)) Then
                b.Background = New SolidColorBrush(CType(obj, Color))
            End If
        End If
    End Sub

End Class