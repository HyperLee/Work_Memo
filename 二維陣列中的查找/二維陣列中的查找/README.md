# Leetcode 240. 搜索二維矩陣 II

## 題目說明

給定一個 m x n 的整數矩陣 matrix，矩陣每一行從左到右遞增、每一列從上到下遞增。請判斷給定的目標值 target 是否存在於該矩陣中。

## 三種解法比較

### 1. 暴力法

- 逐行逐列檢查所有元素。
- 時間複雜度：O (m\*n)

### 2. 二分法

- 對每一行分別進行二分搜尋。
- 時間複雜度：O (m\*logn)
- **限制**：只能利用每一行有序，無法同時利用行與列的有序性。

### 3. 右上角法 (推薦)

- 從右上角 (或左下角) 開始，每次比較：
  - 若目標值小於當前值，往左移動 (減少列)。
  - 若目標值大於當前值，往下移動 (增加行)。
- 每次移動都能排除一整行或一整列，充分利用行與列的有序性。
- 時間複雜度：O (m+n)
- **優點**：
  - 能同時利用行與列的排序特性，搜尋空間縮小更快。
  - 程式碼簡潔，效能最佳。

## 為什麼右上角法比二分法更好？

- 1. 二分法的限制
     二分法只能用在一維有序陣列，但本題的二維陣列只保證「每一行」和「每一列」分別遞增，整體不是完全排序。
     若對每一行分別做二分搜尋，時間複雜度是 O (m\*logn)(m 為行數，n 為列數)，但每次只能縮小一行的搜尋範圍，無法同時利用行與列的有序性。
- 2. 右上角法的優勢
     從右上角 (或左下角) 開始，每次比較都能排除一整行或一整列，因為：
     若目標值比當前值小，往左 (減少列)；
     若目標值比當前值大，往下 (增加行)。
     這樣每一步都能有效利用行與列的排序特性，每次移動都能縮小搜尋空間。
     時間複雜度為 O (m+n)，通常比 O (m\*logn) 更快。
- 3. 實際效能
     右上角法在最壞情況下最多只需 m+n 步，且程式碼簡潔，適合這種「行列皆有序」的二維矩陣搜尋問題。
- 總結：
  右上角法能同時利用行與列的有序性，每次比較都能排除一整行或一整列，搜尋效率更高，因此是本題最佳解法。

## 程式碼結構

- `SearchMatrix`：暴力法
- `SearchMatrix_binary`：二分法
- `SearchMatrix_RightTop`：右上角法 (推薦)

---

本專案包含三種解法，建議優先使用右上角法，能有效提升搜尋效率。
