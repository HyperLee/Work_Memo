# Json_reader

使用 Newtonsoft.Json 解析內嵌 JSON，讀取狀態與資料陣列。

本專案保留原有內嵌 JSON 與逐筆讀取流程，並以 SDK-style .NET 10（`net10.0`）建置。JSON 依賴改由 `Newtonsoft.Json` 13.0.1 的 PackageReference 管理。

## 概念說明

程式使用 `JsonConvert.DeserializeObject<JObject>` 與 `JObject.Parse` 讀取 JSON 物件，取得頂層 `status`、`data`，再逐一走訪 `data` 中的 `JObject`。原始範例中的欄位讀取與字串清理流程均保留。

## 複雜度

令 JSON 文字長度為 m，`data` 項目數為 n：

| 項目 | 複雜度 |
| --- | --- |
| JSON 解析 | O(m)（由 JSON 文件長度決定） |
| data 走訪 | O(n) |
| 額外空間 | O(m)，保存解析後的 JSON 結構 |

## 快速開始

從本專案外層目錄執行：

```powershell
dotnet restore ".\Json_reader\Json_reader.csproj"
dotnet build ".\Json_reader.sln" --configuration Debug --nologo
dotnet run --project ".\Json_reader\Json_reader.csproj" --configuration Debug --no-build --nologo
```

程式不需要輸入；PackageReference 會在 restore 時解析 `Newtonsoft.Json` 13.0.1。

## 代表性案例與實際輸出

程式內嵌包含 `data` 陣列與頂層 `status` 的 JSON。執行結果為：

```text
成功
```

## 專案結構

```text
Json_reader/
├── Json_reader/
│   ├── Program.cs
│   └── Json_reader.csproj
├── .vscode/
│   ├── launch.json
│   └── tasks.json
└── docs/
    └── readme-template.md
```

