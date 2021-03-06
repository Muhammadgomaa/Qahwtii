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
    public partial class Setting : DevExpress.XtraEditors.XtraForm
    {
        public Setting(string x, string y)
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

        //Add User Page (Check rights before add)
        private void tileItem1_ItemClick(object sender, TileItemEventArgs e)
        {
            if (right.Text.Equals("admin"))
            {
                AddUser addUser = new AddUser(name.Text, right.Text);

                if (addUser == null)
                {
                    this.Hide();
                    addUser.Show();
                }
                else
                {
                    this.Hide();
                    addUser.Show();
                    addUser.Focus();
                }
            }
            else
            {
                MessageBox.Show("تلك الصلاحيه ليست مطابقه لمهامك الوظيفيه", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //All Users Page
        private void tileItem3_ItemClick(object sender, TileItemEventArgs e)
        {
            AllUsers allUsers = new AllUsers(name.Text, right.Text);

            if (allUsers == null)
            {
                this.Hide();
                allUsers.Show();
            }
            else
            {
                this.Hide();
                allUsers.Show();
                allUsers.Focus();
            }
        }

        //Delete User (Check rights before delete)
        private void tileItem2_ItemClick(object sender, TileItemEventArgs e)
        {
            if (right.Text.Equals("admin"))
            {
                DeleteUser deleteUser = new DeleteUser(name.Text, right.Text);

                if (deleteUser == null)
                {
                    this.Hide();
                    deleteUser.Show();
                }
                else
                {
                    this.Hide();
                    deleteUser.Show();
                    deleteUser.Focus();
                }
            }
            else
            {
                MessageBox.Show("تلك الصلاحيه ليست مطابقه لمهامك الوظيفيه", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Change Password (Check rights before update)
        private void tileItem4_ItemClick(object sender, TileItemEventArgs e)
        {
            if (right.Text.Equals("admin"))
            {
                ChangePassword1 changePassword = new ChangePassword1(name.Text, right.Text);

                if (changePassword == null)
                {
                    this.Hide();
                    changePassword.Show();
                }
                else
                {
                    this.Hide();
                    changePassword.Show();
                    changePassword.Focus();
                }
            }
            else
            {
                MessageBox.Show("تلك الصلاحيه ليست مطابقه لمهامك الوظيفيه", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}