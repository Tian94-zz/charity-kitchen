<%@ Page Title="" Language="C#" MasterPageFile="~/CharityKitchen.Master" AutoEventWireup="true" CodeBehind="Deliveries.aspx.cs" Inherits="CharityKitchen.Deliveries" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <asp:Label ID="lblHeading" runat="server" Font-Size="30px">Deliveries</asp:Label>
        <div style="padding-left: 50px">    
    <table>
        <tr><td><asp:Calendar ID="calDate" runat="server" OnSelectionChanged="calDate_SelectionChanged"></asp:Calendar></td></tr>
        <tr><td><asp:GridView runat="server" ID="gvResults" AutoGenerateColumns="False" >
            <Columns>
                <asp:BoundField DataField="OrderID" HeaderText="OrderID" />
                <asp:BoundField DataField="ClientFirstName" HeaderText="First Name" />
                <asp:BoundField DataField="ClientLastName" HeaderText="Last Name" />
                <asp:BoundField DataField="ClientPhoneNumber" HeaderText="Contact" />
                <asp:BoundField DataField="ClientAddress" HeaderText="Address" />
                <asp:BoundField DataField="StateName" HeaderText="State" />
                <asp:BoundField DataField="ClientSuburb" HeaderText="Suburb" />
                <asp:BoundField DataField="ClientPostcode" HeaderText="Postcode" />
            </Columns>
            </asp:GridView>
            </td></tr>
    </table>
            </div>
    <asp:Button ID="btnShowHelp" runat="server" Text="Show Help" OnClick="btnShowHelp_Click"/>
    <asp:TextBox ID="txtHelp" runat="server" TextMode="MultiLine" Visible="false" Width="600px" ReadOnly="true"/>
</asp:Content>
