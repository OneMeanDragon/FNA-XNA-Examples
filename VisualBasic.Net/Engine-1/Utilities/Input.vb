Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Xna.Framework.Input
Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics

Namespace GameEngine
    Public Class Input
        Private Shared keyboardState As KeyboardState = Keyboard.GetState()
        Private Shared lastKeyboardState As KeyboardState
        Private Shared mouseState As MouseState
        Private Shared lastMouseState As MouseState

        Shared Sub Update()
            lastKeyboardState = keyboardState
            keyboardState = Keyboard.GetState()
            lastMouseState = mouseState
            mouseState = Mouse.GetState()
        End Sub

        Shared Function IsKeyDown(ByVal input As Keys) As Boolean
            Return keyboardState.IsKeyDown(input)
        End Function

        Function IsKeyUp(ByVal input As Keys) As Boolean
            Return keyboardState.IsKeyUp(input)
        End Function

        Shared Function KeyPressed(ByVal input As Keys) As Boolean
            If keyboardState.IsKeyDown(input) = True AndAlso lastKeyboardState.IsKeyDown(input) = False Then
                Return True
            Else
                Return False
            End If
        End Function

        Shared Function MouseLeftDown() As Boolean
            If mouseState.LeftButton = ButtonState.Pressed Then
                Return True
            Else
                Return False
            End If
        End Function

        Shared Function MouseRightDown() As Boolean
            If mouseState.RightButton = ButtonState.Pressed Then
                Return True
            Else
                Return False
            End If
        End Function

        Shared Function MouseLeftClicked() As Boolean
            If mouseState.LeftButton = ButtonState.Pressed AndAlso lastMouseState.LeftButton = ButtonState.Released Then
                Return True
            Else
                Return False
            End If
        End Function

        Function MouseRightClicked() As Boolean
            If mouseState.RightButton = ButtonState.Pressed AndAlso lastMouseState.RightButton = ButtonState.Released Then
                Return True
            Else
                Return False
            End If
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
