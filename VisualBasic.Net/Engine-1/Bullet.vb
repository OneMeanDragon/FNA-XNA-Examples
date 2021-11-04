Imports System.Collections.Generic

Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Audio
Imports Microsoft.Xna.Framework.Content
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Input
Imports Microsoft.Xna.Framework.Media

Namespace GameEngine
    Public Class Bullet
        Inherits GameObject

        Const _speed As Single = 12.0F
        Private _owner As Character

        Private _destroyTimer As Integer
        Const _maxTimer As Integer = 180

        Public Sub New()
            _active = False
        End Sub

        Public Overrides Sub Load(vContent As ContentManager)
            _image = TextureLoader.Load("bullet", vContent)
            MyBase.Load(vContent)
        End Sub

        Public Overrides Sub Update(vList As List(Of GameObject), vMap As Map)
            If _active = False Then
                Return
            End If

            'Update movement
            _position += _direction * _speed

            'Check if bullet hit an entity or a wall, respond acordingly
            CheckCollisions(vList, vMap)

            'Death timer
            _destroyTimer -= 1
            If (_destroyTimer <= 0) AndAlso (_active = True) Then
                Destroy()
            End If

            MyBase.Update(vList, vMap)
        End Sub

        Private Sub CheckCollisions(vList As List(Of GameObject), vMap As Map)
            ' Bullet hit Entity
            For Each locObject In vList
                If (locObject._active = True) AndAlso
                    (Object.Equals(locObject, Me._owner) = False) AndAlso
                    (locObject.CheckCollision(BoundingBox) = True) Then

                    ' Bullet hit, remove it from the screen
                    Destroy()

                    ' Damage
                    locObject.BulletResponse()

                    Return
                End If
            Next

            ' Bullet hit a wall
            If (vMap.CheckCollision(BoundingBox) <> Rectangle.Empty) Then
                Destroy()
            End If
        End Sub

        Public Sub Destroy()
            _active = False
        End Sub

        Public Sub Fire(vInputOwner As Character, vInputPosition As Vector2, vInputDirection As Vector2)
            _owner = vInputOwner
            _position = vInputPosition
            _direction = vInputDirection
            _active = True
            _destroyTimer = _maxTimer
        End Sub

    End Class
End Namespace