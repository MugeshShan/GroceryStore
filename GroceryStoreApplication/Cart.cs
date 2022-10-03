using GroceryStoreApplication.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        }
    }
}
