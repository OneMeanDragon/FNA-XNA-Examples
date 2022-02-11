Imports System.Collections.Generic
Imports Microsoft.Xna.Framework.Content
Imports Microsoft.Xna.Framework.Graphics


Namespace GameEngine
    ''' <summary>
    ''' Must be initialized through New(ContentManager) @ anywhere prior to using
    ''' </summary>
    Public Class Models

        Shared mTextures As Dictionary(Of String, Model)
        Private Shared TextureLock As New Object
        Private Shared contents As ContentManager

        ''' <summary>
        ''' Initialize at Game.New()
        ''' </summary>
        ''' <param name="con"></param>
        Public Sub New(con As ContentManager)
            If mTextures Is Nothing Then mTextures = New Dictionary(Of String, Model)()
            If contents Is Nothing Then contents = con
        End Sub

        ''' <summary>
        ''' This will keep your Model images preloaded in memory.
        ''' </summary>
        ''' <param name="name"></param>
        ''' <returns> Loaded Model </returns>
        Shared Function Load(name As String) As Model
            SyncLock TextureLock
                Dim locTexture As Model
                If mTextures.ContainsKey(name) Then
                    Return mTextures.Item(name)
                Else
                    locTexture = contents.Load(Of Model)(name)
                    mTextures.Add(name, locTexture)
                    Return locTexture
                End If
            End SyncLock
        End Function

    End Class

End Namespace
