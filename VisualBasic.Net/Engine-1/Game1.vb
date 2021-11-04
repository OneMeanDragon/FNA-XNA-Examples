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

        Private graphics As GraphicsDeviceManager
        Private _SpriteBatch As SpriteBatch

        Public _GameObjects As List(Of GameObject) = New List(Of GameObject)
        Public _map As Map = New Map()

        Private _gameHud As GameHUD = New GameHUD()

#If DEBUG Then
        Private _editor As Editor
#End If

        Public Sub New()
            graphics = New GraphicsDeviceManager(Me)
            Content.RootDirectory = "Content"

            Resolution.Init(graphics)
            Resolution.SetVirtualResolution(1280, 720)

            Resolution.SetResolution(1280, 720, False)
        End Sub

        Protected Overrides Sub Initialize()
#If DEBUG Then
            _editor = New Editor(Me)
            _editor.Show()
#End If

            MyBase.Initialize()
            Camera.Initialize()
            Globals.Initialize(Me)


        End Sub

        Protected Overrides Sub LoadContent()
            _SpriteBatch = New SpriteBatch(GraphicsDevice)

#If DEBUG Then
            _editor.LoadTextures(Content)
#End If

            _map.Load(Content)
            _gameHud.Load(Content)

            LoadLevel("LevelOne.jorge")
        End Sub

        Protected Overrides Sub Update(ByVal gameTime As GameTime)
            Input.Update()

            UpdateObjects()
            _map.Update(_GameObjects)
            UpdateCamera()

#If DEBUG Then
            _editor.Update(_GameObjects, _map)
#End If

            MyBase.Update(gameTime)
        End Sub

        Protected Overrides Sub Draw(ByVal gameTime As GameTime)
            GraphicsDevice.Clear(Color.CornflowerBlue)

            Resolution.BeginDraw()

            _SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, Nothing, Camera.GetTransformMatrix())

#If DEBUG Then
            _editor.Draw(_SpriteBatch)
#End If


            DrawObjects()
            _map.DrawWalls(_SpriteBatch)

            _SpriteBatch.End()

            _gameHud.Draw(_SpriteBatch)

            MyBase.Draw(gameTime)
        End Sub

        Public Sub LoadLevel(filename As String)
            Globals.levelName = filename

            ' Load the level data
            Dim locLevelData As LevelData = XmlHelper.Load("Content\\Levels\\" & filename)

            _map._walls = locLevelData.walls
            _map._decor = locLevelData.decor
            _GameObjects = locLevelData.objects



            '_GameObjects.Add(New Player(New Vector2(640, 360)))
            '_GameObjects.Add(New Enemy(New Vector2(300, 522)))

            '' Add Walls
            '_map._walls.Add(New Wall(New Rectangle(256, 256, 256, 256)))
            '_map._walls.Add(New Wall(New Rectangle(0, 650, 1280, 128)))

            '' Add Decor
            '_map._decor.Add(New Decor(Vector2.Zero, "background", 1.0F))

            ' Load the decor
            _map.LoadMap(Content)

            LoadObjects()
        End Sub

        Public Sub LoadObjects()
            For Each _GameObject In _GameObjects
                _GameObject.Initialize()
                _GameObject.Load(Content)
            Next
        End Sub

        Public Sub UpdateObjects()
            For Each _GameObject In _GameObjects
                _GameObject.Update(_GameObjects, _map)
            Next
        End Sub

        Public Sub DrawObjects()
            For Each _GameObject In _GameObjects
                _GameObject.Draw(_SpriteBatch)
            Next
            For Each _DecorObject In _map._decor
                _DecorObject.Draw(_SpriteBatch)
            Next
        End Sub

        Private Sub UpdateCamera()
            If _GameObjects.Count = 0 Then
                Return
            End If
            Camera.Update(_GameObjects(0)._position)
        End Sub

    End Class

End Namespace