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
        Auction GetById(int auctionId);
        Auction UpdateStatusAuction(int auctionId);
        Auction GetAuctionById(int auctionId);
        Auction UpdateAuction(Auction auction);
        void DeleteAuction(int auctionId);
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
            return _context.Auctions.Include(a => a.Product).Include(b => b.Bids).ToList();
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

        public Auction GetById(int auctionId)
        {
           return _context.Auctions.Include(a => a.Product).Include(b => b.Bids).FirstOrDefault(a => a.Id == auctionId);
        }
        public Auction GetAuctionById(int auctionId)
        {
            return _context.Auctions.FirstOrDefault(a => a.Id == auctionId);
        }

        public Auction UpdateStatusAuction(int auctionId)
        {
            try
            {
                Auction existingAuction = GetById(auctionId);
                if (existingAuction != null)
                {
                    existingAuction.Status = false;
                    _context.Auctions.Update(existingAuction);
                    _context.SaveChanges();
                    return existingAuction;
                }
                else
                {
                    Console.WriteLine($"The auction was not found!");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Update - AuctionRepository: {ex.Message}");
                throw;
            }
        }

        public Auction UpdateAuction(Auction auction)
        {
            try
            {
                Auction existingAuction = GetAuctionById(auction.Id);
                if (existingAuction != null)
                {
                    existingAuction.ProductId = auction.ProductId;
                    existingAuction.StartTime = auction.StartTime;
                    existingAuction.EndTime = auction.EndTime;
                    existingAuction.StartingPrice = auction.StartingPrice;
                    existingAuction.Status = auction.Status;
                     _context.Auctions.Update(existingAuction);
                    _context.SaveChanges();
                    return existingAuction;
                }
                else
                {
                    Console.WriteLine($"The auction was not found!");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Update - AuctionRepository: {ex.Message}");
                throw;
            }
        }

        public void DeleteAuction(int auctionId)
        {
           try
            {
                Auction existingAuction = GetAuctionById(auctionId);
                if (existingAuction != null)
                {
                    _context.Auctions.Remove(existingAuction);
                    _context.SaveChanges();
                }
                else
                {
                    Console.WriteLine($"The auction was not found!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Delete - AuctionRepository: {ex.Message}");
                throw;
            }
        }
    }
}