using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IRoleAccountRepository
    {
        void AddRoleAccount(RoleAccount roleAccount);
    }
    public class RoleAccountRepository : IRoleAccountRepository
    {
        private readonly AuctionItemDbContext _context;
        public RoleAccountRepository()
        {
            _context = new AuctionItemDbContext();
        }
        public void AddRoleAccount(RoleAccount roleAccount)
        {
            try
            {
                RoleAccount newRoleAccount = new RoleAccount
                {
                    RoleId = roleAccount.RoleId,
                    AccountId = roleAccount.AccountId,
                    Status = roleAccount.Status
                };

                _context.RoleAccounts.Add(newRoleAccount);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AddRoleAccount - RoleAccountRepository: {ex.Message}");
                throw;
            }
        }
    }   
}
