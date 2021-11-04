Imports System.Collections.Generic

Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Audio
Imports Microsoft.Xna.Framework.Content
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Input
Imports Microsoft.Xna.Framework.Media


Namespace GameEngine
    Public Class FireCharacter
        Inherits Character

        Private _bullets As List(Of Bullet) = New List(Of Bullet)

        Const _numOfBullets As Integer = 20

        Public Sub New()
        End Sub

        Public Overrides Sub Initialize()
            If _bullets.Count = 0 Then
                For i As Integer = 1 To _numOfBullets
                    _bullets.Add(New Bullet())
                Next
            End If

            MyBase.Initialize()
        End Sub

        Public Overrides Sub Load(vContent As ContentManager)
            For Each _locBullet In _bullets
                _locBullet.Load(vContent)
            Next

            MyBase.Load(vContent)
        End Sub

        Public Overrides Sub Update(vList As List(Of GameObject), vMap As Map)
            For Each _locBullet In _bullets
                _locBullet.Update(vList, vMap)
            Next

            MyBase.Update(vList, vMap)
        End Sub

        Public Sub Fire()
            For Each _locBullet In _bullets
                If (_locBullet._active = False) Then
                    _locBullet.Fire(Me, _position, _direction)
                    Exit For
                End If
            Next
        End Sub

        Public Overrides Sub Draw(vSpriteBatch As SpriteBatch)
            For Each _locBullet In _bullets
                _locBullet.Draw(vSpriteBatch)
            Next

            MyBase.Draw(vSpriteBatch)
        End Sub

    End Class
End Namespace