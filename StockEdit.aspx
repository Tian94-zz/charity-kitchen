<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockEdit.aspx.cs" Inherits="CharityKitchen.StockEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <script type="text/javascript">
        function ResizeWindow()
        {
            var width = 700;
            var height = 300;
            window.resizeTo(width, height);
            window.moveTo(((screen.width - width) / 2), ((screen.height - height) / 2));            
        }
    </script>
</head>

<body onload="ResizeWindow()" style="background-color:antiquewhite">    
    <form id="frmMain" runat="server">
    <div>
    <table style="border:double">
        <tr>
            <td>Stock ID: </td><td><asp:Label runat="server" ID="lblStockID" Text="*stock id" /></td>
        </tr>
        <tr><td></td></tr>
        <tr>
            <td>Stock Name:</td>
            <td><asp:TextBox ID="txtStockName" runat="server"></asp:TextBox></td>
            <td>Quantity:</td>
            <td><asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox></td>
            <td><asp:DropDownList runat="server" ID="drpUnits" /></td>
        </tr>
        <tr>
            <td>Stock Description: </td>
        </tr>
        <tr><td><asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" /></td></tr>
        <tr><td></td></tr>
        <tr><td><asp:Button ID="btnDelete" Text="Delete Stock Item" runat="server" OnClick="btnDelete_Click"/></td>
            <td></td>
            <td></td><td></td>
            <td><asp:Button ID="btnSaveEdit" Text="Save Edit" runat="server" OnClick="btnSaveEdit_Click"/></td>
        </tr>
    </table>
        <asp:Label ID="lblInfo" runat="server" />
    </div>
    </form>
</body>
</html>
