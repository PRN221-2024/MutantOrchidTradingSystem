using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObject
{
    public class AuthenticationObject
    {
        private readonly ICustomerRepository _customerRepository;
        IConfigurationRoot _configuration;
        public AuthenticationObject()
        {
            _customerRepository = new CustomerRepository();
            _configuration = GetConfiguration();
        }
        public Customer isCustomerLogin(string emailLogin, string password)
        {
            return _customerRepository.GetByEmail(emailLogin, password);
        }

        public string isAdminLogin(string emailLogin, string password) {
            var adminEmail = _configuration["AdminInfo:email"];
            var adminPassword = _configuration["AdminInfo:password"];
            if(emailLogin.Equals(adminEmail) && password.Equals(adminPassword))
            {
                return adminEmail;
            }
            return null;
        }
        public IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }
    }
}
