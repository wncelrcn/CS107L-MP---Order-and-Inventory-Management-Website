<%@ Page Title="" Language="C#" MasterPageFile="~/OrderNav.Master" AutoEventWireup="true" CodeBehind="MyOrders.aspx.cs" Inherits="CS107L_MP.MyOrders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        @import url('https://fonts.googleapis.com/css2?family=Poppins:wght@400;600&display=swap');

        body {
             font-family: 'Poppins', Tahoma, Geneva, Verdana, sans-serif;
         }
        
        h1 {
            font-size: 36px;
            color: #333;
            margin-bottom: 30px;
            text-align: left;
            margin-left: 5.5rem;
        }
        
    </style>
    <h1><%:Session["Username"]%>'s Orders</h1>

   
</asp:Content>
