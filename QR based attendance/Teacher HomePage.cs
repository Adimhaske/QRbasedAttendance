using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QR_based_attendance
{
    public partial class Teacher_HomePage : Form
    {
        public Teacher_HomePage()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            QR scanOR = new QR();
            scanOR.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Teacher_HomePage teacher_Home = new Teacher_HomePage();
            teacher_Home.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Teacher_Login main = new Teacher_Login();
            main.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
