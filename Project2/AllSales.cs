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
    public partial class AllSales : DevExpress.XtraEditors.XtraForm
    {
        public AllSales(string x, string y)
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

        //Preview all sales operations and calculate total sales price 
        private void AllSales_Load(object sender, EventArgs e)
        {
            try
            {
                float Total = 0;

                List<String> totalsales = new List<string>();

                DataTable table = new DataTable();

                SqlConnection CONN = new SqlConnection(DatabaseConnection.Connection);
                SqlCommand command = new SqlCommand();

                command.Connection = CONN;
                command.CommandText = "select [Invo_TotalPrice] from Invoices";

                CONN.Open();

                table.Load(command.ExecuteReader());

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    totalsales.Add(table.Rows[i][0].ToString());
                    Total += float.Parse(totalsales[i]);
                }

                total.Text = Total.ToString();

                //___________________________________________________________________________

                DataTable table1 = new DataTable();

                SqlConnection CONN1 = new SqlConnection(DatabaseConnection.Connection);

                SqlCommand command1 = new SqlCommand();

                command1.Connection = CONN1;
                command1.CommandText = "select [Invo_Num] as 'رقم الفاتوره', [Prod_Code] as 'كود المنتج' ,[Prod_Name] as 'اسم المنتج' , [Cus_Name] as 'اسم العميل' ,[Quantity] as 'الكميه المباعه', [Price] as 'سعر الكميه المباعه', [Date] as 'التاريخ', [Time] as 'الوقت' from Sales";

                dataGridView1.DataSource = table1;

                CONN1.Open();
                table1.Load(command1.ExecuteReader());

                CONN1.Close();

            }
            catch (Exception)
            {
                MessageBox.Show("برجاء استكمال البيانات المطلوبه", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Sales Page
        private void button2_Click(object sender, EventArgs e)
        {
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
        }
    }
}