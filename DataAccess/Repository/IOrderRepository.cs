using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
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
        List<Order> GetAllByAccountID(int accountId);
        Order GetById(int orderId);

        List<Order> GetAll();

        List<Account> GetAccounts();
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

        public List<Account> GetAccounts()
        {
            try
            {
                return _context.Accounts.ToList();

            }catch(Exception ex)
            {
                Console.WriteLine($"Error id GetAccounts - OrderRepository: {ex.Message}");
                throw;
            }
        }

        public List<Order> GetAll()
        {
            try
            {
                return _context.Orders.ToList();

            }catch(Exception ex)
            {
                Console.WriteLine($"Error in GetAll - OrderRepository: {ex.Message}");
                throw;
            }
        }

        public List<Order> GetAllByAccountID(int accountId)
        {
            List<Order> orderList = _context.Orders.Where(o => o.AccountId == accountId).ToList();
            return orderList;
        }

        public Order GetById(int orderId)
        {
            Order order = _context.Orders.Include(a => a.Account).Include(o => o.OrderDetails).ThenInclude(p => p.Product).FirstOrDefault(o => o.Id == orderId);
            return order;
        }
    }
}
