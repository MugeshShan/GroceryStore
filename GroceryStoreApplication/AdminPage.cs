﻿using System;
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
    public partial class AdminPage : Form
    {
        public AdminPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CategoryAdminPage categoryAdminPage = new CategoryAdminPage();  
            categoryAdminPage.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //ProductAdminPage
        }
    }
}
