---
mode: 'ask'
---

# do step by step:


1. 在對話訊息提醒先手動搜尋(第一個搜尋條件)，並提示

請在對話中手動提供

```plaintext
#searchResults
```

2. 對話提供 #searchResults 後，印出列表

 - 將所有檔案列表(專案根目錄起的相對路徑)，格式如下：
 
  ```
  1. ./src/components/header.php
  2. ./src/utils/helper.php
  3. ./src/views/home.php
  4. ...
  ```
  
 - 等待我確認搜尋結果。

3. 生成開啟檔案的命令

 - 根據搜尋結果，按檔案搜尋先後順序依序最多開啟 10 個檔案。
 
 生成如下格式的命令並顯示在終端中供我執行：

  ```powershell
  code-insiders ./src/components/header.php ./src/utils/helper.php ./src/views/home.php
  ```

4. 等候我下一步指示，再給我開啟下 10 個檔案的 powershell 指令。