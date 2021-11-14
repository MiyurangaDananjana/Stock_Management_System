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
    public partial class txtdate : Form
    {
        public txtdate()
        {
            InitializeComponent();
            clear();
            this.ActiveControl = txtid;
            txtid.Focus();
            lodtable();
            DisplayData();

        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-6FAC73U\SQLEXPRESS;Initial Catalog=Stock_Managment_System;Integrated Security=True");//database connection
        private SqlDataAdapter adapt;

        
        private void clear()
        {
            textBox1.Clear();
            txtid.Clear();
            txtfname.Clear();
            txtlname.Clear();
            txtemail.Clear();
            txtphone.Clear();
            txtaddress.Clear();
            dateTimePicker1.Text= "";
            



        }
        private void DisplayData()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("select * from Customer", con);

            adapt.Fill(dt);
            dataGridView.DataSource = dt;


            con.Close();
        }
        private void lodtable()
        {
            try
            {

                con.Open();


                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Customer";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView.DataSource = dt;
                //label12.Text = dt.Rows.Count.ToString(); // product show
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);
            }
        }
        private void btnsave_Click(object sender, EventArgs e)
        {
            if (txtid.Text != "" && txtlname.Text != "" && txtfname.Text != "" && txtemail.Text != "" && txtphone.Text != "" && txtaddress.Text != "" && dateTimePicker1.Text != "")
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into Customer(ID_number,Frist_Name,Last_Name,Email,Phone_num,Address,Date) values ('" + txtid.Text + "', '" + txtfname.Text + "', '" + txtlname.Text + "', '" + txtemail.Text + "', '" + txtphone.Text + "', '" + txtaddress.Text + "', '" + dateTimePicker1.Text + "'    )";
                
                cmd.ExecuteNonQuery();
                con.Close();
                lodtable();
                MessageBox.Show("Record has been inserted!");
                clear();

            }
            else
            {

                MessageBox.Show("Data Not Insert Pleas Fill tha data!", "Message");
            }
        }

        private void txtdate_Load(object sender, EventArgs e)
        {

        }

        private void txtid_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                txtfname.Focus();
            }
        }

        private void txtfname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtlname.Focus();
            }
        }

        private void txtlname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtemail.Focus();
            }
        }

        private void txtemail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtphone.Focus();
            }
        }

        private void txtphone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtaddress.Focus();
            }
        }

        private void txtaddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               dateTimePicker1.Focus();
            }
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnsave.PerformClick();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGridView.CurrentRow.Selected = true;

                textBox1.Text = dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtid.Text = dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtfname.Text = dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtlname.Text = dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
               txtemail.Text = dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtphone.Text = dataGridView.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtaddress.Text = dataGridView.Rows[e.RowIndex].Cells[6].Value.ToString();
                dateTimePicker1.Text = dataGridView.Rows[e.RowIndex].Cells[7].Value.ToString();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Update Item", "Massage", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (txtid.Text != "" && txtfname.Text != "" && txtlname.Text != "" && txtemail.Text != "" && txtphone.Text != "" && txtaddress.Text != "" && dateTimePicker1.Text != "")
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                     
                    int id = Convert.ToInt32(textBox1.Text);
                    string id_num = txtid.Text;
                    string fname = txtfname.Text;
                    string lname = txtlname.Text;
                    string email = txtemail.Text;
                    string phone = txtphone.Text;
                    string adress = txtaddress.Text;
                    string date = dateTimePicker1.Text;


                    cmd.CommandText = "update  Customer  set ID_number= '" + id_num + "' , Frist_Name= '" + fname + "' , Last_Name= '" + lname + "' , Email= '" + email + "' , Phone_num= '" + phone + "' , Address= '" + adress + "' , Date= '" + date+ "'  where id= '" + id + "' ";
                    
                    cmd.ExecuteNonQuery();
                    con.Close();
                    DisplayData();
                    lodtable();
                    clear();
                    MessageBox.Show("Update Succses full !", "Massage");




                }
                else
                {

                    MessageBox.Show("Select Update Item !", "Massage");

                }
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Delete Product", "Massage", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (textBox1.Text != "" )
                {

                    string query = "Delete from Customer where id = '" + this.textBox1.Text + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader myreader;

                    try
                    {
                        con.Open();
                        myreader = cmd.ExecuteReader();


                        while (myreader.Read())
                        {
                        }
                        con.Close();

                        lodtable();
                        clear();

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Record Deleted Successfully!");
                    }
                }
                else
                {

                    MessageBox.Show("Select Delete Item !", "Massage");

                }

            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
                txtid.Clear();
            txtfname.Clear();
            txtlname.Clear();
            txtemail.Clear();
            txtphone.Clear();
            txtaddress.Clear();
            dateTimePicker1.Text = "";
        }

        private class privet
        {
        }
    }
}
