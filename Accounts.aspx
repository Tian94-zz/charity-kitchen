<%@ Page Title="" Language="C#" MasterPageFile="~/CharityKitchen.Master" AutoEventWireup="true" CodeBehind="Accounts.aspx.cs" Inherits="CharityKitchen.Accounts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
     <asp:Label ID="lblHeading" runat="server" Font-Size="30px">Accounts</asp:Label>
    <table>
        <tr><td><asp:GridView ID="gvResult" runat="server" OnRowCommand="gvResult_RowCommand" OnRowDeleting="gvResult_RowDeleting">
            <Columns>
                <asp:ButtonField ButtonType="Button" Text="Select" CommandName="Select"/>
                <asp:ButtonField ButtonType="Button" Text="Delete" CommandName="Delete"/>
            </Columns>
            </asp:GridView>
            </td></tr>
        <tr><td><asp:Label ID="lblInfo" runat="server" /></td></tr>
    </table>
    <table>
        <tr>
            <td>User ID: <asp:Label ID="lblID" runat="server" Text="0"/></td>           
        </tr>
        <tr> <td>Username:</td><td> <asp:TextBox runat="server" ID="txtUsername" /></td></tr>
        <tr><td>First Name:</td><td> <asp:TextBox runat="server" ID="txtFirstName" /></td><td>Last Name: <asp:TextBox runat="server" ID="txtLastName" /></td></tr>
        <tr><td>Password:</td><td> <asp:TextBox runat="server" ID="txtPassword" /></td></tr>
        <tr><td>Role: </td><td><asp:DropDownList ID="drpRoles" runat="server" /></td><td>Access Level: <asp:DropDownList ID="drpAccessLevel" runat="server" /></td></tr>
        <tr><td><asp:Button ID="btnNew" runat="server" OnClick="btnNew_Click" Text="New"/></td><td></td><td></td><td><asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save"/></td></tr>
    </table>
    <asp:Button ID="btnShowHelp" runat="server" Text="Show Help" OnClick="btnShowHelp_Click"/>
    <asp:TextBox ID="txtHelp" runat="server" TextMode="MultiLine" Visible="false" Width="600px" ReadOnly="true"/>
</asp:Content>
