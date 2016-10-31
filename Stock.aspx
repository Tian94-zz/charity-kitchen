<%@ Page Title="" Language="C#" MasterPageFile="~/CharityKitchen.Master" AutoEventWireup="true" CodeBehind="Stock.aspx.cs" Inherits="CharityKitchen.Stock" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <div style="border-bottom:groove; border-bottom-color:aqua;">
    <asp:Label ID="lblHeading" runat="server" Font-Size="30px">Stock</asp:Label>
        </div>
    <script type="text/javascript">
        function Maximise()
        {
            window.moveTo(0, 0);
            window.resizeTo(screen.width, screen.height);
        }
    </script>
    <div style="padding-left: 50px;">
        <table>
            <tr class="spaceUnder"><td>Stock Name: </td><td><asp:TextBox ID="txtStockName" runat="server"></asp:TextBox></td></tr>
            <tr class="spaceUnder"><td>Stock Quantity: </td><td><asp:TextBox ID="txtStockQuantity" runat="server" TextMode="Number"></asp:TextBox></td><td><asp:DropDownList ID="drpUnits" runat="server"></asp:DropDownList></td></tr>                      
        </table>
        <table>
            <tr><td>Stock Description: </td></tr>  
            <tr class="spaceUnder"><td><asp:TextBox Width="400px" TextMode="MultiLine" runat="server" ID="txtStockDescription"></asp:TextBox></td></tr>
             <tr class="spaceUnder">
             <td><asp:Button ID="btnClearFields" Text="Clear Fields" runat="server" OnClick="btnClearFields_Click"/></td>
                <td style="text-align:right"><asp:Button ID="btnAddStockItem" Text="Add Stock Item" runat="server" OnClick="btnAddStockItem_Click"/></td>
            </tr>
            <tr>
                <td></td>
                <td><asp:Label ID="lblStockID" runat="server">Stock ID: </asp:Label><asp:TextBox ID="txtStockID" runat="server" /><asp:Button ID="btnEditStock" runat="server" Text="Edit Stock" OnClick="btnEditStock_Click"/></td>
            </tr>
            <tr><td>Quick Edit Stock Quantity: </td></tr>
            <tr><td><asp:TextBox runat="server" ID="txtQuickEditID" Text="*Enter stock ID No.*"></asp:TextBox></td></tr>
            <tr>
                <td><asp:TextBox runat="server" ID="txtQuickEditUnits" Text="*Updated Quantity*"/>
                    <asp:Button runat="server" ID="btnQuickEdit" Text="Update Quantity" OnClick="btnQuickEdit_Click"/></td><td><asp:Label ID="lblStockName" runat="server" Text="Stock Name: ">
                         </asp:Label><asp:TextBox ID="txtSearch" runat="server"></asp:TextBox><asp:Button ID="btnSearch" Text="Search" runat="server" OnClick="btnSearch_Click"/>
                     </td>               
            </tr>
            <tr class="spaceUnder"> <td><asp:Label ForeColor="DarkRed" runat="server" ID="lblInfo" Text="*action information*"></asp:Label></td></tr>
        </table>
        
        <asp:GridView ID="gvResults" runat="server"></asp:GridView>
    </div>
    <asp:Button ID="btnShowHelp" runat="server" Text="Show Help" OnClick="btnShowHelp_Click"/>
    <asp:TextBox ID="txtHelp" runat="server" TextMode="MultiLine" Visible="false" Width="600px" ReadOnly="true"/>
</asp:Content>
