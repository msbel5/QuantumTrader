using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Binance.Common;
using Binance.Spot;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Binance.Common;
using Binance.Spot;
using Binance.Spot.Models;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Binance.Common;
using Binance.Spot;
using Microsoft.Extensions.Logging;



namespace QuantumTrader.Services;

public class BinanceAccountTrade
{
    private readonly ILogger<BinanceAccountTrade> logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string baseUrl;
    private readonly string apiKey;
    private readonly string apiSecret;

    public BinanceAccountTrade(ILogger<BinanceAccountTrade> _logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        logger = _logger;
        _httpClientFactory = httpClientFactory;
        apiKey = configuration["BinanceTestApi:ApiKey"] ?? throw new ArgumentNullException("ApiKey is missing in the configuration");
        apiSecret = configuration["BinanceTestApi:ApiSecret"] ?? throw new ArgumentNullException("ApiSecret is missing in the configuration");
        baseUrl = configuration["BinanceTestApi:BaseUrl"] ?? throw new ArgumentNullException("BaseUrl is missing in the configuration");

    }


    // AccountInfo
    public async Task AccountInfo()
    {
        var websocket = new WebSocketApi(baseUrl: "wss://testnet.binance.vision/ws-api/v3", apiKey: "apiKey", signatureService: new BinanceRsa(apiSecret));

        websocket.OnMessageReceived(
            async (data) =>
            {
                logger.LogInformation(data);
                await Task.CompletedTask;
            }, CancellationToken.None);

        await websocket.ConnectAsync(CancellationToken.None);

        await websocket.AccountTrade.AccountInfoAsync();
        await websocket.AccountTrade.AccountInfoAsync(recvWindow: 6000, requestId: 123);
        await websocket.AccountTrade.AccountInfoAsync(recvWindow: 6000, cancellationToken: CancellationToken.None);

        await Task.Delay(3000);
        logger.LogInformation("Disconnect with WebSocket Server");
        await websocket.DisconnectAsync(CancellationToken.None);
    }

    // AccountOrderRateLimits
    public async Task AccountOrderRateLimits()
    {
        var websocket = new WebSocketApi("wss://testnet.binance.vision/ws-api/v3", "apiKey", new BinanceHmac(apiSecret));

        websocket.OnMessageReceived(
            async (data) =>
            {
                logger.LogInformation(data);
                await Task.CompletedTask;
            }, CancellationToken.None);

        await websocket.ConnectAsync(CancellationToken.None);

        await websocket.AccountTrade.AccountOrderRateLimitsAsync();
        await websocket.AccountTrade.AccountOrderRateLimitsAsync(recvWindow: 6000, requestId: 123);
        await websocket.AccountTrade.AccountOrderRateLimitsAsync(recvWindow: 6000, cancellationToken: CancellationToken.None);

        await Task.Delay(3000);
        logger.LogInformation("Disconnect with WebSocket Server");
        await websocket.DisconnectAsync(CancellationToken.None);
    }


    // AccountPreventedMatches
    public async Task AccountPreventedMatches()
    {
        var websocket = new WebSocketApi("wss://testnet.binance.vision/ws-api/v3", "apiKey", new BinanceHmac(apiSecret));

        websocket.OnMessageReceived(
            async (data) =>
            {
                logger.LogInformation(data);
                await Task.CompletedTask;
            }, CancellationToken.None);

        await websocket.ConnectAsync(CancellationToken.None);

        await websocket.AccountTrade.AccountPreventedMatchesAsync("BNBUSDT", 1);
        await websocket.AccountTrade.AccountPreventedMatchesAsync(symbol: "BNBUSDT", orderId: 1, recvWindow: 6000, requestId: 123);

        await Task.Delay(3000);
        logger.LogInformation("Disconnect with WebSocket Server");
        await websocket.DisconnectAsync(CancellationToken.None);
    }

    // AccountTradeList
    public async Task AccountTradeList()
    {
        var websocket = new WebSocketApi("wss://testnet.binance.vision/ws-api/v3", "apiKey", new BinanceHmac(apiSecret));

        websocket.OnMessageReceived(
            async (data) =>
            {
                logger.LogInformation(data);
                await Task.CompletedTask;
            }, CancellationToken.None);

        await websocket.ConnectAsync(CancellationToken.None);

        await websocket.AccountTrade.AccountTradeListAsync("BNBUSDT", 1);
        await websocket.AccountTrade.AccountTradeListAsync(symbol: "BNBUSDT", startTime: 0, recvWindow: 6000, requestId: 123);

        await Task.Delay(3000);
        logger.LogInformation("Disconnect with WebSocket Server");
        await websocket.DisconnectAsync(CancellationToken.None);
    }

    // AllOcoOrders
    public async Task AllOcoOrders()
    {
        var websocket = new WebSocketApi("wss://testnet.binance.vision/ws-api/v3", "apiKey", new BinanceHmac(apiSecret));

        websocket.OnMessageReceived(
            async (data) =>
            {
                logger.LogInformation(data);
                await Task.CompletedTask;
            }, CancellationToken.None);

        await websocket.ConnectAsync(CancellationToken.None);

        await websocket.AccountTrade.AllOcoOrdersAsync(1);
        await websocket.AccountTrade.AllOcoOrdersAsync(startTime: 0, recvWindow: 6000, requestId: 123);

        await Task.Delay(3000);
        logger.LogInformation("Disconnect with WebSocket Server");
        await websocket.DisconnectAsync(CancellationToken.None);
    }

    // AllOrders
    public async Task AllOrders()
    {
        var websocket = new WebSocketApi("wss://testnet.binance.vision/ws-api/v3", "apiKey", new BinanceHmac(apiSecret));

        websocket.OnMessageReceived(
            async (data) =>
            {
                logger.LogInformation(data);
                await Task.CompletedTask;
            }, CancellationToken.None);

        await websocket.ConnectAsync(CancellationToken.None);

        await websocket.AccountTrade.AllOrdersAsync("BNBUSDT", 1);
        await websocket.AccountTrade.AllOrdersAsync(symbol: "BNBUSDT", startTime: 0, recvWindow: 6000, requestId: 123);

        await Task.Delay(3000);
        logger.LogInformation("Disconnect with WebSocket Server");
        await websocket.DisconnectAsync(CancellationToken.None);
    }

    // CancelAllOpenOrdersOnASymbol
    public async Task CancelAllOpenOrdersOnASymbol()
    {
        var websocket = new WebSocketApi("wss://testnet.binance.vision/ws-api/v3", "apiKey", new BinanceHmac(apiSecret));

        websocket.OnMessageReceived(
            async (data) =>
            {
                logger.LogInformation(data);
                await Task.CompletedTask;
            }, CancellationToken.None);

        await websocket.ConnectAsync(CancellationToken.None);

        await websocket.AccountTrade.CancelAllOpenOrdersOnASymbolAsync(symbol: "BNBUSDT");
        await Task.Delay(3000);
        logger.LogInformation("Disconnect with WebSocket Server");
        await websocket.DisconnectAsync(CancellationToken.None);
    }


    // CancelAndReplaceOrder
    public async Task CancelAndReplaceOrder()
    {
        var websocket = new WebSocketApi("wss://testnet.binance.vision/ws-api/v3", "apiKey", new BinanceHmac(apiSecret));

        websocket.OnMessageReceived(
            async (data) =>
            {
                logger.LogInformation(data);
                await Task.CompletedTask;
            }, CancellationToken.None);

        await websocket.ConnectAsync(CancellationToken.None);

        await websocket.AccountTrade.CancelAndReplaceOrderAsync("BNBUSDT", Side.SELL, OrderType.LIMIT, "STOP_ON_FAILURE", cancelOrderId: 12, timeInForce: TimeInForce.GTC, quantity: 10.1m, price: 295.92m);
        await Task.Delay(3000);
        logger.LogInformation("Disconnect with WebSocket Server");
        await websocket.DisconnectAsync(CancellationToken.None);
    }

    // CancelOcoOrder
    public async Task CancelOcoOrder()
    {
        var websocket = new WebSocketApi("wss://testnet.binance.vision/ws-api/v3", "apiKey", new BinanceHmac(apiSecret));

        websocket.OnMessageReceived(
            async (data) =>
            {
                logger.LogInformation(data);
                await Task.CompletedTask;
            }, CancellationToken.None);

        await websocket.ConnectAsync(CancellationToken.None);

        await websocket.AccountTrade.CancelOcoOrderAsync(symbol: "BNBUSDT", listClientOrderId: "listClientOrderId");

        await Task.Delay(3000);
        logger.LogInformation("Disconnect with WebSocket Server");
        await websocket.DisconnectAsync(CancellationToken.None);
    }


    //  CancelOrder
    public async Task CancelOrder()
    {
        var websocket = new WebSocketApi("wss://testnet.binance.vision/ws-api/v3", "apiKey", new BinanceHmac(apiSecret));

        websocket.OnMessageReceived(
            async (data) =>
            {
                logger.LogInformation(data);
                await Task.CompletedTask;
            }, CancellationToken.None);

        await websocket.ConnectAsync(CancellationToken.None);

        await websocket.AccountTrade.CancelOrderAsync(symbol: "BNBUSDT", origClientOrderId: "origClientOrderId");
        await Task.Delay(3000);
        logger.LogInformation("Disconnect with WebSocket Server");
        await websocket.DisconnectAsync(CancellationToken.None);
    }

    // CurrentOpenOcoOrders
    public async Task CurrentOpenOcoOrders()
    {
        var websocket = new WebSocketApi("wss://testnet.binance.vision/ws-api/v3", "apiKey", new BinanceHmac(apiSecret));

        websocket.OnMessageReceived(
            async (data) =>
            {
                logger.LogInformation(data);
                await Task.CompletedTask;
            }, CancellationToken.None);

        await websocket.ConnectAsync(CancellationToken.None);

        await websocket.AccountTrade.CurrentOpenOcoOrdersAsync();

        await Task.Delay(3000);
        logger.LogInformation("Disconnect with WebSocket Server");
        await websocket.DisconnectAsync(CancellationToken.None);
    }


    // CurrentOpenOrders
    public async Task CurrentOpenOrders()
    {
        var websocket = new WebSocketApi("wss://testnet.binance.vision/ws-api/v3", "apiKey", new BinanceHmac(apiSecret));

        websocket.OnMessageReceived(
            async (data) =>
            {
                logger.LogInformation(data);
                await Task.CompletedTask;
            }, CancellationToken.None);

        await websocket.ConnectAsync(CancellationToken.None);

        await websocket.AccountTrade.CurrentOpenOrdersAsync(symbol: "BNBUSDT");

        await Task.Delay(3000);
        logger.LogInformation("Disconnect with WebSocket Server");
        await websocket.DisconnectAsync(CancellationToken.None);
    }

    // NewOcoOrder
    public async Task NewOcoOrder()
    {
        var websocket = new WebSocketApi("wss://testnet.binance.vision/ws-api/v3", "apiKey", new BinanceHmac(apiSecret));

        websocket.OnMessageReceived(
            async (data) =>
            {
                logger.LogInformation(data);
                await Task.CompletedTask;
            }, CancellationToken.None);

        await websocket.ConnectAsync(CancellationToken.None);

        await websocket.AccountTrade.NewOcoOrderAsync(symbol: "BTCUSDT", side: Side.SELL, quantity: 0.1m, price: 400.15m, stopPrice: 390.3m, selfTradePreventionMode: SelfTradePreventionMode.EXPIRE_BOTH);

        await Task.Delay(3000);
        logger.LogInformation("Disconnect with WebSocket Server");
        await websocket.DisconnectAsync(CancellationToken.None);
    }

    // NewOrder
    public async Task NewOrder()
    {
        var websocket = new WebSocketApi("wss://testnet.binance.vision/ws-api/v3", "apiKey", new BinanceHmac(apiSecret));

        websocket.OnMessageReceived(
            async (data) =>
            {
                logger.LogInformation(data);
                await Task.CompletedTask;
            }, CancellationToken.None);

        await websocket.ConnectAsync(CancellationToken.None);

        await websocket.AccountTrade.NewOrderAsync(symbol: "BNBUSDT", side: Side.BUY, type: OrderType.LIMIT, timeInForce: TimeInForce.GTC, price: 300, quantity: 1, cancellationToken: CancellationToken.None);

        await Task.Delay(3000);
        logger.LogInformation("Disconnect with WebSocket Server");
        await websocket.DisconnectAsync(CancellationToken.None);
    }


    // QueryOcoOrder
    public async Task QueryOcoOrder()
    {
        var websocket = new WebSocketApi("wss://testnet.binance.vision/ws-api/v3", "apiKey", new BinanceHmac(apiSecret));

        websocket.OnMessageReceived(
            async (data) =>
            {
                logger.LogInformation(data);
                await Task.CompletedTask;
            }, CancellationToken.None);

        await websocket.ConnectAsync(CancellationToken.None);

        await websocket.AccountTrade.QueryOcoOrderAsync(origClientOrderId: "vk6UrxuEBfBWqmKxTADNAM");

        await Task.Delay(3000);
        logger.LogInformation("Disconnect with WebSocket Server");
        await websocket.DisconnectAsync(CancellationToken.None);
    }

    // QueryOrder
    public async Task QueryOrder()
    {
        var websocket = new WebSocketApi("wss://testnet.binance.vision/ws-api/v3", "apiKey", new BinanceHmac(apiSecret));

        websocket.OnMessageReceived(
            async (data) =>
            {
                logger.LogInformation(data);
                await Task.CompletedTask;
            }, CancellationToken.None);

        await websocket.ConnectAsync(CancellationToken.None);

        await websocket.AccountTrade.QueryOrderAsync(symbol: "BNBUSDT", origClientOrderId: "vk6UrxuEBfBWqmKxTADNAM");

        await Task.Delay(3000);
        logger.LogInformation("Disconnect with WebSocket Server");
        await websocket.DisconnectAsync(CancellationToken.None);
    }

    // TestNewOrder
    public async Task TestNewOrder()
    {
        var websocket =
            new WebSocketApi("wss://testnet.binance.vision/ws-api/v3", "apiKey", new BinanceHmac(apiSecret));

        websocket.OnMessageReceived(
            async (data) =>
            {
                logger.LogInformation(data);
                await Task.CompletedTask;
            }, CancellationToken.None);

        await websocket.ConnectAsync(CancellationToken.None);

        await websocket.AccountTrade.TestNewOrderAsync(symbol: "BNBUSDT", side: Side.BUY,
            type: OrderType.LIMIT, timeInForce: TimeInForce.GTC, price: 300, quantity: 1,
            cancellationToken: CancellationToken.None);

        await Task.Delay(3000);
        logger.LogInformation("Disconnect with WebSocket Server");
        await websocket.DisconnectAsync(CancellationToken.None);
    }


}


