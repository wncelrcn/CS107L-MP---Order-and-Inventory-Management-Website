<%@ Page Title="Register to _________ "Language="C#" MasterPageFile="~/LogInNav.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="CS107L_MP.WebForm3" %>



<asp:Content ID="RegisterContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
         .register-body {
           font-family: sans-serif;
           margin: 0;
           padding: 0;
           display: flex;
           justify-content: center;
           align-items: center;
           min-height: 100vh;
           background-color: #f5f5f5;
         }

         .register-container {
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

         .regBtn {
             background-color: green;
             color: white;
             padding: 15px 30px;  /* Increased horizontal padding for better look */
             margin: 10px 100px;  /* Center the button horizontally */
             border: none;
             border-radius: 3px;
             cursor: pointer;
         }

         .regBtn:hover {
             background-color: darkgreen;
         }

         .login-link {
           text-align: center;  /* Center the link text */
           margin-top: 10px;  /* Add some space above the link */
         }

         .login-link a {
           color: blue;  /* Set the link color */
           text-decoration: underline;/* Remove underline from default link style */
         }

         .login-link a:hover {
             color: lightblue;
         }
    </style>
    <div class ="register-body">
        <div class ="register-container">
            <h1>Register</h1>

            <form id="Form1" runat="server">
                <div class="form-group">
                    <asp:Label ID="usernameLbl" runat="server" Text="Enter a username:"></asp:Label><br />
                    <asp:TextBox ID="usernameTxtBox" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="usernameValid" runat="server" ErrorMessage="*" ControlToValidate="usernameTxtBox" ForeColor="#CC0000"></asp:RequiredFieldValidator><br />
                </div>

                <div class="form-group">
                    <asp:Label ID="passLbl" runat="server" Text="Enter a password:"></asp:Label><br />
                    <asp:TextBox ID="passTxtBox" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="passValid" runat="server" ErrorMessage="*" ControlToValidate="passTxtBox" ForeColor="#CC0000"></asp:RequiredFieldValidator><br />
                </div>

                <div class="form-group">
                    <asp:Label ID="passLbl2" runat="server" Text="Confirm your password:"></asp:Label><br />
                    <asp:TextBox ID="passTxtBox2" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="passValid2" runat="server" ErrorMessage="*" ControlToValidate="passTxtBox2" ForeColor="#CC0000"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="passValid3" runat="server" ErrorMessage="Passwords must match!" ControlToValidate="passTxtBox2" ControlToCompare="passTxtBox" ForeColor="#CC0000"></asp:CompareValidator><br />
                </div>

                <asp:Button ID="regBtn" class ="regBtn" runat="server" Text="Register" onclick="regBtn_Click"/><br />
                <div class="login-link"> Already have an account? <a href="Login.aspx">Login here!</a>

                </div>
            </form>
        </div>
    </div>
</asp:Content> 

