Imports System
Imports System.Linq
Imports System.Collections.Generic
Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Content
Imports Microsoft.Xna.Framework.Graphics
Imports System.Runtime.CompilerServices

Namespace GameEngine
    Public Module Extensions
        <Extension()>
        Public Function GetIndex(ByVal vmodel As Model, ByVal mesh_name As String) As Integer
            Dim index As Integer = 0
            For Each mesh As ModelMesh In vmodel.Meshes
                For Each effect As BasicEffect In mesh.Effects
                    If mesh.Name = mesh_name Then
                        Return index
                    End If
                    index += 1
                Next
            Next
            Console.WriteLine("Mesh named: " & mesh_name & " was not found")
            Return 0
        End Function

        ' From the point of view of someone looking down on the map (rotate player to face angle of movement)
        <Extension()>
        Public Function ToAngleFlipZ(ByVal vec As Vector2) As Single
            Return Math.Atan2(-vec.Y, vec.X)
        End Function

    End Module
End Namespace