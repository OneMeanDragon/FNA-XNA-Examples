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
    Public Class PowerUp
        Inherits AnimatedObject

        Public Sub New()
        End Sub

        Public Sub New(inputPosition As Vector2)
            _position = startPosition
        End Sub

        Public Overrides Sub Initialize()
            MyBase.Initialize()
            _collidable = False
        End Sub

        Public Overrides Sub Load(vContent As ContentManager)
            '//Load our image/sprite sheet
            _image = TextureLoader.Load("orbsheet", vContent)

            '//Load any animation stuff if this object animates
            LoadAnimation("PowerUp.anm", vContent)
            ChangeAnimation(Animations.IdleLeft) '//Set our Default animation.

            '//Load stuff from our parent class:
            MyBase.Load(vContent)

            '//Customize the size of our bounding box for collisions
            _boundingBoxOffset.X = 0
            _boundingBoxOffset.Y = 0
            _boundingBoxWidth = animationSet.width '//Or use image.Width If it's not animated
            _boundingBoxHeight = animationSet.height '//Or use image.Height If it's not animated
        End Sub

        Public Overrides Sub Update(vList As List(Of GameObject), vMap As Map)
            If _active = False Then
                Return
            End If

            CheckPlayerCollision(vList, vMap)

            MyBase.Update(vList, vMap)
        End Sub

        Private Sub CheckPlayerCollision(vList As List(Of GameObject), vMap As Map)
            If AnimationIsNot(Animations.PickUp) AndAlso vList(0).CheckCollision(BoundingBox) = True Then
                Player._score += 1
                ChangeAnimation(Animations.PickUp)
            End If
        End Sub

        Protected Overrides Sub UpdateAnimations()
            If (currentAnimation Is Nothing) Then
                Return '//Animation isn't loaded, so return.
            End If
            MyBase.UpdateAnimations()

            If AnimationComplete() = True AndAlso GetAnimationName(Animations.PickUp) = currentAnimation.name Then
                _active = False
            End If
        End Sub

    End Class
End Namespace