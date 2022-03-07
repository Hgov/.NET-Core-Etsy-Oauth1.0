using DomainLayer.Concrete.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Concrete.Google.Sheets.Domain
{
    public class Item
    {
        public class Main
        {
            public List<Order> Orders { get; set; }
            public List<Transaction> Transactions { get; set; }
            public int count { get; set; }
        }
    }
}
