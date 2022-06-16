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
    public partial class UpdatePurchases2 : DevExpress.XtraEditors.XtraForm
    {
        public UpdatePurchases2(string x, string y,string i)
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

        //Preview Purchase Operation Information
        private void UpdatePurchases2_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable table = new DataTable();

                SqlConnection CONN = new SqlConnection(DatabaseConnection.Connection);

                SqlCommand command = new SqlCommand();

                command.Connection = CONN;
                command.CommandText = "select * from Purchases where Prod_Code= '" + id.Text + "' ";

                CONN.Open();
                table.Load(command.ExecuteReader());

                prodname.Text = table.Rows[0][3].ToString();
                suppname.Text = table.Rows[0][4].ToString();
                buyprice.Text = table.Rows[0][5].ToString();
                sellprice.Text = table.Rows[0][6].ToString();
                quantity.Text = table.Rows[0][7].ToString();

                CONN.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("برجاء استكمال البيانات المطلوبه", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Calculate The Total Sell Price (Incoming Money) and Total Buy Price (Outcoming Money)
        private void quantity_ValueChanged(object sender, EventArgs e)
        {
            string buy = buyprice.Value.ToString();
            string sell = sellprice.Text;
            string quan = quantity.Value.ToString();

            float totalBuy = float.Parse(buy) * float.Parse(quan);
            totalbuyprice.Text = totalBuy.ToString();

            float totalSell = float.Parse(sell) * float.Parse(quan);
            totalsellprice.Text = totalSell.ToString();

            float Total = totalSell - totalBuy;
            total.Text = Total.ToString();
        }

        //Update Purchase Button
        private void button1_Click(object sender, EventArgs e)
        {
            string proname = prodname.Text;
            string supname = suppname.Text;
            string buy = buyprice.Value.ToString();
            string sell = sellprice.Text;
            string quan = quantity.Value.ToString();
            string available = quan;

            if (proname.Equals("") || supname.Equals("") || buy.Equals("0.00") || sell.Equals("0.00") || quan.Equals("0"))
            {
                MessageBox.Show("برجاء استكمال البيانات المطلوبه", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            int b = int.Parse(buy);
            int s = int.Parse(sell);

            if (s <= b)
            {
                MessageBox.Show("خطأ لا يمكن ان يكون سعر البيع اقل من الشراء", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                List<String> productcode = new List<string>();
                List<String> supplierid = new List<string>();

                DataTable table1 = new DataTable();
                DataTable table2 = new DataTable();

                SqlConnection CONN1 = new SqlConnection(DatabaseConnection.Connection);
                SqlConnection CONN2 = new SqlConnection(DatabaseConnection.Connection);

                SqlCommand command1 = new SqlCommand();
                SqlCommand command2 = new SqlCommand();

                command1.Connection = CONN1;
                command2.Connection = CONN2;

                command1.CommandText = "select [Prod_Code] from Products where Prod_Name = '" + proname + "'  ";
                command2.CommandText = "select [Supp_ID] from Suppliers where Supp_Name = '" + supname + "'";

                CONN1.Open();
                CONN2.Open();

                table1.Load(command1.ExecuteReader());
                table2.Load(command2.ExecuteReader());

                productcode.Add(table1.Rows[0][0].ToString());
                supplierid.Add(table2.Rows[0][0].ToString());

                string procode = productcode[0].ToString();
                string supid = supplierid[0].ToString();

                //_______________________________________________________________________________________

                DialogResult result;
                result = MessageBox.Show("هل متأكد من تعديل عمليه الشراء", "قهوتى", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result == DialogResult.Yes)
                {
                    string totalBuy = totalbuyprice.Text;
                    string totalSell = totalsellprice.Text;

                    SqlConnection CONN3 = new SqlConnection(DatabaseConnection.Connection);

                    SqlCommand command3 = new SqlCommand();

                    command3.Connection = CONN3;
                    command3.CommandText = "update Purchases set Prod_Code='" + procode + "' , Supp_ID='" + supid + "' , Prod_Name='" + proname + "' , Supp_Name='" + supname + "', Purch_Buy='" + buy + "', Purch_Sell='" + sell + "', Purch_Quantity='" + quan + "', Purch_TotalBuy='" + totalBuy + "', Purch_TotalSell='" + totalSell + "', Available_Quantity='" + available + "'   where Prod_Code='" + id.Text + "'";

                    CONN3.Open();
                    command3.ExecuteNonQuery();

                    MessageBox.Show("تم تعديل البيانات بنجاح", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CONN3.Close();

                    Purchase purchase = new Purchase(name.Text, right.Text);

                    if (purchase == null)
                    {
                        this.Hide();
                        purchase.Show();
                    }
                    else
                    {
                        this.Hide();
                        purchase.Show();
                        purchase.Focus();
                    }
                }
            }
        }
    }
}