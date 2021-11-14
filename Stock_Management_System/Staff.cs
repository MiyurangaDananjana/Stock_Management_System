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
    public partial class Staff : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-6FAC73U\SQLEXPRESS;Initial Catalog=Stock_Managment_System;Integrated Security=True");//database connection
        public Staff()
        {
            InitializeComponent();
            this.ActiveControl = txtcode;
            txtcode.Focus();

        }
        public void supplier()
        {
           
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            if (txtcode.Text != "" && txtname.Text != "" && txtquantity.Text != "" && txtprice.Text != "" && txtsprice.Text != "" && txtprofit.Text != "" && comsupplier.Text != "")
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into Stock (Product_Code,Product_Name,Quantity,Orginal_Price,Selling_Price,Profit,Supplier) values ('" + txtcode.Text + "', '" + txtname.Text + "', '" + txtquantity.Text + "', '" + txtprice.Text + "', '" + txtsprice.Text + "', '" + txtprofit.Text + "', '" + comsupplier.Text + "'    )";
                
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record has been inserted!");
                txtcode.Clear();
                txtname.Clear();
                txtquantity.Clear();
                txtprice.Clear();
                txtsprice.Clear();
                txtprofit.Clear();
                comsupplier.Text = "";
            }
            else
            {
                
                MessageBox.Show("Data Not Insert Pleas Fill tha data!", "Message");
            }
        }

        private void txtquantity_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            txtcode.Clear();
            txtname.Clear();
            txtquantity.Clear();
            txtprice.Clear();
            txtsprice.Clear();
            txtprofit.Clear();
            comsupplier.Text = "";
        }

        private void comsupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void txtcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtname.Focus();
            }
        }

        private void txtname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               txtquantity.Focus();
            }
        }

        private void txtquantity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               txtprice.Focus();
            }
        }

        private void txtprice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtsprice.Focus();
            }
        }

        private void txtsprice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtprofit.Focus();
            }
        }

        private void txtprofit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                comsupplier.Focus();
            }
        }

        private void comsupplier_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnsave.PerformClick();
            }
        }

        private void Staff_Load(object sender, EventArgs e)
        {

        }
    }
}
