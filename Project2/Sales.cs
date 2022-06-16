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
    public partial class Sales : DevExpress.XtraEditors.XtraForm
    {
        public Sales(string x, string y)
        {
            InitializeComponent();
            name.Text = x;
            right.Text = y;
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

        //Home Page
        private void home_button_Click(object sender, EventArgs e)
        {
            home_Click(sender, e);
        }

        //Product Page
        private void product_menu_Click_1(object sender, EventArgs e)
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
        private void supplier_menu_Click_1(object sender, EventArgs e)
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
        private void purchase_menu_Click_1(object sender, EventArgs e)
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
        private void customer_menu_Click_1(object sender, EventArgs e)
        {
            Customer customer = new Customer(name.Text, right.Text);

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

        //Report Page
        private void report_menu_Click_1(object sender, EventArgs e)
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

        //Setting
        private void setting_menu_Click_1(object sender, EventArgs e)
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

        //Add Sales Operation
        private void tileItem1_ItemClick(object sender, TileItemEventArgs e)
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

        //All Sales Operation Information
        private void tileItem4_ItemClick(object sender, TileItemEventArgs e)
        {
            AllSales allSales = new AllSales(name.Text, right.Text);

            if (allSales == null)
            {
                this.Hide();
                allSales.Show();
            }
            else
            {
                this.Hide();
                allSales.Show();
                allSales.Focus();
            }
        }

        //Delete Sales Operation (Check right before delete)
        private void tileItem2_ItemClick(object sender, TileItemEventArgs e)
        {
            if (right.Text.Equals("admin"))
            {
                DeleteSales deleteSales = new DeleteSales(name.Text, right.Text);

                if (deleteSales == null)
                {
                    this.Hide();
                    deleteSales.Show();
                }
                else
                {
                    this.Hide();
                    deleteSales.Show();
                    deleteSales.Focus();
                }
            }
            else
            {
                MessageBox.Show("تلك الصلاحيه ليست مطابقه لمهامك الوظيفيه", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Invoice Page
        private void tileItem3_ItemClick(object sender, TileItemEventArgs e)
        {
            Invoice invoice = new Invoice(name.Text, right.Text);

            if (invoice == null)
            {
                this.Hide();
                invoice.Show();
            }
            else
            {
                this.Hide();
                invoice.Show();
                invoice.Focus();
            }
        }
    }
}