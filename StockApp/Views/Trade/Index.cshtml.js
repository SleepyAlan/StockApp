import { StockSocket } from '~/js/StockSocket.js';

// Connects to proxy websocket and subscribes for updates.
(function () {
    const hiddenData = document.getElementById("details");
    const stockSymbol = hiddenData.dataset.stockSymbol;
    const displayPrice = document.getElementById("display-price");
    const priceArrow = document.getElementById("price-arrow");

    // Exits if missing.
    if (!stockSymbol) return;

    // Last known price
    let previousPrice = null;

    function flashColorForPriceChange(currentPrice, previousPrice) {
        displayPrice.className = "";
        priceArrow.className = "";

        void displayPrice.offsetWidth; // Forces DOM reflow to retrigger animation

        if (currentPrice > previousPrice) {
            displayPrice.classList.add("price-up");
            priceArrow.classList.add("price-up");
            priceArrow.innerText = "▲";
        } else if (currentPrice < previousPrice) {
            displayPrice.classList.add("price-down");
            priceArrow.classList.add("price-down");
            priceArrow.innerText = "▼";
        }
    }

    const stockSocket = new StockSocket("/ws/stocks", {
        onTrade(symbol, trade) {
            if (symbol !== stockSymbol) return;

            const currentPrice = trade.p;
            displayPrice.innerText = currentPrice.toFixed(2);

            if (previousPrice != null) {
                flashColorForPriceChange(currentPrice, previousPrice);
            }
            previousPrice = currentPrice;
        },
        onServerError(msg) {
            console.warn("Price feed error:", msg);
        },
    });

    stockSocket.connect().subscribe(stockSymbol);

    // Unsubscribe and close the socket on page unload.
    window.addEventListener("beforeunload", () => stockSocket.close());
})();