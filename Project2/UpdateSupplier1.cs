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
    public partial class UpdateSupplier1 : DevExpress.XtraEditors.XtraForm
    {
        public UpdateSupplier1(string x, string y)
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

        //Supplier Page
        private void button2_Click(object sender, EventArgs e)
        {
            Supplier supplier = new Supplier(name.Text, right.Text);

            if (supplier == null)
            {
                this.Hide();
                supplier.Show();
            }
            else
            {
                this.Hide();
                supplier.Show();
                supplier.Focus();
            }
        }

        //Preview Suppliers Information
        private void UpdateSupplier1_Load(object sender, EventArgs e)
        {
            DataTable table1 = new DataTable();

            SqlConnection CONN1 = new SqlConnection(DatabaseConnection.Connection);

            SqlCommand command1 = new SqlCommand();

            command1.Connection = CONN1;
            command1.CommandText = "select [Supp_ID] as 'كود المورد',[Supp_Name] as 'اسم المورد', [Supp_Phone] as 'هاتف المورد' from Suppliers";

            dataGridView1.DataSource = table1;

            CONN1.Open();
            table1.Load(command1.ExecuteReader());

            CONN1.Close();
        }

        //Choose Supplier to update info.
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string ind = dataGridView1.CurrentCell.Value.ToString();

                DialogResult result;
                result = MessageBox.Show("هل متأكد من تعديل بيانات المورد", "قهوتى", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result == DialogResult.Yes)
                {
                    UpdateSupplier2 updateSupplier = new UpdateSupplier2(name.Text, right.Text, ind);

                    if (updateSupplier == null)
                    {
                        this.Hide();
                        updateSupplier.Show();
                    }
                    else
                    {
                        this.Hide();
                        updateSupplier.Show();
                        updateSupplier.Focus();
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