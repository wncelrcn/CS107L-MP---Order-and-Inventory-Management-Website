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
            background-color: #fff;
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
            color: #333;
        }

        .hero p {
            font-size: 18px;
            margin-bottom: 30px;
            color: #333;
        }

        .btn {
            display: inline-block;
            background-color: #333;
            color: #fff;
            padding: 15px 30px;
            text-decoration: none;
            border-radius: 5px;
            font-size: 18px;
            transition: background-color 0.3s ease;
        }

        .btn:hover {
            background-color: #555;
        }

        footer {
            background-color: #333;
            color: #fff;
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
            <h1>Franchise Distributor</h1>
             <img src="https://www.epgdlaw.com/wp-content/uploads/2021/06/60094008_s.jpg" alt="Your Image Alt Text">
            <h2>Empowering Entrepreneurs, Enriching Communities.</h2>
            <p>Order online and blah blah blah blah blah.</p>
            <a href="Login.aspx" class="btn">Order Now</a>
        </div>
    </div>

    <footer>
        <p>&copy; 2024 Your Company Name. All rights reserved.</p>
    </footer>
</asp:Content>