<%@ Page Title="" Language="C#" MasterPageFile="~/OrderNav.Master" AutoEventWireup="true" CodeBehind="Order.aspx.cs" Inherits="CS107L_MP.Order" %>
<asp:Content ID="OrderPage" ContentPlaceHolderID="MainContent" runat="server">
    <style>

        /* Adjustments to make the product containers centered */
        .products-container {
            text-align: center;
        }

        .product-container {
            border: 1px solid #ccc;
            padding: 10px;
            margin-bottom: 20px;
            width: calc(20% - 20px); /* Adjust based on the number of products per row */
            border-radius: 5px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
            display: inline-block; /* Changed from float: left */
            margin-right: 20px; /* Adjust spacing between products */
            text-align: left; /* Reset text alignment */
        }

        .product-name {
            font-weight: bold;
            font-size: 14px;
            color: #333;
            margin-bottom: 5px;
        }

        .product-price {
            color: #009688;
            font-size: 12px;
            margin-bottom: 5px;
        }

        .product-stock {
            color: #ff5722;
            font-size: 12px;
        }

        /* Clearfix to handle inline-block elements */
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

        /* Styles for the DropDownList container */
        .dropdown-container {
            margin-bottom: 20px; /* Adjust margin bottom as needed */
            text-align: center; /* Align contents center */
        }

        /* Styles for the DropDownList */
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
            display: inline-block; /* Change display to inline-block */
            vertical-align: top; /* Align top */
            margin-left: 20px; /* Adjust margin left to match products */
        }

        .dropDown:hover {
            background-color: #009688;
            color: #fff;
        }

        /* Styles for the h2 element */
        h2 {
            font-size: 24px;
            color: #009688;
            margin-bottom: 20px;
            text-align: center;
            text-transform: uppercase;
        }

        /* Styles for the product actions container */
        .product-actions {
            margin-top: 10px;
            text-align: center;
        }

        /* Styles for the quantity input */
        .quantity-input {
            width: 60px;
            padding: 5px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }

        /* Styles for the "Add to Cart" button */
        .add-to-cart-button {
            margin-left: 10px;
            padding: 5px 10px;
            background-color: #009688;
            color: #fff;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }

        .add-to-cart-button:hover {
            background-color: #00796b;  
        }



    </style>
    <h1>Hi, <%:Session["Username"]%>!  </h1>
    <h2>Product List</h2>
    <form id="Form1" runat="server">
        <div class ="dropdown-container">
            <asp:DropDownList ID="CategoryDropDown" class="dropDown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CategoryDropDown_SelectedIndexChanged">
                <asp:ListItem Text="All Categories" Value=""></asp:ListItem>
                <asp:ListItem Text="Coffee Essentials" Value="Coffee"></asp:ListItem>
                <asp:ListItem Text="Fruit Tea Essentials" Value="FruitTea"></asp:ListItem>
                <asp:ListItem Text="Milktea Essentials" Value="Milktea"></asp:ListItem>
                <asp:ListItem Text="Other Essentials" Value="Misc"></asp:ListItem>
            </asp:DropDownList>
        </div>

        <!-- Centered container for product repeater items -->
        <div class="products-container clearfix">
            <asp:Repeater ID="ProductRepeater" runat="server">
                <ItemTemplate>
                    <div class="product-container">
                        <p class="product-name"><%# Eval("Name") %></p>
                        <p class="product-price">Price: <%# Eval("Price", "{0:C}") %></p>
                        <p class="product-stock">Stock: <asp:Label ID="stockLabel" runat="server" Text='<%# Eval("Stock") %>'></asp:Label></p>
                        
                        <div class="product-actions">
                            <label for="quantity_lbl">Quantity:</label>
                            <asp:TextBox ID="quantityNo" CssClass="quantity-input" runat="server" TextMode="Number" value="1" min="1"></asp:TextBox>
                            <asp:Button runat="server" Text="Add to Cart" CssClass="add-to-cart-button" CommandName="AddToCart" CommandArgument='<%# Eval("ProductId") + ";" + Eval("Name") + ";" + Eval("Price") %>' onclick="AddToCart_Click"/>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

    </form>
</asp:Content>
