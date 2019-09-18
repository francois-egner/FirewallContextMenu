Imports System.IO

Public Class Form1
    Private arguments As ObjectModel.ReadOnlyCollection(Of String) = My.Application.CommandLineArgs
    Private command As String


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Check if arguments are given/set
        If arguments.Count > 0 Then
            command = arguments(0)
            If command = "register" Then
                register()
                Application.Exit()
            ElseIf command = "unregister" Then
                unregister()
                Application.Exit()
            ElseIf command = "block" Or command = "allow" Then
                Dim exeName As String = arguments(1).Split("\")(arguments(1).Split("\").Length - 1)
                Me.Text = String.Format("{0} {1}", Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(command), Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(exeName))
            ElseIf command = "delete" Then
                Dim filename As String = arguments(1)
                If (exist(filename)) Then
                    If deleteRule(filename) Then
                        MsgBox("Successfully deleted Rule!")
                        Application.Exit()
                    Else
                        MsgBox("Failed to delete Rule!")
                    End If
                Else
                    MsgBox("There is no rule for this executable set in your firewall... Aborting!")
                End If

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
        If Name.Length = 0 Or networks.Count = 0 Or (Not cbox_in.Checked And Not cbox_out.Checked) Then
            MsgBox("Invalid settings! Please check your given information!")
            Return
        End If

        If exist(filename) Then
            If MessageBox.Show("Rule for this executable already exists. Do you want to override it?", "Overwrite rule", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                deleteRule(filename)
                add(filename, networks)
            Else
                Return
            End If
        Else
            add(filename, networks)
        End If
        MsgBox("Succesfully added rule to firewall!")
        Application.Exit()
    End Sub

    Private Function add(ByVal filename As String, ByVal networks As List(Of String)) As Boolean
        Try
            'If rule should be for incoming connections
            If cbox_in.Checked Then
                Dim result As Boolean = addApp(filename, String.Join(",", networks), True, filename, command = "allow")
                If Not result Then
                    MsgBox("Failed to add new firewall rule! [INCOMING]")
                End If
            End If
            'If rule should be for outgoing connections
            If cbox_out.Checked Then
                Dim result As Boolean = addApp(filename, String.Join(",", networks), False, filename, command = "allow")
                If Not result Then
                    MsgBox("Failed to add new firewall rule [OUTGOING]!")
                End If
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function
    Private Function netshRequest(ByVal argumentsString As String) As String
        Dim output As String
        Dim p As Process = New Process
        With p
            .StartInfo.CreateNoWindow = True
            .StartInfo.RedirectStandardOutput = True
            .StartInfo.UseShellExecute = False
            .StartInfo.FileName = "netsh"
            .StartInfo.Arguments = argumentsString 'String.Format(My.Resources.addString, Name, networks, direction, filename, blockOrAllowString)
            .Start()
            output = .StandardOutput.ReadToEnd
            .WaitForExit()
        End With

        Return output
    End Function
    Private Function getRule(ByVal filename As String) As String
        Dim resultString As String = netshRequest(String.Format(My.Resources.existString, filename))
        Return resultString
    End Function

    Private Function deleteRule(ByVal exeName As String) As Boolean
        Dim resultString As String = netshRequest(String.Format(My.Resources.deleteString, exeName))
        Return If(resultString.Contains("OK."), True, False)
    End Function
    Private Function exist(ByVal exeName As String) As Boolean
        Dim invalidRuleLineCount As Integer = getRule("").Split(vbCrLf).Count
        Dim valid As Boolean = If(getRule(exeName).Split(vbCrLf).Count = invalidRuleLineCount, False, True)
        Return valid
    End Function
    Private Function addApp(ByVal name As String, ByVal networks As String, ByVal inOrOut As Boolean, ByVal filename As String, ByVal blockOrAllow As Boolean) As Boolean

        Dim direction As String = If(inOrOut, "In", "Out")
        Dim blockOrAllowString As String = If(blockOrAllow, "Allow", "Block")

        Dim resultString As String = netshRequest(String.Format(My.Resources.addString, name, networks, direction, filename, blockOrAllowString))

        Return (resultString.Split(vbCrLf)(0) = "OK.")

    End Function

    'Delete context menu entries
    Public Sub unregister()
        Try
            My.Computer.Registry.ClassesRoot.DeleteSubKeyTree("exefile\shell\Block in firewall")
            My.Computer.Registry.ClassesRoot.DeleteSubKeyTree("exefile\shell\Allow in firewall")
            My.Computer.Registry.ClassesRoot.DeleteSubKeyTree("exefile\shell\Delete rule")
        Catch ex As Exception
            MsgBox("Failed to delete contex menu entries!" & vbCrLf & ex.Message)
        End Try

    End Sub
    'Create context menu entries
    Private Sub register()
        Try
            'Create context menu entry for block action
            My.Computer.Registry.SetValue("HKEY_CLASSES_ROOT\exefile\shell\Delete rule\command", "", Application.ExecutablePath() & " delete " & ControlChars.Quote & "%1" & ControlChars.Quote)
            My.Computer.Registry.SetValue("HKEY_CLASSES_ROOT\exefile\shell\Delete rule\", "Icon", My.Application.Info.DirectoryPath & "\delete.ico")

            'Create context menu entry for block action
            My.Computer.Registry.SetValue("HKEY_CLASSES_ROOT\exefile\shell\Block in firewall\command", "", Application.ExecutablePath() & " block " & ControlChars.Quote & "%1" & ControlChars.Quote)
            My.Computer.Registry.SetValue("HKEY_CLASSES_ROOT\exefile\shell\Block in firewall\", "Icon", My.Application.Info.DirectoryPath & "\block.ico")

            'Create context menu entry for allow action
            My.Computer.Registry.SetValue("HKEY_CLASSES_ROOT\exefile\shell\Allow in firewall\command", "", Application.ExecutablePath() & " allow " & ControlChars.Quote & "%1" & ControlChars.Quote)
            My.Computer.Registry.SetValue("HKEY_CLASSES_ROOT\exefile\shell\Allow in firewall\", "Icon", My.Application.Info.DirectoryPath & "\allow.ico")
        Catch ex As Exception
            MsgBox("Failed to create contex menu entries!" & vbCrLf & ex.Message)
            'TODO: If second creation failed -> delete first
        End Try
    End Sub
End Class
