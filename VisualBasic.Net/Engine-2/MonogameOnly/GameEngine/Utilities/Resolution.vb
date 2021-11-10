Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics

Namespace GameEngine
    Public Class Resolution
        Private Shared _Device As GraphicsDeviceManager = Nothing
        Private Shared _Width As Integer = 800
        Private Shared _Height As Integer = 600
        Private Shared _VWidth As Integer = 1024
        Private Shared _VHeight As Integer = 768
        Private Shared _ScaleMatrix As Matrix
        Private Shared _FullScreen As Boolean = False
        Private Shared _dirtyMatrix As Boolean = True
        Private Shared mVirtualViewportX As Integer
        Private Shared mVirtualViewportY As Integer

        Public Shared ReadOnly Property VirtualViewportX As Integer
            Get
                Return mVirtualViewportX
            End Get
        End Property

        Public Shared ReadOnly Property VirtualViewportY As Integer
            Get
                Return mVirtualViewportY
            End Get
        End Property

        Public Shared ReadOnly Property VirtualWidth As Integer
            Get
                Return _VWidth
            End Get
        End Property

        Public Shared ReadOnly Property VirtualHeight As Integer
            Get
                Return _VHeight
            End Get
        End Property

        Shared Sub Init(ByRef device As GraphicsDeviceManager)
            _Width = device.PreferredBackBufferWidth
            _Height = device.PreferredBackBufferHeight
            _Device = device
            _dirtyMatrix = True
            ApplyResolutionSettings()
        End Sub

        Shared Function getTransformationMatrix() As Matrix
            If _dirtyMatrix Then RecreateScaleMatrix()
            Return _ScaleMatrix
        End Function

        Shared Sub SetResolution(ByVal Width As Integer, ByVal Height As Integer, ByVal FullScreen As Boolean)
            _Width = Width
            _Height = Height
            _FullScreen = FullScreen
            ApplyResolutionSettings()
        End Sub

        Shared Sub SetVirtualResolution(ByVal Width As Integer, ByVal Height As Integer)
            _VWidth = Width
            _VHeight = Height
            _dirtyMatrix = True
        End Sub

        Private Shared Sub ApplyResolutionSettings()
            If _FullScreen = False Then

                If (_Width <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width) AndAlso (_Height <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height) Then
                    _Device.PreferredBackBufferWidth = _Width
                    _Device.PreferredBackBufferHeight = _Height
                    _Device.IsFullScreen = _FullScreen
                    _Device.PreferMultiSampling = True
                    _Device.ApplyChanges()
                End If
            Else

                For Each dm As DisplayMode In GraphicsAdapter.DefaultAdapter.SupportedDisplayModes

                    If (dm.Width = _Width) AndAlso (dm.Height = _Height) Then
                        _Device.PreferredBackBufferWidth = _Width
                        _Device.PreferredBackBufferHeight = _Height
                        _Device.IsFullScreen = _FullScreen
                        _Device.PreferMultiSampling = True
                        _Device.ApplyChanges()
                    End If
                Next
            End If

            _dirtyMatrix = True
            _Width = _Device.PreferredBackBufferWidth
            _Height = _Device.PreferredBackBufferHeight
        End Sub

        Shared Sub BeginDraw()
            FullViewport()
            _Device.GraphicsDevice.Clear(Color.Black)
            ResetViewport()
            _Device.GraphicsDevice.Clear(Color.Black)
        End Sub

        Private Shared Sub RecreateScaleMatrix()
            _dirtyMatrix = False
            _ScaleMatrix = Matrix.CreateScale(CSng(_Device.GraphicsDevice.Viewport.Width) / _VWidth, CSng(_Device.GraphicsDevice.Viewport.Width) / _VWidth, 1.0F)
        End Sub

        Shared Sub FullViewport()
            Dim vp As Viewport = New Viewport()
            vp.X = vp.Y = 0
            vp.Width = _Width
            vp.Height = _Height
            _Device.GraphicsDevice.Viewport = vp
        End Sub

        Shared Function getVirtualAspectRatio() As Single
            Return CSng(_VWidth) / CSng(_VHeight)
        End Function

        Shared Sub ResetViewport()
            Dim targetAspectRatio As Single = getVirtualAspectRatio()
            Dim width As Integer = _Device.PreferredBackBufferWidth
            Dim height As Integer = CInt((width / targetAspectRatio + 0.5F))
            Dim changed As Boolean = False

            If height > _Device.PreferredBackBufferHeight Then
                height = _Device.PreferredBackBufferHeight
                width = CInt((height * targetAspectRatio + 0.5F))
                changed = True
            End If

            Dim viewport As Viewport = New Viewport()
            viewport.X = (_Device.PreferredBackBufferWidth / 2) - (width / 2)
            viewport.Y = (_Device.PreferredBackBufferHeight / 2) - (height / 2)
            mVirtualViewportX = viewport.X
            mVirtualViewportY = viewport.Y
            viewport.Width = width
            viewport.Height = height
            viewport.MinDepth = 0
            viewport.MaxDepth = 1

            If changed Then
                _dirtyMatrix = True
            End If

            _Device.GraphicsDevice.Viewport = viewport
        End Sub

    End Class
End Namespace