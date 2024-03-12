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

    <h1>My Orders</h1>
    <div class="order-container">
        <asp:Repeater ID="OrderRepeater" runat="server">
            <ItemTemplate>
                <div class="order-item">
                    <div class="item-details">
                        <p><strong><%# Eval("ProductName") %></strong></p>
                        <p>Quantity: <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label></p>
                        <p>Price per item: <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("Price", "{0:C}") %>'></asp:Label></p>
                        <p>Total Price: <asp:Label ID="lblTotalPrice" runat="server" Text='<%# Eval("TotalPrice", "{0:C}") %>'></asp:Label></p>
                        
                    </div>
                    <div class="order-date">
                        <p>Date Ordered: <%# Eval("OrderDate", "{0:MMMM dd, yyyy}") %></p>
                        <p>Status: Order placed</p>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>

