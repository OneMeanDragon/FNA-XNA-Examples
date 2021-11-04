Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Audio
Imports Microsoft.Xna.Framework.Content
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Input
Imports Microsoft.Xna.Framework.Media

Imports System.Xml.Serialization
Imports System.IO

Namespace GameEngine
    Public Class AnimationLoader

        Public Shared Function Load(ByVal name As String) As AnimationData
            Dim serializer As XmlSerializer = New XmlSerializer(GetType(AnimationData))
            Dim reader As TextReader = New StreamReader("Content\Animations\" & name)
            Dim obj As AnimationData = CType(serializer.Deserialize(reader), AnimationData)
            reader.Close()
            Return obj
        End Function

    End Class
End Namespace