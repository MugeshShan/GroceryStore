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
    public partial class Payment : Form
    {
        public Payment()
        {
            InitializeComponent();
        }

        [Obsolete]
        private void button1_Click(object sender, EventArgs e)
        {
            Utils.Utility.User.Address = richTextBox1.Text;
            Utils.Utility.User.City = textBox2.Text;
            Utils.Utility.User.State = textBox3.Text;
            Utils.Utility.User.Pincode = textBox4.Text;
            Utils.Utility.User.Phone = textBox5.Text;
            var user = Utils.Utility.User;
            var section = ConfigurationSettings.GetConfig("connectionStrings");
            OleDbConnection connection = new OleDbConnection();
            var str = ConfigurationManager.AppSettings["GroceryStoreDBConnectionString"];
            connection.ConnectionString = ConfigurationManager.AppSettings["GroceryStoreDBConnectionString"];
            connection.Open();
            //var command = String.Format("Insert INTO [UserDetails] (ID, [Username], Address, [City], State, [Pincode], Phone ) VALUES ('{0}', '{1}', {2}, {3},  {4}, {5}, {6})", user.Id, user.Username, user.Address, user.City, user.State, user.Pincode, user.Phone);
            //OleDbCommand command2 = new OleDbCommand(command, connection);
            //command2.ExecuteNonQuery();
            var paymentCommand = String.Format("INSERT INTO [Payment] (OrderId, [BillAmount], IsConfirmed , [UserId])  VALUES ('{0}', '{1}', {2}, {3})", Utils.Utility.Order.Id, Utils.Utility.Order.BillAmount, "Yes", Utils.Utility.User.Id);
            OleDbCommand paymentCommand2 = new OleDbCommand(paymentCommand, connection);
            paymentCommand2.ExecuteNonQuery();
            connection.Close();
            ThankYou thankYou = new ThankYou();
            thankYou.Show();
        }

        private void Payment_Load(object sender, EventArgs e)
        {
            textBox1.Text = Utils.Utility.User.Username;
            textBox6.Text = Convert.ToString(Utils.Utility.Order.BillAmount);
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
