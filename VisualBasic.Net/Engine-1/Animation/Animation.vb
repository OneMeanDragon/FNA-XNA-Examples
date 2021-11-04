Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Audio
Imports Microsoft.Xna.Framework.Content
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Input
Imports Microsoft.Xna.Framework.Media

Namespace GameEngine
    Public Class Animation
        Public [name] As String
        Public [animationOrder] As List(Of Integer) = New List(Of Integer)
        Public [speed] As Integer

        Public Sub New()
        End Sub

        Public Sub New(vInputName As String, vInputSpeed As Integer, vInputAnimationOrder As List(Of Integer))
            [name] = vInputName
            [speed] = vInputSpeed
            [animationOrder] = vInputAnimationOrder
        End Sub
    End Class

    Public Class AnimationSet
        Public [width] As Integer
        Public [height] As Integer
        Public [gridX] As Integer
        Public [gridY] As Integer

        Public [animationList] As List(Of Animation) = New List(Of Animation)

        Public Sub New()
        End Sub

        Public Sub New(vWidth As Integer, vHeight As Integer, vGridX As Integer, vGridY As Integer)
            [width] = vWidth
            [height] = vHeight
            [gridX] = vGridX
            [gridY] = vGridY
        End Sub
    End Class

    Public Class AnimationData
        Public Property [animation] As AnimationSet
        Public Property [texturePath] As String
    End Class

End Namespace