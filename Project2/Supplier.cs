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
    public partial class Supplier : DevExpress.XtraEditors.XtraForm
    {
        public Supplier(string x, string y)
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

        //Home Page
        private void home_button_Click(object sender, EventArgs e)
        {
            home_Click(sender, e);
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

        //Add Supplier Page (Check rights before add)
        private void tileItem1_ItemClick(object sender, TileItemEventArgs e)
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

        //Update Supplier Page (Check rights before update)
        private void tileItem2_ItemClick(object sender, TileItemEventArgs e)
        {
            if (right.Text.Equals("admin"))
            {
                UpdateSupplier1 updateSupplier = new UpdateSupplier1(name.Text, right.Text);

                if (updateSupplier == null)
                {
                    this.Hide();
                    updateSupplier.Show();
                }
                else
                {
                    this.Hide();
                    updateSupplier.Show();
                    updateSupplier.Focus();
                }
            }
            else
            {
                MessageBox.Show("تلك الصلاحيه ليست مطابقه لمهامك الوظيفيه", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //All Suppliers Page
        private void tileItem4_ItemClick(object sender, TileItemEventArgs e)
        {
            AllSuppliers allSuppliers = new AllSuppliers(name.Text, right.Text);

            if (allSuppliers == null)
            {
                this.Hide();
                allSuppliers.Show();
            }
            else
            {
                this.Hide();
                allSuppliers.Show();
                allSuppliers.Focus();
            }
        }

        //All Supplier's Purchases Page 
        private void tileItem8_ItemClick(object sender, TileItemEventArgs e)
        {
            SupplierPurchases supplierPurchases = new SupplierPurchases(name.Text, right.Text);

            if (supplierPurchases == null)
            {
                this.Hide();
                supplierPurchases.Show();
            }
            else
            {
                this.Hide();
                supplierPurchases.Show();
                supplierPurchases.Focus();
            }
        }
    }
}