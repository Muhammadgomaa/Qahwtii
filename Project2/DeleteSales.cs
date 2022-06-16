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
    public partial class DeleteSales : DevExpress.XtraEditors.XtraForm
    {
        public DeleteSales(string x, string y)
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

        //Get all invoices sales in DB
        private void DeleteSales_Load(object sender, EventArgs e)
        {
            DataTable table = new DataTable();

            SqlConnection CONN = new SqlConnection(DatabaseConnection.Connection);

            SqlCommand command = new SqlCommand();

            command.Connection = CONN;
            command.CommandText = "Select [Invo_Num] as 'رقم الفاتوره' , [Invo_TotalPrice] as 'اجمالى سعر الفاتوره' from Invoices";

            dataGridView1.DataSource = table;

            CONN.Open();
            table.Load(command.ExecuteReader());

            CONN.Close();
        }

        //Delete Sales info. from sales and invoice tables and update quantity
        public void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string ind = dataGridView1.CurrentCell.Value.ToString();

                DialogResult result;
                result = MessageBox.Show("هل متأكد من مسح عمليه البيع", "قهوتى", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result == DialogResult.Yes)
                {

                    List<String> Products_Code = new List<string>();
                    List<String> Products_Quantity = new List<string>();

                    DataTable table5 = new DataTable();
                    DataTable table6 = new DataTable();

                    SqlConnection CONN5 = new SqlConnection(DatabaseConnection.Connection);
                    SqlCommand command5 = new SqlCommand();

                    SqlConnection CONN6 = new SqlConnection(DatabaseConnection.Connection);
                    SqlCommand command6 = new SqlCommand();

                    command5.Connection = CONN5;
                    command5.CommandText = "select [Prod_Code] from Sales where Invo_Num =' " +ind+ " ' ";

                    command6.Connection = CONN6;
                    command6.CommandText = "select [Quantity] from Sales where Invo_Num =' " + ind + " ' ";

                    CONN5.Open();
                    CONN6.Open();

                    table5.Load(command5.ExecuteReader());
                    table6.Load(command6.ExecuteReader());

                    for (int i = 0; i < table5.Rows.Count; i++)
                    {
                        Products_Code.Add(table5.Rows[i][0].ToString());
                    }

                    for (int i = 0; i < table6.Rows.Count; i++)
                    {
                        Products_Quantity.Add(table6.Rows[i][0].ToString());
                    }

                    for (int i = 0; i < table5.Rows.Count; i++)
                    {
                        DataTable table7 = new DataTable();

                        SqlConnection CONN7 = new SqlConnection(DatabaseConnection.Connection);
                        SqlCommand command7 = new SqlCommand();

                        command7.Connection = CONN7;
                        command7.CommandText = "select [Available_Quantity] from Purchases where Prod_Code= '" + Products_Code[i] + "' ";

                        CONN7.Open();
                        table7.Load(command7.ExecuteReader());

                        string Available_Quan = table7.Rows[0][0].ToString();
                        string ProdQuan = Products_Quantity[i];

                        float Update_Quan = float.Parse(Available_Quan) + float.Parse(ProdQuan);
                        CONN7.Close();

                        //_________________________________________________________________

                        SqlConnection CONN8 = new SqlConnection(DatabaseConnection.Connection);
                        SqlCommand command8 = new SqlCommand();

                        command8.Connection = CONN8;
                        command8.CommandText = "update Purchases set Available_Quantity='" + Update_Quan + "' where Prod_Code= '" + Products_Code[i] + "'";

                        CONN8.Open();
                        command8.ExecuteNonQuery();

                        //_________________________________________________________________
                    }

                    SqlConnection CONN1 = new SqlConnection(DatabaseConnection.Connection);

                    SqlCommand command1 = new SqlCommand();

                    command1.Connection = CONN1;

                    command1.CommandText = "delete from Sales where Invo_Num = '" + ind + "' ";

                    CONN1.Open();

                    command1.ExecuteNonQuery();

                    CONN1.Close();

                    //__________________________________________________________________________

                    SqlConnection CONN2 = new SqlConnection(DatabaseConnection.Connection);

                    SqlCommand command2 = new SqlCommand();

                    command2.Connection = CONN2;

                    command2.CommandText = "delete from Invoices where Invo_Num = '" + ind + "' ";

                    CONN2.Open();

                    command2.ExecuteNonQuery();

                    CONN2.Close();

                    MessageBox.Show("تم مسح البيانات بنجاح", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Information);

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