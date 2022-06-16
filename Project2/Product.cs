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
    public partial class Product : DevExpress.XtraEditors.XtraForm
    {
        public Product(string x, string y)
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

        //Home Page
        private void home_button_Click(object sender, EventArgs e)
        {
            home_Click(sender, e);
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

        //Add Product Page (Check rights before add)
        private void tileItem1_ItemClick(object sender, TileItemEventArgs e)
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

        //Delete Product Page (Check rights before add)
        private void tileItem3_ItemClick(object sender, TileItemEventArgs e)
        {
            if (right.Text.Equals("admin"))
            {
                DeleteProduct deleteProduct = new DeleteProduct(name.Text, right.Text);

                if (deleteProduct == null)
                {
                    this.Hide();
                    deleteProduct.Show();
                }
                else
                {
                    this.Hide();
                    deleteProduct.Show();
                    deleteProduct.Focus();
                }
            }
            else
            {
                MessageBox.Show("تلك الصلاحيه ليست مطابقه لمهامك الوظيفيه", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Update Product Page (Check rights before add)
        private void tileItem2_ItemClick(object sender, TileItemEventArgs e)
        {
            if (right.Text.Equals("admin"))
            {
                UpdateProduct1 updateProduct = new UpdateProduct1(name.Text, right.Text);

                if (updateProduct == null)
                {
                    this.Hide();
                    updateProduct.Show();
                }
                else
                {
                    this.Hide();
                    updateProduct.Show();
                    updateProduct.Focus();
                }
            }
            else
            {
                MessageBox.Show("تلك الصلاحيه ليست مطابقه لمهامك الوظيفيه", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //All Products Page
        private void tileItem4_ItemClick(object sender, TileItemEventArgs e)
        {
            AllProducts allProducts = new AllProducts(name.Text, right.Text);

            if (allProducts == null)
            {
                this.Hide();
                allProducts.Show();
            }
            else
            {
                this.Hide();
                allProducts.Show();
                allProducts.Focus();
            }
        }
    }
}