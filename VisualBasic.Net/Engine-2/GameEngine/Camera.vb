Imports System.Collections.Generic
Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Content
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Input

Namespace GameEngine
    Public Class Camera

        Public Const CAM_HEIGHT_OFFSET As Single = 80.0F

        Public Const FAR_PLANE As Single = 2000.0F
        Public pos, target As Vector3
        Public view, proj, view_proj As Matrix
        Public up As Vector3
        Private current_angle As Single
        Private angle_velocity As Single
        Private redius As Single = 100.0F
        Private unit_direction As Vector3

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
        End Sub

        ' MOVE CAMERA
        Public Sub MoveCamera(move As Vector3)
            pos += move
            view = Matrix.CreateLookAt(pos, target, up)
            view_proj = view * proj
        End Sub

        ' UPDATE TARGET
        Public Sub UpdateTarget(new_target As Vector3)
            target = new_target : target.Y -= 10
            view = Matrix.CreateLookAt(pos, target, up)
            view_proj = view * proj
        End Sub

        ' UPDATE PLAYER CAM
        Public Sub Update_Player_Cam()
#Region "TEMPORARY_ADDITIONAL_CAMERA_CONTROL"
            If inp.KeyDown(Keys.A) Then
                pos.Y += 5
            End If
            If inp.KeyDown(Keys.Z) Then
                pos.Y -= 5
            End If
            If inp.KeyDown(Keys.Q) Then
                pos.X += 5
            End If
            If inp.KeyDown(Keys.W) Then
                pos.X -= 5
            End If
            If inp.KeyDown(Keys.X) Then
                pos.Z += 5
            End If
            If inp.KeyDown(Keys.S) Then
                pos.Z -= 5
            End If
#End Region
            UpdateTarget(Vector3.Zero) 'UpdateTarget(hero.pos)
        End Sub

    End Class
End Namespace