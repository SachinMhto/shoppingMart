using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace KingstonMartApp
{
    public partial class EditItem : Form
    {
        private DataTable dt=new DataTable();

        public EditItem()
        {
            InitializeComponent();
            BindGridView();
            textBox5.TextChanged += textBox5_TextChanged; // Subscribe to the TextChanged event
        }

        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con=new SqlConnection(cs);
            string querry = "update items_tbl set item_Name=@name, item_Price=@price, item_Discount=@disc where item_id=@id";
            SqlCommand cmd = new SqlCommand(querry,con);
            con.Open();
            cmd.Parameters.AddWithValue("@name",textBox1.Text);
            cmd.Parameters.AddWithValue("@price", textBox2.Text);
            cmd.Parameters.AddWithValue("@disc", textBox3.Text);
            cmd.Parameters.AddWithValue("@id", textBox4.Text);
            int a =cmd.ExecuteNonQuery();
            if (a>0) {
                MessageBox.Show("Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                reset();
            }
            else {
                MessageBox.Show("Error Updating Data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            con.Close();
            BindGridView();
        }
        void reset()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();

        }
        void BindGridView()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    string query = "select * from items_tbl";
                    SqlDataAdapter sda = new SqlDataAdapter(query, con);
                    dt = new DataTable();
                    sda.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            DataView dvItems= dt.DefaultView;
            dvItems.RowFilter = "item_Name like '%"+textBox5.Text+"%'";
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();


        }

        private void button2_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(cs);
            string querry = "delete from items_tbl where item_Id=@id";
            SqlCommand cmd = new SqlCommand(querry, con);
            con.Open();
            cmd.Parameters.AddWithValue("@id", textBox4.Text);
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Deleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                reset();
            }
            else
            {
                MessageBox.Show("Error Deleting Data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            con.Close();
            BindGridView();
        }
    
    }
}
