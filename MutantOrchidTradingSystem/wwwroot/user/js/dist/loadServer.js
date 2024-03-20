const connection = new signalR.HubConnectionBuilder()
    .withUrl("/signalrServer")
    .build();

connection.start().then(() => {
    console.log("SignalR Connected load.");
}).catch(err => console.error(err.toString()));
connection.on("UpdateProfile", function () {
    
    location.href = "/User/Profile";
});