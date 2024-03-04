using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IOrderDetailRepository
    {
        OrderDetail AddOrderDetail(OrderDetail orderDetail);

    }
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly AuctionItemDbContext _context;
        public OrderDetailRepository()
        {
            _context = new AuctionItemDbContext();
        }
        public OrderDetail AddOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                OrderDetail newOrderDetail = new OrderDetail
                {
                    OrderId = orderDetail.OrderId,
                    ProductId = orderDetail.ProductId,
                    Quantity = orderDetail.Quantity,
                    Price = orderDetail.Price
                };

                _context.OrderDetails.Add(newOrderDetail);
                _context.SaveChanges();
                return newOrderDetail;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AddOrderDetail - OrderDetailRepository: {ex.Message}");
                throw;
            }
        }
    }
}
