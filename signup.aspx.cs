using System;
using System.Web.UI;

namespace MatchingCoffees
{
    public partial class signup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e){}

        protected void Terms_CheckedChanged(object sender, EventArgs e)
        {
            // Checks if Terms&Conditions checkbox is checked
            // if checked, the NEXT button appears
            BTN_NextSignupPage.Visible = CHK_Terms.Checked ? true : false;
        }

        protected void BTN_NextSignupPage_Click(object sender, EventArgs e)
        {
            // Get & Format user's inputs
            string First_Name = TXT_First_Name.Text.Trim();
            string Last_Name = TXT_Last_Name.Text.Trim();
            string Username = TXT_Email.Text.Trim();
            string DOB = TXT_DOB.Text.Trim();
            string Gender = DRP_Gender.SelectedValue.ToString().Trim();
            string Password = TXT_Pass.Text.Trim();

            // if all information entered
            if (First_Name.Length != 0 && Last_Name.Length != 0 && Username.Length != 0 && DOB.Length != 0 && Gender.Length != 0 && Password.Length != 0)
            {
                // Create parameter string to send in the url for the second part of signing up
                string param = 
                    $"First_Name={First_Name}&"+
                    $"Last_Name={Last_Name}&"+
                    $"Username={Username}&"+
                    $"DOB={DOB}&"+
                    $"Gender={Gender}&"+
                    $"Password={Password}";

                // Redirect user with the params to the second part
                Response.Redirect($"./signupDetails.aspx?{param}");
            }
        }
    }
}