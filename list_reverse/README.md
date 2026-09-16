# list_reverse

示範 `List<string>.Reverse()` 將恐龍名稱清單原地反轉。

本專案保留原有鏈結串列／集合演算法、類別、方法與輸出，並由舊式 .NET Framework 轉為 SDK-style .NET 10（`net10.0`）。

## 概念說明

Main 建立六個恐龍名稱，先逐行輸出 `Before`，呼叫 `dinosaurs.Reverse()` 後，再逐行輸出 `After`。註解中的部分反轉範例仍保留但不執行。

## 複雜度

| 項目 | 時間 | 額外空間 |
| --- | --- | --- |
| 反轉 n 個清單項目 | O(n) | O(1)（原地反轉） |

## 快速開始

從本專案外層目錄執行：

```powershell
dotnet restore ".\list_reverse\list_reverse.csproj"
dotnet build ".\list_reverse.sln" --configuration Debug --nologo
dotnet run --project ".\list_reverse\list_reverse.csproj" --configuration Debug --no-build --nologo
```

本專案沒有獨立測試專案；驗收使用上述命令與固定示範資料。錯誤輸入仍遵循原本的解析與例外語意。

## 代表性案例與實際輸出

本案使用程式內固定示範資料，執行結果如下：

```text
Before: Pachycephalosaurus
Before: Parasauralophus
Before: Mamenchisaurus
Before: Amargasaurus
Before: Coelophysis
Before: Oviraptor

After: Oviraptor
After: Coelophysis
After: Amargasaurus
After: Mamenchisaurus
After: Parasauralophus
After: Pachycephalosaurus
```

## 專案結構

```text
list_reverse/
├── list_reverse/
│   ├── Program.cs
│   └── list_reverse.csproj
├── list_reverse.sln
├── .vscode/
│   ├── launch.json
│   └── tasks.json
└── docs/
    └── readme-template.md
```

