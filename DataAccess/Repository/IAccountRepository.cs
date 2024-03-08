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
        Account Register(Account account);
        Account GetById(int accountId);
        void Update(Account account);

        void Delete(int accountId);
        List<Account> GetAll();
    }
    public class AccountRepository : IAccountRepository
    {
        private readonly AuctionItemDbContext _context;
        public AccountRepository()
        {
            _context = new AuctionItemDbContext();
        }

        public void Delete(int accountId)
        {
            try
            {
                Account existingAccount = _context.Accounts.FirstOrDefault(p => p.Id == accountId);
                if (existingAccount != null)
                {
                    existingAccount.Status = false;
                    _context.SaveChanges();
                }
                else
                {
                    Console.WriteLine($"The product with {accountId} doest not exist!");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error in Delete - AccountRepository: {ex.Message}");
                throw;
            }
        }

        public List<Account> GetAll()
        {
            try
            {
                return _context.Accounts.ToList();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error in GetAll - AccountRepository: {ex.Message}");
                throw;
            }
        }

        public Account GetById(int accountId)
        {
            try
            {
                Account account = _context.Accounts.FirstOrDefault(a => a.Id == accountId);
                if(account == null)
                {
                    return null;
                }
                return account;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetById - AccountRepository: {ex.Message}");
                throw;
            }
        }

        public Account Login(string username, string password)
        {
            try
            {
                Account account = _context.Accounts.Include(a => a.RoleAccounts).FirstOrDefault(a => a.Username.Equals(username) && a.Password.Equals(password)); // Kết hợp thông tin từ bảng RoleAccount

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

        public Account Register(Account account)
        {
            try
            {
                Account newAccount = new Account
                {
                    Username = account.Username,
                    Password = account.Password,
                    FullName = account.FullName,
                    Email = account.Email,
                    Status = account.Status,
                    Address = account.Address,
                    Phone = account.Phone
                };

                _context.Accounts.Add(newAccount);
                _context.SaveChanges();
               return newAccount;
            }catch(Exception ex)
            {
                Console.WriteLine($"Error in Register - AccountRepository: {ex.Message}");
                throw;
            }
        }

        public void Update(Account account)
        {
            try
            {
                Account updateAccount = _context.Accounts.FirstOrDefault(a => a.Id == account.Id);
                if(updateAccount != null)
                {
                    updateAccount.Username = account.Username;
                    updateAccount.Password = account.Password;
                    updateAccount.FullName = account.FullName;
                    updateAccount.Email = account.Email; 
                    updateAccount.Status = account.Status;
                    updateAccount.Address = account.Address;
                    updateAccount.Phone = account.Phone;
                    _context.Accounts.Update(updateAccount);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Customer not exists!");
                }

            }catch(Exception ex)
            {
                Console.WriteLine($"Error in Update - AccountRepository: {ex.Message}");
                throw;
            }
        }
    }
}
