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
    public partial class AddProduct : DevExpress.XtraEditors.XtraForm
    {
        public AddProduct(string x, string y)
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

        //Add Product Button
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
                    List<String> productsname = new List<string>();

                    DataTable table = new DataTable();

                    SqlConnection CONN = new SqlConnection(DatabaseConnection.Connection);

                    SqlCommand command = new SqlCommand();

                    command.Connection = CONN;
                    command.CommandText = "select [Prod_Name] from Products";

                    CONN.Open();

                    table.Load(command.ExecuteReader());
                    
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        productsname.Add(table.Rows[i][0].ToString());
                    }

                    if (productsname.Contains(proname))
                    {
                        MessageBox.Show("هذا المنتج تم اضافته من قبل يرجى التأكد", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        DialogResult result;
                        result = MessageBox.Show("هل متأكد من اضافه منتج جديد", "قهوتى", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                        if (result == DialogResult.Yes)
                        {
                            SqlConnection CONN1 = new SqlConnection(DatabaseConnection.Connection);

                            SqlCommand command1 = new SqlCommand();

                            command1.Connection = CONN1;
                            command1.CommandText = "insert into Products values ('" + proname + "','" + proprice + "')";

                            CONN1.Open();

                            command1.ExecuteNonQuery();

                            MessageBox.Show("تم اضافه البيانات بنجاح", "قهوتى", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            CONN.Close();

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
            }
            catch (Exception)
            {
                MessageBox.Show("برجاء استكمال البيانات المطلوبه", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Get All Products Name in DB (Purchases Table)
        private void AddProduct_Load(object sender, EventArgs e)
        {
            List<String> Products_Name = new List<string>();

            DataTable table1 = new DataTable();

            SqlConnection CONN1 = new SqlConnection(DatabaseConnection.Connection);
            SqlCommand command1 = new SqlCommand();

            command1.Connection = CONN1;
            command1.CommandText = "select [Prod_Name] from Purchases";

            CONN1.Open();

            table1.Load(command1.ExecuteReader());

            for (int i = 0; i < table1.Rows.Count; i++)
            {
                Products_Name.Add(table1.Rows[i][0].ToString());
                prodname.Items.Add(Products_Name[i]);
            }
        }
    }
}