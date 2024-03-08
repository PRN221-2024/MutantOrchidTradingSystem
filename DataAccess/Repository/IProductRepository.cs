using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IProductRepository
    {
        List<Product> GetAll();
        Product GetById(int productId);
        public Product UpdateProduct (Product product);

        public Product CreateProduct (Product product);

    }
}
