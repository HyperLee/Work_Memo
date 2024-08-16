--https://dotblogs.com.tw/joysdw12/2011/05/31/26731

--更改欄位大小型態:
ALTER TABLE [TableName] ALTER COLUMN [ColumnName] [DataType]


--增加欄位:
ALTER TABLE [TableName] ADD [ColumnName] [DataType] Default [Value]


--欄位資料替換:
update [TableName]
set [ColumnName] = REPLACE([ColumnName], '原始文字', '要替換文字')
where [ColumnName] like '%原始文字%'
