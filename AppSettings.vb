
Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Security.Permissions

' Store and retrieve application settings.
Public Class AppSettings

    Const fileName As String = "AppSettings#@@#.dat"
    Dim aspRatio As Single
    Dim lkupDir As String
    Dim saveTime As Integer
    Dim statusBar As Boolean

    Sub New()

        ' Create default application settings.
        aspRatio = 1.3333
        lkupDir = "C:\AppDirectory"
        saveTime = 30
        statusBar = False

        If File.Exists(fileName) Then
            Dim binReader As New BinaryReader( _
                File.Open(fileName, FileMode.Open))
            Try

                ' If the file is not empty,
                ' read the application settings.
                ' First read 4 bytes into a buffer to 
                ' determine if the file is empty.
                Dim testArray As Byte() = {0, 0, 0, 0}
                Dim count As Integer = binReader.Read(testArray, 0, 3)

                If count <> 0 Then

                    ' Reset the position in the stream to zero.
                    binReader.BaseStream.Seek(0, SeekOrigin.Begin)

                    aspRatio = binReader.ReadSingle()
                    lkupDir = binReader.ReadString()
                    saveTime = binReader.ReadInt32()
                    statusBar = binReader.ReadBoolean()
                    Return
                End If

                ' If the end of the stream is reached before reading
                ' the four data values, ignore the error and use the
                ' default settings for the remaining values.
            Catch ex As EndOfStreamException
                Console.WriteLine("{0} caught and ignored. " & _
                    "Using default values.", ex.GetType().Name)
            Finally
                binReader.Close()
            End Try
        End If

    End Sub

    ' Create a file and store the application settings.
    Sub Close()
        Dim binWriter As New BinaryWriter( _
            File.Open(fileName, FileMode.Create))
        Try
            binWriter.Write(aspRatio)
            binWriter.Write(lkupDir)
            binWriter.Write(saveTime)
            binWriter.Write(statusBar)
        Finally
            binWriter.Close()
        End Try
    End Sub

    Property AspectRatio As Single
        Get
            Return aspRatio
        End Get
        Set(ByVal value As Single)
            aspRatio = Value
        End Set
    End Property

    Property LookupDir As String
        Get
            Return lkupDir
        End Get
        Set(ByVal value As String)
            lkupDir = Value
        End Set
    End Property

    Property AutoSaveTime As Integer
        Get
            Return saveTime
        End Get
        Set(ByVal value As Integer)
            saveTime = Value
        End Set
    End Property

    Property ShowStatusBar As Boolean
        Get
            Return statusBar
        End Get
        Set(ByVal value As Boolean)
            statusBar = Value
        End Set
    End Property

End Class

Public Class Test
    Shared Sub Main()
        ' Load application settings.
        Dim appSettings As New AppSettings()
        Console.WriteLine("App settings:" & vbcrLf & "Aspect " & _
            "Ratio: {0}, Lookup directory: {1}," & vbcrLf & "Auto " & _
            "save time: {2} minutes, Show status bar: {3}" & vbCrLf, _
            New Object(3) {appSettings.AspectRatio.ToString(), _
            appSettings.LookupDir, _
            appSettings.AutoSaveTime.ToString(), _
            appSettings.ShowStatusBar.ToString()})

        ' Change the settings.
        appSettings.AspectRatio = 1.25
        appSettings.LookupDir = "C:\Temp"
        appSettings.AutoSaveTime = 10
        appSettings.ShowStatusBar = True

        ' Save the new settings.
        appSettings.Close()

    End Sub
End Class