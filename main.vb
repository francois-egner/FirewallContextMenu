Imports System.IO

Public Class Form1
    Private arguments As ObjectModel.ReadOnlyCollection(Of String) = My.Application.CommandLineArgs


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If arguments.Count > 0 Then
            Dim command As String = arguments(0)
            If command = "register" Then
                register()
                Application.Exit()
            ElseIf command = "unregister" Then
                unregister()
                Application.Exit()
            ElseIf command = "block" Or command = "allow" Then
                Dim exeName As String = arguments(1).Split("\")(arguments(1).Split("\").Length - 1)
                Me.Text = String.Format("{0} {1}", Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(command), exeName)
                txtbox_name.Text = exeName.Split(".")(0)
            Else
                MsgBox("Invalid parameter! Program will exit!")
                Application.Exit()
            End If
        Else
            MsgBox("No parameter given. Program will exit!")
            Application.Exit()
        End If



    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim filename As String = arguments(1)
        Dim name As String = txtbox_name.Text
        'Prepare list of network interfaces
        Dim networks As New List(Of String)
        For Each control In panel_networks.Controls
            If TypeOf control Is CheckBox Then
                Dim castedControl As CheckBox = CType(control, CheckBox)
                If castedControl.Checked Then
                    networks.Add(castedControl.Text.ToLower)
                End If
            End If
        Next

        'Check if settings are valid
        If name.Length = 0 Or networks.Count = 0 Or Not cbox_in.Checked Or Not cbox_out.Checked Then
            MsgBox("Invalid settings! Please check your given information!")
            Return
        End If

        'If rule should be for incoming connections
        If cbox_in.Checked Then
            Dim result As Boolean = addApp(name, String.Join(",", networks), True, filename, command = "allow")
            If Not result Then
                MsgBox("Failed to add new firewall rule! [INCOMING]")
            End If
        End If

        'If rule should be for outgoing connections
        If cbox_out.Checked Then
            Dim result As Boolean = addApp(name, String.Join(",", networks), False, filename, command = "allow")
            If Not result Then
                MsgBox("Failed to add new firewall rule [OUTGOING]!")
            End If
        End If

        Application.Exit()

    End Sub

    Private Function addApp(ByVal name As String, ByVal networks As String, ByVal inOrOut As Boolean, ByVal filename As String, ByVal blockOrAllow As Boolean) As Boolean

        Dim direction As String = If(inOrOut, "In", "Out")
        Dim blockOrAllowString As String = If(blockOrAllow, "Allow", "Block")

        Dim output As String
        Dim p As Process = New Process
        With p
            .StartInfo.CreateNoWindow = True
            .StartInfo.RedirectStandardOutput = True
            .StartInfo.UseShellExecute = False
            .StartInfo.FileName = "netsh"
            .StartInfo.Arguments = String.Format(My.Resources.ruleString, name, networks, direction, filename, blockOrAllowString)
            .Start()
            output = .StandardOutput.ReadToEnd
            .WaitForExit()
        End With

        Return (output.Split(vbCrLf)(0) = "OK.")

    End Function


    Public Sub unregister()
        Try
            My.Computer.Registry.ClassesRoot.DeleteSubKeyTree("exefile\shell\Block in firewall")
            My.Computer.Registry.ClassesRoot.DeleteSubKeyTree("exefile\shell\Allow in firewall")
        Catch ex As Exception
            MsgBox("Failed to delete contex menu entries!" & vbCrLf & ex.Message)
        End Try

    End Sub
    Private Sub register()
        Try
            My.Computer.Registry.SetValue("HKEY_CLASSES_ROOT\exefile\shell\Block in firewall\command", "", Application.ExecutablePath() & " block " & ControlChars.Quote & "%1" & ControlChars.Quote)
            My.Computer.Registry.SetValue("HKEY_CLASSES_ROOT\exefile\shell\Block in firewall\", "Icon", My.Application.Info.DirectoryPath & "\block.ico")


            My.Computer.Registry.SetValue("HKEY_CLASSES_ROOT\exefile\shell\Allow in firewall\command", "", Application.ExecutablePath() & " allow " & ControlChars.Quote & "%1" & ControlChars.Quote)
            My.Computer.Registry.SetValue("HKEY_CLASSES_ROOT\exefile\shell\Allow in firewall\", "Icon", My.Application.Info.DirectoryPath & "\allow.ico")
        Catch ex As Exception
            MsgBox("Failed to create contex menu entries!" & vbCrLf & ex.Message)
        End Try
    End Sub
End Class
