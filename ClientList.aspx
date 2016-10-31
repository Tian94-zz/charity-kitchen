<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientList.aspx.cs" Inherits="CharityKitchen.ClientList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <script type="text/javascript">
        function ResizeWindow()
        {
            var width = 700;
            var height = 400;
            window.resizeTo(width, height);
            window.moveTo(((screen.width - width) / 2), ((screen.height - height) / 2));            
        }
    </script>
</head>

<body onload="ResizeWindow()" style="background-color:antiquewhite">    
    <form id="frmMain" runat="server">
    <div style="border:double">
        <table>
            <tr><td>First Name: <asp:TextBox ID="txtFirstName" runat="server" /></td><td>Last Name: <asp:TextBox ID="txtLastName" runat="server" /><asp:Button ID="btnSearch" Text="Search" runat="server" OnClick="btnSearch_Click"/></td></tr>
        </table>
    <table>
        <tr><td><asp:GridView ID="gvResults" runat="server" /></td></tr>
        </table>
    </div>
    </form>
</body>
</html>
