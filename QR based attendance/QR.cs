using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;
using ZXing.Aztec;
using System.Data.SqlClient;
using System.IO;

namespace QR_based_attendance
{
    public partial class QR : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-12FJN640\SQLEXPRESS;Initial Catalog=user_info;Integrated Security=True");
        public QR()
        {
            InitializeComponent();
        }
        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice videoCaptureDevice;

       

        private void button1_Click(object sender, EventArgs e)
        {
            videoCaptureDevice= new VideoCaptureDevice(filterInfoCollection[comboBox1.SelectedIndex].MonikerString);
            videoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
            videoCaptureDevice.Start();
        }

        private void VideoCaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
            BarcodeReader reader = new BarcodeReader();
            var result = reader.Decode(bitmap);
            if(result!=null)
            {
                textBox1.Invoke(new MethodInvoker(delegate ()
                {
                    con.Open();
                    textBox1.Text = result.ToString();
                    SqlCommand cmd = new SqlCommand("select SName,Class,Phone from StudentDetails where ID='"+textBox1.Text+"'",con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    dr.Read();
                    if(dr.HasRows)
                    {
                        textBox2.Text = dr["SName"].ToString();
                        textBox3.Text = dr["Phone"].ToString();
                        textBox6.Text = dr["class"].ToString();
                    }
                    con.Close();
                 
                }));
               
            }
            pictureBox1.Image = bitmap;
        }

        private void QR_Load(object sender, EventArgs e)
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo device in filterInfoCollection)
                comboBox1.Items.Add(device.Name);
            comboBox1.SelectedIndex = 0;
            label9.Text = DateTime.Now.ToShortDateString();
            label10.Text = DateTime.Now.ToShortTimeString();
        }

        private void QR_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoCaptureDevice.IsRunning)
                videoCaptureDevice.Stop();
            
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmmd = new SqlCommand("select Date from Attendance where ID='"+textBox1.Text+"'",con);
            SqlDataAdapter sda = new SqlDataAdapter(cmmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            if(ds.Tables[0].Rows.Count>0)
            {
                MessageBox.Show("Student Id "+textBox1.Text+ " already present");
            }
            else
            { 
            SqlCommand cmd = new SqlCommand("insert into Attendance(ID,SName,Phone,class,Intime,Date) values(@1,@2,@3,@4,@5,@6)", con);
            cmd.Parameters.AddWithValue("@1", textBox1.Text);
            cmd.Parameters.AddWithValue("@2", textBox2.Text);
            cmd.Parameters.AddWithValue("@3", textBox3.Text);
            cmd.Parameters.AddWithValue("@4", textBox6.Text);
            cmd.Parameters.AddWithValue("@5", label10.Text);
            cmd.Parameters.AddWithValue("@6", label9.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Attendance Mark Successfully");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Teacher_HomePage teacher_Home = new Teacher_HomePage();
            teacher_Home.Show();
            this.Hide();
        }
    }
}
