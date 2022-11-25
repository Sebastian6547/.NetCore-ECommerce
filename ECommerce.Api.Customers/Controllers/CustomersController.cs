using ECommerce.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersProvider customersProvider;

        public CustomersController(ICustomersProvider customersProvider)
        {
            this.customersProvider = customersProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var (isSuccess, customers, errorMessage) = await customersProvider.GetCustomersAsync();
            if (isSuccess)
            {
                return Ok(customers);
            }
            return NotFound(errorMessage);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerAsync(int id)
        {
            var (isSuccess, customer, errorMessage) = await customersProvider.GetCustomerAsync(id);
            if (isSuccess)
            {
                return Ok(customer);
            }
            return NotFound(errorMessage);
        }
    }
}
