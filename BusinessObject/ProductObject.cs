using AutoMapper;
using DataAccess.Models;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class ProductObject
    {
        private readonly ProductRepository _productRepository;

        public ProductObject()
        {
            _productRepository = new ProductRepository();
        }

        public List<Product> GetAllProducts() => _productRepository.GetAll();

        public Product GetById(int productId) => _productRepository.GetById(productId);
    }
}
