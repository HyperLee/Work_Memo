在 C# ASP.NET 中，你可以使用 JavaScript 判斷 `CheckBox` 是否被勾選。這通常用於在提交表單之前進行驗證，或在用戶與頁面交互時做出動態反應。

### 步驟 1：添加 JavaScript 函數來檢查 `CheckBox` 的狀態

在你的 ASPX 頁面中，可以編寫一個 JavaScript 函數來判斷 `CheckBox` 是否被勾選：

```html
<script type="text/javascript">
    function isChecked() {
        var checkBox = document.getElementById('<%= CheckBox1.ClientID %>');
        if (checkBox.checked) {
            alert("Checkbox is checked.");
        } else {
            alert("Checkbox is not checked.");
        }
    }
</script>
```

### 步驟 2：在 ASPX 頁面中創建一個 `CheckBox` 控制項和一個按鈕

```aspx
<asp:CheckBox ID="CheckBox1" runat="server" Text="I agree to the terms" />
<br />
<asp:Button ID="Button1" runat="server" Text="Submit" OnClientClick="isChecked(); return false;" />
```

### 說明：
1. **document.getElementById**：用於獲取 `CheckBox` 控制項的元素。`<%= CheckBox1.ClientID %>` 是為了確保 JavaScript 正確地找到 ASP.NET 控制項的實際 `ID`，因為 ASP.NET 會自動生成唯一的 `ID`。

2. **checkBox.checked**：這是 JavaScript 的一個屬性，用來判斷 `CheckBox` 是否被勾選。`checked` 屬性是布爾值，當 `CheckBox` 被勾選時，它返回 `true`，否則返回 `false`。

3. **OnClientClick**：在按鈕的 `OnClientClick` 屬性中調用 `isChecked()` 函數。`return false;` 的作用是阻止按鈕的默認行為（即阻止提交表單），這樣可以僅執行 JavaScript 函數而不會提交表單。

### 可選的：進一步的驗證

如果你希望在 `CheckBox` 未勾選時阻止表單提交，可以這樣做：

```html
<script type="text/javascript">
    function validateCheckBox() {
        var checkBox = document.getElementById('<%= CheckBox1.ClientID %>');
        if (!checkBox.checked) {
            alert("Please agree to the terms before submitting.");
            return false; // 阻止表單提交
        }
        return true; // 允許表單提交
    }
</script>
```

然後在 `Button` 的 `OnClientClick` 中使用這個函數：

```aspx
<asp:Button ID="Button1" runat="server" Text="Submit" OnClientClick="return validateCheckBox();" />
```

### 說明：
- 如果 `CheckBox` 沒有被勾選，`validateCheckBox()` 函數會顯示提示並返回 `false`，阻止表單提交。
- 如果 `CheckBox` 被勾選，函數返回 `true`，允許表單提交並執行後端的 `Button1_Click` 事件。

這樣，你就能在用戶提交表單前，利用 JavaScript 檢查 `CheckBox` 是否被勾選並作出相應處理。