using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStoreApplication.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public string CategoryId { get; set; }

        public double Price { get; set; }   
    }
}
