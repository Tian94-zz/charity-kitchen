<%@ Page Title="" Language="C#" MasterPageFile="~/CharityKitchen.Master" AutoEventWireup="true" CodeBehind="Meals.aspx.cs" Inherits="CharityKitchen.Meals" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
     <asp:Label ID="lblHeading" runat="server" Font-Size="30px">Meals</asp:Label>
        <div style="padding-left: 50px">
    <table>
        <tr><td>Meal ID: </td><td style="width: 128px"><asp:Label runat="server" ID="lblMealID">*id</asp:Label></td>
            <td>Meal No.: <asp:Label ID="lblCurrentIndex" runat="server" /> of <asp:Label ID="lblTotalIndex" runat="server" /></td>
            <td><asp:Button runat="server" ID="btnNew" Text="Create New Meal" OnClick="btnNew_Click"/></td>           
        </tr>
        <tr> <td>Find Meal by ID: </td><td style="width: 128px"><asp:TextBox ID="txtSearchID" runat="server" Width="40px"/><asp:Button ID="btnSearchID" runat="server" Text="Search" /></td></tr>
        <tr><td>Meal Name: </td><td style="width: 128px"><asp:TextBox ID="txtName" runat="server" /></td>
            <td>Price ($): </td><td><asp:TextBox runat="server" ID="txtPrice" /></td>
        </tr>
        <tr><td></td></tr>
        <tr><td>Description: </td></tr>
        <tr class="spaceUnder"><td><asp:TextBox runat="server" ID="txtDescription" TextMode="MultiLine" /></td>
            <td style="width: 128px"></td>
        </tr>
    </table>
    <asp:Button ID="btnFirst" runat="server" Text="|<" Width="40px" OnClick="btnFirst_Click"/>
     <asp:Button ID="btnPrevious" runat="server" Text="<" Width="40px" OnClick="btnPrevious_Click"/>
     <asp:Button ID="btnNext" runat="server" Text=">" Width="40px" OnClick="btnNext_Click"/>
     <asp:Button ID="btnLast" runat="server" Text=">|" Width="40px" OnClick="btnLast_Click"/>
    <table>
        <tr><td></td></tr>
        <tr><td><asp:Label ID="lblInfo" runat="server" /></td></tr>
        <tr><td></td></tr>
        <tr><td>Ingredient: </td><td>Quantity: </td><td>Unit: </td></tr>
        <tr><td><asp:DropDownList runat="server" ID="drpIngredients" AutoPostBack="true" OnSelectedIndexChanged="drpIngredients_SelectedIndexChanged"/></td>
            <td><asp:TextBox runat="server" ID="txtQuantity" /></td>
            <td><asp:Label runat="server" ID="lblUnit" /></td>
            <td><asp:Button runat="server" ID="btnAddIngredient" Text="Add Ingredient" OnClick="btnAddIngredient_Click"/></td>
        </tr>
        <tr><td>Ingredients List: </td></tr>
        <tr>
            <td><asp:GridView ID="gvResults" runat="server" AutoGenerateColumns="False" OnRowDeleted="gvResults_RowDeleted" OnRowCommand="gvResults_RowCommand" OnRowDeleting="gvResults_RowDeleting" DataKeyNames="MealLineID">
                <Columns>
                    <asp:BoundField DataField="MealID" HeaderText="MealID" Visible="False" />
                    <asp:BoundField DataField="MealLineID" HeaderText="MealLineID" Visible="False" />
                    <asp:BoundField DataField="StockName" HeaderText="Ingredient Name" />
                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                    <asp:BoundField DataField="Shorthand" HeaderText="Unit" />
                    <asp:ButtonField ButtonType="Button" Text="Delete Ingredient" CommandName="Delete"/>
                </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr><td><asp:Button runat="server" ID="btnDelete" Text="Delete Meal" OnClick="btnDelete_Click"/></td>
            <td></td>
            <td></td>
            <td><asp:Button runat="server" ID="btnSave" Text="Save/Update" OnClick="btnSave_Click"/></td>
        </tr>
    </table>
    </div>
    <asp:Button ID="btnShowHelp" runat="server" Text="Show Help" OnClick="btnShowHelp_Click"/>
    <asp:TextBox ID="txtHelp" runat="server" TextMode="MultiLine" Visible="false" Width="600px" ReadOnly="true"/>
</asp:Content>
