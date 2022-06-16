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
    public partial class Invoice : DevExpress.XtraEditors.XtraForm
    {
        public Invoice(string x, string y)
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

        //Get All Invoices Numbers in DB
        private void Invoice_Load(object sender, EventArgs e)
        {
            List<String> Invoices_Num = new List<string>();

            DataTable table = new DataTable();

            SqlConnection CONN = new SqlConnection(DatabaseConnection.Connection);
            SqlCommand command = new SqlCommand();

            command.Connection = CONN;
            command.CommandText = "select [Invo_Num] from Invoices";

            CONN.Open();

            table.Load(command.ExecuteReader());

            for (int i = 0; i < table.Rows.Count; i++)
            {
                Invoices_Num.Add(table.Rows[i][0].ToString());
                invonum.Items.Add(Invoices_Num[i]);
            }
        }

        //Preview all invoice information based on invoice number and calculate the total invoice price
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string invoNum = invonum.Text;

                if (invoNum.Equals(""))
                {
                    MessageBox.Show("برجاء استكمال البيانات المطلوبه", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DataTable table1 = new DataTable();

                    SqlConnection CONN1 = new SqlConnection(DatabaseConnection.Connection);
                    SqlCommand command1 = new SqlCommand();

                    command1.Connection = CONN1;
                    command1.CommandText = "select [Cus_Name] as 'اسم العميل',[Prod_Code] as 'كود المنتج',[Prod_Name] as 'اسم المنتح',[Quantity] as 'الكميه',[Price] as 'السعر' , [Date] as 'التاريخ' , [Time] as 'الوقت' from Sales where Invo_Num = '" + invoNum + "' ";

                    dataGridView1.DataSource = table1;

                    CONN1.Open();
                    table1.Load(command1.ExecuteReader());

                    CONN1.Close();

                    //____________________________________________________________________

                    float t = 0;

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        t += float.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
                    }

                    totalinvo.Text = t.ToString();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("برجاء استكمال البيانات المطلوبه", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Sales Page
        private void button2_Click(object sender, EventArgs e)
        {
            Sales sales = new Sales(name.Text, right.Text);

            if (sales == null)
            {
                this.Hide();
                sales.Show();
            }
            else
            {
                this.Hide();
                sales.Show();
                sales.Focus();
            }
        }
    }
}