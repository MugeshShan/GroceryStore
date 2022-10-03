using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStoreApplication.Models
{
    public class Order
    {
        public Guid Id { get; set; }

        public string ProductName { get; set; }

        public double Price { get; set; }

        public double Quantity { get; set; }
    }
}
