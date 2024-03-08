using DataAccess.Models;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class AccountObject
    {
        private readonly AccountRepository _accountRepository;
        private readonly RoleAccountRepository _roleAccountRepository;
        public AccountObject()
        {
            _accountRepository = new AccountRepository();
            _roleAccountRepository = new RoleAccountRepository();
        }
        public Account Login(string username, string password)
        {
            try
            {
               var account = _accountRepository.Login(username, password);
                if (account == null)
                {
                    return null;
                }
                return account;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }
        public void Register(Account account)
        {
            try
            {
                _accountRepository.Register(account);
                RoleAccount roleAccount = new RoleAccount
                {
                    RoleId = 2,
                    AccountId = account.Id,
                    Status = true
                };
                _roleAccountRepository.AddRoleAccount(roleAccount);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Account> GetAllAccounts() => _accountRepository.GetAll();
    }
}
