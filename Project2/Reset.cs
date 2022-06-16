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
using System.Net;
using System.Net.Mail;

namespace Project2
{
    public partial class Reset : DevExpress.XtraEditors.XtraForm
    {
        public Reset()
        {
            InitializeComponent();
        }

        private void logout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("شكرا لاستخدامكم البرنامج", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.None);
            Environment.Exit(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string username = name.Text;
                string usermail = email.Text;

                if (username.Equals("") || usermail.Equals(""))
                {
                    MessageBox.Show("برجاءاستكمال البيانات المطلوبه", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    List<String> usernames = new List<string>();
                    List<String> passwords = new List<string>();

                    DataTable table1 = new DataTable();
                    DataTable table2 = new DataTable();

                    SqlConnection CONN = new SqlConnection(DatabaseConnection.Connection);
                    SqlConnection CONN1 = new SqlConnection(DatabaseConnection.Connection);

                    SqlCommand command1 = new SqlCommand();
                    SqlCommand command2 = new SqlCommand();

                    command1.Connection = CONN;
                    command1.CommandText = "select [User_Name] from Users";

                    command2.Connection = CONN1;
                    command2.CommandText = "select [User_Password] from Users";


                    CONN.Open();
                    CONN1.Open();

                    table1.Load(command1.ExecuteReader());
                    table2.Load(command2.ExecuteReader());

                    for (int i = 0; i < table1.Rows.Count; i++)
                    {
                        usernames.Add(table1.Rows[i][0].ToString());
                        passwords.Add(table2.Rows[i][0].ToString());
                    }

                    if (usernames.Contains(username))
                    {

                        int x = usernames.IndexOf(username);
                        string pass = passwords[x];

                        MailMessage mail = new MailMessage();
                        mail.From = new System.Net.Mail.MailAddress("gomaam2711@gmail.com");
                        SmtpClient smtp = new SmtpClient();
                        smtp.Port = 587;
                        smtp.EnableSsl = true;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(mail.From.Address, "Tsongaosa2020");
                        smtp.Host = "smtp.gmail.com";
                        //recipient
                        mail.To.Add(new MailAddress(usermail));
                        mail.IsBodyHtml = false;
                        mail.Subject = "Reset Password";
                        string st = "Hello" + "[" + username + "]" + "\n"
                            + " You recently requested to reset the password for your account and the password is " + "(" + pass + ")" + "\n"
                            + "Thanks ," + "\n"
                            + "Best Regards ," + "\n"
                            + "Muhammad Gomaa ©";
                        mail.Body = st;
                        smtp.Send(mail);

                        MessageBox.Show("تم استعاده كلمه المرور عبر البريد الالكترونى", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        Application.OpenForms[0].Show();
                        this.Close();

                    }

                    else
                    {
                        MessageBox.Show("اسم المستخدم غير موجود يرجى التأكد اولا من اسم المستخدم", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("برجاءاستكمال البيانات المطلوبه", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Login login = new Login();

            if (login == null)
            {
                this.Hide();
                login.Show();
            }
            else
            {
                this.Hide();
                login.Show();
                login.Focus();
            }
        }
    }
}