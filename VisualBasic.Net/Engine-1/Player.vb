Imports System.Collections.Generic

Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Audio
Imports Microsoft.Xna.Framework.Content
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Input
Imports Microsoft.Xna.Framework.Media

Namespace GameEngine
    Public Class Player
        Inherits FireCharacter 'Character 'GameObject

        Public Shared _score As Integer

        Private song As SoundEffect
        Private songInstance As SoundEffectInstance

        Public Sub New()
        End Sub

        Public Sub New(vInputPosition As Vector2)
            _position = vInputPosition
        End Sub

        Public Overrides Sub Initialize()
            _score = 0
            MyBase.Initialize()
        End Sub

        Public Overrides Sub Load(vContent As ContentManager)
            _image = TextureLoader.Load("spritesheet", vContent)

            ' Load animation stuff
            LoadAnimation("ShyBoy.anm", vContent)
            ChangeAnimation(Animations.IdleLeft)

            MyBase.Load(vContent)

            _boundingBoxOffset.X = 0
            _boundingBoxOffset.Y = 0
            _boundingBoxWidth = [animationSet].width
            _boundingBoxHeight = [animationSet].height

            ' Load song
            song = vContent.Load(Of SoundEffect)("Audio\\song")
            If songInstance Is Nothing Then
                songInstance = song.CreateInstance()
            End If
        End Sub

        Public Overrides Sub Update(vList As List(Of GameObject), vMap As Map)
            CheckInput(vList, vMap)
            UpdateMusic()
            MyBase.Update(vList, vMap)
        End Sub

        Private Sub UpdateMusic()
            If songInstance.State <> SoundState.Playing Then
                songInstance.IsLooped = True
                songInstance.Play()
            End If
        End Sub

        Private Sub CheckInput(vList As List(Of GameObject), vMap As Map)
            If Character._applyGravity = False Then
                If Input.IsKeyDown(Keys.D) = True Then
                    MoveRight()
                ElseIf Input.IsKeyDown(Keys.A) = True Then
                    MoveLeft()
                End If

                If Input.IsKeyDown(Keys.S) = True Then
                    MoveDown()
                ElseIf Input.IsKeyDown(Keys.W) = True Then
                    MoveUp()
                End If
            Else
                If Input.IsKeyDown(Keys.D) = True Then
                    MoveRight()
                ElseIf Input.IsKeyDown(Keys.A) = True Then
                    MoveLeft()
                End If
                If Input.KeyPressed(Keys.W) = True Then
                    Jump(vMap)
                End If
            End If

            If Input.KeyPressed(Keys.Space) = True Then
                Fire()
            End If
        End Sub

        Protected Overrides Sub UpdateAnimations()
            If currentAnimation Is Nothing Then
                Return
            End If

            MyBase.UpdateAnimations()

            If _velocity <> Vector2.Zero OrElse _jumping = True Then
                If _direction.X < 0 AndAlso AnimationIsNot(Animations.RunLeft) Then
                    ChangeAnimation(Animations.RunLeft)
                ElseIf _direction.X > 0 AndAlso AnimationIsNot(Animations.RunRight) Then
                    ChangeAnimation(Animations.RunRight)
                End If
            ElseIf _velocity = Vector2.Zero AndAlso _jumping = False Then
                If _direction.X < 0 AndAlso AnimationIsNot(Animations.IdleLeft) Then
                    ChangeAnimation(Animations.IdleLeft)
                ElseIf _direction.X > 0 AndAlso AnimationIsNot(Animations.IdleRight) Then
                    ChangeAnimation(Animations.IdleRight)
                End If
            End If
        End Sub

    End Class
End Namespace