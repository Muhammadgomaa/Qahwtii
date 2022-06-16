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
    public partial class SupplierPurchases : DevExpress.XtraEditors.XtraForm
    {
        public SupplierPurchases(string x, string y)
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

        //Supplier Page
        private void button2_Click(object sender, EventArgs e)
        {
            Supplier supplier = new Supplier(name.Text, right.Text);

            if (supplier == null)
            {
                this.Hide();
                supplier.Show();
            }
            else
            {
                this.Hide();
                supplier.Show();
                supplier.Focus();
            }
        }

        //Preview All Suppliers Name
        private void SupplierPurchases_Load(object sender, EventArgs e)
        {
            List<String> Suppliers_Name = new List<string>();

            DataTable table = new DataTable();

            SqlConnection CONN = new SqlConnection(DatabaseConnection.Connection);
            SqlCommand command = new SqlCommand();

            command.Connection = CONN;
            command.CommandText = "select [Supp_Name] from Suppliers";

            CONN.Open();

            table.Load(command.ExecuteReader());

            for (int i = 0; i < table.Rows.Count; i++)
            {
                Suppliers_Name.Add(table.Rows[i][0].ToString());
                suppname.Items.Add(Suppliers_Name[i]);
            }
        }

        //Preview Supplier's Purchases Button
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string supname = suppname.Text;

                if (supname.Equals(""))
                {
                    MessageBox.Show("برجاء استكمال البيانات المطلوبه", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DataTable table1 = new DataTable();

                    SqlConnection CONN1 = new SqlConnection(DatabaseConnection.Connection);
                    SqlCommand command1 = new SqlCommand();

                    command1.Connection = CONN1;
                    command1.CommandText = "select [Supp_Name] as 'اسم المورد',[Prod_Code] as 'كود المنتج',[Prod_Name] as 'اسم المنتح',[Purch_Quantity] as 'الكميه',[Purch_TotalBuy] as 'اجمالى قيمه الكميه' from Purchases where Supp_Name = '" + supname + "' ";

                    dataGridView1.DataSource = table1;

                    CONN1.Open();
                    table1.Load(command1.ExecuteReader());

                    CONN1.Close();

                }
            }
            catch (Exception)
            {
                MessageBox.Show("برجاء استكمال البيانات المطلوبه", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}