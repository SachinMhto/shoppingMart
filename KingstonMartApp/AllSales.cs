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

namespace KingstonMartApp
{
    public partial class AllSales : Form
    {
        public AllSales()
        {
            InitializeComponent();
        }
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

        private void AllSales_Load(object sender, EventArgs e)
        {
            load();
        }
        void load() { 
        SqlConnection con=new SqlConnection(cs);
            string q = "sp_getBothTablesData";
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.CommandType=CommandType.StoredProcedure;
            SqlDataAdapter sda=new SqlDataAdapter();
            sda.SelectCommand = cmd;
            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           loadDT();
        }
        void loadDT()
        {
            DateTime startDate = dateTimePicker1.Value;
            DateTime endDate = dateTimePicker2.Value;

            if (startDate<endDate) {

                SqlConnection con = new SqlConnection(cs);
                string q = "sp_getBothTablesDate";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameters
                cmd.Parameters.AddWithValue("@StartDate", dateTimePicker1.Value.ToString());
                cmd.Parameters.AddWithValue("@EndDate", dateTimePicker2.Value.ToString());

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataTable data = new DataTable();
                sda.Fill(data);
                dataGridView1.DataSource = data;
                dataGridView1.Columns[9].Visible = false;
            }
            else {
                MessageBox.Show("Start date must be less than", "Invalid Date Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            load();
        }
    }
}
