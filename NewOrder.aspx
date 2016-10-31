<%@ Page Title="" Language="C#" MasterPageFile="~/CharityKitchen.Master" AutoEventWireup="true" CodeBehind="NewOrder.aspx.cs" Inherits="CharityKitchen.Orders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <asp:Label ID="lblHeading" runat="server" Font-Size="30px">New Order</asp:Label>
    <div style="padding-left:50px">       
    <asp:ScriptManager ID="ScriptMaster" runat="server" />
        <asp:UpdatePanel runat="server" ID="udpOrder" updatemode="Conditional">
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="btnClientByID" />
            <asp:AsyncPostBackTrigger ControlID="btnConfirmOrder" />
        </Triggers>            
            <ContentTemplate>
                <table style="width: 500px">
                    <tr><td>Order ID: </td><td><asp:Label ID="lblID" runat="server"></asp:Label></td></tr>
                    <tr><td>Current Date: </td><td><asp:Label ID="lblCurrentDate" runat="server" /></td></tr>
                    <tr><td>Delivery Date: </td><td><asp:Calendar runat="server" ID="calDelDate" /></td></tr>
                    <tr class="spaceUnder"><td></td><td><asp:Label ID="lblDeliveryInfo" runat="server" /></td></tr>
                    <tr><td>Client ID: </td><td><asp:TextBox ID="txtID" runat="server" /><asp:Button ID="btnClientByID" runat="server" Text="Submit" OnClick="btnClientByID_Click"/></td><td><asp:Button ID="btnClientList" runat="server" Text="Client List" OnClick="btnClientList_Click"/></td></tr>
                    <tr><td></td><td><asp:Label ID="lblInfo" runat="server" Text="Information"/></td></tr>
                    <tr><td>First Name: </td><td><asp:Label runat="server" ID="lblClientFirstName" Text="First" /></td><td>Last Name: <asp:Label runat="server" ID="lblClientLastName" Text="Last"/></td></tr>
                    <tr class="spaceUnder"><td></td></tr>
                </table>         
            </ContentTemplate>
        </asp:UpdatePanel>
    <asp:UpdatePanel runat="server" ID="udpOrderLines" UpdateMode="Conditional">
        <Triggers>

        </Triggers>
        <ContentTemplate>
            <table style="width: 500px">
                <tr class="spaceUnder"><td>Meal: <asp:DropDownList ID="drpMeals" runat="server" OnSelectedIndexChanged="drpMeals_SelectedIndexChanged" AutoPostBack="true"/></td><td></td><td>Price: <asp:Label runat="server" ID="lblPrice" /></td><td><asp:Button ID="btnAddToOrder" runat="server" Text="Add To Order" OnClick="btnAddToOrder_Click"/></td></tr>
                <tr><td>Order Items:</td></tr>
                <tr><td><asp:GridView ID="gvResults" runat="server" OnRowCommand="gvResults_RowCommand" OnRowDeleting="gvResults_RowDeleting">
                    <Columns>
                        <asp:ButtonField ButtonType="Button" Text="Delete" CommandName="Delete"/>
                    </Columns>
                    </asp:GridView>
                    </td></tr>
                <tr><td><asp:Button ID="btnCancelOrder" runat="server" Text="Cancel Order" OnClick="btnCancelOrder_Click"/></td><td></td><td>Order Total($): <asp:Label ID="lblOrderTotal" runat="server" Text="0"/></td><td><asp:Button ID="btnConfirmOrder" runat="server" Text="Confirm Order" OnClick="btnConfirmOrder_Click"/></td></tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
        </div>
    <asp:Button ID="btnShowHelp" runat="server" Text="Show Help" OnClick="btnShowHelp_Click"/>
    <asp:TextBox ID="txtHelp" runat="server" TextMode="MultiLine" Visible="false" Width="600px" ReadOnly="true"/>
</asp:Content>
