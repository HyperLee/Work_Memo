# LeetCode 290. Word Pattern — Dictionary 雙向對應解法

使用 C# `Dictionary.Equals` 搭配雙向 Dictionary 實現 **Word Pattern** 問題，展示如何透過雙射 (bijection) 驗證 pattern 與字串之間的對應關係。

## 題目說明

> 給定一個 `pattern` 字串與一個以空白分隔的字串 `s`，判斷 `s` 中的單字是否**完全遵循** `pattern` 的對應規則。
>
> 所謂「完全遵循」是指 pattern 中的字元與 s 中的單字之間存在**雙射 (bijection)** 關係：
> - 每個 pattern 字元對應到**唯一一個**單字
> - 每個單字也只對應到**唯一一個** pattern 字元

### 範例

| pattern | s | 結果 | 原因 |
|---------|---|------|------|
| `"abba"` | `"dog cat cat dog"` | `true` | a↔dog, b↔cat，雙向一致 |
| `"abba"` | `"dog cat cat fish"` | `false` | a 先對應 dog，後出現 fish，衝突 |
| `"aaaa"` | `"dog cat cat dog"` | `false` | a 先對應 dog，後出現 cat，衝突 |
| `"abba"` | `"dog dog dog dog"` | `false` | a→dog、b→dog，不同字元對應到同一個 word |

### 限制條件

- `1 <= pattern.length <= 300`
- `pattern` 只包含小寫英文字母
- `1 <= s.length <= 3000`
- `s` 只包含小寫英文字母和空白，且不包含前導或尾隨空白
- `s` 中每個單字以**單一空白**分隔

## 解題概念與出發點

### 核心觀察

直覺上，我們需要驗證「pattern 字元 ↔ word」的**一對一**對應。如果只建立**單方向**的對應（例如只有 char → word），會漏掉反向衝突的情況：

```
pattern = "abba", s = "dog dog dog dog"
```

若只檢查 char → word：a→dog ✓, b→dog ✓（b 是新字元，建立新對應），但此時 `dog` 同時對應到 `a` 和 `b`，違反雙射。

### 解決方案：雙向 Dictionary

建立**兩個 Dictionary** 互相驗證：

| Dictionary | Key | Value | 作用 |
|------------|-----|-------|------|
| `dic1` | `char`（pattern 字元） | `string`（word） | 確保同一字元只對應同一個 word |
| `dic2` | `string`（word） | `char`（pattern 字元） | 確保同一個 word 只對應同一字元 |

這樣就形成了嚴格的**雙射**驗證。

## 解法詳細說明

```csharp
public bool WordPattern(string pattern, string s)
{
    // 1. 建立雙向 Dictionary
    Dictionary<char, string> dic1 = new Dictionary<char, string>();   // char → word
    Dictionary<string, char> dic2 = new Dictionary<string, char>();   // word → char

    int length = pattern.Length;
    string[] arr = s.Split(new char[] { ' ' });

    // 2. 長度不同，直接不匹配
    if (arr.Length != length)
        return false;

    // 3. 逐一比對每個位置
    for (int i = 0; i < length; i++)
    {
        char c = pattern[i];
        string word = arr[i];

        // 正向檢查：char → word
        if (!dic1.ContainsKey(c))
            dic1.Add(c, word);              // 第一次見到此字元，建立對應
        else if (!dic1[c].Equals(word))
            return false;                   // 同一字元對應到不同 word → 衝突

        // 反向檢查：word → char
        if (!dic2.ContainsKey(word))
            dic2.Add(word, c);              // 第一次見到此 word，建立對應
        else if (!dic2[word].Equals(c))
            return false;                   // 同一 word 對應到不同字元 → 衝突
    }

    return true;
}
```

### 時間與空間複雜度

- **時間複雜度**：$O(n)$，其中 $n$ 為 pattern 長度，每個位置只做常數時間的 Dictionary 查詢
- **空間複雜度**：$O(n)$，兩個 Dictionary 最多各存 $n$ 個 key-value pair

## 舉例演示流程

以 `pattern = "abba"`, `s = "dog cat cat dog"` 為例：

### 步驟一：初始化

```
dic1 = {}          // char → word
dic2 = {}          // word → char
arr  = ["dog", "cat", "cat", "dog"]
```

### 步驟二：i = 0, c = 'a', word = "dog"

```
dic1 不含 'a' → 加入 dic1['a'] = "dog"
dic2 不含 "dog" → 加入 dic2["dog"] = 'a'

dic1 = { 'a': "dog" }
dic2 = { "dog": 'a' }
```

### 步驟三：i = 1, c = 'b', word = "cat"

```
dic1 不含 'b' → 加入 dic1['b'] = "cat"
dic2 不含 "cat" → 加入 dic2["cat"] = 'b'

dic1 = { 'a': "dog", 'b': "cat" }
dic2 = { "dog": 'a', "cat": 'b' }
```

### 步驟四：i = 2, c = 'b', word = "cat"

```
dic1 含 'b' → dic1['b'] = "cat" == "cat" ✓ 通過
dic2 含 "cat" → dic2["cat"] = 'b' == 'b' ✓ 通過
```

### 步驟五：i = 3, c = 'a', word = "dog"

```
dic1 含 'a' → dic1['a'] = "dog" == "dog" ✓ 通過
dic2 含 "dog" → dic2["dog"] = 'a' == 'a' ✓ 通過
```

### 結果：所有位置通過雙向檢查 → 回傳 `true`

---

### 反例演示：`pattern = "abba"`, `s = "dog dog dog dog"`

| 步驟 | c | word | dic1 檢查 | dic2 檢查 | 結果 |
|------|---|------|----------|----------|------|
| i=0 | a | dog | 新增 a→dog | 新增 dog→a | ✓ |
| i=1 | b | dog | 新增 b→dog | dog→**a** ≠ **b** | **false** |

> `dic2["dog"]` 已經是 `'a'`，但現在期望對應到 `'b'`，產生**反向衝突**，立即回傳 `false`。

## 執行方式

```bash
dotnet run --project DictionaryEquals用法/DictionaryEquals用法.csproj
```

## 參考資源

- [LeetCode 290. Word Pattern](https://leetcode.com/problems/word-pattern/)
