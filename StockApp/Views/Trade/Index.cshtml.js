// Websocket for live updates on price
(function () {
    const hiddenData = document.getElementById("details");
    const stockSymbol = hiddenData.dataset.stockSymbol;
    const displayPrice = document.getElementById("display-price");

    // Exits if missing.
    if (!stockSymbol) return;

    const wsScheme = window.location.protocol === "https:" ? "wss" : "ws";

    const socket = new WebSocket(`${wsScheme}://${window.location.host}/ws/stocks`);

    // Opening connection and subscribing.
    socket.addEventListener('open', function (event) {
        socket.send(JSON.stringify({ 'type': 'subscribe', 'symbol': stockSymbol }));
        console.log(`Subscribed to ${stockSymbol}`);
    });

    // Listening for messages
    socket.addEventListener('message', function (event) {
        console.log('Message from server ', event.data);

        try {
            const response = JSON.parse(event.data);

            if (response.type === "trade" && response.data) {
                let price = 0;
                response.data.forEach(trade => {
                    if (trade.p > price) {
                        price = trade.p;
                    }
                });
                displayPrice.innerText = price.toFixed(2);
            }
        }
        catch (e) {
            console.error("Error parsing data: ", e);
        }
    });

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
