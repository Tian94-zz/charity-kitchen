<%@ Page Title="" Language="C#" MasterPageFile="~/CharityKitchen.Master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="CharityKitchen.Orders1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <asp:Label ID="lblHeading" runat="server" Font-Size="30px">Orders</asp:Label>
    <table>
        <tr><td><asp:Button ID="btnNewOrder" runat="server" Text="New Order" OnClick="btnNewOrder_Click"/></td></tr>
        <tr><td><asp:GridView ID="gvResults" runat="server" OnRowCommand="gvResults_RowCommand" OnRowEditing="gvResults_RowEditing" OnRowDeleting="gvResults_RowDeleting">
            <Columns>
                <asp:ButtonField ButtonType="Button" Text="Edit" CommandName="Edit"/>
                <asp:ButtonField ButtonType="Button" Text="Delete" CommandName="Delete"/>
            </Columns>
            </asp:GridView></td></tr>
    </table>
    <asp:Button ID="btnShowHelp" runat="server" Text="Show Help" OnClick="btnShowHelp_Click"/>
    <asp:TextBox ID="txtHelp" runat="server" TextMode="MultiLine" Visible="false" Width="600px" ReadOnly="true"/>
</asp:Content>
