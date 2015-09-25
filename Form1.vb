Imports System.Net
Imports System.Net.Sockets
Imports System.IO
Imports System.Threading
Imports System.Text

Public Class Form1

    Dim tcpclient As TcpClient
    Dim netstream As NetworkStream

    Private ReadOnly CHUNK_SIZE As Integer = 1024

    Private ReadOnly START_MESSAGE As Char = Chr(System.Convert.ToUInt32("B", 16))
    Private ReadOnly END_MESSAGE As Char = Chr(System.Convert.ToUInt32("1C", 16))
    Private ReadOnly CR As Char = Chr(13)

    'Connetti
    Private Sub BtnConn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnConn.Click

        tcpclient = New TcpClient

        Try
            tcpclient.Connect(hostname.Text, CInt(porta.Text))
            netstream = tcpclient.GetStream()

        Catch ex As Exception
            '            Console.WriteLine("couldnt connect to IP Address")
        End Try

        Status1.Text = "Client " + IIf(tcpclient.Connected, "connesso", "non connesso")

        Timer1.Enabled = tcpclient.Connected
        Disconnetti.Enabled = tcpclient.Connected
        Invio.Enabled = tcpclient.Connected

    End Sub

    'Disconnetti
    Private Sub Disconnetti_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Disconnetti.Click
        Timer1.Enabled = False
        Disconnetti.Enabled = False
        Invio.Enabled = False

        If (Not tcpclient Is Nothing) Then
            tcpclient.Close()
            netstream.Close()
        End If

        tcpclient = Nothing
        Status1.Text = "Client diconnesso"

    End Sub

    'Invia
    Private Sub Invio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Invio.Click

        If Not IO.File.Exists(FilePathToSend.Text) Then
            'se il file non esiste viene avvertito l'utente e si esce dalla procedura
            MessageBox.Show("Il file non esiste, inserire il percorso di un file esistente", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Disconnetti.Enabled = False

        execute()

        Disconnetti.Enabled = True


    End Sub

    Private Function getAnswer() As String
        ''risposta del server
        'If netstream.CanRead Then
        '    ' Reads the NetworkStream into a byte buffer.
        '    Dim readBytes(tcpclient.ReceiveBufferSize) As Byte
        '    netstream.Read(readBytes, 0, CInt(tcpclient.ReceiveBufferSize))

        '    ' Returns the data received from the host to the console.
        '    Dim returndata As String = Encoding.ASCII.GetString(readBytes)
        '    '            Console.WriteLine(("This is what the host returned to you: " + returndata))
        'Else
        '    Console.WriteLine("You cannot read data from this stream.")
        'End If

        Dim returndata As String = ""
        If netstream.CanRead Then

            ' Reads the NetworkStream into a byte buffer.
            Dim bytes(tcpclient.ReceiveBufferSize) As Byte
            ' Read can return anything from 0 to numBytesToRead. This method blocks until at least one byte is read.
            netstream.Read(bytes, 0, CInt(tcpclient.ReceiveBufferSize))

            'la risposta bytes è nel formato hl7 ed andrebbe ripulita

            returndata = Encoding.ASCII.GetString(bytes)    ' Returns the data received from the host to the console.
            'Else
            '    'Console.WriteLine("You cannot read data from this stream.")
            '    'tcpclient.Close()
            '    ''Closing the tcpClient instance does not close the network stream.
            '    'netstream.Close()
            '    Return ""
        End If

        Return returndata

    End Function

    Private Sub Browse1_Click(ByVal sen As System.Object, ByVal e As System.EventArgs) Handles Browse1.Click
        Dim Open As New OpenFileDialog
        Open.Filter = "file hl7|*.hl7|Tutti i file|*.*"
        Open.Multiselect = False
        'vine creata una OpenFileDialog che permetterà all'utente di scegliere il file
        'la proprietà multiselect impedisce o permette che l'utente scelga più file 
        If Open.ShowDialog = Windows.Forms.DialogResult.OK Then
            FilePathToSend.Text = Open.FileName
            'quando l'utente ha scelto il file il percorso viene mostrato nella textbox
        End If
    End Sub

    Private Sub Slplit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Split.Click
        If Not IO.File.Exists(FilePathToSend.Text) Then
            'se il file non esiste viene avvertito l'utente e si esce dalla procedura
            MessageBox.Show("Il file non esiste, inserire il percorso di un file esistente", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        'Dim data() As Byte = File.ReadAllBytes(FilePathToSend.Text)

        Dim bytes() As Byte = bytesFromFile(FilePathToSend.Text)

    End Sub

    'Private Sub DumpBytes(ByVal bdata() As Byte, ByVal len As Integer)

    '    Dim i As Integer
    '    Dim j As Integer = 0
    '    Dim dchar As Char
    '    ' 3 * 16 chars for hex display, 16 chars for text and 8 chars
    '    ' for the 'gutter' int the middle.
    '    Dim dumptext As New StringBuilder("        ", 16 * 4 + 8)
    '    For i = 0 To len - 1
    '        dumptext.Insert(j * 3, String.Format("{0:X2} ", CType(bdata(i), Integer)))
    '        dchar = Convert.ToChar(bdata(i))
    '        ' replace 'non-printable' chars with a '.'.
    '        If Char.IsWhiteSpace(dchar) Or Char.IsControl(dchar) Then
    '            dchar = "."
    '        End If
    '        dumptext.Append(dchar)
    '        j += 1
    '        If j = 16 Then
    '            Console.WriteLine(dumptext)
    '            dumptext.Length = 0
    '            dumptext.Append("        ")
    '            j = 0
    '        End If
    '    Next i
    '    ' display the remaining line
    '    If j > 0 Then
    '        ' add blank hex spots to align the 'gutter'.
    '        For i = j To 15
    '            dumptext.Insert(j * 3, "   ")
    '        Next i
    '        Console.WriteLine(dumptext)
    '    End If

    'End Sub

    Private Function bytesFromFile(ByVal filePath As String) As Byte()

        Return File.ReadAllBytes(filePath)

    End Function


    'spedisce la stringa passat
    Private Function send(ByVal msgToSend As String) As Boolean

        Dim retValue As Boolean = True
        Dim bytes As Byte() = System.Text.Encoding.ASCII.GetBytes(msgToSend)

        Try
            netstream.Write(bytes, 0, msgToSend.Length)
            netstream.Flush()
        Catch ex As Exception
            Status1.Text = "Collegamento con il server interrotto"
            retValue = False
        End Try

        Return retValue
    End Function


    'codifica la stringa secondo il formato di un messaggio hl7
    Private Function hl7Encode(ByVal readText As String()) As String

        Dim message As String = ""
        For counter As Integer = 0 To readText.Length - 1
            message += IIf(counter = 0, START_MESSAGE, "") + readText(counter) + IIf(counter = readText.Length - 1, END_MESSAGE + CR, CR)
        Next

        'hl7Encode = message
        Return message

    End Function

    'legge il file, crea i gruppi di messaggi con dentro le righe del singolo messaggio, invia il messaggio e gestisce la risposta
    Private Sub execute()

        Dim readText() As String = File.ReadAllLines(FilePathToSend.Text)


        Dim information = My.Computer.FileSystem.GetFileInfo(FilePathToSend.Text)
        msgBox("")

        'msgBox("The file's full name is " & information.FullName & ".")
        'msgBox("Last access time is " & information.LastAccessTime & ".")
        'msgBox("The length is " & information.Length & ".")



        'ciclare su readText e fare un invio x ogni gruppo separato da un record vuoto
        Dim groups As ArrayList = New ArrayList()
        Dim lines As ArrayList = Nothing

        For Each row As String In readText

            If (row.Length > 0) Then
                If (lines Is Nothing) Then
                    lines = New ArrayList()
                    groups.Add(lines)
                End If
                lines.Add(row)
            Else
                lines = Nothing
            End If
        Next

        For Each lines In groups
            Dim msgToSend As String() = lines.ToArray(GetType(String))
            Dim ok As Boolean = send(hl7Encode(msgToSend))
            Dim answer As String = Nothing
            If (ok) Then
                answer = getAnswer()    'risposta dal server
                answer = answer.Replace(CR, vbCrLf).Replace(START_MESSAGE, "").Replace(END_MESSAGE, "") 'la risposta è nel formato hl7 e quindi va pulita
                'se a causa della risposta c'è un esito negativo, il messaggio va comunque trattato come garbage
            End If

            If (ok) Then
                FConsole.Text += answer
            Else
                'scrive tutte le righe lines sul file garbage. Le righe sono precedute da un record vuoto dal secondo messaggio in poi
            End If

        Next
    End Sub

End Class

