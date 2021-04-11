using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartVisionAPI.Model
{
    public class Customer
    {
       

            public int CustID { get; set; }
            public string CustName { get; set; }
            public string Address { get; set; }
            public string Telephone { get; set; }


    }

    public class CustomerList
    {
        public List<Customer> Customers { get; set; }
    }
}
