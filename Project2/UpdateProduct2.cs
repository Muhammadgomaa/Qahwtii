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
    public partial class UpdateProduct2 : DevExpress.XtraEditors.XtraForm
    {
        public UpdateProduct2(string x, string y, string i)
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

        //Preview Product Information
        private void UpdateProduct2_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable table = new DataTable();

                SqlConnection CONN = new SqlConnection(DatabaseConnection.Connection);

                SqlCommand command = new SqlCommand();

                command.Connection = CONN;
                command.CommandText = "select * from Products where Prod_Code= '" + id.Text + "' ";

                CONN.Open();
                table.Load(command.ExecuteReader());

                prodname.Text = table.Rows[0][1].ToString();
                prodprice.Text = table.Rows[0][2].ToString();

                CONN.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("برجاء استكمال البيانات المطلوبه", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Update Product Button
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string proname = prodname.Text;
                string proprice = prodprice.Value.ToString();

                if (proname.Equals("") || proprice.Equals("0.00"))
                {
                    MessageBox.Show("برجاء استكمال البيانات المطلوبه", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result;
                    result = MessageBox.Show("هل متأكد من تعديل بيانات المنتج", "قهوتى", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (result == DialogResult.Yes)
                    {
                        SqlConnection CONN = new SqlConnection(DatabaseConnection.Connection);

                        SqlCommand command = new SqlCommand();

                        command.Connection = CONN;
                        command.CommandText = "update Products set Prod_Price='" + proprice + "'  where Prod_Code='" + id.Text + "'";

                        CONN.Open();

                        command.ExecuteNonQuery();

                        CONN.Close();

                        //___________________________________________________________________________________

                        SqlConnection CONN1 = new SqlConnection(DatabaseConnection.Connection);

                        SqlCommand command1 = new SqlCommand();

                        command1.Connection = CONN1;
                        command1.CommandText = "update Purchases set Purch_Sell='" + proprice + "'  where Prod_Code='" + id.Text + "'";

                        CONN1.Open();

                        command1.ExecuteNonQuery();

                        CONN1.Close();

                        //___________________________________________________________________________________

                        DataTable table2 = new DataTable();

                        SqlConnection CONN2 = new SqlConnection(DatabaseConnection.Connection);
                        SqlCommand command2 = new SqlCommand();

                        command2.Connection = CONN2;
                        command2.CommandText = "select [Purch_Quantity] from Purchases where Prod_Code= '" + id.Text + "' ";

                        CONN2.Open();
                        table2.Load(command2.ExecuteReader());

                        string Quantity = table2.Rows[0][0].ToString();

                        CONN2.Close();

                        //___________________________________________________________________________________

                        float TotalSellBuy = float.Parse(Quantity) * float.Parse(proprice);

                        SqlConnection CONN3 = new SqlConnection(DatabaseConnection.Connection);

                        SqlCommand command3 = new SqlCommand();

                        command3.Connection = CONN3;
                        command3.CommandText = "update Purchases set Purch_TotalSell='" + TotalSellBuy.ToString() + "'  where Prod_Code='" + id.Text + "'";

                        CONN3.Open();

                        command3.ExecuteNonQuery();

                        CONN3.Close();

                        //___________________________________________________________________________________


                        MessageBox.Show("تم تعدبل البيانات بنجاح", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
            }
            catch (Exception)
            {
                MessageBox.Show("برجاء استكمال البيانات المطلوبه", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}