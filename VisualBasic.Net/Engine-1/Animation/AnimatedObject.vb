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
    Public Class AnimatedObject
        Inherits GameObject

        Protected currentAnimationFrame As Integer
        Protected animationTimer As Single
        Protected currentAnimationX As Integer = -1
        Protected currentAnimationY As Integer = -1
        Protected [animationSet] As AnimationSet = New AnimationSet()
        Protected currentAnimation As Animation

        Protected flipRightFrames As Boolean = True
        Protected flipLeftFrames As Boolean = False
        Protected spriteEffect As SpriteEffects = SpriteEffects.None

        Protected Enum [Animations]
            [RunLeft]
            [RunRight]
            [IdleLeft]
            [IdleRight]
            [PickUp]
        End Enum

        Protected Sub LoadAnimation(vPath As String, vContent As ContentManager)
            Dim locAnimationData As AnimationData = AnimationLoader.Load(vPath)
            [animationSet] = locAnimationData.animation

            ' Set initial values 
            _center.X = [animationSet].width / 2
            _center.Y = [animationSet].height / 2

            ' Default the current animation to the first item in the list
            If ([animationSet].animationList.Count > 0) Then
                currentAnimation = [animationSet].animationList(0)

                currentAnimationFrame = 0
                animationTimer = 0.0F
                CalculateFramePosition()
            End If
        End Sub

        Public Overrides Sub Update(vList As List(Of GameObject), vMap As Map)
            MyBase.Update(vList, vMap)
            If currentAnimation IsNot Nothing Then
                UpdateAnimations()
            End If
        End Sub

        Protected Overridable Sub UpdateAnimations()
            If currentAnimation.animationOrder.Count < 1 Then
                Return
            End If

            animationTimer -= 1

            If animationTimer <= 0 Then
                animationTimer = currentAnimation.speed

                If AnimationComplete() = True Then
                    currentAnimationFrame = 0
                Else
                    currentAnimationFrame += 1
                End If

                CalculateFramePosition()
            End If
        End Sub

        Protected Overridable Sub ChangeAnimation(newAnimation As Animations)
            currentAnimation = GetAnimation(newAnimation)

            If currentAnimation Is Nothing Then
                Return
            End If

            ' Start on the first frame of the new animation 
            currentAnimationFrame = 0
            animationTimer = currentAnimation.speed

            CalculateFramePosition()

            ' check to see if this is an animation that we want to flip
            If (flipRightFrames = True AndAlso currentAnimation.name.Contains("Right") OrElse
                    flipLeftFrames = True AndAlso currentAnimation.name.Contains("Left")) Then
                spriteEffect = SpriteEffects.FlipHorizontally
            Else
                spriteEffect = SpriteEffects.None
            End If

        End Sub

        Private Function GetAnimation(vAnimation As Animations) As Animation
            Dim locName As String = GetAnimationName(vAnimation)

            For Each locAnimation In [animationSet].animationList
                If locAnimation.name = locName Then
                    Return locAnimation
                End If
            Next

            Return Nothing
        End Function

        Protected Sub CalculateFramePosition()
            Dim coordinate As Integer = currentAnimation.animationOrder(currentAnimationFrame)

            currentAnimationX = (coordinate Mod [animationSet].gridX) * [animationSet].width
            currentAnimationY = (coordinate \ [animationSet].gridX) * [animationSet].height  ' VB division note the \ instead of the /
        End Sub

        '33:48

        Public Function AnimationComplete() As Boolean
            Return currentAnimationFrame >= (currentAnimation.animationOrder.Count - 1)
        End Function

        Public Overrides Sub Draw(vSpriteBatch As SpriteBatch)
            If _active = False Then
                Return
            End If

            If (currentAnimationX = -1 OrElse currentAnimationY = -1) Then
                MyBase.Draw(vSpriteBatch)
            Else
                vSpriteBatch.Draw(_image, _position, New Rectangle(currentAnimationX, currentAnimationY, [animationSet].width, [animationSet].height), _drawColor, _rotation, Vector2.Zero, _scale, spriteEffect, _layerDepth)
            End If
        End Sub

        Protected Function GetAnimationName(vAnimation As Animations) As String
            'Make an accurately spaced string. Example: "RunLeft" will now be "Run Left":
            Return AddSpacesToSentence(vAnimation.ToString(), False)
        End Function

        Protected Function AnimationIsNot(vInput As Animations) As Boolean
            'Used to check if our currentAnimation isn't set to the one passed in:
            Return (currentAnimation IsNot Nothing) AndAlso (GetAnimationName(vInput) <> currentAnimation.name)
        End Function

        Public Function AddSpacesToSentence(ByVal vText As String, ByVal vPreserveAcronyms As Boolean) As String
            If String.IsNullOrWhiteSpace(vText) Then Return String.Empty
            Dim newText As StringBuilder = New StringBuilder(vText.Length * 2)
            newText.Append(vText(0))

            For i As Integer = 1 To vText.Length - 1

                If Char.IsUpper(vText(i)) Then
                    If (vText(i - 1) <> " "c AndAlso Not Char.IsUpper(vText(i - 1))) OrElse (vPreserveAcronyms AndAlso Char.IsUpper(vText(i - 1)) AndAlso i < vText.Length - 1 AndAlso Not Char.IsUpper(vText(i + 1))) Then newText.Append(" "c)
                End If

                newText.Append(vText(i))
            Next

            Return newText.ToString()
        End Function
    End Class
End Namespace