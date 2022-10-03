using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStoreApplication.Models
{
    public class OrderedProducts
    {

        public string ProductName { get; set; }

        public double Quantity { get; set; }

        public double Price { get; set; }

        public int CategoryId { get; set; }

        public double Total { get; set; }

    }
}
