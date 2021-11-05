Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Xna.Framework.Input
Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics

Namespace GameEngine
    Public Class Input
        ' Keyboard
        Private Shared keyboardState As KeyboardState = Keyboard.GetState()
        Private Shared lastKeyboardState As KeyboardState
        ' Mouse
        Private Shared mouseState As MouseState
        Private Shared lastMouseState As MouseState
        ' Gamepad
        Const MaxGamepads As Integer = 4
        Private Shared gamepadState As GamePadState = GamePad.GetState(PlayerIndex.One)
        Private Shared lastGamepadState As GamePadState

        Shared Sub Update()
            ' Keyboard
            lastKeyboardState = keyboardState
            keyboardState = Keyboard.GetState()
            ' Mouse
            lastMouseState = mouseState
            mouseState = Mouse.GetState()
            ' Gamepad
            lastGamepadState = gamepadState
            gamepadState = GamePad.GetState(PlayerIndex.One) 'depending on the amount were using..
        End Sub

#Region "GamePad"
        Shared Function IsButtonDown(button As Buttons) As Boolean
            Return gamepadState.IsButtonDown(button)
        End Function
        Shared Function IsButtonUp(button As Buttons) As Boolean
            Return gamepadState.IsButtonUp(button)
        End Function
        Shared Function ButtonPressed(button As Buttons) As Boolean
#If DEBUG Then
            If gamepadState.IsButtonDown(button) = True AndAlso lastGamepadState.IsButtonDown(button) = False Then
                Return True
            Else
                Return False
            End If
#Else
            Return (gamepadState.IsButtonDown(button) = True AndAlso lastGamepadState.IsButtonDown(button) = False)
#End If
        End Function
        Shared Function LeftStickPressed(button As Buttons) As Boolean
#If DEBUG Then
            If gamepadState.IsButtonDown(button) = True AndAlso lastGamepadState.IsButtonDown(button) = True Then
                Return True
            Else
                Return False
            End If
#Else
            Return (gamepadState.IsButtonDown(button) = True AndAlso lastGamepadState.IsButtonDown(button) = True)
#End If
        End Function
#End Region

        Shared Function IsKeyDown(ByVal input As Keys) As Boolean
            Return keyboardState.IsKeyDown(input)
        End Function

        Function IsKeyUp(ByVal input As Keys) As Boolean
            Return keyboardState.IsKeyUp(input)
        End Function

        Shared Function KeyPressed(ByVal input As Keys) As Boolean
#If DEBUG Then
            If keyboardState.IsKeyDown(input) = True AndAlso lastKeyboardState.IsKeyDown(input) = False Then
                Return True
            Else
                Return False
            End If
#Else
            Return (keyboardState.IsKeyDown(input) = True AndAlso lastKeyboardState.IsKeyDown(input) = False)
#End If
        End Function

        Shared Function MouseLeftDown() As Boolean
#If DEBUG Then
            If mouseState.LeftButton = ButtonState.Pressed Then
                Return True
            Else
                Return False
            End If
#Else
            Return (mouseState.LeftButton = ButtonState.Pressed)
#End If
        End Function

        Shared Function MouseRightDown() As Boolean
#If DEBUG Then
            If mouseState.RightButton = ButtonState.Pressed Then
                Return True
            Else
                Return False
            End If
#Else
            Return (mouseState.RightButton = ButtonState.Pressed)
#End If
        End Function

        Shared Function MouseLeftClicked() As Boolean
#If DEBUG Then
            If mouseState.LeftButton = ButtonState.Pressed AndAlso lastMouseState.LeftButton = ButtonState.Released Then
                Return True
            Else
                Return False
            End If
#Else
            Return (mouseState.LeftButton = ButtonState.Pressed AndAlso lastMouseState.LeftButton = ButtonState.Released)
#End If
        End Function

        Function MouseRightClicked() As Boolean
#If DEBUG Then
            If mouseState.RightButton = ButtonState.Pressed AndAlso lastMouseState.RightButton = ButtonState.Released Then
                Return True
            Else
                Return False
            End If
#Else
            Return (mouseState.RightButton = ButtonState.Pressed AndAlso lastMouseState.RightButton = ButtonState.Released)
#End If
        End Function

        Shared Function MousePositionCamera() As Vector2
            Dim mousePosition As Vector2 = Vector2.Zero
            mousePosition.X = mouseState.X
            mousePosition.Y = mouseState.Y
            Return ScreenToWorld(mousePosition)
        End Function

        Shared Function LastMousePositionCamera() As Vector2
            Dim mousePosition As Vector2 = Vector2.Zero
            mousePosition.X = lastMouseState.X
            mousePosition.Y = lastMouseState.Y
            Return ScreenToWorld(mousePosition)
        End Function

        Private Shared Function ScreenToWorld(ByVal input As Vector2) As Vector2
            input.X -= Resolution.VirtualViewportX
            input.Y -= Resolution.VirtualViewportY
            Return Vector2.Transform(input, Matrix.Invert(Camera.GetTransformMatrix()))
        End Function
    End Class
End Namespace
