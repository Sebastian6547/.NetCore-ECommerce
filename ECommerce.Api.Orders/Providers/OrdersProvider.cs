using AutoMapper;
using ECommerce.Api.Orders.Db;
using ECommerce.Api.Orders.Interfaces;
using ECommerce.Api.Orders.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Providers
{
    public class OrdersProvider : IOrdersProvider
    {
        private readonly OrdersDbContext dbContext;
        private readonly ILogger<OrdersProvider> logger;
        private readonly IMapper mapper;

        public OrdersProvider(OrdersDbContext dbContext, ILogger<OrdersProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;
            
            SeedData();
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId)
        {
            try
            {
                var orders = await dbContext.Orders.Where(o => o.CustomerId == customerId).Include(o=>o.Items).ToListAsync();
                if (orders != null && orders.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.Order>>(orders);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Order> Orders, string ErrorMessage)> GetOrdersAsync()
        {
            try
            {
                var orders = await dbContext.Orders.Include(o => o.Items).ToListAsync();
                if (orders != null && orders.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.Order>>(orders);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        private void SeedData()
        {
            if (!dbContext.Orders.Any())
            {
                dbContext.Orders.Add(new Db.Order()
                {
                    Id = 1,
                    CustomerId = 1,
                    OrderDate = DateTime.UtcNow,
                    Total = 100,
                    Items = new List<Db.OrderItem>()
                        {
                            new Db.OrderItem()
                            {
                                Id = 1,
                                OrderId = 1,
                                ProductId = 1,
                                Quantity = 1,
                                UnitPrice = 100
                            }
                        }
                });
                dbContext.Orders.Add(new Db.Order()
                {
                    Id = 2,
                    CustomerId = 2,
                    OrderDate = DateTime.UtcNow,
                    Total = 200,
                    Items = new List<Db.OrderItem>()
                        {
                            new Db.OrderItem()
                            {
                                Id = 2,
                                OrderId = 2,
                                ProductId = 2,
                                Quantity = 1,
                                UnitPrice = 200
                            }
                        }
                });
                dbContext.Orders.Add(new Db.Order()
                {
                    Id = 3,
                    CustomerId = 3,
                    OrderDate = DateTime.UtcNow,
                    Total = 300,
                    Items = new List<Db.OrderItem>()
                        {
                            new Db.OrderItem()
                            {
                                Id = 3,
                                OrderId = 3,
                                ProductId = 3,
                                Quantity = 1,
                                UnitPrice = 300
                            }
                        }
                });
                dbContext.SaveChanges();
            }
        }
    }
}
