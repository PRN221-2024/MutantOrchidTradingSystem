using DataAccess.DTO;
using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.SignalR;
using NuGet.Packaging.Signing;

namespace MutantOrchidTradingSysRazorPage
{
    public class SignalServer : Hub
    {
        public async Task UpdateAuctionList()
        {
            await Clients.All.SendAsync("UpdateAuctionList");
        }

        public async Task SendBid(int auctionId)
        {
            
            
            await Clients.All.SendAsync("ReceiveBid", auctionId);
        }
    }
}
