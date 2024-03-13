<%@ Page Title="" Language="C#" MasterPageFile="~/OrderNav.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="CS107L_MP.Cart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   
    <style>
        @import url('https://fonts.googleapis.com/css2?family=Poppins:wght@400;600&display=swap');

        /* Apply font-family */
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

        .cart-container {
            width: 80%;
            margin: 0 auto;
            padding: 20px;
            background-color: #f9f9f9;
            border-radius: 5px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
        }

        .cart-item {
            border-bottom: 1px solid #ccc;
            padding: 10px 0;
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

        .item-details {
            flex-grow: 1;
        }

        .item-details p {
            margin: 5px 0;
            font-size: 18px;
        }

        .quantityTxtBox {
            font-size: 18px;
            width: 60px;
        }

        .quantity-controls button {
            padding: 5px 10px;
            background-color: #009688;
            color: #fff;
            border: none;
            border-radius: 3px;
            cursor: pointer;
            margin: 0 5px;
        }

        .remove-button {
            margin-left: 10px;
            padding: 5px 10px;
            color: #fff;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            background-color: #ff5722;
            font-size: 18px;
        }

        .plus-button,
        .minus-button {
            width: 30px;
            height: 30px;
            font-size: 18px;
            color: #fff;
            border-radius: 50%;
        }

        .plus-button {
            background-color: #4caf50;
        }

        .minus-button {
            background-color: #f44336;
        }

        .total-cart-container {
            margin-top: 20px;
            padding: 10px;
            background-color: #f9f9f9;
            border-radius: 5px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

        .total-cart-container .checkout-button {
            padding: 10px 20px;
            background-color: #009688;
            color: #fff;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 16px;
            transition: background-color 0.3s ease;
        }

        .total-cart-container .checkout-button:hover {
            background-color: #00796b;
        }

        .total-cart-container label {
            font-size: 18px;
        }
    </style>

    <form id="Form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <h1><%:Session["Username"]%>'s Cart</h1>
        <div class="cart-container">
            <asp:Repeater ID="ItemRepeater" runat="server">
                <ItemTemplate>
                    <div class="cart-item">
                        <div class="item-details">
                            <p><strong><%# Eval("ProductName") %></strong></p>
                            <p>Quantity: <asp:TextBox ID="txtQuantity" CssClass="quantityTxtBox" runat="server" Text='<%# Eval("Quantity") %>' Enabled="False"></asp:TextBox></p>
                            <p class ="product-price">Price per item: <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("Price", "{0:C}") %>'></asp:Label> </p>
                            <p class ="product-total-price">Total Price: <asp:Label ID="lblTotalPrice" runat="server" Text='<%# Eval("TotalPrice", "{0:C}") %>'></asp:Label> </p>
                        </div>
                        <div class="quantity-controls">
                            <asp:Button ID="minusButton" CssClass="minus-button" runat="server" Text="-" OnClick="minusButton_Click" CommandArgument='<%# Eval("ProductID") + ";" + Eval("Quantity") + ";" + Eval("TotalPrice") %>' />
                            <asp:Button ID="plusButton" CssClass="plus-button" runat="server" Text="+" OnClick ="plusButton_Click"  CommandArgument='<%# Eval("ProductID") + ";" + Eval("Quantity") + ";" + Eval("TotalPrice") + ";" + Eval("Stock") %>' />
                            <asp:Button ID="btnRemove" runat="server" Text="Remove" CssClass="remove-button" CommandArgument='<%# Eval("ProductId") %>' OnClick="RemoveFromCart_Click" />
                        </div>
                    </div>
                </ItemTemplate>
             </asp:Repeater>
            <div class="total-cart-container">
                <asp:Label ID="lblTotalCartPrice" runat="server" Text=""></asp:Label>
                <asp:Button ID="btnCheckout" runat="server" Text="Checkout" CssClass="checkout-button" OnClick="btnCheckout_Click" />
            </div>
        </div>
    </form>
</asp:Content>