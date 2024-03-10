<%@ Page Title="Log in to ________ "Language="C#" MasterPageFile="~/LogInNav.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CS107L_MP.WebForm2" %>

<asp:Content ID="LogInContent" ContentPlaceHolderID="MainContent" runat="server">

    <style>

        @import url('https://fonts.googleapis.com/css2?family=Poppins:wght@400;600&display=swap');

        .login-body {
          font-family: 'Poppins', Tahoma, Geneva, Verdana, sans-serif;
          margin: 0;
          padding: 0;
          display: flex;
          justify-content: center;
          align-items: center;
          min-height: 100vh;
          background-color: #f5f5f5;
        }

        .login-container {
          background-color: #fff;
          padding: 30px;
          border-radius: 5px;
          box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
          width: 300px;
        }

        h1 {
          text-align: center;
          margin-bottom: 20px;
        }

        label {
          display: block;
          margin-bottom: 5px;
        }

        .form-group { /* Group labels and inputs for better styling */
          margin-bottom: 15px;
          margin-right: 15px;
        }

        input[type="text"],
        input[type="password"] {
          width: 100%;
          padding: 10px;
          border: 1px solid #ccc;
          border-radius: 3px;
        }

        .loginBtn {
            background-color: green;
            color: white;
            padding: 15px 30px;  /* Increased horizontal padding for better look */
            margin: 10px 100px;  /* Center the button horizontally */
            border: none;
            border-radius: 3px;
            cursor: pointer;
        }

        .loginBtn:hover {
            background-color: darkgreen;
        }

        .register-link {
          text-align: center;  /* Center the link text */
          margin-top: 10px;  /* Add some space above the link */
        }

        .register-link a {
          color: blue;  /* Set the link color */
          text-decoration: underline;/* Remove underline from default link style */
        }

        .register-link a:hover {
            color: lightblue;
        }

    </style>

    <div class ="login-body">
        <div class ="login-container">
            <h1>Login to ______</h1>

            <form id="Form1" runat="server">
                <div class="form-group">
                    <asp:Label ID="usernameLbl" runat="server" Text="Username:"></asp:Label><br />
                    <asp:TextBox ID="usernameTxtBox" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="userLoginValid" runat="server" ErrorMessage="*" ControlToValidate="usernameTxtBox" ForeColor="#CC0000"></asp:RequiredFieldValidator>

                </div>
                <div class="form-group">
                    <asp:Label ID="passLbl" runat="server" Text="Password:"></asp:Label><br />
                    <asp:TextBox ID="passTxtBox" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="passLoginValid" runat="server" ErrorMessage="*" ControlToValidate="passTxtBox" ForeColor="#CC0000"></asp:RequiredFieldValidator>
                        
                </div>
                <asp:Button ID="loginBtn" class="loginBtn" runat="server" Text="Log in" onclick="loginBtn_Click"/><br />
                 <div class="register-link">  Don't have an account? <a href="Register.aspx">Register here!</a>
            </div>
            </form>

        </div>
    </div>

    
    
</asp:Content>
