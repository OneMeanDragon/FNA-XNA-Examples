Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.IO
Imports System.Xml.Serialization

Namespace GameEngine
    Public MustInherit Class XmlHelper
        Public Shared Function Load(ByVal fileName As String) As LevelData
            Dim serializer As XmlSerializer = New XmlSerializer(GetType(LevelData))
            Dim reader As TextReader = New StreamReader(fileName)
            Dim obj As LevelData = CType(serializer.Deserialize(reader), LevelData)
            reader.Close()
            Return obj
        End Function

        Public Shared Sub Save(Of T)(ByVal obj As T, ByVal fileName As String)
            Dim writer As TextWriter = New StreamWriter(fileName)
            Dim serializer As XmlSerializer = New XmlSerializer(GetType(T))
            serializer.Serialize(writer, obj)
            writer.Close()
        End Sub
    End Class
End Namespace