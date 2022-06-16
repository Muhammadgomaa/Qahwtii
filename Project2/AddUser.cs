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
    public partial class AddUser : DevExpress.XtraEditors.XtraForm
    {
        public AddUser(string x, string y)
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

        //Add User Button
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string username = uname.Text;
                string userpass = upass.Text;
                string userright = uright.Text;

                if (username.Equals("") || userpass.Equals("") || userright.Equals(""))
                {
                    MessageBox.Show("برجاء استكمال البيانات المطلوبه", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if(userpass.Length < 5)
                {
                    MessageBox.Show("برجاء ادخال كلمه مرور لا تقل عن 6 ارقام او حروف", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    List<String> usersname = new List<string>();

                    DataTable table = new DataTable();

                    SqlConnection CONN = new SqlConnection(DatabaseConnection.Connection);

                    SqlCommand command = new SqlCommand();

                    command.Connection = CONN;
                    command.CommandText = "select [User_Name] from Users";

                    CONN.Open();

                    table.Load(command.ExecuteReader());

                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        usersname.Add(table.Rows[i][0].ToString());
                    }

                    if (usersname.Contains(username))
                    {
                        MessageBox.Show("هذا المستخدم تم اضافته من قبل يرجى التأكد", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        DialogResult result;
                        result = MessageBox.Show("هل متأكد من اضافه مستخدم جديد", "قهوتى", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                        if (result == DialogResult.Yes)
                        {
                            SqlConnection CONN1 = new SqlConnection(DatabaseConnection.Connection);

                            SqlCommand command1 = new SqlCommand();

                            command1.Connection = CONN1;
                            command1.CommandText = "insert into Users values ('" + username + "','" + userpass + "','" + userright + "')";

                            CONN1.Open();

                            command1.ExecuteNonQuery();

                            MessageBox.Show("تم اضافه البيانات بنجاح", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            CONN.Close();

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