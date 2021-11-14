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
using ClosedXML.Excel;

namespace Stock_Management_System
{
    public partial class Add_inventory : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-6FAC73U\SQLEXPRESS;Initial Catalog=Stock_Managment_System;Integrated Security=True");//database connection
        private SqlDataAdapter adapt;

        public Add_inventory()
        {
            InitializeComponent();
            lodtable();
            DisplayData();
            cleartextbox();
            focus();
            quantitysum();
            
        }
        private void quantitysum()
        {
            decimal colsum = 0;
            for (int i = 0; i < dataGridView.Rows.Count; ++i)
            {
                colsum += Convert.ToDecimal(dataGridView.Rows[i].Cells[3].Value);
            }
            lblquantity.Text = colsum.ToString();  //Sum data
        }
        private void export()// save exal sheet data
        {
            lodtable();
            dataGridView.SelectAll();
            DataObject copydata = dataGridView.GetClipboardContent();
            if (copydata != null) Clipboard.SetDataObject(copydata);
            Microsoft.Office.Interop.Excel.Application xlapp = new Microsoft.Office.Interop.Excel.Application();
            xlapp.Visible = true;
            Microsoft.Office.Interop.Excel.Workbook xlWbook;
            Microsoft.Office.Interop.Excel.Worksheet xlsheet;
            object miseddata = System.Reflection.Missing.Value;
            xlWbook = xlapp.Workbooks.Add(miseddata);
            xlsheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWbook.Worksheets.get_Item(1);
            Microsoft.Office.Interop.Excel.Range xrl = (Microsoft.Office.Interop.Excel.Range)xlsheet.Cells[1, 1];
            xrl.Select();
            xlsheet.PasteSpecial(xrl, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
        }

        private void focus()
        {
            this.ActiveControl = txtname;
            txtname.Focus();
        }
        private void DisplayData()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("select * from Stock", con);

            adapt.Fill(dt);
            dataGridView.DataSource = dt;
           

            con.Close();
        }
        private void cleartextbox()
        {
            lodtable();
            txtid.Clear();
            txtcode.Clear();
            txtname.Clear();
            txtquantity.Clear();
            txtprice.Clear();
            txtsprice.Clear();
            txtprofit.Clear();
            textBox1.Text = "";
        }
        private void lodtable()
        {
            try
            {

                con.Open();


                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Stock";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView.DataSource = dt;
                label12.Text = dt.Rows.Count.ToString(); // product show
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);
            }
        }


        private void btnaddinventory_Click(object sender, EventArgs e)
        {
            new Staff().Show();
        }

        private void Add_inventory_Load(object sender, EventArgs e)
        {
            
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e) // click event
        {
            if (dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGridView.CurrentRow.Selected = true;

                txtid.Text = dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtcode.Text = dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtname.Text = dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtquantity.Text = dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtprice.Text = dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtsprice.Text = dataGridView.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtprofit.Text = dataGridView.Rows[e.RowIndex].Cells[6].Value.ToString();
                textBox1.Text = dataGridView.Rows[e.RowIndex].Cells[7].Value.ToString();

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            lodtable();
            quantitysum();
            cleartextbox();
            DisplayData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            DialogResult dialogResult = MessageBox.Show("Update Item", "Massage", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (txtcode.Text != "" && txtname.Text != "" && txtquantity.Text != "" && txtprice.Text != "" && txtsprice.Text != "" && txtprofit.Text != "" && textBox1.Text != "")
                {
                    con.Open();
                    
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;

                    int id = Convert.ToInt32(txtid.Text);
                    string code = txtcode.Text;
                    string name = txtname.Text;
                    string quantity = txtquantity.Text;
                    string price = txtprice.Text;
                    string sprise = txtsprice.Text;
                    string profit = txtprofit.Text;
                    string sup = textBox1.Text;


                    cmd.CommandText = "update  Stock  set Product_Code= '" + code + "' , Product_Name= '" + name + "' , Quantity= '" + quantity + "' , Orginal_Price= '" + price + "' , Selling_Price= '" + sprise + "' , Profit= '" + profit + "' , Supplier= '" + sup + "'  where Product_ID= '" + id + "' ";

                    cmd.ExecuteNonQuery();
                    con.Close();
                    DisplayData();
                    lodtable();
                    cleartextbox();
                    quantitysum();
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

        private void txtprice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtsprice.Focus();
            }
        }

        private void txtsprice_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtprice.Text != "" && txtsprice.Text != "")
            {
                if (e.KeyCode == Keys.Enter)
                {
                    int orginal_price, selling_price;
                    orginal_price = int.Parse(txtprice.Text);
                    selling_price = int.Parse(txtsprice.Text);
                    txtprofit.Text = (selling_price - orginal_price).ToString();
                    textBox1.Focus();
                }

            }
            else
            {
                MessageBox.Show("Select Update Item !", "Massage");
                textBox1.Focus();
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

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnupdate.PerformClick();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("Delete Product", "Massage", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (txtcode.Text != "" && txtname.Text != "" && txtquantity.Text != "" && txtprice.Text != "" && txtsprice.Text != "" && txtprofit.Text != "" && textBox1.Text != "")
                {

                    string query = "Delete from Stock where Product_ID = '" + this.txtid.Text + "'";
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
                    quantitysum();
                    cleartextbox();

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

        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            string query = "select * from Stock where Product_Name ='" + textBox3.Text + "'";
            SqlDataAdapter da = new SqlDataAdapter(query,con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView.DataSource = ds.Tables[0];
            con.Close();
            textBox3.Clear();
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button3.PerformClick();
            }
        }

        private void Profit_Tick(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {


            export();

        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            cleartextbox();
        }
    }
}
