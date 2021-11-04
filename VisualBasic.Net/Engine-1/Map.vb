Imports System.Collections.Generic

Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Audio
Imports Microsoft.Xna.Framework.Content
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Input
Imports Microsoft.Xna.Framework.Media


Namespace GameEngine
    Public Class Map
        Public _decor As List(Of Decor) = New List(Of Decor)
        Public _walls As List(Of Wall) = New List(Of Wall)
        Private _wallImage As Texture2D

        Public _width As Integer = 15
        Public _height As Integer = 9
        Public _tilesize As Integer = 128

        Public Sub LoadMap(vContent As ContentManager)
            For Each locDecor In _decor
                locDecor.Load(vContent, locDecor._imagePath)
            Next
        End Sub

        Public Sub Load(vContent As ContentManager)
            _wallImage = TextureLoader.Load("pixel", vContent)
        End Sub

        Public Function CheckCollision(vInput As Rectangle) As Rectangle
            For Each _wallobj In _walls
                If (_wallobj IsNot Nothing) AndAlso (_wallobj._wall.Intersects(vInput) = True) Then
                    Return _wallobj._wall
                End If
            Next
            Return Rectangle.Empty
        End Function

        Public Sub Update(vObjects As List(Of GameObject))
            For Each locDecor In _decor
                locDecor.Update(vObjects, Me)
            Next
        End Sub

        Public Sub DrawWalls(vSpriteBatch As SpriteBatch)
            For Each _wallobj In _walls
                If (_wallobj IsNot Nothing) AndAlso (_wallobj._active = True) Then
                    vSpriteBatch.Draw(_wallImage, New Vector2(_wallobj._wall.X, _wallobj._wall.Y), _wallobj._wall, Color.Black, 0.0F, Vector2.Zero, 1.0F, SpriteEffects.None, 0.7F)
                End If
            Next
        End Sub

        Public Function GetTileIndex(inputPosition As Vector2) As Point
            If inputPosition = New Vector2(-1, -1) Then
                Return New Point(-1, -1)
            End If

            Return New Point(inputPosition.X \ _tilesize, inputPosition.Y \ _tilesize)
        End Function

    End Class

    Public Class Wall
        Public _wall As Rectangle
        Public _active As Boolean

        Public Sub New()
        End Sub

        Public Sub New(vInputRectangle As Rectangle)
            _wall = vInputRectangle
            _active = True
        End Sub
    End Class

    Public Class Decor
        Inherits GameObject

        Public _imagePath As String
        Public _sourceRect As Rectangle

        Public ReadOnly Property Name As String
            Get
                Return _imagePath
            End Get
            'Set(value As String)
            '    _imagePath = value
            'End Set
        End Property

        Public Sub New()
            _collidable = False
        End Sub

        Public Sub New(vInputPosition As Vector2, vInputImagePath As String, vLayerDepth As Single)
            _position = vInputPosition
            _imagePath = vInputImagePath
            _layerDepth = vLayerDepth
            _active = True
            _collidable = False
        End Sub

        Public Overridable Overloads Sub Load(vContent As ContentManager, vAsset As String)
            _image = TextureLoader.Load(vAsset, vContent)
            _image.Name = vAsset

            _boundingBoxWidth = _image.Width
            _boundingBoxHeight = _image.Height

            If _sourceRect = Rectangle.Empty Then
                _sourceRect = New Rectangle(0, 0, _image.Width, _image.Height)
            End If
        End Sub

        Public Sub SetImage(vInput As Texture2D, vNewPath As String)
            _image = vInput
            _imagePath = vNewPath
            _boundingBoxWidth = _image.Width
            _sourceRect.Width = _image.Width
            _boundingBoxHeight = _image.Height
            _sourceRect.Height = _image.Height
        End Sub

        Public Overrides Sub Draw(vSpriteBatch As SpriteBatch)
            If (_image IsNot Nothing) AndAlso
                    (_active = True) Then
                vSpriteBatch.Draw(_image, _position, _sourceRect, _drawColor, _rotation, Vector2.Zero, _scale, SpriteEffects.None, _layerDepth)
            End If
        End Sub

    End Class

End Namespace