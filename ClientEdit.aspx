<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientEdit.aspx.cs" Inherits="CharityKitchen.ClientEdit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <script type="text/javascript">
        function ResizeWindow()
        {
            var width = 580;
            var height = 350;
            window.resizeTo(width, height);
            window.moveTo(((screen.width - width) / 2), ((screen.height - height) / 2));            
        }
    </script>
</head>

<body onload="ResizeWindow()" style="background-color:antiquewhite">    
    <form id="frmMain" runat="server">
    <div>
    <table style="border:double">
        <tr><td>Client ID: </td><td><asp:Label ID="lblClientID" runat="server" /></td><td><asp:Label ID="lblClientID2" runat="server" /></td></tr>
        <tr>
            <td>First Name:</td>
            <td><asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox></td>
            <td>Last Name:</td>
            <td><asp:TextBox ID="txtLastName" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>D.O.B.(dd/mm/yyyy):</td>
            <td ><asp:TextBox ID="txtDOB" TextMode="DateTime" runat="server" /></td>
        </tr>
        <tr>
                <td>Phone Number:</td>
                <td><asp:TextBox ID="txtPhoneNumber" runat="server"></asp:TextBox></td>
        </tr>
            <tr>
            <td>Email:</td>
            <td><asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Address:</td>
            <td><asp:TextBox ID="txtAddress" runat="server"></asp:TextBox></td>
            <td>State:</td>
            <td><asp:DropDownList ID="drpState" runat="server"></asp:DropDownList></td>
        </tr> 
        <tr>
            <td>Suburb:</td>
            <td><asp:TextBox ID="txtSuburb" runat="server"></asp:TextBox></td>
            <td>Postcode:</td>
            <td><asp:TextBox TextMode="Number" ID="txtPostcode" runat="server"></asp:TextBox></td>
        </tr> 
        <tr style="text-align:right">
            <td></td><td></td><td></td>
            <td><asp:Button ID="btnSaveEdit" Text="Save Edit" runat="server" OnClick="btnSaveEdit_Click"></asp:Button></td>
        </tr>
        <tr> 
            <td><asp:Button ID="btnDeleteClient" Text="Delete Client" runat="server" OnClick="btnDeleteClient_Click"></asp:Button></td>
        </tr>
        <tr><td style="color:red; font-size:20px"><asp:Label ID="lblUserMessage" Text="" runat="server"></asp:Label></td></tr>
    </table>
    </div>
    </form>
</body>
</html>
