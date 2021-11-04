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
    Public Class Camera
        Private Shared transformMatrix As Matrix
        Private Shared position As Vector2
        Public Shared rotation As Single
        Private Shared _Zoom As Single
        Private Shared _ScreenRect As Rectangle
        Public Shared updateYAxis As Boolean = False
        Public Shared updateXAxis As Boolean = True

        Shared Sub Initialize()
            Zoom = 1.0F
            rotation = 0.0F
            position = New Vector2(Resolution.VirtualWidth / 2, Resolution.VirtualHeight / 2)
        End Sub

        Public Shared ReadOnly Property ScreenRect As Rectangle
            Get
                Return _ScreenRect
            End Get
        End Property

        Public Shared Property Zoom As Single
            Get
                Return _Zoom
            End Get
            Set(ByVal value As Single)
                _Zoom = value
                If _Zoom < 0.1F Then _Zoom = 0.1F
            End Set
        End Property

        Shared Sub Update(ByVal follow As Vector2)
            UpdateMovement(follow)
            CalculateMatrixAndRectangle()
        End Sub

        Private Shared Sub UpdateMovement(ByVal follow As Vector2)
            If updateXAxis = True Then position.X += ((follow.X - position.X))
            If updateYAxis = True Then position.Y += ((follow.Y - position.Y))
        End Sub

        Shared Sub LookAt(ByVal lookAt As Vector2)
            If updateXAxis = True Then position.X = lookAt.X
            If updateYAxis = True Then position.Y = lookAt.Y
        End Sub

        Private Shared Sub CalculateMatrixAndRectangle()
            transformMatrix = Matrix.CreateTranslation(New Vector3(-position, 0)) * Matrix.CreateRotationZ(rotation) * Matrix.CreateScale(New Vector3(Zoom, Zoom, 1)) * Matrix.CreateTranslation(New Vector3(Resolution.VirtualWidth * 0.5F, Resolution.VirtualHeight * 0.5F, 0))
            transformMatrix = transformMatrix * Resolution.getTransformationMatrix()
            transformMatrix.M41 = CSng(Math.Round(transformMatrix.M41, 0))
            transformMatrix.M42 = CSng(Math.Round(transformMatrix.M42, 0))
            _ScreenRect = VisibleArea()
        End Sub

        Private Shared Function VisibleArea() As Rectangle
            Dim inverseViewMatrix As Matrix = Matrix.Invert(transformMatrix)
            Dim tl As Vector2 = Vector2.Transform(Vector2.Zero, inverseViewMatrix)
            Dim tr As Vector2 = Vector2.Transform(New Vector2(Resolution.VirtualWidth, 0), inverseViewMatrix)
            Dim bl As Vector2 = Vector2.Transform(New Vector2(0, Resolution.VirtualHeight), inverseViewMatrix)
            Dim br As Vector2 = Vector2.Transform(New Vector2(Resolution.VirtualWidth, Resolution.VirtualHeight), inverseViewMatrix)
            Dim min As Vector2 = New Vector2(MathHelper.Min(tl.X, MathHelper.Min(tr.X, MathHelper.Min(bl.X, br.X))), MathHelper.Min(tl.Y, MathHelper.Min(tr.Y, MathHelper.Min(bl.Y, br.Y))))
            Dim max As Vector2 = New Vector2(MathHelper.Max(tl.X, MathHelper.Max(tr.X, MathHelper.Max(bl.X, br.X))), MathHelper.Max(tl.Y, MathHelper.Max(tr.Y, MathHelper.Max(bl.Y, br.Y))))
            Return New Rectangle(CInt(min.X), CInt(min.Y), CInt((Resolution.VirtualWidth / Zoom)), CInt((Resolution.VirtualHeight / Zoom)))
        End Function

        Shared Function GetTransformMatrix() As Matrix
            Return transformMatrix
        End Function
    End Class
End Namespace
