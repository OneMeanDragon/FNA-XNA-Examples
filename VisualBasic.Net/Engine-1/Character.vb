Imports System.Collections.Generic

Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Audio
Imports Microsoft.Xna.Framework.Content
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Input
Imports Microsoft.Xna.Framework.Media

Namespace GameEngine
    Public Class Character
        Inherits AnimatedObject 'GameObject

        Public _velocity As Vector2

        ' custom feel of movement
        Protected _decel As Single = 1.2F ' the lower this value the slower you slow down
        Protected _accel As Single = 0.78F ' the lower this value the slower you speed up
        Protected _maxSpeed As Single = 5.0F ' 35 to fast but you see the deceleration

        Const _gravity As Single = 1.0F
        Const _jumpVelocity As Single = 16.0F
        Const _maxFallVelocity As Single = 32.0F

        Protected _jumping As Boolean
        Public Shared _applyGravity As Boolean = True

        Public Overrides Sub Initialize()
            _velocity = Vector2.Zero
            _jumping = False
            MyBase.Initialize()
        End Sub

        Public Overrides Sub Update(vList As List(Of GameObject), vMap As Map)
            UpdateMovement(vList, vMap)
            MyBase.Update(vList, vMap)
        End Sub

        Private Sub UpdateMovement(vList As List(Of GameObject), vMap As Map)
            If (_velocity.X <> 0) AndAlso (CheckCollisions(vMap, vList, True) = True) Then
                _velocity.X = 0
            End If
            _position.X += _velocity.X

            If (_velocity.Y <> 0) AndAlso (CheckCollisions(vMap, vList, False) = True) Then
                _velocity.Y = 0
            End If
            _position.Y += _velocity.Y

            If _applyGravity = True Then
                ApplyGravity(vMap)
            End If

            _velocity.X = TendToZero(_velocity.X, _decel)
            If _applyGravity = False Then
                _velocity.Y = TendToZero(_velocity.Y, _decel)
            End If
        End Sub

        Private Sub ApplyGravity(vMap As Map)
            If (_jumping = True) OrElse (OnGround(vMap) = Rectangle.Empty) Then
                _velocity.Y += _gravity
            End If

            If _velocity.Y >= _maxFallVelocity Then
                _velocity.Y = _maxFallVelocity
            End If
        End Sub

        Protected Sub MoveRight()
            _velocity.X += _accel + _decel

            If _velocity.X > _maxSpeed Then
                _velocity.X = _maxSpeed
            End If

            _direction.X = 1
        End Sub

        Protected Sub MoveLeft()
            _velocity.X -= _accel + _decel

            If _velocity.X < _maxSpeed Then
                _velocity.X = -_maxSpeed
            End If

            _direction.X = -1
        End Sub

        Protected Sub MoveDown()
            _velocity.Y += _accel + _decel

            If _velocity.Y > _maxSpeed Then
                _velocity.Y = _maxSpeed
            End If

            _direction.Y = 1
        End Sub

        Protected Sub MoveUp()
            _velocity.Y -= _accel + _decel

            If _velocity.Y < _maxSpeed Then
                _velocity.Y = -_maxSpeed
            End If

            _direction.Y = -1
        End Sub

        Protected Function Jump(vMap As Map) As Boolean
            If _jumping = True Then
                Return False
            End If

            If (_velocity.Y = 0) AndAlso
                (OnGround(vMap) <> Rectangle.Empty) Then
                _velocity.Y -= _jumpVelocity
                _jumping = True
                Return True
            End If

            Return False
        End Function

        Protected Overridable Function CheckCollisions(vMap As Map, vObjects As List(Of GameObject), vxAxis As Boolean) As Boolean
            Dim futureBoundingBox As Rectangle = BoundingBox

            Dim MaxX As Integer = _maxSpeed
            Dim MaxY As Integer = _maxSpeed

            If _applyGravity = True Then
                MaxY = _jumpVelocity
            End If

            If (vxAxis = True) AndAlso (_velocity.X <> 0) Then
                If _velocity.X > 0 Then
                    futureBoundingBox.X += MaxX
                Else
                    futureBoundingBox.X -= MaxX
                End If
            ElseIf (_applyGravity = False) AndAlso (vxAxis = False) AndAlso (_velocity.Y <> 0) Then
                If _velocity.Y > 0 Then
                    futureBoundingBox.Y += MaxY
                Else
                    futureBoundingBox.Y -= MaxY
                End If
            ElseIf (_applyGravity = True) AndAlso (vxAxis = False) AndAlso (_velocity.Y <> _gravity) Then
                If _velocity.Y > 0 Then
                    futureBoundingBox.Y += MaxY
                Else
                    futureBoundingBox.Y -= MaxY
                End If
            End If

            Dim locWallCollision As Rectangle = vMap.CheckCollision(futureBoundingBox)

            If locWallCollision <> Rectangle.Empty Then
                If (_applyGravity = True) AndAlso
                    (_velocity.Y >= _gravity) AndAlso
                    (futureBoundingBox.Bottom > (locWallCollision.Top - _maxSpeed)) AndAlso
                    (futureBoundingBox.Bottom <= (locWallCollision.Top + _velocity.Y)) Then
                    'Land response
                    LandResponse(locWallCollision)
                    Return True
                Else
                    Return True
                End If
            End If

            ' Check for object collisions
            For Each locObject In vObjects
                If (Object.Equals(locObject, Me) = False) AndAlso
                    (locObject._active = True) AndAlso
                    (locObject._collidable = True) AndAlso
                    (locObject.CheckCollision(futureBoundingBox) = True) Then
                    Return True
                End If
            Next

            Return False
        End Function

        Public Sub LandResponse(vWallCollision As Rectangle)
            _position.Y = vWallCollision.Top - (_boundingBoxHeight + _boundingBoxOffset.Y)
            _velocity.Y = 0
            _jumping = False
        End Sub

        Protected Function TendToZero(val As Single, amount As Single) As Single
            If val <> 0.0F Then
                If (val > 0.0F) AndAlso ((val - amount) < 0.0F) Then
                    Return 0.0F
                End If
                If (val < 0.0F) AndAlso ((val + amount) > 0.0F) Then
                    Return 0.0F
                End If
                If val > 0.0F Then 'because vb dosent allow (val -= amount) (val += amount) in a condition check
                    Return val - amount
                    'End If
                    'If val < 0.0F Then
                Else
                    Return val + amount
                End If
            Else
                Return val
            End If
        End Function

        Protected Function OnGround(vMap As Map) As Rectangle
            Dim locFutureBoundingBox = New Rectangle((_position.X + _boundingBoxOffset.X), (_position.Y + _boundingBoxOffset.Y + (_velocity.Y + _gravity)), _boundingBoxWidth, _boundingBoxHeight)
            Return vMap.CheckCollision(locFutureBoundingBox)
        End Function

    End Class
End Namespace