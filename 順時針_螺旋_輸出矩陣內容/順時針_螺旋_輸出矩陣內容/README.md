# 順時針螺旋輸出矩陣內容

本專案示範如何以 C# 撰寫程式，將二維矩陣內容依照順時針螺旋順序輸出。

## 題目來源
- LeetCode 54. Spiral Matrix
- [題目連結](https://leetcode.com/problems/spiral-matrix/description/)

## 專案結構
- `Program.cs`：主程式與螺旋輸出邏輯實作，含詳細中文註解。
- `題目描述`：原始題目說明（markdown 格式）。

## 執行方式
1. 使用 .NET 8.0 或相容環境。
2. 於專案目錄下執行：

```powershell
dotnet run
```

## 程式邏輯說明
- 透過一個方向陣列 `DIRS` 控制移動方向（右、下、左、上）。
- 每次將目前元素加入結果，並標記已拜訪。
- 若遇到邊界或已拜訪過的格子則換方向。
- 直到所有元素皆被拜訪。

## 範例輸出

```
res: 1, 2, 3, 6, 9, 8, 7, 4, 5
```

## 參考
- [C# 官方文件](https://learn.microsoft.com/zh-tw/dotnet/csharp/)
- LeetCode 題解討論區
