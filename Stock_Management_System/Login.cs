using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Stock_Management_System
{
    public partial class formlogin : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-6FAC73U\SQLEXPRESS;Initial Catalog=Stock_Managment_System;Integrated Security=True");//database connection
        public formlogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)

        {

            SqlDataAdapter sda = new SqlDataAdapter("SELECT COUNT(*) FROM User_Login WHERE username='" + txtusername.Text + "' AND password='" + txtpassword.Text + "'", con);
            /* in above line the program is selecting the whole data from table and the matching it with the user name and password provided by user. */
            DataTable dt = new DataTable(); //this is creating a virtual table  
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {

                this.Hide();
                new Home().Show();
                txtusername.Text = "";
                txtpassword.Text = "";



            }

            else
            {
                MessageBox.Show("Invalid username or password");
                txtusername.Text = "";
                txtpassword.Text = "";
            }
        }

        private void txtusername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtpassword.Focus();
            }
        }

        private void txtpassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtpassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnlogin.PerformClick();
            }
        }

        private void txtusername_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
