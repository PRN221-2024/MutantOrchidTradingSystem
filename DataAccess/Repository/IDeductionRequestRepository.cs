using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IDeductionRequestRepository
    {
        List<DeductionRequest> GetAll();
        List<DeductionRequest> GetListByAccountId(int accountId);
        DeductionRequest Create(DeductionRequest deductionRequest);
    }

    public class DeductionRequestRepository : IDeductionRequestRepository
    {
        private readonly AuctionItemDbContext _context;
        public DeductionRequestRepository()
        {
            _context = new AuctionItemDbContext();
        }

        public DeductionRequest Create(DeductionRequest deductionRequest)
        {
            try 
            {
                _context.DeductionRequests.Add(deductionRequest);
                _context.SaveChanges();
                return deductionRequest;
            }
            catch  (Exception ex)
                               { 
                Console.WriteLine($"Error in Create - DeductionRequestRepository: {ex.Message}");
                throw;
                }
            
        }

        public List<DeductionRequest> GetAll()
        {
            try
            {
                var deductionRequestList = _context.DeductionRequests.ToList();
                return deductionRequestList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAll - DeductionRequestRepository: {ex.Message}");
                throw;
            }
        }

        public List<DeductionRequest> GetListByAccountId(int accountId)
        {
            try{
                return _context.DeductionRequests.Where(a => a.AccountId == accountId).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetListByAccountId - DeductionRequestRepository: {ex.Message}");
                throw;
            }
        }


    }
}
