# Backtracking（回溯法）專案開發規格書

## 1. 專案概述（Overview）

### 1.1 專案名稱
BackTrack — 回溯法（Backtracking）演算法教學與示範專案

### 1.2 專案目的
建立一個專門解說 **Backtracking（回溯法）** 與 **剪枝（Pruning）** 的 .NET Console 應用程式，做為學習與教學用途。專案需同時提供：

1. **可執行的 C# 範例程式碼**：六題經典回溯法題目，每題獨立檔案、附詳細註解、可由互動式選單觸發執行。
2. **完整的理論說明文件（README.md）**：包含定義、原理、應用場景、優缺點、複雜度分析、剪枝技巧、流程圖示，以及對應每題的解法說明。

### 1.3 目標讀者
- 想深入理解回溯法的學習者
- 演算法面試準備者
- 中文母語但需熟悉英文術語的工程師

### 1.4 設計原則
- **關注點分離**：理論說明寫在 `README.md`，程式碼只放「為何這樣寫」的精簡註解，不在 `.cs` 中堆砌長篇教學文字。
- **單一職責**：每個經典題目一個獨立 `.cs` 檔案，一個 class，便於閱讀與維護。
- **可重複執行**：使用者可從互動式選單中任意挑選題目反覆執行。

---

## 2. 技術棧（Tech Stack）

| 項目 | 選用 |
| --- | --- |
| 語言 | C# 14（latest） |
| Framework | .NET 10 |
| 專案類型 | Console Application（`OutputType=Exe`） |
| Nullable | `enable` |
| ImplicitUsings | `enable` |
| 測試 | 不納入（後續可擴充） |
| 外部套件 | 無（純 BCL） |

開發規範遵循 `.github/instructions/csharp.instructions.md`：file-scoped namespace、PascalCase、`is null` / `is not null`、`nameof`、必要時加 XML doc comment。

---

## 3. 專案結構（Project Structure）

```
BackTrack/
├── BackTrack.slnx
├── README.md                          ← 完整理論說明文件（新增）
└── BackTrack/
    ├── BackTrack.csproj
    ├── Program.cs                     ← 互動式選單入口
    └── Problems/                      ← 經典題目資料夾（新增）
        ├── IBacktrackingProblem.cs    ← 統一介面
        ├── NQueens.cs
        ├── Permutations.cs
        ├── Combinations.cs
        ├── Subsets.cs
        ├── SudokuSolver.cs
        └── GenerateParentheses.cs
```

---

## 4. 程式碼規格（Code Specification）

### 4.1 統一介面 `IBacktrackingProblem`
所有經典題目共用同一介面，方便 `Program.cs` 註冊與驅動。

```csharp
namespace BackTrack.Problems;

/// <summary>
/// 回溯法題目共通介面：每個題目實作此介面，便於選單統一呼叫。
/// </summary>
public interface IBacktrackingProblem
{
    /// <summary>選單顯示用的中文標題（含英文術語）。</summary>
    string Title { get; }

    /// <summary>一句話描述題目。</summary>
    string Description { get; }

    /// <summary>執行示範（自帶預設輸入），將解答輸出至 Console。</summary>
    void Run();
}
```

### 4.2 各題目程式檔規格

每個題目檔案統一的撰寫規範：

- **檔名 / 類別名**：對應題目英文名稱，例：`NQueens.cs` → `class NQueens : IBacktrackingProblem`
- **namespace**：`BackTrack.Problems`（file-scoped）
- **註解原則**：
  - 在 class 上方以 XML doc comment 簡述題目與解法主軸（含時間/空間複雜度）
  - 在遞迴主函式上方以 XML doc comment 說明參數意義與遞迴終止條件
  - 在「選擇 / 遞迴 / 撤銷」三個關鍵步驟以 `//` 行內註解標示
  - **不要**在程式碼中放大段教學說明（這些都進 README.md）
- **輸出**：
  - `Run()` 必須印出「題目標題」、「輸入參數」、「找到的解（前 N 個或全部）」、「解的總數」
  - 若解很多（如 Permutations(4) 有 24 解），可列印全部；Subsets 同理
  - Sudoku 印 9×9 棋盤；N-Queens 印每解的棋盤示意

### 4.3 各題目實作要點

| # | 題目 | 預設輸入 | 核心剪枝重點 | 預期輸出 |
| --- | --- | --- | --- | --- |
| 1 | **N-Queens 八皇后** | n = 8 | 行/主對角線/副對角線三組 `bool[]` 標記衝突 | 92 組解，列印前 3 組棋盤 |
| 2 | **Permutations 全排列** | nums = [1,2,3] | `used[]` 陣列避免重複選擇 | 6 個排列全部列印 |
| 3 | **Combinations 組合** | n = 4, k = 2 | `start` 指標避免重複組合 | C(4,2)=6 個組合 |
| 4 | **Subsets 子集** | nums = [1,2,3] | 每個元素「選 / 不選」二元決策樹 | 2³=8 個子集 |
| 5 | **Sudoku Solver 數獨求解** | 內建一題 9×9 局部數獨 | 行/列/3×3 宮的 `bool[,]` 標記 | 解出唯一解並列印棋盤 |
| 6 | **Generate Parentheses 括號生成** | n = 3 | `open < n`、`close < open` 兩個剪枝條件 | 5 種合法字串 |

### 4.4 `Program.cs` 互動式選單規格

```
=========================================
  Backtracking 回溯法 經典題目示範
=========================================
  1) N-Queens 八皇后 (n=8)
  2) Permutations 全排列 ([1,2,3])
  3) Combinations 組合 (n=4, k=2)
  4) Subsets 子集 ([1,2,3])
  5) Sudoku Solver 數獨求解
  6) Generate Parentheses 括號生成 (n=3)
  0) 離開
-----------------------------------------
請輸入編號：
```

行為規格：
- 使用 `Dictionary<int, IBacktrackingProblem>` 註冊題目，便於擴充。
- 輸入非數字或不存在的編號 → 顯示錯誤訊息後重新顯示選單。
- 執行完一題後 → 顯示「按任意鍵返回選單」並回到主選單。
- 輸入 `0` → 結束程式。
- 主迴圈包在 `try-catch` 中，例外訊息以友善方式顯示。
- 使用 `nameof`、pattern matching、switch expression 等 C# 14 慣用法。

---

## 5. README.md 規格（Documentation）

### 5.1 撰寫語言
**繁體中文為主，重要術語以「中文（English Term）」對照標示**。
範例：「回溯法（Backtracking）」、「剪枝（Pruning）」、「狀態空間樹（State Space Tree）」。

### 5.2 章節結構

文件需有清楚的章節區塊（使用 `##`、`###` 標題），目錄如下：

```
# Backtracking（回溯法）完整解說

## 目錄（Table of Contents）

## 1. 什麼是回溯法（What is Backtracking）
   1.1 定義（Definition）
   1.2 與暴力搜尋（Brute Force）的差異
   1.3 與分支限界法（Branch and Bound）/ DFS / 動態規劃的關係

## 2. 核心原理（Core Principle）
   2.1 狀態空間樹（State Space Tree）
   2.2 三步驟模型：選擇（Choose） → 遞迴（Explore） → 撤銷（Un-choose）
   2.3 通用模板（Pseudo Code Template）
   2.4 遞迴樹圖示（Mermaid / ASCII Diagram）

## 3. 剪枝（Pruning）
   3.1 為何需要剪枝
   3.2 常見剪枝策略：可行性剪枝、最優性剪枝、對稱性剪枝、記憶化剪枝
   3.3 剪枝對效能的影響（前後對照圖）

## 4. 應用場景（Use Cases）
   - 排列、組合、子集
   - 棋盤類問題（N-Queens、騎士巡邏）
   - 約束滿足問題（Sudoku、Crossword）
   - 路徑搜尋（迷宮、Word Search）
   - 字串分割（Palindrome Partitioning）

## 5. 優缺點（Pros & Cons）
   5.1 優點
   5.2 缺點
   5.3 適用 vs 不適用情境

## 6. 複雜度分析（Complexity Analysis）
   6.1 時間複雜度通用框架：O(分支數 ^ 深度)
   6.2 空間複雜度：遞迴堆疊 + 狀態紀錄

## 7. 經典題目與解法（Classic Problems & Solutions）
   每題包含：題目描述 → 解題思路 → 狀態定義 → 剪枝分析 → 時間/空間複雜度 → C# 程式碼連結 → 執行結果範例
   7.1 N-Queens 八皇后
   7.2 Permutations 全排列
   7.3 Combinations 組合
   7.4 Subsets 子集
   7.5 Sudoku Solver 數獨求解
   7.6 Generate Parentheses 括號生成

## 8. 學習路徑與延伸題目（Further Reading）
   - LeetCode 推薦題單
   - 進階主題：Dancing Links、Constraint Propagation、Iterative Deepening

## 9. 如何執行本專案（How to Run）
   - dotnet --version 需求
   - dotnet run 步驟
   - 互動式選單操作說明

## 10. 名詞對照表（Glossary）
   中英術語對照表格
```

### 5.3 圖示需求
- 第 2.4 節需有遞迴樹 / 狀態空間樹圖示，採 **Mermaid `graph TD`** 為主，並附 ASCII 備援版本。
- 第 7.1 節（N-Queens）至少要有一張棋盤示意圖（ASCII）。
- 第 3.3 節剪枝對照可用簡易表格或 ASCII 樹呈現。

### 5.4 複雜度標示
每題需明確列出：
- 時間複雜度（含最壞與平均）
- 空間複雜度
- 剪枝後的實際表現討論

### 5.5 程式碼片段
README 中可放重點程式片段（例如三步驟模板），但完整實作以連結指向 `BackTrack/Problems/*.cs` 為主，避免重複維護。

---

## 6. 註解規範（Commenting Guidelines）

| 位置 | 註解類型 | 內容 |
| --- | --- | --- |
| 類別 | `///` XML doc | 題目一句話描述 + 時空複雜度 |
| 公開方法 | `///` XML doc | 參數、回傳值、終止條件 |
| 遞迴函式內三步驟 | `//` 行內 | 標註「選擇 / 遞迴 / 撤銷」 |
| 剪枝條件 | `//` 行內 | 一行說明剪枝理由 |
| 私有輔助欄位 | 不必註解 | 命名清楚即可 |

**禁止**：
- 在 `.cs` 中以 `/* ... */` 寫整段教學文字
- 將 README 內容複製到程式碼註解中
- 過度註解顯而易見的程式行

---

## 7. 驗收標準（Acceptance Criteria）

- [ ] `dotnet build` 無錯誤、無警告（在 `Nullable=enable` 下）
- [ ] `dotnet run` 後出現互動式選單，輸入 1–6 各題目皆能正確執行並輸出預期結果
- [ ] 輸入 `0` 可正常離開；輸入非法值有錯誤提示
- [ ] 每個 `.cs` 檔案均符合 `.editorconfig` 與 `csharp.instructions.md`
- [ ] `README.md` 包含全部 10 章節，且各章節皆有實質內容
- [ ] README 包含至少一張 Mermaid 圖示與一張 ASCII 圖示
- [ ] 每題在 README 中皆有對應的時空複雜度與剪枝分析
- [ ] 中英術語對照表完整呈現

---

## 8. 開發步驟（Implementation Order）

1. 建立 `Problems/` 資料夾與 `IBacktrackingProblem` 介面
2. 依序實作六題：Permutations → Combinations → Subsets → GenerateParentheses → NQueens → SudokuSolver（由易到難）
3. 改寫 `Program.cs` 為互動式選單
4. `dotnet build` + `dotnet run` 驗證每題輸出
5. 撰寫 `README.md`（依第 5 節章節結構）
6. 最終驗收（依第 7 節）

---

## 9. 不在本次範圍（Out of Scope）

- 單元測試專案（xUnit）
- Benchmark / 效能測試
- 圖形化介面（GUI）
- 其他演算法（DP、Greedy）的對照實作
- 多語系（i18n）支援