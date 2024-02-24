using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace DataAccess.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly FUMiniHotelManagementContext _context;
        public CustomerRepository()
        {
            _context = new FUMiniHotelManagementContext();
        }
        public Customer GetById(int id)
        {
            try
            {
                return _context.Customers.Find(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetById - CustomerRepository: {ex.Message}");
                throw;
            }
        }

        public List<Customer> GetAll()
        {
            try
            {
                return _context.Customers.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAll - CustomerRepository: {ex.Message}");
                throw;
            }
        }

        public bool Add(Customer customer)
        {
            try
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Add - CustomerRepository: {ex.Message}");
                throw;
            }
        }

        public bool Update(Customer customer)
        {
            try
            {
                _context.Customers.Update(customer);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Update - CustomerRepository: {ex.Message}");
                throw;
            }
        }

        public bool Delete(Customer customer)
        {
            try
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Delete - CustomerRepository: {ex.Message}");
                throw;
                return false;
            }
        }
        public Customer GetByEmail(string emailAddress, string password)
        {
            try
            {
                // Check if a customer with the provided email and password exists
                return _context.Customers
                    .FirstOrDefault(c => c.EmailAddress == emailAddress && c.Password == password);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Login - CustomerRepository: {ex.Message}");
                throw;
            }
        }

        public List<Customer> GetByNameContains(string keyValue)
        {
            try
            {
                return _context.Customers.Where(c => c.CustomerFullName.ToLower().Contains(keyValue.ToLower())).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetByNameContains - CustomerRepository: {ex.Message}");
                throw;
            }
        }
    }
}
