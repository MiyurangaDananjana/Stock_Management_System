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
using DGVPrinterHelper;

namespace Stock_Management_System
{
    public partial class Selling : Form
    {
        public Selling()
        {
            InitializeComponent();
            this.ActiveControl = txtcid;
            txtcid.Focus();
            
            lodtable();

            //datagardViwe data show
            dataGridView.ColumnCount = 5;

            dataGridView.Columns[0].Name = "Product Name";
            dataGridView.Columns[1].Name = "Date of purchase";
            dataGridView.Columns[2].Name = "Warranty Close Date";
            dataGridView.Columns[3].Name = "Quntiti";
            dataGridView.Columns[4].Name = "Price";

            txtdiscount.Text = "0";

        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-6FAC73U\SQLEXPRESS;Initial Catalog=Stock_Managment_System;Integrated Security=True");//database connection#

        private void histr()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            
            cmd.CommandText = "insert into History(Customer_ID,Product_Code,Orginal_Price,Puchars_Date,Warranty_close_date,Quantity,Discount,Discount_Given,Discount_Sale_Price,Profit) values ('" + txtcid.Text + "', '" + txtcode.Text + "', '" + txtsellin.Text + "', '" + this.dateTimePicker1.Text + "', '" + this.dateTimePicker2.Text + "', '" + txtquantity.Text + "', '" + txtdiscount.Text + "'  , '" + txtgiven.Text + "'  , '" + txtdiscountsale.Text + "'  , '" + txtprofit.Text + "' )";

            cmd.ExecuteNonQuery();
            con.Close();
            
           



        }
        private void printbill()

        {
            
                if (txtsellin.Text != "" && txtquantity.Text != "" && txtdiscount.Text != "")
            {
                int Orginal_Price = 0;
                double Discount, Discount_Give, quantity, sale_price = 0;

                Orginal_Price = int.Parse(txtsellin.Text);
                Discount = double.Parse(txtdiscount.Text);
                quantity = int.Parse(txtquantity.Text);

                Discount_Give = Orginal_Price * (Discount / 100);
                sale_price = ((Orginal_Price - Discount_Give) * quantity);

                //disply 

                txtgiven.Text = Discount_Give.ToString();
                txtdiscountsale.Text = sale_price.ToString();



                // decrease number
                int oquntity, selquntity, decrease;
                oquntity = int.Parse(txtquantity.Text);
                selquntity = int.Parse(textBox2.Text);

                decrease = (selquntity - oquntity);
                // dispaly decreasre number
                textBox4.Text = decrease.ToString();
                //add rowdata
                addRow(txtname.Text, dateTimePicker1.Text, dateTimePicker2.Text, txtquantity.Text, txtdiscountsale.Text);



                       //decrease
                                con.Open();
                                SqlCommand cmd = new SqlCommand();
                                cmd.Connection = con;
                                cmd.CommandType = CommandType.Text;
                                int Product_Code = Convert.ToInt32(txtcode.Text);
                                string id_num = textBox4.Text;
                                cmd.CommandText = "update  Stock  set Quantity= '" + id_num + "'  where Product_Code= '" + Product_Code + "' ";
                                cmd.ExecuteNonQuery();
                                con.Close();
                                lodtable();
                                histr();

            }
            else
            {

                MessageBox.Show("Select  Item !", "Message");

            }
            
        }
        private void addRow(string Product_Name ,string Oder_date, string waranty_date, string quantity, string sale_price)
        {
            string[] row = { Product_Name, Oder_date, waranty_date, quantity, sale_price };
            dataGridView.Rows.Add(row);
        }
        private void clear()
        {
            //clear all taxtbox and datagradviwe
            txtcid.Clear();
            txtcname.Clear();
            txtcode.Clear();
            txtname.Clear();
            txtsellin.Clear();
            dateTimePicker1.Text ="";
            dateTimePicker2.Text = "";
            txtquantity.Clear();
            txtdiscount.Clear();
            txtgiven.Clear();
            txtdiscountsale.Clear();
            textBox2.Clear();
            dataGridView.Rows.Clear();
            
            

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
                dataGridViewProduct.DataSource = dt;
                
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);
            }
        }


        private void Selling_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'stock_Managment_SystemDataSet5.Stock' table. You can move, or remove it, as needed.
            this.stockTableAdapter.Fill(this.stock_Managment_SystemDataSet5.Stock);
            // TODO: This line of code loads data into the 'stock_Managment_SystemDataSet4.Customer' table. You can move, or remove it, as needed.
            this.customerTableAdapter.Fill(this.stock_Managment_SystemDataSet4.Customer);


        }



        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewProduct.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGridViewProduct.CurrentRow.Selected = true;

                txtcode.Text = dataGridViewProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtname.Text = dataGridViewProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBox2.Text = dataGridViewProduct.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtsellin.Text = dataGridViewProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtprofit.Text = dataGridViewProduct.Rows[e.RowIndex].Cells[5].Value.ToString();

            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewCustomer.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGridViewCustomer.CurrentRow.Selected = true;

                txtcid.Text = dataGridViewCustomer.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtcname.Text = dataGridViewCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();


            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtcname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtcode.Focus();
            }
        }

        private void txtcid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtcname.Focus();
            }
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
                txtsellin.Focus();
            }
        }

        private void txtsellin_KeyDown(object sender, KeyEventArgs e)
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
                dateTimePicker2.Focus();
            }
        }

        private void dateTimePicker2_KeyDown(object sender, KeyEventArgs e)
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
                txtdiscount.Focus();
            }
        }

        private void txtdiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                printbill();
            }
        }

        private void txtgiven_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtdiscountsale.Focus();
            }
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            printbill();
            
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // code to print bill
            DGVPrinter Printer = new DGVPrinter();
            
            Printer.Title = "\r\n\r\n Miyuranga PVT.LTD. \r\n";
            Printer.SubTitle = "\r\n 076 0743231 \r\n";
            Printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            Printer.PageNumbers = false;
            Printer.PageNumberInHeader = false;
            Printer.PorportionalColumns = true;
            Printer.HeaderCellAlignment = StringAlignment.Near;
            Printer.Footer = "Customer Name : " + txtcname.Text + "\r\n\r\n" + "Customer ID :" + txtcid.Text;
            Printer.FooterSpacing = 15;
            Printer.PrintDataGridView(dataGridView);



        }

        private void stockBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }
    }
}
