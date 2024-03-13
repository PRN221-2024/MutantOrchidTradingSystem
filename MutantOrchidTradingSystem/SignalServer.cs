using Microsoft.AspNetCore.SignalR;

namespace MutantOrchidTradingSysRazorPage
{
    public class SignalServer : Hub
    {
        public async Task Send(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
        public async Task SendBid(int auctionId, decimal bidAmount)
        {
            await Clients.All.SendAsync("ReceiveBid", auctionId, bidAmount);
        }
    }
}
