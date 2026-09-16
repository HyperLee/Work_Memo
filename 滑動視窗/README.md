# 滑動視窗：固定長度最大和與最長不重複子字串

這個 .NET 10 主控台專案示範兩種常見滑動視窗：固定長度視窗的最大總和，以及可變長度視窗的最長不重複子字串。

## 題目與需求

專案保留兩個公開 API：

- `MaxSumSubarray(int[] arr, int k)`：找出長度恰為 `k` 的連續子陣列最大和。
- `LongestUniqueSubstring(string str)`：找出沒有重複字元的最長連續子字串長度。

預設入口必須能無人值守地執行，不能讀取鍵盤或依賴隨機值，且每個案例都要輸出預期值、實際值與通過狀態。

## 輸入、輸出與限制

### 固定長度最大和

- 輸入：非 null、非空的 `int[]`，以及 `1 <= k <= arr.Length`。
- 輸出：所有長度為 `k` 的連續視窗中最大總和。
- 非法輸入會拋出 `ArgumentException`。
- 使用 `int` 加總，因此極端輸入仍受 `Int32` 溢位限制。

### 最長不重複子字串

- 輸入：任意 .NET `string`；null 或空字串回傳 `0`。
- 輸出：以 UTF-16 `char` 計算的最長不重複連續區段長度。

## 快速開始

從本 README 所在目錄執行：

```bash
dotnet restore SlidingWindow/SlidingWindow.csproj
dotnet build SlidingWindow/SlidingWindow.csproj --nologo
dotnet run --project SlidingWindow/SlidingWindow.csproj --no-build --nologo
```

若只有較新的 major runtime，再使用 `DOTNET_ROLL_FORWARD=Major`。任何 smoke case 失敗都會讓程序以非零結束碼結束。

## 核心概念

滑動視窗把相鄰區段的重複計算轉成局部更新：

- 固定長度：`新總和 = 舊總和 - 離開值 + 進入值`。
- 可變長度：右端逐步前進；遇到重複字元時，只把左端推到該字元上次位置的下一格。

## 詳細設計

### `MaxSumSubarray`

1. 驗證陣列與 `k`。
2. 計算第一個長度 `k` 視窗。
3. 每次移除 `arr[i-k]` 並加入 `arr[i]`。
4. 持續更新最大值。

### `LongestUniqueSubstring`

1. 用字典保存每個字元最近出現的索引。
2. `end` 每次向右移一格。
3. 若字元重複，以 `Max(start, previousIndex + 1)` 移動左界，避免左界倒退。
4. 用 `end - start + 1` 更新答案。

## 逐步範例

對 `[1,4,2,10,2,3,1,0,20]`、`k = 4`：

| 視窗 | 總和 | 目前最大 |
|---|---:|---:|
| `[1,4,2,10]` | 17 | 17 |
| `[4,2,10,2]` | 18 | 18 |
| `[2,10,2,3]` | 17 | 18 |
| `[10,2,3,1]` | 16 | 18 |
| `[2,3,1,0]` | 6 | 18 |
| `[3,1,0,20]` | 24 | 24 |

對 `pwwkew`，讀到第二個 `w` 時左界越過第一個 `w`；之後最長有效視窗為 `wke`，長度是 3。

## 正確性與不變量

- 固定視窗迴圈中，`windowSum` 永遠等於目前恰好 `k` 個元素的總和；`maxSum` 是已走訪視窗的最大值。
- 可變視窗中，`start..end` 永遠不含重複字元，而且 `start` 單調不減；因此每個合法視窗長度都能被正確比較。

## 複雜度

| API | 時間 | 額外空間 |
|---|---|---|
| `MaxSumSubarray` | `O(n)` | `O(1)` |
| `LongestUniqueSubstring` | 平均 `O(n)` | `O(min(n, 字元種類數))` |

## 測試矩陣

| 類別 | 案例 | 預期 |
|---|---|---:|
| 最大和 | 一般陣列 | 24 |
| 最大和 | 全為負數 | -5 |
| 最大和 | 視窗等於陣列 | 6 |
| 最大和 | `k = 0` | `ArgumentException` |
| 最長不重複 | `abcabcbb` | 3 |
| 最長不重複 | `pwwkew` | 3 |
| 最長不重複 | 空字串 | 0 |
| 最長不重複 | null | 0 |

## 完整執行輸出

```text
Case: 最大和：一般陣列
Expected: 24
Actual: 24
PASS-FAIL: PASS

Case: 最大和：全為負數
Expected: -5
Actual: -5
PASS-FAIL: PASS

Case: 最大和：視窗等於陣列
Expected: 6
Actual: 6
PASS-FAIL: PASS

Case: 最大和：視窗為零
Expected: ArgumentException
Actual: ArgumentException
PASS-FAIL: PASS

Case: 最長不重複：一般字串
Expected: 3
Actual: 3
PASS-FAIL: PASS

Case: 最長不重複：重複字元跨視窗
Expected: 3
Actual: 3
PASS-FAIL: PASS

Case: 最長不重複：空字串
Expected: 0
Actual: 0
PASS-FAIL: PASS

Case: 最長不重複：null
Expected: 0
Actual: 0
PASS-FAIL: PASS

Summary: 8/8 checks passed.
```

## 專案結構

```text
.
├── SlidingWindow/
│   ├── Program.cs
│   └── SlidingWindow.csproj
├── SlidingWindow.Tests/   # 既有相鄰專案，本次不修改
├── .vscode/
├── docs/readme-template.md
├── AGENTS.md
└── README.md
```

## 參考資料

- [Microsoft Learn：Dictionary<TKey,TValue>](https://learn.microsoft.com/dotnet/api/system.collections.generic.dictionary-2)
- [Microsoft Learn：例外狀況與例外狀況處理](https://learn.microsoft.com/dotnet/csharp/fundamentals/exceptions/)

## 已知限制

- 字串演算法依 UTF-16 `char` 判斷重複，不會把代理對組合成單一 Unicode 純量值。
- 本次驗證只執行 console 專案；相鄰的 `SlidingWindow.Tests` 明確不在修改與執行範圍。
