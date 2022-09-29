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
    public partial class Form1 : Form
    {
        List<User> users = new List<User>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        [Obsolete]
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var section = ConfigurationSettings.GetConfig("connectionStrings");
                OleDbConnection connection =  new OleDbConnection();
                var str = ConfigurationManager.AppSettings["GroceryStoreDBConnectionString"];
                connection.ConnectionString = ConfigurationManager.AppSettings["GroceryStoreDBConnectionString"];
                connection.Open();
                var command = String.Format("Insert INTO [User] (Username, [Password], RoleId) VALUES ('{0}', '{1}', {2})", textBox1.Text, textBox3.Text, 2);
                OleDbCommand command2 = new OleDbCommand(command, connection);
                if (textBox3.Text != textBox2.Text)
                {
                    MessageBox.Show("Passwords are not same");
                }
                else
                {
                    command2.ExecuteNonQuery();
                    MessageBox.Show("User Inserted. Please login..");
                }
                connection.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("There is some problem...");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        [Obsolete]
        private void button2_Click(object sender, EventArgs e)
        {
            var section = ConfigurationSettings.GetConfig("connectionStrings");
            OleDbConnection connection = new OleDbConnection();
            var str = ConfigurationManager.AppSettings["GroceryStoreDBConnectionString"];
            connection.ConnectionString = ConfigurationManager.AppSettings["GroceryStoreDBConnectionString"];
            connection.Open();
            var command = String.Format("Select * from [User]");
            OleDbCommand command2 = new OleDbCommand(command, connection);
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.SelectCommand = command2;
            var ds = new DataSet();
            adapter.Fill(ds);
            var dt = ds.Tables[0];
            foreach(DataRow dr in dt.Rows)
            {
                var tempUser = new User
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Username = dr["Username"].ToString(),
                    Password = dr["Password"].ToString(),
                    RoleId = Convert.ToInt32(dr["RoleId"]),
                    IsAdmin = Convert.ToBoolean(dr["IsAdmin"])
                };
                if(tempUser != null)
                {
                    users.Add(tempUser);
                }
            }
            if(users.Find(x=>x.Username == textBox4.Text && x.Password == textBox5.Text) != null)
            {
                MessageBox.Show("Welcome " + textBox4.Text + " !!!");
                ClearTextBoxes();
                Products products = new Products();
                products.Show();
            }
            else
            {
                MessageBox.Show("Username or Password is incorrect!!!");
                ClearTextBoxes();
            }
            connection.Close();
            
        }

        private void ClearTextBoxes()
        {
            Action<Control.ControlCollection> func = null;

            func = (controls) =>
            {
                foreach (Control control in controls)
                    if (control is TextBox)
                        (control as TextBox).Clear();
                    else
                        func(control.Controls);
            };

            func(Controls);
        }

    }
}
