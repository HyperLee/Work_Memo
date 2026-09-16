# LeetCode 290：Word Pattern（Dictionary 雙向對應）

這個 .NET 10 主控台專案使用兩個 `Dictionary` 驗證 pattern 字元與單字之間的雙射關係，並以固定案例提供可重複執行的 smoke test。

## 題目與需求

給定 `pattern` 與以空白分隔的字串 `s`，判斷每個 pattern 字元是否只對應一個單字，且每個單字是否也只對應一個 pattern 字元。

例如：

- `"abba"` 與 `"dog cat cat dog"` 為 `true`。
- `"abba"` 與 `"dog dog dog dog"` 為 `false`，因為 `a`、`b` 不能同時對應 `dog`。

## 輸入、輸出與限制

- 公開 API：`bool WordPattern(string pattern, string s)`。
- 輸入：小寫 pattern 字元，以及由單一空白分隔的單字序列。
- 輸出：符合雙射回傳 `true`，否則回傳 `false`。
- 題目限制：`1 <= pattern.Length <= 300`、`1 <= s.Length <= 3000`；題目保證沒有前後空白，單字間只有一個空白。
- 目前實作依照上述題目輸入契約，不額外處理 null 或任意連續空白。

## 快速開始

從本 README 所在目錄執行：

```bash
dotnet restore DictionaryEquals用法/DictionaryEquals用法.csproj
dotnet build DictionaryEquals用法/DictionaryEquals用法.csproj --nologo
dotnet run --project DictionaryEquals用法/DictionaryEquals用法.csproj --no-build --nologo
```

若電腦只有較新的 major runtime，可在執行命令前加上 `DOTNET_ROLL_FORWARD=Major`。任何案例失敗時，程式會設定 `Environment.ExitCode = 1`。

## 核心概念

單向 `char -> word` 只能保證同一個字元不改配其他單字，無法阻止兩個不同字元共用同一單字。因此實作同時維護：

| 字典 | 不變量 |
|---|---|
| `Dictionary<char, string>` | 一個 pattern 字元只能對應一個單字 |
| `Dictionary<string, char>` | 一個單字只能對應一個 pattern 字元 |

程式使用 `.Equals` 比較已記錄的值；雙向字典共同建立完整的一對一關係。

## 詳細設計

1. 以空白切割 `s`。
2. 單字數量若與 `pattern.Length` 不同，立即回傳 `false`。
3. 逐位置讀取字元與單字。
4. 第一次出現時寫入正向與反向字典。
5. 已存在時，任一方向的既有值不相同就回傳 `false`。
6. 所有位置都沒有衝突才回傳 `true`。

主程式每次直接提供新的輸入值，輸出 `Expected`、`Actual` 與 `PASS-FAIL`，最後輸出總結。

## 逐步範例

輸入 `pattern = "abba"`、`s = "dog cat cat dog"`：

| 位置 | 字元 | 單字 | 正向字典 | 反向字典 |
|---:|:---:|---|---|---|
| 0 | a | dog | 新增 `a -> dog` | 新增 `dog -> a` |
| 1 | b | cat | 新增 `b -> cat` | 新增 `cat -> b` |
| 2 | b | cat | 與既有值相同 | 與既有值相同 |
| 3 | a | dog | 與既有值相同 | 與既有值相同 |

兩個方向始終一致，因此結果是 `true`。

## 正確性與不變量

迴圈處理完索引 `0..i` 後，正向字典記錄該前綴中每個字元唯一的單字，反向字典記錄每個單字唯一的字元。遇到衝突立即拒絕；若迴圈結束，所有位置同時滿足兩個唯一性條件，所以關係為雙射。

## 複雜度

令 `n` 為 pattern 長度。忽略切割及字串雜湊的內容成本時，平均時間複雜度為 `O(n)`，額外空間為 `O(n)`；切割字串另需保存單字陣列。

## 測試矩陣

| 案例 | 預期 |
|---|:---:|
| 標準雙射 | `true` |
| 同一字元對應不同單字 | `false` |
| 重複字元產生衝突 | `false` |
| 不同字元對應同一單字 | `false` |
| 字元數與單字數不同 | `false` |
| 單一對應 | `true` |
| 全部位置維持同一對應 | `true` |

## 完整執行輸出

```text
Case: 標準雙射
Expected: True
Actual: True
PASS-FAIL: PASS

Case: 同一字元對應不同單字
Expected: False
Actual: False
PASS-FAIL: PASS

Case: 重複字元產生衝突
Expected: False
Actual: False
PASS-FAIL: PASS

Case: 不同字元對應同一單字
Expected: False
Actual: False
PASS-FAIL: PASS

Case: 字元數與單字數不同
Expected: False
Actual: False
PASS-FAIL: PASS

Case: 單一對應
Expected: True
Actual: True
PASS-FAIL: PASS

Case: 全部位置維持同一對應
Expected: True
Actual: True
PASS-FAIL: PASS

Summary: 7/7 checks passed.
```

## 專案結構

```text
.
├── DictionaryEquals用法/
│   ├── DictionaryEquals用法.csproj
│   └── Program.cs
├── .vscode/
├── docs/readme-template.md
├── AGENTS.md
└── README.md
```

## 參考資料

- [LeetCode 290. Word Pattern](https://leetcode.com/problems/word-pattern/)
- [Microsoft Learn：Dictionary<TKey,TValue>](https://learn.microsoft.com/dotnet/api/system.collections.generic.dictionary-2)

## 已知限制

- smoke harness 是專案內建的整合檢查，不是獨立測試框架。
- API 遵循原題有效輸入契約；null、前後空白或多重分隔空白不在目前支援範圍。
