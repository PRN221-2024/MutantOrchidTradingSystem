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

        public async Task UpdateDepositList()
        {
            await Clients.All.SendAsync("UpdateDepositList");
        }

        public async Task UpdateProfile()
        {
            await Clients.All.SendAsync("UpdateProfile");
        }

        public async Task SendBid(int auctionId)
        {
            
            
            await Clients.All.SendAsync("ReceiveBid", auctionId);
        }
    }
}
