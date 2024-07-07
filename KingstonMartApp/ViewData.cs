using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace KingstonMartApp
{
    public partial class ViewData : Form
    {
        public ViewData()
        {
            InitializeComponent();
        }
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        private void ViewData_Load(object sender, EventArgs e)
        {
            BindGridView();
        }
        void BindGridView()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    string query = "select * from items_tbl";
                    SqlDataAdapter sda = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddFormItem ad = new AddFormItem();
            ad.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EditItem ed = new EditItem();
            ed.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EditItem ed = new EditItem();
            ed.ShowDialog();
        }

        private void ViewData_Activated(object sender, EventArgs e)
        {
            BindGridView();
        }
    }
}
