using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AuctionItemDBContext _context;

        public ProductRepository()
        {
            _context = new AuctionItemDBContext();
        }

        public List<Product> GetAll()
        {
            try
            {
                var ProductList = _context.Products.ToList();
                return ProductList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAll - ProductRepository: {ex.Message}");
                throw;
            }
        }

        public Product GetById(int productId)
        {
            return _context.Products.FirstOrDefault(p => p.Id == productId);
        }
    }
}
