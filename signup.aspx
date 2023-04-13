<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="signup.aspx.cs" Inherits="MatchingCoffees.signup" %>

<!DOCTYPE html>
<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="./assets/styles/fonts.css">
    <link rel="stylesheet" href="./assets/styles/base.css">
    <title>Matchingcoffees - Sign Up</title>
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
            <h2 style="margin-bottom:15px;font-size:18px;">Sign Up</h2>
            <label for="First_Name">First Name 
                <asp:RequiredFieldValidator ID="First_Name_Validator" runat="server" ErrorMessage="*" ControlToValidate="TXT_First_Name"></asp:RequiredFieldValidator>
            </label>
                <asp:TextBox ID="TXT_First_Name" runat="server"></asp:TextBox>
            <label for="Last_Name">Last Name
                <asp:RequiredFieldValidator ID="Last_Name_Validator" runat="server" ErrorMessage="*" ControlToValidate="TXT_Last_Name"></asp:RequiredFieldValidator>
            </label>
                <asp:TextBox ID="TXT_Last_Name" runat="server"></asp:TextBox>
            <label for="Email">Email Address
                <asp:RequiredFieldValidator ID="TXT_Email_Validator" runat="server" ErrorMessage="*" ControlToValidate="TXT_Email"></asp:RequiredFieldValidator>
            </label>
                 <asp:TextBox ID="TXT_Email" runat="server" TextMode="Email"></asp:TextBox>
            <label>Date of birth
                <asp:RequiredFieldValidator ID="TXT_DOB_Validator" runat="server" ErrorMessage="*" ControlToValidate="TXT_DOB"></asp:RequiredFieldValidator>
            </label>
                 <asp:TextBox ID="TXT_DOB" runat="server" TextMode="Date"></asp:TextBox>
            <label for="Gender">Gender</label>
                <asp:DropDownList ID="DRP_Gender" runat="server">
                    <asp:ListItem>Male</asp:ListItem>
                    <asp:ListItem>Female</asp:ListItem>
                    <asp:ListItem>Other</asp:ListItem>
                </asp:DropDownList>
            <div class="password flx-col">
                <label for="Pass">Password
                    <asp:RequiredFieldValidator ID="TXT_Pass_Validator" runat="server" ErrorMessage="*" ControlToValidate="TXT_Pass"></asp:RequiredFieldValidator>
                </label>
                    <asp:TextBox ID="TXT_Pass" runat="server" TextMode="Password"></asp:TextBox>
            </div>
            
            <div class="terms-n-services">
                <asp:CheckBox ID="CHK_Terms" runat="server" OnCheckedChanged="Terms_CheckedChanged" AutoPostBack="true" />
                <p>Agree to terms & services</p>
            </div>
            
            <asp:Button ID="BTN_NextSignupPage" runat="server" Text="Next" OnClick="BTN_NextSignupPage_Click" Visible="False" />

            <a href="./login.aspx">Already have an account? Login</a>
        </form>
        <div class="signup-image">
        </div>
    </section>
</body>
</html>
