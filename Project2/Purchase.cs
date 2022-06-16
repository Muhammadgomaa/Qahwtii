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
    public partial class Purchase : DevExpress.XtraEditors.XtraForm
    {
        public Purchase(string x, string y)
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

        //Sipplier Page
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

        //Home Page
        private void home_menu_Click(object sender, EventArgs e)
        {
            home_Click(sender, e);
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

        //Add Purchase Page (Check right before add)
        private void tileItem1_ItemClick(object sender, TileItemEventArgs e)
        {
            if (right.Text.Equals("admin"))
            {
                AddPurchase addPurchase = new AddPurchase(name.Text, right.Text);

                if (addPurchase == null)
                {
                    this.Hide();
                    addPurchase.Show();
                }
                else
                {
                    this.Hide();
                    addPurchase.Show();
                    addPurchase.Focus();
                }
            }
            else
            {
                MessageBox.Show("تلك الصلاحيه ليست مطابقه لمهامك الوظيفيه", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Delete Purchase Page (Check right before delete)
        private void tileItem3_ItemClick(object sender, TileItemEventArgs e)
        {
            if (right.Text.Equals("admin"))
            {
                DeletePurchases deletePurchases = new DeletePurchases(name.Text, right.Text);

                if (deletePurchases == null)
                {
                    this.Hide();
                    deletePurchases.Show();
                }
                else
                {
                    this.Hide();
                    deletePurchases.Show();
                    deletePurchases.Focus();
                }
            }
            else
            {
                MessageBox.Show("تلك الصلاحيه ليست مطابقه لمهامك الوظيفيه", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Update Purhcase Page (Check right before update)
        private void tileItem2_ItemClick(object sender, TileItemEventArgs e)
        {
            if (right.Text.Equals("admin"))
            {
                UpdatePurchases1 updatePurchases1 = new UpdatePurchases1(name.Text, right.Text);

                if (updatePurchases1 == null)
                {
                    this.Hide();
                    updatePurchases1.Show();
                }
                else
                {
                    this.Hide();
                    updatePurchases1.Show();
                    updatePurchases1.Focus();
                }
            }
            else
            {
                MessageBox.Show("تلك الصلاحيه ليست مطابقه لمهامك الوظيفيه", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //All Purchases Page
        private void tileItem4_ItemClick(object sender, TileItemEventArgs e)
        {
            AllPurchases allPurchases = new AllPurchases(name.Text, right.Text);

            if (allPurchases == null)
            {
                this.Hide();
                allPurchases.Show();
            }
            else
            {
                this.Hide();
                allPurchases.Show();
                allPurchases.Focus();
            }
        }
    }
}