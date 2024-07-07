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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public static string username = "";
        public static string password = "";

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            SqlConnection con=new SqlConnection(cs);
            string querry = "select * from signup_tbl where name=@user AND pasword=@pass";
            SqlCommand cmd = new SqlCommand(querry,con);
            con.Open();
            cmd.Parameters.AddWithValue("@user",textBox1.Text);
            cmd.Parameters.AddWithValue("@pass", textBox2.Text);
           SqlDataReader dr=cmd.ExecuteReader();
            if (dr.HasRows==true) {
                MessageBox.Show("Login Successfully!!","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
                this.Hide();
                username = textBox1.Text;
                password = textBox2.Text;
                Mart MainForm = new Mart();
                MainForm.Show();
                
            } else {
                MessageBox.Show("Login Failed!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            con.Close();
        }

        private void showPw_CheckedChanged(object sender, EventArgs e)
        {
            bool check=showPw.Checked;
            if (check == true)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else {
                textBox2.UseSystemPasswordChar = true;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
            RegistrationForm rf=new RegistrationForm();
            this.Hide();
            rf.ShowDialog();

        }
    }
}
