using ECommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersProvider ordersProvider;

        public OrdersController(IOrdersProvider ordersProvider)
        {
            this.ordersProvider = ordersProvider;
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetOrders(int customerId)
        {
            var (isSuccess, orders, errorMessage) = await ordersProvider.GetOrdersAsync(customerId);
            if (isSuccess)
            {
                return Ok(orders);
            }
            return NotFound(errorMessage);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var (isSuccess, orders, errorMessage) = await ordersProvider.GetOrdersAsync();
            if (isSuccess)
            {
                return Ok(orders);
            }
            return NotFound(errorMessage);
        }
    }
}
