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
    public partial class Main : DevExpress.XtraEditors.XtraForm
    {
        public Main(string x,string y)
        {
            InitializeComponent();
            name.Text=x;
            right.Text=y;
        }

        //Product Page
        private void product_menu_Click(object sender, EventArgs e)
        {
            Product product = new Product(name.Text,right.Text);

            if (product == null)
            {
                this.Hide();
                product.Show();
            }
            else
            {
                this.Hide();
                product.Show();
                product.Focus();
            }
        }

        //Supplier Page
        private void supplier_menu_Click(object sender, EventArgs e)
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

        //Purchase Page
        private void purchase_menu_Click(object sender, EventArgs e)
        {
            Purchase purchase = new Purchase(name.Text, right.Text);

            if (purchase == null)
            {
                this.Hide();
                purchase.Show();
            }
            else
            {
                this.Hide();
                purchase.Show();
                purchase.Focus();
            }
        }

        //Customer Page
        private void customer_menu_Click(object sender, EventArgs e)
        {
            Customer customer= new Customer(name.Text, right.Text);

            if (customer == null)
            {
                this.Hide();
                customer.Show();
            }
            else
            {
                this.Hide();
                customer.Show();
                customer.Focus();
            }
        }

        //Sales Page
        private void sales_menu_Click(object sender, EventArgs e)
        {
            Sales sales= new Sales(name.Text, right.Text);

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

        //Report Page
        private void report_menu_Click(object sender, EventArgs e)
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

        //Setting Page
        private void setting_menu_Click(object sender, EventArgs e)
        {
            Setting setting = new Setting(name.Text, right.Text);

            if (setting == null)
            {
                this.Hide();
                setting.Show();
            }
            else
            {
                this.Hide();
                setting.Show();
                setting.Focus();
            }
        }

        //Logout
        private void logout_Click_1(object sender, EventArgs e)
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

        //Add Product Page (Check rights befroe add)
        private void button1_Click(object sender, EventArgs e)
        {
            if (right.Text.Equals("admin"))
            {
                AddProduct addProduct = new AddProduct(name.Text, right.Text);

                if (addProduct == null)
                {
                    this.Hide();
                    addProduct.Show();
                }
                else
                {
                    this.Hide();
                    addProduct.Show();
                    addProduct.Focus();
                }
            }
            else
            {
                MessageBox.Show("تلك الصلاحيه ليست مطابقه لمهامك الوظيفيه", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Add Sales Page
        private void button2_Click(object sender, EventArgs e)
        {
            AddSales addSales = new AddSales(name.Text, right.Text);

            if (addSales == null)
            {
                this.Hide();
                addSales.Show();
            }
            else
            {
                this.Hide();
                addSales.Show();
                addSales.Focus();
            }
        }

        //Add Customer Page
        private void button3_Click(object sender, EventArgs e)
        {
            AddCustomer addCustomer = new AddCustomer(name.Text, right.Text);

            if (addCustomer == null)
            {
                this.Hide();
                addCustomer.Show();
            }
            else
            {
                this.Hide();
                addCustomer.Show();
                addCustomer.Focus();
            }
        }

        //Add Supplier Page (Check rights before add)
        private void button4_Click(object sender, EventArgs e)
        {
            if (right.Text.Equals("admin"))
            {
                AddSupplier addSupplier = new AddSupplier(name.Text, right.Text);

                if (addSupplier == null)
                {
                    this.Hide();
                    addSupplier.Show();
                }
                else
                {
                    this.Hide();
                    addSupplier.Show();
                    addSupplier.Focus();
                }
            }
            else
            {
                MessageBox.Show("تلك الصلاحيه ليست مطابقه لمهامك الوظيفيه", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //To Preview Numbers of Products,Suppliers,Customers,Purchases Operations , Sales Operations and Reports.
        private void Main_Load(object sender, EventArgs e)
        {
            try
            {
                List<String> productscount = new List<string>();
                List<String> supplierscount = new List<string>();
                List<String> purchasescount = new List<string>();
                List<String> customerscount = new List<string>();
                List<String> salescount = new List<string>();
                List<String> reportscount = new List<string>();

                DataTable table = new DataTable();
                DataTable table1 = new DataTable();
                DataTable table2 = new DataTable();
                DataTable table3 = new DataTable();
                DataTable table4 = new DataTable();
                DataTable table5 = new DataTable();

                SqlConnection CONN = new SqlConnection(DatabaseConnection.Connection);
                SqlConnection CONN1 = new SqlConnection(DatabaseConnection.Connection);
                SqlConnection CONN2 = new SqlConnection(DatabaseConnection.Connection);
                SqlConnection CONN3 = new SqlConnection(DatabaseConnection.Connection);
                SqlConnection CONN4 = new SqlConnection(DatabaseConnection.Connection);
                SqlConnection CONN5 = new SqlConnection(DatabaseConnection.Connection);

                SqlCommand command = new SqlCommand();
                SqlCommand command1 = new SqlCommand();
                SqlCommand command2 = new SqlCommand();
                SqlCommand command3 = new SqlCommand();
                SqlCommand command4 = new SqlCommand();
                SqlCommand command5 = new SqlCommand();

                command.Connection = CONN;
                command.CommandText = "select [Prod_Code] from Products";

                command1.Connection = CONN1;
                command1.CommandText = "select [Supp_ID] from Suppliers";

                command2.Connection = CONN2;
                command2.CommandText = "select [Purch_ID] from Purchases";

                command3.Connection = CONN3;
                command3.CommandText = "select [Cus_ID] from Customers";

                command4.Connection = CONN4;
                command4.CommandText = "select [Invo_Num] from Invoices";

                CONN.Open();
                CONN1.Open();
                CONN2.Open();
                CONN3.Open();
                CONN4.Open();

                table.Load(command.ExecuteReader());
                table1.Load(command1.ExecuteReader());
                table2.Load(command2.ExecuteReader());
                table3.Load(command3.ExecuteReader());
                table4.Load(command4.ExecuteReader());

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    productscount.Add(table.Rows[i][0].ToString());
                }
                for (int i = 0; i < table1.Rows.Count; i++)
                {
                    supplierscount.Add(table1.Rows[i][0].ToString());
                }
                for (int i = 0; i < table2.Rows.Count; i++)
                {
                    purchasescount.Add(table2.Rows[i][0].ToString());
                }
                for (int i = 0; i < table3.Rows.Count; i++)
                {
                    customerscount.Add(table3.Rows[i][0].ToString());
                }
                for (int i = 0; i < table4.Rows.Count; i++)
                {
                    salescount.Add(table4.Rows[i][0].ToString());
                }
                numofprod.Text = productscount.Count.ToString();
                numofsupp.Text = supplierscount.Count.ToString();
                numofpurchase.Text = purchasescount.Count.ToString();
                numofcus.Text = customerscount.Count.ToString();
                numofsales.Text = salescount.Count.ToString();
                numofrepo.Text = "8";

            }
            catch (Exception)
            {
                MessageBox.Show("برجاء استكمال البيانات المطلوبه", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}