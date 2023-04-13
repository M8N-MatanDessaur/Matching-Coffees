<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="messenger.aspx.cs" Inherits="MatchingCoffees.messenger" %>

<!DOCTYPE html>
<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta charset="UTF-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <link rel="stylesheet" href="./assets/styles/fonts.css">
        <link rel="stylesheet" href="./assets/styles/base.css">
         <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.1/jquery.min.js"></script>
        <title>Matchingcoffees - Messenger</title>
    </head>
<body>
    <section class="discover">
        <nav class="left-side-navigation not-on-mobile">
            <h1 style="margin-bottom: 10px;">Matchingcoffees
                <span style="vertical-align: middle;">
                    <svg width="25" height="25" fill="none" stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                        <path d="M3 14c.83.643 2.077 1.018 3.5 1 1.423.018 2.67-.357 3.5-1 .83-.641 2.077-1.016 3.5-1 1.423-.016 2.67.359 3.5 1"></path>
                        <path d="M8 3a2.4 2.4 0 0 0-1 2 2.4 2.4 0 0 0 1 2"></path>
                        <path d="M12 3a2.4 2.4 0 0 0-1 2 2.401 2.401 0 0 0 1 2"></path>
                        <path d="M3 10h14v5a6 6 0 0 1-6 6H9a6 6 0 0 1-6-6v-5Z"></path>
                        <path d="M16.746 16.726a3 3 0 1 0 .252-5.555"></path>
                      </svg>
                </span>
            </h1>

            <ul>
                <li><a href="./discovery.aspx">Discover</a></li>
                <li><a href="./messenger.aspx" class="selected">Messages</a></li>
                <li><a href="#">Settings</a></li>
            </ul>
             <div id="liQuote">
                    <q id="loveQuote"></q>
            </div>
        </nav>
        <nav class="bottom-navigation">
            <ul>
                <li><a href="./discovery.aspx">Discover</a></li>
                <li><a href="./messenger.aspx" class="selected">Messages</a></li>
                <li><a href="#">Settings</a></li>
                <li><a href="./login.aspx?action=disconnect">Disconnect</a></li>
            </ul>
        </nav>
        <main class="main-area">
            <header class="ribbon">
                <h1 style="margin-bottom: 10px;" class="mobile-only">
                    <span style="vertical-align: middle;">
                        <svg width="25" height="25" fill="none" stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                            <path d="M3 14c.83.643 2.077 1.018 3.5 1 1.423.018 2.67-.357 3.5-1 .83-.641 2.077-1.016 3.5-1 1.423-.016 2.67.359 3.5 1"></path>
                            <path d="M8 3a2.4 2.4 0 0 0-1 2 2.4 2.4 0 0 0 1 2"></path>
                            <path d="M12 3a2.4 2.4 0 0 0-1 2 2.401 2.401 0 0 0 1 2"></path>
                            <path d="M3 10h14v5a6 6 0 0 1-6 6H9a6 6 0 0 1-6-6v-5Z"></path>
                            <path d="M16.746 16.726a3 3 0 1 0 .252-5.555"></path>
                          </svg>
                    </span>
                </h1>
               <p>Hi, <%=Session["First_Name"]%></p> 
               <img src="./assets/images/profiles/<%=Session["ProfilePicture"] %>" height="30px">
            </header>
            <div id="main_content" class="main-content" runat="server">
  
            </div>
        </main>
    </section>
     <a href="./login.aspx?action=disconnect" class="not-on-mobile" style="position:absolute;bottom:15px;right:15px;font-size:12px;">Disconnect</a>
       <script>
        $.ajax({
            method: 'GET',
            url: 'https://api.api-ninjas.com/v1/quotes?category=love',
            headers: { 'X-Api-Key': 'P5pvNEICNOmeimEjt6YtcA==QLM6nLA32LcVVkNJ' },
            contentType: 'application/json',
            success: function (result) {
                document.getElementById("loveQuote").innerHTML = JSON.stringify(result[0]['quote']);
            },
            error: function ajaxError(jqXHR) {
                document.body.innerHTML = ('Error: ', jqXHR.responseText);
            }
        });
       </script>
</body>
</html>