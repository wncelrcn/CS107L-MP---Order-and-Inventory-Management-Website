<%@ Page Title="" Language="C#" MasterPageFile="~/NavBar.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="CS107L_MP.About" %>
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
            background-color: #fff;
            border-radius: 5px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
            text-align: center;
        }

        h1 {
            color: #333;
        }

        p {
            color: #333;
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
            color: #333;
        }

        
    </style>

    <div class="container">
        <h1>Franchise Distributor</h1>
        <asp:Image ID="YourDistributorImage" runat="server" ImageUrl="https://fsf-bigmindph.weebly.com/uploads/3/7/9/0/37908249/3730793_orig.png" />
        <p><span class="highlight">Your Distributor</span> is a key player in the realm of franchise distribution, offering a unique and comprehensive solution for aspiring entrepreneurs. Our commitment extends beyond mere business transactions; we are dedicated to empowering franchisees and fostering success in every venture.</p>

        <h2>Our Services</h2>
        <ul>
            <li><span class="highlight">Franchise Support</span>: We provide unparalleled support to our franchisees, guiding them through every step of the business journey. From initial setup to ongoing operations, we are invested in the success of each franchise.</li>
            <li><span class="highlight">Resource Procurement</span>: As a distributor, we specialize in sourcing and supplying high-quality raw materials, ingredients, and utilities essential for franchise operations. Our extensive network ensures a seamless and efficient supply chain.</li>
        </ul>

        <h2>Collaborative Network</h2>
        <p>At <span class="highlight">Your Distributor</span>, we believe in the strength of collaboration. Our network of franchisees forms a community that shares resources, experiences, and best practices. Together, we build a foundation for mutual growth and prosperity.</p>

        <h2>Technology Integration</h2>
        <p>We embrace cutting-edge technology to enhance the efficiency of our distribution system. Our state-of-the-art Sales and Inventory Management System ensures real-time tracking, order fulfillment, and inventory control, providing franchisees with the tools they need for success.</p>
    </div>


</asp:Content>
