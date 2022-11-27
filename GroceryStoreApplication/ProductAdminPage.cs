using GroceryStoreApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GroceryStoreApplication
{
    public partial class ProductAdminPage : Form
    {
        List<Category> categories; 
        public ProductAdminPage()
        {
            InitializeComponent();
        }

        [Obsolete]
        private void ProductAdminPage_Load(object sender, EventArgs e)
        {
            categories = new List<Category>();
            var section = ConfigurationSettings.GetConfig("connectionStrings");
            OleDbConnection connection = new OleDbConnection();
            var str = ConfigurationManager.AppSettings["GroceryStoreDBConnectionString"];
            connection.ConnectionString = ConfigurationManager.AppSettings["GroceryStoreDBConnectionString"];
            connection.Open();
            var command = String.Format("Select * from Category");
            OleDbCommand command2 = new OleDbCommand(command, connection);
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.SelectCommand = command2;
            var ds = new DataSet();
            adapter.Fill(ds);
            var dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                var tempCat = new Category
                {
                    Id = Convert.ToInt32(dr["ID"]),
                    Name = dr["CatName"].ToString()
                };
                if (tempCat != null)
                {
                    categories.Add(tempCat);
                }
            }
            this.comboBox1.DataSource = categories.Select(x => x.Name).ToList();
            connection.Close();
        }

        [Obsolete]
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var category = this.comboBox1.SelectedItem.ToString();
                var id = categories.First(x => x.Name == category).Id;
                var section = ConfigurationSettings.GetConfig("connectionStrings");
                OleDbConnection connection = new OleDbConnection();
                var str = ConfigurationManager.AppSettings["GroceryStoreDBConnectionString"];
                connection.ConnectionString = ConfigurationManager.AppSettings["GroceryStoreDBConnectionString"];
                connection.Open();
                var command = String.Format("Insert INTO [Product] (ProductName, [CategoryId], Price) VALUES ('{0}', {1}, {2})", textBox1.Text, id, textBox2.Text);
                OleDbCommand command2 = new OleDbCommand(command, connection);
                if (textBox1.Text != "")
                {
                    command2.ExecuteNonQuery();
                    MessageBox.Show("Product " + textBox1.Text + " is added !!");
                }
                else
                {
                    MessageBox.Show("Please enter product name");
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("There is some problem...");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = ConfigurationManager.AppSettings["GroceryStoreDBConnectionString"];
            connection.Open();

            var command3 = "Select * from Product";
            OleDbCommand command4 = new OleDbCommand(command3, connection);
            OleDbDataAdapter adapter1 = new OleDbDataAdapter();
            adapter1.SelectCommand = command4;
            var ds1 = new DataSet();
            adapter1.Fill(ds1);
            var dt1 = ds1.Tables[0];

            var payment = new List<object>();

            foreach (DataRow dr in dt1.Rows)
            {

                var tempUser = new
                {
                    Id = Convert.ToInt32(dr["ID"]),
                    ProductName = dr["ProductName"].ToString(),
                    CategoryId = Convert.ToInt32(dr["CategoryId"]),
                    Price = Convert.ToInt32(dr["Price"])

                };

                payment.Add(tempUser);

            }

            this.dataGridView1.DataSource = payment;
        }
    }
}
