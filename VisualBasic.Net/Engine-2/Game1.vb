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
        Const SCREENWIDTH = 728, SCREENHEIGHT As Integer = 1024 ' Target format

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

        ' 3D
        Private basic3d As Basic3DObjects

        Private landscape As Model


        Public Sub New()
            Dim desktop_width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 10
            Dim desktop_height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 10

            graphics = New GraphicsDeviceManager(Me)

            With graphics
                .PreferredBackBufferWidth = desktop_width
                .PreferredBackBufferHeight = desktop_height
                .IsFullScreen = False
                .PreferredDepthStencilFormat = DepthFormat.None
                .GraphicsProfile = GraphicsProfile.HiDef
            End With

            Window.IsBorderlessEXT = True
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
            basic3d = New Basic3DObjects(gpu, cam.up, Content)

            MyBase.Initialize()
        End Sub

        Protected Overrides Sub LoadContent()
            font = Content.Load(Of SpriteFont)("Fonts\\Arial")

            ' BASIC 3D
            basic3d.AddFloor(200, 200, Vector3.Zero, New Vector3(3.14F, 0, 0), "Images\\test_image", Nothing)
            basic3d.objex(0).pos.Y = -68 : basic3d.objex(0).UpdateTransform()
            basic3d.AddCube(50, 50, 50, Vector3.Zero, Vector3.Zero, "Images\\test_image", Nothing)
            basic3d.objex(1).pos = New Vector3(30, -40, -30)

            ' 3D MODEL LOADING
            landscape = Content.Load(Of Model)("Models\\Landscapes\\landscape")

        End Sub

        Protected Overrides Sub UnloadContent()
            MyBase.UnloadContent()
        End Sub

        Protected Overrides Sub Update(ByVal gameTime As GameTime)
            inp.Update()

            If inp.back_down = True OrElse inp.KeyDown(Keys.Escape) = True Then
                MyBase.Exit()
            End If

            cam.MoveCamera(New Vector3(inp.gp.ThumbSticks.Left.X, inp.gp.ThumbSticks.Right.Y, inp.gp.ThumbSticks.Left.Y))
            If inp.KeyDown(Keys.Up) Then basic3d.objex(1).pos.Z += 1
            If inp.KeyDown(Keys.Down) Then basic3d.objex(1).pos.Z -= 1
            If inp.KeyDown(Keys.Left) Then basic3d.objex(1).pos.X -= 1
            If inp.KeyDown(Keys.Right) Then basic3d.objex(1).pos.X += 1
            If inp.KeyDown(Keys.A) Then basic3d.objex(1).pos.Y -= 1
            If inp.KeyDown(Keys.Z) Then basic3d.objex(1).pos.Y += 1
            basic3d.objex(1).rot.Y += 0.03F ' Rotate just for blehs
            basic3d.objex(1).UpdateTransform()

            MyBase.Update(gameTime)
        End Sub

        Private Sub Set3DStates()
            gpu.BlendState = BlendState.NonPremultiplied
            gpu.DepthStencilState = DepthStencilState.Default
            If gpu.RasterizerState.CullMode = CullMode.None Then
                Dim rs As RasterizerState = New RasterizerState With {.CullMode = CullMode.CullCounterClockwiseFace}
                gpu.RasterizerState = rs
            End If
        End Sub

        Protected Overrides Sub Draw(ByVal gameTime As GameTime)
            gpu.SetRenderTarget(MainTarget)

            Set3DStates()

            gpu.Clear(ClearOptions.Target Or ClearOptions.DepthBuffer, Color.CornflowerBlue, 1.0F, 0)

            ' Render scene objects
            basic3d.Draw(cam)
            ' Render models
            drawmodel(landscape)

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
                        '.World = world_rotation * transforms(mesh.parentbone.index) * matrix.translationmatrix
                    End With
                Next
                mesh.Draw()
            Next
        End Sub

    End Class

End Namespace