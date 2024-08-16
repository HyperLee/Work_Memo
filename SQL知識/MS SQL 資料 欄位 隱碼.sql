-- MS SQL 資料 欄位 隱碼 取代 個資 身分證 生日 姓名 

-- 文字隱碼 
use DBName
update TableName
set [ColumnName] = REPLACE([ColumnName], SUBSTRING(ColumnName, 2, 1), '**')


第二個字開始, 一個文字 取代為 * 
