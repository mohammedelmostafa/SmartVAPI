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
    public class CustomersController : ControllerBase
    {

        private readonly ILogger<CustomersController> _logger;

        public CustomersController(ILogger<CustomersController> logger)
        {
            _logger = logger;
        }

        [HttpGet]

        public ActionResult Get(string CustName, string Telepohone)
        {
            string jsonDb = Path.Combine(Directory.GetCurrentDirectory(), $"Data\\CustomersData.json");
            var jsonString = System.IO.File.ReadAllText(jsonDb);
            var responseresult = Newtonsoft.Json.JsonConvert.DeserializeObject<CustomerList>(jsonString);

            if (responseresult != null)
            {
                if (!String.IsNullOrEmpty(CustName)) responseresult.Customers = responseresult.Customers.Where(p => p.CustName == CustName).ToList();
                if (!String.IsNullOrEmpty(Telepohone)) responseresult.Customers = responseresult.Customers.Where(p => p.Telephone == Telepohone).ToList();
               

                return Ok(responseresult);
            }

            return BadRequest();
        }

        [HttpPost]
        public ActionResult Post(Customer req)
        {
            string jsonDb = Path.Combine(Directory.GetCurrentDirectory(), $"Data\\CustomersData.json");
            var jsonString = System.IO.File.ReadAllText(jsonDb);
            var responseresult = Newtonsoft.Json.JsonConvert.DeserializeObject<CustomerList>(jsonString);
            var customers = responseresult.Customers;
            customers.Add(new Customer
            {
                CustID = customers.Last().CustID + 1,
                CustName = req.CustName,
                Address = req.Address,
                Telephone = req.Telephone
            });
            responseresult.Customers = customers;
            var newJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(responseresult, Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText(jsonDb, newJsonResult);
            return Ok();
        }
       
        [HttpPut]
        public ActionResult Put(Customer req)
        {
            string jsonDb = Path.Combine(Directory.GetCurrentDirectory(), $"Data\\CustomersData.json");
            var jsonString = System.IO.File.ReadAllText(jsonDb);
            var responseresult = Newtonsoft.Json.JsonConvert.DeserializeObject<CustomerList>(jsonString);
            var customers = responseresult.Customers;
            customers.Remove(customers.FirstOrDefault(c => c.CustID == req.CustID));
            customers.Add(new Customer
            {
                CustID = req.CustID,
                CustName = req.CustName,
                Address = req.Address,
                Telephone = req.Telephone
            });
            responseresult.Customers = customers;
            var newJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(responseresult, Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText(jsonDb, newJsonResult);
            return Ok();
        }
        [HttpDelete]
        public ActionResult Delete(int cusID)
        {
            string jsonDb = Path.Combine(Directory.GetCurrentDirectory(), $"Data\\CustomersData.json");
            var jsonString = System.IO.File.ReadAllText(jsonDb);
            var responseresult = Newtonsoft.Json.JsonConvert.DeserializeObject<CustomerList>(jsonString);
            var customers = responseresult.Customers;
            customers.Remove(customers.FirstOrDefault(c => c.CustID == cusID));
            responseresult.Customers = customers;
            var newJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(responseresult, Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText(jsonDb, newJsonResult);
            return Ok();
        }

    }
}
