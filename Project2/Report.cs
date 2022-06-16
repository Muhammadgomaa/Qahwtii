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
    public partial class Report : DevExpress.XtraEditors.XtraForm
    {
        public Report(string x, string y)
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

        //Sales Page
        private void sales_menu_Click_1(object sender, EventArgs e)
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

        //Setting Page
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

        //Customers Report
        private void tileItem1_ItemClick(object sender, TileItemEventArgs e)
        {
            CustomerReport customerReport = new CustomerReport(name.Text, right.Text);

            if (customerReport == null)
            {
                this.Hide();
                customerReport.Show();
            }
            else
            {
                this.Hide();
                customerReport.Show();
                customerReport.Focus();
            }
        }

        //Products Reoprt
        private void tileItem2_ItemClick(object sender, TileItemEventArgs e)
        {
            ProductReport productReport = new ProductReport(name.Text, right.Text);

            if (productReport == null)
            {
                this.Hide();
                productReport.Show();
            }
            else
            {
                this.Hide();
                productReport.Show();
                productReport.Focus();
            }
        }

        //Suppliers Report
        private void tileItem3_ItemClick(object sender, TileItemEventArgs e)
        {
            SupplierReport supplierReport = new SupplierReport(name.Text, right.Text);

            if (supplierReport == null)
            {
                this.Hide();
                supplierReport.Show();
            }
            else
            {
                this.Hide();
                supplierReport.Show();
                supplierReport.Focus();
            }
        }

        //Purchases Report
        private void tileItem4_ItemClick(object sender, TileItemEventArgs e)
        {
            PurchaseReport purchaseReport = new PurchaseReport(name.Text, right.Text);

            if (purchaseReport == null)
            {
                this.Hide();
                purchaseReport.Show();
            }
            else
            {
                this.Hide();
                purchaseReport.Show();
                purchaseReport.Focus();
            }
        }

        //Sales Report
        private void tileItem5_ItemClick(object sender, TileItemEventArgs e)
        {
            SalesReport salesReport = new SalesReport(name.Text, right.Text);

            if (salesReport == null)
            {
                this.Hide();
                salesReport.Show();
            }
            else
            {
                this.Hide();
                salesReport.Show();
                salesReport.Focus();
            }
        }

        //Daily Sales Report
        private void tileItem6_ItemClick(object sender, TileItemEventArgs e)
        {
            DailySalesReport dailySalesReport = new DailySalesReport(name.Text, right.Text);

            if (dailySalesReport == null)
            {
                this.Hide();
                dailySalesReport.Show();
            }
            else
            {
                this.Hide();
                dailySalesReport.Show();
                dailySalesReport.Focus();
            }
        }

        //Users Report
        private void tileItem7_ItemClick(object sender, TileItemEventArgs e)
        {
            UsersReport usersReport = new UsersReport(name.Text, right.Text);

            if (usersReport == null)
            {
                this.Hide();
                usersReport.Show();
            }
            else
            {
                this.Hide();
                usersReport.Show();
                usersReport.Focus();
            }
        }

        //Inventory Report
        private void tileItem8_ItemClick(object sender, TileItemEventArgs e)
        {
            InventoryReport inventoryReport = new InventoryReport(name.Text, right.Text);

            if (inventoryReport == null)
            {
                this.Hide();
                inventoryReport.Show();
            }
            else
            {
                this.Hide();
                inventoryReport.Show();
                inventoryReport.Focus();
            }
        }
    }
}