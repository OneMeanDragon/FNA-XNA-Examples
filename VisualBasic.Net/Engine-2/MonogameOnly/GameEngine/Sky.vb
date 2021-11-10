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
        Private cloud_index As Integer
        Private rotate As Single
        Private translate As Matrix
        Public textures As Texture2D

        'Construct
        Public Sub New(thisGpu As GraphicsDevice, thisContent As ContentManager)
            gpu = thisGpu : contents = thisContent
            translate = Matrix.CreateTranslation(0, 4, 0)
        End Sub

        'Load
        Public Sub Load(SkyModelName As String)
            models = contents.Load(Of Model)(SkyModelName)
            'cloud_index = models.getindex("cloud_layer")
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
                        If textures IsNot Nothing Then
                            effect.Texture = textures
                        End If
                        gpu.BlendState = BlendState.Additive
                        effect.World = Matrix.CreateFromYawPitchRoll(rotate, 0, 0) * translate
                        rotate += 0.002F
                    Else
                        gpu.BlendState = BlendState.Opaque
                        effect.World = translate
                    End If
                    With effect
                        .LightingEnabled = False
                        .View = view
                        .Projection = cam.proj
                        .TextureEnabled = True
                    End With
                Next
                mesh.Draw()
            Next
        End Sub

    End Class
End Namespace