using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace MatchingCoffees
{
    public partial class discovery : System.Web.UI.Page
    {
        // PAGE_GLOBAL VARIABLES //
        static OleDbConnection connection;
        static string filterWakeTime = string.Empty;
        static string filterCoffee = string.Empty;
        ///////////////////////////
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    // Create connection when first-time loading
                    connection = new OleDbConnection();
                    connection.ConnectionString = @" Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + Server.MapPath(@"App_Data\MatchingCoffeesDB.accdb; Persist Security Info = True");

                    // <Actions> //
                    // LIKE / MATCH
                    if (Request.QueryString["Action"] is "Like")
                    {
                        connection.Open();
                        // Selects all from Friendship table
                        OleDbCommand selectAllRelationships = new OleDbCommand($"SELECT * FROM Friendship WHERE RecieverUID = {Session["UID"]}", connection);
                        OleDbDataReader verifRelations = selectAllRelationships.ExecuteReader();
                        while (verifRelations.Read())
                        {
                            // if there theres a request  MATCH!!! ❤️
                            if (Convert.ToDecimal(verifRelations["SenderUID"]) == Convert.ToDecimal(Request.QueryString["toUser"]))
                            {
                                OleDbCommand likeUser = new OleDbCommand($"UPDATE Friendship SET AreFriends = True WHERE SenderUID = {Request.QueryString["toUser"]}", connection);
                                likeUser.ExecuteNonQuery();
                                verifRelations.Close();
                                goto Exists; // Skips request creation 
                            }
                        }
                        // if no requests sent, send one
                        OleDbCommand AddLikeUser = new OleDbCommand($"INSERT INTO Friendship(SenderUID, RecieverUID) VALUES('{Session["UID"]}','{Request.QueryString["toUser"]}')", connection);
                        AddLikeUser.ExecuteNonQuery();

                    Exists:
                        verifRelations.Close();

                        connection.Close();
                        Response.Redirect("./discovery.aspx", false);

                    }

                    //DISLIKE / UNMATCH
                    if (Request.QueryString["Action"] is "Dislike")
                    {
                        connection.Open();
                        // if already friends then delete relation
                        OleDbCommand likeUser = new OleDbCommand($"DELETE FROM Friendship WHERE ((SenderUID = {Request.QueryString["toUser"]} ) AND(RecieverUID = {Session["UID"]}) OR (SenderUID = {Session["UID"]}) AND(RecieverUID = {Request.QueryString["toUser"]} )) AND(AreFriends = True)", connection);
                        likeUser.ExecuteNonQuery();
                        Response.Redirect("./discovery.aspx", false);
                        connection.Close();
                        connection.Open();
                        OleDbCommand delMessage = new OleDbCommand($"DELETE FROM Messages WHERE (From_User_UID = {Session["UID"]}) AND (To_User_UID = {Request.QueryString["toUser"]}) OR  (From_User_UID = {Request.QueryString["toUser"]}) AND (To_User_UID = {Session["UID"]})", connection);
                        delMessage.ExecuteNonQuery();
                        connection.Close();
                    }
                    // </Actions> 

                    //--FILL FILTERS 
                    connection.Open();
                    OleDbCommand selectCoffeesFilter = new OleDbCommand($"SELECT DISTINCT Pref_Coffee FROM Preferences",connection);
                    OleDbDataReader coffeeMatch = selectCoffeesFilter.ExecuteReader();
                    coffeeMatch.Read();
                    filter_coffees.DataSource = coffeeMatch;
                    filter_coffees.DataTextField = "Pref_Coffee";
                    filter_coffees.DataValueField = "Pref_Coffee";
                    filter_coffees.DataBind();
                    coffeeMatch.Close();
                    connection.Close();
                    filter_coffees.Items.Insert(0, new ListItem("All Coffees", ""));

                    connection.Open();
                    OleDbCommand selectWaketimeFilter = new OleDbCommand($"SELECT DISTINCT Pref_WakeHour FROM Preferences", connection);
                    OleDbDataReader wakeTimeMatch = selectWaketimeFilter.ExecuteReader();
                    wakeTimeMatch.Read();
                    filter_wakehour.DataSource = wakeTimeMatch;
                    filter_wakehour.DataTextField = "Pref_WakeHour";
                    filter_wakehour.DataValueField = "Pref_WakeHour";
                    filter_wakehour.DataBind();
                    wakeTimeMatch.Close();
                    connection.Close();
                    filter_wakehour.Items.Insert(0, new ListItem("Any time", ""));
                    //------------------------

                    // Show users that matches 
                    ShowUsers();
                }
                catch
                {
                    Response.Redirect("./login.aspx?action=error");
                }
            }   

        }
        protected void filter_coffees_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(filter_coffees.SelectedIndex != 0)
            {
                filterCoffee = $"AND Pref_Coffee = '{filter_coffees.SelectedValue}'";
            }
            else
            {
                filterCoffee = string.Empty;
            }

            ShowUsers();

        }

        protected void filter_wakehour_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(filter_wakehour.SelectedIndex != 0)
            {
                filterWakeTime = $"AND Pref_WakeHour = '{filter_wakehour.SelectedValue}'";
            }
            else
            {
                filterWakeTime = string.Empty;
            }
            ShowUsers();
        }


        private void ShowUsers()
        {
            main_content.InnerHtml = string.Empty;
            connection.Open();
            OleDbCommand selectUsersByPref = new OleDbCommand($"SELECT * " +
                                                              $"FROM Users INNER JOIN Preferences ON Users.UID = Preferences.RefUser " +
                                                              $"WHERE Users.UID NOT IN " +
                                                              $"(SELECT UID " +
                                                              $"FROM Users WHERE UID IN " +
                                                              $"(SELECT DISTINCT SenderUID " +
                                                              $"FROM Friendship " +
                                                              $"WHERE RecieverUID = {Session["UID"]} " +
                                                              $")) " +
                                                              $"AND " +
                                                              $"Users.UID NOT IN " +
                                                              $"(SELECT UID " +
                                                              $"FROM Users WHERE UID IN " +
                                                              $"(SELECT DISTINCT RecieverUID " +
                                                              $"FROM Friendship " +
                                                              $"WHERE(SenderUID = {Session["UID"]}) " +
                                                              $"))AND Gender = '{Session["Pref_Gender"]}' {filterCoffee} {filterWakeTime}"
                                                              , connection);
            OleDbDataReader possibleMatch = selectUsersByPref.ExecuteReader();
            if (!possibleMatch.HasRows)
            {
                main_content.InnerHtml += $"<h1>No Results</h1>";
            }
            else
            {
                while (possibleMatch.Read())
                {
                    main_content.InnerHtml += $"<div class='discover-user'>";
                    main_content.InnerHtml += $"     <img src = './assets/images/profiles/{possibleMatch["ProfilePicture"]}' height='50px'>";
                    main_content.InnerHtml += $"     <div class='user-info'>";
                    main_content.InnerHtml += $"          <h3>{possibleMatch["First_Name"]} {possibleMatch["Last_Name"]}</h3>";
                    main_content.InnerHtml += $"         <blockquote>";
                    main_content.InnerHtml += $"            {possibleMatch["Bio"]}";
                    main_content.InnerHtml += $"         </blockquote>";
                    main_content.InnerHtml += $"         <span style='font-size:12px;'>{possibleMatch["Pref_WakeHour"]} - {possibleMatch["Pref_Coffee"]}</span>";
                    main_content.InnerHtml += $"     </div>";
                    main_content.InnerHtml += $"     <a href = './discovery.aspx?action=Like&toUser={possibleMatch["UID"]}' style='position: absolute;top: 5px;right: 5px;' > <svg width='20' height='20' fill='currentColor' viewBox='0 0 24 24' xmlns='http://www.w3.org/2000/svg'>";
                    main_content.InnerHtml += $"         <path d = 'M12 4.597a5.904 5.904 0 0 0-6.278-1.1 5.942 5.942 0 0 0-1.93 1.3c-2.354 2.363-2.353 6.06.001 8.412l7.333 7.332a.995.995 0 0 0 1.32.382.99.99 0 0 0 .347-.299l7.415-7.415c2.354-2.354 2.354-6.049-.002-8.416a5.938 5.938 0 0 0-4.21-1.754 5.9 5.9 0 0 0-3.995 1.558Zm6.791 1.61c1.564 1.571 1.564 4.025.003 5.588L12 18.588l-6.794-6.793c-1.562-1.563-1.56-4.017-.002-5.584a3.953 3.953 0 0 1 2.8-1.172c1.044 0 2.034.416 2.788 1.17l.5.5a1 1 0 0 0 1.415 0l.5-.5c1.511-1.509 4.074-1.505 5.583-.002Z' ></ path >";
                    main_content.InnerHtml += $" </svg>";
                    main_content.InnerHtml += $" </a>";
                    main_content.InnerHtml += $" </div>";
                }
            }
            possibleMatch.Close();
            connection.Close();


            main_content.InnerHtml += "<hr style='width:100%;opacity:0.5;'>";

            // if liked
            connection.Open();
            OleDbCommand selectUsersByPref03 = new OleDbCommand($"SELECT * FROM Users, Friendship, Preferences WHERE (Users.UID = Friendship.RecieverUID AND Users.UID = RefUser) AND Friendship.SenderUID = {Session["UID"]} AND AreFriends = False", connection);
            OleDbDataReader possibleMatch03 = selectUsersByPref03.ExecuteReader();
            while (possibleMatch03.Read())
            {
                main_content.InnerHtml += $"<div class='discover-user' style='border:1px solid #ff004c'>";
                main_content.InnerHtml += $"     <img src = './assets/images/profiles/{possibleMatch03["ProfilePicture"]}' height='50px'>";
                main_content.InnerHtml += $"     <div class='user-info'>";
                main_content.InnerHtml += $"          <h3>{possibleMatch03["First_Name"]} {possibleMatch03["Last_Name"]} <span style='font-size:15px;'>&nbsp; LIKED💕</span></h3>";
                main_content.InnerHtml += $"         <blockquote>";
                main_content.InnerHtml += $"            {possibleMatch03["Bio"]}";
                main_content.InnerHtml += $"         </blockquote>";
                main_content.InnerHtml += $"         <span style='font-size:12px;'>{possibleMatch03["Pref_WakeHour"]} - {possibleMatch03["Pref_Coffee"]}</span>";
                main_content.InnerHtml += $"     </div>";
                main_content.InnerHtml += $" </div>";
            }
            possibleMatch03.Close();
            connection.Close();

            // if matched
            connection.Open();
            OleDbCommand selectUsersByPref02 = new OleDbCommand($"SELECT DISTINCT * FROM  Users INNER JOIN Preferences ON Users.UID = Preferences.RefUser WHERE (Gender = '{Session["Pref_Gender"]}') AND (UID IN (SELECT SenderUID FROM Friendship WHERE(RecieverUID = {Session["UID"]}) AND AreFriends = True)) OR (UID IN (SELECT RecieverUID FROM  Friendship Friendship_1 WHERE(SenderUID = {Session["UID"]})AND AreFriends = True))", connection);
            OleDbDataReader possibleMatch02 = selectUsersByPref02.ExecuteReader();
            while (possibleMatch02.Read())
            {
                main_content.InnerHtml += $"<div class='discover-user'>";
                main_content.InnerHtml += $"     <img src = './assets/images/profiles/{possibleMatch02["ProfilePicture"]}' height='50px'>";
                main_content.InnerHtml += $"     <div class='user-info'>";
                main_content.InnerHtml += $"          <h3>{possibleMatch02["First_Name"]} {possibleMatch02["Last_Name"]}</h3>";
                main_content.InnerHtml += $"         <blockquote>";
                main_content.InnerHtml += $"            {possibleMatch02["Bio"]}";
                main_content.InnerHtml += $"         </blockquote>";
                main_content.InnerHtml += $"         <span style='font-size:12px;'>{possibleMatch02["Pref_WakeHour"]} - {possibleMatch02["Pref_Coffee"]}</span>";
                main_content.InnerHtml += $"     </div>";
                main_content.InnerHtml += $" <a href='./discovery.aspx?action=Dislike&toUser={possibleMatch02["UID"]}' style='position: absolute;top: 8px;right: 5px;'>";
                main_content.InnerHtml += $" <svg width='20' height='20' fill='currentColor' viewBox='0 0 24 24' xmlns='http://www.w3.org/2000/svg'>";
                main_content.InnerHtml += $" <path d='M20.206 4.793a5.938 5.938 0 0 0-4.21-1.754 5.9 5.9 0 0 0-3.995 1.558 5.904 5.904 0 0 0-6.279-1.1 5.942 5.942 0 0 0-1.93 1.3c-2.354 2.363-2.353 6.06.001 8.412L12 21.416l8.207-8.207c2.354-2.353 2.355-6.049-.002-8.416Z'></path>";
                main_content.InnerHtml += $" </svg>";
                main_content.InnerHtml += $" </a>";
                main_content.InnerHtml += $" </div>";
            }
            possibleMatch02.Close();
            connection.Close();
        }
    }
}



