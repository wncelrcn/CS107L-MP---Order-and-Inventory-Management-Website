<%@ Page Title="" Language="C#" MasterPageFile="~/AdminNavBar.Master" AutoEventWireup="true" CodeBehind="ManageOrders.aspx.cs" Inherits="CS107L_MP.ManageOrders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        /* CSS styles here */
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

        .order-container {
            width: 80%;
            margin: 0 auto;
            padding: 20px;
            background-color: #f9f9f9;
            border-radius: 5px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
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
        }

        .order-date {
            text-align: right;
        }
    </style>

    <h1>Manage Orders</h1>
    <form id="form1" runat="server">
    <asp:Repeater ID="OrderRepeater" runat="server" OnItemDataBound="OrderRepeater_ItemDataBound">
        <ItemTemplate>
            <div class="order-item">
                <!-- Order details -->
                <div class="item-details">
                    <p>Transaction ID: <asp:Label ID="lblTransactionID" runat="server" Text='<%# Eval("TransactionID") %>'></asp:Label></p>
                    <p>Username: <asp:Label ID="lblUsername" runat="server" Text='<%# Eval("Username") %>'></asp:Label></p>
                    <p>Status: <asp:Label ID="lblCurrentStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label></p>
                    <p>Product Details:</p>
                    <asp:Repeater ID="ProductRepeater" runat="server">
                        <ItemTemplate>
                            <p><strong><%# Eval("ProductName") %></strong> Quantity: <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label></p>
                        </ItemTemplate>
                    </asp:Repeater>
                    <p>Total Price: <asp:Label ID="lblTotalPrice" runat="server" Text='<%# Eval("TotalPrice", "{0:C}") %>'></asp:Label></p>
                </div>
                <!-- Order status dropdown -->
                <div class="order-status">
                    <asp:DropDownList ID="ddlStatus" runat="server">
                        <asp:ListItem Text="Order Placed" Value="Order Placed"></asp:ListItem>
                        <asp:ListItem Text="In Transit" Value="In Transit"></asp:ListItem>
                        <asp:ListItem Text="Preparing to Ship" Value="Preparing to Ship"></asp:ListItem>
                        <asp:ListItem Text="Delivered" Value="Delivered"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btnUpdateStatus" runat="server" Text="Update Status" OnClick="btnUpdateStatus_Click" />
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
        </form>
</asp:Content>