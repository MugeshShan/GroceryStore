using GroceryStoreApplication.Models;
using GroceryStoreApplication.Utils;
using Newtonsoft.Json;
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
    public partial class Cart : Form
    {
        double Total;
        ToolTip toolTip1 = new ToolTip();
        ToolTip toolTip2 = new ToolTip();
        public Cart()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void Cart_Load(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = Utility.StaticOrderedProducts;
            Total = Utility.StaticOrderedProducts.Sum(x => x.Total);
            label3.Text = (Total + 100).ToString();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            toolTip2.Active = false;
            toolTip1.Show("Normal delivery fee will be added and delivered in 2 hours", this.radioButton1);
            label3.Text = (Total + 100).ToString();
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            toolTip1.Active = false;
            toolTip2.Show("Normal delivery fee + 100 will be added and delivered in 30 mins", this.radioButton2);
            label3.Text = (Total + 200).ToString();
        }

        [Obsolete]
        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                Total += 100;
            }
            if (radioButton2.Checked)
            {
                Total += 200;
            }
            label3.Text = Total.ToString();

            var order = new Order();
            order.Id = Guid.NewGuid();
            order.OrderedProducts.AddRange(Utility.StaticOrderedProducts);
            order.IsInstaBuy = radioButton2.Checked;
            order.BillAmount = Total;

            var json = JsonConvert.SerializeObject(order.OrderedProducts);

            var section = ConfigurationSettings.GetConfig("connectionStrings");
            OleDbConnection connection = new OleDbConnection();
            var str = ConfigurationManager.AppSettings["GroceryStoreDBConnectionString"];
            connection.ConnectionString = ConfigurationManager.AppSettings["GroceryStoreDBConnectionString"];
            connection.Open();
            var command = String.Format("Insert INTO [Order] (OrderId, [OrderedProducts], IsInstaBuy, [BillAmount] ) VALUES ('{0}', '{1}', {2}, {3})", order.Id, json, order.IsInstaBuy, order.BillAmount);
            OleDbCommand command2 = new OleDbCommand(command, connection);
            command2.ExecuteNonQuery();
            connection.Close();
        }
    }
}
