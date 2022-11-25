using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Api.Products
{
    public interface IProductsProvider
    {
        //Model/Product
        Task<(bool IsSuccess, IEnumerable<Models.Product> Products, string ErrorMessage)> GetProductsAsync();
        Task<(bool IsSuccess, Models.Product Product, string ErrorMessage)> GetProductAsync(int id);
    }
}
