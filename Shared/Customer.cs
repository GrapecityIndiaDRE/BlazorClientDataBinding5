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

        public static IEnumerable<Customers> GetCustomers(int Count)
        {
            var _names = "Alex,Ben,Bob,Chris,David,Evan,Freddy,George,Henry,Isac,Jack,Ken,Laurie".Split(",");
            var _buyingGroups = "WJ,SP,C1,AR".Split(",");
            var _categories = "Platinum,Standard".Split(","); 
            var _random = new Random();
            var data =new List<Customers>();
            for (int i = 0; i < Count; i++)
            {
                var _rnd = _random.Next();
                data.Add(new Customers()
                {
                    BillToCustomer = _names[_rnd % _names.Length],
                    BuyingGroup = _buyingGroups[_rnd % _buyingGroups.Length],
                    PrimaryContact = _names[_rnd % _names.Length],
                    Category = _categories[_rnd % _categories.Length],
                    Customer = _names[_rnd % _names.Length],
                    CustomerKey = i,
                    PostalCode = (_rnd % 99999).ToString(),
                    ValidFrom = DateTime.Now.AddDays(i),
                    ValidTo = DateTime.Now.AddYears(i % 2)
                });
            }

            return data;

             
        }
    }
    public class CustomerResponse
    {
        public int TotalCount { get; set; }
        public IEnumerable<Customers> Customers { get; set; }
    }
}
