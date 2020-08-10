using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LoginSystem
{
    public partial class Login : Form
    {

        int countFailed;

        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Opens connection to the server
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dylan\Documents\dbLogin.mdf;Integrated Security=True;Connect Timeout=30");
            
            // Checks if the username and password match in the server
            SqlDataAdapter sqa = new SqlDataAdapter("Select count(*) From tblLogin where Username ='" + tbUsername.Text + "' and Password = '" + tbPassword.Text + "'", con);
            // Inputs the users data into a data table
            DataTable dt = new DataTable();
            sqa.Fill(dt);

            // Checks if the user exists in the server
            if (dt.Rows[0][0].ToString() == "1")
            {
                // Closes the login screen and opens the main program
                this.Hide();
                Main main = new Main();
                main.Show();
            } else
            {
                // Prompts the user of the remaining tries of failed logins before the program closes
                string remainingTries = (2 - countFailed).ToString();
                MessageBox.Show("Username / Password is incorrect. Please Try again!\n\nTries Left: " + remainingTries, "Failed Login Authentication");
                countFailed++;
            }

            if (countFailed == 3)
            {
                // Closes the program when the login limit is reached
                Environment.Exit(-1);
            }

        }
        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void btnRegistration_Click(object sender, EventArgs e)
        {
            // Closes login screen and prompts the user to registration menu
            this.Hide();
            Registration registration = new Registration();
            registration.Show();
        }
    }
}
