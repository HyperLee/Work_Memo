# 飲料抽選機入門網頁規格書

## 1. 專案架構

- 採用 ASP.NET Core Razor Pages。
- 前端頁面：`Pages/Index.cshtml`、`Pages/Index.cshtml.cs`
- 樣式：`wwwroot/css/site.css`
- 前端互動：`wwwroot/js/site.js`（可選）

## 2. 功能需求

- 顯示一個標題：「飲料抽選機」。
- 顯示一個「抽選」按鈕。
- 按下按鈕後，從預設飲料清單中隨機選出一項，並顯示於頁面上。
- 飲料清單可寫死於後端程式碼（如 `List<string>`）。
- 食物清單透過 #websearch 幫我抓取台灣新竹縣竹北市飲料店家（不要超過 50 個店家）。

## 3. 頁面設計

- 版面簡潔，適合初學者理解。
- 使用 Bootstrap 進行基礎美化（專案已內建）。
- 結果區域明顯顯示抽選結果。

## 4. 程式碼設計

- 在 `Index.cshtml.cs` 中建立飲料清單與抽選邏輯。
- 使用 POST 方法觸發抽選。
- 抽選結果透過 Model 傳遞至 Razor Page 顯示。

## 6. 其他建議

- 可於 `site.css` 增加自訂樣式美化頁面。
- 可於 `site.js` 增加前端互動（進階）。
- 無需登入、註冊等功能
- 無需資料庫
- 無需 API
- 僅需支援桌面瀏覽器