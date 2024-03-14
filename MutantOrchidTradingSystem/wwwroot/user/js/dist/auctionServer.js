const connection = new signalR.HubConnectionBuilder()
    .withUrl("/signalrServer")
    .build();

connection.start().then(() => {
    console.log("SignalR Connected.");
}).catch(err => console.error(err.toString()));

connection.on("ReceiveBid", (auctionId) => {
    console.log("AuctionsId: " + auctionId);
    refreshBidTable(auctionId);
    //location.href = "/User/Bid?id=" + auctionId;
});

function refreshBidTable(auctionId) {
    fetch('/AuctionJson?handler=AuctionData&id=' + auctionId) 
        .then(response => response.json())
        .then(data => {

    const bidTableBody = document.getElementById('bidTableBody');
            bidTableBody.innerHTML = ''; // Clear current content
            console.log("data: " + JSON.stringify(data));
            data.Bids.$values.forEach(bid => {
        const row = document.createElement('tr');
        row.innerHTML = `
            <td>
                <div class="d-flex align-items-center gap-2">
                    <img src="~/user/img/auction/images/avatar-5.png"
                         class="dashboard-avatar" alt="avatar">
                    <p>${bid.FullName}</p>
                </div>
            </td>
            <td>${bid.Address}</td>
            <td>${bid.Amount}</td>
            <td>${bid.BidTime}</td>
        `;
        bidTableBody.appendChild(row);
            });

           
            
            document.getElementById('latestBid').innerText = data.CurrentBid;
            document.getElementById('totalBids').innerText = data.BidCount;
        })
        .catch(error => console.error('Error:', error));
}