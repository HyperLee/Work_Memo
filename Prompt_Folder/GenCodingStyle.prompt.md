# 使用這份文件來產 Coding Style 規格範本

根據現有的程式碼，請分析主要的程式設計風格和慣例。提供一份規則清單，並寫入 .github/instructions/coding-style.instructions.md 指示文件中。請尋找以下方面的慣例：
1.  **格式化**：縮排（tab 或 space）、引號風格（單引號或雙引號）。
2.  **命名慣例**：變數（camelCase）、類別（PascalCase）、私有欄位（底線前綴）。
3.  **錯誤處理**：例如，使用 try-catch、回傳 Nullable<T> 而非 null，或自訂例外類別。
4.  **日誌記錄實踐**：偏好的日誌函式庫（如 Serilog）、日誌級別和訊息格式。
5.  **測試方法**：偏好的測試框架（如 xUnit、NUnit）、測試風格（BDD vs. TDD）以及模擬（mocking，例如 Moq）策略。
6.  **測試方法**：偏好的測試框架（如 JUnit、Jest）、測試風格（BDD vs. TDD）以及模擬（mocking）策略。