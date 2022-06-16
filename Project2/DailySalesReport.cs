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
    public partial class DailySalesReport : DevExpress.XtraEditors.XtraForm
    {
        public DailySalesReport(string x, string y)
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

        //Report Page
        private void button2_Click(object sender, EventArgs e)
        {
            Report report = new Report(name.Text, right.Text);

            if (report == null)
            {
                this.Hide();
                report.Show();
            }
            else
            {
                this.Hide();
                report.Show();
                report.Focus();
            }
        }

        //Preview Sales of Selected Day
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string salesday = date.Value.ToString("dd/MM/yyyy");
                float totalSales = 0;

                if (salesday.Equals(""))
                {
                    MessageBox.Show("برجاء ادخال تاريخ لعرض المبيعات", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    List<String> dates = new List<string>();

                    DataTable table = new DataTable();

                    SqlConnection CONN = new SqlConnection(DatabaseConnection.Connection);
                    SqlCommand command = new SqlCommand();

                    command.Connection = CONN;
                    command.CommandText = "select [Date] from Sales";

                    CONN.Open();

                    table.Load(command.ExecuteReader());

                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        dates.Add(table.Rows[i][0].ToString());
                    }
                    if (dates.Contains(salesday))
                    {
                        DataTable table1 = new DataTable();

                        SqlConnection CONN1 = new SqlConnection(DatabaseConnection.Connection);

                        SqlCommand command1 = new SqlCommand();

                        command1.Connection = CONN1;
                        command1.CommandText = "select [Invo_Num] as 'رقم الفاتوره' , [Cus_Name] as 'اسم العميل' , [Prod_Name] as 'اسم المنتح' ,[Prod_Code] as 'كود المنتج', [Quantity] as 'الكميه', [Price] as 'اجمالى السعر', [Time] as 'وقت العمليه' from Sales where Date='" + salesday + "' ";

                        dataGridView1.DataSource = table1;

                        CONN1.Open();
                        table1.Load(command1.ExecuteReader());

                        CONN1.Close();

                        //_______________________________________________

                        List<String> totalsales = new List<string>();

                        DataTable table2 = new DataTable();

                        SqlConnection CONN2 = new SqlConnection(DatabaseConnection.Connection);
                        SqlCommand command2 = new SqlCommand();

                        command2.Connection = CONN2;
                        command2.CommandText = "select [Price] from Sales where Date='" + salesday + "'";

                        CONN2.Open();

                        table2.Load(command2.ExecuteReader());

                        for (int i = 0; i < table2.Rows.Count; i++)
                        {
                            totalsales.Add(table2.Rows[i][0].ToString());
                            totalSales += float.Parse(totalsales[i]);
                        }

                        total.Text = totalSales.ToString();
                    }
                    else
                    {
                        MessageBox.Show("لا يوجد مبيعات فى ذلك اليوم", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("برجاء استكمال البيانات المطلوبه", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}