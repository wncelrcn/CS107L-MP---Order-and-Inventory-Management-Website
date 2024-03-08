<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="CS107L_MP.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <div class ="register-container">
    <h1>Register</h1>

    <form id="Form1" runat="server">
        <div>
            <asp:Label ID="usernameLbl" runat="server" Text="Enter a username:"></asp:Label><br />
            <asp:TextBox ID="usernameTxtBox" runat="server"></asp:TextBox><br />
            <asp:Label ID="passLbl" runat="server" Text="Enter a password:"></asp:Label><br />
            <asp:TextBox ID="passTxtBox" runat="server"></asp:TextBox><br />
            <asp:Label ID="passLbl2" runat="server" Text="Confirm your password:"></asp:Label><br />
            <asp:TextBox ID="passTxtBox2" runat="server"></asp:TextBox><br />
            <asp:Button ID="regBtn" runat="server" Text="Register" /><br />

        </div>
    </form>
</div>
</body>
</html>
