using GroceryStoreApplication.Models;
using GroceryStoreApplication.Utils;
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
    public partial class Products : Form
    {
        OleDbConnection connection;
        List<Category> categories;
        List<Order> Orders;
        GroupBox groupBox;
        List<OrderedProducts> OrderedProducts;
        int categoryId = 0;
        public Products()
        {
            categories = new List<Category>();
            Orders = new List<Order>();
            groupBox = new GroupBox();
            OrderedProducts = new List<OrderedProducts>();
            InitializeComponent();
        }

        [Obsolete]
        private void Products_Load(object sender, EventArgs e)
        {
            var section = ConfigurationSettings.GetConfig("connectionStrings");
            connection = new OleDbConnection();
            var str = ConfigurationManager.AppSettings["GroceryStoreDBConnectionString"];
            connection.ConnectionString = ConfigurationManager.AppSettings["GroceryStoreDBConnectionString"];
            connection.Open();
            var command = String.Format("Select * from [Category]");
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
            this.comboBox1.DataSource = categories.Select(x=>x.Name).ToList();
            if (OrderedProducts.Count > 0)
            {
                Utility.StaticOrderedProducts.AddRange(OrderedProducts);
            }
            //this.comboBox1.Items.Add(categories.Select(x=>x.Name));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(this.flowLayoutPanel1.Controls.Count > 0)
            {
                if(OrderedProducts.Count > 0)
                {
                    //Utility.StaticOrderedProducts.AddRange(OrderedProducts);
                }
                this.flowLayoutPanel1.Controls.Clear();
            }
            categoryId = categories.Find(x => x.Name == this.comboBox1.SelectedValue.ToString()).Id;
            var command = String.Format("Select * from [Product] where CategoryId = {0}", categoryId);
            OleDbCommand command2 = new OleDbCommand(command, connection);
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.SelectCommand = command2;
            var ds = new DataSet();
            adapter.Fill(ds);
            var dt = ds.Tables[0];

            Label NameTxt = new Label();
            NameTxt.Text = "Product Name";
            NameTxt.Font = new Font("Times New Roman", 10F);

            Label PriceTxt = new Label();
            PriceTxt.Text = "Product Price";
            PriceTxt.Font = new Font("Times New Roman", 10F);

            Label QtyTxt = new Label();
            QtyTxt.Text = "Quantity (in Kg)";
            QtyTxt.Font = new Font("Times New Roman", 10F);


            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
            this.flowLayoutPanel1.Controls.Add(NameTxt);
            this.flowLayoutPanel1.Controls.Add(PriceTxt);
            this.flowLayoutPanel1.Controls.Add(QtyTxt);

            int i = 1;

            foreach (DataRow dr in dt.Rows)
            {
                GroupBox groupBox = new GroupBox();
                Label nameLabel = new Label();
                nameLabel.Name = "ProductNameTxt-" + i;
                nameLabel.Text = dr["ProductName"].ToString();
                Label priceLabel = new Label();
                priceLabel.Name = "PriceTxt-" + i;
                priceLabel.Text = dr["Price"].ToString();
                TextBox qtyTextBox = new TextBox();
                qtyTextBox.Name = "QtyTxt-" + i;
                qtyTextBox.Text = "0";
                i++;
                groupBox.Text = "groupBox-" + i;
                groupBox.Visible = true;
                groupBox.Controls.Add(nameLabel);
                groupBox.Controls.Add(priceLabel);
                groupBox.Controls.Add(qtyTextBox);
                this.flowLayoutPanel1.Controls.Add(nameLabel);
                this.flowLayoutPanel1.Controls.Add(priceLabel);
                this.flowLayoutPanel1.Controls.Add(qtyTextBox);
                //this.flowLayoutPanel1.Controls.Add(groupBox);
            }
            Button addCartButton = new Button();
            addCartButton.Text = "Add";
            this.flowLayoutPanel1.Controls.Add(addCartButton);
            addCartButton.Click += new System.EventHandler(this.AddToCart);
            addCartButton.Font = new Font("Times New Roman", 10F);

            Button goToCart = new Button();
            goToCart.Text = "Go To Cart";
            this.flowLayoutPanel1.Controls.Add(goToCart);
            goToCart.Click += new System.EventHandler(this.GoToCart);
            goToCart.Font = new Font("Times New Roman", 10F);

            if (dt.Rows.Count > 0)
            { 
                
            }

           
            
        }

        private void GoToCart(object sender, EventArgs e)
        {
            Utility.StaticOrderedProducts.AddRange(OrderedProducts);
            Cart cart = new Cart();
            cart.Show();
            this.Close();
        }

        private void AddToCart(object sender, EventArgs e)
        {
            int i = 1;
            var tempOrder = new OrderedProducts();
            foreach(Control controls in this.flowLayoutPanel1.Controls)
            {              
                if(controls.Name == ("ProductNameTxt-" + i))
                {
                    tempOrder.ProductName = controls.Text;
                }
                else if(controls.Name == ("PriceTxt-" + i))
                {
                    tempOrder.Price = Convert.ToDouble(controls.Text);
                }
                else if(controls.Name == ("QtyTxt-" + i))
                {
                    tempOrder.Quantity = Convert.ToDouble(controls.Text);
                }
                if(tempOrder.ProductName != null && tempOrder.Price != 0 && tempOrder.Quantity != 0)
                {
                    if (tempOrder.Quantity != 0)
                    {
                        tempOrder.CategoryId = categoryId;
                        tempOrder.Total = tempOrder.Quantity * tempOrder.Price;
                        OrderedProducts.Add(tempOrder);
                        tempOrder = new OrderedProducts();
                    }
                    i++;
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
