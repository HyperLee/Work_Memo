# 主控台專案統一升級至 .NET 10

## 目標

將已確認可建置、可執行且屬於 SDK-style 主控台的 48 個 `net8.0`／`net9.0` 專案升級至 `net10.0`，同步修正專案本地版本文件與 VS Code 啟動設定，並更新 `專案架構與.NET版本盤點.md`。

## 範圍與限制

- 僅處理 `OutputType=Exe`、非 WinForms、具有 `Program.cs` 且基線 build/run 已通過的 48 個專案。
- 排除 xUnit、WinForms、舊式 .NET Framework、`bin`、`obj`、`.vs` 與其他非主控台資料夾。
- 保留既有程式碼行為；在目前工作區直接修改，不建立 worktree、不 commit、不 push。

## 執行與驗證（已完成）

1. [x] 將 48 個指定 `.csproj` 的唯一 TFM 由 `net8.0`／`net9.0` 改為 `net10.0`。
2. [x] 只在這 48 個專案的 `AGENTS.md`、README、`spec.md`（有舊版標記者）與 `.vscode/*.json` 同步版本文字與輸出路徑。
3. [x] 以 Debug 組態逐一執行 83 個完整主控台的 `dotnet build` 與 `dotnet run --no-build`；互動式入口使用有限固定輸入，記錄成功、失敗、逾時與 warning。
4. [x] 更新盤點文件為 83 個完整主控台全部 `net10.0`、舊 TFM 遺漏數 0，保留原 5 個 net9 與 43 個 net8 的歷史清單。
5. [x] 清理本輪產生的 tracked `bin`／`obj` 變更，確認目標 TFM、舊版文字、`git diff --check` 與 UTF-8/CRLF 格式。
