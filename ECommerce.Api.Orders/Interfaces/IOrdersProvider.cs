using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Interfaces
{
    public interface IOrdersProvider
    {
        Task<(bool IsSuccess, IEnumerable<Models.Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId);
        Task<(bool IsSuccess, IEnumerable<Models.Order> Orders, string ErrorMessage)> GetOrdersAsync();
        //Task<(bool IsSuccess, Models.Order Order, string ErrorMessage)> GetOrderAsync(int id);
        //Task<(bool IsSuccess, IEnumerable<Models.OrderItem> OrderItems, string ErrorMessage)> GetOrderItemsAsync();
        //Task<(bool IsSuccess, Models.OrderItem ModelItem, string ErrorMessage)> GetOrderItemAsync(int id);

    }
}