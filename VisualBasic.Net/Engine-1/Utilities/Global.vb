Imports System
Imports System.Collections.Generic
Imports Microsoft.Xna.Framework

Namespace GameEngine
    Public Class Globals

        Public Shared [game] As Game1
        Public Shared random As Random = New Random()
        Public Shared levelName As String

        Shared Sub Initialize(ByVal inputGame As Game1)
            [game] = inputGame
        End Sub



    End Class
End Namespace
