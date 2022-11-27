using GroceryStoreApplication.Models;
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
    public partial class Payment : Form
    {
        public Payment()
        {
            InitializeComponent();
        }

        [Obsolete]
        private void button1_Click(object sender, EventArgs e)
        {
            var userDetails = new UserDetails();
            userDetails.Address = richTextBox1.Text;
            userDetails.City = textBox2.Text;
            userDetails.State = textBox3.Text;
            userDetails.Pincode = textBox4.Text;
            userDetails.Phone = textBox5.Text;
            var addressJson = JsonConvert.SerializeObject(userDetails);
            var user = Utils.Utility.User;
            var section = ConfigurationSettings.GetConfig("connectionStrings");
            OleDbConnection connection = new OleDbConnection();
            var str = ConfigurationManager.AppSettings["GroceryStoreDBConnectionString"];
            connection.ConnectionString = ConfigurationManager.AppSettings["GroceryStoreDBConnectionString"];
            connection.Open();
            //var command = String.Format("Insert INTO [UserDetails] (ID, [Username], Address, [City], State, [Pincode], Phone ) VALUES ('{0}', '{1}', {2}, {3},  {4}, {5}, {6})", user.Id, user.Username, user.Address, user.City, user.State, user.Pincode, user.Phone);
            //OleDbCommand command2 = new OleDbCommand(command, connection);
            //command2.ExecuteNonQuery();
            var payment = new Payments();
            payment.Id = Guid.NewGuid();
            payment.OrderId = Utils.Utility.Order.Id;
            payment.BillAmount = Utils.Utility.Order.BillAmount;
            payment.IsConfirmed = true;
            payment.UserId = Utils.Utility.User.Id;
            var date = DateTime.Now.ToString("MM/dd/yyyy");
            var paymentCommand = String.Format("INSERT INTO [Payment] (Id, [OrderId], BillAmount, [IsConfirmed] , UserId, [PaymentDate], [Address])  " +
                "VALUES ('{0}', '{1}', {2}, {3}, {4},  '{5}')", payment.Id, Utils.Utility.Order.Id, Utils.Utility.Order.BillAmount, "Yes", Utils.Utility.User.Id, date);
            OleDbCommand paymentCommand2 = new OleDbCommand(paymentCommand, connection);
            paymentCommand2.ExecuteNonQuery();
            var json = JsonConvert.SerializeObject(Utils.Utility.Order.OrderedProducts);
            var sales = new Sales();
            sales.Price = payment.BillAmount;
            sales.Products = json;
            sales.Quantity = Utils.Utility.StaticOrderedProducts.Count();
            sales.UserId = payment.UserId;
            sales.PaymentId = payment.Id;
            var command = String.Format("Insert INTO [Sales] ([Products], [Price], [Quantity], [UserId], [PaymentId], [SalesDate]) VALUES ('{0}', {1}, {2}, {3}, '{4}', {5})", JsonConvert.SerializeObject(Utils.Utility.Order.OrderedProducts), sales.Price, sales.Quantity, sales.UserId, sales.PaymentId, date);
            OleDbCommand command2 = new OleDbCommand(command, connection);
            command2.ExecuteNonQuery();
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
