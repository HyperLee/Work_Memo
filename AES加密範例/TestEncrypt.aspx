<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestEncrypt.aspx.cs" Inherits="TestEncrypt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>AES128 加解密</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <font color="#FF0000">AES/CBC/128/PKCS7Padding加密解密</font>
        </br>
    <font color="#FF0000">key:AAES</font>
    </br>
    </br>
    輸入要加密字串: <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox> <asp:Button ID="Button1" runat="server" Text="加密" OnClick="Button1_Click" /></br>
    加密字串: <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    </br>
     
    </br>
    輸入解密字串: <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox> <asp:Button ID="Button2" runat="server" Text="解密" OnClick="Button2_Click" />
    </br>
    解密明文: <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
    </div>
    </form>
</body>
</html>
