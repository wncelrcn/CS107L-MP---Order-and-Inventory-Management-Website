<%@ Page Title="" Language="C#" MasterPageFile="~/OrderNav.Master" AutoEventWireup="true" CodeBehind="Order.aspx.cs" Inherits="CS107L_MP.Order" %>
<asp:Content ID="OrderPage" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Hi, <%:Session["Username"]%>  </h1>
</asp:Content>
