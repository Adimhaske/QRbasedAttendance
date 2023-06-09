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
using System.Text.RegularExpressions;

namespace QR_based_attendance
{
    public partial class Student_Details : Form
    {
        string emailpattern = "^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$";
        public Student_Details()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            QRCoder.QRCodeGenerator QG = new QRCoder.QRCodeGenerator();
            var MyData = QG.CreateQrCode(id_text.Text,QRCoder.QRCodeGenerator.ECCLevel.H);
        
            
            var code = new QRCoder.QRCode(MyData);
            
            pictureBox1.Image = code.GetGraphic(100);
            SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-12FJN640\SQLEXPRESS;Initial Catalog=user_info;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into StudentDetails(ID,SName,Email,DOB,Class,Phone,Password) values(@1,@2,@3,@4,@5,@6,@7)", con);
            cmd.Parameters.AddWithValue("@1", id_text.Text);
            cmd.Parameters.AddWithValue("@2", textBox2.Text);
            cmd.Parameters.AddWithValue("@3", textBox3.Text);
            cmd.Parameters.AddWithValue("@4", dateTimePicker1.Text);

            cmd.Parameters.AddWithValue("@5", comboBox1.Text);
            cmd.Parameters.AddWithValue("@6", textBox6.Text);
            cmd.Parameters.AddWithValue("@7", textBox4.Text);


            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Record inserted successfully");
            string saveqr= @"C:\Users\ADITYA\OneDrive\Pictures\Documents\QRfiles";
            var dialog = new SaveFileDialog();
            dialog.InitialDirectory = saveqr;
            
            dialog.Filter = "PNG(*.png)|*.png";

            if (dialog.ShowDialog()==DialogResult.OK)
            {
                pictureBox1.Image.Save(dialog.FileName);
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (Regex.IsMatch(textBox3.Text, emailpattern) == false)
            {
                textBox3.Focus();
                MessageBox.Show("Please enter valid gmail");

            }
            else
            {

            }
        }

        private void id_text_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Error, ID cannot contain character");
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.TextLength == 10)
            {
                textBox6.ForeColor = Color.Black;
            }
            else
            {
                textBox6.ForeColor = Color.Red;


            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Error,Phone number cannot contain character");
            }
        }
    }
}
