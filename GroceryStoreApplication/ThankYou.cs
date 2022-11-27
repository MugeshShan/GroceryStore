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
    public partial class ThankYou : Form
    {
        public ThankYou()
        {
            InitializeComponent();
        }

        private void ThankYou_Load(object sender, EventArgs e)
        {
            label1.Text = "Hi " + Utils.Utility.User.Username;
            var time = Utils.Utility.Order.IsInstaBuy ? "30 mins" : "2 hours";
            label2.Text = "Your items will arrive in " + time;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Products cart = new Products();
            cart.Show();
            this.Close();
        }
    }
}
