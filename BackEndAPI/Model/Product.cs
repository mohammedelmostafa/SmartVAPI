using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace SmartVisionAPI.Model
{
    public class Product
    {
      
        public int? Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }

    public class ProductList
    {
      
        public List<Product> Products { get; set; }
    }
}
