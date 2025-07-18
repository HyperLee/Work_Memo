# 語音記帳 app

## 1. 專案簡介
本專案為一款語音記帳應用程式，採用 ASP.NET Core Razor Pages 7.0（C# 13）開發，主打以語音輸入快速記錄日常收支。資料暫不儲存於資料庫，僅於前端暫存。主頁面為 `Bookkeeping App/Pages/Index.cshtml`。

## 2. 功能需求

### 2.1 語音輸入
- 使用微軟語音辨識服務（如 Azure Speech Service）將語音即時轉換為文字。
- 支援中文語音辨識，辨識內容自動填入記帳欄位。
- 提供語音啟動/停止按鈕，顯示辨識狀態。
- 若語音辨識失敗，顯示錯誤提示。

### 2.2 記帳功能
- 支援手動與語音輸入記帳內容。
- 記帳欄位包含：日期、金額、類別（收入/支出）、備註。
- 記帳資料暫存於前端（如 JavaScript 陣列），不連接資料庫。
- 可即時於頁面下方顯示已輸入的記帳紀錄，支援刪除單筆紀錄。

### 2.3 UI/UX
- 主頁面設計簡潔，適合行動裝置操作。
- 語音輸入按鈕明顯易用，辨識時有動畫或狀態提示。
- 記帳表單欄位自動聚焦於語音輸入後。
- 支援深色/淺色主題切換（加分項）。

### 2.4 其他
- 不需登入註冊功能。
- 不需資料匯出/匯入功能。
- 不需資料庫連線。

## 3. 技術規範

- ASP.NET Core Razor Pages 7.0
- C# 13
- 前端可用 JavaScript/TypeScript，建議使用原生 JS 或輕量函式庫
- 語音辨識優先採用微軟 Azure Speech Service，若無法使用可考慮 Web Speech API
- 程式碼需遵循專案內 `.github/instructions/csharp.instructions.md` 規範

## 4. 非功能性需求

- 程式碼需具備良好註解與結構
- UI 需具備基礎響應式設計
- 支援 Edge、Chrome、Safari 等主流瀏覽器
- 需有錯誤處理與使用者提示

## 5. 頁面結構

- `Index.cshtml`：主頁面，包含語音輸入、記帳表單、紀錄列表
- 其他頁面（如隱私權、錯誤頁）可維持預設

## 6. 參考資源

- 參考 Google Apps Script 範例：https://script.google.com/macros/s/AKfycbxJ-XJXPe06Rejboq-XONbdCuyC1zkbuGaEace-Zev-3TSh3tbvK5SGBtahhU6An2jAMg/exec
- 微軟 Azure Speech Service：https://azure.microsoft.com/zh-tw/products/ai-services/speech-to-text

