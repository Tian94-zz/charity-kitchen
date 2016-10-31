<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CharityKitchen.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="frmMain" runat="server">
    <div>
        <table>
   <tr><td>Username: </td><td> <asp:TextBox ID="txtUsername" runat="server" /></td></tr>
   <tr><td>Password: </td><td> <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"/></td></tr>            
            </table>
        <table>
            <tr><td style="padding-left:150px"><asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click"/></td></tr>
            <tr><td><asp:Label ID="lblInfo" runat="server"></asp:Label></td></tr>
        </table>
        </div>
    </form>
</body>
</html>
