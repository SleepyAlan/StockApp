using FinnhubServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace StockApp.Controllers;

// TODO: This is fine for solo development. Can implement Publish/Subscribe with SignalR so it can scale.
public class FinnhubProxyWebSocket(IFinnhubService finnhubService) : Controller
{
    [Route("ws/stocks")]
    public async Task GetStockStream() 
    {
        if (!HttpContext.WebSockets.IsWebSocketRequest)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            return;
        }

        using WebSocket clientSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
        using ClientWebSocket finnhubSocket = new();

        string apiKey = finnhubService.GetApiKey();
        var finnhubWebSocketUri = new Uri($"wss://ws.finnhub.io?token={apiKey}");

        try
        {
            await finnhubSocket.ConnectAsync(finnhubWebSocketUri, CancellationToken.None);

            // Two Concurrent loop within
            var recieveFromFinnhubTask = ForwardFinnhubDataToClient(finnhubSocket, clientSocket);
            var recieveFromClientTask = ForwardClientCommandToFinnhub(clientSocket, finnhubSocket);

            // Connection stays alive until either side closes it.
            await Task.WhenAny(recieveFromFinnhubTask, recieveFromClientTask);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            // TODO: Add logging error
        }
        finally
        {
            if (clientSocket.State == WebSocketState.Open)
            {
                await clientSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
            }
        }
    }

    private async Task ForwardClientCommandToFinnhub(
        WebSocket client,
        ClientWebSocket finnhub)
    {
        var buffer = new byte[1024 * 4];
        while (finnhub.State == WebSocketState.Open && client.State == WebSocketState.Open)
        {
            var result = await finnhub.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            if (result.MessageType == WebSocketMessageType.Close)
            {
                break;
            }

            await client.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count),
                result.MessageType, result.EndOfMessage, CancellationToken.None);
        }
    }

    private async Task ForwardFinnhubDataToClient(
        ClientWebSocket finnhub,
        WebSocket client)
    {
        var buffer = new byte[1024 * 4];
        while (client.State == WebSocketState.Open && finnhub.State == WebSocketState.Open)
        {
            var result = await client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            if (result.MessageType == WebSocketMessageType.Close)
            {
                break;
            }
            await finnhub.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count),
                result.MessageType, result.EndOfMessage, CancellationToken.None);
        }
    }

}
