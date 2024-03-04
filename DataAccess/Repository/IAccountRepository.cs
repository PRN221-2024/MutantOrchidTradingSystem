using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IAccountRepository
    {
        Account Login(string username, string password);
    }
    public class AccountRepository : IAccountRepository
    {
        private readonly AuctionItemDbContext _context;
        public AccountRepository()
        {
            _context = new AuctionItemDbContext();
        }
        public Account Login(string username, string password)
        {
            try
            {
                Account account = _context.Accounts.FirstOrDefault(a => a.Username.Equals(username) && a.Password.Equals(password)); // Kết hợp thông tin từ bảng RoleAccount

                //Include(a => a.RoleAccounts).ThenInclude(ra => ra.RoleId).
                if (account == null)
            {
                   return null;
            }
            return account;
               }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Login - AccountRepository: {ex.Message}");
                throw;
            }
        }
    }
}
