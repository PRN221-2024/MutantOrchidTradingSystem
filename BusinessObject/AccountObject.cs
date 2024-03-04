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
        public AccountObject()
        {
            _accountRepository = new AccountRepository();
        }
        public bool Login(string username, string password)
        {
            try
            {
               var account = _accountRepository.Login(username, password);
                if (account == null)
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return true;
        }
    }
}
