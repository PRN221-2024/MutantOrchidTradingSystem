using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using DataAccess.Repository;
using DataAccess.Models;
namespace PhanSyTrongRazorPage.Pages.Admin.ManageCustomer
{
    public class CustomerProfileModel : PageModel
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomerProfileModel(ICustomerRepository customerRepository, IHttpContextAccessor httpContextAccessor)
        {
            _customerRepository = customerRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public Customer customer { get; set; }

        public IActionResult OnGet()
        {
            int? customerId = _httpContextAccessor.HttpContext.Session.GetInt32("CustomerId");
            if (customerId == null)
            {
                return RedirectToPage("/Login");
            }
            customer = _customerRepository.GetById(customerId.Value);
            if (customer == null)
            {
                return RedirectToPage("/NotFound");
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            int? customerId = _httpContextAccessor.HttpContext.Session.GetInt32("CustomerId");
            if (customerId == null)
            {
                return RedirectToPage("/Login");
            }
            var updatedCustomer = _customerRepository.GetById(customerId.Value);
            if (updatedCustomer == null)
            {
                return RedirectToPage("/NotFound");
            }


            updatedCustomer.CustomerFullName = Request.Form["customerName"];
            updatedCustomer.EmailAddress = Request.Form["customerEmail"];
            updatedCustomer.CustomerBirthday = DateTime.Parse(Request.Form["customerBirthday"]);
            updatedCustomer.Telephone = Request.Form["customerTelephone"];
            updatedCustomer.CustomerStatus = (byte?)int.Parse(Request.Form["customerStatus"]);


            _customerRepository.Update(updatedCustomer);

            return RedirectToPage();
        }
    }
}