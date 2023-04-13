using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MatchingCoffees
{
    public partial class messenger : System.Web.UI.Page
    {
        // PAGE_GLOBAL VARIABLES //
        static OleDbConnection connection;
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
                    // </Actions> //

                    // Show matched users
                    connection.Open();
                    OleDbCommand selectUsersByPref02 = new OleDbCommand($"SELECT * FROM  Users WHERE (Gender = '{Session["Pref_Gender"]}') AND (UID IN (SELECT SenderUID FROM Friendship WHERE(RecieverUID = {Session["UID"]}) AND AreFriends = True)) OR (UID IN (SELECT RecieverUID FROM  Friendship Friendship_1 WHERE(SenderUID = {Session["UID"]})AND AreFriends = True))", connection);
                    OleDbDataReader possibleMatch02 = selectUsersByPref02.ExecuteReader();
                    while (possibleMatch02.Read())
                    {
                        main_content.InnerHtml += $"<a href='./chat.aspx?ToUser={possibleMatch02["UID"]}'>";
                        main_content.InnerHtml += $"<div class='discover-user' style='min-width: 280px;align-items: center;'>";
                        main_content.InnerHtml += $"     <img src = './assets/images/profiles/{possibleMatch02["ProfilePicture"]}' height='50px'>";
                        main_content.InnerHtml += $"     <div class='user-info'>";
                        main_content.InnerHtml += $"          <h3>{possibleMatch02["First_Name"]} {possibleMatch02["Last_Name"]}</h3>";
                        main_content.InnerHtml += $"     </div>";
                        main_content.InnerHtml += $" <svg width='20' height='20' fill='currentColor' viewBox='0 0 24 24' xmlns='http://www.w3.org/2000/svg'>";
                        main_content.InnerHtml += $" <path d='M20.206 4.793a5.938 5.938 0 0 0-4.21-1.754 5.9 5.9 0 0 0-3.995 1.558 5.904 5.904 0 0 0-6.279-1.1 5.942 5.942 0 0 0-1.93 1.3c-2.354 2.363-2.353 6.06.001 8.412L12 21.416l8.207-8.207c2.354-2.353 2.355-6.049-.002-8.416Z'></path>";
                        main_content.InnerHtml += $" </svg>";
                        main_content.InnerHtml += $" </div>";
                        main_content.InnerHtml += "</a>";
                    }
                    possibleMatch02.Close();
                    connection.Close();
                }
                catch
                {
                    Response.Redirect("./login.aspx?action=error");
                }
            }
        }
    }
}