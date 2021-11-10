Namespace GameEngine
    Module Main

        <STAThread>
        Function Main(ByVal cmdArgs() As String) As Integer
            'MsgBox("The Main procedure is starting the application.")
            Dim returnValue As Integer = 0
            ' See if there are any arguments.
            If cmdArgs.Length > 0 Then
                For argNum As Integer = 0 To UBound(cmdArgs, 1)
                    ' Insert code to examine cmdArgs(argNum) and take
                    ' appropriate action based on its value.
                Next
            End If

            ' Insert call to appropriate starting place in your code.
            Dim mGame As Game1 = New Game1()
            mGame.Run()

#If DEBUG Then
            ' On return, assign appropriate value to returnValue.
            ' 0 usually means successful completion.
            MsgBox("The application is terminating with error level " & CStr(returnValue) & ".")
#End If
            Return returnValue
        End Function

    End Module
End Namespace
