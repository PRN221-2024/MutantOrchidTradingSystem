using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace DataAccess.Repository
{
    public interface ICustomerRepository
    {
        public Customer GetById(int id);
        public List<Customer> GetAll();
        public List<Customer> GetByNameContains(string keyValue);
        public bool Add(Customer customer);
        public bool Update(Customer customer);
        public bool Delete(Customer customer);
        public Customer GetByEmail(string email, string password);
    }
}
