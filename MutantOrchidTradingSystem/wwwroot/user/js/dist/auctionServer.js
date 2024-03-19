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

connection.on("UpdateAuctionList", function () {
    // Refresh room list
    location.href = "/User/Auction";
});
function refreshBidTable(auctionId) {
    fetch('/AuctionJson?handler=AuctionData&id=' + auctionId) 
        .then(response => response.json())
        .then(data => {

    const bidTableBody = document.getElementById('bidTableBody');
            bidTableBody.innerHTML = ''; // Clear current content
            console.log("data: " + JSON.stringify(data));
            data.Bids.$values.forEach(bid =>
            {
        const row = document.createElement('tr');
        row.innerHTML = `
            <td>
                <div class="d-flex align-items-center gap-2">
                    <img src="/user/img/auction/images/avatar-5.png"
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
//            const containerElement = document.getElementById('AuctionId');
//            containerElement.innerHTML = '';
//            const newAuctionElement = document.createElement('div');


//            // Thiết lập thuộc tính và nội dung của phần tử div mới
//            newAuctionElement.className = 'card-image';
           

//            // Thiết lập nội dung HTML của phần tử div mới
//            newAuctionElement.innerHTML = `
   
//        <img src="${data.Product.Path}" alt="auction-card-img">
//        <div class="timer-wrapper">
//            <div class="timer-inner" id="time-${data.Auction.Id}"></div>
//        </div>
    
//`;

//            // Thêm phần tử div mới vào trong DOM
          
//            containerElement.appendChild(newAuctionElement);

            // Cập nhật thông tin về đấu giá
            document.getElementById('latestBid').innerText = data.CurrentBid;
            document.getElementById('totalBids').innerText = data.BidCount;
        })
        .catch(error => console.error('Error:', error));
}