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
    </style>

    <div class="container">
        <h2>Sales Summary</h2>
        <asp:Repeater ID="SalesRepeater" runat="server">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>Transaction ID</th>
                        <th>Total Revenue</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval("TransactionID") %></td>
                    <td><%# Eval("TotalRevenue", "{0:C}") %></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>