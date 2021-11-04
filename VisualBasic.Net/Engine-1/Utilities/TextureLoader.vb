Imports System
Imports System.Collections.Generic
Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Content
Imports Microsoft.Xna.Framework.Graphics

Namespace GameEngine
    Public Class TextureLoader
        Const usingPipeline As Boolean = False

        Public Shared Function Load(ByVal vFilePath As String, ByVal vContent As ContentManager) As Texture2D
            Dim image As Texture2D = vContent.Load(Of Texture2D)(vFilePath)
            If usingPipeline = False Then PremultiplyTexture(image)
            Return image
        End Function

        Private Shared Sub PremultiplyTexture(ByVal vTexture As Texture2D)
            Dim buffer As Color() = New Color(vTexture.Width * vTexture.Height - 1) {}
            vTexture.GetData(buffer)

            For i As Integer = 0 To buffer.Length - 1
                buffer(i) = Color.FromNonPremultiplied(buffer(i).R, buffer(i).G, buffer(i).B, buffer(i).A)
            Next

            vTexture.SetData(buffer)
        End Sub
    End Class
End Namespace
