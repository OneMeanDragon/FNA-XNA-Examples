Imports System
Imports System.Collections.Generic
Imports System.Runtime.InteropServices
Imports System.ComponentModel
Imports System.Data
Imports System.Linq
Imports System.Text
Imports System.String
Imports System.IO
Imports System.Windows.Forms
Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Content
Imports Microsoft.Xna.Framework.Graphics
Imports SDL2
Imports Color = Microsoft.Xna.Framework.Color

Namespace GameEngine
    Partial Public Class Editor
        Inherits Form

        Public game As Game1
        Private gameWinHandle As IntPtr

        Public Enum CreateMode
            None
            Walls
            Objects
            Decor
        End Enum

        Public mode As CreateMode = CreateMode.None
        Public placingItem As Boolean = False
        Private grid, pixel As Texture2D
        Private cameraPosition As Vector2
        Private savePath As String = ""

        Enum ObjectType
            Enemy
            PowerUp
            NumOfObjects
        End Enum

        Const objectsNamespace As String = "GameEngine."

        Public Sub New(ByVal inputGame As Game1)
            InitializeComponent()
            game = inputGame
            game.IsMouseVisible = True
            Dim info As SDL.SDL_SysWMinfo = New SDL.SDL_SysWMinfo()
            SDL.SDL_GetWindowWMInfo(game.Window.Handle, info)
            gameWinHandle = info.info.win.window
            Dim gameWindow As RECT = New RECT()
            GetWindowRect(gameWinHandle, gameWindow)
            Location = New System.Drawing.Point(gameWindow.Right + 11, gameWindow.Top)
            PopulateObjectList()
            mapHeight.Value = game._map._height
            mapWidth.Value = game._map._width
        End Sub

        Private Function myGetType(valueIn As String) As Type
            Dim valueType = Type.GetType(valueIn)
            If valueType IsNot Nothing Then
                Return valueType
            End If
            For Each a In AppDomain.CurrentDomain.GetAssemblies
                valueType = a.GetType(valueIn)
                If valueType IsNot Nothing Then
                    Return valueType
                End If
            Next
            Return Nothing
        End Function

        Private Sub addButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            If objectTypes.SelectedIndex = -1 Then Return

            If mode = CreateMode.Objects Then
                Dim selectedObject As ObjectType = CType(objectTypes.Items(objectTypes.SelectedIndex), ObjectType)
                Dim locType As Type = myGetType(objectsNamespace & selectedObject.ToString()) 'Type.GetType
                If locType Is Nothing Then
                    Return
                End If
                Dim newObject As GameObject = CType(Activator.CreateInstance(locType), GameObject)
                    If newObject Is Nothing Then Return
                    newObject.Load(game.Content)
                    game._GameObjects.Add(newObject)
                    placingItem = True
                    FocusGameWindow()
                    SetListBox(game._GameObjects, False)
                ElseIf mode = CreateMode.Decor Then
                    Dim newDecor As Decor = New Decor()
                newDecor._imagePath = "decorplaceholder"
                newDecor.Load(game.Content, newDecor._imagePath)
                game._map._decor.Add(newDecor)
                placingItem = True
                SetListBox(game._map._decor, False)
                FocusGameWindow()
            End If
        End Sub

        Public Sub LoadTextures(ByVal content As ContentManager)
            grid = TextureLoader.Load("128grid", content)
            pixel = TextureLoader.Load("pixel", content)
        End Sub

        Public Sub Update(ByVal objects As List(Of GameObject), ByVal map As Map)
            Dim mousePosition As Vector2 = Input.MousePositionCamera()
            Dim desiredIndex As Point = map.GetTileIndex(mousePosition)

            If (Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftControl) OrElse (Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.RightControl))) AndAlso mode <> CreateMode.Walls Then

                If Input.MouseLeftClicked() AndAlso mode = CreateMode.Objects Then

                    For i As Integer = 0 To objects.Count - 1

                        If objects(i).CheckCollision(New Rectangle(desiredIndex.X * map._tilesize, desiredIndex.Y * map._tilesize, 128, 128)) Then
                            listBox.SelectedIndex = i
                            Exit For
                        End If
                    Next
                ElseIf Input.MouseLeftClicked() AndAlso mode = CreateMode.Decor Then

                    For i As Integer = 0 To map._decor.Count - 1

                        If map._decor(i).CheckCollision(New Rectangle(desiredIndex.X * map._tilesize, desiredIndex.Y * map._tilesize, 128, 128)) Then
                            listBox.SelectedIndex = i
                            Exit For
                        End If
                    Next
                ElseIf Input.KeyPressed(Microsoft.Xna.Framework.Input.Keys.C) AndAlso mode = CreateMode.Decor Then
                    CopyDecor()
                End If
            ElseIf Input.MouseLeftDown() = True AndAlso GameWindowFocused() = True Then

                If mode = CreateMode.Walls Then

                    If desiredIndex.X >= 0 AndAlso desiredIndex.X < map._width AndAlso desiredIndex.Y >= 0 AndAlso desiredIndex.Y < map._height Then
                        Dim newWall As Rectangle = New Rectangle(desiredIndex.X * map._tilesize, desiredIndex.Y * map._tilesize, map._tilesize, map._tilesize)

                        If map.CheckCollision(newWall) = Rectangle.Empty Then
                            Dim oldWall As Rectangle = Rectangle.Empty

                            For i As Integer = 0 To map._walls.Count - 1
                                oldWall = map._walls(i)._wall

                                If map._walls(i)._wall.Intersects(New Rectangle(newWall.X + map._tilesize, newWall.Y, newWall.Width, newWall.Height)) AndAlso map._walls(i)._wall.Y = newWall.Y AndAlso map._walls(i)._wall.Height = newWall.Height Then
                                    newWall = New Rectangle(oldWall.X - map._tilesize, oldWall.Y, oldWall.Width + map._tilesize, oldWall.Height)
                                    map._walls(i)._wall = newWall
                                    Exit For
                                ElseIf map._walls(i)._wall.Intersects(New Rectangle(newWall.X - map._tilesize, newWall.Y, newWall.Width, newWall.Height)) AndAlso map._walls(i)._wall.Y = newWall.Y AndAlso map._walls(i)._wall.Height = newWall.Height Then
                                    newWall = New Rectangle(oldWall.X, oldWall.Y, oldWall.Width + map._tilesize, oldWall.Height)
                                    map._walls(i)._wall = newWall
                                    Exit For
                                End If

                                If map._walls(i)._wall.Intersects(New Rectangle(newWall.X, newWall.Y + map._tilesize, newWall.Width, newWall.Height)) AndAlso map._walls(i)._wall.X = newWall.X AndAlso map._walls(i)._wall.Width = newWall.Width Then
                                    newWall = New Rectangle(oldWall.X, oldWall.Y - map._tilesize, oldWall.Width, oldWall.Height + map._tilesize)
                                    map._walls(i)._wall = newWall
                                    Exit For
                                ElseIf map._walls(i)._wall.Intersects(New Rectangle(newWall.X, newWall.Y - map._tilesize, newWall.Width, newWall.Height)) AndAlso map._walls(i)._wall.X = newWall.X AndAlso map._walls(i)._wall.Width = newWall.Width Then
                                    newWall = New Rectangle(oldWall.X, oldWall.Y, oldWall.Width, oldWall.Height + map._tilesize)
                                    map._walls(i)._wall = newWall
                                    Exit For
                                End If

                                oldWall = Rectangle.Empty
                            Next

                            If oldWall = Rectangle.Empty Then map._walls.Add(New Wall(newWall))
                            SetListBox(map._walls, False)
                        Else

                            For i As Integer = 0 To map._walls.Count - 1

                                If map._walls(i)._wall.Intersects(newWall) Then
                                    listBox.SelectedIndex = i
                                    Exit For
                                End If
                            Next
                        End If
                    End If
                ElseIf mode = CreateMode.Objects AndAlso placingItem = True Then
                    game._GameObjects(game._GameObjects.Count - 1).startPosition = game._GameObjects(game._GameObjects.Count - 1)._position
                    game._GameObjects(game._GameObjects.Count - 1).Initialize()
                    SetListBox(game._GameObjects, False)
                ElseIf mode = CreateMode.Decor AndAlso placingItem = True Then
                    SetListBox(game._map._decor, False)
                End If

                placingItem = False
            ElseIf Input.MouseRightDown() = True AndAlso GameWindowFocused() = True Then

                If mode = CreateMode.Walls Then
                    Dim input As Rectangle = New Rectangle(CInt(mousePosition.X), CInt(mousePosition.Y), 1, 1)

                    For i As Integer = 0 To game._map._walls.Count - 1

                        If game._map._walls(i)._wall.Intersects(input) = True Then
                            RemoveWall(i)
                            Exit For
                        End If
                    Next
                End If
            ElseIf placingItem = True Then

                If mode = CreateMode.Objects Then
                    game._GameObjects(game._GameObjects.Count - 1)._position = New Vector2(desiredIndex.X * map._tilesize, desiredIndex.Y * map._tilesize)
                ElseIf mode = CreateMode.Decor Then
                    game._map._decor(game._map._decor.Count - 1)._position = New Vector2(desiredIndex.X * map._tilesize, desiredIndex.Y * map._tilesize)
                End If
            End If

            If paused.Checked = False AndAlso game._GameObjects.Count > 0 Then
                cameraPosition = game._GameObjects(0)._position
            Else

                If Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Right) Then
                    cameraPosition.X += 6
                ElseIf Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Left) Then
                    cameraPosition.X -= 6
                End If

                If Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Down) Then
                    cameraPosition.Y += 6
                ElseIf Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Up) Then
                    cameraPosition.Y -= 6
                End If

                Camera.Update(cameraPosition)
            End If
        End Sub

        Public Sub Draw(ByVal spriteBatch As SpriteBatch)
            DrawSelectedItem(spriteBatch)
            If drawGridCheckBox.Checked = False Then Return

            For x As Integer = 0 To game._map._width - 1

                For y As Integer = 0 To game._map._height - 1
                    spriteBatch.Draw(grid, New Vector2(x, y) * game._map._tilesize, Nothing, Color.Cyan, 0F, Vector2.Zero, 1.0F, SpriteEffects.None, 0.1F)
                Next
            Next
        End Sub

        <DllImport("user32.dll")>
        Private Shared Function SetForegroundWindow(ByVal hWnd As IntPtr) As Boolean
        End Function
        <DllImport("user32.dll")>
        Private Shared Function GetForegroundWindow() As IntPtr
        End Function
        <DllImport("user32.dll", SetLastError:=True)> '<MarshalAs(UnmanagedType.Bool)>
        Private Shared Function GetWindowRect(ByVal hWnd As IntPtr, ByRef lpRect As RECT) As Boolean
        End Function

        <StructLayout(LayoutKind.Sequential)>
        Private Structure RECT
            Public Left As Integer
            Public Top As Integer
            Public Right As Integer
            Public Bottom As Integer
        End Structure

        Private Function LoadTextureStream(ByVal graphicsDevice As GraphicsDevice, ByVal filePath As String) As Texture2D
            Dim file As Texture2D = Nothing
            Dim resultTexture As Texture2D
            Dim result As RenderTarget2D = Nothing

            Try

                Using titleStream As System.IO.Stream = TitleContainer.OpenStream(filePath)
                    file = Texture2D.FromStream(graphicsDevice, titleStream)
                End Using

            Catch
                Throw New System.IO.FileLoadException("Cannot load '" & filePath & "' file!")
            End Try

            Dim pp As PresentationParameters = graphicsDevice.PresentationParameters
            result = New RenderTarget2D(graphicsDevice, file.Width, file.Height, True, pp.BackBufferFormat, pp.DepthStencilFormat)
            graphicsDevice.SetRenderTarget(result)
            graphicsDevice.Clear(Color.Black)
            Dim blendColor As BlendState = New BlendState()
            blendColor.ColorWriteChannels = ColorWriteChannels.Red Or ColorWriteChannels.Green Or ColorWriteChannels.Blue
            blendColor.AlphaDestinationBlend = Blend.Zero
            blendColor.ColorDestinationBlend = Blend.Zero
            blendColor.AlphaSourceBlend = Blend.SourceAlpha
            blendColor.ColorSourceBlend = Blend.SourceAlpha
            Dim spriteBatch As SpriteBatch = New SpriteBatch(graphicsDevice)
            spriteBatch.Begin(SpriteSortMode.Immediate, blendColor)
            spriteBatch.Draw(file, file.Bounds, Color.White)
            spriteBatch.[End]()
            Dim blendAlpha As BlendState = New BlendState()
            blendAlpha.ColorWriteChannels = ColorWriteChannels.Alpha
            blendAlpha.AlphaDestinationBlend = Blend.Zero
            blendAlpha.ColorDestinationBlend = Blend.Zero
            blendAlpha.AlphaSourceBlend = Blend.One
            blendAlpha.ColorSourceBlend = Blend.One
            spriteBatch.Begin(SpriteSortMode.Immediate, blendAlpha)
            spriteBatch.Draw(file, file.Bounds, Color.White)
            spriteBatch.[End]()
            graphicsDevice.SetRenderTarget(Nothing)
            resultTexture = New Texture2D(graphicsDevice, result.Width, result.Height)
            Dim data As Color() = New Color(result.Height * result.Width - 1) {}
            Dim textureColor As Color() = New Color(result.Height * result.Width - 1) {}
            result.GetData(Of Color)(textureColor)

            For i As Integer = 0 To result.Height - 1

                For j As Integer = 0 To result.Width - 1
                    data(j + i * result.Width) = textureColor(j + i * result.Width)
                Next
            Next

            resultTexture.SetData(data)
            Return resultTexture
        End Function

        Private Sub DrawSelectedItem(ByVal spriteBatch As SpriteBatch)
            If drawSelected.Checked = False Then Return
            If listBox.SelectedIndex = -1 Then Return

            If mode = CreateMode.Walls Then
                If game._map._walls.Count = 0 OrElse listBox.SelectedIndex >= game._map._walls.Count Then Return
                Dim selectedWall As Wall = game._map._walls(listBox.SelectedIndex)
                spriteBatch.Draw(pixel, New Vector2(CInt(selectedWall._wall.X), CInt(selectedWall._wall.Y)), selectedWall._wall, Color.SkyBlue, 0F, Vector2.Zero, 1.0F, SpriteEffects.None, 0)
            ElseIf mode = CreateMode.Objects Then
                If game._GameObjects.Count = 0 OrElse listBox.SelectedIndex >= game._GameObjects.Count Then Return
                Dim selectedObject As GameObject = game._GameObjects(listBox.SelectedIndex)
                spriteBatch.Draw(pixel, New Vector2(CInt(selectedObject.BoundingBox.X), CInt(selectedObject.BoundingBox.Y)), selectedObject.BoundingBox, New Color(80, 80, 100, 80), 0F, Vector2.Zero, 1.0F, SpriteEffects.None, 0)
            ElseIf mode = CreateMode.Decor Then
                If game._map._decor.Count = 0 OrElse listBox.SelectedIndex >= game._map._decor.Count Then Return
                Dim selectedDecor As Decor = game._map._decor(listBox.SelectedIndex)
                spriteBatch.Draw(pixel, New Vector2(CInt(selectedDecor.BoundingBox.X), CInt(selectedDecor.BoundingBox.Y)), selectedDecor.BoundingBox, New Color(80, 80, 100, 80), 0F, Vector2.Zero, 1.0F, SpriteEffects.None, 0)
            End If
        End Sub

        Private Sub CopyDecor()
            If listBox.SelectedIndex = -1 Then Return
            Dim selectedDecor As Decor = CType(game._map._decor(listBox.SelectedIndex), Decor)
            Dim newDecor As Decor = New Decor(selectedDecor._position, selectedDecor._imagePath, selectedDecor._layerDepth)
            newDecor.Load(game.Content, newDecor._imagePath)
            game._map._decor.Add(newDecor)
            SetListBox(game._map._decor, False)
        End Sub

        Public Sub RemoveWall(ByVal index As Integer)
            Dim bookmarkIndex As Integer = listBox.SelectedIndex
            game._map._walls.RemoveAt(index)
            SetListBox(game._map._walls, False)
        End Sub

        Private Sub ResetGame()
            For i As Integer = 0 To game._GameObjects.Count - 1
                game._GameObjects(i).Initialize()
                game._GameObjects(i).SetToDefaultPosition()
            Next

            For i As Integer = 0 To game._map._decor.Count - 1
                game._map._decor(i).Initialize()
            Next
        End Sub

        Public Sub PopulateObjectList()
            For i As Integer = 0 To CInt(ObjectType.NumOfObjects) - 1
                objectTypes.Items.Add(CType(i, ObjectType))
            Next

            objectTypes.SelectedIndex = 0
        End Sub

        Private Sub ResetEditorList()
            objectsRadioButton.Checked = False
            decorRadioButton.Checked = False
            wallsRadioButton.Checked = False
            noneRadioButton.Checked = True
            Dim [nothing] As List(Of Integer) = New List(Of Integer)()
            SetListBox([nothing], True)
            FocusGameWindow()
        End Sub

        Private Sub LoadLevelContent()
            For i As Integer = 0 To game._map._decor.Count - 1
                game._map._decor(i).Load(game.Content, game._map._decor(i)._imagePath)
            Next

            For i As Integer = 0 To game._GameObjects.Count - 1
                game._GameObjects(i).Initialize()
                game._GameObjects(i).Load(game.Content)
            Next
        End Sub

        Public Sub SetListBox(Of T)(ByVal inputList As List(Of T), ByVal highlightFirstInList As Boolean)
            listBox.DataSource = Nothing
            listBox.DataSource = inputList

            If highlightFirstInList = True AndAlso inputList IsNot Nothing AndAlso inputList.Count > 0 Then
                listBox.SelectedIndex = listBox.TopIndex = 0
            ElseIf highlightFirstInList = True AndAlso inputList IsNot Nothing Then
                listBox.SelectedIndex = listBox.TopIndex = -1
            ElseIf listBox.SelectedIndex < 0 AndAlso listBox.Items.Count > 0 Then
                listBox.SelectedIndex = 0
            Else
                listBox.SelectedIndex = listBox.Items.Count - 1
            End If

            SetDisplayMember()
        End Sub

        Private Sub SetDisplayMember()
            If mode = CreateMode.Walls Then
                listBox.DisplayMember = "EditorWall"
            ElseIf mode = CreateMode.Objects Then
            ElseIf mode = CreateMode.Decor Then
                listBox.DisplayMember = "Name"
            End If
        End Sub

        Public Sub RefreshListBox(Of T)(ByVal inputList As List(Of T))
            If listBox.SelectedIndex - 1 >= 0 Then
                listBox.SelectedIndex -= 1
            Else
                listBox.SelectedIndex = 0
            End If

            placingItem = False
            Dim bookmarkIndex As Integer = listBox.SelectedIndex
            Dim displayMember As String = ""

            If mode = CreateMode.Walls Then
                If bookmarkIndex = -1 AndAlso game._map._walls.Count > 0 Then bookmarkIndex = 0
            ElseIf mode = CreateMode.Objects Then
                If bookmarkIndex = -1 AndAlso game._GameObjects.Count > 0 Then bookmarkIndex = 0
            ElseIf mode = CreateMode.Decor Then
                If bookmarkIndex = -1 AndAlso game._map._decor.Count > 0 Then bookmarkIndex = 0
            End If

            Dim bookmarkTopIndex As Integer = listBox.TopIndex
            listBox.DataSource = Nothing
            listBox.DataSource = inputList
            listBox.DisplayMember = displayMember

            If listBox.DataSource IsNot Nothing AndAlso inputList.Count > 0 Then
                listBox.SelectedIndex = bookmarkIndex
                listBox.TopIndex = bookmarkTopIndex
            End If

            SetDisplayMember()
        End Sub

        Private Sub wallsRadioButton_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            If wallsRadioButton.Checked = True Then
                mode = CreateMode.Walls
                SetListBox(game._map._walls, True)
                height.Enabled = width.Enabled = True
            End If
        End Sub

        Private Sub objectsRadioButton_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            If objectsRadioButton.Checked = True Then
                mode = CreateMode.Objects
                SetListBox(game._GameObjects, True)
                objectTypes.Visible = True
                height.Enabled = width.Enabled = False
            Else
                objectTypes.Visible = False
            End If
        End Sub

        Private Sub decorRadioButton_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            If decorRadioButton.Checked = True Then
                mode = CreateMode.Decor
                SetListBox(game._map._decor, True)
                height.Enabled = width.Enabled = False
                imagePath.Visible = True
                imagePathLabel.Visible = True
                loadImageButton.Visible = True
                layerDepthLabel.Visible = True
                layerDepth.Visible = True
                decorSourceXLabel.Visible = True
                decorSourceX.Visible = True
                decorSourceYLabel.Visible = True
                decorSourceY.Visible = True
                decorSourceWidthLabel.Visible = True
                decorSourceWidth.Visible = True
                decorSourceHeightLabel.Visible = True
                decorSourceHeight.Visible = True
                sourceRectangleLabel.Visible = True
            Else
                imagePath.Visible = False
                imagePathLabel.Visible = False
                loadImageButton.Visible = False
                layerDepthLabel.Visible = False
                layerDepth.Visible = False
                decorSourceXLabel.Visible = False
                decorSourceX.Visible = False
                decorSourceYLabel.Visible = False
                decorSourceY.Visible = False
                decorSourceWidthLabel.Visible = False
                decorSourceWidth.Visible = False
                decorSourceHeightLabel.Visible = False
                decorSourceHeight.Visible = False
                sourceRectangleLabel.Visible = False
            End If
        End Sub

        Private Sub removeButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            If listBox.SelectedIndex = -1 Then Return
            Dim savedIndex As Integer = listBox.SelectedIndex

            If mode = CreateMode.Walls Then
                game._map._walls.RemoveAt(listBox.SelectedIndex)
                RefreshListBox(game._map._walls)
            ElseIf mode = CreateMode.Objects AndAlso TypeOf game._GameObjects(listBox.SelectedIndex) Is Player = False Then
                game._GameObjects.RemoveAt(listBox.SelectedIndex)
                RefreshListBox(game._GameObjects)
            ElseIf mode = CreateMode.Decor Then
                game._map._decor.RemoveAt(listBox.SelectedIndex)
                RefreshListBox(game._map._decor)
            End If

            placingItem = False
        End Sub

        Private Sub listBox_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
            If listBox.SelectedIndex = -1 Then Return

            If mode = CreateMode.Walls Then
                Dim selectedWall As Rectangle = game._map._walls(listBox.SelectedIndex)._wall
                xPosition.Value = selectedWall.X
                yPosition.Value = selectedWall.Y
                height.Value = selectedWall.Height
                width.Value = selectedWall.Width
            ElseIf mode = CreateMode.Objects Then
                Dim selectedObject As GameObject = game._GameObjects(listBox.SelectedIndex)
                xPosition.Value = CInt(selectedObject.startPosition.X)
                yPosition.Value = CInt(selectedObject.startPosition.Y)
            ElseIf mode = CreateMode.Decor Then
                Dim selectedDecor As Decor = game._map._decor(listBox.SelectedIndex)
                xPosition.Value = CDec(selectedDecor._position.X)
                yPosition.Value = CDec(selectedDecor._position.Y)
                layerDepth.Value = CDec(selectedDecor._layerDepth)
                imagePath.Text = selectedDecor._imagePath
                decorSourceX.Value = CDec(selectedDecor._sourceRect.X)
                decorSourceY.Value = CDec(selectedDecor._sourceRect.Y)
                decorSourceWidth.Value = CDec(selectedDecor._sourceRect.Width)
                decorSourceHeight.Value = CDec(selectedDecor._sourceRect.Height)
            End If
        End Sub

        Private Sub xPosition_ValueChanged(ByVal sender As Object, ByVal e As EventArgs)
            If listBox.SelectedIndex = -1 Then Return

            If mode = CreateMode.Walls Then
                Dim selectedWall As Rectangle = game._map._walls(listBox.SelectedIndex)._wall
                selectedWall.X = CInt(xPosition.Value)
                game._map._walls(listBox.SelectedIndex)._wall = selectedWall
            ElseIf mode = CreateMode.Objects Then
                game._GameObjects(listBox.SelectedIndex).startPosition.X = CSng(xPosition.Value)
            ElseIf mode = CreateMode.Decor Then
                game._map._decor(listBox.SelectedIndex)._position.X = CSng(xPosition.Value)
            End If
        End Sub

        Private Sub yPosition_ValueChanged(ByVal sender As Object, ByVal e As EventArgs)
            If listBox.SelectedIndex = -1 Then Return

            If mode = CreateMode.Walls Then
                Dim selectedWall As Rectangle = game._map._walls(listBox.SelectedIndex)._wall
                selectedWall.Y = CInt(yPosition.Value)
                game._map._walls(listBox.SelectedIndex)._wall = selectedWall
            ElseIf mode = CreateMode.Objects Then
                game._GameObjects(listBox.SelectedIndex).startPosition.Y = CSng(yPosition.Value)
            ElseIf mode = CreateMode.Decor Then
                game._map._decor(listBox.SelectedIndex)._position.Y = CSng(yPosition.Value)
            End If
        End Sub

        Private Sub width_ValueChanged(ByVal sender As Object, ByVal e As EventArgs)
            If listBox.SelectedIndex = -1 Then Return

            If mode = CreateMode.Walls Then
                Dim selectedWall As Rectangle = game._map._walls(listBox.SelectedIndex)._wall
                selectedWall.Width = CInt(width.Value)
                game._map._walls(listBox.SelectedIndex)._wall = selectedWall
            End If
        End Sub

        Private Sub height_ValueChanged(ByVal sender As Object, ByVal e As EventArgs)
            If listBox.SelectedIndex = -1 Then Return

            If mode = CreateMode.Walls Then
                Dim selectedWall As Rectangle = game._map._walls(listBox.SelectedIndex)._wall
                selectedWall.Height = CInt(height.Value)
                game._map._walls(listBox.SelectedIndex)._wall = selectedWall
            End If
        End Sub

        Private Sub layerDepth_ValueChanged(ByVal sender As Object, ByVal e As EventArgs)
            If listBox.SelectedIndex = -1 Then Return
            game._map._decor(listBox.SelectedIndex)._layerDepth = CSng(layerDepth.Value)
        End Sub

        Private Sub loadImageButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            If listBox.SelectedIndex = -1 Then Return
            Dim openFileDialog1 As OpenFileDialog = New OpenFileDialog()
            openFileDialog1.Filter = "PNG (.png)|*.png"
            openFileDialog1.FilterIndex = 1
            openFileDialog1.Multiselect = False

            If openFileDialog1.ShowDialog() = DialogResult.OK Then

                Try
                    If Directory.Exists("BackupTextures") = False Then Directory.CreateDirectory("BackupTextures")
                    File.Copy(openFileDialog1.FileName, "BackupTextures\" & openFileDialog1.SafeFileName, True)
                    Dim newImage As Texture2D = LoadTextureStream(game.GraphicsDevice, "BackupTextures\" & openFileDialog1.SafeFileName)
                    Dim fileName As String = Path.GetFileNameWithoutExtension(openFileDialog1.SafeFileName)
                    game._map._decor(listBox.SelectedIndex).SetImage(newImage, fileName)
                    SetListBox(game._map._decor, False)
                    FocusGameWindow()
                Catch exception As Exception
                    MessageBox.Show("Error Loading Image: " & exception.Message)
                End Try
            End If
        End Sub

        Private Sub menuStrip_MouseEnter(ByVal sender As Object, ByVal e As EventArgs)
            Focus()
        End Sub

        Private Sub mapWidth_ValueChanged(ByVal sender As Object, ByVal e As EventArgs)
            If mapWidth.Value > mapWidth.Maximum Then mapWidth.Value = mapWidth.Maximum
            game._map._width = CInt(mapWidth.Value)
        End Sub

        Private Sub mapHeight_ValueChanged(ByVal sender As Object, ByVal e As EventArgs)
            If mapHeight.Value > mapHeight.Maximum Then mapHeight.Value = mapHeight.Maximum
            game._map._height = CInt(mapHeight.Value)
        End Sub

        Private Sub resetNPC_Click(ByVal sender As Object, ByVal e As EventArgs)
            ResetGame()
            FocusGameWindow()
        End Sub

        Private Sub paused_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            FocusGameWindow()
        End Sub

        Private Sub newToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            game._GameObjects.Clear()
            game._map._walls.Clear()
            game._map._decor.Clear()
            game._GameObjects.Add(New Player(Vector2.Zero))
            mapWidth.Value = game._map._width = 30
            mapHeight.Value = game._map._height = 17
            savePath = ""

            For i As Integer = 0 To game._GameObjects.Count - 1
                game._GameObjects(i).Load(game.Content)
                game._GameObjects(i).Initialize()
            Next

            ResetEditorList()
        End Sub

        Private Sub Editor_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs)
            game.[Exit]()
        End Sub

        Private Sub decorSourceX_ValueChanged(ByVal sender As Object, ByVal e As EventArgs)
            If listBox.SelectedIndex = -1 Then Return
            game._map._decor(listBox.SelectedIndex)._sourceRect.X = CInt(decorSourceX.Value)
        End Sub

        Private Sub decorSourceY_ValueChanged(ByVal sender As Object, ByVal e As EventArgs)
            If listBox.SelectedIndex = -1 Then Return
            game._map._decor(listBox.SelectedIndex)._sourceRect.Y = CInt(decorSourceY.Value)
        End Sub

        Private Sub decorSourceWidth_ValueChanged(ByVal sender As Object, ByVal e As EventArgs)
            If listBox.SelectedIndex = -1 Then Return
            game._map._decor(listBox.SelectedIndex)._sourceRect.Width = CInt(decorSourceWidth.Value)
        End Sub

        Private Sub decorSourceHeight_ValueChanged(ByVal sender As Object, ByVal e As EventArgs)
            If listBox.SelectedIndex = -1 Then Return
            game._map._decor(listBox.SelectedIndex)._sourceRect.Height = CInt(decorSourceHeight.Value)
        End Sub

        Private Sub noneRadioButton_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            If noneRadioButton.Checked = True Then
                mode = CreateMode.None
                Dim [nothing] As List(Of Integer) = New List(Of Integer)()
                SetListBox([nothing], False)
            End If
        End Sub

        Private Sub FocusGameWindow()
            SetForegroundWindow(gameWinHandle)
        End Sub

        Private Function GameWindowFocused() As Boolean
            Return GetForegroundWindow() = gameWinHandle
        End Function

        Private Sub saveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            SaveAs()
        End Sub

        Private Sub saveToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            Save()
        End Sub

        Private Sub openToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            OpenLevel()
        End Sub

        Private Sub SaveAs()
            Dim saveFileDialog As SaveFileDialog = New SaveFileDialog()
            savePath = ""
            saveFileDialog.Filter = "JORGE (.jorge)|*.jorge"

            Try

                If saveFileDialog.ShowDialog() = DialogResult.OK Then
                    savePath = saveFileDialog.FileName
                    SaveLevel()
                End If

            Catch exception As Exception
                MessageBox.Show("Error Saving: " & exception.Message & " " & exception.InnerException.ToString)
            End Try
        End Sub

        Private Sub Save()
            If savePath = "" Then
                SaveAs()
                Return
            End If

            Try
                SaveLevel()
            Catch exception As Exception
                MessageBox.Show("Error Saving: " & exception.Message & " " & exception.InnerException.ToString)
            End Try
        End Sub

        Private Sub SaveLevel()
            ResetGame()
            Dim levelData As LevelData = New LevelData() With {
                .objects = game._GameObjects,
                .walls = game._map._walls,
                .decor = game._map._decor,
                .mapWidth = game._map._width,
                .mapHeight = game._map._height
            }
            XmlHelper.Save(levelData, savePath)
        End Sub

        Public Sub OpenLevel()
            Dim openFileDialog1 As OpenFileDialog = New OpenFileDialog()
            openFileDialog1.Filter = "JORGE (.jorge)|*.jorge"
            openFileDialog1.Multiselect = False

            If openFileDialog1.ShowDialog() = DialogResult.OK Then

                Try
                    Dim levelData As LevelData = XmlHelper.Load(openFileDialog1.FileName)
                    game._GameObjects = levelData.objects
                    game._map._walls = levelData.walls
                    game._map._decor = levelData.decor
                    mapWidth.Value = game._map._width = levelData.mapWidth
                    mapHeight.Value = game._map._height = levelData.mapHeight
                    LoadLevelContent()
                    If game._GameObjects.Count > 0 Then Camera.LookAt(game._GameObjects(0)._position)
                    ResetEditorList()
                    savePath = ""
                    FocusGameWindow()
                Catch exception As Exception
                    MessageBox.Show("Error Loading: " & exception.Message & " " & exception.InnerException.ToString)
                End Try
            End If
        End Sub

    End Class
End Namespace
