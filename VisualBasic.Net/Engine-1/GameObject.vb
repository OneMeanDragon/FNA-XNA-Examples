Imports System.Collections.Generic

Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Audio
Imports Microsoft.Xna.Framework.Content
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Input
Imports Microsoft.Xna.Framework.Media

Namespace GameEngine
    Public Class GameObject
        Protected _image As Texture2D
        Public _position As Vector2
        Public _drawColor As Color = Color.White
        Public _scale As Single = 1.0F
        Public _rotation As Single = 0.0F
        Public _layerDepth As Single = 0.5F
        Public _active As Boolean = True
        Protected _center As Vector2

        Public _collidable As Boolean = True
        Protected _boundingBoxWidth As Integer
        Protected _boundingBoxHeight As Integer
        Protected _boundingBoxOffset As Vector2
        Private _boundingBoxImage As Texture2D
        Const _drawBoundingBoxes As Boolean = True

        Protected _direction As Vector2 = New Vector2(1, 0)

        Public startPosition As Vector2 = New Vector2(-1, -1)

        Public ReadOnly Property BoundingBox As Rectangle
            Get
                Return New Rectangle(_position.X + _boundingBoxOffset.X, _position.Y + _boundingBoxOffset.Y, _boundingBoxWidth, _boundingBoxHeight)
            End Get
        End Property

        Public Sub New()
        End Sub

        Public Overridable Sub Initialize()
            If startPosition = New Vector2(-1, -1) Then
                startPosition = _position
            End If
        End Sub

        Public Overridable Sub SetToDefaultPosition()
            _position = startPosition
        End Sub

        Public Overridable Sub Load(vContent As ContentManager)
            _boundingBoxImage = TextureLoader.Load("pixel", vContent)

            CalculateCenter()

            If _image IsNot Nothing Then
                _boundingBoxWidth = _image.Width
                _boundingBoxHeight = _image.Height
            End If
        End Sub

        Public Overridable Sub Update(vList As List(Of GameObject), vMap As Map)
        End Sub

        Public Overridable Function CheckCollision(vInput As Rectangle) As Boolean
            Return BoundingBox.Intersects(vInput)
        End Function

        Public Overridable Sub Draw(vSpriteBatch As SpriteBatch)
            If (_boundingBoxImage IsNot Nothing) AndAlso
                (_drawBoundingBoxes = True) AndAlso
                (_active = True) Then

                vSpriteBatch.Draw(_boundingBoxImage, New Vector2(BoundingBox.X, BoundingBox.Y), BoundingBox, New Color(120, 120, 120, 120), 0.0F, Vector2.Zero, 1.0F, SpriteEffects.None, 0.1F)
            End If
            If (_image IsNot Nothing) AndAlso (_active = True) Then
                vSpriteBatch.Draw(_image, _position, Nothing, _drawColor, _rotation, Vector2.Zero, _scale, SpriteEffects.None, _layerDepth)
            End If
        End Sub

        Public Overridable Sub BulletResponse()
        End Sub

        Private Sub CalculateCenter()
            If _image Is Nothing Then
                Return
            End If
            _center.X = _image.Width / 2
            _center.Y = _image.Height / 2
        End Sub
    End Class
End Namespace