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
    public partial class DeleteProduct : DevExpress.XtraEditors.XtraForm
    {
        public DeleteProduct(string x, string y)
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

        //Product Page
        private void button2_Click(object sender, EventArgs e)
        {
            Product product = new Product(name.Text, right.Text);

            if (product == null)
            {
                this.Hide();
                product.Show();
            }
            else
            {
                this.Hide();
                product.Show();
                product.Focus();
            }
        }

        //Preview Products Information
        private void DeleteProduct_Load(object sender, EventArgs e)
        {
            DataTable table = new DataTable();

            SqlConnection CONN = new SqlConnection(DatabaseConnection.Connection);

            SqlCommand command = new SqlCommand();

            command.Connection = CONN;
            command.CommandText = "select [Prod_Code] as 'كود المنتج', [Prod_Name] as 'اسم المنتج' , [Supp_Name] as 'اسم المورد' ,[Purch_Buy] as 'سعر الشراء | التكلفه', [Purch_Sell] as 'سعر البيع', [Purch_Quantity] as 'الكميه' from Purchases";

            dataGridView1.DataSource = table;

            CONN.Open();
            table.Load(command.ExecuteReader());

            CONN.Close();
        }

        //Delete Product Information form Products and Purchases Tables
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string ind = dataGridView1.CurrentCell.Value.ToString();

                DialogResult result;
                result = MessageBox.Show("هل متأكد من مسح المنتج", "قهوتى", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result == DialogResult.Yes)
                {
                    SqlConnection CONN1 = new SqlConnection(DatabaseConnection.Connection);

                    SqlCommand command1 = new SqlCommand();

                    command1.Connection = CONN1;

                    command1.CommandText = "delete from Purchases where Prod_Code = '" + ind + "' ";

                    CONN1.Open();

                    command1.ExecuteNonQuery();

                    MessageBox.Show("تم مسح البيانات بنجاح", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   
                    CONN1.Close();

                    Product product = new Product(name.Text, right.Text);

                    if (product == null)
                    {
                        this.Hide();
                        product.Show();
                    }
                    else
                    {
                        this.Hide();
                        product.Show();
                        product.Focus();
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