<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Richiesto da Progettazione Windows Form
    Private components As System.ComponentModel.IContainer

    'NOTA: la procedura che segue è richiesta da Progettazione Windows Form
    'Può essere modificata in Progettazione Windows Form.  
    'Non modificarla nell'editor del codice.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.BtnConn = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.hostname = New System.Windows.Forms.TextBox()
        Me.porta = New System.Windows.Forms.TextBox()
        Me.FConsole = New System.Windows.Forms.TextBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.FilePathToSend = New System.Windows.Forms.TextBox()
        Me.Browse1 = New System.Windows.Forms.Button()
        Me.Split = New System.Windows.Forms.Button()
        Me.msgBox = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.Status1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BtnConn
        '
        Me.BtnConn.Location = New System.Drawing.Point(225, 12)
        Me.BtnConn.Name = "BtnConn"
        Me.BtnConn.Size = New System.Drawing.Size(75, 23)
        Me.BtnConn.TabIndex = 0
        Me.BtnConn.Text = "Connetti"
        Me.BtnConn.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Enabled = False
        Me.Button2.Location = New System.Drawing.Point(225, 41)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "Disconnetti"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Enabled = False
        Me.Button3.Location = New System.Drawing.Point(713, 46)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(50, 23)
        Me.Button3.TabIndex = 2
        Me.Button3.Text = "Invia"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'hostname
        '
        Me.hostname.Location = New System.Drawing.Point(52, 16)
        Me.hostname.Name = "hostname"
        Me.hostname.Size = New System.Drawing.Size(152, 20)
        Me.hostname.TabIndex = 4
        Me.hostname.Text = "localhost"
        '
        'porta
        '
        Me.porta.Location = New System.Drawing.Point(52, 42)
        Me.porta.Name = "porta"
        Me.porta.Size = New System.Drawing.Size(152, 20)
        Me.porta.TabIndex = 5
        Me.porta.Text = "62438"
        '
        'FConsole
        '
        Me.FConsole.Location = New System.Drawing.Point(12, 118)
        Me.FConsole.Multiline = True
        Me.FConsole.Name = "FConsole"
        Me.FConsole.ReadOnly = True
        Me.FConsole.Size = New System.Drawing.Size(751, 333)
        Me.FConsole.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label1.Location = New System.Drawing.Point(7, 102)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(45, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Console"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label3.Location = New System.Drawing.Point(7, 45)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(32, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Porta"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label4.Location = New System.Drawing.Point(7, 19)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(29, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Host"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label5.Location = New System.Drawing.Point(323, 22)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(40, 13)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "File hl7"
        '
        'FilePathToSend
        '
        Me.FilePathToSend.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.FilePathToSend.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem
        Me.FilePathToSend.Location = New System.Drawing.Point(407, 19)
        Me.FilePathToSend.Name = "FilePathToSend"
        Me.FilePathToSend.Size = New System.Drawing.Size(300, 20)
        Me.FilePathToSend.TabIndex = 12
        '
        'Browse1
        '
        Me.Browse1.Location = New System.Drawing.Point(713, 17)
        Me.Browse1.Name = "Browse1"
        Me.Browse1.Size = New System.Drawing.Size(50, 23)
        Me.Browse1.TabIndex = 14
        Me.Browse1.Text = "Sfoglia"
        Me.Browse1.UseVisualStyleBackColor = True
        '
        'Split
        '
        Me.Split.Location = New System.Drawing.Point(713, 75)
        Me.Split.Name = "Split"
        Me.Split.Size = New System.Drawing.Size(50, 23)
        Me.Split.TabIndex = 15
        Me.Split.Text = "Split"
        Me.Split.UseVisualStyleBackColor = True
        '
        'msgBox
        '
        Me.msgBox.AutoSize = True
        Me.msgBox.Location = New System.Drawing.Point(13, 476)
        Me.msgBox.Name = "msgBox"
        Me.msgBox.Size = New System.Drawing.Size(0, 13)
        Me.msgBox.TabIndex = 16
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(13, 476)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(0, 13)
        Me.Label6.TabIndex = 17
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Status1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 487)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(795, 22)
        Me.StatusStrip1.TabIndex = 18
        '
        'Status1
        '
        Me.Status1.Name = "Status1"
        Me.Status1.Size = New System.Drawing.Size(0, 17)
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(795, 509)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.msgBox)
        Me.Controls.Add(Me.Split)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.FilePathToSend)
        Me.Controls.Add(Me.Browse1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.FConsole)
        Me.Controls.Add(Me.porta)
        Me.Controls.Add(Me.hostname)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.BtnConn)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Form1"
        Me.Text = "Client hlt7"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BtnConn As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents hostname As System.Windows.Forms.TextBox
    Friend WithEvents porta As System.Windows.Forms.TextBox
    Friend WithEvents FConsole As System.Windows.Forms.TextBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents FilePathToSend As System.Windows.Forms.TextBox
    Friend WithEvents Browse1 As System.Windows.Forms.Button
    Friend WithEvents Split As System.Windows.Forms.Button
    Friend WithEvents msgBox As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents Status1 As System.Windows.Forms.ToolStripStatusLabel

End Class
