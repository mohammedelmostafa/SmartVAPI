using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartVisionAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.IO;

namespace SmartVisionAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SaleTransactionsController : ControllerBase
    {

        private readonly ILogger<SaleTransactionsController> _logger;

        public SaleTransactionsController(ILogger<SaleTransactionsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult Get(string prodName, string customerName)
        {
            string jsonDb = Path.Combine(Directory.GetCurrentDirectory(), $"Data\\SaleTransactionData.json");
            var jsonString = System.IO.File.ReadAllText(jsonDb);
            var responseresult = Newtonsoft.Json.JsonConvert.DeserializeObject<SaleTransactionList>(jsonString);

            if (responseresult != null)
            {

                if (!String.IsNullOrEmpty(prodName)) responseresult.Sales = responseresult.Sales.Where(p => p.ProducName == prodName).ToList();
                if (!String.IsNullOrEmpty(customerName)) responseresult.Sales = responseresult.Sales.Where(p => p.CustomerName == customerName).ToList();
                      

                return Ok(responseresult);

            }

            return BadRequest();
        }

        //[HttpGet]
        //public ActionResult Get()
        //{
        //    //sales transaction
        //    string jsonDb = Path.Combine(Directory.GetCurrentDirectory(), $"Data\\SaleTransactionData.json");
        //    var jsonString = System.IO.File.ReadAllText(jsonDb);
        //    var responseresult = Newtonsoft.Json.JsonConvert.DeserializeObject<SaleTransactionList>(jsonString);


        //    //Product transaction
        //    string jsonDbProduct = Path.Combine(Directory.GetCurrentDirectory(), $"Data\\ProductData.json");
        //    var jsonStringProduct = System.IO.File.ReadAllText(jsonDbProduct);
        //    var responseresultProduct = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductList>(jsonStringProduct);

        //    //Customer transaction
        //    string jsonDbCustomer = Path.Combine(Directory.GetCurrentDirectory(), $"Data\\CustomersData.json");
        //    var jsonStringCustomer = System.IO.File.ReadAllText(jsonDbCustomer);
        //    var responseresultCustomer = Newtonsoft.Json.JsonConvert.DeserializeObject<CustomerList>(jsonStringCustomer);

        //    var saleTrans = new List<SaleTransactionView>();
        //    foreach (var item in responseresult.SaleTransactions)
        //    {
        //        saleTrans.Add(new SaleTransactionView
        //        {
        //            Id = item.Id,
        //            ProductName  = responseresultProduct.Products.FirstOrDefault(x=>x.Id == item.ProductId)?.Name ,
        //            ProductPrice  = responseresultProduct.Products.FirstOrDefault(x=>x.Id == item.ProductId)?.Price ,
        //           CustomerName   = responseresultCustomer.Customers.FirstOrDefault(x=>x.CustID == item.CustomerId)?.CustName
        //        });
        //    }

        //    return Ok(saleTrans);
        //}

        [HttpPost]
        public ActionResult Post(Sale req)
        {
            string jsonDb = Path.Combine(Directory.GetCurrentDirectory(), $"Data\\SaleTransactionData.json");
            var jsonString = System.IO.File.ReadAllText(jsonDb);
            var responseresult = Newtonsoft.Json.JsonConvert.DeserializeObject<SaleTransactionList>(jsonString);
            var SaleTransactions = responseresult.Sales;
            SaleTransactions.Add(new Sale
            {
                Id = SaleTransactions.Last().Id+1,
                ProducName = req.ProducName,
                CustomerName = req.CustomerName,
                ProdPrice = req.ProdPrice,
                Qnt = req.Qnt,
             
            });
            responseresult.Sales = SaleTransactions;
            var newJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(responseresult, Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText(jsonDb, newJsonResult);
            return Ok();
        }
       
        [HttpPut]
        public ActionResult Put(Sale req)
        {
            string jsonDb = Path.Combine(Directory.GetCurrentDirectory(), $"Data\\SaleTransactionData.json");
            var jsonString = System.IO.File.ReadAllText(jsonDb);
            var responseresult = Newtonsoft.Json.JsonConvert.DeserializeObject<SaleTransactionList>(jsonString);
            var SaleTransactions = responseresult.Sales;
            SaleTransactions.Remove(SaleTransactions.FirstOrDefault(c => c.Id == req.Id));
            SaleTransactions.Add(new Sale
            {

                Id = req.Id,
                ProducName = req.ProducName,
                CustomerName = req.CustomerName,
                ProdPrice=req.ProdPrice,
                Qnt=req.Qnt,
            });
            responseresult.Sales = SaleTransactions;
            var newJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(responseresult, Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText(jsonDb, newJsonResult);
            return Ok();
        }
        [HttpDelete]
        public ActionResult Delete(int Id)
        {
            string jsonDb = Path.Combine(Directory.GetCurrentDirectory(), $"Data\\SaleTransactionData.json");
            var jsonString = System.IO.File.ReadAllText(jsonDb);
            var responseresult = Newtonsoft.Json.JsonConvert.DeserializeObject<SaleTransactionList>(jsonString);
            var SaleTransactions = responseresult.Sales;
            SaleTransactions.Remove(SaleTransactions.FirstOrDefault(c => c.Id == Id));
            responseresult.Sales = SaleTransactions;
            var newJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(responseresult, Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText(jsonDb, newJsonResult);
            return Ok();
        }

    }
}
