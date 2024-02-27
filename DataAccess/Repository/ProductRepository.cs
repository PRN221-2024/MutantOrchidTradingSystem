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
        }

        public ProductRepository(AuctionItemDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public List<Product> GetAll()
        {
            return _context.Products.ToList();
        }

    }
}
