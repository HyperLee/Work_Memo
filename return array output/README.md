# return array output

以 Two Sum 範例示範方法回傳整數陣列並由呼叫端輸出索引。

本專案保留原有演算法、類別、方法與主控台互動，並由舊式 .NET Framework 轉為 SDK-style .NET 10（`net10.0`）。

## 概念說明

`TwoSum` 使用字典記錄已走訪值與索引；對每個數字查詢 `target - nums[i]`，找到配對時回傳兩個索引的整數陣列。

## 複雜度

以下以程式中的一般輸入規模說明；固定示範資料本身仍是固定成本。

| 項目 | 時間 | 額外空間 |
| --- | --- | --- |
| 字典走訪 | O(n) 平均 | O(n) |

## 快速開始

從本專案外層目錄執行：

```powershell
dotnet restore ".\return array output\return array output.csproj"
dotnet build ".\return array output.sln" --configuration Debug --nologo
dotnet run --project ".\return array output\return array output.csproj" --configuration Debug --no-build --nologo
```

本專案沒有獨立測試專案；驗收使用 restore、solution build 與代表性 `dotnet run` smoke test。若程式需要輸入，請依原提示逐行提供資料；錯誤輸入仍遵循原本的解析與例外語意。

## 代表性案例與實際輸出

程式使用固定示範資料，執行結果如下：

```text
[0,1]
```

## 專案結構

```text
return array output/
├── return array output/
│   ├── Program.cs
│   └── return array output.csproj
├── return array output.sln
├── .vscode/
│   ├── launch.json
│   └── tasks.json
└── docs/
    └── readme-template.md
```

