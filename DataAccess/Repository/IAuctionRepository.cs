using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IAuctionRepository
    {
        List<Auction> GetAll();
        Auction Create(Auction auction);
    }
    public class AuctionRepository : IAuctionRepository
    {
        private readonly AuctionItemDbContext _context;
        public AuctionRepository(AuctionItemDbContext context)
        {
            _context = context;
        }
        public List<Auction> GetAll()
        {
            return _context.Auctions.Include(a => a.Product).ToList();
        }
        public Auction Create(Auction auction)
        {
            try
            {
                _context.Auctions.Add(auction);
                _context.SaveChanges();
                return auction;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Create - AuctionRepository: {ex.Message}");
                throw;
            }
        }
    }
}