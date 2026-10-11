// Websocket for live updates on price
(function () {
    const hiddenData = document.getElementById("details");
    const stockSymbol = hiddenData.dataset.stockSymbol;
    const displayPrice = document.getElementById("display-price");

    // Last known price
    let previousPrice = null;

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

            if (response.type === "trade" && response.data && response.data.length > 0) {
                const latestTrade = response.data[response.data.length - 1];
                const currentPrice = latestTrade.p;
                displayPrice.innerText = currentPrice.toFixed(2);
                if (previousPrice != null) {
                    flashColorForPriceChange(currentPrice, previousPrice);
                }
                previousPrice = currentPrice;
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

    var flashColorForPriceChange = function (currentPrice, previousPrice) {
        displayPrice.className = "";
        void displayPrice.offsetWidth; // Forces DOM reflow to retrigger animation
        if (currentPrice > previousPrice) {
            displayPrice.classList.add("price-up");
        }
        else {
            displayPrice.classList.add("price-down");
        }
    }

})();
