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

        private IMapper _mapper;

        public ProductObject()
        {
            _productRepository = new ProductRepository();
        }

        public List<Product> GetAllProducts() => _productRepository.GetAll();
    }
}
