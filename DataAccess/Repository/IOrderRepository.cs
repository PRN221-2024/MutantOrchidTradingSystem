using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IOrderRepository
    {
        Order Add(Order order);
    }

    public class OrderRepository : IOrderRepository
    {
        private readonly AuctionItemDbContext _context;
        public OrderRepository()
        {
            _context = new AuctionItemDbContext();
        }

        public Order Add(Order order)
        {
            try
            {
                Order newOrder = new Order
                {
                   AccountId = order.AccountId,
                   Name = order.Name,
                   Created = order.Created,
                   Status = order.Status

                };

                _context.Orders.Add(newOrder);
                _context.SaveChanges();
                return newOrder;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Add - OrderRepository: {ex.Message}");
                throw;
            }
        }
    }
}
