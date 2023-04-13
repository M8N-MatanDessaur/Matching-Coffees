using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MatchingCoffees
{
    public partial class chat : System.Web.UI.Page
    {
        // PAGE_GLOBAL VARIABLES //
        static OleDbConnection connection;
        public string toUser_UID;
        public string toUser_First_Name;
        public string toUser_Last_Name;
        public string toUser_ProfilePicture;
        ///////////////////////////
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    // Create connection when first-time loading
                    connection = new OleDbConnection();
                    connection.ConnectionString = @" Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + Server.MapPath(@"App_Data\MatchingCoffeesDB.accdb; Persist Security Info = True");
                }

                // <Actions> //
                // </Actions> //

                // GET ToUser INFORMATION
                connection.Open();
                OleDbCommand toUserInfo = new OleDbCommand($"SELECT UID, First_Name, Last_Name, ProfilePicture FROM Users Where UID = {Request.QueryString["ToUser"]}", connection);
                OleDbDataReader readToUserInfo = toUserInfo.ExecuteReader();
                readToUserInfo.Read();
                toUser_UID = readToUserInfo["UID"].ToString();
                toUser_First_Name = readToUserInfo["First_Name"].ToString();
                toUser_Last_Name = readToUserInfo["Last_Name"].ToString();
                toUser_ProfilePicture = readToUserInfo["ProfilePicture"].ToString();
                readToUserInfo.Close();
                connection.Close();

                //GET MESSAGES
                connection.Open();
                OleDbCommand getMessages = new OleDbCommand($"SELECT * FROM Messages Where (To_User_UID = {Session["UID"]} AND From_User_UID = {toUser_UID}) OR (To_User_UID = {toUser_UID} AND From_User_UID = {Session["UID"]}) ORDER BY Date_Sent", connection);
                OleDbDataReader readMessages = getMessages.ExecuteReader();
                while (readMessages.Read())
                {
                    if (readMessages["From_User_UID"].ToString() == toUser_UID)
                    {
                        messages_container.InnerHtml += $"<div class='msg-recieved'><p>{readMessages["body"]}</p></div>";
                    }
                    if (readMessages["From_User_UID"].ToString() == Session["UID"].ToString())
                    {
                        messages_container.InnerHtml += $"<div class='msg-sent'><p>{readMessages["body"]}</p></div>";
                    }
                }
                readMessages.Close();
                connection.Close();
            }
            catch
            {
                Response.Redirect("./login.aspx?action=error");
            }
        }
        // Send message
        protected void BTN_SEND_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand sendMessage = new OleDbCommand($"INSERT INTO Messages([From_User_UID], [To_User_UID], [Body]) VALUES({Session["UID"]}, {toUser_UID}, '{TXT_msg.Text.Replace("'", "''")}')", connection);
                sendMessage.ExecuteNonQuery();
                Response.Redirect($"./chat.aspx?ToUser={toUser_UID}", false);
                connection.Close();
            }
            catch
            {
                Response.Redirect("./login.aspx?action=error");
            }
        }
    }
}