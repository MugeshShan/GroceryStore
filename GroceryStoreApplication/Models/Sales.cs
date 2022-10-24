using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStoreApplication.Models
{
    public class Sales
    {
        public int Id { get; set; }

        public string Products { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public int UserId { get; set; }

        public Guid PaymentId { get; set; }
    }
}
