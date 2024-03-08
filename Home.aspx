<%@ Page Title="" Language="C#" MasterPageFile="~/NavBar.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="CS107L_MP.WebForm1" %>

<asp:Content ID="HomePage" ContentPlaceHolderID="MainContent" runat="server">

    <style>
    h1, h2, p, ul, li {
      margin: 0;
      padding: 0;
    }

    section {
      display: flex;
      flex-direction: column;
      max-height: 100vh; 
    }

    .hero {
      display: flex;
      justify-content: center; 
      align-items: center;
      background-image: url('coffee-background.jpg'); /* Replace 'coffee-background.jpg' with your image */
      background-size: cover;
      color: #808080ff;
      text-align: center;
      padding: 100px 0;
      min-height: 100vh; 
    }

    .hero-content {
      max-width: 600px;
    }

    .hero h2 {
      font-size: 36px;
      margin-bottom: 20px;
    }

    .hero p {
      font-size: 18px;
      margin-bottom: 30px;
    }

    .btn {
      display: inline-block;
      background-color: #333;
      color: #fff;
      padding: 10px 20px;
      text-decoration: none;
      border-radius: 5px;
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



    </style>

    <section class="hero">
        <div class="hero-content">
          <h2>Your First Brew Coffee Needs, now here.</h2>
          <p>Order online and blah blah blah blah blah.</p>
          <a href="Order.aspx" class="btn">Order Now</a>
        </div>

    </section>
    
   
   
    <footer>
        <p>&copy; 2024 _____________. All rights reserved.</p>

    </footer>
</asp:Content>
