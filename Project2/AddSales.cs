using DevExpress.XtraEditors;
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

namespace Project2
{
    public partial class AddSales : DevExpress.XtraEditors.XtraForm
    {
        int Invo_Num;
        string Invo_TotalPrice;

        public AddSales(string x, string y)
        {
            InitializeComponent();
            name.Text = x;
            right.Text = y;
        }

        //Logout
        private void logout_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("هل متأكد من تسجيل الخروج", "قهوتى", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                Application.OpenForms[0].Show();
                this.Close();
            }
        }

        //Notification
        private void notify_Click(object sender, EventArgs e)
        {
            MessageBox.Show("لا يوجد اشعارات حاليا", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //Home Page
        private void home_Click(object sender, EventArgs e)
        {
            Main main = new Main(name.Text, right.Text);

            if (main == null)
            {
                this.Hide();
                main.Show();
            }
            else
            {
                this.Hide();
                main.Show();
                main.Focus();
            }
        }

        //Get All Products and Customers Name From DB , Get Current Date and Time
        private void AddSales_Load(object sender, EventArgs e)
        {
            List<String> Products_Name = new List<string>();
            List<String> Customers_Name = new List<string>();

            DataTable table = new DataTable();
            DataTable table1 = new DataTable();

            SqlConnection CONN = new SqlConnection(DatabaseConnection.Connection);
            SqlConnection CONN1 = new SqlConnection(DatabaseConnection.Connection);

            SqlCommand command = new SqlCommand();
            SqlCommand command1 = new SqlCommand();

            command.Connection = CONN;
            command1.Connection = CONN1;

            command.CommandText = "select [Prod_Name] from Purchases";
            command1.CommandText = "select [Cus_Name] from Customers";

            CONN.Open();
            CONN1.Open();

            table.Load(command.ExecuteReader());
            table1.Load(command1.ExecuteReader());

            for (int i = 0; i < table.Rows.Count; i++)
            {
                Products_Name.Add(table.Rows[i][0].ToString());
                prodname.Items.Add(Products_Name[i]);
            }
            for (int i = 0; i < table1.Rows.Count; i++)
            {
                Customers_Name.Add(table1.Rows[i][0].ToString());
                cusname.Items.Add(Customers_Name[i]);
            }

            //_________________________________________________________
           
            string datenow = DateTime.Today.ToString("dd/MM/yyyy");
            date.Text = datenow;
            string timenow = DateTime.Now.ToString("HH:mm:ss");
            time.Text = timenow;

            //_________________________________________________________

        }

        //Get The Product Code from DB and Check for Decimal Quantity
        private void prodname_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] Products1 = { "بن ساده", "بن محوج", "بن اسبيشيال", "بن ملوكى", "بن كولومبى", "بن السعاده", "بن تخسيس", "بن عربى", "بن اسبريسو", "بن نكهات فرنساوى", "نسكافيه خام" };

            List<String> Products_Code = new List<string>();

            DataTable table2 = new DataTable();

            SqlConnection CONN2 = new SqlConnection(DatabaseConnection.Connection);

            SqlCommand command2 = new SqlCommand();

            command2.Connection = CONN2;

            command2.CommandText = "select [Prod_Code] from Products where Prod_Name = '" + prodname.Text + "' ";

            CONN2.Open();

            table2.Load(command2.ExecuteReader());

            Products_Code.Add(table2.Rows[0][0].ToString());

            prodcode.Text = Products_Code[0].ToString();

            //_________________________________________________________

            if (Products1.Contains(prodname.Text))
            {
                prodquan.DecimalPlaces = 3;
            }
            else
            {
                prodquan.DecimalPlaces = 0;
            }
        }

        //Calculate The Product Price depends of the product quantity
        private void prodquan_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                string proname = prodname.Text;
                string proquan = prodquan.Value.ToString();

                if(proname.Equals("") || proquan.Equals("0.000") || proquan.Equals("0"))
                {
                    MessageBox.Show("برجاء استكمال البيانات المطلوبه", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DataTable table3 = new DataTable();

                    SqlConnection CONN3 = new SqlConnection(DatabaseConnection.Connection);
                    SqlCommand command3 = new SqlCommand();

                    command3.Connection = CONN3;
                    command3.CommandText = "select [Prod_Price] from Products where Prod_Name = '" + proname + "' ";

                    CONN3.Open();
                    table3.Load(command3.ExecuteReader());

                    string proprice = table3.Rows[0][0].ToString();
                    float totalprice = float.Parse(proquan) * float.Parse(proprice);

                    total.Text = totalprice.ToString();
                    CONN3.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("برجاء استكمال البيانات المطلوبه", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Add Product at Invoice (Check available quantity before add item and calculate the total invoice price) , hide the customer name filed to avoid update it
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string procode = prodcode.Text;
                string proname = prodname.Text;
                string cuname = cusname.Text;
                string proquan = prodquan.Value.ToString();
                float totalprice = float.Parse(total.Text);

                if (proname.Equals("") || cuname.Equals("") || proquan.Equals("0"))
                {
                    MessageBox.Show("برجاء استكمال البيانات المطلوبه", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DataTable table4 = new DataTable();

                    SqlConnection CONN4 = new SqlConnection(DatabaseConnection.Connection);
                    SqlCommand command4 = new SqlCommand();

                    command4.Connection = CONN4;
                    command4.CommandText = "select [Available_Quantity] from Purchases where Prod_Name= '" + proname + "' ";

                    CONN4.Open();
                    table4.Load(command4.ExecuteReader());

                    string Available_Quan = table4.Rows[0][0].ToString();

                    CONN4.Close();

                    if (float.Parse(proquan) > float.Parse(Available_Quan))
                    {
                        MessageBox.Show("هذاالمنتج غير متوفر بالكميه الحاليه", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        DialogResult result;
                        result = MessageBox.Show("هل متأكد من اضافه الصنف للفاتوره", "قهوتى", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                        if (result == DialogResult.Yes)
                        {
                            object[] row = { procode, proname, proquan, totalprice };

                            dataGridView1.Rows.Add(row);

                            total.Text = "";

                            float t = 0;

                            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                            {
                                t += float.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                            }

                            totalinvo.Text = t.ToString();

                            cusname.Hide();
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("برجاء استكمال البيانات المطلوبه", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Calculate Total invoice price and write number in DB,Get the number of invoice,Update quantity,Update Total Sales Price
        private void button1_Click(object sender, EventArgs e)
        {
            ((Form)printPreviewDialog1).WindowState = FormWindowState.Maximized;

            try
            {
                string cuname = cusname.Text;
                string orddate = date.Text;
                string ordtime = time.Text;
                string cuid;
                List<String> ProductsCode = new List<string>();
                List<String> ProductsName = new List<string>();
                List<String> ProductsQuantity = new List<string>();
                List<String> ProductsPrice = new List<string>();

                if (dataGridView1.Rows.Count == 0 || cuname.Equals("") || orddate.Equals("") || ordtime.Equals(""))
                {
                    MessageBox.Show("برجاء استكمال البيانات المطلوبه", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else
                {
                    DialogResult result;
                    result = MessageBox.Show("هل متأكد من اضافه عمليه بيع جديده", "قهوتى", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (result == DialogResult.Yes)
                    {
                        SqlConnection CONN5 = new SqlConnection(DatabaseConnection.Connection);
                        SqlCommand command5 = new SqlCommand();

                        command5.Connection = CONN5;

                        Invo_TotalPrice = totalinvo.Text;

                        command5.CommandText = "INSERT INTO Invoices values('" + Invo_TotalPrice + "')";

                        CONN5.Open();
                        command5.ExecuteNonQuery();

                        CONN5.Close();

                        //_________________________________________________________________


                        List<String> Invo_Nums = new List<string>();

                        DataTable table6 = new DataTable();

                        SqlConnection CONN6 = new SqlConnection(DatabaseConnection.Connection);
                        SqlCommand command6 = new SqlCommand();

                        command6.Connection = CONN6;
                        command6.CommandText = "select [Invo_Num] from Invoices";

                        CONN6.Open();

                        table6.Load(command6.ExecuteReader());

                        for (int j = 0; j < table6.Rows.Count; j++)
                        {
                            Invo_Nums.Add(table6.Rows[j][0].ToString());
                        }
                        Invo_Num = int.Parse(Invo_Nums.Last().ToString());


                        if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                        {
                            printDocument1.Print();
                        }

                        //_________________________________________________________________

                        DataTable table7 = new DataTable();

                        SqlConnection CONN7 = new SqlConnection(DatabaseConnection.Connection);

                        SqlCommand command7 = new SqlCommand();

                        command7.Connection = CONN6;
                        command7.CommandText = "select [Cus_ID] from Customers where Cus_Name= '" + cusname.Text + "'";

                        CONN7.Open();
                        table7.Load(command7.ExecuteReader());

                        cuid = table7.Rows[0][0].ToString();

                        CONN6.Close();

                        //_________________________________________________________________
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            ProductsCode.Add(dataGridView1.Rows[i].Cells[0].Value.ToString());
                            ProductsName.Add(dataGridView1.Rows[i].Cells[1].Value.ToString());
                            ProductsQuantity.Add(dataGridView1.Rows[i].Cells[2].Value.ToString());
                            ProductsPrice.Add(dataGridView1.Rows[i].Cells[3].Value.ToString());

                            //_________________________________________________________________

                            DataTable table8 = new DataTable();

                            SqlConnection CONN8 = new SqlConnection(DatabaseConnection.Connection);
                            SqlCommand command8 = new SqlCommand();

                            command8.Connection = CONN8;
                            command8.CommandText = "select [Available_Quantity] from Purchases where Prod_Code= '" + dataGridView1.Rows[i].Cells[0].Value + "' ";

                            CONN8.Open();
                            table8.Load(command8.ExecuteReader());

                            string Available_Quan = table8.Rows[0][0].ToString();
                            string ProdQuan = dataGridView1.Rows[i].Cells[2].Value.ToString();

                            float Update_Quan = float.Parse(Available_Quan) - float.Parse(ProdQuan);
                            CONN8.Close();

                            //_________________________________________________________________

                            SqlConnection CONN9 = new SqlConnection(DatabaseConnection.Connection);
                            SqlCommand command9 = new SqlCommand();

                            command9.Connection = CONN9;
                            command9.CommandText = "update Purchases set Available_Quantity='" + Update_Quan + "' where Prod_Code= '" + dataGridView1.Rows[i].Cells[0].Value + "'";

                            CONN9.Open();
                            command9.ExecuteNonQuery();

                            //_________________________________________________________________

                            SqlConnection CONN10 = new SqlConnection(DatabaseConnection.Connection);
                            SqlCommand command10 = new SqlCommand();

                            command10.Connection = CONN10;

                            command10.CommandText = "INSERT INTO Sales values(" + Invo_Num + ",'" + cuid + "','" + cuname + "','" + ProductsCode[i] + "','" + ProductsName[i] + "','" + ProductsQuantity[i] + "' ," + ProductsPrice[i] + ",'" + orddate + "','" + ordtime + "')";

                            CONN10.Open();
                            command10.ExecuteNonQuery();

                            CONN10.Close();

                            //_________________________________________________________________
                        }

                        float Total = 0;

                        List<String> totalsales = new List<string>();

                        DataTable table11 = new DataTable();

                        SqlConnection CONN11 = new SqlConnection(DatabaseConnection.Connection);
                        SqlCommand command11 = new SqlCommand();

                        command11.Connection = CONN11;
                        command11.CommandText = "select [Invo_TotalPrice] from Invoices";

                        CONN11.Open();

                        table11.Load(command11.ExecuteReader());

                        for (int i = 0; i < table11.Rows.Count; i++)
                        {
                            totalsales.Add(table11.Rows[i][0].ToString());
                            Total += float.Parse(totalsales[i]);
                        }

                        //_________________________________________________________________

                        SqlConnection CONN12 = new SqlConnection(DatabaseConnection.Connection);

                        SqlCommand command12 = new SqlCommand();

                        command12.Connection = CONN12;
                        command12.CommandText = "update TotalSales set Total_Sales='" + Total + "'";

                        CONN12.Open();
                        command12.ExecuteNonQuery();

                        CONN12.Close();

                        Sales sales = new Sales(name.Text, right.Text);

                        if (sales == null)
                        {
                            this.Hide();
                            sales.Show();
                        }
                        else
                        {
                            this.Hide();
                            sales.Show();
                            sales.Focus();
                        }

                        MessageBox.Show("شكرا لتعاملكم معنا", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("برجاء استكمال البيانات المطلوبه", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Print Document Details
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            float mar = 50;

            Font font1 = new Font("Kristen ITC", 24, FontStyle.Bold);
            Font font2 = new Font("Kristen ITC", 18, FontStyle.Regular);
            Font font3 = new Font("Kristen ITC", 18, FontStyle.Bold);
            Font font4 = new Font("Kristen ITC", 12, FontStyle.Underline);

            SizeF size1 = e.Graphics.MeasureString("رقم الفاتوره :" + Invo_Num.ToString(), font1);
            SizeF size2 = e.Graphics.MeasureString("التاريخ : " + date.Text, font2);
            SizeF size3 = e.Graphics.MeasureString("الوقت : " + time.Text, font2);
            SizeF size4 = e.Graphics.MeasureString("اسم العميل : " + cusname.Text, font2);
            SizeF size5 = e.Graphics.MeasureString("Copyrights : Muhammad Gomaa ©", font4);

            e.Graphics.DrawImage(Properties.Resources.turkish_coffee, mar - 45, mar - 45, 200, 200);

            e.Graphics.DrawString("رقم الفاتوره :" + Invo_Num.ToString(), font1, Brushes.Black, (e.PageBounds.Width - size1.Width) / 2, mar);
            e.Graphics.DrawString("التاريخ : " + date.Text, font2, Brushes.Black, (e.PageBounds.Width - size2.Width - mar), mar + size1.Height);
            e.Graphics.DrawString("الوقت : " + time.Text, font2, Brushes.Black, (e.PageBounds.Width - size3.Width - mar), mar + size1.Height + size2.Height);
            e.Graphics.DrawString("اسم العميل : " + cusname.Text, font2, Brushes.Black, (e.PageBounds.Width - size4.Width - mar), mar + size1.Height + size2.Height + size3.Height);
            e.Graphics.DrawString("Copyrights : Muhammad Gomaa ©", font4, Brushes.Black, (e.PageBounds.Width - size5.Width) / 2, mar + size1.Height + size2.Height + size3.Height + size4.Height);


            float PreHeights = mar + size1.Height + size2.Height + size3.Height + size4.Height + 70;

            e.Graphics.DrawRectangle(Pens.Black, mar, PreHeights, e.PageBounds.Width - mar * 2, e.PageBounds.Height - mar - PreHeights);

            float ColHeight = 60;

            float Col1Width = 100;
            float Col2Width = 300 + Col1Width;
            float Col3Width = 125 + Col2Width;
            float Col4Width = 150 + Col3Width;

            e.Graphics.DrawLine(Pens.Black, mar, PreHeights + ColHeight, e.PageBounds.Width - mar, PreHeights + ColHeight);

            e.Graphics.DrawString("كود المنتج", font3, Brushes.Black, (e.PageBounds.Width - mar * 2) - Col1Width, PreHeights);
            e.Graphics.DrawLine(Pens.Black, (e.PageBounds.Width - mar * 2) - Col1Width, PreHeights, (e.PageBounds.Width - mar * 2) - Col1Width, e.PageBounds.Height - mar);

            e.Graphics.DrawString("اسم المنتج", font3, Brushes.Black, (e.PageBounds.Width - mar * 2) - Col2Width, PreHeights);
            e.Graphics.DrawLine(Pens.Black, (e.PageBounds.Width - mar * 2) - Col2Width, PreHeights, (e.PageBounds.Width - mar * 2) - Col2Width, e.PageBounds.Height - mar);

            e.Graphics.DrawString("الكميه", font3, Brushes.Black, (e.PageBounds.Width - mar * 2) - Col3Width, PreHeights);
            e.Graphics.DrawLine(Pens.Black, (e.PageBounds.Width - mar * 2) - Col3Width, PreHeights, (e.PageBounds.Width - mar * 2) - Col3Width, e.PageBounds.Height - mar);

            e.Graphics.DrawString("اجمالى السعر", font3, Brushes.Black, (e.PageBounds.Width - mar * 2) - Col4Width, PreHeights);

            float RowsHeight = 60;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {

                e.Graphics.DrawString(dataGridView1.Rows[i].Cells[0].Value.ToString(), font2, Brushes.Black, (e.PageBounds.Width - mar * 2) - Col1Width, PreHeights + RowsHeight);
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells[1].Value.ToString(), font2, Brushes.Black, (e.PageBounds.Width - mar * 2) - Col2Width, PreHeights + RowsHeight);
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells[2].Value.ToString(), font2, Brushes.Black, (e.PageBounds.Width - mar * 2) - Col3Width, PreHeights + RowsHeight);
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells[3].Value.ToString(), font2, Brushes.Black, (e.PageBounds.Width - mar * 2) - Col4Width, PreHeights + RowsHeight);
                e.Graphics.DrawLine(Pens.Black, mar, PreHeights + RowsHeight + ColHeight, e.PageBounds.Width - mar, PreHeights + RowsHeight + ColHeight);


                RowsHeight += 60;
            }

            e.Graphics.DrawString("اجمالى سعر الفاتوره", font3, Brushes.Blue, (e.PageBounds.Width - mar * 2) - Col3Width, PreHeights + RowsHeight);
            e.Graphics.DrawString(totalinvo.Text, font3, Brushes.Blue, (e.PageBounds.Width - mar * 2) - Col4Width, PreHeights + RowsHeight);
            e.Graphics.DrawLine(Pens.Black, mar, PreHeights + RowsHeight + ColHeight, e.PageBounds.Width - mar, PreHeights + RowsHeight + ColHeight);

        }
    }
}