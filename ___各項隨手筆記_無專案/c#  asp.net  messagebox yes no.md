在 C# ASP.NET Web 應用程序中，因為是基於Web的，所以無法像在桌面應用程序（Windows Forms 或 WPF）中那樣直接使用 `MessageBox` 顯示 Yes/No 的對話框。然而，你可以使用 JavaScript 在客戶端實現類似的功能，並根據用戶的選擇執行相應的後端代碼。

以下是一個實現這種功能的範例：

### 步驟 1：在 ASPX 頁面中加入 JavaScript
```aspx
<script type="text/javascript">
    function confirmAction() {
        var result = confirm("Are you sure you want to proceed?");
        return result; // 如果選擇了 "Yes"，返回 true；否則返回 false
    }
</script>
```

### 步驟 2：在 ASPX 頁面中的 Button 控制項上綁定 JavaScript
```aspx
<asp:Button ID="Button1" runat="server" Text="Delete" OnClientClick="return confirmAction();" OnClick="Button1_Click" />
```

### 步驟 3：在後端的 C# 中處理按鈕的 Click 事件
```csharp
protected void Button1_Click(object sender, EventArgs e)
{
    // 如果用戶選擇了 "Yes"，則會執行這段代碼
    // 這裡可以放置你需要執行的邏輯
    Response.Write("You clicked Yes.");
}
```

### 說明：
1. **JavaScript confirm() 函數**：這是一個標準的 JavaScript 函數，用於顯示一個帶有 "OK" 和 "Cancel" 按鈕的對話框。如果用戶選擇 "OK"，該函數返回 `true`；如果選擇 "Cancel"，返回 `false`。

2. **OnClientClick**：這個屬性用於綁定客戶端的 JavaScript 事件。在這個範例中，如果 `confirmAction()` 函數返回 `true`，表單會繼續提交，並觸發後端的 `Button1_Click` 事件；如果返回 `false`，表單提交會被阻止，後端的事件也不會被觸發。

這樣你就能實現類似 `MessageBox.Show("Yes/No")` 的功能，根據用戶的選擇執行不同的後端邏輯。