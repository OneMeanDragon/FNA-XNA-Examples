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

    Public Class LevelData
        <XmlElement("Player", Type:=GetType(Player))>
        <XmlElement("Enemy", Type:=GetType(Enemy))>
        <XmlElement("PowerUp", Type:=GetType(PowerUp))>
        Public Property [objects] As List(Of GameObject)
        Public Property [walls] As List(Of Wall)
        Public Property [decor] As List(Of Decor)
        Public Property [mapWidth] As Integer
        Public Property [mapHeight] As Integer
    End Class

End Namespace