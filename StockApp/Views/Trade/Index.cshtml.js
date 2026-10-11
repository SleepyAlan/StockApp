// Websocket to keep 
// TODO: proxy websocket... Connect to websocket to backend service, websocket it to here.
(function () {
    const hiddenData = document.getElementById("details");
    const stockSymbol = hiddenData.dataset.stockSymbol;
    const token = hiddenData.dataset.apiToken;
    const displayPrice = document.getElementById("display-price");

    // Exits if either is missing
    if (!token || !stockSymbol) return;

    const socket = new WebSocket(`wss://ws.finnhub.io?token=${token}`);

    // Connection opened -> Subscribe
    socket.addEventListener('open', function (event) {
        socket.send(JSON.stringify({ 'type': 'subscribe', 'symbol': stockSymbol }));
        console.log(`Subscribed to ${stockSymbol}`);
    });

    // Listen for messages
    socket.addEventListener('message', function (event) {
        console.log('Message from server ', event.data);
        if (event.type === "trade") {
            let price = 0;
            event.data.forEach(trade => {
                if (trade.p > price) {
                    price = trade.p;
                }
            });
            displayPrice.innerText = price;
        }
    });

    // Unsubscribe
    var unsubscribe = function (symbol) {
        if (socket && socket.readyState === WebSocket.OPEN) {
            socket.send(JSON.stringify({ 'type': 'unsubscribe', 'symbol': symbol }))
            console.log(`Unsubscribed to ${stockSymbol}`);
        }
    }

    // Trigger unsubscribe and closing socket.
    window.addEventListener("beforeunload", function () {
        unsubscribe(stockSymbol);
        socket.close();
    })
})();
