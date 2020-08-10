using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LoginSystem
{

    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Opens connection to server
            SqlConnection con = new SqlConnection(@"Server Source");
            con.Open();

            // Checks the server if there is an existing account with the users username
            SqlCommand sqa = new SqlCommand("Select count(*) From tblLogin where Username ='" + tbUsername.Text + "'", con);

            // Checks the server and gives a count
            int check = (int)sqa.ExecuteScalar();

            // Throws 1 if there is an account with the username
            if (check >= 1)
            {
                // Show a message that the username already exists
                MessageBox.Show("The Username "+ tbUsername.Text + " already exists. Please Try again!", "Failed Registration Authentication");
            }
            else
            {
                // Inserts the new users username and password into the server
                SqlCommand cmd = new SqlCommand("INSERT INTO tblLogin (Username,Password) VALUES (@Username,@Password)", con);
                // Puts in these values into the given data points
                cmd.Parameters.AddWithValue("@Username", tbUsername.Text);
                cmd.Parameters.AddWithValue("@Password", tbPassword.Text);
                // Executes the data input
                cmd.ExecuteNonQuery();

                // Closes this window and causes the user to go to the Login Screen to login
                this.Hide();
                Login login = new Login();
                login.Show();
            }

        }

        private void Registration_Load(object sender, EventArgs e)
        {

        }
    }
}
