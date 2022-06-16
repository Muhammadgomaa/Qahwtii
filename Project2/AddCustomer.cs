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
    public partial class AddCustomer : DevExpress.XtraEditors.XtraForm
    {
        public AddCustomer(string x, string y)
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

        //Add Customer Button
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string cname = cusname.Text;
                string cphone = cusphone.Text;

                if(cname.Equals("") || cphone.Equals("") || cphone.Length != 11)
                {
                    MessageBox.Show("برجاء استكمال البيانات المطلوبه", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    List<String> customersphone = new List<string>();

                    DataTable table = new DataTable();

                    SqlConnection CONN = new SqlConnection(DatabaseConnection.Connection);

                    SqlCommand command = new SqlCommand();

                    command.Connection = CONN;
                    command.CommandText = "select [Cus_Phone] from Customers";

                    CONN.Open();

                    table.Load(command.ExecuteReader());

                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        customersphone.Add(table.Rows[i][0].ToString());
                    }

                    if (customersphone.Contains(cphone))
                    {
                        MessageBox.Show("هذا العميل تم اضافته من قبل يرجى التأكد", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        DialogResult result;
                        result = MessageBox.Show("هل متأكد من اضافه عميل جديد", "قهوتى", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                        if (result == DialogResult.Yes)
                        {
                            SqlConnection CONN1 = new SqlConnection(DatabaseConnection.Connection);

                            SqlCommand command1 = new SqlCommand();

                            command1.Connection = CONN1;
                            command1.CommandText = "insert into Customers values ('" + cname + "','" + cphone + "')";

                            CONN1.Open();

                            command1.ExecuteNonQuery();

                            MessageBox.Show("تم اضافه البيانات بنجاح", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            CONN.Close();

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