
class StockSocket {
    /**
     * @param {string} path
     * @param {object} [handlers]
     * @param {function(string, object)} [handlers.onTrade] - (symbol, latestTrade) once per symbol per message
     * @param {function(string)} [handlers.onServerError] - Finnhub/proxy error message
     * @param {function(object)} [handlers.onMessage] - every parsed message
     * @param {function(Event)} [handlers.onOpen]
     * @param {function(Event)} [handlers.onClose]
     * @param {function(Event|Error)} [handlers.onError]
     */

    constructor(path, handlers = {}) {
        this.path = path;
        this.handlers = handlers;
        this.socket = null;
        this.symbols = new Set(); // Symbols to (re)subscribe on open
    }

    get url() {
        const scheme = window.location.protocol === "https:" ? "wss" : "ws";
        return `${scheme}://${window.location.host}${this.path}`;
    }

    get isOpen() {
        return this.socket && this.socket.readyState === WebSocket.OPEN;
    }

    connect() {
        if (this.socket) return this;

        this.socket = new WebSocket(this.url);

        this.socket.addEventListener("open", (event) => {
            // Subscribe to anything requested before the connection was ready
            this.symbols.forEach((symbol) => this._send("subscribe", symbol));
            this.handlers.onOpen?.(event);
        });

        this.socket.addEventListener("message", (event) => this._handleMessage(event));
        this.socket.addEventListener("error", (event) => this.handlers.onError?.(event));
        this.socket.addEventListener("close", (event) => {
            this.socket = null;
            this.handlers.onClose?.(event);
        });

        return this;
    }

    subscribe(symbol) {
        this.symbols.add(symbol);
        if (this.isOpen) this._send("subscribe", symbol);
        return this;
    }

    unsubscribe(symbol) {
        this.symbols.delete(symbol);
        if (this.isOpen) this._send("unsubscribe", symbol);
        return this;
    }

    close() {
        if (!this.socket) return;
        this.symbols.forEach((symbol) => this.unsubscribe(symbol));
        this.socket.close();
    }

    _send(type, symbol) {
        this.socket.send(JSON.stringify({ type, symbol }));
        console.log(`${type === "subscribe" ? "Subscribed to" : "Unsubscribed from"} ${symbol}`);
    }

    _handleMessage(event) {
        let response;
        try {
            response = JSON.parse(event.data);
        } catch (e) {
            console.error("Error parsing data: ", e);
            this.handlers.onError?.(e);
            return;
        }

        switch (response.type) {
            case "ping":
                return; // Heartbeat, nothing to do

            case "error":
                console.error("Server error: ", response.msg);
                this.handlers.onServerError?.(response.msg);
                return;

            case "trade":
                this.handlers.onMessage?.(response);
                this._dispatchTrades(response.data);
                return;

            default:
                this.handlers.onMessage?.(response);
        }
    }

    _dispatchTrades(trades) {
        if (!Array.isArray(trades) || trades.length === 0) return;

        // Keep only the most recent trade per symbol (highest timestamp)
        const latestBySymbol = new Map();
        for (const trade of trades) {
            const current = latestBySymbol.get(trade.s);
            if (!current || trade.t >= current.t) {
                latestBySymbol.set(trade.s, trade);
            }
        }

        latestBySymbol.forEach((trade, symbol) => this.handlers.onTrade?.(symbol, trade));
    }
}