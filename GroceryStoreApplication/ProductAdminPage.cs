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
    }
}
