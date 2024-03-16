<%@ Page Title="" Language="C#" MasterPageFile="~/AdminNavBar.Master" AutoEventWireup="true" CodeBehind="ViewSales.aspx.cs" Inherits="CS107L_MP.ViewSales" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   <style>
        @import url('https://fonts.googleapis.com/css2?family=Poppins:wght@400;600&display=swap');

        body {
            font-family: 'Poppins', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
        }

        .container {
            max-width: 800px;
            margin: 0 auto;
            padding: 20px;
        }

        h2 {
            font-size: 24px;
            font-weight: 600;
            color: #333;
            margin-bottom: 20px;
        }

        table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }

        th, td {
            padding: 10px;
            border: 1px solid #ddd;
            text-align: left;
        }

        th {
            background-color: #f2f2f2;
            font-weight: 600;
        }

        td {
            background-color: #fff;
        }

        tr:nth-child(even) td {
            background-color: #f9f9f9;
        }

        tr:hover td {
            background-color: #e9e9e9;
        }

        .summary {
            margin-top: 20px;
            font-weight: bold;
        }


        .custom-dropdown {
            width: 200px; /* Adjust width as needed */
            padding: 5px;
            border: 1px solid #ccc;
            border-radius: 5px;
            background-color: #fff;
            color: #333;
            font-size: 14px;
            /* Add more styles as needed */
        }

    </style>

    <div class="container">
        <h2>Sales Summary</h2>
        <table>
            <tr>
                <th>Transaction ID</th>
                <th>Sales</th>
                <th>Profit</th>
            </tr>
            <asp:Repeater ID="SalesRepeater" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("TransactionID") %></td>
                        <td><%# Eval("TotalRevenue", "{0:C}") %></td>
                        <td><%# (Convert.ToDouble(Eval("TotalRevenue")) * 0.2).ToString("C") %></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>   
    <div class="container">
        <h2>Total Summary</h2>
        <form runat="server">
            <asp:DropDownList ID="DateRangeDropDown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DateRangeDropDown_SelectedIndexChanged" CssClass="custom-dropdown">
                 <asp:ListItem Text="Daily" Value="Daily"></asp:ListItem>
                 <asp:ListItem Text="Weekly" Value="Weekly"></asp:ListItem>
                 <asp:ListItem Text="Monthly" Value="Monthly"></asp:ListItem>
                 <asp:ListItem Text="Yearly" Value="Yearly"></asp:ListItem>
            </asp:DropDownList>
        </form>
        <table>
            <tr class="summary">
                <td>Total Accumulated Sales</td>
                <td><asp:Literal runat="server" ID="TotalAccumulatedSalesLiteral"></asp:Literal></td>
                <td>Total Accumulated Profit</td>
                <td><asp:Literal runat="server" ID="TotalAccumulatedProfitLiteral"></asp:Literal></td>
            </tr>
        </table>
    </div>
</asp:Content>