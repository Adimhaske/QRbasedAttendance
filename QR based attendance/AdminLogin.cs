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
    public partial class AdminLogin : Form
    {
        public AdminLogin()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-12FJN640\SQLEXPRESS;Initial Catalog=user_info;Integrated Security=True");

        private void log_btn_Click(object sender, EventArgs e)
        {
            String username, user_password;

            username = user_text.Text;
            user_password = password_text.Text;

            try
            {
                String querry = "SELECT * FROM Login_db WHERE username = '" + user_text.Text + "' AND password = '" + password_text.Text + "'";
                SqlDataAdapter sda = new SqlDataAdapter(querry, conn);

                DataTable dtable = new DataTable();
                sda.Fill(dtable);

                if (dtable.Rows.Count > 0)
                {
                    username = user_text.Text;
                    user_password = password_text.Text;

                    //page that needed to be load 
                    AdminHome adminHome = new AdminHome();
                    adminHome.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid Details", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    user_text.Clear();
                    password_text.Clear();
                    user_text.Focus();
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }
            finally
            {
                conn.Close();
            }



        }

        private void clear_btn_Click(object sender, EventArgs e)
        {
            user_text.Clear();
            password_text.Clear();
            user_text.Focus();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            MainPage main = new MainPage();
            main.Show();
            this.Hide();
        }
    }
    }

