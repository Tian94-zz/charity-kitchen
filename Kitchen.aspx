<%@ Page Title="" Language="C#" MasterPageFile="~/CharityKitchen.Master" AutoEventWireup="true" CodeBehind="Kitchen.aspx.cs" Inherits="CharityKitchen.Kitchen" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
     <asp:Label ID="lblHeading" runat="server" Font-Size="30px">Kitchen Orders</asp:Label>
    <div style="padding-left: 50px">       
    <table>
        <tr><td>Orders</td></tr>
        <tr>
            <td><asp:GridView runat="server" ID="gvResults"></asp:GridView></td>
            </tr>
        <tr class="spaceUnder">
            <td>Complete Order (enter order ID): <asp:TextBox Width="30px" ID="txtOrderNumber" runat="server" /></td><td><asp:Button ID="btnSubmitOrder" runat="server" Text="Submit" OnClick="btnSubmitOrder_Click"/></td>
        </tr>
    </table>
    <table>
        <tr><td>Order Meals</td></tr>
        <tr>
            <td><asp:GridView runat="server" ID="gvOrderMeals"></asp:GridView></td>
        </tr>
    </table>
</div>
    <asp:Button ID="btnShowHelp" runat="server" Text="Show Help" OnClick="btnShowHelp_Click"/>
    <asp:TextBox ID="txtHelp" runat="server" TextMode="MultiLine" Visible="false" Width="600px" ReadOnly="true"/>
</asp:Content>
