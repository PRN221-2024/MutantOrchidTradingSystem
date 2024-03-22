using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IDepositRequestRepository
    {
        DepositRequest Create(DepositRequest depositRequest);
        List<DepositRequest> GetAll();
        DepositRequest GetById(int depositRequestId);
        DepositRequest UpdateDepositRequest(DepositRequest depositRequest);
        List<DepositRequest> GetListByAccountId(int accountId);
    }

    public class DepositRequestRepository : IDepositRequestRepository
    {
        private readonly AuctionItemDbContext _context;
        public DepositRequestRepository(AuctionItemDbContext context)
        {
            _context = context;
        }

        public DepositRequest Create(DepositRequest depositRequest)
        {
            try {                 
                _context.DepositRequests.Add(depositRequest);
                _context.SaveChanges();
                return depositRequest;
                       }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Create - DepositRequestRepository: {ex.Message}");
                throw;
            }
        }

        public List<DepositRequest> GetAll()
        {
            return _context.DepositRequests.ToList();
        }

        public DepositRequest GetById(int depositRequestId)
        {
            try
            {
                return _context.DepositRequests.FirstOrDefault(a => a.Id == depositRequestId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetById - DepositRequestRepository: {ex.Message}");
                throw;
            }
        }

        public List<DepositRequest> GetListByAccountId(int accountId)
        {
            try {
                    return _context.DepositRequests.Where(a => a.AccountId == accountId).ToList();
                }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetListByAccountId - DepositRequestRepository: {ex.Message}");
                throw;
            }
        }

        public DepositRequest UpdateDepositRequest(DepositRequest depositRequest)
        {
            try
            {
                var existingDepositRequest = GetById(depositRequest.Id);
                if (existingDepositRequest != null)
                {
                    existingDepositRequest.Status = depositRequest.Status;
                    _context.DepositRequests.Update(existingDepositRequest);
                    _context.SaveChanges();
                    return existingDepositRequest;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateDepositRequest - DepositRequestRepository: {ex.Message}");
                throw;
            }
        }
    }
}
