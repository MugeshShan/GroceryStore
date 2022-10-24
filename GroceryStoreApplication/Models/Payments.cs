using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStoreApplication.Models
{
    public class Payments
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public double BillAmount { get; set; }

        public bool IsConfirmed { get; set; }

        public int UserId { get; set; }
    }
}
