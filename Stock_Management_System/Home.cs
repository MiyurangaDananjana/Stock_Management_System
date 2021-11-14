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
using System.Data.OleDb;

namespace Stock_Management_System
{
    
    public partial class Home : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-6FAC73U\SQLEXPRESS;Initial Catalog=Stock_Managment_System;Integrated Security=True");//database connection
        public Home()
        {

            InitializeComponent();
            DisplayData();
            DisplayDatacount();
            total_customer();
            Profit();
            Date.Start();
        }
        private void Profit()
        {
            try
            {
                con.Open();


                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                
                cmd.CommandText = "SELECT SUM(Profit) Puchars_Date FROM History WHERE DATEPART(m, Puchars_Date) = DATEPART(m, DATEADD(m, 0, getdate()))";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                object result = cmd.ExecuteScalar();
                label3.Text = Convert.ToString(result);
                con.Close();

                ////////////////////////////////////////////////////////////////////////////////////////////////

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);
            }
        }
        private void DisplayData()
        {
            try
            {
                con.Open();


                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT SUM(Quantity) FROM Stock";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                object result = cmd.ExecuteScalar();
                lblitems.Text = Convert.ToString(result);
                con.Close();

                ////////////////////////////////////////////////////////////////////////////////////////////////
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);
            }
        }
        private void DisplayDatacount()
        {
            try
            {


                ////////////////////////////////////////////////////////////////////////////////////////////////
                con.Open();


                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Stock";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                
                label12.Text = dt.Rows.Count.ToString(); // product show
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);
            }
        }
        private void total_customer()
        {
            try
            {


                ////////////////////////////////////////////////////////////////////////////////////////////////
                con.Open();


                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Customer";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                label6.Text = dt.Rows.Count.ToString(); // product show
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);
            }
        }



        private void btnaddinventory_Click(object sender, EventArgs e)
        {
            
            new Add_inventory().Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Selling().Show();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            




        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            new txtdate().Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer_total_Tick(object sender, EventArgs e)
        {
            DisplayData();
            DisplayDatacount();
            total_customer();
            Profit();

        }

        private void Date_Tick(object sender, EventArgs e)
        {
            label13.Text = DateTime.Now.ToString("MM/dd/yyyy");
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            new Dashbord().Show();
        }
    }
}
