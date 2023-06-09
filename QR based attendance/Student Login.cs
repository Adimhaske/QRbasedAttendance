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

namespace QR_based_attendance
{
    public partial class Student_Login : Form
    {
        public Student_Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBox1.Text);
            string pass = textBox2.Text;

            SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-12FJN640\SQLEXPRESS;Initial Catalog=user_info;Integrated Security=True");
            con.Open();
            SqlDataAdapter cmd = new SqlDataAdapter("select * from StudentDetails where ID='" + textBox1.Text + "'and Password='" + textBox2.Text + "'", con);

            DataTable dt = new DataTable();
            cmd.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                id = Convert.ToInt32(textBox1.Text);
                pass = textBox2.Text;
                Student_HomePage teacher_HomePage = new Student_HomePage();
                teacher_HomePage.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid Login", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            con.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            MainPage main = new MainPage();
            main.Show();
            this.Hide();
        }

        private void Student_Login_Load(object sender, EventArgs e)
        {

        }
    }
}
