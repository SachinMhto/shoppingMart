using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Xml.Schema;

namespace KingstonMartApp
{
    public partial class Mart : Form
    {
        int price = 0;
        int tax = 0;
        int discount = 0;
        int SrNo = 0;
        int Final = 0;
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public Mart()
        {
            InitializeComponent();
            UsertextBox.Text = LoginForm.username;
            getAllItems();
            getInvoiceid();
            grid1.ColumnCount = 8;
            grid1.Columns[0].Name = "SR NO";
            grid1.Columns[1].Name = "ITEM NAME";
            grid1.Columns[2].Name = "UNIT PRICE";
            grid1.Columns[3].Name = "DISCOUNT PER ITEM";
            grid1.Columns[4].Name = "QUANTITY";
            grid1.Columns[5].Name = "SUB TOTAL";
            grid1.Columns[6].Name = "TAX";
            grid1.Columns[7].Name = "TOTAL COST";

        }

        void getAllItems()
        {
            comboBox1.Items.Clear();
            SqlConnection con = new SqlConnection(cs);
            string querry = "SELECT * FROM items_tbl";
            SqlCommand cmd = new SqlCommand(querry, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string item_name = dr.GetString(1);
                comboBox1.Items.Add(item_name);
                comboBox1.Sorted = true;
            }
            con.Close();
        }
        void getPrice()
        {
            if (comboBox1.SelectedItem == null)
            {
            }
            else
            {
                SqlConnection con = new SqlConnection(cs);
                string querry = "SELECT item_price FROM items_tbl WHERE item_name=@name";
                SqlDataAdapter sda = new SqlDataAdapter(querry, con);
                sda.SelectCommand.Parameters.AddWithValue("@name", comboBox1.SelectedItem.ToString());
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    price = Convert.ToInt32(dt.Rows[0]["item_price"]);
                }
                UnittextBox.Text = price.ToString();
            }

        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            getPrice();
            itemDiscount();
            QuantitytextBox.Enabled = true;

        }
        void itemDiscount()
        {
            if (comboBox1.SelectedItem == null)
            {
            }
            else
            {
                SqlConnection con = new SqlConnection(cs);
                string querry = "SELECT item_discount FROM items_tbl WHERE item_name=@name";
                SqlDataAdapter sda = new SqlDataAdapter(querry, con);
                sda.SelectCommand.Parameters.AddWithValue("@name", comboBox1.SelectedItem.ToString());
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    discount = Convert.ToInt32(dt.Rows[0]["item_discount"]);
                }
                DiscounttextBox.Text = discount.ToString();
            }

        }

        private void QuantitytextBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(QuantitytextBox.Text))
            {
                errorProvider1.SetError(this.QuantitytextBox, "Cannot be Empty");
                Addbtn.Visible = false;

            }
            else if (QuantitytextBox.Text == "0" || QuantitytextBox.Text == "00" || QuantitytextBox.Text == "000")
            {
                errorProvider1.SetError(this.QuantitytextBox, "Quantity cannot be 0");
                Addbtn.Visible = false;

            }
            else
            {
                Addbtn.Visible = true;
            }

            int price;
            int discount;
            int quantity;

            if (!int.TryParse(UnittextBox.Text, out price))
            {
                errorProvider1.SetError(this.UnittextBox, "Invalid price format");
                return;
            }

            if (!int.TryParse(DiscounttextBox.Text, out discount))
            {
                errorProvider1.SetError(this.DiscounttextBox, "Invalid discount format");
                return;
            }

            if (!int.TryParse(QuantitytextBox.Text, out quantity))
            {
                errorProvider1.SetError(this.QuantitytextBox, "Invalid quantity format");
                return;
            }

            errorProvider1.SetError(this.QuantitytextBox, ""); // Clear any previous errors

            int subTotal = price * quantity;
            int totalcost = subTotal - (discount * quantity);
            SubtextBox.Text = subTotal.ToString();
        }


        private void SubtextBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SubtextBox.Text) == true) { }
            else
            {
                int subTotal = Convert.ToInt32(SubtextBox.Text);
                if (subTotal >= 10000)
                {
                    tax = (int)(subTotal * 0.15);
                    TaxtextBox.Text = tax.ToString();
                }
                else if (subTotal >= 6000)
                {
                    tax = (int)(subTotal * 0.10);
                    TaxtextBox.Text = tax.ToString();
                }
                else if (subTotal >= 3000)
                {
                    tax = (int)(subTotal * 0.7);
                    TaxtextBox.Text = tax.ToString();
                }
                else if (subTotal >= 1000)
                {
                    tax = (int)(subTotal * 0.5);
                    TaxtextBox.Text = tax.ToString();
                }
                else
                {
                    tax = (int)(subTotal * 0.2);
                    TaxtextBox.Text = tax.ToString();
                }
            }
        }

        private void TaxtextBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TaxtextBox.Text) == true) { }
            else
            {
                int subTotal = Convert.ToInt32(SubtextBox.Text);
                int tax = Convert.ToInt32(TaxtextBox.Text);
                int totalCost = subTotal + tax;
                TotaltextBox.Text = totalCost.ToString();
            }
        }
        void addDataToGridView(string Sr_No, string item_name, string Unit_Price, string discount, string Quantity, string Sub_Total, string tax, string total_cost)
        {
            string[] row = { Sr_No, item_name, Unit_Price, discount, Quantity, Sub_Total, tax, total_cost };
            grid1.Rows.Add(row);
        }

        private void Mart_Load(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void UnittextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void UsertextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void TotaltextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void Addbtn_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null) { }
            else
            {
                addDataToGridView((++SrNo).ToString(), comboBox1.SelectedItem.ToString(), UnittextBox.Text, DiscounttextBox.Text, QuantitytextBox.Text, SubtextBox.Text, TaxtextBox.Text, TotaltextBox.Text);
                RestControls();
                errorProvider1.Clear();
                CalculateFinalCost();
                AmounttextBox.Enabled = true;
            }
        }
        void RestControls()
        {
            comboBox1.SelectedItem = null;
            UnittextBox.Clear();
            DiscounttextBox.Clear();
            QuantitytextBox.Clear();
            SubtextBox.Clear();
            TaxtextBox.Clear();
            TotaltextBox.Clear();
            FinaltextBox.Clear();
            AmounttextBox.Clear();
            ChangetextBox.Clear();
            QuantitytextBox.Enabled = false;
            AmounttextBox.Enabled = false;
        }
        void CalculateFinalCost()
        {
            Final = 0;
            for (int i = 0; i < grid1.Rows.Count; i++)
            {
                Final = Final + Convert.ToInt32(grid1.Rows[i].Cells[7].Value);
            }
            FinaltextBox.Text = Final.ToString();
        }

        private void grid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AmounttextBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(AmounttextBox.Text) == true) { }
            else
            {
                int AmtPaid = Convert.ToInt32(AmounttextBox.Text);
                int FinalCost = Convert.ToInt32(FinaltextBox.Text);
                int change = AmtPaid - FinalCost;
                ChangetextBox.Text = change.ToString();
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void FinaltextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void ChangetextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            RestControls();
            errorProvider1.Clear();
        }
        void getInvoiceid()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "select invoice_id from order_master";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable data = new DataTable();
            sda.Fill(data);
            if (data.Rows.Count < 1)
            {
                textBox1.Text = "1";
            }
            else
            {
                string query2 = "select max(invoice_id) from order_master";
                SqlCommand cmd = new SqlCommand(query2, con);
                con.Open();
                int a = Convert.ToInt32(cmd.ExecuteScalar());
                a = a + 1;
                textBox1.Text = a.ToString();
                con.Close();
            }
        }
        int getlastInvoiceId() {
            SqlConnection con = new SqlConnection(cs);
            string querry = "select max(invoice_id) from order_master";
            SqlCommand cmd = new SqlCommand(querry,con);  
            con.Open();
            int maxInvoiceId=Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            return maxInvoiceId;
        }
        void insertIntoOrderdetail()
        {
            int a = 0;
            SqlConnection con = new SqlConnection(cs);
            try
            {
                for (int i = 0; i < grid1.Rows.Count-1;i++) {
                    string query = "INSERT INTO orders_tbl VALUES (@invoice, @name, @price, @discount, @quantity, @sub, @tax, @totalcost)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@invoice", getlastInvoiceId());
                    cmd.Parameters.AddWithValue("@name", grid1.Rows[i].Cells[1].Value.ToString());
                    cmd.Parameters.AddWithValue("@price", grid1.Rows[i].Cells[2].Value);
                    cmd.Parameters.AddWithValue("@discount", grid1.Rows[i].Cells[3].Value);
                    cmd.Parameters.AddWithValue("@quantity", grid1.Rows[i].Cells[4].Value);
                    cmd.Parameters.AddWithValue("@sub", grid1.Rows[i].Cells[5].Value);
                    cmd.Parameters.AddWithValue("@tax", grid1.Rows[i].Cells[6].Value);
                    cmd.Parameters.AddWithValue("@totalcost", grid1.Rows[i].Cells[7].Value);
                    a += cmd.ExecuteNonQuery();
                    con.Close();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void DiscounttextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void ClearGridViewBtn_Click(object sender, EventArgs e)
        {
            grid1.Rows.Clear();
            SrNo = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string querry = "insert into order_master values(@id,@user,@datetime,@finalcost)";
            SqlCommand cmd = new SqlCommand(querry, con);
            cmd.Parameters.AddWithValue("@id", textBox1.Text);
            cmd.Parameters.AddWithValue("@user", UsertextBox.Text);
            cmd.Parameters.AddWithValue("@datetime", DateTime.Now.ToString());
            cmd.Parameters.AddWithValue("finalcost", FinaltextBox.Text);
            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                getInvoiceid();
                insertIntoOrderdetail();
                RestControls();
                grid1.Rows.Clear();
                SrNo = 0;
               

            }
            else
            {
                MessageBox.Show("Error Saving Data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            con.Close();
           

        }

        private void QuantitytextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (char.IsDigit(ch) == true)
            {
                e.Handled = false;
            }
            else if (ch == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

        }

        private void AmounttextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (char.IsDigit(ch) == true)
            {
                e.Handled = false;
            }
            else if (ch == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // Set the A5 size dimensions in pixels (considering 96 DPI)
            int width = 420;  // A5 width in pixels (148mm * 96 DPI / 25.4mm/inch)
            int height = 595; // A5 height in pixels (210mm * 96 DPI / 25.4mm/inch)

            // Adjust positions and font sizes to fit within the A5 size
            Bitmap bmp = Properties.Resources.billimg;
            Image img = bmp;
            e.Graphics.DrawImage(img, 30, 5, width - 60, 100); // Adjusted image size
            e.Graphics.DrawString("Invoice Id: " + textBox1.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new PointF(30, 110));
            e.Graphics.DrawString("User Name: " + UsertextBox.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new PointF(30, 130));
            e.Graphics.DrawString("Date: " + DateTime.Now.ToShortDateString(), new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new PointF(30, 150));
            e.Graphics.DrawString("Time: " + DateTime.Now.ToShortTimeString(), new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new PointF(30, 170));
            e.Graphics.DrawString("-----------------------------------------------------------------------------------------------------", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new PointF(30, 190));
            e.Graphics.DrawString("Item", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new PointF(30, 210));
            e.Graphics.DrawString("Quantity", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new PointF(100, 210));
            e.Graphics.DrawString("Unit Price", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new PointF(200, 210));
            e.Graphics.DrawString("Discount", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new PointF(300, 210));
            e.Graphics.DrawString("-----------------------------------------------------------------------------------------------------", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new PointF(30, 230));


            int gap = 250;
            if (grid1.Rows.Count > 0)
            {
                for (int i = 0; i < grid1.Rows.Count; i++)
                {
                    try
                    {
                        if (grid1.Rows[i].Cells[1] != null && grid1.Rows[i].Cells[1].Value != null)
                        {
                            e.Graphics.DrawString(
                                grid1.Rows[i].Cells[1].Value.ToString(),
                                new Font("Arial", 12, FontStyle.Bold),
                                Brushes.Black,
                                new PointF(30, gap)
                            );

                            gap += 20; // Adjusted line gap
                        }
                    }
                    catch (Exception ex)
                    {
                        // Show a message box if an error occurs
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }

            int gap1 = 250;
            if (grid1.Rows.Count > 0)
            {
                for (int i = 0; i < grid1.Rows.Count; i++)
                {
                    try
                    {
                        if (grid1.Rows[i].Cells[4] != null && grid1.Rows[i].Cells[4].Value != null)
                        {
                            e.Graphics.DrawString(
                                grid1.Rows[i].Cells[4].Value.ToString(),
                                new Font("Arial", 12, FontStyle.Bold),
                                Brushes.Black,
                                new PointF(155, gap1)
                            );

                            gap1 += 20; // Adjusted line gap
                        }
                    }
                    catch (Exception ex)
                    {
                        // Show a message box if an error occurs
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }

            int gap2 = 250;
            if (grid1.Rows.Count > 0)
            {
                for (int i = 0; i < grid1.Rows.Count; i++)
                {
                    try
                    {
                        if (grid1.Rows[i].Cells[2] != null && grid1.Rows[i].Cells[2].Value != null)
                        {
                            e.Graphics.DrawString(
                                grid1.Rows[i].Cells[2].Value.ToString(),
                                new Font("Arial", 12, FontStyle.Bold),
                                Brushes.Black,
                                new PointF(250, gap2)
                            );

                            gap2 += 20; // Adjusted line gap
                        }
                    }
                    catch (Exception ex)
                    {
                        // Show a message box if an error occurs
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }

            int gap3 = 250;
            if (grid1.Rows.Count > 0)
            {
                for (int i = 0; i < grid1.Rows.Count; i++)
                {
                    try
                    {
                        if (grid1.Rows[i].Cells[3] != null && grid1.Rows[i].Cells[3].Value != null)
                        {
                            e.Graphics.DrawString(
                                grid1.Rows[i].Cells[3].Value.ToString(),
                                new Font("Arial", 12, FontStyle.Bold),
                                Brushes.Black,
                                new PointF(350, gap3)
                            );

                            gap3 += 20; // Adjusted line gap
                        }
                    }
                    catch (Exception ex)
                    {
                        // Show a message box if an error occurs
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
            e.Graphics.DrawString("-------------------------------------------------------", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new PointF(30, 550));

            int subTotalPrint = 0;
            for (int i = 0; i < grid1.Rows.Count; i++)
            {
                subTotalPrint = subTotalPrint + Convert.ToInt32(grid1.Rows[i].Cells[5].Value);
            }
            e.Graphics.DrawString("Sub Total: " + subTotalPrint.ToString(), new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new PointF(30, 570));


            int tax = 0;
            for (int i = 0; i < grid1.Rows.Count; i++)
            {
                tax = tax + Convert.ToInt32(grid1.Rows[i].Cells[6].Value);
            }
            e.Graphics.DrawString("Tax: " + tax.ToString(), new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new PointF(30, 590));

            int finalAmt = 0;
            for (int i = 0; i < grid1.Rows.Count; i++)
            {
                finalAmt = finalAmt + Convert.ToInt32(grid1.Rows[i].Cells[7].Value);
            }
            e.Graphics.DrawString("Final Amt: " + finalAmt.ToString(), new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new PointF(30, 610));
            e.Graphics.DrawString("-------------------------------------------------------", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new PointF(30, 630));
            e.Graphics.DrawString("Amount Paid: " + AmounttextBox.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new PointF(30, 650));
            e.Graphics.DrawString("Change: " + ChangetextBox.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new PointF(30, 670));
        }


        private void button3_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }

        private void addItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
            AddFormItem adf=new AddFormItem();
            adf.ShowDialog();

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Mart_Activated(object sender, EventArgs e)
        {
            getAllItems();
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void editItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditItem et = new EditItem();
            et.ShowDialog();
        }

        private void viewDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewData vd = new ViewData();
            vd.ShowDialog();
        }

        private void totalSalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AllSales all = new AllSales();
            all.ShowDialog();
            
        }
    }
}
    

    


