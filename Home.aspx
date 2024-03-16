<%@ Page Title="" Language="C#" MasterPageFile="~/NavBar.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="CS107L_MP.WebForm1" %>

<asp:Content ID="HomePage" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        @import url('https://fonts.googleapis.com/css2?family=Poppins:wght@400;600&display=swap');

        body {
            font-family: 'Poppins', Tahoma, Geneva, Verdana, sans-serif;
        }
        
        .container {
            max-width: 800px;
            margin: 0 auto;
            padding: 20px;
            background-color: #f3f6f7;
            border-radius: 5px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
            text-align: center;
        }

        h2 {
            font-size: 36px;
            margin-bottom: 20px;
        }

        h1 {
            font-size: 48px;
            margin-bottom: 20px;
            font-weight: bold;
            color: #231a18;
        }

        .hero p {
            font-size: 18px;
            margin-bottom: 30px;
            color: #231a18;
        }

        .btn {
            display: inline-block;
            background-color: #009688;
            color: #f3f6f7;
            padding: 15px 30px;
            text-decoration: none;
            border-radius: 5px;
            font-size: 18px;
            transition: background-color 0.3s ease;
        }

        .btn:hover {
            background-color: #00796b;
        }

        footer {
            background-color: #009688;
            color: #f3f6f7;
            text-align: center;
            padding: 20px 0;
            margin-top: auto;
        }

        .container img {
            width: 50%;
            margin-left: 20px;
            max-width: 100%;
            height: auto;
            margin-top: 20px;
            border-radius: 30px;
            transition: transform 0.5s cubic-bezier(0.4, 0, 0.2, 1);
        }

        .container img:hover {
            transform: scale(0.9);
        }
    </style>

    <div class="container">
        <div class="hero">
            <h1>JLR Food Products Trading</h1>
            <img src="/Pics/JLR%20Food%20Trading.png" alt="JLR Food Trading">
            <h2>Empower Your First Brew Coffee Business</h2>
            <p>Supplying Quality Ingredients and Materials to Fuel Your Success as a First Brew Coffee Franchisee. Elevate your Coffee Offering Today!</p>
            <a href="Login.aspx" class="btn">Order Now</a>
        </div>
    </div>

    <footer>
        <p>&copy; 2024 JLR Food Products Trading. All rights reserved.</p>
    </footer>
</asp:Content>