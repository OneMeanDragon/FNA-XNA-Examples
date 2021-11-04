Imports System.Collections.Generic

Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Audio
Imports Microsoft.Xna.Framework.Content
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Input
Imports Microsoft.Xna.Framework.Media


Namespace GameEngine
    Public Class Enemy
        Inherits Character

        Private _respawnTimer As Integer
        Const _maxRespawnTimer As Integer = 60

        Private _random As Random = New Random()

        Private _explosion As SoundEffect

        Public Sub New()
        End Sub
        Public Sub New(vInputPosition As Vector2)
            _position = vInputPosition
        End Sub

        Public Overrides Sub Initialize()
            _active = True
            _collidable = False
            _position.X = _random.Next(0, 1100)

            MyBase.Initialize()
        End Sub

        Public Overrides Sub Load(vContent As ContentManager)
            _image = TextureLoader.Load("enemy", vContent)
            _explosion = vContent.Load(Of SoundEffect)("Audio\\explosion")

            MyBase.Load(vContent)
        End Sub

        Public Overrides Sub Update(vList As List(Of GameObject), vMap As Map)
            If (_respawnTimer > 0) Then
                _respawnTimer -= 1
                If _respawnTimer <= 0 Then
                    Initialize()
                End If
            End If

            MyBase.Update(vList, vMap)
        End Sub

        Public Overrides Sub BulletResponse()
            _active = False
            _respawnTimer = _maxRespawnTimer

            Player._score += 1  'This can be done from the bullet class aswell, in the collision detect
            '                    Bullet.Owner._score += 1 if it destroyed said enemy
            _explosion.Play()

            MyBase.BulletResponse()
        End Sub

    End Class
End Namespace