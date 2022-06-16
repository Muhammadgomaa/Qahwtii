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
    public partial class UpdateCustomer1 : DevExpress.XtraEditors.XtraForm
    {
        public UpdateCustomer1(string x, string y)
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

        //Customer Page
        private void button2_Click(object sender, EventArgs e)
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

        //Preview Customers Information
        private void UpdateCustomer1_Load(object sender, EventArgs e)
        {
                DataTable table1 = new DataTable();

                SqlConnection CONN1 = new SqlConnection(DatabaseConnection.Connection);

                SqlCommand command1 = new SqlCommand();

                command1.Connection = CONN1;
                command1.CommandText = "select [Cus_ID] as 'كود العميل',[Cus_Name] as 'اسم العميل', [Cus_Phone] as 'رقم العميل' from Customers";

                dataGridView1.DataSource = table1;

                CONN1.Open();
                table1.Load(command1.ExecuteReader());

                CONN1.Close();
        }

        //Choose Customer to update info.
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string ind = dataGridView1.CurrentCell.Value.ToString();

                DialogResult result;
                result = MessageBox.Show("هل متأكد من تعديل بيانات العميل", "قهوتى", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result == DialogResult.Yes)
                {
                    UpdateCustomer2 updateCustomer = new UpdateCustomer2(name.Text, right.Text, ind);

                    if (updateCustomer == null)
                    {
                        this.Hide();
                        updateCustomer.Show();
                    }
                    else
                    {
                        this.Hide();
                        updateCustomer.Show();
                        updateCustomer.Focus();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("برجاء استكمال البيانات المطلوبه", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}