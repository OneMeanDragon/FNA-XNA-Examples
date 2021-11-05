Imports System.Collections.Generic

Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Audio
Imports Microsoft.Xna.Framework.Content
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Input
Imports Microsoft.Xna.Framework.Media

Namespace GameEngine
    Public Class Sky
        Private gpu As GraphicsDevice
        Private contents As ContentManager
        Private models As Model
        Public textures As Texture2D
        Private rotate As Single

        'Construct
        Public Sub New(thisGpu As GraphicsDevice, thisContent As ContentManager)
            gpu = thisGpu : contents = thisContent
        End Sub

        'Load
        Public Sub Load(SkyModelName As String)
            models = contents.Load(Of Model)(SkyModelName)
            rotate = 0.0F
        End Sub

        'Draw
        Public Sub Draw(cam As Camera)
            With gpu
                .BlendState = BlendState.Opaque
                .RasterizerState = RasterizerState.CullNone
                .DepthStencilState = DepthStencilState.None
                .SamplerStates(0) = SamplerState.LinearWrap
            End With
            Dim view As Matrix = cam.view
            view.Translation = Vector3.Zero

            For Each mesh As ModelMesh In models.Meshes
                For Each effect As BasicEffect In mesh.Effects
                    If mesh.Name = "cloud_layer" Then
                        gpu.BlendState = BlendState.Additive
                        effect.World = Matrix.CreateFromYawPitchRoll(rotate, 0, 0)
                        rotate += 0.002F
                    Else
                        gpu.BlendState = BlendState.Opaque
                    End If
                    With effect
                        .View = view
                        .Projection = cam.proj
                        .TextureEnabled = True
                    End With
                    If textures IsNot Nothing Then
                        effect.Texture = textures
                    End If
                Next
                mesh.Draw()
            Next
        End Sub

    End Class
End Namespace