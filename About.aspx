<%@ Page Title="About Us" Language="C#" MasterPageFile="~/NavBar.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="CS107L_MP.About" %>
<asp:Content ID="AboutPage" ContentPlaceHolderID="MainContent" runat="server">

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

        h1 {
            color: #231a18;
        }

        p {
            color: #231a18;
        }

        .container img {
            max-width: 100%;
            height: auto;
            margin-top: 20px;
            border-radius: 30px;
            transition: transform 0.5s cubic-bezier(0.4, 0, 0.2, 1);
        }

        .container img:hover {
            transform: scale(0.9);
        }

        .highlight {
            font-weight: bold;
            color: #231a18;
        }

        
        footer {
            background-color: #009688;
            color: #f3f6f7;
            text-align: center;
            padding: 20px 0;
            margin-top: auto;
        }

        footer p {
            color: #f3f6f7; 
        }

        
    </style>

    <div class="container">
        <h1>JLR Food Products Trading</h1>
        <asp:Image ID="YourDistributorImage" runat="server"  ImageUrl="~/Pics/About-us.jpg" />
        <p>Welcome to <span class="highlight">JLR Food Products Trading</span>, a cornerstone in the realm of First Brew Coffee franchise distribution. We offer a specialized and comprehensive solution designed to empower aspiring entrepreneurs like you. Our commitment transcends mere transactions; we are dedicated to fostering success in every venture we support.</p>

        <h2>Our Services</h2>
        <ul>
            <li><span class="highlight">Franchisee Empowerment</span>: We provide unparalleled support to our First Brew Coffee franchisees, guiding them through every stage of their business journey. From initial setup to ongoing operations, we are deeply invested in the success of each franchisee we serve.</li>
            <li><span class="highlight">Quality Ingredient Sourcing</span>: As a provincial distributor, we specialize in sourcing and supplying high-quality raw materials, ingredients, and utilities essential for First Brew Coffee franchise operations. Our extensive network ensures a seamless and efficient supply chain, empowering franchisees to focus on delivering exceptional coffee experiences to their customers.</li>
        </ul>

        <h2>Collaborative Network</h2>
        <p>At <span class="highlight">JLR Food Products Trading</span>, we believe in the power of collaboration. Our network of First Brew Coffee franchisees forms a community built on sharing resources, experiences, and best practices. Together, we establish a solid foundation for mutual growth and prosperity within the provincial coffee industry.</p>

        <h2>Technology Integration</h2>
        <p>We leverage cutting-edge technology to streamline our distribution system. Our state-of-the-art Order, Sales, and Inventory Management System enables real-time tracking, efficient order fulfillment, and precise inventory control. This empowers First Brew Coffee franchisees with the tools they need to thrive in today's competitive market landscape.</p>
    </div>

    <footer>
    <p>&copy; 2024 JLR Food Products Trading. All rights reserved.</p>
    </footer>

</asp:Content>
