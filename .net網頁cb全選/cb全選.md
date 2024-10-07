c# .net 網頁 實作 checkbox 全選行為

-** aspx檔案 使用 javascript


```aspx
<script type="text/javascript">
    function cbfun(chkName) 
	{
        var i;
        var flag = false;
        if (document.getElementsByName(chkName)[0].checked) 
		{
            flag = true;
        }

		// 迴圈次數為 cb 數量
        for (i = 0; i <= 18; i++) 
		{
            var tempCheck = "ctl00$ContentPlaceHolder1$cbAType$" + i;
            document.getElementsByName(tempCheck)[0].checked = flag;
        }
    }
</script>
```
	
--** .cs檔案
```csharp
protected void Page_Load(object sender, EventArgs e)
{
	//全選
	this.chkAll.Attributes.Add("onclick", "cbfun('" + this.chkAll.UniqueID.Replace('_', '$').Trim() + "')");
}
```