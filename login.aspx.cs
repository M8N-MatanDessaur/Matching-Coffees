using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MatchingCoffees
{
    public partial class login : System.Web.UI.Page
    {
        // PAGE_GLOBAL VARIABLES //
        static OleDbConnection connection;
        ///////////////////////////
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // <action> //
                if (Request.QueryString["action"] == "disconnect")
                {
                    Session.Clear();
                    Session.Abandon();
                    Response.Redirect("./login.aspx");
                }
                if (Request.QueryString["action"] == "error")
                {
                    Session.Clear();
                    Session.Abandon();
                    error_msg.InnerHtml = (@"<script> Toastify({text: 'An error has occured', duration: 3000,gravity: 'bottom', stopOnFocus: true, style:{background: '#ff004c'}}).showToast(); </script>");
                }
                // </action> //

                // Create connection when first-time loading
                connection = new OleDbConnection();
                connection.ConnectionString = @" Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + Server.MapPath(@"App_Data\MatchingCoffeesDB.accdb; Persist Security Info = True");
            }
        }

        protected void BTN_Login_Click(object sender, EventArgs e)
        {
            // if all information entered
            if (TXT_Username.Text.Length != 0 && TXT_Pass.Text.Length != 0) 
            {
                // Test if the entered information corresponds with the database 
                // Checks if user exists
                connection.Open();
                    OleDbCommand selectUser = new OleDbCommand($"SELECT Users.UID, Users.Username, Users.First_Name, Users.Last_Name, Users.DOB, Users.Gender, Users.Bio, Users.ProfilePicture, Users.[Password], Users.Online FROM Users WHERE Username = '{TXT_Username.Text.Trim()}' AND Password = '{TXT_Pass.Text.Trim()}'",connection);
                    OleDbDataReader searchForExistingUser = selectUser.ExecuteReader();
                    searchForExistingUser.Read();
                        if (searchForExistingUser.HasRows)
                        {                            
                            // Sends user's info to Session
                            Session["First_Name"] = searchForExistingUser["First_Name"];
                            Session["Last_Name"] = searchForExistingUser["Last_Name"];
                            Session["Username"] = searchForExistingUser["Username"];
                            Session["UID"] = searchForExistingUser["UID"];
                            Session["DOB"] = searchForExistingUser["DOB"];
                            Session["ProfilePicture"] = searchForExistingUser["ProfilePicture"];
                            Session["Gender"] = searchForExistingUser["Gender"];

                            // Selects User's preferences
                            OleDbCommand selectPref = new OleDbCommand($"SELECT * FROM PREFERENCES WHERE RefUser = {searchForExistingUser["UID"]}", connection);
                            searchForExistingUser.Close();
                            OleDbDataReader searchUserPref = selectPref.ExecuteReader();
                            searchUserPref.Read();
                            if (searchUserPref.HasRows)
                            {
                                // Sends user's preferences to Session
                                Session["Pref_Coffee"] = searchUserPref["Pref_Coffee"];
                                Session["Pref_WakeHour"] = searchUserPref["Pref_WakeHour"];
                                Session["Pref_Gender"] = searchUserPref["Pref_Gender"];
                                Response.Redirect("./discovery.aspx");
                            }
                            else
                            {
                                error_msg.InnerHtml = (@"<script> Toastify({text: 'An error has occured', duration: 3000,gravity: 'bottom', stopOnFocus: true, style:{background: '#ff004c'}}).showToast(); </script>");
                            }
                            searchUserPref.Close();
                        }
                        else
                        {
                            error_msg.InnerHtml = (@"<script> Toastify({text: 'Email or password is incorrect', duration: 3000,gravity: 'bottom', stopOnFocus: true, style:{background: '#ff004c'}}).showToast(); </script>");
                        }
                    
                connection.Close(); 
            }
        }
    }
}
