<%@ Page Title="Manage Orders" Language="C#" MasterPageFile="~/AdminNavBar.Master" AutoEventWireup="true" CodeBehind="ManageOrders.aspx.cs" Inherits="CS107L_MP.ManageOrders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        /* CSS styles here */
        @import url('https://fonts.googleapis.com/css2?family=Poppins:wght@400;600&display=swap');

        body {
            font-family: 'Poppins', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f3f3f3;
            margin: 0;
            padding: 0;
        }
        
        .container {
            max-width: 1200px;
            margin: 0 auto;
            padding: 20px;
        }

        h1 {
            font-size: 36px;
            color: #333;
            margin-bottom: 30px;
        }

        .order-container {
            background-color: #fff;
            border-radius: 5px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
            padding: 20px;
            margin-bottom: 20px;
        }

        .order-item {
            border-bottom: 1px solid #ccc;
            padding: 20px 0;
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

        .item-details {
            flex-grow: 1;
            margin-right: 20px; /* Adjust spacing between details and order date */
        }

        .item-details p {
            margin: 5px 0;
            font-size: 18px;
            color: #555;
        }

        .order-status {
            text-align: center;
        }

        .order-status .btnUpdateStatus {
            background-color: #009688;
            color: #fff;
            border: none;
            padding: 8px 16px;
            border-radius: 4px;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }

        .order-status .btnUpdateStatus:hover {
            background-color: #00796b;
        }

        .order-status select {
            padding: 8px 12px;
            border: 1px solid #ccc;
            border-radius: 4px;

        }

        .order-sort {
            margin-bottom: 20px;
        }

        .order-sort label {
            font-weight: bold;
            margin-right: 10px;
        }

        .order-sort select {
            padding: 8px 12px;
            border: 1px solid #ccc;
            border-radius: 4px;
        }

    </style>

    <div class="container">
        <h1>Manage Orders</h1>
        <form id="form1" runat="server">
             <div class="order-sort">
                <asp:Label ID="lblSortBy" runat="server" AssociatedControlID="ddlOrderStatus">Sort by Status:</asp:Label>
                <asp:DropDownList ID="ddlOrderStatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOrderStatus_SelectedIndexChanged">
                    <asp:ListItem Text="All Orders" Value=""></asp:ListItem>
                    <asp:ListItem Text="Order Placed" Value="Order Placed"></asp:ListItem>
                    <asp:ListItem Text="Preparing to Ship" Value="Preparing to Ship"></asp:ListItem>
                    <asp:ListItem Text="In Transit" Value="In Transit"></asp:ListItem>
                    <asp:ListItem Text="Delivered" Value="Delivered"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <asp:Repeater ID="OrderRepeater" runat="server" OnItemDataBound="OrderRepeater_ItemDataBound">
                <ItemTemplate>
                    <div class="order-container">
                        <div class="order-item">
                            <!-- Order details -->
                            <div class="item-details">
                                <p><strong>Transaction ID:</strong> <asp:Label ID="lblTransactionID" runat="server" Text='<%# Eval("TransactionID") %>'></asp:Label></p>
                                <p><strong>Username:</strong> <asp:Label ID="lblUsername" runat="server" Text='<%# Eval("Username") %>'></asp:Label></p>
                                <p><strong>Status:</strong> <asp:Label ID="lblCurrentStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label></p>
                                <p><strong>Product Details:</strong></p>
                                <asp:Repeater ID="ProductRepeater" runat="server">
                                    <ItemTemplate>
                                        <p><strong><%# Eval("ProductName") %></strong> Quantity: <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label></p>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <p><strong>Total Price:</strong> <asp:Label ID="lblTotalPrice" runat="server" Text='<%# Eval("TotalPrice", "{0:C}") %>'></asp:Label></p>
                            </div>
                            <!-- Order status dropdown -->
                            <div class="order-status">
                                <asp:DropDownList ID="ddlStatus" runat="server">
                                    <asp:ListItem Text="Order Placed" Value="Order Placed"></asp:ListItem>
                                    <asp:ListItem Text="Preparing to Ship" Value="Preparing to Ship"></asp:ListItem>
                                    <asp:ListItem Text="In Transit" Value="In Transit"></asp:ListItem>
                                    <asp:ListItem Text="Delivered" Value="Delivered"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:Button ID="btnUpdateStatus" runat="server" CssClass="btnUpdateStatus" Text="Update Status" OnClick="btnUpdateStatus_Click" />
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </form>
    </div>
</asp:Content>
