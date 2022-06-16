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
    public partial class UsersReport : DevExpress.XtraEditors.XtraForm
    {
        public UsersReport(string x, string y)
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

        //Report Page
        private void button2_Click(object sender, EventArgs e)
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

        //Get Num. of Users , Preview all Users Information
        private void UsersReport_Load(object sender, EventArgs e)
        {
            try
            {
                List<String> numofusers = new List<string>();

                DataTable table = new DataTable();

                SqlConnection CONN = new SqlConnection(DatabaseConnection.Connection);
                SqlCommand command = new SqlCommand();

                command.Connection = CONN;
                command.CommandText = "select [User_ID] from Users";

                CONN.Open();

                table.Load(command.ExecuteReader());

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    numofusers.Add(table.Rows[i][0].ToString());
                }

                total.Text = numofusers.Count.ToString();

                //___________________________________________________________________________

                DataTable table1 = new DataTable();

                SqlConnection CONN1 = new SqlConnection(DatabaseConnection.Connection);

                SqlCommand command1 = new SqlCommand();

                command1.Connection = CONN1;
                command1.CommandText = "select [User_Name] as 'اسم المستخدم' , [User_Rights] as 'صلاحيه المستخدم' from Users";

                dataGridView1.DataSource = table1;

                CONN1.Open();
                table1.Load(command1.ExecuteReader());

                CONN1.Close();

            }
            catch (Exception)
            {
                MessageBox.Show("برجاء استكمال البيانات المطلوبه", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}