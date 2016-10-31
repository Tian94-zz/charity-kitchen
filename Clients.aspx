<%@ Page Title="" Language="C#" MasterPageFile="~/CharityKitchen.Master" AutoEventWireup="true" CodeBehind="Clients.aspx.cs" Inherits="CharityKitchen.Clients" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <asp:Label ID="lblHeading" runat="server" Font-Size="30px">Clients</asp:Label>
    <script type="text/javascript">
        function Maximise()
        {
            window.moveTo(0, 0);
            window.resizeTo(screen.width, screen.height);
        }
    </script>
        <div style="padding-left: 50px">    
        <table>
        <tr>
            <td>First Name:</td>
            <td><asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox></td>
            <td>Last Name:</td>
            <td><asp:TextBox ID="txtLastName" runat="server"></asp:TextBox></td>
        </tr>
        <tr class="spaceUnder">
            <td>D.O.B.(dd/mm/yyyy):</td>
            <td ><asp:TextBox ID="txtDOB" TextMode="Date" runat="server" /></td>
        </tr>
        <tr>
                <td>Phone Number:</td>
                <td><asp:TextBox ID="txtPhoneNumber" TextMode="Number" runat="server"></asp:TextBox></td>
        </tr>
            <tr class="spaceUnder">
            <td>Email:</td>
            <td><asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Address:</td>
            <td><asp:TextBox ID="txtAddress" runat="server"></asp:TextBox></td>
            <td>State:</td>
            <td><asp:DropDownList ID="drpState" runat="server"></asp:DropDownList></td>
        </tr> 
        <tr class="spaceUnder">
            <td>Suburb:</td>
            <td><asp:TextBox ID="txtSuburb" runat="server"></asp:TextBox></td>
            <td>Postcode:</td>
            <td><asp:TextBox TextMode="Number" ID="txtPostcode" runat="server"></asp:TextBox></td>
        </tr> 
            <tr class="spaceUnder">
             <td><asp:Button ID="btnClearFields" Text="Clear Fields" runat="server" OnClick="btnClearFields_Click"/></td>
                <td></td>
                <td><asp:Label ID="lblInfo" runat="server" /></td>
                <td style="text-align:right"><asp:Button ID="btnAddNewClient" Text="Add New Client" runat="server" OnClick="btnAddNewClient_Click"/></td>
            </tr>
            <tr>
               <td>
                </td>
                <td></td>
                <td>Client ID:</td>
                <td><asp:TextBox ID="txtClientID" runat="server" /> <asp:Button ID="btnEditClient" Text="Edit Client" runat="server" OnClick="btnEditClient_Click" /></td>
            </tr>
             <tr>
               <td>
                </td>
                <td></td>
                 <td></td>
                <td style="color:red"><asp:Label ID="lblClientIDErrors" runat="server" /></td>
            </tr>
            <tr>
                <td>First Name:</td>
                <td><asp:TextBox ID="txtFirstNameSearch" runat="server" /></td>
                <td>Last Name:</td>
                <td style="text-align:right"><asp:TextBox ID="txtLastNameSearch" runat="server"/><asp:Button ID="btnSearch" Text="Search" runat="server" OnClick="btnSearch_Click" /></td>                
            </tr>
    </table>
    <asp:GridView ID="gvClientList" runat="server">
    </asp:GridView>
    </div>

    <asp:Button ID="btnShowHelp" runat="server" Text="Show Help" OnClick="btnShowHelp_Click"/>
    <asp:TextBox ID="txtHelp" runat="server" TextMode="MultiLine" Visible="false" Width="600px" ReadOnly="true"/>

</asp:Content>
