Public Class Form1
    Dim workPath As String

    Private Sub BtnAgregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAgregar.Click

        Dim DlgAbrir As New OpenFileDialog()
        Dim ruta As String
        DlgAbrir.Multiselect = True
        If (DlgAbrir.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            For Each ruta In DlgAbrir.FileNames
                LstFiles.Items.Add(ruta)
            Next
        End If
        TextBox1.Text = IO.Path.GetDirectoryName(ruta)
        workPath = IO.Path.GetDirectoryName(ruta)
    End Sub

    Private Sub BtnClearLst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClearLst.Click

        LstFiles.Items.Clear()
        LstFiles.Refresh()
        TextBox1.Text = LstFiles.Items.Count
    End Sub

    Private Sub BtnProcesar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnProcesar.Click
        Dim path_Eres As String
        Dim path_TempRes As String
        Dim path_GainPMT As String
        Dim path_EmisTest As String
        Dim path_TestTime As String
        path_Eres = TextBox2.Text
        path_GainPMT = TextBox4.Text
        path_TempRes = TextBox3.Text
        path_EmisTest = TextBox5.Text
        path_TestTime = TextBox6.Text


        With ProgressBar1
            .Visible = True
            .Maximum = 5
            .Minimum = 0
            .Value = 0
        End With

        WriteERes(path_Eres)
        Refresh()
        WriteTempRes(path_TempRes)
        Refresh()
        WriteGainPMT(path_GainPMT)
        Refresh()
        WriteEmisTest(path_EmisTest)
        Refresh()
        WriteTimeTest(path_TestTime)
        Refresh()

    End Sub

    Private Function WriteERes(ByVal fich_Eres As String)
        Dim path_orig As String
        Dim findText As String
        Dim fileString As String
        Dim posEnergy As Integer
        Dim posFecha As Integer
        Dim fecha As String = ""
        Dim posEres As Integer

        Dim Eres, Emin, Emax As String
        Dim CentMed, CentMin, CentMax As String

        findText = "Prueba de energ"
        With Label1
            .Visible = True
            .Text = "Resolución Energia"
            'Refresh()
        End With
        ProgressBar1.Value = 1
        'Refresh()
        Dim sw As New System.IO.StreamWriter(workPath + "\" + fich_Eres, True)
        sw.WriteLine("Challenge Accepted!!!")
        sw.WriteLine("Fecha : ERes (%) : Emin (%) : Emax (%) : CentMed : CentMin : CentMax")

        For i = 0 To LstFiles.Items.Count - 1
            path_orig = LstFiles.Items.Item(i)
            fileString = My.Computer.FileSystem.ReadAllText(path_orig)
            posEnergy = InStr(fileString, findText)
            'With ProgressBar1
            ' .Visible = True
            ' .Maximum = LstFiles.Items.Count - 1
            ' .Minimum = 0
            ' .Value = i
            ' End With
            If posEnergy <> 0 Then
                findText = "Hora de inicio"
                posFecha = InStr(fileString, findText)
                fecha = fileString.Substring(posFecha + 32, 10)
                'TextBox1.Text = fecha
                sw.Write(fecha + vbTab)
                findText = "98 <= (Media Centroide)"
                posEres = InStr(fileString, findText)
                '174 son los caracteres desde el inicio de 'fintText hasta justo el comienzo de Eres
                'son 171 caracteres + 4 saltos de linea - 1
                Eres = fileString.Substring(posEres + 174, 5)
                Emin = fileString.Substring(posEres + 217, 5)
                Emax = fileString.Substring(posEres + 260, 5)
                CentMed = fileString.Substring(posEres + 163, 5)
                CentMin = fileString.Substring(posEres + 206, 5)
                CentMax = fileString.Substring(posEres + 249, 5)
                'TextBox1.Text = fecha
                sw.Write(Eres + vbTab + Emin + vbTab + Emax + vbTab + CentMed + vbTab + CentMin + vbTab + CentMax + vbCrLf)
            End If
        Next
        sw.Close()

        'Refresh()
        'Label1.Visible = False

    End Function

    Private Function WriteTempRes(ByVal fich_TempRes As String)
        Dim path_orig As String
        Dim findText As String
        Dim fileString As String
        Dim posicion As Integer
        Dim posFecha As Integer
        Dim fecha As String = ""
        Dim posEres As Integer
        Dim Eres As String

        findText = "Prueba de tiempo"
        With Label1
            .Visible = True
            .Text = "Resolución Temporal"
            'Refresh()
        End With
        ProgressBar1.Value = 2
        'Refresh()
        Dim sw As New System.IO.StreamWriter(workPath + "\" + fich_TempRes, True)
        sw.WriteLine("Challenge Accepted!!!")
        sw.WriteLine("Fecha : TempRes (ps)")

        For i = 0 To LstFiles.Items.Count - 1
            path_orig = LstFiles.Items.Item(i)
            fileString = My.Computer.FileSystem.ReadAllText(path_orig)
            posicion = InStr(fileString, findText)
            'With ProgressBar1
            ' .Visible = True
            ' .Maximum = LstFiles.Items.Count - 1
            ' .Minimum = 0
            ' .Value = i
            ' End With
            If posicion <> 0 Then
                findText = "Hora de inicio"
                posFecha = InStr(fileString, findText)
                fecha = fileString.Substring(posFecha + 32, 10)
                'TextBox1.Text = fecha
                sw.Write(fecha + vbTab)
                findText = "Cambiar < 20.0-ps"
                posEres = InStr(fileString, findText)
                Eres = fileString.Substring(posEres + 50, 5)
                'TextBox1.Text = fecha
                sw.Write(Eres + vbCrLf)
            End If
        Next
        sw.Close()
        'ProgressBar1.Value = 2
        'Refresh()
        'Label1.Visible = False

    End Function

    Private Function WriteGainPMT(ByVal fich_GainPMT As String)
        Dim path_orig As String
        Dim findText As String
        Dim fileString As String
        Dim posicion As Integer
        Dim posFecha As Integer
        Dim fecha As String = ""
        Dim posGain As Integer

        Dim iteraciones, ErrMed, ErrMax As String
        Dim GainMed, GainMin, GainMax As String

        findText = "ganancia de PMT:"
        With Label1
            .Visible = True
            .Text = "Ganancia PMT"
        End With
        ProgressBar1.Value = 3
        'Refresh()
        Dim sw As New System.IO.StreamWriter(workPath + "\" + fich_GainPMT, True)
        sw.WriteLine("Challenge Accepted!!!")
        sw.WriteLine("Fecha : Iter# : ErrMed(%) : ErrMax(%) : GainMed : GainMax : GainMin")

        For i = 0 To LstFiles.Items.Count - 1
            path_orig = LstFiles.Items.Item(i)
            fileString = My.Computer.FileSystem.ReadAllText(path_orig)
            posicion = InStr(fileString, findText)
            'With ProgressBar1
            ' .Visible = True
            ' .Maximum = LstFiles.Items.Count - 1
            ' .Minimum = 0
            ' .Value = i
            ' End With
            If posicion <> 0 Then
                findText = "Hora de inicio"
                posFecha = InStr(fileString, findText)
                fecha = fileString.Substring(posFecha + 32, 10)
                'TextBox1.Text = fecha
                sw.Write(fecha + vbTab)
                findText = "Factor de ganancia"
                posGain = InStr(fileString, findText)
                iteraciones = fileString.Substring(posGain - 207, 2)
                ErrMed = fileString.Substring(posGain - 164, 5)
                ErrMax = fileString.Substring(posGain - 113, 6)
                If ErrMax.Substring(0, 1) <> "-" Then
                    ErrMax = fileString.Substring(posGain - 113, 5)
                End If
                GainMed = fileString.Substring(posGain + 93, 6)
                GainMin = fileString.Substring(posGain + 132, 6)
                GainMax = fileString.Substring(posGain + 171, 6)
                'TextBox1.Text = fecha
                sw.Write(iteraciones + vbTab + ErrMed + vbTab + ErrMax + vbTab + GainMed + vbTab + GainMin + vbTab + GainMax + vbCrLf)
            End If
        Next
        sw.Close()
        ProgressBar1.Value = 3
        Refresh()
        'Label1.Visible = False

    End Function

    Private Function WriteEmisTest(ByVal fich_EmisTest As String)
        Dim path_orig As String
        Dim findText As String
        Dim fileString As String
        Dim posicion As Integer
        Dim posFecha As Integer
        Dim fecha As String = ""
        Dim posEmis As Integer

        Dim Lin_3, Lin_2, Lin_1 As String
        Dim Lin3, Lin2, Lin1, Lin0 As String

        findText = "Prueba de emisiones"
        With Label1
            .Visible = True
            .Text = "Test de emisión"
        End With
        Dim sw As New System.IO.StreamWriter(workPath + "\" + fich_EmisTest, True)
        sw.WriteLine("Challenge Accepted!!!")
        sw.WriteLine("Fecha : Lin(-3) : Lin(-2) : Lin(-1) : Lin(0) : Lin(1) : Lin(2) : Lin(3)")

        For i = 0 To LstFiles.Items.Count - 1
            path_orig = LstFiles.Items.Item(i)
            fileString = My.Computer.FileSystem.ReadAllText(path_orig)
            posicion = InStr(fileString, findText)
            'With ProgressBar1
            ' .Visible = True
            ' .Maximum = LstFiles.Items.Count - 1
            ' .Minimum = 0
            ' .Value = i
            ' End With
            If posicion <> 0 Then
                findText = "Hora de inicio"
                posFecha = InStr(fileString, findText)
                fecha = fileString.Substring(posFecha + 32, 10)
                'TextBox1.Text = fecha
                sw.Write(fecha + vbTab)
                findText = "Error de linealidad <= 2.0"
                posEmis = InStr(fileString, findText)
                Lin_3 = fileString.Substring(posEmis + 196, 6)
                Lin_2 = fileString.Substring(posEmis + 244, 6)
                Lin_1 = fileString.Substring(posEmis + 292, 6)
                Lin0 = fileString.Substring(posEmis + 340, 6)
                Lin1 = fileString.Substring(posEmis + 388, 6)
                Lin2 = fileString.Substring(posEmis + 436, 6)
                Lin3 = fileString.Substring(posEmis + 484, 6)
                'TextBox1.Text = fecha
                sw.Write(Lin_3 + vbTab + Lin_2 + vbTab + Lin_1 + vbTab + Lin0 + vbTab + Lin1 + vbTab + Lin2 + vbTab + Lin3 + vbCrLf)
            End If
        Next
        sw.Close()
        ProgressBar1.Value = 4
        'Label1.Visible = False

    End Function

    Private Function WriteTimeTest(ByVal fich_TimeTest As String)
        Dim path_orig As String
        Dim findText As String
        Dim fileString As String
        Dim posicion As Integer
        Dim posFecha As Integer
        Dim fecha As String = ""
        Dim posTime As Integer
        Dim Time As String

        findText = "RESUMEN:"
        With Label1
            .Visible = True
            .Text = "Duracion del Test"
        End With
        ProgressBar1.Value = 5
        Dim sw As New System.IO.StreamWriter(workPath + "\" + fich_TimeTest, True)
        sw.WriteLine("Challenge Accepted!!!")
        sw.WriteLine("Fecha : Duracion (min)")

        For i = 0 To LstFiles.Items.Count - 1
            path_orig = LstFiles.Items.Item(i)
            fileString = My.Computer.FileSystem.ReadAllText(path_orig)
            posicion = InStr(fileString, findText)

            If posicion <> 0 Then
                findText = "Hora de inicio"
                posFecha = InStr(fileString, findText)
                fecha = fileString.Substring(posFecha + 32, 10)
                sw.Write(fecha + vbTab)
                findText = "Tiempo de"
                posTime = InStr(fileString, findText)
                Time = fileString.Substring(posTime + 32, 4)
                sw.Write(Time + vbCrLf)
            End If
        Next
        sw.Close()

    End Function
End Class
