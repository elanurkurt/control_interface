Imports System.IO.Ports
Imports System.Reflection.Emit
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Form1
    Dim WithEvents SerialPort As New SerialPort()
    Dim sensorData As String()
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SerialPort.PortName = "COM6"
        SerialPort.BaudRate = 9600
        SerialPort.Parity = Parity.None
        SerialPort.StopBits = StopBits.One
        SerialPort.DataBits = 8
        SerialPort.Handshake = Handshake.None
        SerialPort.Encoding = System.Text.Encoding.UTF8
        SerialPort.RtsEnable = True
        SerialPort.DtrEnable = True
    End Sub


    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        SerialPort.Open()
    End Sub

    Private Sub SerialPort_DataReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles SerialPort.DataReceived
        Dim strBuffer As String = SerialPort.ReadExisting()
        Me.Invoke(Sub() DoUpdate(strBuffer))
    End Sub

    Private Sub DoUpdate(ByVal strBuffer As String)
        ' Girdi dizesini virgül karakteri ile ayırarak sensör verilerine ayrıştır
        sensorData = strBuffer.Split(","c)

        ' Her bir veri için
        For Each data As String In sensorData
            ' Veriyi iki bölüme ayır (sensör adı ve değeri)
            Dim tempData As String() = data.Split(":"c)

            ' Eğer sensör adı DHT11T ise
            If tempData(0) = "DHT11T" Then
                ' Sıcaklık değerini etikete yaz
                Label1.Text = tempData(1)

                ProgressBar1.Minimum = 0
                ProgressBar1.Maximum = 60
                Dim MyNumber As Integer
                MyNumber = Int(tempData(1))
                ProgressBar1.Value = MyNumber / 100

            ElseIf tempData(0) = "DHT11H" Then
                ' Nem değerini etikete yaz
                Label2.Text = tempData(1)

                ProgressBar2.Minimum = 0
                ProgressBar2.Maximum = 100
                Dim MyNumber As Integer
                MyNumber = Int(tempData(1))
                ProgressBar2.Value = MyNumber / 100

            ElseIf tempData(0) = "HIZ" Then
                ' Hız değerini etikete yaz
                Label3.Text = tempData(1)

            ElseIf tempData(0) = "SIVISEVIYE" Then
                ' Hız değerini etikete yaz
                Label14.Text = tempData(1)

                ProgressBar4.Minimum = 0
                ProgressBar4.Maximum = 100
                Dim MyNumber As Integer
                MyNumber = Int(tempData(1))
                ProgressBar4.Value = MyNumber / 100

            End If
        Next
    End Sub


    Private Sub SerialPort_ErrorReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialErrorReceivedEventArgs) Handles SerialPort.ErrorReceived
        MessageBox.Show("Hata kodu: " & e.EventType.ToString())
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        SerialPort.Close()
    End Sub

End Class