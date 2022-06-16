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
    public partial class ProductReport : DevExpress.XtraEditors.XtraForm
    {
        public ProductReport(string x, string y)
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

        //Get All Products Name in DB
        private void ProductReport_Load(object sender, EventArgs e)
        {
            List<String> Products_Name = new List<string>();

            DataTable table = new DataTable();

            SqlConnection CONN = new SqlConnection(DatabaseConnection.Connection);
            SqlCommand command = new SqlCommand();

            command.Connection = CONN;
            command.CommandText = "select [Prod_Name] from Purchases";

            CONN.Open();

            table.Load(command.ExecuteReader());

            for (int i = 0; i < table.Rows.Count; i++)
            {
                Products_Name.Add(table.Rows[i][0].ToString());
                prodname.Items.Add(Products_Name[i]);
            }
        }

        //Preview All Products Information and Num. of Sales
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string pname = prodname.Text;

                if (pname.Equals(""))
                {
                    MessageBox.Show("برجاء استكمال البيانات المطلوبه", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DataTable table1 = new DataTable();

                    SqlConnection CONN1 = new SqlConnection(DatabaseConnection.Connection);
                    SqlCommand command1 = new SqlCommand();

                    command1.Connection = CONN1;
                    command1.CommandText = "select [Prod_Code] as 'كود المنتج' , [Prod_Name] as 'اسم المنتح', [Supp_Name] as 'اسم المورد' , [Purch_Buy] as ' التكلفه|سعر الشراء' , [Purch_Sell] as 'سعر البيع' from Purchases where Prod_Name = '" + pname + "' ";

                    dataGridView1.DataSource = table1;

                    CONN1.Open();
                    table1.Load(command1.ExecuteReader());

                    CONN1.Close();

                    //____________________________________________________________________________________

                    List<String> productsales = new List<string>();

                    DataTable table2 = new DataTable();

                    SqlConnection CONN2 = new SqlConnection(DatabaseConnection.Connection);
                    SqlCommand command2 = new SqlCommand();

                    command2.Connection = CONN2;
                    command2.CommandText = "select [Prod_Code] from Sales where Prod_Name = '" + pname + "'";

                    CONN2.Open();

                    table2.Load(command2.ExecuteReader());

                    for (int i = 0; i < table2.Rows.Count; i++)
                    {
                        productsales.Add(table2.Rows[i][0].ToString());
                    }
                    total.Text = productsales.Count.ToString();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("برجاء استكمال البيانات المطلوبه", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}