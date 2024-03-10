using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
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
        List<Product> GetProducts();

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

        public List<Product> GetProducts()
        {
            try
            {
                return _context.Products.ToList();

            }catch(Exception ex)
            {
                Console.WriteLine($"Error in Products - OrderDetailRepository: {ex.Message}");
                throw;
            }
        }
    }
}
