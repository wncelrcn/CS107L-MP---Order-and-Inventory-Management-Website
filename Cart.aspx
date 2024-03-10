<%@ Page Title="" Language="C#" MasterPageFile="~/OrderNav.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="CS107L_MP.Cart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>My Cart</h2>

    <form id="form1" runat="server">
    <asp:GridView ID="CartGridView" runat="server" AutoGenerateColumns="False" EmptyDataText="Your cart is empty">
    <Columns>
        <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
        <asp:BoundField DataField="TotalPrice" HeaderText="Total Price" DataFormatString="{0:C}" />
        <asp:TemplateField HeaderText="Actions">
            <ItemTemplate>
                <asp:Button ID="btnReduceQuantity" runat="server" Text="Reduce Quantity" CommandName="ReduceQuantity" CommandArgument='<%# Eval("ProductID") + ";" + Eval("Quantity") + ";" + Eval("TotalPrice") %>' OnCommand="CartGridView_Command" />
                <asp:Button ID="btnAddQuantity" runat="server" Text="Add Quantity" CommandName="AddQuantity" CommandArgument='<%# Eval("ProductID") + ";" + Eval("Quantity") + ";" + Eval("TotalPrice") + ";" + Eval("Stock") %>' OnCommand="CartGridView_Command" />
                <asp:Button ID="btnRemoveItem" runat="server" Text="Remove Item" CommandName="RemoveItem" CommandArgument='<%# Eval("ProductID") %>' OnCommand="CartGridView_Command" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

    </form>
</asp:Content>
