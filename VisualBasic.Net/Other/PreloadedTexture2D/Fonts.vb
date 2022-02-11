Imports System.Collections.Generic
Imports Microsoft.Xna.Framework.Content
Imports Microsoft.Xna.Framework.Graphics


Namespace GameEngine

    Public Class Fonts

        Shared mTextures As Dictionary(Of String, SpriteFont)
        Private Shared TextureLock As New Object
        Private Shared contents As ContentManager

        ''' <summary>
        ''' Initialize at Game.New()
        ''' </summary>
        ''' <param name="con"></param>
        Public Sub New(con As ContentManager)
            If mTextures Is Nothing Then mTextures = New Dictionary(Of String, SpriteFont)()
            If contents Is Nothing Then contents = con
        End Sub

        ''' <summary>
        ''' This will keep your SpriteFont images preloaded in memory.
        ''' </summary>
        ''' <param name="name"></param>
        ''' <returns> Loaded SpriteFont </returns>
        Shared Function Load(name As String) As SpriteFont
            SyncLock TextureLock
                Dim locTexture As SpriteFont
                If mTextures.ContainsKey(name) Then
                    Return mTextures.Item(name)
                Else
                    locTexture = contents.Load(Of SpriteFont)(name)
                    mTextures.Add(name, locTexture)
                    Return locTexture
                End If
            End SyncLock
        End Function

    End Class

End Namespace
