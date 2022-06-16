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
    public partial class AddSupplier : DevExpress.XtraEditors.XtraForm
    {
        public AddSupplier(string x, string y)
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

        //Add Supplier Button
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string supname = suppname.Text;
                string supphone = suppphone.Text;

                if(supname.Equals("") || supphone.Equals("") || supphone.Length!=11)
                {
                    MessageBox.Show("برجاء استكمال البيانات المطلوبه", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    List<String> suppliersphone = new List<string>();

                    DataTable table = new DataTable();

                    SqlConnection CONN = new SqlConnection(DatabaseConnection.Connection);

                    SqlCommand command = new SqlCommand();

                    command.Connection = CONN;
                    command.CommandText = "select [Supp_Phone] from Suppliers";

                    CONN.Open();

                    table.Load(command.ExecuteReader());

                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        suppliersphone.Add(table.Rows[i][0].ToString());
                    }

                    if (suppliersphone.Contains(supphone))
                    {
                        MessageBox.Show("هذا المورد تم اضافته من قبل يرجى التأكد", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        DialogResult result;
                        result = MessageBox.Show("هل متأكد من اضافه مورد جديد", "قهوتى", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                        if (result == DialogResult.Yes)
                        {
                            SqlConnection CONN1 = new SqlConnection(DatabaseConnection.Connection);

                            SqlCommand command1 = new SqlCommand();

                            command1.Connection = CONN1;
                            command1.CommandText = "insert into Suppliers values ('" + supname + "','" + supphone + "')";

                            CONN1.Open();

                            command1.ExecuteNonQuery();

                            MessageBox.Show("تم اضافه البيانات بنجاح", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            CONN.Close();

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