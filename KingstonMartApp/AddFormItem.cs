using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace KingstonMartApp
{
    public partial class AddFormItem : Form
    {
        public AddFormItem()
        {
            InitializeComponent();
        }
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(cs);
                string querry = "INSERT INTO items_tbl (item_name, item_price, item_Discount) VALUES (@name, @price, @disc)";
                SqlCommand cmd = new SqlCommand(querry, con);
                con.Open();
                cmd.Parameters.AddWithValue("@name", textBox1.Text);
                cmd.Parameters.AddWithValue("@price", textBox2.Text);
                cmd.Parameters.AddWithValue("@disc", textBox3.Text);
                int a = cmd.ExecuteNonQuery();
                if (a > 0)
                {
                    MessageBox.Show("New Product Added..", "Succcess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    resetClear();
                }
                else
                {
                    MessageBox.Show("Failed To Add..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Found: " + ex.Message);
            }
            void resetClear()
            {
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox1.Focus();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            if (char.IsDigit(c))
            {
                e.Handled = false;
            }
            else if (c == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            if (char.IsDigit(c))
            {
                e.Handled = false;
            }
            else if (c == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

        }
    }
}
