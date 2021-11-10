Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Content
Imports Microsoft.Xna.Framework.Graphics
Imports System

Namespace GameEngine
    Public Class DirectionLight
        Public direction As Vector3
        Public diffuseColor As Vector3
        Public specularColor As Vector3
        Public enabled As Boolean
    End Class

    Public Class MySkinFX
        Public Const MAX_BONES As Integer = 180
        Public Const WEIGHTS_PER_VERTEX As Integer = 4

        ' R E F E R E N C E D :
        Public cam As Camera

        Public fx As Effect
        Public diffuseTex, default_tex As Texture2D
        Public diffuseCol As Vector4 = Vector4.One
        Public emissiveCol As Vector3 = Vector3.Zero
        Public specularCol As Vector3 = Color.LightYellow.ToVector3()
        Public specularPow As Single = 32.0F
        Public ambientCol As Vector3 = Vector3.Zero
        Public preferPerPixelLighting As Boolean = True
        Public oneLight As Boolean = False
        Public fogEnabled As Boolean = False
        Public lights() As DirectionLight
        Public alpha As Single = 1.0F
        Public fogStart As Single = 0F
        Public fogEnd As Single = 1.0F
        Public world As Matrix = Matrix.Identity
        Public worldView As Matrix = Matrix.Identity


        Public Sub New(contents As ContentManager, cams As Camera, fx_filename As String, Optional enableAllLights As Boolean = True, Optional enableFog As Boolean = False)
            Array.Resize(Of DirectionLight)(lights, 3)
            For i As Integer = 0 To lights.Length - 1
                lights(i) = New DirectionLight()
            Next
            cam = cams
            fx = contents.Load(Of Effect)(fx_filename)
            default_tex = contents.Load(Of Texture2D)("default_texture")
            Dim identityBones(MAX_BONES) As Matrix
            For i As Integer = 0 To MAX_BONES - 1
                identityBones(i) = Matrix.Identity
            Next
            SetBoneTransforms(identityBones)
            fx.Parameters("DiffuseTexture").SetValue(default_tex)
            fx.Parameters("DiffuseColor").SetValue(diffuseCol)
            fx.Parameters("EmissiveColor").SetValue(emissiveCol)
            fx.Parameters("SpecularColor").SetValue(specularCol)
            fx.Parameters("SpecularPower").SetValue(specularPow)

            ToggleDefaultLighting(enableAllLights)
            If enableFog Then
                ToggleFog()
            End If
        End Sub

        ' set bones
        Public Sub SetBoneTransforms(boneTransforms() As Matrix)
            If boneTransforms Is Nothing OrElse boneTransforms.Length = 0 Then
                Throw New ArgumentNullException("boneTransforms")
            End If
            If boneTransforms.Length > MAX_BONES Then
                Throw New ArgumentException("boneTransforms, bone count exceded [" & MAX_BONES.ToString() & "]")
            End If
            fx.Parameters("Bones").SetValue(boneTransforms)
        End Sub

        ' enable lighting
        Public Sub ToggleDefaultLighting(ByVal all_lights As Boolean)
            If all_lights Then
                oneLight = False
            Else
                oneLight = True
            End If

            Dim u As Single = cam.up.Y
            ambientCol = New Vector3(0.05333332F, 0.09882354F, 0.1819608F)
            lights(0).direction = New Vector3(-0.5265408F, -0.5735765F * u, -0.6275069F)
            lights(0).diffuseColor = New Vector3(1, 0.9607844F, 0.8078432F)
            lights(0).specularColor = New Vector3(1, 0.9607844F, 0.8078432F)
            lights(0).enabled = True
            fx.Parameters("DirLight0Direction").SetValue(lights(0).direction)
            fx.Parameters("DirLight0DiffuseColor").SetValue(lights(0).diffuseColor)
            fx.Parameters("DirLight0SpecularColor").SetValue(lights(0).specularColor)

            If all_lights Then
                lights(1).direction = New Vector3(0.7198464F, 0.3420201F * u, 0.6040227F)
                lights(1).diffuseColor = New Vector3(0.9647059F, 0.7607844F, 0.4078432F)
                lights(1).specularColor = Vector3.Zero
                lights(1).enabled = True
                lights(2).direction = New Vector3(0.4545195F, -0.7660444F * u, 0.4545195F)
                lights(2).diffuseColor = New Vector3(0.3231373F, 0.3607844F, 0.3937255F)
                lights(2).specularColor = New Vector3(0.3231373F, 0.3607844F, 0.3937255F)
                lights(2).enabled = True
                fx.Parameters("DirLight1Direction").SetValue(lights(1).direction)
                fx.Parameters("DirLight1DiffuseColor").SetValue(lights(1).diffuseColor)
                fx.Parameters("DirLight1SpecularColor").SetValue(lights(1).specularColor)
                fx.Parameters("DirLight2Direction").SetValue(lights(2).direction)
                fx.Parameters("DirLight2DiffuseColor").SetValue(lights(2).diffuseColor)
                fx.Parameters("DirLight2SpecularColor").SetValue(lights(2).specularColor)
            End If
        End Sub

        ' set directional light
        Public Sub SetDirectionalLight(ByVal index As Integer, ByVal direction As Vector3, ByVal diffuse_color As Color, ByVal specular_color As Color)
            If index >= 3 Then Return
            lights(index).direction = direction
            lights(index).diffuseColor = diffuse_color.ToVector3()
            lights(index).specularColor = specular_color.ToVector3()
            lights(index).enabled = True

            Select Case index
                Case 0
                    fx.Parameters("DirLight0Direction").SetValue(lights(0).direction)
                    fx.Parameters("DirLight0DiffuseColor").SetValue(lights(0).diffuseColor)
                    fx.Parameters("DirLight0SpecularColor").SetValue(lights(0).specularColor)
                Case 1
                    fx.Parameters("DirLight1Direction").SetValue(lights(1).direction)
                    fx.Parameters("DirLight1DiffuseColor").SetValue(lights(1).diffuseColor)
                    fx.Parameters("DirLight1SpecularColor").SetValue(lights(1).specularColor)
                Case 2
                    fx.Parameters("DirLight2Direction").SetValue(lights(2).direction)
                    fx.Parameters("DirLight2DiffuseColor").SetValue(lights(2).diffuseColor)
                    fx.Parameters("DirLight2SpecularColor").SetValue(lights(2).specularColor)
            End Select
        End Sub

        Public Sub SetFogStart(ByVal fog_start As Single)
            fogEnabled = False
            fogStart = fog_start
            ToggleFog()
        End Sub
        Public Sub SetFogEnd(ByVal fog_end As Single)
            fogEnabled = False
            fogEnd = fog_end
            ToggleFog()
        End Sub
        Public Sub SetFogColor(ByVal fog_color As Color)
            fx.Parameters("FogColor").SetValue(fog_color.ToVector3())
        End Sub

        ' Toggle fog
        Public Sub ToggleFog()
            If Not fogEnabled Then

                If fogStart = fogEnd Then
                    fx.Parameters("FogVector").SetValue(New Vector4(0, 0, 0, 1))
                Else
                    '// We want to transform vertex positions into view space, take the resulting Z value, then scale And offset according to the fog start/end distances.
                    '// Because we only care about the Z component, the shader can do all this with a single dot product, using only the Z row of the world+view matrix.
                    Dim scale As Single = 1.0F / (fogStart - fogEnd)
                    Dim fogVector As Vector4 = New Vector4()
                    fogVector.X = worldView.M13 * scale
                    fogVector.Y = worldView.M23 * scale
                    fogVector.Z = worldView.M33 * scale
                    fogVector.W = (worldView.M43 + fogStart) * scale
                    fx.Parameters("FogVector").SetValue(fogVector)
                    fogEnabled = True
                End If
            Else
                fx.Parameters("FogVector").SetValue(Vector4.Zero)
                fogEnabled = False
            End If
        End Sub

        ' set draw paras (use just before drawing)
        Public Sub SetDrawParams(ByVal cam As Camera, ByVal tex As Texture2D)
            Matrix.Multiply(world, cam.view, worldView)
            Dim worldTranspose, worldInverseTranspose As Matrix
            Matrix.Invert(world, worldTranspose)
            Matrix.Transpose(worldTranspose, worldInverseTranspose)
            Dim diffuse As Vector4 = New Vector4()
            Dim emissive As Vector3 = New Vector3()
            diffuse.X = diffuseCol.X * alpha
            diffuse.Y = diffuseCol.Y * alpha
            diffuse.Z = diffuseCol.Z * alpha
            diffuse.W = alpha
            emissive.X = (emissiveCol.X + ambientCol.X * diffuseCol.X) * alpha
            emissive.Y = (emissiveCol.Y + ambientCol.Y * diffuseCol.Y) * alpha
            emissive.Z = (emissiveCol.Z + ambientCol.Z * diffuseCol.Z) * alpha
            fx.Parameters("World").SetValue(world)
            fx.Parameters("WorldViewProj").SetValue(world * cam.view_proj)
            fx.Parameters("CamPos").SetValue(cam.pos)

            If tex IsNot Nothing Then
                fx.Parameters("DiffuseTexture").SetValue(tex)
            Else
                fx.Parameters("DiffuseTexture").SetValue(default_tex)
            End If

            fx.Parameters("WorldInverseTranspose").SetValue(worldInverseTranspose)
            fx.Parameters("DiffuseColor").SetValue(diffuse)
            fx.Parameters("EmissiveColor").SetValue(emissive)

            If fogEnabled Then

                If preferPerPixelLighting Then
                    fx.CurrentTechnique = fx.Techniques("Skin_PerPixel_Lit_Fog")
                ElseIf oneLight Then
                    fx.CurrentTechnique = fx.Techniques("Skin_Vertex_1Light_Fog")
                Else
                    fx.CurrentTechnique = fx.Techniques("Skin_Vertex_Lights_Fog")
                End If
            Else

                If preferPerPixelLighting Then
                    fx.CurrentTechnique = fx.Techniques("Skin_PerPixel_Lit")
                ElseIf oneLight Then
                    fx.CurrentTechnique = fx.Techniques("Skin_Vertex_1Light")
                Else
                    fx.CurrentTechnique = fx.Techniques("Skin_Vertex_Lights")
                End If
            End If

            fx.CurrentTechnique.Passes(0).Apply()
        End Sub

        Public Sub SetDiffuseCol(ByVal diffuse As Vector4)
            diffuseCol = diffuse
        End Sub

        Public Sub SetEmissiveCol(ByVal emissive As Vector3)
            emissiveCol = emissive
        End Sub

        Public Sub SetSpecularCol(ByVal specular As Vector3)
            specularCol = specular
            fx.Parameters("SpecularColor").SetValue(specularCol)
        End Sub

        Public Sub SetSpecularPow(ByVal power As Single)
            specularPow = power
            fx.Parameters("SpecularPower").SetValue(power)
        End Sub

        Public Sub SetShineAmplify(ByVal amp As Single)
            fx.Parameters("shine_amplify").SetValue(amp)
        End Sub

    End Class
End Namespace