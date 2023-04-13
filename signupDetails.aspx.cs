using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MatchingCoffees
{
    public partial class signupDetails : System.Web.UI.Page
    {
        // PAGE_GLOBAL VARIABLES //
        static OleDbConnection connection;
        ///////////////////////////
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                // Create connection when first-time loading
                connection = new OleDbConnection();
                connection.ConnectionString = @" Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + Server.MapPath(@"App_Data\MatchingCoffeesDB.accdb; Persist Security Info = True");
            }
        }

        protected void BTN_Register_Click(object sender, EventArgs e)
        {
            // Get & Format user's inputs
            string Pref_Gender = DRP_PrefGender.SelectedValue.ToString().Trim();
            string Pref_CoffeeTime = TXT_CoffeeTime.Text.Trim();
            string Pref_CoffeeType = DRP_CoffeeTypes.SelectedValue.ToString().Trim();
            string UserBio = TXT_Bio.Text;
            string ProfilePicture = string.Empty;
            string ProfilePicturePath = string.Empty;

            // if all information entered
            if (Pref_Gender.Length != 0 && Pref_CoffeeTime.Length != 0 && Pref_CoffeeType.Length != 0 && UserBio.Length != 0)
            {
                // if a profile picture was attached, get it, else leave it as empty
                if (FILE_ProfilePicture.HasFile)
                {
                    ProfilePicture = FILE_ProfilePicture.FileName;
                    string path = Path.GetFileName(ProfilePicture);
                    path = path.Replace(" ", "");
                    FILE_ProfilePicture.SaveAs(Server.MapPath("~/assets/images/profiles/") + path);
                }

                connection.Open();
                    // Insert New user informations in Users table
                    OleDbCommand insertNewUser = new OleDbCommand(
                        "INSERT INTO Users(Username, First_Name, Last_Name, DOB, Gender, ProfilePicture, Bio, [Password])" +
                        $"VALUES('{Request.QueryString["Username"]}','{Request.QueryString["First_Name"]}','{Request.QueryString["Last_Name"]}','{Request.QueryString["DOB"]}','{Request.QueryString["Gender"]}','{ProfilePicture}','{UserBio}', '{Request.QueryString["Password"]}')",
                        connection
                    );
                    insertNewUser.ExecuteNonQuery();
                connection.Close();
                connection.Open();
                    // Select the last entered user to get the UID
                    OleDbCommand newUserUID = new OleDbCommand($"SELECT * FROM Users WHERE (Username = '{Request.QueryString["Username"]}')", connection);
                    OleDbDataReader getNewUserUID = newUserUID.ExecuteReader();
                    getNewUserUID.Read();
                        // Insert user's preferences using his UID as a reference
                        OleDbCommand insertNewUserPref = new OleDbCommand(
                            "INSERT INTO Preferences(Pref_Coffee, Pref_WakeHour, Pref_Gender, RefUser)" +
                            $"VALUES('{Pref_CoffeeType}','{Pref_CoffeeTime}','{Pref_Gender}', {getNewUserUID["UID"]})",
                            connection
                        );
                        insertNewUserPref.ExecuteNonQuery();
                    getNewUserUID.Close();
                connection.Close();

                // Redirect user to the login page
                Response.Redirect("./login.aspx");
            }
        }
    }
}