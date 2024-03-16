<%@ Page Title="Admin Panel" Language="C#" MasterPageFile="~/AdminNavBar.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="CS107L_MP.Admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        /* CSS styles here */
        @import url('https://fonts.googleapis.com/css2?family=Poppins:wght@400;600&display=swap');

        body {
            font-family: 'Poppins', sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
        }

        .container {
            max-width: 800px;
            margin: 100px auto;
            padding: 40px;
            background-color: #fff;
            border-radius: 10px;
            box-shadow: 0 5px 15px rgba(0, 0, 0, 0.1);
        }

        h1 {
            text-align: center;
            color: #333;
            font-size: 36px;
            margin-bottom: 20px;
        }

        p {
            text-align: center;
            color: #666;
            font-size: 18px;
            margin-top: 20px;
        }

        .btn {
            display: inline-block;
            padding: 12px 24px;
            background-color: #009688;
            color: #fff;
            text-decoration: none;
            border-radius: 5px;
            transition: background-color 0.3s ease;
        }

        .btn:hover {
            background-color: #00796b;
        }

        .admin-icon {
            display: block;
            margin: 0 auto 30px;
            width: 120px;
        }
    </style>

    <div class="container">
        <img src="/Pics/JLR%20Food%20Trading.png" alt="JLR Food Products Trading" class="admin-icon">
        <h1>Welcome to Admin Panel</h1>
        <p>You have access to manage various aspects such as your Orders, Inventory, and Sales.</p>
        <div style="text-align: center;">
            <a href="ManageOrders.aspx" class="btn">Manage your Orders</a>
            <a href="ManageInventory.aspx" class="btn">Manage Inventory</a>
            <a href="ViewSales.aspx" class="btn">View your Sales</a>
        </div>
    </div>
</asp:Content>
