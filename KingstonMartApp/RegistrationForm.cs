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
using System.Configuration;

namespace KingstonMartApp
{
    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
        }
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "insert into signup_tbl values(@name,@gender,@age,@address,@email,@password)";
            SqlCommand cmd=new SqlCommand(query, con);
            con.Open();
            cmd.Parameters.AddWithValue("@name",textBox1.Text);
            cmd.Parameters.AddWithValue("@gender",comboBox1.SelectedItem);
            cmd.Parameters.AddWithValue("age",Convert.ToInt32(numericUpDown1.Value));
            cmd.Parameters.AddWithValue("address",textBox4.Text);
            cmd.Parameters.AddWithValue("@email",textBox5.Text);
            cmd.Parameters.AddWithValue("@password",textBox6.Text);
            int a=cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Successfully Registered..", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                LoginForm lg = new LoginForm();
                lg.Show();
            }
            else {
                MessageBox.Show("Failed To Register..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            con.Close();
        }
    }
}
