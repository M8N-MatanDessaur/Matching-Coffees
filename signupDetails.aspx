<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="signupDetails.aspx.cs" Inherits="MatchingCoffees.signupDetails" %>

<!DOCTYPE html>
<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="./assets/styles/fonts.css">
    <link rel="stylesheet" href="./assets/styles/base.css">
    <title>Matchingcoffees - Specifications</title>
    <style>span{color: red;}</style>
</head>
<body class="signup">
    <section class="signup">
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
            <label for="DRP_Pref_Gender">Who are you looking for?
                <asp:RequiredFieldValidator ID="DRP_PrefGender_Validator" runat="server" ErrorMessage="*" ControlToValidate="DRP_PrefGender"></asp:RequiredFieldValidator>
            </label>
                <asp:DropDownList ID="DRP_PrefGender" runat="server">
                    <asp:ListItem Value="Male">A male</asp:ListItem>
                    <asp:ListItem Value="Female">A female</asp:ListItem>
                    <asp:ListItem>Other</asp:ListItem>
                </asp:DropDownList>
            <label for="TXT_CoffeeTime">When do you usualy drink your coffee
                <asp:RequiredFieldValidator ID="TXT_CoffeeTime_Validator" runat="server" ErrorMessage="*" ControlToValidate="TXT_CoffeeTime"></asp:RequiredFieldValidator>
            </label>
                    <asp:TextBox ID="TXT_CoffeeTime" runat="server" TextMode="Time"></asp:TextBox>
            <label for="coffee-types">Coffee types
                <asp:RequiredFieldValidator ID="DRP_CoffeeTypes_Validator" runat="server" ErrorMessage="*" ControlToValidate="DRP_CoffeeTypes"></asp:RequiredFieldValidator>
            </label>
                <asp:DropDownList ID="DRP_CoffeeTypes" runat="server">
                    <asp:ListItem>Espresso</asp:ListItem>
                    <asp:ListItem>Latte</asp:ListItem>
                    <asp:ListItem>Mocha</asp:ListItem>
                    <asp:ListItem>Machiato</asp:ListItem>
                    <asp:ListItem>Americano</asp:ListItem>
                    <asp:ListItem>Cortado</asp:ListItem>
                    <asp:ListItem>Dark</asp:ListItem>
                </asp:DropDownList>
            <label for="FILE_ProfilePicture">Profile Picture
                <asp:RequiredFieldValidator ID="FILE_ProfilePicture_Validator" runat="server" ErrorMessage="*" ControlToValidate="FILE_ProfilePicture"></asp:RequiredFieldValidator>
            </label>
                <asp:FileUpload ID="FILE_ProfilePicture" runat="server" />
            <label for="bio">Bio</label>
                <asp:TextBox ID="TXT_Bio" runat="server" TextMode="MultiLine"></asp:TextBox>
            <asp:Button ID="BTN_Register" runat="server" Text="Register" OnClick="BTN_Register_Click" />
        </form>
        <div class="signup-image">
        </div>
    </section>
</body>
</html>
