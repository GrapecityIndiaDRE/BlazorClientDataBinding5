using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDataBinding.Shared
{
    public class Customers
    {
        
        public int CustomerKey { get; set; }
        public string? Customer { get; set; }
        public string? BillToCustomer { get; set; }
        public string? Category { get; set; }
        public string? BuyingGroup { get; set; }
        public string? PrimaryContact { get; set; }
        public string? PostalCode { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }

    }
}
