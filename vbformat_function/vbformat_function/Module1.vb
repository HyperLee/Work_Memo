Module Module1

    Sub Main()
        Dim UnitWeight As String = ""
        Dim UnitWeight2 As String = ""
        'Dim TotalWeight As Double = 0.0
        Dim TotalWeight As String = ""
        Dim QTY As String = ""

        TotalWeight = 13.5
        QTY = 30

        'UnitWeight = CStr(Format(CDbl(TotalWeight) / CInt(QTY), "0.00"))
        'UnitWeight = CStr((CInt(TotalWeight) / CInt(QTY)))
        'Console.WriteLine(UnitWeight)
        'Console.ReadLine()

        If QTY <> "0" And QTY <> "" And TotalWeight <> "" Then
            UnitWeight = CStr(Format(CInt(TotalWeight) / CInt(QTY), "###0.00"))
            UnitWeight2 = CStr(Format(CDbl(TotalWeight) / CDbl(QTY), "###0.00"))
        End If


        Console.WriteLine("TotalWeight_CInt: " & (CInt(TotalWeight)))
        Console.WriteLine("TotalWeight_CDbl: " & (CDbl(TotalWeight)))
        Console.WriteLine("UnitWeight: " & UnitWeight)
        Console.WriteLine("UnitWeight2: " & UnitWeight2)
        Console.ReadLine()

        '// 20210729 modify; CInt會造成 原始資料TotalWeight 含有小數點會主動四捨五入變成整數
        '// 要改成使用 CDbl 才能處理 原始資料TotalWeight是含有小數點的狀況下會是原始數值,不主動四捨五入

    End Sub

End Module
