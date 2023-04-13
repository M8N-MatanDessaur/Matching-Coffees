<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="MatchingCoffees.login" %>

<!DOCTYPE html>
<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" type="text/css" href="https://cdn.jsdelivr.net/npm/toastify-js/src/toastify.min.css">
    <link rel="stylesheet" href="./assets/styles/fonts.css">
    <link rel="stylesheet" href="./assets/styles/base.css">
    <title>Matchingcoffees - Login</title>
    <style>span{color: red;}</style>
</head>
<body class="signin">
    <section class="signin">
        <div class="signin-image">
        </div>
        <form method="post" runat="server">
            <h1 style="margin-bottom: 10px;">Matchingcoffees
                <span style="vertical-align: middle;">
                    <svg width="35" height="35" fill="none" stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                        <path d="M3 14c.83.643 2.077 1.018 3.5 1 1.423.018 2.67-.357 3.5-1 .83-.641 2.077-1.016 3.5-1 1.423-.016 2.67.359 3.5 1"></path>
                        <path d="M8 3a2.4 2.4 0 0 0-1 2 2.4 2.4 0 0 0 1 2"></path>
                        <path d="M12 3a2.4 2.4 0 0 0-1 2 2.401 2.401 0 0 0 1 2"></path>
                        <path d="M3 10h14v5a6 6 0 0 1-6 6H9a6 6 0 0 1-6-6v-5Z"></path>
                        <path d="M16.746 16.726a3 3 0 1 0 .252-5.555"></path>
                      </svg>
                </span>
            </h1>
            <h2 style="margin-bottom:15px;font-size:18px;">Login</h2>
            <label for="Email">Email Address</label>
                <asp:TextBox ID="TXT_Username" runat="server" TextMode="Email"></asp:TextBox>
            <div class="password flx-col">
                <label for="Pass">Password</label>
                    <asp:TextBox ID="TXT_Pass" runat="server" TextMode="Password"></asp:TextBox>
            </div>
            <asp:Button ID="BTN_Login" runat="server" Text="Login" OnClick="BTN_Login_Click" />
            <a href="./signup.aspx">Create an account</a>
        </form>
      
    </section>
    <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/toastify-js"></script>
    <div id="error_msg" runat="server"></div>
</body>
</html>