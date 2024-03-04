using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IProductCategoryRepository
    {
        List<int> GetProductIdsByCategoryId(int categoryId);
    }

    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly AuctionItemDbContext _context;

        public ProductCategoryRepository()
        {
            _context = new AuctionItemDbContext();
        }

        public List<int> GetProductIdsByCategoryId(int categoryId)
        {
            try
            {
                List<int?> productIds = _context.ProductCategories
                .Where(pc => pc.CategoryId == categoryId)
                .Select(pc => pc.ProductId)
                .ToList();

                List<int> result = productIds.Select(id => id ?? 0).ToList();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetProductIdsByCategoryId - ProductCategoryRepository: {ex.Message}");
                throw;
            }
        }
    }
}
