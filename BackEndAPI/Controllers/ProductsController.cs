using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SmartVisionAPI.Model;
using Newtonsoft.Json.Linq;

namespace SmartVisionAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger)
        {
            _logger = logger;
        }
        [HttpPost]
        public ActionResult Post(Product req)
        {
            string jsonDb = Path.Combine(Directory.GetCurrentDirectory(), $"Data\\ProductData.json");
            var jsonString = System.IO.File.ReadAllText(jsonDb);
            var responseresult = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductList>(jsonString);
            var products = responseresult.Products;
            products.Add(new Product
            {
                Id = products.Last().Id + 1,
                Name = req.Name,
                Price = req.Price
            });
            responseresult.Products = products;
            var newJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(responseresult, Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText(jsonDb, newJsonResult);
            return Ok();
        }
        [HttpGet]
        public ActionResult Get(string Name, int? MinPrice, int? MaxPrice)
        {
            string jsonDb = Path.Combine(Directory.GetCurrentDirectory(), $"Data\\ProductData.json");
            var jsonString = System.IO.File.ReadAllText(jsonDb);
            var responseresult = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductList>(jsonString);

            if (responseresult != null) {

                if (!String.IsNullOrEmpty(Name)) responseresult.Products = responseresult.Products.Where(p => p.Name == Name).ToList();
                if (MinPrice.HasValue && MinPrice.Value > 0) responseresult.Products = responseresult.Products.Where(p => p.Price >= MinPrice.Value).ToList();
                if (MaxPrice.HasValue && MaxPrice.Value > 0) responseresult.Products = responseresult.Products.Where(p => p.Price <= MaxPrice.Value).ToList();

                return Ok(responseresult);

            }

            return BadRequest();
        }
        [HttpPut]
        public ActionResult Put(Product req)
        {
            string jsonDb = Path.Combine(Directory.GetCurrentDirectory(), $"Data\\ProductData.json");
            var jsonString = System.IO.File.ReadAllText(jsonDb);
            var responseresult = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductList>(jsonString);
            var products = responseresult.Products;
            products.Remove(products.FirstOrDefault(c => c.Id == req.Id));
            products.Add(new Product
            {
                Id = req.Id,
                Name = req.Name,
                Price = req.Price
            });
            responseresult.Products = products;
            var newJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(responseresult, Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText(jsonDb, newJsonResult);
            return Ok();
        }
        [HttpDelete]
        public ActionResult Delete(int Id)
        {
            string jsonDb = Path.Combine(Directory.GetCurrentDirectory(), $"Data\\ProductData.json");
            var jsonString = System.IO.File.ReadAllText(jsonDb);
            var responseresult = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductList>(jsonString);
            var products = responseresult.Products;
            products.Remove(products.FirstOrDefault(c => c.Id == Id));
            responseresult.Products = products;
            var newJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(responseresult, Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText(jsonDb, newJsonResult);
            return Ok();
        }
    }
}
