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
        private readonly AuctionItemDbContext _context;

        public ProductRepository()
        {
            _context = new AuctionItemDbContext();
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

        public Product UpdateProduct(Product updatedProduct)
        {
            try
            {
                Product existingProduct = _context.Products.FirstOrDefault(p => p.Id == updatedProduct.Id);
                if (existingProduct != null)
                {
                    existingProduct.Name = updatedProduct.Name;
                    existingProduct.Description = updatedProduct.Description;
                    existingProduct.Price = updatedProduct.Price;
                    existingProduct.Quantity = updatedProduct.Quantity;
                    existingProduct.Path = updatedProduct.Path;
                    existingProduct.Status = updatedProduct.Status;
                    _context.Products.Update(existingProduct);
                    _context.SaveChanges();
                    return existingProduct;
                }
                else
                {
                    Console.WriteLine($"The product was not found!");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateProduct - ProductRepository: {ex.Message}");
                throw;
            }
        }

        public Product CreateProduct(Product newProduct)
        {          
            try
            {
                Product existingProduct = _context.Products.FirstOrDefault(p => p.Id == newProduct.Id);
                if (existingProduct == null)
                {
                    newProduct.Status = true;
                    _context.Products.Add(newProduct);
                    _context.SaveChanges();
                    return newProduct;
                }
                else
                {
                    Console.WriteLine($"The product already existed!");
                    return null;
                }
            }catch(Exception ex)
            {
                Console.WriteLine($"Error in CreateProduct - ProductRepository: {ex.Message}");
                throw;
            }
        }
    }
}
