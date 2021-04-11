using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace SmartVisionAPI.Model
{
    public class Sale
    {
      
        public int Id { get; set; }
        public string ProducName { get; set; }
        public string CustomerName { get; set; }
        public double ProdPrice { get; set; }
        public int Qnt { get; set; }
    }

    public class SaleTransactionList
    {
      
        public List<Sale> Sales { get; set; }
    }

  
}
