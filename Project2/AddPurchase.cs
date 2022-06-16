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
    public partial class AddPurchase : DevExpress.XtraEditors.XtraForm
    {
        public AddPurchase(string x, string y)
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

        //Get All Products and Suppliers Name From DB
        private void AddPurchase_Load(object sender, EventArgs e)
        {
            List<String> Suppliers_Name = new List<string>();
            List<String> Products_Name = new List<string>();

            DataTable table = new DataTable();
            DataTable table1 = new DataTable();

            SqlConnection CONN = new SqlConnection(DatabaseConnection.Connection);
            SqlConnection CONN1 = new SqlConnection(DatabaseConnection.Connection);

            SqlCommand command = new SqlCommand();
            SqlCommand command1 = new SqlCommand();

            command.Connection = CONN;
            command1.Connection = CONN1;

            command.CommandText = "select [Prod_Name] from Products";
            command1.CommandText = "select [Supp_Name] from Suppliers";

            CONN.Open();
            CONN1.Open();

            table.Load(command.ExecuteReader());
            table1.Load(command1.ExecuteReader());

            for (int i = 0; i < table.Rows.Count; i++)
            {
                Products_Name.Add(table.Rows[i][0].ToString());
                prodname.Items.Add(Products_Name[i]);
            }
            for (int i = 0; i < table1.Rows.Count; i++)
            {
                Suppliers_Name.Add(table1.Rows[i][0].ToString());
                suppname.Items.Add(Suppliers_Name[i]);
            }
        }

        //Get The Product's Sell Price From DB
        private void prodname_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string proname = prodname.Text;

                if (proname.Equals(""))
                {
                    string sellPrice = sellprice.Value.ToString();
                    sellPrice = "0.00";
                }
                else
                {
                    List<String> Products_Price = new List<string>();

                    DataTable table2 = new DataTable();

                    SqlConnection CONN2 = new SqlConnection(DatabaseConnection.Connection);

                    SqlCommand command2 = new SqlCommand();

                    command2.Connection = CONN2;

                    command2.CommandText = "select [Prod_Price] from Products where Prod_Name = '" + proname + "' ";

                    CONN2.Open();

                    table2.Load(command2.ExecuteReader());

                    Products_Price.Add(table2.Rows[0][0].ToString());

                    sellprice.Value = decimal.Parse(Products_Price[0].ToString());

                }
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
            string sell = sellprice.Value.ToString();
            string quan = quantity.Value.ToString();

            float totalBuy = float.Parse(buy) * float.Parse(quan);
            totalbuyprice.Text = totalBuy.ToString();

            float totalSell = float.Parse(sell) * float.Parse(quan);
            totalsellprice.Text = totalSell.ToString();

            float Total = totalSell - totalBuy;
            total.Text = Total.ToString();
        }

        //Add Purchase Button
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string proname = prodname.Text;
                string supname = suppname.Text;
                string buy = buyprice.Value.ToString();
                string sell = sellprice.Value.ToString();
                string quan = quantity.Value.ToString();
                string available = quan;

                if (proname.Equals("") || supname.Equals("") || buy.Equals("0.00") || sell.Equals("0.00") || quan.Equals("0"))
                {
                    MessageBox.Show("برجاء استكمال البيانات المطلوبه", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //Check for Sell Price Must be Grater than or Equal Buy Price.
                else if(sellprice.Value <= buyprice.Value)
                {
                    MessageBox.Show("خطأ لا يمكن ان يكون سعر البيع اقل من الشراء", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    List<String> productsname = new List<string>();

                    DataTable table3 = new DataTable();

                    SqlConnection CONN3 = new SqlConnection(DatabaseConnection.Connection);

                    SqlCommand command3 = new SqlCommand();

                    command3.Connection = CONN3;
                    command3.CommandText = "select [Prod_Name] from Purchases";

                    CONN3.Open();

                    table3.Load(command3.ExecuteReader());

                    for (int i = 0; i < table3.Rows.Count; i++)
                    {
                        productsname.Add(table3.Rows[i][0].ToString());
                    }
                    if (productsname.Contains(proname))
                    {
                        MessageBox.Show("هذا المنتج تم شرائه من قبل يرجى التأكد", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        List<String> productcode = new List<string>();
                        List<String> supplierid = new List<string>();

                        DataTable table4 = new DataTable();
                        DataTable table5 = new DataTable();

                        SqlConnection CONN4 = new SqlConnection(DatabaseConnection.Connection);
                        SqlConnection CONN5 = new SqlConnection(DatabaseConnection.Connection);

                        SqlCommand command4 = new SqlCommand();
                        SqlCommand command5 = new SqlCommand();

                        command4.Connection = CONN4;
                        command5.Connection = CONN5;

                        command4.CommandText = "select [Prod_Code] from Products where Prod_Name = '" + proname + "'  ";
                        command5.CommandText = "select [Supp_ID] from Suppliers where Supp_Name = '" + supname + "'";

                        CONN4.Open();
                        CONN5.Open();

                        table4.Load(command4.ExecuteReader());
                        table5.Load(command5.ExecuteReader());

                        productcode.Add(table4.Rows[0][0].ToString());
                        supplierid.Add(table5.Rows[0][0].ToString());

                        string procode = productcode[0].ToString();
                        string supid = supplierid[0].ToString();

                        //_______________________________________________________________________________________

                        DialogResult result;
                        result = MessageBox.Show("هل متأكد من اضافه عمليه شراء جديده", "قهوتى", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                        if (result == DialogResult.Yes)
                        {
                            string totalBuy = totalbuyprice.Text;
                            string totalSell = totalsellprice.Text;

                            SqlConnection CONN6 = new SqlConnection(DatabaseConnection.Connection);

                            SqlCommand command6 = new SqlCommand();

                            command6.Connection = CONN6;
                            command6.CommandText = "insert into Purchases values ('" + procode + "','" + supid+ "' ,'" + proname + "','" + supname + "','" + buy + "','" + sell + "','" + quan + "','" + totalBuy + "','" + totalSell + "','" + available + "')";

                            CONN6.Open();

                            command6.ExecuteNonQuery();

                            MessageBox.Show("تم اضافه البيانات بنجاح", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            CONN6.Close();

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
            catch (Exception)
            {
                MessageBox.Show("برجاء استكمال البيانات المطلوبه", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}