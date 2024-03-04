using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
    }

    public class CategoryRepository : ICategoryRepository
    {
        private readonly AuctionItemDbContext _context;
        public CategoryRepository()
        {
            _context = new AuctionItemDbContext();
        }
        public List<Category> GetAll()
        {
            try
            {
                var categoryList = _context.Categories.ToList();
                return categoryList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAll - CategoryRepository: {ex.Message}");
                throw;
            }
        }
    }
}
