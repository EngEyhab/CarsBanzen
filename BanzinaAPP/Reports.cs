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
using System.IO;
namespace BanzinaAPP
{
    public partial class Reports : Form
    {
        public Reports()
        {
            InitializeComponent();
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void Reports_Load(object sender, EventArgs e)
        {
            comboBox3.Items.Add("بنــــزيـــن");

            comboBox3.Items.Add("ســـولار");
            
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=ALLINONE-PC; Initial Catalog= BanzenDB; Integrated Security=SSPI";
            con.Open();
            try
            {

                SqlCommand command = new SqlCommand("SELECT name FROM RoadKetaat  ", con);
                SqlDataReader DR = command.ExecuteReader();
                while (DR.Read())
                {
                    comboBox2.Items.Add(DR.GetValue(0).ToString());
                }
                DR.Close();

            }
            catch
            {

            }
            try
            {

                SqlCommand command = new SqlCommand("SELECT DISTINCT carNumber FROM carnumbers  ", con);
                SqlDataReader DR = command.ExecuteReader();
                while (DR.Read())
                {
                    comboBox1.Items.Add(DR.GetValue(0).ToString());
                }
                DR.Close();

            }
            catch
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string total = "0";
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=ALLINONE-PC; Initial Catalog= BanzenDB; Integrated Security=SSPI";
                con.Open();

                SqlCommand command = new SqlCommand("SELECT SUM(numberoflitres)  FROM carcounter WHERE date =@ And carnumber=@x And fuelType=@m", con);
                command.Parameters.Add(new SqlParameter("@", dateTimePicker1.Value.Date.ToString("yyyy-MM-dd")));
                command.Parameters.Add(new SqlParameter("@x", comboBox1.SelectedItem.ToString()));
                command.Parameters.Add(new SqlParameter("@m", comboBox3.SelectedItem.ToString()));


                SqlDataReader DR1 = command.ExecuteReader();
                if (DR1.Read())
                {
                    total = DR1.GetValue(0).ToString();
                    int mytotal = Int32.Parse(total);

                    string x = mytotal.ToString("#,##0");
                    dataGridView1.Hide();
                    //button11.Hide();
                    button9.Hide();

                    MessageBox.Show(" اجمالي الكمية المنصرفة في هذا اليوم هي  " + x + " لتر");
                    //  this.Close();
                }
                // Reports rep = new Reports();
                //  rep.Show();

            }

            catch (Exception ec)
            {

                MessageBox.Show("عفوا قم بإختيار رقم العربة و التاريخ و نوع الوقود ");
            }


            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=ALLINONE-PC; Initial Catalog= BanzenDB; Integrated Security=SSPI";
                con.Open();

                SqlCommand command = new SqlCommand("SELECT date   AS التاريخ ,cartype AS  'نوع العربة',carnumber AS ' رقم العربة',drivername AS ' اسم السائق  ' , numberoflitres As  ' الكمية المنصرفة ' FROM carcounter WHERE carnumber = @z And date=@d And fuelType=@m", con);



                command.Parameters.Add(new SqlParameter("@z", comboBox1.SelectedItem.ToString()));

                command.Parameters.Add(new SqlParameter("@d", dateTimePicker1.Value.Date.ToString("yyyy-MM-dd")));

                command.Parameters.Add(new SqlParameter("@m", comboBox3.SelectedItem.ToString()));


                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = command;

                DataTable dt = new DataTable();

                da.Fill(dt);

                BindingSource bsource = new BindingSource();
                bsource.DataSource = dt;
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("mohammad art 1", 10.00F, FontStyle.Bold);

                dataGridView1.DefaultCellStyle.Font = new Font("mohammad art 1", 10.00F, FontStyle.Bold);

                dataGridView1.DataSource = bsource;
                dataGridView1.Show();
             //   button11.Show();
                button9.Show();

            }

            catch (Exception ec)
            {



            }
           // Reports rep = new Reports();
            //rep.Show();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string total = "عفوا تأكد من التاريخ الذي أدخلته";
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=ALLINONE-PC; Initial Catalog= BanzenDB; Integrated Security=SSPI";
                con.Open();

                SqlCommand command = new SqlCommand("SELECT SUM(numberoflitres)  FROM carcounter WHERE date >= @m AND date<= @y  And carnumber=@x And fuelType=@d", con);

                command.Parameters.Add(new SqlParameter("@m", dateTimePicker2.Value.Date.ToString("yyyy-MM-dd")));
                command.Parameters.Add(new SqlParameter("@y", dateTimePicker3.Value.Date.ToString("yyyy-MM-dd")));
                command.Parameters.Add(new SqlParameter("@x", comboBox1.SelectedItem.ToString()));
                command.Parameters.Add(new SqlParameter("@d", comboBox3.SelectedItem.ToString()));


                SqlDataReader DR1 = command.ExecuteReader();
                if (DR1.Read())
                {

                    total = DR1.GetValue(0).ToString();
                    int mytotal = Int32.Parse(total);

                    string x = mytotal.ToString("#,##0");

                    dataGridView1.Hide();
                   // button11.Hide();
                    button9.Hide();

                    MessageBox.Show(" اجمالي الكمية المنصرفة في هذه الفترة هي  " + x + " لتر");
                    // MessageBox.Show("اجمالي الايراد للطريق هو  " + total);
                //    this.Close();
                }

             //   Reports rep = new Reports();
              //  rep.Show();

            }


            catch (Exception ec)
            {

                MessageBox.Show("عفوا تأكد من اختيار فترة صحيحة مُسجلة او رقم العربة او نوع الوقود ");

            }


            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=ALLINONE-PC; Initial Catalog= BanzenDB; Integrated Security=SSPI";
                con.Open();


                SqlCommand command = new SqlCommand("SELECT date   AS التاريخ ,cartype AS  ' نوع العربة ',carnumber AS ' رقم العربة' ,drivername AS ' اسم السائق '  , numberoflitres As' الكمية المنصرفة'  FROM carcounter WHERE carnumber = @z And date >= @m AND date<= @y And fuelType=@d", con);



                command.Parameters.Add(new SqlParameter("@z", comboBox1.SelectedItem.ToString()));

                command.Parameters.Add(new SqlParameter("@m", dateTimePicker2.Value.Date.ToString("yyyy-MM-dd")));
                command.Parameters.Add(new SqlParameter("@y", dateTimePicker3.Value.Date.ToString("yyyy-MM-dd")));
                command.Parameters.Add(new SqlParameter("@d", comboBox3.SelectedItem.ToString()));


                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = command;

                DataTable dt = new DataTable();

                da.Fill(dt);

                BindingSource bsource = new BindingSource();
                bsource.DataSource = dt;
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("mohammad art 1", 10.00F, FontStyle.Bold);

                dataGridView1.DefaultCellStyle.Font = new Font("mohammad art 1", 10.00F, FontStyle.Bold);

                dataGridView1.DataSource = bsource;
                dataGridView1.Show();
           //     button11.Show();
                button9.Show();

            }

            catch (Exception ec)
            {



            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string total = "0";
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=ALLINONE-PC; Initial Catalog= BanzenDB; Integrated Security=SSPI";
                con.Open();

                SqlCommand command = new SqlCommand("SELECT SUM(distance)  FROM carcounter WHERE date =@ And carnumber=@x ", con);
                command.Parameters.Add(new SqlParameter("@", dateTimePicker4.Value.Date.ToString("yyyy-MM-dd")));
                command.Parameters.Add(new SqlParameter("@x", comboBox1.SelectedItem.ToString()));

                SqlDataReader DR1 = command.ExecuteReader();
                if (DR1.Read())
                {
                    total = DR1.GetValue(0).ToString();
                    int mytotal = Int32.Parse(total);

                    string x = mytotal.ToString("#,##0");
                    dataGridView1.Hide();
                    //button11.Hide();
                    button9.Hide();

                    MessageBox.Show(" اجمالي المسافة المقطوعة  في هذا اليوم هي  " + x + " كم ");
                    //  this.Close();
                }
                // Reports rep = new Reports();
                //  rep.Show();

            }

            catch (Exception ec)
            {

                MessageBox.Show(" عفوا قم بإختيار رقم العربة و التاريخ و نوع الوقود ");
            }
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=ALLINONE-PC; Initial Catalog= BanzenDB; Integrated Security=SSPI";
                con.Open();


                SqlCommand command = new SqlCommand("SELECT date   AS التاريخ ,cartype AS ' نوع العربة',carnumber AS' رقم العربة',drivername AS' اسم السائق '  , readbefore AS' قراءة العداد قبل ' , readafter AS 'قراءة العداد بعد', distance AS 'المسافة المقطوعة 'FROM carcounter WHERE carnumber = @z And date=@d ", con);



                command.Parameters.Add(new SqlParameter("@z", comboBox1.SelectedItem.ToString()));

                command.Parameters.Add(new SqlParameter("@d", dateTimePicker4.Value.Date.ToString("yyyy-MM-dd")));



                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = command;

                DataTable dt = new DataTable();

                da.Fill(dt);

                BindingSource bsource = new BindingSource();
                bsource.DataSource = dt;
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("mohammad art 1", 8.00F, FontStyle.Bold);

                dataGridView1.DefaultCellStyle.Font = new Font("mohammad art 1", 8.00F, FontStyle.Bold);

                dataGridView1.DataSource = bsource;
                dataGridView1.Show();
            //    button11.Show();
                button9.Show();


            }

            catch (Exception ec)
            {

               

            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            string total = "عفوا تأكد من التاريخ الذي أدخلته";
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=ALLINONE-PC; Initial Catalog= BanzenDB; Integrated Security=SSPI";
                con.Open();

                SqlCommand command = new SqlCommand("SELECT SUM(distance)  FROM carcounter WHERE date >= @m AND date<= @y  And carnumber=@x  ", con);

                command.Parameters.Add(new SqlParameter("@m", dateTimePicker5.Value.Date.ToString("yyyy-MM-dd")));
                command.Parameters.Add(new SqlParameter("@y", dateTimePicker6.Value.Date.ToString("yyyy-MM-dd")));
                command.Parameters.Add(new SqlParameter("@x", comboBox1.SelectedItem.ToString()));


                SqlDataReader DR1 = command.ExecuteReader();
                if (DR1.Read())
                {

                    total = DR1.GetValue(0).ToString();
                    int mytotal = Int32.Parse(total);

                    string x = mytotal.ToString("#,##0");

                    dataGridView1.Hide();
                 //   button11.Hide();
                    button9.Hide();


                    MessageBox.Show(" اجمالي المسافة المقطوعة في هذه الفترة هي  " + x + " كم ");
                    // MessageBox.Show("اجمالي الايراد للطريق هو  " + total);
                    //    this.Close();
                }

                //   Reports rep = new Reports();
                //  rep.Show();

            }


            catch (Exception ec)
            {

                MessageBox.Show("عفوا تأكد من اختيار فترة صحيحة مُسجلة او رقم العربة ");

            }

            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=ALLINONE-PC; Initial Catalog= BanzenDB; Integrated Security=SSPI";
                con.Open();


                SqlCommand command = new SqlCommand("SELECT date   AS التاريخ ,cartype AS ' نوع العربة',carnumber AS 'رقم العربة',drivername AS 'اسم السائق ' , readbefore AS 'قراءة العداد قبل ' , readafter AS 'قراءة العداد بعد', distance AS' المسافة المقطوعة' FROM carcounter WHERE carnumber = @z AND date >= @x AND date<= @y  ORDER BY date ", con);



                command.Parameters.Add(new SqlParameter("@z", comboBox1.SelectedItem.ToString()));

                //      command.Parameters.Add(new SqlParameter("@x", dateTimePicker7.Value.Date.ToString("yyyy-MM-dd")));

                command.Parameters.Add(new SqlParameter("@x", dateTimePicker5.Value.Date.ToString("yyyy-MM-dd")));
                command.Parameters.Add(new SqlParameter("@y", dateTimePicker6.Value.Date.ToString("yyyy-MM-dd")));

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = command;

                DataTable dt = new DataTable();

                da.Fill(dt);

                BindingSource bsource = new BindingSource();
                bsource.DataSource = dt;
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("mohammad art 1", 8.00F, FontStyle.Bold);

                dataGridView1.DefaultCellStyle.Font = new Font("mohammad art 1", 8.00F, FontStyle.Bold);

                dataGridView1.DataSource = bsource;
                dataGridView1.Show();
           //     button11.Show();
                button9.Show();


            }

            catch (Exception ec)
            {

                //   MessageBox.Show("عفوا لابد من اختيار منفذ رئيسي");


            }

        }
        Bitmap bmp; 

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                int height = dataGridView1.Height;
                dataGridView1.Height = dataGridView1.RowCount * dataGridView1.RowTemplate.Height * 10;
                bmp = new Bitmap(dataGridView1.Width, dataGridView1.Height);
                dataGridView1.DrawToBitmap(bmp, new Rectangle(0, 0, dataGridView1.Width, dataGridView1.Height));
                dataGridView1.Height = height;
                //     printPreviewDialog1.ShowDialog(); 
                PrintDialog printdialog = new PrintDialog();
                printdialog.AllowSomePages = true;
                printdialog.Document = printDocument1;
                printdialog.UseEXDialog = true;

                if (DialogResult.OK == printdialog.ShowDialog())
                {
                    printDocument1.DocumentName = "test";

                    printDocument1.Print();
                }
                PrintPreviewDialog objdialog = new PrintPreviewDialog();

                objdialog.Document = printDocument1;
                objdialog.ShowDialog();
            }
            catch
            {
                MessageBox.Show("عفوا ليس هناك شئ يمكن طباعته");
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //  e.Graphics.DrawString("cccc",new Font(dataGridView1.Font.FontFamily('Arial')));


            Graphics graphics = e.Graphics;
            Font font = new Font("mohammad bold art 1", 2);
            int startx = 230;
            int starty = 5;
            int offset = 2;
            //   graphics.DrawString("تقرير معدل التدفق عن الفترة من 4/5/2016 الي 5/5/2015 ", new Font("mohammad bold art 1", 16), new SolidBrush(Color.Black), startx, starty + offset);

         //   graphics.DrawString(textBox1.Text.ToString(), new Font("mohammad bold art 1", 16), new SolidBrush(Color.Black), startx, starty + offset);

            //   e.HasMorePages = true;


            e.Graphics.DrawImage(bmp, 45, 45);
        }
        private void Toexcel(DataGridView DGV, string filename)
        {

            string stout = "";
            string sheader = "";
            for (int j = 0; j < DGV.Columns.Count; j++)
            {
                sheader = sheader.ToString() + Convert.ToString(DGV.Columns[j].HeaderText) + "\t";

            }
            stout += sheader + "\r\n";
            for (int i = 0; i < DGV.RowCount; i++)
            {
                string strline = "";
                for (int j = 0; j < DGV.Rows[i].Cells.Count; j++)
                {
                    strline = strline.ToString() + Convert.ToString(DGV.Rows[i].Cells[j].Value) + "\t";

                }
                stout += strline + "\r\n";

            }

            Encoding utf16 = Encoding.GetEncoding(1256);
            byte[] output = utf16.GetBytes(stout);
            FileStream fs = new FileStream(filename, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(output, 0, output.Length);
            bw.Flush();
            bw.Close();
            fs.Close();
        }
        private void button9_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Document{*.xls}|*.xls";
            sfd.FileName = "Report.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                
                Toexcel(dataGridView1, sfd.FileName);

            }
        }

        private void dateTimePicker7_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker8_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=ALLINONE-PC; Initial Catalog= BanzenDB; Integrated Security=SSPI";
            con.Open();

            if (comboBox2.SelectedItem.ToString() == "دهشور")
            {

               SqlCommand command = new SqlCommand("SELECT date   AS التاريخ ,cartype AS  نوع_العربة,carnumber AS رقم_العربة, readbefore AS قراءة_العداد_قبل  , readafter AS قراءة_العداد_بعد, distance AS المسافة_المقطوعة ,numberoflitres AS الكمية_المنصرفة,fuelType AS نوع_الوقود, ketaa  القطاع FROM carcounter WHERE ketaa like '%دهشور%' AND date >= @m AND date<= @y  ORDER BY date ", con);


                command.Parameters.Add(new SqlParameter("@m", dateTimePicker8.Value.Date.ToString("yyyy-MM-dd")));
                command.Parameters.Add(new SqlParameter("@y", dateTimePicker7.Value.Date.ToString("yyyy-MM-dd")));



                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = command;

                DataTable dt = new DataTable();

                da.Fill(dt);

                BindingSource bsource = new BindingSource();
                bsource.DataSource = dt;
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("mohammad art 1", 8.00F, FontStyle.Bold);

                dataGridView1.DefaultCellStyle.Font = new Font("mohammad art 1", 8.00F, FontStyle.Bold);

                dataGridView1.DataSource = bsource;
                dataGridView1.Show();
                //button11.Show();
                button9.Show();
            }
            else if (comboBox2.SelectedItem.ToString() == "المنيا")
            {

                SqlCommand command = new SqlCommand("SELECT date   AS التاريخ ,cartype AS  نوع_العربة,carnumber AS رقم_العربة, readbefore AS قراءة_العداد_قبل  , readafter AS قراءة_العداد_بعد, distance AS المسافة_المقطوعة ,numberoflitres AS الكمية_المنصرفة, fuelType AS نوع_الوقود,ketaa  القطاع FROM carcounter WHERE ketaa like '%المنيا%' AND date >= @m AND date<= @y  ORDER BY date ", con);
              //  SqlCommand command = new SqlCommand("SELECT date   AS التاريخ ,cartype AS  نوع_العربة,carnumber AS رقم_العربة, readbefore AS قراءة_العداد_قبل  , readafter AS قراءة_العداد_بعد, distance AS المسافة_المقطوعة ,numberoflitres AS الكمية_المنصرفة,fuelType AS نوع_الوقود, ketaa  القطاع FROM carcounter WHERE ketaa like '%دهشور%' AND date >= @m AND date<= @y  ORDER BY date ", con);


                command.Parameters.Add(new SqlParameter("@m", dateTimePicker8.Value.Date.ToString("yyyy-MM-dd")));
                command.Parameters.Add(new SqlParameter("@y", dateTimePicker7.Value.Date.ToString("yyyy-MM-dd")));


                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = command;

                DataTable dt = new DataTable();

                da.Fill(dt);

                BindingSource bsource = new BindingSource();
                bsource.DataSource = dt;
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("mohammad art 1", 8.00F, FontStyle.Bold);

                dataGridView1.DefaultCellStyle.Font = new Font("mohammad art 1", 8.00F, FontStyle.Bold);

                dataGridView1.DataSource = bsource;
                dataGridView1.Show();
                //button11.Show();
                button9.Show();

            }
            else if (comboBox2.SelectedItem.ToString() == "أسيوط")
            {

              // SqlCommand command = new SqlCommand("SELECT date   AS التاريخ ,cartype AS  نوع_العربة,carnumber AS رقم_العربة,drivername AS اسم_السائق  , readbefore AS قراءة_العداد_قبل  , readafter AS قراءة_العداد_بعد, distance AS المسافة_المقطوعة , ketaa  القطاع FROM carcounter WHERE ketaa like '%أسيوط%' AND date >= @m AND date<= @y   ORDER BY date ", con);
               // SqlCommand command = new SqlCommand("SELECT date   AS التاريخ ,cartype AS  نوع_العربة,carnumber AS رقم_العربة, readbefore AS قراءة_العداد_قبل  , readafter AS قراءة_العداد_بعد, distance AS المسافة_المقطوعة ,numberoflitres AS الكمية_المنصرفة,fuelType AS نوع_الوقود, ketaa  القطاع FROM carcounter WHERE ketaa like '%دهشور%' AND date >= @m AND date<= @y  ORDER BY date ", con);

                SqlCommand command = new SqlCommand("SELECT date   AS التاريخ ,cartype AS  نوع_العربة,carnumber AS رقم_العربة, readbefore AS قراءة_العداد_قبل  , readafter AS قراءة_العداد_بعد, distance AS المسافة_المقطوعة ,numberoflitres AS الكمية_المنصرفة, fuelType AS نوع_الوقود,ketaa  القطاع FROM carcounter WHERE ketaa like '%أسيوط%' AND date >= @m AND date<= @y  ORDER BY date ", con);

                command.Parameters.Add(new SqlParameter("@m", dateTimePicker8.Value.Date.ToString("yyyy-MM-dd")));
                command.Parameters.Add(new SqlParameter("@y", dateTimePicker7.Value.Date.ToString("yyyy-MM-dd")));


                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = command;

                DataTable dt = new DataTable();

                da.Fill(dt);

                BindingSource bsource = new BindingSource();
                bsource.DataSource = dt;
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("mohammad art 1", 8.00F, FontStyle.Bold);

                dataGridView1.DefaultCellStyle.Font = new Font("mohammad art 1", 8.00F, FontStyle.Bold);

                dataGridView1.DataSource = bsource;
                dataGridView1.Show();
                //button11.Show();
                button9.Show();

            }
            else if (comboBox2.SelectedItem.ToString() == "قنا")
            {
              //  SqlCommand command = new SqlCommand("SELECT date   AS التاريخ ,cartype AS  نوع_العربة,carnumber AS رقم_العربة,drivername AS اسم_السائق  , readbefore AS قراءة_العداد_قبل  , readafter AS قراءة_العداد_بعد, distance AS المسافة_المقطوعة , ketaa  القطاع FROM carcounter WHERE ketaa like '%قنا%' AND date >= @m AND date<= @y  ORDER BY date ", con);

                SqlCommand command = new SqlCommand("SELECT date   AS التاريخ ,cartype AS  نوع_العربة,carnumber AS رقم_العربة, readbefore AS قراءة_العداد_قبل  , readafter AS قراءة_العداد_بعد, distance AS المسافة_المقطوعة ,numberoflitres AS الكمية_المنصرفة, fuelType AS نوع_الوقود,ketaa  القطاع FROM carcounter WHERE ketaa like '%قنا%' AND date >= @m AND date<= @y  ORDER BY date ", con);

                command.Parameters.Add(new SqlParameter("@m", dateTimePicker8.Value.Date.ToString("yyyy-MM-dd")));
                command.Parameters.Add(new SqlParameter("@y", dateTimePicker7.Value.Date.ToString("yyyy-MM-dd")));


                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = command;

                DataTable dt = new DataTable();

                da.Fill(dt);

                BindingSource bsource = new BindingSource();
                bsource.DataSource = dt;
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("mohammad art 1", 8.00F, FontStyle.Bold);

                dataGridView1.DefaultCellStyle.Font = new Font("mohammad art 1", 8.00F, FontStyle.Bold);

                dataGridView1.DataSource = bsource;
                dataGridView1.Show();
                //button11.Show();
                button9.Show();


            }
            else if (comboBox2.SelectedItem.ToString() == "أسوان")
            {

               // SqlCommand command = new SqlCommand("SELECT date   AS التاريخ ,cartype AS  نوع_العربة,carnumber AS رقم_العربة,drivername AS اسم_السائق  , readbefore AS قراءة_العداد_قبل  , readafter AS قراءة_العداد_بعد, distance AS المسافة_المقطوعة , ketaa  القطاع FROM carcounter WHERE ketaa like '%أسوان%' AND date >= @m AND date<= @y   ORDER BY date ", con);
                SqlCommand command = new SqlCommand("SELECT date   AS التاريخ ,cartype AS  نوع_العربة,carnumber AS رقم_العربة, readbefore AS قراءة_العداد_قبل  , readafter AS قراءة_العداد_بعد, distance AS المسافة_المقطوعة ,numberoflitres AS الكمية_المنصرفة,fuelType AS نوع_الوقود, ketaa  القطاع FROM carcounter WHERE ketaa in('أدفو','السباعية','صحاري') AND date >= @m AND date<= @y  ORDER BY date ", con);


                command.Parameters.Add(new SqlParameter("@m", dateTimePicker8.Value.Date.ToString("yyyy-MM-dd")));
                command.Parameters.Add(new SqlParameter("@y", dateTimePicker7.Value.Date.ToString("yyyy-MM-dd")));


                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = command;

                DataTable dt = new DataTable();

                da.Fill(dt);

                BindingSource bsource = new BindingSource();
                bsource.DataSource = dt;
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("mohammad art 1", 8.00F, FontStyle.Bold);

                dataGridView1.DefaultCellStyle.Font = new Font("mohammad art 1", 8.00F, FontStyle.Bold);

                dataGridView1.DataSource = bsource;
                dataGridView1.Show();
                //button11.Show();
                button9.Show();

            }
            else
            {
                MessageBox.Show("عفوا اختيار القطاع خاطئ");
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker9_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
         
        private void button5_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=ALLINONE-PC; Initial Catalog= BanzenDB; Integrated Security=SSPI";
            con.Open();
            
            if (comboBox2.SelectedItem.ToString() == "دهشور")
            {

               // SqlCommand command = new SqlCommand("SELECT date   AS التاريخ ,cartype AS ' نوع العربة',carnumber AS' رقم العربة', readbefore AS 'قراءة العداد قبل ' , readafter AS' قراءة العداد بعد', distance AS 'المسافة المقطوعة ',numberoflitres AS' الكمية المنصرفة',fuelType AS' نوع الوقود', ketaa  القطاع FROM carcounter WHERE ketaa like '%دهشور%' AND date = @x   ORDER BY date ", con);
                SqlCommand command = new SqlCommand("SELECT date   AS التاريخ ,cartype AS  نوع_العربة,carnumber AS رقم_العربة, readbefore AS قراءة_العداد_قبل  , readafter AS قراءة_العداد_بعد, distance AS المسافة_المقطوعة ,numberoflitres AS الكمية_المنصرفة,fuelType AS نوع_الوقود, ketaa  القطاع FROM carcounter WHERE ketaa like '%دهشور%' AND date = @x   ORDER BY date ", con);


                command.Parameters.Add(new SqlParameter("@x", dateTimePicker9.Value.Date.ToString("yyyy-MM-dd")));


                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = command;

                DataTable dt = new DataTable();

                da.Fill(dt);

                BindingSource bsource = new BindingSource();
                bsource.DataSource = dt;
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("mohammad art 1", 8.00F, FontStyle.Bold);

                dataGridView1.DefaultCellStyle.Font = new Font("mohammad art 1", 8.00F, FontStyle.Bold);

                dataGridView1.DataSource = bsource;
                dataGridView1.Show();
                //button11.Show();
                button9.Show();
            }
            else if (comboBox2.SelectedItem.ToString() == "المنيا")
            {

              //  SqlCommand command = new SqlCommand("SELECT date   AS التاريخ ,cartype AS  'نوع العربة',carnumber AS' رقم العربة',drivername AS' اسم السائق ' , readbefore AS' قراءة العداد قبل ' , readafter AS' قراءة العداد بعد', distance AS' المسافة المقطوعة ', ketaa  القطاع FROM carcounter WHERE ketaa like '%المنيا%' AND date = @x   ORDER BY date ", con);

                SqlCommand command = new SqlCommand("SELECT date   AS التاريخ ,cartype AS  نوع_العربة,carnumber AS رقم_العربة, readbefore AS قراءة_العداد_قبل  , readafter AS قراءة_العداد_بعد, distance AS المسافة_المقطوعة ,numberoflitres AS الكمية_المنصرفة,fuelType AS نوع_الوقود, ketaa  القطاع FROM carcounter WHERE ketaa like '%المنيا%' AND date = @x   ORDER BY date ", con);

                command.Parameters.Add(new SqlParameter("@x", dateTimePicker9.Value.Date.ToString("yyyy-MM-dd")));


                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = command;

                DataTable dt = new DataTable();

                da.Fill(dt);

                BindingSource bsource = new BindingSource();
                bsource.DataSource = dt;
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("mohammad art 1", 8.00F, FontStyle.Bold);

                dataGridView1.DefaultCellStyle.Font = new Font("mohammad art 1", 8.00F, FontStyle.Bold);

                dataGridView1.DataSource = bsource;
                dataGridView1.Show();
                //button11.Show();
                button9.Show();

            }
            else if (comboBox2.SelectedItem.ToString() == "أسيوط")
            {

             //   SqlCommand command = new SqlCommand("SELECT date   AS التاريخ ,cartype AS  'نوع العربة',carnumber AS' رقم العربة',drivername AS' اسم السائق ' , readbefore AS' قراءة العداد قبل ' , readafter AS' قراءة العداد بعد', distance AS' المسافة المقطوعة ', ketaa  القطاع FROM carcounter WHERE ketaa like '%أسيوط%' AND date = @x   ORDER BY date ", con);

                SqlCommand command = new SqlCommand("SELECT date   AS التاريخ ,cartype AS  نوع_العربة,carnumber AS رقم_العربة, readbefore AS قراءة_العداد_قبل  , readafter AS قراءة_العداد_بعد, distance AS المسافة_المقطوعة ,numberoflitres AS الكمية_المنصرفة,fuelType AS نوع_الوقود, ketaa  القطاع FROM carcounter WHERE ketaa like '%أسيوط%' AND date = @x   ORDER BY date ", con);

                command.Parameters.Add(new SqlParameter("@x", dateTimePicker9.Value.Date.ToString("yyyy-MM-dd")));


                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = command;

                DataTable dt = new DataTable();

                da.Fill(dt);

                BindingSource bsource = new BindingSource();
                bsource.DataSource = dt;
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("mohammad art 1", 8.00F, FontStyle.Bold);

                dataGridView1.DefaultCellStyle.Font = new Font("mohammad art 1", 8.00F, FontStyle.Bold);

                dataGridView1.DataSource = bsource;
                dataGridView1.Show();
                //button11.Show();
                button9.Show();

            }
            else if (comboBox2.SelectedItem.ToString() == "قنا")
            {
               // SqlCommand command = new SqlCommand("SELECT date   AS التاريخ ,cartype AS  'نوع العربة',carnumber AS' رقم العربة',drivername AS 'اسم السائق ' , readbefore AS 'قراءة العداد قبل ' , readafter AS' قراءة العداد بعد', distance AS' المسافة المقطوعة ', ketaa  القطاع FROM carcounter WHERE ketaa like '%قنا%' AND date = @x   ORDER BY date ", con);

                SqlCommand command = new SqlCommand("SELECT date   AS التاريخ ,cartype AS  نوع_العربة,carnumber AS رقم_العربة, readbefore AS قراءة_العداد_قبل  , readafter AS قراءة_العداد_بعد, distance AS المسافة_المقطوعة ,numberoflitres AS الكمية_المنصرفة,fuelType AS نوع_الوقود, ketaa  القطاع FROM carcounter WHERE ketaa like '%قنا%' AND date = @x   ORDER BY date ", con);

                command.Parameters.Add(new SqlParameter("@x", dateTimePicker9.Value.Date.ToString("yyyy-MM-dd")));


                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = command;

                DataTable dt = new DataTable();

                da.Fill(dt);

                BindingSource bsource = new BindingSource();
                bsource.DataSource = dt;
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("mohammad art 1", 8.00F, FontStyle.Bold);

                dataGridView1.DefaultCellStyle.Font = new Font("mohammad art 1", 8.00F, FontStyle.Bold);

                dataGridView1.DataSource = bsource;
                dataGridView1.Show();
                //button11.Show();
                button9.Show();


            }
            else if (comboBox2.SelectedItem.ToString() == "أسوان")
            {

              //  SqlCommand command = new SqlCommand("SELECT date   AS التاريخ ,cartype AS  نوع_العربة,carnumber AS رقم_العربة,drivername AS اسم_السائق  , readbefore AS قراءة_العداد_قبل  , readafter AS قراءة_العداد_بعد, distance AS المسافة_المقطوعة , ketaa  القطاع FROM carcounter WHERE ketaa like '%أسوان%' AND date = @x   ORDER BY date ", con);
                SqlCommand command = new SqlCommand("SELECT date   AS التاريخ ,cartype AS  نوع_العربة,carnumber AS رقم_العربة, readbefore AS قراءة_العداد_قبل  , readafter AS قراءة_العداد_بعد, distance AS المسافة_المقطوعة ,numberoflitres AS الكمية_المنصرفة,fuelType AS نوع_الوقود, ketaa  القطاع FROM carcounter WHERE ketaa in('أدفو','السباعية','صحاري') AND date = @x   ORDER BY date ", con);
              

                command.Parameters.Add(new SqlParameter("@x", dateTimePicker9.Value.Date.ToString("yyyy-MM-dd")));


                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = command;

                DataTable dt = new DataTable();

                da.Fill(dt);

                BindingSource bsource = new BindingSource();
                bsource.DataSource = dt;
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("mohammad art 1", 8.00F, FontStyle.Bold);

                dataGridView1.DefaultCellStyle.Font = new Font("mohammad art 1", 8.00F, FontStyle.Bold);

                dataGridView1.DataSource = bsource;
                dataGridView1.Show();
                //button11.Show();
                button9.Show();

            }
            else
            {
                MessageBox.Show("عفوا اختيار القطاع خاطئ");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=ALLINONE-PC; Initial Catalog= BanzenDB; Integrated Security=SSPI";
                con.Open();

             
              //  string[] arr = { "ص ص ف / 357", "ف ص ف / 694", "ج ط ف / 671", "ج ه ف / 572", "أ م ر / 4358", "أ م ر / 4361", "ف و ص / 281", "ف و ص / 261", "ج ي ب / 385", "ف و ص /497", "ي ل ب / 629", "و ف م / 716", "ل ف م / 657", "ن ف م / 924", "ب د ر / 492", "ي ي هـ / 284", "ف د ر / 654" };
           
            //    for (int i=0;i<arr[i].Length-1;i++)
              //  {
                SqlCommand command = new SqlCommand("SELECT carnumber AS 'رقم العربة' ,cartype AS 'نوع العربة',ketaa AS 'القطاع', date AS 'التاريخ',readbefore AS 'قراءة العداد قبل', readafter AS ' قراءة العداد بعد ' ,distance AS 'المسافة المقطوعة', numberoflitres AS'كمية الوقود المنصرفة' from carcounter where carnumber in ('ص ص ف / 357', 'ف ص ف / 694','ج ط ف / 671','ج ه ف / 572','أ م ر / 4358','أ م ر / 4361','ف و ص / 281','ف و ص / 261','ج ي ب / 385','ف و ص /497','ي ل ب / 629','و ف م / 716','ل ف م / 657','ن ف م / 924','ب د ر / 492','ي ي هـ / 284','ف د ر / 654','',''  )AND date in(@m,@y) ORDER BY carnumber, date", con);
             //   SqlCommand command = new SqlCommand("SELECT carnumber AS 'رقم العربة' , date AS 'التاريخ', readafter AS 'قراءة العداد ' from carcounter where carnumber in ('ص ص ف / 357', 'ف ص ف / 694','ج ط ف / 671','ج ه ف / 572','أ م ر / 4358','أ م ر / 4361','ف و ص / 281','ف و ص / 261','ج ي ب / 385','ف و ص /497','ي ل ب / 629','و ف م / 716','ل ف م / 657','ن ف م / 924','ب د ر / 492','ي ي هـ / 284','ف د ر / 654','',''  )AND date =@m , date=@y ORDER BY carnumber, date", con);




             //   command.Parameters.Add(new SqlParameter("@z", comboBox1.SelectedItem.ToString()));

                command.Parameters.Add(new SqlParameter("@m", dateTimePicker11.Value.Date.ToString("yyyy-MM-dd")));
                command.Parameters.Add(new SqlParameter("@y", dateTimePicker10.Value.Date.ToString("yyyy-MM-dd")));
               // command.Parameters.Add(new SqlParameter("@d", comboBox3.SelectedItem.ToString()));


                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = command;

                DataTable dt = new DataTable();

                da.Fill(dt);

                BindingSource bsource = new BindingSource();
                bsource.DataSource = dt;
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("mohammad art 1", 10.00F, FontStyle.Bold);

                dataGridView1.DefaultCellStyle.Font = new Font("mohammad art 1", 10.00F, FontStyle.Bold);

                dataGridView1.DataSource = bsource;


              //  }
                dataGridView1.Show();
                //     button11.Show();
                button9.Show();

            }

            catch (Exception ec)
            {



            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=ALLINONE-PC; Initial Catalog= BanzenDB; Integrated Security=SSPI";
                con.Open();




                SqlCommand command = new SqlCommand("SELECT  carnumber AS 'رقم العربة', SUM(distance) AS 'اجمالي المسافة' ,cartype AS 'نوع العربة' from carcounter  where date>@m and date<=@y   Group BY carnumber,cartype ORDER BY SUM(distance) DESC  ", con);


                command.Parameters.Add(new SqlParameter("@m", dateTimePicker12.Value.Date.ToString("yyyy-MM-dd")));
                command.Parameters.Add(new SqlParameter("@y", dateTimePicker13.Value.Date.ToString("yyyy-MM-dd")));
                // command.Parameters.Add(new SqlParameter("@d", comboBox3.SelectedItem.ToString()));


                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = command;

                DataTable dt = new DataTable();

                da.Fill(dt);

                BindingSource bsource = new BindingSource();
                bsource.DataSource = dt;
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("mohammad art 1", 10.00F, FontStyle.Bold);

                dataGridView1.DefaultCellStyle.Font = new Font("mohammad art 1", 10.00F, FontStyle.Bold);

                dataGridView1.DataSource = bsource;


                //  }
                dataGridView1.Show();
                //     button11.Show();
                button9.Show();

            }

            catch (Exception ec)
            {



            }
        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker14_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker15_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=ALLINONE-PC; Initial Catalog= BanzenDB; Integrated Security=SSPI";
                con.Open();



                //      SqlCommand command = new SqlCommand("SELECT  SUM(distance) from carcounter  where carnumber in('ي ي هـ / 284','ف د ر / 654') AND  date >= @m AND date<= @y  ", con);


                //  SELECT SUM(distance)  FROM carcounter WHERE date >= @m AND date<= @y  And carnumber=@x

                SqlCommand command = new SqlCommand("SELECT carnumber AS 'رقم العربة', SUM(numberoflitres) AS 'اجمالي كمية الوقود المنصرفة',cartype AS 'نوع العربة' from carcounter  where date>=@m and date<=@y   Group BY carnumber,cartype ORDER BY SUM(numberoflitres) DESC", con);


                command.Parameters.Add(new SqlParameter("@m", dateTimePicker14.Value.Date.ToString("yyyy-MM-dd")));
                command.Parameters.Add(new SqlParameter("@y", dateTimePicker15.Value.Date.ToString("yyyy-MM-dd")));
                // command.Parameters.Add(new SqlParameter("@d", comboBox3.SelectedItem.ToString()));


                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = command;

                DataTable dt = new DataTable();

                da.Fill(dt);

                BindingSource bsource = new BindingSource();
                bsource.DataSource = dt;
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("mohammad art 1", 10.00F, FontStyle.Bold);

                dataGridView1.DefaultCellStyle.Font = new Font("mohammad art 1", 10.00F, FontStyle.Bold);

                dataGridView1.DataSource = bsource;


                //  }
                dataGridView1.Show();
                //     button11.Show();
                button9.Show();

            }

            catch (Exception ec)
            {



            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker5_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker6_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker12_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker13_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker10_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker11_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }
    }
}
