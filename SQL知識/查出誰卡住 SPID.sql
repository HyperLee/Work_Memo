-- https://www.uuu.com.tw/Public/content/article/20/20201207.htm
use DB

 SELECT r.session_id,
    r.status AS [指令狀態],
    r.command AS [指令類型],
    r.wait_time/1000.0 AS [等待時間(秒)],
    s.client_interface_name AS [連線資料庫的驅動程式],
    s.host_name AS [電腦名稱],
    s.program_name AS [執行程式名稱],
    t.text AS [執行的SQL語法],
    r.blocking_session_id AS [被鎖定卡住的session_id]
    FROM sys.dm_exec_requests r
    INNER JOIN sys.dm_exec_sessions s
    ON r.session_id = s.session_id
    CROSS APPLY sys.dm_exec_sql_text(r.sql_handle) t
    WHERE s.is_user_process = 1;
	
	
-- 查出誰卡住 SPID, 然後用 KILL ID 就可以
-- 砍掉招鎖定的SPID
KILL <SPID>