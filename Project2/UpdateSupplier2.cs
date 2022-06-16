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
    public partial class UpdateSupplier2 : DevExpress.XtraEditors.XtraForm
    {
        public UpdateSupplier2(string x, string y, string i)
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

        //Preview Supplier Information
        private void UpdateSupplier2_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable table = new DataTable();

                SqlConnection CONN = new SqlConnection(DatabaseConnection.Connection);

                SqlCommand command = new SqlCommand();

                command.Connection = CONN;
                command.CommandText = "select * from Suppliers where Supp_ID= '" + id.Text + "' ";

                CONN.Open();
                table.Load(command.ExecuteReader());

                suppname.Text = table.Rows[0][1].ToString();
                suppphone.Text = table.Rows[0][2].ToString();

                CONN.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("برجاء استكمال البيانات المطلوبه", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Update Supplier Information
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string supname = suppname.Text;
                string supphone = suppphone.Text;

                if (supname.Equals("") || supphone.Equals("") || supphone.Length != 11)
                {
                    MessageBox.Show("برجاء استكمال البيانات المطلوبه", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    List<String> suppliersphone = new List<string>();

                    DataTable table1 = new DataTable();

                    SqlConnection CONN1 = new SqlConnection(DatabaseConnection.Connection);

                    SqlCommand command1 = new SqlCommand();

                    command1.Connection = CONN1;
                    command1.CommandText = "select [Supp_Phone] from Suppliers";

                    CONN1.Open();

                    table1.Load(command1.ExecuteReader());

                    for (int i = 0; i < table1.Rows.Count; i++)
                    {
                        suppliersphone.Add(table1.Rows[i][0].ToString());
                    }

                    if (suppliersphone.Contains(supphone))
                    {
                        MessageBox.Show("رقم الهاتف الذى ادخلته لدى عميل اخر يرجى التأكد", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        DialogResult result;
                        result = MessageBox.Show("هل متأكد من تعديل بيانات مورد", "قهوتى", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                        if (result == DialogResult.Yes)
                        {
                            SqlConnection CONN2 = new SqlConnection(DatabaseConnection.Connection);

                            SqlCommand command2 = new SqlCommand();

                            command2.Connection = CONN2;
                            command2.CommandText = "update Suppliers set Supp_Name='" + supname + "' , Supp_Phone='" + supphone + "' where Supp_ID='" + id.Text + "'";

                            CONN2.Open();

                            command2.ExecuteNonQuery();

                            MessageBox.Show("تم تعديل البيانات بنجاح", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            CONN2.Close();

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