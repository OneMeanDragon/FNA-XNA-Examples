Imports System.Collections.Generic

Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Audio
Imports Microsoft.Xna.Framework.Content
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Input
Imports Microsoft.Xna.Framework.Media

Namespace GameEngine
    Public Class GameHUD

        Private _font As SpriteFont

        Public Sub Load(vContent As ContentManager)
            _font = vContent.Load(Of SpriteFont)("Fonts\\Arial")
        End Sub

        Public Sub Draw(vSpriteBatch As SpriteBatch)
            vSpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, Nothing, Resolution.getTransformationMatrix())
            vSpriteBatch.DrawString(_font, "Score: " & Player._score.ToString(), Vector2.Zero, Color.White)
            vSpriteBatch.End()
        End Sub

    End Class
End Namespace