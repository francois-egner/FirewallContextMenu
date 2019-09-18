<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cbox_private = New System.Windows.Forms.CheckBox()
        Me.cbox_public = New System.Windows.Forms.CheckBox()
        Me.cbox_domain = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cbox_in = New System.Windows.Forms.CheckBox()
        Me.cbox_out = New System.Windows.Forms.CheckBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.panel_networks = New System.Windows.Forms.Panel()
        Me.panel_networks.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Network(s):"
        '
        'cbox_private
        '
        Me.cbox_private.AutoSize = True
        Me.cbox_private.Location = New System.Drawing.Point(70, 8)
        Me.cbox_private.Name = "cbox_private"
        Me.cbox_private.Size = New System.Drawing.Size(59, 17)
        Me.cbox_private.TabIndex = 3
        Me.cbox_private.Text = "Private"
        Me.cbox_private.UseVisualStyleBackColor = True
        '
        'cbox_public
        '
        Me.cbox_public.AutoSize = True
        Me.cbox_public.Location = New System.Drawing.Point(135, 8)
        Me.cbox_public.Name = "cbox_public"
        Me.cbox_public.Size = New System.Drawing.Size(55, 17)
        Me.cbox_public.TabIndex = 4
        Me.cbox_public.Text = "Public"
        Me.cbox_public.UseVisualStyleBackColor = True
        '
        'cbox_domain
        '
        Me.cbox_domain.AutoSize = True
        Me.cbox_domain.Location = New System.Drawing.Point(196, 8)
        Me.cbox_domain.Name = "cbox_domain"
        Me.cbox_domain.Size = New System.Drawing.Size(62, 17)
        Me.cbox_domain.TabIndex = 5
        Me.cbox_domain.Text = "Domain"
        Me.cbox_domain.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 46)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(63, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Direction(s):"
        '
        'cbox_in
        '
        Me.cbox_in.AutoSize = True
        Me.cbox_in.Location = New System.Drawing.Point(81, 45)
        Me.cbox_in.Name = "cbox_in"
        Me.cbox_in.Size = New System.Drawing.Size(69, 17)
        Me.cbox_in.TabIndex = 7
        Me.cbox_in.Text = "Incoming"
        Me.cbox_in.UseVisualStyleBackColor = True
        '
        'cbox_out
        '
        Me.cbox_out.AutoSize = True
        Me.cbox_out.Location = New System.Drawing.Point(148, 46)
        Me.cbox_out.Name = "cbox_out"
        Me.cbox_out.Size = New System.Drawing.Size(69, 17)
        Me.cbox_out.TabIndex = 8
        Me.cbox_out.Text = "Outgoing"
        Me.cbox_out.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(15, 68)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(254, 23)
        Me.Button1.TabIndex = 10
        Me.Button1.Text = "Add"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'panel_networks
        '
        Me.panel_networks.Controls.Add(Me.Label3)
        Me.panel_networks.Controls.Add(Me.cbox_private)
        Me.panel_networks.Controls.Add(Me.cbox_public)
        Me.panel_networks.Controls.Add(Me.cbox_domain)
        Me.panel_networks.Location = New System.Drawing.Point(13, 9)
        Me.panel_networks.Name = "panel_networks"
        Me.panel_networks.Size = New System.Drawing.Size(256, 30)
        Me.panel_networks.TabIndex = 11
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(281, 98)
        Me.Controls.Add(Me.panel_networks)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.cbox_out)
        Me.Controls.Add(Me.cbox_in)
        Me.Controls.Add(Me.Label4)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.panel_networks.ResumeLayout(False)
        Me.panel_networks.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label3 As Label
    Friend WithEvents cbox_private As CheckBox
    Friend WithEvents cbox_public As CheckBox
    Friend WithEvents cbox_domain As CheckBox
    Friend WithEvents Label4 As Label
    Friend WithEvents cbox_in As CheckBox
    Friend WithEvents cbox_out As CheckBox
    Friend WithEvents Button1 As Button
    Friend WithEvents panel_networks As Panel
End Class
