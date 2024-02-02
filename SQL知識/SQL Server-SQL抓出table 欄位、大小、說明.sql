use DBName

SELECT a.Table_schema +'.'+a.Table_name   as 表格名稱   
       ,b.COLUMN_NAME                     as 欄位名稱   
       ,b.DATA_TYPE                       as 資料型別   
       ,isnull(b.CHARACTER_MAXIMUM_LENGTH,'') as 長度   
       ,isnull(b.COLUMN_DEFAULT,'')           as 預設值   
       ,b.IS_NULLABLE                         as 是否允許空值   
       ,( SELECT value   
          FROM fn_listextendedproperty (NULL, 'schema', a.Table_schema, 'table', a.TABLE_NAME, 'column', default)   
          WHERE name='MS_Description' and objtype='COLUMN'    
          and objname Collate Chinese_Taiwan_Stroke_CI_AS = b.COLUMN_NAME   
        ) as 欄位描述   
FROM INFORMATION_SCHEMA.TABLES  a   
 LEFT JOIN INFORMATION_SCHEMA.COLUMNS b ON a.TABLE_NAME = b.TABLE_NAME   
WHERE TABLE_TYPE='BASE TABLE'
and a.TABLE_NAME = 'TableName' -- 要查特定table name, 不用可以註解 查詢全部
ORDER BY a.TABLE_NAME , b.ORDINAL_POSITION



