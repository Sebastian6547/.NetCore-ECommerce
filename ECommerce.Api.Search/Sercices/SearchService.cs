using ECommerce.Api.Search.Interfaces;
using System.Threading.Tasks;
using System.Linq;

namespace ECommerce.Api.Search.Sercices
{
    public class SearchService:ISearchService
    {
        public readonly IOrdersService ordersService;
        public readonly IProductsService productsService;
        public readonly ICustomersService customersService;

        public SearchService(IOrdersService ordersService, IProductsService productsService, ICustomersService customersService)
        {
            this.ordersService = ordersService;
            this.productsService = productsService;
            this.customersService = customersService;
        }
        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            var ordersResult = await ordersService.GetOrdersAsync(customerId);
            var productsResult = await productsService.GetProductsAsync();
            var customerResult = await customersService.GetCustomerAsync(customerId);
            if (ordersResult.IsSuccess)
            {
                foreach (var order in ordersResult.Orders)
                {
                    foreach (var item in order.Items)
                    {
                        item.ProductName = productsResult.IsSuccess?productsResult.Products.FirstOrDefault(p => p.Id == item.ProductId).Name:"Product Information is not available";
                    }
                }

                var result = new
                {
                    Customer = customerResult.IsSuccess ? customerResult.Customer : new {Name="Customer information not available"},
                    Orders = ordersResult.Orders
                };
                return (true, result);
            }
            return (false, null);
        }
    }
}
