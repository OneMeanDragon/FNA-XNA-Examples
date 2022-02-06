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

        Public Const FRAMERATE_TARGET As Single = 60.0F ' Fixed timestep Framerate

        Private graphics As GraphicsDeviceManager
        Private spriteBat As SpriteBatch
        Private RenderTarget As RenderTarget2D

        Public scale As Single = 0.44444F
        Private timepassed As Single = 0F


        Private stickMan As Texture2D = Nothing
        Private background1 As Texture2D = Nothing

        Private playerpos As Vector2 = Vector2.Zero

        Public Sub New()
            graphics = New GraphicsDeviceManager(Me)
            Content.RootDirectory = "Content"

        End Sub

        Protected Overrides Sub Initialize()
            TargetElapsedTime = TimeSpan.FromSeconds(1.0F / FRAMERATE_TARGET)
            IsFixedTimeStep = True

            MyBase.Initialize()

            graphics.PreferredBackBufferWidth = 1280
            graphics.PreferredBackBufferHeight = 720
            graphics.ApplyChanges()

        End Sub

        Protected Overrides Sub LoadContent()
            spriteBat = New SpriteBatch(GraphicsDevice)

            stickMan = Content.Load(Of Texture2D)("images\cursor.png")
            background1 = Content.Load(Of Texture2D)("images\background.png")

            RenderTarget = New RenderTarget2D(GraphicsDevice, 1920, 1080)
        End Sub

        Protected Overrides Sub UnloadContent()
            MyBase.UnloadContent()
        End Sub

        Protected Overrides Sub Update(ByVal gameTime As GameTime)
            timepassed = gameTime.ElapsedGameTime.Milliseconds

            Dim movementspeed As Single = timepassed / 2.38F

            Dim keystate As KeyboardState = Keyboard.GetState()

            If keystate.IsKeyDown(Keys.Right) Then
                playerpos.X += movementspeed
            End If
            If keystate.IsKeyDown(Keys.Left) Then
                playerpos.X -= movementspeed
            End If
            If keystate.IsKeyDown(Keys.Up) Then
                playerpos.Y -= movementspeed
            End If
            If keystate.IsKeyDown(Keys.Down) Then
                playerpos.Y += movementspeed
            End If

            MyBase.Update(gameTime)
        End Sub

        Protected Overrides Sub Draw(ByVal gameTime As GameTime)
            scale = 1.0F / (1080 / graphics.GraphicsDevice.Viewport.Height)
            GraphicsDevice.SetRenderTarget(RenderTarget)

            GraphicsDevice.Clear(Color.CornflowerBlue)

            spriteBat.Begin()

            spriteBat.Draw(background1, Vector2.Zero, Color.White)
            spriteBat.Draw(stickMan, playerpos, Color.YellowGreen)

            spriteBat.End()

            GraphicsDevice.SetRenderTarget(Nothing)
            GraphicsDevice.Clear(Color.CornflowerBlue)

            spriteBat.Begin()
            spriteBat.Draw(RenderTarget, Vector2.Zero, Nothing, Color.White, 0F, Vector2.Zero, scale, SpriteEffects.None, 0F)
            spriteBat.End()

            MyBase.Draw(gameTime)
        End Sub

    End Class

End Namespace
