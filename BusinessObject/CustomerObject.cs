using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using DataAccess.Repository;

namespace BusinessObject
{
    public class CustomerObject
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly byte DEFAULT_CUSTOMER_STATUS;

        public CustomerObject()
        {
            _customerRepository = new CustomerRepository();
            DEFAULT_CUSTOMER_STATUS = 1;
        }

        public List<Customer> GetAll() => _customerRepository.GetAll();
        public Customer GetById(int id) => _customerRepository.GetById(id);
        public List<Customer> GetByNameContains(string keyValue) => _customerRepository.GetByNameContains(keyValue);
        public bool Delete(Customer customer) => _customerRepository.Delete(customer);
        public bool Create(Customer customer)
        {
            customer.CustomerStatus = DEFAULT_CUSTOMER_STATUS;
            return _customerRepository.Add(customer);
        }

        public bool Update(Customer updatedCustomer)
        {
            try
            {
                // Kiểm tra xem đối tượng cần cập nhật có tồn tại trong cơ sở dữ liệu không
                var existingCustomer = _customerRepository.GetById(updatedCustomer.CustomerId);

                if (existingCustomer != null)
                {
                    // Cập nhật các thuộc tính của đối tượng hiện tại với giá trị từ đối tượng được chuyển đến
                    existingCustomer.EmailAddress = updatedCustomer.EmailAddress;
                    existingCustomer.Password = updatedCustomer.Password;
                    existingCustomer.CustomerFullName = updatedCustomer.CustomerFullName;
                    existingCustomer.CustomerBirthday = updatedCustomer.CustomerBirthday;
                    existingCustomer.CustomerStatus = updatedCustomer.CustomerStatus;
                    existingCustomer.CustomerBirthday = updatedCustomer.CustomerBirthday;

                    // Gọi phương thức Update của Repository để cập nhật vào cơ sở dữ liệu
                    _customerRepository.Update(existingCustomer);

                    return true; // Cập nhật thành công
                }
                else
                {
                    // Đối tượng không tồn tại trong cơ sở dữ liệu
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có
                Console.WriteLine($"Error updating customer - CustomerObj: {ex.Message}");
                return false;
            }
        }


    }
}
