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
    public partial class ChangePassword2 : DevExpress.XtraEditors.XtraForm
    {
        public ChangePassword2(string x, string y, string i)
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

        //Update User Button
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string username = uname.Text;
                string userpass = upass.Text;

                if (username.Equals("") || userpass.Equals(""))
                {
                    MessageBox.Show("برجاء استكمال البيانات المطلوبه", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (userpass.Length < 5)
                {
                    MessageBox.Show("برجاء ادخال كلمه مرور لا تقل عن 6 ارقام او حروف", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result;
                    result = MessageBox.Show("هل متأكد من تغير كلمه المرور", "قهوتى", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (result == DialogResult.Yes)
                    {
                        SqlConnection CONN2 = new SqlConnection(DatabaseConnection.Connection);

                        SqlCommand command2 = new SqlCommand();

                        command2.Connection = CONN2;
                        command2.CommandText = "update Users set User_Password='" + userpass + "'  where User_ID='" + id.Text + "'";

                        CONN2.Open();

                        command2.ExecuteNonQuery();

                        MessageBox.Show("تم تغير كلمه المرور بنجاح", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        CONN2.Close();

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
            catch (Exception)
            {
                MessageBox.Show("برجاء استكمال البيانات المطلوبه", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Preview User Information
        private void ChangePassword2_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable table = new DataTable();

                SqlConnection CONN = new SqlConnection(DatabaseConnection.Connection);

                SqlCommand command = new SqlCommand();

                command.Connection = CONN;
                command.CommandText = "select * from Users where User_ID= '" + id.Text + "' ";

                CONN.Open();
                table.Load(command.ExecuteReader());

                uname.Text = table.Rows[0][1].ToString();

                CONN.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("برجاء استكمال البيانات المطلوبه", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}