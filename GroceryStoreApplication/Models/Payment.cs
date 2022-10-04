using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStoreApplication.Models
{
    public class Payment
    {
        public Guid Id { get; set; }

        public bool IsConfirmed { get; set; }

        public User User { get; set; }
    }
}
