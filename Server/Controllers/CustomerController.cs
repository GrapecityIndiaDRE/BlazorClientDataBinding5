using BlazorDataBinding.Shared;
using C1.DataCollection;
using C1.DataCollection.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorDataBinding.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private static IEnumerable<Customers> _customers = Customers.GetCustomers(200);
        private readonly ILogger<CustomerController> logger;
        public CustomerController(ILogger<CustomerController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public async Task<CustomerResponse> Get()
        {   
            var skip = 0;
            var take = 0;
            int.TryParse(Request.Query?["skip"].FirstOrDefault(), out skip);
            int.TryParse(Request.Query?["take"].FirstOrDefault(), out take);

            var customers = _customers.ToList();

            #region filter
            var filter = Request.Query?["filter"].FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                var options = new JsonSerializerOptions { Converters = { new FilterExpressionJsonConverter() } };
                var filterExpression = JsonSerializer.Deserialize<FilterExpression>(filter, options);
                var filterCollection = new C1FilterDataCollection<Customers>(customers);
                await filterCollection.FilterAsync(filterExpression);
                customers = filterCollection.ToList();
            }
            #endregion

            #region sorting
            var sort = Request.Query?["sort"].FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(sort))
            {
                var options = new JsonSerializerOptions { Converters = { new SortDescriptionJsonConverter() } };
                var sortDescriptions = JsonSerializer.Deserialize<SortDescription[]>(sort, options);
                var sortCollection = new C1SortDataCollection<Customers>(customers);
                await sortCollection.SortAsync(sortDescriptions);
                customers = sortCollection.ToList();
            }
            #endregion
            return new CustomerResponse { TotalCount = customers.Count, Customers = customers.Skip(skip).Take(take) };
        }
    }
}
