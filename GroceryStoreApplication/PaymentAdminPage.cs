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
    public partial class PaymentAdminPage : Form
    {
        public PaymentAdminPage()
        {
            InitializeComponent();
        }

        private void PaymentAdminPage_Load(object sender, EventArgs e)
        {
            try
            {
                var section = ConfigurationSettings.GetConfig("connectionStrings");
                OleDbConnection connection = new OleDbConnection();
                var str = ConfigurationManager.AppSettings["GroceryStoreDBConnectionString"];
                connection.ConnectionString = ConfigurationManager.AppSettings["GroceryStoreDBConnectionString"];
                connection.Open();
                var command = String.Format("Select * from Payment");
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
