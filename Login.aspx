<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CS107L_MP.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <div class ="login-container">
        <h1>Login</h1>

        <form id="Form1" runat="server">
            <div>
                <asp:Label ID="usernameLbl" runat="server" Text="Username:"></asp:Label><br />
                <asp:TextBox ID="usernameTxtBox" runat="server"></asp:TextBox><br />
                <asp:Label ID="passLbl" runat="server" Text="Password:"></asp:Label><br />
                <asp:TextBox ID="passTxtBox" runat="server"></asp:TextBox><br />
                <asp:Button ID="loginBtn" runat="server" Text="Log in" /><br />

            </div>
        </form>
    </div>
    
</body>
</html>
