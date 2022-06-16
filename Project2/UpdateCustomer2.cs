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
    public partial class UpdateCustomer2 : DevExpress.XtraEditors.XtraForm
    {
        public UpdateCustomer2(string x, string y, string i)
        {
            InitializeComponent();
            name.Text = x;
            right.Text = y;
            id.Text = i;
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

        //Preview Customer Information
        private void UpdateCustomer2_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable table = new DataTable();

                SqlConnection CONN = new SqlConnection(DatabaseConnection.Connection);

                SqlCommand command = new SqlCommand();

                command.Connection = CONN;
                command.CommandText = "select * from Customers where Cus_ID= '" + id.Text + "' ";

                CONN.Open();
                table.Load(command.ExecuteReader());

                cusname.Text = table.Rows[0][1].ToString();
                cusphone.Text = table.Rows[0][2].ToString();

                CONN.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("برجاء استكمال البيانات المطلوبه", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Update Customer Button
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string cname = cusname.Text;
                string cphone = cusphone.Text;

                if (cname.Equals("") || cphone.Equals("") || cphone.Length != 11)
                {
                    MessageBox.Show("برجاء استكمال البيانات المطلوبه", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    List<String> customersphone = new List<string>();

                    DataTable table1 = new DataTable();

                    SqlConnection CONN1 = new SqlConnection(DatabaseConnection.Connection);

                    SqlCommand command1 = new SqlCommand();

                    command1.Connection = CONN1;
                    command1.CommandText = "select [Cus_Phone] from Customers";

                    CONN1.Open();

                    table1.Load(command1.ExecuteReader());

                    for (int i = 0; i < table1.Rows.Count; i++)
                    {
                        customersphone.Add(table1.Rows[i][0].ToString());
                    }

                    if (customersphone.Contains(cphone))
                    {
                        MessageBox.Show("رقم الهاتف الذى ادخلته لدى عميل اخر يرجى التأكد", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        DialogResult result;
                        result = MessageBox.Show("هل متأكد من تعديل بيانات العميل", "قهوتى", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                        if (result == DialogResult.Yes)
                        {
                            SqlConnection CONN2 = new SqlConnection(DatabaseConnection.Connection);

                            SqlCommand command2 = new SqlCommand();

                            command2.Connection = CONN2;
                            command2.CommandText = "update Customers set Cus_Name='" + cname + "' , Cus_Phone='" + cphone + "' where Cus_ID='" + id.Text + "'";

                            CONN2.Open();

                            command2.ExecuteNonQuery();

                            MessageBox.Show("تم تعدبل البيانات بنجاح", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            CONN2.Close();

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