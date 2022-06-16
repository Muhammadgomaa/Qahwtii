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
    public partial class ChangePassword1 : DevExpress.XtraEditors.XtraForm
    {
        public ChangePassword1(string x, string y)
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

        //Setting Page
        private void button2_Click(object sender, EventArgs e)
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

        //Preview Users Information
        private void ChangePassword1_Load(object sender, EventArgs e)
        {
            DataTable table1 = new DataTable();

            SqlConnection CONN1 = new SqlConnection(DatabaseConnection.Connection);

            SqlCommand command1 = new SqlCommand();

            command1.Connection = CONN1;
            command1.CommandText = "select [User_ID] as 'كود المستخدم',[User_Name] as 'اسم المستخدم' from Users";

            dataGridView1.DataSource = table1;

            CONN1.Open();
            table1.Load(command1.ExecuteReader());

            CONN1.Close();
        }

        //Choose User to update info.
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string ind = dataGridView1.CurrentCell.Value.ToString();

                DialogResult result;
                result = MessageBox.Show("هل متأكد من تغير كلمه مرور المستخدم", "قهوتى", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result == DialogResult.Yes)
                {
                    ChangePassword2 changePassword = new ChangePassword2(name.Text, right.Text, ind);

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
            }
            catch (Exception)
            {
                MessageBox.Show("برجاء استكمال البيانات المطلوبه", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}