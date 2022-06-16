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
    public partial class Login : DevExpress.XtraEditors.XtraForm
    {
        public Login()
        {
            InitializeComponent();
        }

        //Login Button
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string username = name.Text;
                string userpass = pass.Text;
                string userright;

                if(username.Equals("") || userpass.Equals(""))
                {
                    MessageBox.Show("برجاء استكمال البيانات المطلوبه", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    List<String> usernames = new List<string>();
                    List<String> passwords = new List<string>();
                    List<String> rights = new List<string>();

                    DataTable table1 = new DataTable();
                    DataTable table2 = new DataTable();
                    DataTable table3 = new DataTable();

                    SqlConnection CONN = new SqlConnection(DatabaseConnection.Connection);

                    SqlCommand command1 = new SqlCommand();
                    SqlCommand command2 = new SqlCommand();
                    SqlCommand command3 = new SqlCommand();

                    command1.Connection = CONN;
                    command1.CommandText = "select [User_Name] from Users";

                    command2.Connection = CONN;
                    command2.CommandText = "select [User_Password] from Users";

                    command3.Connection = CONN;
                    command3.CommandText = "select [User_Rights] from Users";

                    CONN.Open();

                    table1.Load(command1.ExecuteReader());
                    table2.Load(command2.ExecuteReader());
                    table3.Load(command3.ExecuteReader());

                    for (int i = 0; i < table1.Rows.Count; i++)
                    {
                        usernames.Add(table1.Rows[i][0].ToString());
                        passwords.Add(table2.Rows[i][0].ToString());
                        rights.Add(table3.Rows[i][0].ToString());
                    }

                    if (usernames.Contains(username) && passwords[usernames.IndexOf(username)].Equals(userpass))
                    {
                        MessageBox.Show("مرحبا بك", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        userright=rights[usernames.IndexOf(username)];

                        Main main = new Main(username,userright);

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
                    else
                    {
                        MessageBox.Show("خطأ فى اسم المستخدم او كلمه المرور", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                   CONN.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("خطأ فى اسم المستخدم او كلمه المرور", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Logout Button
        private void logout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("شكرا لاستخدامكم البرنامج", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.None);
            Environment.Exit(0);
        }

        //Must be Admin to Reset Password and username field not empty
        private void label3_Click(object sender, EventArgs e)
        {
            try
            {
                string username = name.Text;
                string userright;

                if (username.Equals(""))
                {
                    MessageBox.Show("برجاء استكمال البيانات المطلوبه", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    List<String> usernames = new List<string>();
                    List<String> rights = new List<string>();

                    DataTable table1 = new DataTable();
                    DataTable table2 = new DataTable();

                    SqlConnection CONN = new SqlConnection(DatabaseConnection.Connection);

                    SqlCommand command1 = new SqlCommand();
                    SqlCommand command2 = new SqlCommand();

                    command1.Connection = CONN;
                    command1.CommandText = "select [User_Name] from Users";

                    command2.Connection = CONN;
                    command2.CommandText = "select [User_Rights] from Users";

                    CONN.Open();

                    table1.Load(command1.ExecuteReader());
                    table2.Load(command2.ExecuteReader());

                    for (int i = 0; i < table1.Rows.Count; i++)
                    {
                        usernames.Add(table1.Rows[i][0].ToString());
                        rights.Add(table2.Rows[i][0].ToString());
                    }
                    userright = rights[usernames.IndexOf(username)];

                    if (userright.Equals("admin"))
                    {
                        Reset reset = new Reset();

                        if (reset == null)
                        {
                            this.Hide();
                            reset.Show();
                        }
                        else
                        {
                            this.Hide();
                            reset.Show();
                            reset.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("لا يمكن استعاده كلمه المرور الرجاء العوده للمختصين", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    CONN.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("خطأ فى اسم المستخدم او كلمه المرور", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}