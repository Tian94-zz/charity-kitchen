<%@ Page Title="" Language="C#" MasterPageFile="~/CharityKitchen.Master" AutoEventWireup="true" CodeBehind="GeneralError.aspx.cs" Inherits="CharityKitchen.GeneralError" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    There has been an error. The data you are looking for is un-reachable or doesn't exist.
    <br />
    Your error has been noted and logged to the administrator.
    <br />
    <br />
    <asp:HyperLink NavigateUrl="~/Default.aspx" runat="server" ID="hlRedirect">Click Here To Be Redirected Home</asp:HyperLink>
</asp:Content>
