Imports MGSkinnedModel

Imports System.Collections.Generic
Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Audio
Imports Microsoft.Xna.Framework.Content
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Input
Imports Microsoft.Xna.Framework.Media


Namespace GameEngine

    Public Class Game1
        Inherits Microsoft.Xna.Framework.Game

        ' Display
        Const SCREENWIDTH = 1280, SCREENHEIGHT As Integer = 720 ' Target format

        Private graphics As GraphicsDeviceManager
        Private gpu As GraphicsDevice
        Private spriteBat As SpriteBatch
        Private font As SpriteFont

        Shared screenW, screenH As Integer
        Private cam As Camera

        ' Input and Utils
        Private inp As Input

        ' Rectangles
        Private desktopRect As Rectangle
        Private screenRect As Rectangle

        ' Rendertargets and textures
        Private MainTarget As RenderTarget2D

        ' Models
        Private mSky As Sky
        Private landscape As Model

        Private idleAnimationPlayer, walkAnimationPlayer, runAnimationPlayer As AnimationPlayer
        Private idleTransforms(), walkTransforms(), runTransforms(), blendTransforms() As Matrix
        Private hero(0) As Model
        'Private heroFX As MySkinFX

        Const mIDLE = 0, mWALK = 1, mRUN = 2
        Private hero_pos As Vector3 = New Vector3(0, -1, 0)
        Private hero_vel As Vector3
        Private last_nonzero_vel As Vector2
        Private hero_angle, playspeed, speed, hero_height As Single
        Private ground_y As Single
        Private mtx_hero_rotate As Matrix
        Private onGround As Boolean = True
        ' Song
        Private mSong As Song

        ' 3D
        'Private basic3d As Basic3DObjects



        Public Sub New()
            graphics = New GraphicsDeviceManager(Me)

            Resolution.Init(graphics)
            Resolution.SetVirtualResolution(1280, 720)
            Resolution.SetResolution(1280, 720, False)

            ' M O U S E  V I S I B L I T Y
            IsMouseVisible = True

            Content.RootDirectory = "Content"
        End Sub

        Protected Overrides Sub Initialize()
            ' Display
            gpu = GraphicsDevice

            Dim pp As PresentationParameters = gpu.PresentationParameters
            spriteBat = New SpriteBatch(gpu)
            MainTarget = New RenderTarget2D(gpu, SCREENWIDTH, SCREENHEIGHT, False, pp.BackBufferFormat, DepthFormat.Depth24)
            screenW = MainTarget.Width
            screenH = MainTarget.Height
            desktopRect = New Rectangle(0, 0, pp.BackBufferWidth, pp.BackBufferHeight)
            screenRect = New Rectangle(0, 0, screenW, screenH)

            ' new
            inp = New Input(pp, MainTarget)

            ' INIT 3D
            cam = New Camera(gpu, Vector3.Down, inp)
            'basic3d = New Basic3DObjects(gpu, cam.up, Content)
            mSky = New Sky(gpu, Content)
            ' Because we cant use redim anymore
            Array.Resize(Of Model)(hero, 3)
            last_nonzero_vel = New Vector2(0, -1.0F)
            hero_height = 1.5F

            MyBase.Initialize()
        End Sub

        Protected Overrides Sub LoadContent()
            font = Content.Load(Of SpriteFont)("Fonts\\Arial")
            'heroFX = New MySkinFX(Content, cam, "MySkinFX")

            ' BASIC 3D
            'basic3d.AddFloor(200, 200, Vector3.Zero, New Vector3(3.14F, 0, 0), "Images\\test_image", Nothing)
            'basic3d.objex(0).pos.Y = -68 : basic3d.objex(0).UpdateTransform()
            'basic3d.AddCube(50, 50, 50, Vector3.Zero, Vector3.Zero, "Images\\test_image", Nothing)
            'basic3d.objex(1).pos = New Vector3(30, -40, -30)

            ' 3D MODEL LOADING
            mSky.Load("Models\\Skys\\sky_model")
            landscape = Content.Load(Of Model)("Models\\Landscapes\\landscape")

            ' LOAD kid
            'Content.RootDirectory = "Content/kid"
            'hero(mIDLE) = Content.Load(Of Model)("kid_idle")
            'hero(mWALK) = Content.Load(Of Model)("kid_walk")
            'hero(mRUN) = Content.Load(Of Model)("kid_run")
            'Content.RootDirectory = "Content"

            '' LOAD Stuffy
            Content.RootDirectory = "Content/stuffy"
            hero(mIDLE) = Content.Load(Of Model)("stuffy_idle")
            hero(mWALK) = Content.Load(Of Model)("stuffy_walk")
            hero(mRUN) = Content.Load(Of Model)("stuffy_run")

            Content.RootDirectory = "Content"
            'hero(mIDLE).
            'Dim tmpTest As SkinningData = Content.Load(Of SkinningData)("stuffy/stuffy_idle")

            Dim skinningDataIdle As SkinningData = hero(mIDLE).Tag
            If skinningDataIdle Is Nothing Then Throw New InvalidOperationException("This model dosent contain a skinningdata tag")
            idleAnimationPlayer = New AnimationPlayer(skinningDataIdle)

            Dim skinningDataWalk As SkinningData = hero(mWALK).Tag
            If skinningDataWalk Is Nothing Then Throw New InvalidOperationException("This model dosent contain a skinningdata tag")
            walkAnimationPlayer = New AnimationPlayer(skinningDataWalk)

            Dim skinningDataRun As SkinningData = hero(mRUN).Tag
            If skinningDataRun Is Nothing Then Throw New InvalidOperationException("This model dosent contain a skinningdata tag")
            runAnimationPlayer = New AnimationPlayer(skinningDataRun)

            Dim bones_count As Integer = hero(mIDLE).Bones.Count
            Array.Resize(Of Matrix)(idleTransforms, bones_count)
            Array.Resize(Of Matrix)(walkTransforms, bones_count)
            Array.Resize(Of Matrix)(runTransforms, bones_count)
            Array.Resize(Of Matrix)(blendTransforms, bones_count)

            ' COPY BONE TRANSFORMS
            hero(mIDLE).CopyAbsoluteBoneTransformsTo(idleTransforms)
            hero(mWALK).CopyAbsoluteBoneTransformsTo(walkTransforms)
            hero(mRUN).CopyAbsoluteBoneTransformsTo(runTransforms)

            ' START AN ANIMATION 
            Dim idleClip As AnimationClip = skinningDataIdle.AnimationClips("Take 001")
            Dim walkClip As AnimationClip = skinningDataWalk.AnimationClips("Take 001")
            Dim runClip As AnimationClip = skinningDataRun.AnimationClips("Take 001")
            idleAnimationPlayer.StartClip(idleClip)
            walkAnimationPlayer.StartClip(walkClip)
            runAnimationPlayer.StartClip(runClip)


            ' Sound Loading
            mSong = Content.Load(Of Song)("Songs\\ForestShadows")
            MediaPlayer.Volume = 0.5F
            MediaPlayer.Play(mSong)
            MediaPlayer.IsRepeating = True
        End Sub

        Protected Overrides Sub UnloadContent()
            MyBase.UnloadContent()
        End Sub

        Protected Overrides Sub Update(ByVal gameTime As GameTime)
            inp.Update()

            If inp.back_down = True OrElse inp.KeyDown(Keys.Escape) = True Then
                MyBase.Exit()
            End If

            ' CAMERA
            'cam.MoveCamera(New Vector3(inp.gp.ThumbSticks.Left.X, inp.gp.ThumbSticks.Right.Y, inp.gp.ThumbSticks.Left.Y))
            cam.Update_Player_Cam(hero_pos)
            If inp.KeyDown(Keys.Left) Then hero_vel += New Vector3(cam.view.Left.X, 0, cam.view.Left.Z) * 0.1F
            If inp.KeyDown(Keys.Right) Then hero_vel += New Vector3(cam.view.Right.X, 0, cam.view.Right.Z) * 0.1F
            If inp.KeyDown(Keys.Up) Then hero_vel += New Vector3(cam.view.Forward.X, 0, cam.view.Forward.Z) * 0.1F
            If inp.KeyDown(Keys.Down) Then hero_vel += New Vector3(cam.view.Backward.X, 0, cam.view.Backward.Z) * 0.1F

            Dim jump_pressed As Boolean = (inp.KeyDown(Keys.Space) OrElse inp.gp.IsButtonDown(Buttons.A))
            If ((jump_pressed) AndAlso (onGround)) Then
                hero_vel.Y = -1.3F
                onGround = False
            End If

            'If inp.KeyDown(Keys.PageDown) Then hero_vel.Y += 0.05F
            ' GAMEPAD CONTROL
            Dim MovePad_LeftRight = 0.0F, MovePad_UpDown As Single = 0.0F
            If inp.gp.IsConnected Then
                MovePad_LeftRight = inp.gp.ThumbSticks.Left.X
                MovePad_UpDown = inp.gp.ThumbSticks.Left.Y
                If (MovePad_UpDown < -Input.DEADZONE) OrElse
                        (MovePad_UpDown > Input.DEADZONE) OrElse
                        (MovePad_LeftRight < -Input.DEADZONE) OrElse
                        (MovePad_LeftRight > Input.DEADZONE) Then
                    hero_vel.X = MovePad_LeftRight * cam.view.Right.X + MovePad_UpDown * cam.view.Forward.X
                    hero_vel.Z = MovePad_LeftRight * cam.view.Right.Z + MovePad_UpDown * cam.view.Forward.Z
                End If
            End If

            ' COLLIDE WITH LOW-GROUND
            ground_y = 0.8F ' = GetGround(hero_pos)
            If hero_pos.Y > (ground_y - hero_height) Then
                If onGround = False Then
                    hero_pos.Y = ground_y - hero_height
                End If
                hero_vel.Y = 0
                onGround = True
            ElseIf hero_vel.y < 8.0F Then
                hero_vel.Y += 0.03F
            End If

            hero_pos += hero_vel
            hero_vel.X *= 0.96F
            hero_vel.Z *= 0.96F
            Dim map_vel As Vector2 = New Vector2(hero_vel.X, hero_vel.Z)
            speed = map_vel.Length()
            If speed > 1.0F Then
                speed = 1.0F
                map_vel.Normalize()
                hero_vel.X = map_vel.X
                hero_vel.Z = map_vel.Y
            End If
            If speed > 0.01F Then
                last_nonzero_vel = map_vel
            End If
            hero_angle = last_nonzero_vel.ToAngleFlipZ()
            mtx_hero_rotate = Matrix.CreateFromYawPitchRoll(hero_angle - MathHelper.ToRadians(90), 3.14F, 0)

            Dim playspeed As Single = speed * 6.0F + 0.5F '0.8F
            Dim elapsedTime As TimeSpan = TimeSpan.FromSeconds(gameTime.ElapsedGameTime.TotalSeconds * playspeed)

            idleAnimationPlayer.Update(elapsedTime, True, Matrix.Identity)
            walkAnimationPlayer.Update(elapsedTime, True, Matrix.Identity)
            runAnimationPlayer.Update(elapsedTime, True, Matrix.Identity)

            ' Adjust hero size
            mtx_hero_rotate *= Matrix.CreateScale(0.8F)

            MyBase.Update(gameTime)
        End Sub

#Region "S E T  3 D  S T A T E S -----------"
        Private rs_ccw As RasterizerState = New RasterizerState With {.FillMode = FillMode.Solid, .CullMode = CullMode.CullCounterClockwiseFace}
        Private Sub Set3DStates()
            gpu.BlendState = BlendState.NonPremultiplied
            gpu.DepthStencilState = DepthStencilState.Default
            If gpu.RasterizerState.CullMode = CullMode.None Then
                gpu.RasterizerState = rs_ccw
            End If
        End Sub
#End Region

        Protected Overrides Sub Draw(ByVal gameTime As GameTime)
            gpu.SetRenderTarget(MainTarget)

            gpu.Clear(ClearOptions.Target Or ClearOptions.DepthBuffer, Color.Black, 1.0F, 0)

            ' Render scene objects
            mSky.Draw(cam)
            Set3DStates()            'basic3d.Draw(cam)
            ' Render models
            DrawModel(landscape)

            ' SETUP FOR SHADOWS
            Dim light_pos As Vector3 = New Vector3(20, -40, 20)
            'Dim light_radius As Single = 80.0F, light_radius_sq As Single = light_radius * light_radius
            Dim litDir As Vector3 = hero_pos - light_pos
            'Dim distance_intensity As Single = litDir.LengthSquared()
            'If distance_intensity > light_radius_sq * 4 Then
            '    distance_intensity = 0
            'Else
            '    distance_intensity = 1.0F - distance_intensity \ (light_radius_sq * 4)
            'End If
            ' matrix to project shadow onto plane.
            Dim locPlane As Plane = New Plane(Vector3.Up, ground_y)
            Dim ShadowTransform As Matrix = Matrix.CreateShadow(New Vector3(litDir.X, litDir.Y, litDir.Z), locPlane)


            ' Start SkinnedMesh Animation
            Dim bones1() As Matrix
            Dim bones2() As Matrix
            Dim percentage As Single

            If speed <= 0.2F Then
                bones1 = idleAnimationPlayer.GetSkinTransforms()
                bones2 = walkAnimationPlayer.GetSkinTransforms()
                percentage = 1.0F / 0.2F * speed
            Else
                bones1 = walkAnimationPlayer.GetSkinTransforms()
                bones2 = runAnimationPlayer.GetSkinTransforms()
                percentage = 1.0F / 0.8F * (speed - 0.2F)
            End If

            Dim i As Integer = 0
            While i < bones1.Length
                blendTransforms(i) = Matrix.Lerp(bones1(i), bones2(i), percentage)
                i += 1
            End While
            blendTransforms = idleAnimationPlayer.GetSkinTransforms()

            ' RENDER SKIN MESH
            For Each mesh As ModelMesh In hero(mIDLE).Meshes
#Region "kid renderer"
                'For Each part As ModelMeshPart In mesh.MeshParts
                '    Dim oldEffect As SkinnedEffect = part.Effect
                '    heroFX.alpha = oldEffect.Alpha
                '    If heroFX.alpha >= 0.6 Then
                '        With heroFX
                '            .preferPerPixelLighting = True
                '            .SetDiffuseCol(Color.White.ToVector4())
                '            .SetSpecularCol(New Vector3(0.2F, 0.3F, 0.05F))
                '            .SetSpecularPow(256.0F)
                '            .SetBoneTransforms(blendTransforms)
                '            .world = mtx_hero_rotate * Matrix.CreateTranslation(hero_pos)
                '            .SetDrawParams(cam, oldEffect.Texture)
                '        End With
                '        gpu.SetVertexBuffer(part.VertexBuffer)
                '        gpu.Indices = part.IndexBuffer
                '        'gpu.DrawPrimitives(PrimitiveType.TriangleList, part.VertexOffset, part.StartIndex, part.PrimitiveCount)
                '        gpu.DrawPrimitives(PrimitiveType.TriangleList, part.VertexOffset, part.PrimitiveCount)
                '    Else
                '        Continue For
                '    End If
                'Next
#End Region

#Region "Stuffy Render"
                For Each effect As SkinnedEffect In mesh.Effects
                    With effect
                        .Alpha = 1.0F
                        .EnableDefaultLighting()
                        .PreferPerPixelLighting = True
                        .SetBoneTransforms(blendTransforms)
                        .World = blendTransforms(mesh.ParentBone.Index) * mtx_hero_rotate * Matrix.CreateTranslation(hero_pos)
                        .View = cam.view
                        .Projection = cam.proj
                        .SpecularColor = New Vector3(0.2F, 0.3F, 0.05F)
                        .SpecularPower = 128.0F
                    End With
                Next
                mesh.Draw()
                ' D R A W  S H A D O W
                For Each fct As SkinnedEffect In mesh.Effects
                    Dim old_color As Vector3 = fct.DiffuseColor
                    With fct
                        .DiffuseColor = Vector3.Zero
                        .World *= ShadowTransform
                        .Alpha = 0.9F 'distance_intensity
                        .DiffuseColor = old_color
                    End With
                Next
                mesh.Draw()
#End Region
            Next




            ' Draw maintarget to backbuffer
            gpu.SetRenderTarget(Nothing)
            spriteBat.Begin(SpriteSortMode.Deferred, BlendState.Opaque, SamplerState.LinearWrap, DepthStencilState.None, RasterizerState.CullNone)
            spriteBat.Draw(MainTarget, desktopRect, Color.White)
            spriteBat.End()

            MyBase.Draw(gameTime)
        End Sub

        Private Sub DrawModel(inModel As Model)
            'dim transforms(inmodel.bones.count) as matrix
            'model.copyabsolutebonetransformsto(transforms) 'get model transforms
            For Each mesh As ModelMesh In inModel.Meshes
                For Each effect As BasicEffect In mesh.Effects
                    With effect
                        .EnableDefaultLighting()
                        .PreferPerPixelLighting = True
                        .TextureEnabled = True
                        .View = cam.view
                        .Projection = cam.proj
                        .AmbientLightColor = New Vector3(0.2F, 0.1F, 0.3F)
                        .DiffuseColor = New Vector3(0.95F, 0.96F, 0.85F)

                        .FogEnabled = True
                        .FogStart = 2.0F
                        .FogEnd = 500.0F
                        .FogColor = New Vector3(0.04F, 0.0F, 0.02F)
                        '.World = world_rotation * transforms(mesh.parentbone.index) * matrix.translationmatrix
                    End With
                Next
                mesh.Draw()
            Next
        End Sub

    End Class

End Namespace