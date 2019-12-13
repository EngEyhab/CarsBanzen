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
namespace BanzinaAPP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

     

        private void Form1_Load(object sender, EventArgs e)
        {
            //comboBox1.Items.Add("دودج رام");
            //comboBox1.Items.Add("فورد");
            //comboBox1.Items.Add("ربع نقل");
            comboBox3.Items.Add("بنــــزيـــن");

            comboBox3.Items.Add("ســـولار");


            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=ALLINONE-PC; Initial Catalog= BanzenDB; Integrated Security=SSPI";
            con.Open();
            try
            {

                SqlCommand command = new SqlCommand("SELECT cartype FROM cartype  ", con);
                SqlDataReader DR = command.ExecuteReader();
                while (DR.Read())
                {
                    comboBox1.Items.Add(DR.GetValue(0).ToString());
                }
                DR.Close();

            }
            catch
            {
                MessageBox.Show("");
            }



            try
            {

                SqlCommand command = new SqlCommand("SELECT name FROM ketaa  ", con);
                SqlDataReader DR = command.ExecuteReader();
                while (DR.Read())
                {
                    comboBox2.Items.Add(DR.GetValue(0).ToString());
                }
                DR.Close();

            }
            catch
            {
                MessageBox.Show("");
            }



            try
            {

                SqlCommand command = new SqlCommand("SELECT carNumber FROM carnumbers  ", con);
                SqlDataReader DR = command.ExecuteReader();
                while (DR.Read())
                {
                    comboBox4.Items.Add(DR.GetValue(0).ToString());
                }
                DR.Close();

            }
            catch
            {
                MessageBox.Show("");
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=  ALLINONE-PC ; Initial Catalog= BanzenDB; Integrated Security=SSPI";
                con.Open();


                string query = "insert into carcounter(date,cartype,carnumber,drivername,numberoflitres,readbefore,readafter,distance,ketaa,fuelType) values('" + dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "','" + comboBox1.SelectedItem.ToString() + "','" + comboBox4.SelectedItem.ToString() + "','" + textBox2.Text.ToString() + "','" + textBox3.Text.ToString() + "','" + textBox4.Text.ToString() + "','" + textBox5.Text.ToString() + "','" + textBox6.Text.ToString() + "','" + comboBox2.SelectedItem.ToString() + "','" + comboBox3.SelectedItem.ToString()+ "')";



                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();

                MessageBox.Show("تم حفظ البيانات بنجاح ");
                this.Refresh();
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                comboBox4.Text = null;


            }
            catch
            {
                MessageBox.Show("! من فضلك أدخل البيانات");
            }
        }

        private void textBox6_KeyUp(object sender, KeyEventArgs e)
        {
            int x = Int32.Parse(textBox5.Text.ToString());
            int y = Int32.Parse(textBox4.Text.ToString());

            int sub = x - y;
            textBox6.Text = sub.ToString();
        }

        private void textBox6_Click(object sender, EventArgs e)
        {
            int x = Int32.Parse(textBox5.Text.ToString());
            int y = Int32.Parse(textBox4.Text.ToString());

            int sub = x - y;
            textBox6.Text = sub.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           Reports rep = new Reports();
           rep.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //SqlConnection con = new SqlConnection();
            //con.ConnectionString = "Data Source=ALLINONE-PC; Initial Catalog= BanzenDB; Integrated Security=SSPI";
            //con.Open();
            //try
            //{

            //    SqlCommand command = new SqlCommand("SELECT date,cartype,carnumber,drivername,numberoflitres,readbefore,readafter,distance,ketaa,fuelType FROM carcounter where date=@x , carnumber=@y  ", con);
            //    command.Parameters.Add(new SqlParameter("@x", dateTimePicker1.Value.Date.ToString("yyyy-MM-dd")));
            //    command.Parameters.Add(new SqlParameter("@y", comboBox4.SelectedItem.ToString()));

            //    SqlDataReader DR = command.ExecuteReader();
            //    while (DR.Read())
            //    {
                 

            //      dateTimePicker1.Text = DR.GetValue(0).ToString();
            //      comboBox1.Items.Add(DR.GetValue(1).ToString());
            //      comboBox4.Items.Add(DR.GetValue(2).ToString());
            //      textBox2.Text = DR.GetValue(3).ToString();
            //      textBox3.Text = DR.GetValue(4).ToString();
            //      textBox4.Text = DR.GetValue(5).ToString();
            //      textBox5.Text = DR.GetValue(6).ToString();
            //      textBox6.Text = DR.GetValue(7).ToString();
            //      comboBox2.Items.Add(DR.GetValue(8).ToString());
            //      comboBox3.Items.Add(DR.GetValue(9).ToString());







            //    }
            //    DR.Close();

            //}
            //catch
            //{
            //    MessageBox.Show("");
            //}
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //////
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source= ALLINONE-PC; Initial Catalog= BanzenDB; Integrated Security=SSPI";
                con.Open();

                //   SqlCommand command = new SqlCommand("SELECT elrakmelaskary AS رقم_تحقيق_الشخصية, id AS الرقم_القومى, daraga AS الدرجة, name AS الاسم ,moahel AS المؤهل,phonenum AS رقم_التليفون, address AS العنوان , dateOfbirth AS تاريخ_الميلاد, dyana AS الديانة , marriedstatus AS الحالة_الاجتماعية, numOfzawgat AS عدد_الزوجات,numOfchilds AS عدد_الاولاد,numOfsonchilds AS عدد_الاولاد_البنين,numOfgirlchilds AS عدد_الاولاد_البنات,manzelphonenum AS رقم_المنزل, anotherphonenum AS رقم_محمول_أخر,nameOfclosest1 AS أقرب_الاقارب,addressOfclosest1 AS عنوان_أقرب_الأقارب,phonenumOfclosest1 AS رقم_أقرب_الاقارب,nameOfclosest2 AS أقرب_الاقارب,addressOfclosest2 AS عنوان_أقرب_الأقارب,phonenumOfclosest2 AS رقم_أقرب_الاقارب,nameOfclosest3 AS أقرب_الاقارب,addressOfclosest3 AS عنوان_أقرب_الأقارب,phonenumOfclosest3 AS رقم_أقرب_الاقارب, dateOfjoin AS تاريخ_الضم_علي_الطريق,dateOfeltagneed AS تاريخ_التجنيد, dateOfeltsreeh AS تاريخ_التسريح, manfazOrwehda AS  الوحدة_المنفذ,workbeforetagneed AS العمل_قبل_التجنيد,worknow AS العمل_القائم_به FROM gnoodDB WHERE name = @0", con);
                SqlCommand command = new SqlCommand("SELECT date,cartype,carnumber,drivername,numberoflitres,readbefore,readafter,distance,ketaa,fuelType FROM carcounter WHERE carnumber=@a And date=@b", con);
                command.Parameters.Add(new SqlParameter("@b", dateTimePicker1.Value.Date.ToString("yyyy-MM-dd")));
                command.Parameters.Add(new SqlParameter("@a", comboBox4.SelectedItem.ToString()));



                SqlDataReader DR1 = command.ExecuteReader();
                if (DR1.Read())
                {
                    dateTimePicker1.Text = DR1.GetValue(0).ToString();
                    comboBox1.Text = DR1.GetValue(1).ToString();
                    comboBox4.Text = DR1.GetValue(2).ToString();
                    textBox2.Text = DR1.GetValue(3).ToString();
                    textBox3.Text = DR1.GetValue(4).ToString();
                    textBox4.Text = DR1.GetValue(5).ToString();
                    textBox5.Text = DR1.GetValue(6).ToString();
                    textBox6.Text = DR1.GetValue(7).ToString();
                    comboBox2.Text = DR1.GetValue(8).ToString();
                    comboBox3.Text = DR1.GetValue(9).ToString();


                }

                else
                {
                    MessageBox.Show("من فضلك أختر نوع العربة و التاريخ");
                
                }



            }
            catch
            {
                MessageBox.Show("من فضلك أختر نوع العربة و التاريخ");
            }

         
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source= ALLINONE-PC; Initial Catalog=BanzenDB; Integrated Security=SSPI";
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE carcounter SET  cartype=@b,drivername=@d,numberoflitres=@e,readbefore=@f,readafter=@g,distance=@h,ketaa=@i,fuelType=@j  Where date =@uu and carnumber=@vv ";
                cmd.Parameters.AddWithValue("@uu", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@vv", comboBox4.SelectedItem.ToString());

                //cmd.Parameters.AddWithValue("@a", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("b", comboBox1.SelectedItem.ToString());
          //     cmd.Parameters.AddWithValue("@c", comboBox4.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("d", textBox2.Text.ToString());
                cmd.Parameters.AddWithValue("e", textBox3.Text.ToString());
                cmd.Parameters.AddWithValue("f", textBox4.Text.ToString());
                cmd.Parameters.AddWithValue("g", textBox5.Text.ToString());
                cmd.Parameters.AddWithValue("h", textBox6.Text.ToString());
                cmd.Parameters.AddWithValue("i", comboBox2.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("j", comboBox3.SelectedItem.ToString());

                cmd.ExecuteNonQuery();
                MessageBox.Show("تم تعديل البيانات بنجاح ");
                this.Hide();
                Form1 frm1 = new Form1();
                frm1.Show();
              
            }
            catch
            {
                MessageBox.Show("! من فضلك قم بتعديل  البيانات");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

      

    }
}
