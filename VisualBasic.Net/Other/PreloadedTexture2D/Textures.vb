Imports System.Collections.Generic
Imports Microsoft.Xna.Framework.Content
Imports Microsoft.Xna.Framework.Graphics


Namespace GameEngine
    ''' <summary>
    ''' Must be initialized through New(ContentManager) @ anywhere prior to using
    ''' </summary>
    Public Class Textures

        Shared mTextures As Dictionary(Of String, Texture2D)
        Private Shared TextureLock As New Object
        Private Shared contents As ContentManager

        ''' <summary>
        ''' Initialize at Game.New()
        ''' </summary>
        ''' <param name="con"></param>
        Public Sub New(con As ContentManager)
            If mTextures Is Nothing Then mTextures = New Dictionary(Of String, Texture2D)()
            If contents Is Nothing Then contents = con
        End Sub

        ''' <summary>
        ''' This will keep your Texture2D images preloaded in memory.
        ''' </summary>
        ''' <param name="name"></param>
        ''' <returns> Loaded Texture2D </returns>
        Shared Function Load(name As String) As Texture2D
            SyncLock TextureLock
                Dim locTexture As Texture2D
                If mTextures.ContainsKey(name) Then
                    Return mTextures.Item(name)
                Else
                    locTexture = contents.Load(Of Texture2D)(name)
                    mTextures.Add(name, locTexture)
                    Return locTexture
                End If
            End SyncLock
        End Function

        ''' <summary>
        ''' Warning: Should keep track of the referances that are in use, 
        '''          dont want to prematurely unload an inuse texture.
        ''' </summary>
        ''' <param name="name"></param>
        ''' <returns> if it was unloaded or not </returns>
        Shared Function UnLoad(name As String) As Boolean
            SyncLock TextureLock
                If mTextures.ContainsKey(name) Then
                    Dim result As Texture2D = mTextures.Item(name)
                    mTextures.Remove(name)

                    'Should maybe disregard disposing and leave it
                    'to the GC, incase we still have the texture
                    'loaded somewhere.
                    '   and in that case the return result is
                    '   Return mTextures.ContainsKey(name)
                    result.Dispose()

                    Return result.IsDisposed
                End If
                Return True 'was nothing to unload just send back a yes its dead
            End SyncLock
        End Function

    End Class
End Namespace
