using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IBidRepository
    {
      List<Bid>  GetBidsForAuction(int auctionId);
        Bid AddBid(Bid bid);
        List<Bid> GetListByaccountId (int accountId);
        Bid GetById(int id);
    }
    public class BidRepository : IBidRepository
    {
        private readonly AuctionItemDbContext _context;
        public BidRepository(AuctionItemDbContext context)
        {
            _context = context;
        }
        public List<Bid> GetBidsForAuction(int auctionId)
        {
            return _context.Bids.Include(a => a.Auction).Include(ac => ac.Account).Where(b => b.AuctionId == auctionId).ToList();
        }
        public Bid AddBid(Bid bid)
        {
            _context.Bids.Add(bid);
            _context.SaveChanges();
            return bid;
        }
        public Bid GetById(int id)
        {
            return _context.Bids.Include(b => b.Auction).ThenInclude(p => p.Product).
                Include(b => b.Account).FirstOrDefault(b => b.Id == id);
        }

        public List<Bid> GetListByaccountId(int accountId)
        {
            return _context.Bids
        .Include(b => b.Auction).ThenInclude(a => a.Product)
        .Include(b => b.Account)
        .Where(b => b.AccountId == accountId)
        .ToList();
        }
    }
}
