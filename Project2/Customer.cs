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

namespace Project2
{
    public partial class Customer : DevExpress.XtraEditors.XtraForm
    {
        public Customer(string x, string y)
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

        //Product Page
        private void product_menu_Click(object sender, EventArgs e)
        {
            Product product = new Product(name.Text, right.Text);

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

        //Home Page
        private void home_button_Click(object sender, EventArgs e)
        {
            home_Click(sender, e);
        }

        //Sales Page
        private void sales_menu_Click(object sender, EventArgs e)
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

        //Add Customer Page
        private void tileItem1_ItemClick(object sender, TileItemEventArgs e)
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

        //Update Customer Page
        private void tileItem2_ItemClick(object sender, TileItemEventArgs e)
        {
            UpdateCustomer1 updateCustomer1 = new UpdateCustomer1(name.Text, right.Text);

            if (updateCustomer1 == null)
            {
                this.Hide();
                updateCustomer1.Show();
            }
            else
            {
                this.Hide();
                updateCustomer1.Show();
                updateCustomer1.Focus();
            }
        }

        //All Customers Page
        private void tileItem4_ItemClick(object sender, TileItemEventArgs e)
        {
            AllCustomers allCustomers = new AllCustomers(name.Text, right.Text);

            if (allCustomers == null)
            {
                this.Hide();
                allCustomers.Show();
            }
            else
            {
                this.Hide();
                allCustomers.Show();
                allCustomers.Focus();
            }
        }

        //Customer Orders Page
        private void tileItem5_ItemClick(object sender, TileItemEventArgs e)
        {
            CustomerOrders customerOrders = new CustomerOrders(name.Text, right.Text);

            if (customerOrders == null)
            {
                this.Hide();
                customerOrders.Show();
            }
            else
            {
                this.Hide();
                customerOrders.Show();
                customerOrders.Focus();
            }
        }
    }
}