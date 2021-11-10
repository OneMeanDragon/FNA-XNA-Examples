Imports System.Collections.Generic

Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Audio
Imports Microsoft.Xna.Framework.Content
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Input
Imports Microsoft.Xna.Framework.Media

Namespace GameEngine
    Public Class Input
        Public Const DEADZONE As Single = 0.12F '"deadzone" for analog on peripheral devices

        Public Const ButtonUp As ButtonState = ButtonState.Released
        Public Const ButtonDown As ButtonState = ButtonState.Pressed

        ' Keyboard
        Public kb, okb As KeyboardState
        Public shift_down, control_down, alt_down, shift_pressed, control_pressed, alt_pressed As Boolean
        Public old_shift_down, old_control_down, old_alt_down, old_shift_pressed, old_control_pressed, old_alt_pressed As Boolean

        ' Mouse
        Public ms, oms As MouseState
        Public leftClick, midClick, rightClick, leftDown, midDown, rightDown As Boolean
        Public mosx, mosy As Integer
        Public mosV As Vector2
        Public mp As Point

        ' GamePad
        Public gp, ogp As GamePadState
        Public a_down, b_down, x_down, y_down, rb_down, lb_down, start_down, back_down, leftstick_down, rightstick_down As Boolean
        Public a_press, b_press, x_press, y_press, rb_press, lb_press, start_press, back_press, leftstick_press, rightstick_press As Boolean

        Private screenScaleX, screenScaleY As Single

        Public Sub New(pp As PresentationParameters, target As RenderTarget2D)
            screenScaleX = 1.0F / (pp.BackBufferWidth / target.Width)
            screenScaleY = 1.0F / (pp.BackBufferHeight / target.Height)
        End Sub

        Public Function KeyPress(k As Keys) As Boolean
            If kb.IsKeyDown(k) AndAlso okb.IsKeyUp(k) Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function KeyDown(k As Keys) As Boolean
            If kb.IsKeyDown(k) Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function ButtonPress(button As Buttons) As Boolean
            If gp.IsButtonDown(button) AndAlso ogp.IsButtonUp(button) Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Sub Update()
            old_alt_down = alt_down
            old_shift_down = shift_down
            old_control_down = control_down
            okb = kb
            oms = ms
            ogp = gp
            kb = Keyboard.GetState
            ms = Mouse.GetState
            gp = GamePad.GetState(PlayerIndex.One)

            ' Keyboard stuff
            shift_pressed = False    ' This entire chunk of shit is un needed
            control_pressed = False
            alt_pressed = False
            alt_down = False
            shift_down = False
            control_down = False
            If kb.IsKeyDown(Keys.LeftShift) OrElse kb.IsKeyDown(Keys.RightShift) Then
                shift_down = True
            End If
            If kb.IsKeyDown(Keys.LeftControl) OrElse kb.IsKeyDown(Keys.RightControl) Then
                control_down = True
            End If
            If kb.IsKeyDown(Keys.LeftAlt) OrElse kb.IsKeyDown(Keys.RightAlt) Then
                alt_down = True
            End If
            If shift_down AndAlso (Not old_shift_down) Then
                shift_pressed = True
            End If
            If control_down AndAlso (Not old_control_down) Then
                control_pressed = True
            End If
            If alt_down AndAlso (Not old_alt_down) Then
                alt_pressed = True
            End If

            ' Mouse Stuff
            mosV = New Vector2(ms.X * screenScaleX, ms.Y * screenScaleY)
            mosx = mosV.X
            mosy = mosV.Y
            mp = New Point(mosx, mosy)
            leftClick = False
            midClick = False
            rightClick = False
            leftDown = False
            rightDown = False
            midDown = False
            If ms.LeftButton = ButtonDown Then
                leftDown = True
            End If
            If ms.MiddleButton = ButtonDown Then
                midDown = True
            End If
            If ms.RightButton = ButtonDown Then
                rightDown = True
            End If
            If leftDown AndAlso (oms.LeftButton = ButtonUp) Then
                leftClick = True
            End If
            If midDown AndAlso (oms.MiddleButton = ButtonUp) Then
                midClick = True
            End If
            If rightDown AndAlso (oms.RightButton = ButtonUp) Then
                rightClick = True
            End If

            ' GamePad stuff
            a_down = False
            b_down = False
            x_down = False
            y_down = False
            rb_down = False
            lb_down = False
            start_down = False
            back_down = False
            leftstick_down = False
            rightstick_down = False

            a_press = False
            b_press = False
            x_press = False
            y_press = False
            rb_press = False
            lb_press = False
            start_press = False
            back_press = False
            leftstick_press = False
            rightstick_press = False

            If gp.Buttons.A = ButtonState.Pressed Then
                a_down = True
            End If
            If gp.Buttons.A = ButtonState.Released Then
                a_press = True
            End If
            If gp.Buttons.B = ButtonState.Pressed Then
                b_down = True
            End If
            If gp.Buttons.B = ButtonState.Released Then
                b_press = True
            End If
            If gp.Buttons.X = ButtonState.Pressed Then
                x_down = True
            End If
            If gp.Buttons.X = ButtonState.Released Then
                x_press = True
            End If
            If gp.Buttons.Y = ButtonState.Pressed Then
                y_down = True
            End If
            If gp.Buttons.Y = ButtonState.Released Then
                y_press = True
            End If
            If gp.Buttons.RightShoulder = ButtonState.Pressed Then
                rb_down = True
            End If
            If gp.Buttons.RightShoulder = ButtonState.Released Then
                rb_press = True
            End If
            If gp.Buttons.LeftShoulder = ButtonState.Pressed Then
                lb_down = True
            End If
            If gp.Buttons.LeftShoulder = ButtonState.Released Then
                lb_press = True
            End If
            If gp.Buttons.Back = ButtonState.Pressed Then
                back_down = True
            End If
            If gp.Buttons.Back = ButtonState.Released Then
                back_press = True
            End If
            If gp.Buttons.Start = ButtonState.Pressed Then
                start_down = True
            End If
            If gp.Buttons.Start = ButtonState.Released Then
                start_press = True
            End If
            If gp.Buttons.LeftStick = ButtonState.Pressed Then
                leftstick_down = True
            End If
            If gp.Buttons.LeftStick = ButtonState.Released Then
                leftstick_press = True
            End If
            If gp.Buttons.RightStick = ButtonState.Pressed Then
                rightstick_down = True
            End If
            If gp.Buttons.RightStick = ButtonState.Released Then
                rightstick_press = True
            End If

        End Sub

    End Class
End Namespace