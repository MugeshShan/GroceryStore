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

        public List<OrderedProducts> OrderedProducts { get; set; }

        public bool IsInstaBuy { get; set; }

        public double BillAmount { get; set; }

        public Order()
        {
            Id = new Guid();
            OrderedProducts = new List<OrderedProducts>();
        }
    }
}
