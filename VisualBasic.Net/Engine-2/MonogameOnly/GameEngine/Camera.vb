Imports System.Collections.Generic
Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Content
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Input

Namespace GameEngine
    Public Class Camera

        Public Const CAM_HEIGHT As Single = 20.0F

        Public Const FAR_PLANE As Single = 2000.0F
        Public pos, target As Vector3
        Public view, proj, view_proj As Matrix
        Public up As Vector3
        Private current_angle As Single
        Private angle_velocity As Single
        Private radius As Single = 100.0F
        Private unit_direction As Vector3
        Private maf As Maf

        Private inp As Input

        ' Constructor
        Public Sub New(gpu As GraphicsDevice, UpDirection As Vector3, input As Input)
            up = UpDirection
            pos = New Vector3(20, -30, -50)
            target = Vector3.Zero
            view = Matrix.CreateLookAt(pos, target, up)
            proj = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, gpu.Viewport.AspectRatio, 0.1F, FAR_PLANE)
            view_proj = view * proj
            inp = input
            unit_direction = view.Forward : unit_direction.Normalize()
            maf = New Maf()
        End Sub

        ' MOVE CAMERA
        Public Sub MoveCamera(move As Vector3)
            pos += move
            view = Matrix.CreateLookAt(pos, target, up)
            view_proj = view * proj
        End Sub

        ' UPDATE TARGET
        Public Sub UpdateTarget(new_target As Vector3)
            target = new_target
            view = Matrix.CreateLookAt(pos, target, up)
            view_proj = view * proj
        End Sub

        ' UPDATE PLAYER CAM
        Public Sub Update_Player_Cam(hero_pos As Vector3)
            If target = Vector3.Zero Then
                target = hero_pos
            End If

#Region "TEMPORARY_ADDITIONAL_CAMERA_CONTROL"
            'If inp.KeyDown(Keys.A) Then
            '    pos.Y += 5
            'End If
            'If inp.KeyDown(Keys.Z) Then
            '    pos.Y -= 5
            'End If
            'If inp.KeyDown(Keys.Q) Then
            '    pos.X += 5
            'End If
            'If inp.KeyDown(Keys.W) Then
            '    pos.X -= 5
            'End If
            'If inp.KeyDown(Keys.X) Then
            '    pos.Z += 5
            'End If
            'If inp.KeyDown(Keys.S) Then
            '    pos.Z -= 5
            'End If
#End Region

            Dim CamPad_LeftRight As Single = inp.gp.ThumbSticks.Right.X
            Dim CamPad_UpDown As Single = inp.gp.ThumbSticks.Right.Y

            Dim forward As Vector3 = hero_pos - pos
            Dim x1 As Single = forward.X
            Dim y1 As Single = forward.Y
            Dim z1 As Single = forward.Z

            ' GET UP DOWN LOOK
            pos.Y -= ((pos.Y - (hero_pos.Y - CAM_HEIGHT)) * 0.05F) 'was 0.02f

            If (CamPad_UpDown > Input.DEADZONE) OrElse (CamPad_UpDown < -Input.DEADZONE) Then
                If CamPad_UpDown < 0.0F Then
                    Dim targ_height As Single = hero_pos.Y - CAM_HEIGHT - 100.0F
                    If pos.Y > targ_height Then
                        pos.Y -= ((pos.Y - targ_height) * 0.05F)
                    End If
                End If
                If CamPad_UpDown > 0.0F Then
                    If pos.Y < hero_pos.Y + 5 Then 'added + 5 so can look up a little bit
                        pos.Y += ((pos.Y + 5 - pos.Y) * 0.05F)
                    End If
                End If
            End If
            ' ROTATE CAMERA (accelerate rotation in a direction)
            If inp.KeyDown(Keys.OemPeriod) Then
                angle_velocity -= Maf.RADIANS_QUARTER
                If angle_velocity < -Maf.RADIANS_2 Then angle_velocity = -Maf.RADIANS_1
            End If
            If inp.KeyDown(Keys.OemComma) Then
                angle_velocity += Maf.RADIANS_QUARTER
                If angle_velocity > Maf.RADIANS_2 Then angle_velocity = Maf.RADIANS_1
            End If
            If (CamPad_LeftRight > Input.DEADZONE) OrElse (CamPad_LeftRight < -Input.DEADZONE) Then
                If CamPad_LeftRight < 0.0F Then
                    angle_velocity -= CamPad_LeftRight * 0.0038F
                    If angle_velocity < -Maf.RADIANS_2 Then
                        angle_velocity = -Maf.RADIANS_1
                    End If
                End If
                If CamPad_LeftRight > 0.0F Then
                    angle_velocity -= CamPad_LeftRight * 0.0038F
                    If angle_velocity > Maf.RADIANS_2 Then
                        angle_velocity = Maf.RADIANS_1
                    End If
                End If
            End If
            radius = Math.Sqrt(x1 * x1 + z1 * z1)
            ' G E T  N E W  R O T A T I O N  A N G L E ( and update camera position )
            If angle_velocity <> 0.0F Then
                current_angle = maf.Calculate2DAngleFromZero(-x1, -z1) ' get angle
                current_angle += angle_velocity                        ' get aditional angle velocity
                current_angle = maf.ClampAngle(current_angle)
                pos.X = hero_pos.X + radius * Math.Cos(current_angle)
                pos.Z = hero_pos.Z + radius * Math.Sin(current_angle)
                angle_velocity *= 0.9F
            End If

            ' C A M E R A  Z O O M  (move camera toward the player if it is to far away)
            Const MIN_DIST As Single = 50.0F
            unit_direction = forward
            unit_direction.Normalize()
            Dim adjust As Single = 0.02F
            If (radius > 400) OrElse (radius < MIN_DIST) Then adjust = 1.0F
            pos.X += unit_direction.X * (radius - MIN_DIST) * adjust
            pos.Z += unit_direction.Z * (radius - MIN_DIST) * adjust
            target.X += (hero_pos.X - target.X) * 0.1F
            target.Y += (hero_pos.Y - 10 - target.Y) * 0.1F
            target.Z += (hero_pos.Z - target.Z) * 0.1F

            UpdateTarget(target)
        End Sub

    End Class
End Namespace