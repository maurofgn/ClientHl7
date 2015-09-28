Imports System.Net
Imports System.Net.Sockets
Imports System.IO
Imports System.Threading
Imports System.Text
Imports System.Security

Public Class Form1

    Dim tcpclient As TcpClient
    Dim netstream As NetworkStream
    Dim fileExist As Boolean = False
    Dim tcpConnected As Boolean = False

    Private ReadOnly CHUNK_SIZE As Integer = 1024

    Private ReadOnly START_MESSAGE As Char = Chr(System.Convert.ToUInt32("B", 16))
    Private ReadOnly END_MESSAGE As Char = Chr(System.Convert.ToUInt32("1C", 16))
    Private ReadOnly CR As Char = Chr(13)

    'Connetti
    Private Sub BtnConn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnConn.Click

        tcpConnect()

        Status1.Text = "Client " + IIf(tcpclient.Connected, "connesso", "non connesso")
        enableButton()

    End Sub

    'Disconnetti
    Private Sub Disconnetti_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Disconnetti.Click

        tcpDisconnect()
        enableButton()
        Status1.Text = "Client diconnesso"

    End Sub

    'Invia
    Private Sub Invio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Invio.Click

        If Not IO.File.Exists(FilePathToSend.Text) Then
            'se il file non esiste viene avvertito l'utente e si esce dalla procedura
            MessageBox.Show("Il file non esiste, inserire il percorso di un file esistente", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        execute(FilePathToSend.Text)
        enableButton()

    End Sub

    Private Sub FilePathToSend_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FilePathToSend.TextChanged

        Dim information As FileInfo

        fileExist = False
        Try
            information = My.Computer.FileSystem.GetFileInfo(FilePathToSend.Text)
            fileExist = information.Exists
        Catch ex As ArgumentException
            Status1.Text = "formato del nome del percorso non è corretto"
        Catch ex As NotSupportedException
            Status1.Text = "Nel percorso sono presenti i due punti a metà della stringa"
        Catch ex As PathTooLongException
            Status1.Text = "Percorso troppo lungo"
        Catch ex As SecurityException
            Status1.Text = "non si dispone delle autorizzazioni necessarie"
        End Try


        Invio.Enabled = tcpConnected And fileExist

    End Sub

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

    'Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
    '    If netstream.DataAvailable = True Then
    '        Dim stringar As String = Nothing
    '        Dim bytes(tcpclient.ReceiveBufferSize()) As Byte
    '        netstream.Read(bytes, 0, bytes.Length)

    '        stringar += System.Text.Encoding.ASCII.GetString(bytes)
    '        FConsole.Text += stringar & vbCrLf
    '    End If
    'End Sub

    'spedisce la stringa passat


    Private Function getAnswer() As String
        'risposta del server
        Dim returndata As String = ""
        If netstream.CanRead Then

            ' Reads the NetworkStream into a byte buffer.
            Dim bytes(tcpclient.ReceiveBufferSize) As Byte
            ' Read can return anything from 0 to numBytesToRead. This method blocks until at least one byte is read.

            Try
                netstream.Read(bytes, 0, CInt(tcpclient.ReceiveBufferSize))
                returndata = Encoding.ASCII.GetString(bytes)    ' Returns the data received from the host to the console.
                returndata = returndata.Replace(CR, vbCrLf).Replace(START_MESSAGE, "").Replace(END_MESSAGE, "") 'la risposta è nel formato hl7 e quindi va pulita
            Catch ex As IOException
                returndata = Nothing
            End Try
        End If


        Return returndata

    End Function

    Private Function send(ByVal msgToSend As String) As Boolean

        Dim retValue As Boolean = True
        Dim bytes As Byte() = System.Text.Encoding.ASCII.GetBytes(msgToSend)

        Try
            netstream.Write(bytes, 0, msgToSend.Length)
            netstream.Flush()
        Catch ex As Exception
            Status1.Text = "Collegamento con il server interrotto"
            tcpConnected = False
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
    Private Sub execute(ByVal fileName As String)

        Dim readText() As String = File.ReadAllLines(fileName)

        'Dim information = My.Computer.FileSystem.GetFileInfo(FilePathToSend.Text)
        'MsgBox(information.DirectoryName)

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

            If (Not tcpConnected) Then
                tcpConnect()
            End If


            Dim msgToSend As String() = lines.ToArray(GetType(String))
            Dim ok As Boolean = send(hl7Encode(msgToSend))
            Dim answer As String = Nothing
            If (ok) Then
                answer = getAnswer()    'risposta dal server
                If (answer Is Nothing) Then
                    Status1.Text = "Il server non ha fornito risposta"
                    ok = False
                End If

                'se a causa della risposta c'è un esito negativo, il messaggio va trattato come garbage
            End If


            If (ok) Then
                FConsole.Text += answer
            Else
                'scrive tutte le righe lines sul file garbage. Le righe sono precedute da un record vuoto dal secondo messaggio in poi
            End If

            tcpDisconnect() 'la disconnessione e la connessione è necessaria perchè il server si disconnette per ogni messaggio
            tcpConnect()

        Next

        enableButton()

    End Sub

    Private Sub tcpDisconnect()


        tcpConnected = False
        If (Not tcpclient Is Nothing) Then
            tcpclient.Close()
            netstream.Close()
        End If
        tcpclient = Nothing


    End Sub

    Private Sub tcpConnect()
        tcpclient = New TcpClient

        Try
            tcpclient.Connect(hostname.Text, CInt(porta.Text))
            netstream = tcpclient.GetStream()

        Catch ex As Exception
            '            Console.WriteLine("couldnt connect to IP Address")
        End Try

        tcpConnected = tcpclient.Connected

    End Sub

    Private Sub enableButton()
        Disconnetti.Enabled = tcpConnected
        BtnConn.Enabled = Not tcpConnected
        hostname.Enabled = Not tcpConnected
        porta.Enabled = Not tcpConnected
        Invio.Enabled = tcpConnected And fileExist
    End Sub

End Class

