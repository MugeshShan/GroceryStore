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
    public partial class CategoryAdminPage : Form
    {
        public CategoryAdminPage()
        {
            InitializeComponent();
        }

        [Obsolete]
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var section = ConfigurationSettings.GetConfig("connectionStrings");
                OleDbConnection connection = new OleDbConnection();
                var str = ConfigurationManager.AppSettings["GroceryStoreDBConnectionString"];
                connection.ConnectionString = ConfigurationManager.AppSettings["GroceryStoreDBConnectionString"];
                connection.Open();
                var command = String.Format("Insert INTO [Category] (CatName) VALUES ('{0}')", textBox1.Text);
                OleDbCommand command2 = new OleDbCommand(command, connection);
                if (textBox1.Text != "")
                {
                    command2.ExecuteNonQuery();
                    MessageBox.Show("Category " + textBox1.Text + "is added !!");
                }            
                else
                {
                    MessageBox.Show("Please enter catgory name");
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
            try
            {
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
                this.dataGridView1.DataSource = dt;
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("There is some problem...");
            }
        }
    }
}
