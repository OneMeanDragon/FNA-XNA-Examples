Imports Microsoft.Xna.Framework
Imports System
Imports System.Runtime.CompilerServices

Namespace GameEngine
    Class Maf
        Public Const PI As Single = 3.14159274F
        Public Const RADIANS_1 As Single = (CSng((CDbl(3.1415926536) / CDbl(180.0))))
        Public Const RADIANS_2 As Single = RADIANS_1 * 2.0F
        Public Const RADIANS_3 As Single = RADIANS_1 * 3.0F
        Public Const RADIANS_4 As Single = RADIANS_1 * 4.0F
        Public Const RADIANS_5 As Single = RADIANS_1 * 5.0F
        Public Const RADIANS_6 As Single = RADIANS_1 * 6.0F
        Public Const RADIANS_10 As Single = RADIANS_1 * 10.0F
        Public Const RADIANS_90 As Single = RADIANS_1 * 90.0F
        Public Const RADIANS_180 As Single = RADIANS_1 * 180.0F
        Public Const RADIANS_270 As Single = RADIANS_1 * 270.0F
        Public Const RADIANS_360 As Single = RADIANS_1 * 360.0F
        Public Const RADIANS_HALF As Single = RADIANS_1 / 2.0F
        Public Const RADIANS_QUARTER As Single = RADIANS_1 / 4.0F
        Public Const EPSILON As Single = 0.0001F

        Public Function Calculate2DAngleFromZero(ByVal x As Single, ByVal y As Single) As Single
            If (x = 0.0F) AndAlso (y = 0.0F) Then Return 0.0F

            If x > 0.0F Then
                If x < EPSILON Then x = EPSILON

                If y > 0.0F Then
                    If y < EPSILON Then y = EPSILON
                    Return CSng(Math.Atan(CDbl((y / x))))
                Else
                    If y > -EPSILON Then y = -EPSILON
                    Return (RADIANS_270 + CSng(Math.Atan(CDbl((x / (-y))))))
                End If
            Else
                If x > -EPSILON Then x = -EPSILON

                If y > 0.0F Then
                    If y < EPSILON Then y = EPSILON
                    Return (RADIANS_90 + CSng(Math.Atan(CDbl(((-x) / y)))))
                Else
                    If y > -EPSILON Then y = -EPSILON
                    Return (RADIANS_180 + CSng(Math.Atan(CDbl((y / x)))))
                End If
            End If
        End Function

        Public Function ClampAngle(ByVal angle As Single) As Single
            While angle > Maf.RADIANS_360
                angle -= Maf.RADIANS_360
            End While

            While angle < 0
                angle += Maf.RADIANS_360
            End While

            Return angle
        End Function
    End Class

    Module MafExtensions
        <Extension()>
        Function ToAngle(ByVal vector As Vector2) As Single
            Return CSng(Math.Atan2(vector.Y, vector.X))
        End Function
    End Module
End Namespace