要使用 SQL 语法查看存储过程的内容，你可以使用以下几种方法：

### 1. 使用 `sp_helptext`
这个系统存储过程会返回存储过程的文本定义：

```sql
EXEC sp_helptext 'StoredProcedureName';
```

#### 示例：
```sql
EXEC sp_helptext 'dbo.MyStoredProcedure';
```

这个查询会返回存储过程的所有 T-SQL 代码。

### 2. 使用 `sys.sql_modules` 和 `sys.objects`
通过系统视图 `sys.sql_modules` 和 `sys.objects`，可以查看存储过程的内容：

```sql
SELECT 
    sm.definition 
FROM 
    sys.sql_modules AS sm
INNER JOIN 
    sys.objects AS o ON sm.object_id = o.object_id
WHERE 
    o.name = 'StoredProcedureName'
AND 
    o.type = 'P';
```

#### 示例：
```sql
SELECT 
    sm.definition 
FROM 
    sys.sql_modules AS sm
INNER JOIN 
    sys.objects AS o ON sm.object_id = o.object_id
WHERE 
    o.name = 'MyStoredProcedure'
AND 
    o.type = 'P';
```

- `o.type = 'P'` 表示只查询存储过程，如果你需要查询其他类型的对象，类型标识符可能不同。
  
### 3. 使用 `OBJECT_DEFINITION` 函数
`OBJECT_DEFINITION` 函数可以返回特定对象（如存储过程）的定义：

```sql
SELECT 
    OBJECT_DEFINITION(OBJECT_ID('StoredProcedureName')) AS ProcedureDefinition;
```

#### 示例：
```sql
SELECT 
    OBJECT_DEFINITION(OBJECT_ID('dbo.MyStoredProcedure')) AS ProcedureDefinition;
```

这些 SQL 语法可以让你方便地查询和查看存储过程的内容。




--------- 實際使用過可以查看的語法
-- 這個還保留原本的排版, 不用手動分行斷行
use DB

SELECT 
    OBJECT_DEFINITION(OBJECT_ID('StoredProcedureName')) AS ProcedureDefinition;