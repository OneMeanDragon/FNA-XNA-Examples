Imports System
Imports System.Linq
Imports System.Collections.Generic
Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Content
Imports Microsoft.Xna.Framework.Graphics

Namespace GameEngine
    Public Module ConstantValues
        Public Const SIZEOF_FLOAT As Integer = 4
        Public Const SIZEOF_SHORT As Integer = 2
    End Module

    Public Class Basic3DObjects
        Public Class Obj3D
            Public start_index As Integer
            Public triangle_count As Integer
            Public source_rect As Rectangle
            Public tex As Texture2D
            Public rot As Vector3
            Public pos As Vector3
            Public transform As Matrix

            ' IF POSITION CHANGES (and-or rotation: )
            Public Sub UpdateTransform()
                If rot = Vector3.Zero Then
                    transform = Matrix.CreateTranslation(pos)
                Else
                    transform = Matrix.CreateFromYawPitchRoll(rot.Y, rot.X, rot.Z) * Matrix.CreateTranslation(pos)
                End If
            End Sub

            ' INIT (common action for all objects)
            Protected Sub Init(position As Vector3, infile As String)
                start_index = ibuf_start
                pos = position : transform = Matrix.CreateTranslation(pos)
                tex = LoadTexture(infile)
                If (source_rect.Width < 1) OrElse (source_rect.Height < 1) Then
                    source_rect = New Rectangle(0, 0, tex.Width, tex.Height)
                End If
            End Sub

            ' GET UV COORDS
            Protected Sub GetUVCoords(ByRef u1 As Single, ByRef v1 As Single, ByRef u2 As Single, ByRef v2 As Single)
                u1 = source_rect.X \ tex.Width
                v1 = source_rect.Y \ tex.Height
                u2 = (source_rect.X + source_rect.Width) \ tex.Width
                v2 = (source_rect.Y + source_rect.Height) \ tex.Height
            End Sub

            ' ADD QUAD
            Public Sub AddQuad(position As Vector3, width As Single, height As Single, rotation As Vector3,
                               texturefile As String, sourceRect? As Rectangle)
                If sourceRect.HasValue Then
                    source_rect = sourceRect.Value
                End If
                Init(pos, texturefile) : rot = rotation : UpdateTransform()
                Dim u1 As Single = 0, v1 As Single = 0, u2 As Single = 1, v2 As Single = 1, hw As Single = width \ 2, hl As Single = height \ 2
                GetUVCoords(u1, v1, u2, v2)
                Dim norm As Vector3 = Vector3.Up
                Dim y = pos.Y, l = -hw + pos.X, r = hw + pos.X, n = -hl + pos.Z, f = hl + pos.Z
                AddVertex(l, y, f, norm, u1, v1) : AddVertex(r, y, f, norm, u2, v1) : AddVertex(r, y, n, norm, u2, v2)
                AddVertex(l, y, n, norm, u1, v2)
                AddTriangle(0, 1, 2) : triangle_count += 1
                AddTriangle(2, 3, 0) : triangle_count += 1
                vertexbuffer.SetData(Of VertexPositionNormalTexture)(vbuf_start * vbytes, verts, 0, v_cnt, vbytes) : vbuf_start = v_cnt : v_cnt = 0
                indexbuffer.SetData(Of UShort)(ibuf_start * ibytes, indices, 0, i_cnt) : ibuf_start = i_cnt : i_cnt = 0
            End Sub

            ' ADD CUBE
            Public Sub AddCube(position As Vector3, size As Vector3, rotation As Vector3, texturefile As String, sourceRect? As Rectangle)
                If sourceRect.HasValue Then
                    source_rect = sourceRect
                End If
                Init(pos, texturefile) : rot = rotation : UpdateTransform()
                Dim u1 As Single = 0, v1 As Single = 0, u2 As Single = 1, v2 As Single = 1, hw As Single = size.X \ 2, hl As Single = size.Z \ 2, hh As Single = size.Y \ 2
                GetUVCoords(u1, v1, u2, v2)
                Dim t = pos.Y - hh, b = pos.Y + hh, l = pos.X - hw, r = pos.X + hw, n = pos.Z - hl, f = pos.Z + hl
                Dim norm As Vector3 = Vector3.Up : AddVertex(l, t, f, norm, u1, v1) : AddVertex(r, t, f, norm, u2, v1) : AddVertex(r, t, n, norm, u2, v2) : AddVertex(l, t, n, norm, u1, v2)
                norm = Vector3.Right : AddVertex(r, b, f, norm, u1, v1) : AddVertex(r, b, n, norm, u1, v2)
                norm = Vector3.Down : AddVertex(l, b, f, norm, u2, v1) : AddVertex(l, b, n, norm, u2, v2)
                norm = Vector3.Backward : AddVertex(l, t, n, norm, u1, v1) : AddVertex(r, t, n, norm, u2, v1) : AddVertex(r, b, n, norm, u2, v2) : AddVertex(l, b, n, norm, u1, v2)
                norm = Vector3.Forward : AddVertex(r, t, f, norm, u1, v1) : AddVertex(l, t, f, norm, u2, v1) : AddVertex(l, b, f, norm, u2, v2) : AddVertex(r, b, f, norm, u1, v2)
                AddTriangle(0, 1, 2) : triangle_count += 1 : AddTriangle(2, 3, 0) : triangle_count += 1
                AddTriangle(2, 1, 4) : triangle_count += 1 : AddTriangle(4, 5, 2) : triangle_count += 1
                AddTriangle(5, 4, 6) : triangle_count += 1 : AddTriangle(6, 7, 5) : triangle_count += 1
                AddTriangle(7, 6, 0) : triangle_count += 1 : AddTriangle(0, 3, 7) : triangle_count += 1
                AddTriangle(8, 9, 10) : triangle_count += 1 : AddTriangle(10, 11, 8) : triangle_count += 1
                AddTriangle(12, 13, 14) : triangle_count += 1 : AddTriangle(14, 15, 12) : triangle_count += 1
                vertexbuffer.SetData(Of VertexPositionNormalTexture)(vbuf_start * vbytes, verts, 0, v_cnt, vbytes) : vbuf_start = v_cnt : v_cnt = 0
                indexbuffer.SetData(Of UShort)(ibuf_start * ibytes, indices, 0, i_cnt) : ibuf_start = i_cnt : i_cnt = 0
            End Sub

            Public Sub UpdateTransformQuaternion()
                If rot = Vector3.Zero Then
                    transform = Matrix.CreateTranslation(pos)
                Else
                    Dim qR As Quaternion = Quaternion.CreateFromYawPitchRoll(rot.Y, rot.X, rot.Z)
                    transform = Matrix.CreateFromQuaternion(qR) * Matrix.CreateTranslation(pos)
                End If
            End Sub

        End Class

        ' COMMON
        Private gpu As GraphicsDevice
        ' light as LIGHT ' <- add later
        Private basic_effect As BasicEffect
        Private upDirection As Vector3
        Const vbytes = SIZEOF_FLOAT * 8, ibytes As Integer = SIZEOF_SHORT

        ' GEO
        Private world As Matrix
        Public objex As List(Of Obj3D)
        ' static private allows nested classes to access: (yea we'll see)
        Shared contents As ContentManager
        Shared indexbuffer As IndexBuffer
        Shared vertexbuffer As VertexBuffer
        Shared indices(0) As UShort
        Shared verts(0) As VertexPositionNormalTexture
        Shared i_cnt As Integer = 0
        Shared ibuf_start As Integer = 0
        Shared v_cnt As Integer = 0
        Shared vbuf_start As Integer = 0
        Shared textures As Dictionary(Of String, Texture2D)

        Public Sub New(myGPU As GraphicsDevice, Up_Direction As Vector3, con As ContentManager)
            gpu = myGPU
            world = Matrix.Identity
            basic_effect = New BasicEffect(gpu) ' Light = new_light
            'verts() = New VertexPositionNormalTexture()(65535)
            'indices(0 To 65535) = New UShort
            Array.Resize(Of UShort)(indices, 65535)
            Array.Resize(Of VertexPositionNormalTexture)(verts, 65535)
            contents = con
            upDirection = Up_Direction
            textures = New Dictionary(Of String, Texture2D)()
            vertexbuffer = New VertexBuffer(gpu, GetType(VertexPositionNormalTexture), 65535, BufferUsage.WriteOnly)
            indexbuffer = New IndexBuffer(gpu, GetType(UShort), 65535, BufferUsage.WriteOnly)
            objex = New List(Of Obj3D)()

            ' INIT
            With basic_effect
                .Alpha = 1.0F
                .LightingEnabled = True
                .AmbientLightColor = New Vector3(0.1F, 0.2F, 0.3F)
                .DiffuseColor = New Vector3(0.94F, 0.94F, 0.94F)
                .EnableDefaultLighting()
                .TextureEnabled = True
            End With
        End Sub

        ' ADD VERTEX
        Shared Sub AddVertex(x As Single, y As Single, z As Single, norm As Vector3, u As Single, v As Single)
            If (vbuf_start + v_cnt) > 65535 Then
                Console.WriteLine("Exceeded vertex buffer size")
                Return
            End If
            verts(v_cnt) = New VertexPositionNormalTexture(New Vector3(x, y, z), norm, New Vector2(u, v))
            v_cnt += 1
        End Sub

        ' ADD TRIANGLE
        Shared Sub AddTriangle(a As UShort, b As UShort, c As UShort)
            If (ibuf_start + 3) > 65535 Then
                Console.WriteLine("Exceeded index buffer size [may need uint32 type]")
                Return
            End If
            Dim offset As UShort = UShort.Parse(vbuf_start)
            a += offset : b += offset : c += offset
            indices(i_cnt) = a : i_cnt += 1 : indices(i_cnt) = b : i_cnt += 1 : indices(i_cnt) = c : i_cnt += 1
        End Sub

        Shared Function LoadTexture(name As String) As Texture2D
            Dim locTexture As Texture2D
            If textures.ContainsKey(name) Then 'textures.TryGetValue(name, locTexture)
                Return textures.Item(name) 'Return locTexture
            Else
                locTexture = contents.Load(Of Texture2D)(name)
                textures.Add(name, locTexture)
                Return locTexture
            End If
        End Function

        ' Basic Floor
        Public Sub AddFloor(width As Single, length As Single, mid_position As Vector3, rotation As Vector3, texturefile As String, sourceRect? As Rectangle)
            Dim obj As Obj3D = New Obj3D()
            obj.AddQuad(mid_position, width, length, rotation, texturefile, sourceRect)
            objex.Add(obj)
        End Sub

        ' Basic Cube
        Public Sub AddCube(width As Single, length As Single, height As Single, mid_position As Vector3, rotation As Vector3, texturefile As String, sourceRect? As Rectangle)
            Dim obj As Obj3D = New Obj3D()
            obj.AddCube(mid_position, New Vector3(width, height, length), rotation, texturefile, sourceRect)
            objex.Add(obj)
        End Sub

        ' Draw
        Public Sub Draw(cam As Camera)
            gpu.SetVertexBuffer(vertexbuffer)
            gpu.Indices = indexbuffer
            Dim obj_cnt = objex.Count, o As Integer
            o = 0 : While (o < obj_cnt)
                Dim ob As Obj3D = objex(o)
                ' [reminder to set lighting draw params later for custom lighting class and effect
                ' SET SHADER PARAMETERS:
                With basic_effect
                    .Texture = ob.tex
                    .World = ob.transform
                    .View = cam.view
                    .Projection = cam.proj
                End With
                For Each pass In basic_effect.CurrentTechnique.Passes
                    pass.Apply()
                    gpu.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, vertexbuffer.VertexCount, ob.start_index, ob.triangle_count) : o += 1
                Next
            End While
        End Sub

    End Class
End Namespace