<%@ Page Title="" Language="C#" MasterPageFile="~/AdminNavBar.Master" AutoEventWireup="true" CodeBehind="ManageInventory.aspx.cs" Inherits="CS107L_MP.ManageInventory" %>
<asp:Content ID="ManageInventoryPage" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        /* CSS styles here */
        @import url('https://fonts.googleapis.com/css2?family=Poppins:wght@400;600&display=swap');

        body {
            font-family: 'Poppins', Tahoma, Geneva, Verdana, sans-serif;
        }

        .products-container {
            text-align: center;
        }

        .product-container {
            border: 1px solid #ccc;
            padding: 10px;
            margin-bottom: 20px;
            width: calc(20% - 20px);
            border-radius: 5px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
            display: inline-block;
            margin-right: 20px;
            text-align: left;
        }

        .product-name {
            font-weight: bold;
            font-size: 16px;
            color: #333;
            margin-bottom: 5px;
        }

        .product-price {
            color: #009688;
            font-size: 14px;
            margin-bottom: 5px;
        }

        .product-stock {
            color: #ff5722;
            font-size: 14px;
        }

        .clearfix::after {
            content: "";
            display: table;
            clear: both;
        }

        h1 {
            font-size: 36px;
            color: #333;
            margin-bottom: 30px;
            text-align: left;
            margin-left: 5.5rem;
        }

        .dropdown-container {
            margin-bottom: 20px;
            text-align: center;
        }

        .dropDown {
            font-size: 16px;
            padding: 10px;
            border: 2px solid #009688;
            border-radius: 5px;
            background-color: #fff;
            color: #333;
            width: 200px;
            text-align: center;
            cursor: pointer;
            transition: all 0.3s ease;
            display: inline-block;
            vertical-align: top;
            margin-left: 20px;
        }

        .dropDown:hover {
            background-color: #009688;
            color: #fff;
        }

        h2 {
            font-size: 24px;
            color: #009688;
            margin-bottom: 20px;
            text-align: center;
            text-transform: uppercase;
        }

        .product-actions {
            margin-top: 10px;
            text-align: center;
        }

        .quantity-input {
            width: 60px;
            padding: 5px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }

        .update-stock-button {
            margin-left: 10px;
            padding: 5px 10px;
            background-color: #009688;
            color: #fff;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }

        .update-stock-button:hover {
            background-color: #00796b;
        }
    </style>
    <h1>Manage Inventory</h1>
    <form id="form1" runat="server">
        <div class="dropdown-container">
            <asp:DropDownList ID="CategoryDropDown" runat="server" CssClass="dropDown" AutoPostBack="True" OnSelectedIndexChanged="CategoryDropDown_SelectedIndexChanged">
                <asp:ListItem Text="All Categories" Value=""></asp:ListItem>
                <asp:ListItem Text="Coffee Essentials" Value="Coffee"></asp:ListItem>
                <asp:ListItem Text="Fruit Tea Essentials" Value="FruitTea"></asp:ListItem>
                <asp:ListItem Text="Milktea Essentials" Value="Milktea"></asp:ListItem>
                <asp:ListItem Text="Other Essentials" Value="Misc"></asp:ListItem>
            </asp:DropDownList>
        </div>

        <div class="products-container clearfix">
            <asp:Repeater ID="ProductRepeater" runat="server">
                <ItemTemplate>
                    <div class="product-container">
                        <p class="product-name"><%# Eval("Name") %></p>
                        <p class="product-price">Price: <%# Eval("Price", "{0:C}") %></p>
                        <p class="product-stock">Stock: <asp:Label ID="stockLabel" runat="server" Text='<%# Eval("Stock") %>'></asp:Label></p>
                        <div class="product-actions">
                            <label for="quantity_lbl">Quantity:</label>
                            <asp:TextBox ID="quantityNo" CssClass="quantity-input" runat="server" Text='<%# Eval("Stock") %>'></asp:TextBox>
                            <asp:Button runat="server" Text="Update Stock" CssClass="update-stock-button" CommandName="UpdateStock" CommandArgument='<%# Eval("ProductId") + ";" + Eval("Name") %>' onclick="UpdateStock_Click"/>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</asp:Content>
