using System;
using System.Threading;
using System.Threading.Tasks;
using Binance.Spot;
using System.Threading;
using System.Threading.Tasks;
using Binance.Spot;
using Binance.Spot.Models;
using Microsoft.Extensions.Logging;

namespace QuantumTrader.Services;

public class BinanceMarket
{

    private readonly ILogger<BinanceMarket> logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string baseUrl;
    private readonly string apiKey;
    private readonly string apiSecret;


    public BinanceMarket(ILogger<BinanceMarket> _logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        logger = _logger;
        _httpClientFactory = httpClientFactory;
        apiKey = configuration["BinanceTestApi:ApiKey"] ?? throw new ArgumentNullException("ApiKey is missing in the configuration");
        apiSecret = configuration["BinanceTestApi:ApiSecret"] ?? throw new ArgumentNullException("ApiSecret is missing in the configuration");
        baseUrl = configuration["BinanceTestApi:BaseUrl"] ?? throw new ArgumentNullException("BaseUrl is missing in the configuration");

    }


    // AggregateTradesList
    public async Task AggregateTradesList()
    {
        // Create WebSocket API
        var websocket = new WebSocketApi();

        // Receive WebSocket API Response
        websocket.OnMessageReceived(
            async (data) =>
            {
                Console.WriteLine(data);
                await Task.CompletedTask;
            }, CancellationToken.None);

        await websocket.ConnectAsync(CancellationToken.None);

        await websocket.Market.AggregateTradesListAsync(symbol: "BNBUSDT", limit: 10, cancellationToken: CancellationToken.None);

        // wait for 5s before disconnected
        await Task.Delay(5000);
        logger.LogInformation("Disconnect with WebSocket Server");
        await websocket.DisconnectAsync(CancellationToken.None);
    }

    // CurrentAveragePrice
    public async Task CurrentAveragePrice()
    {
        // Create WebSocket API
        var websocket = new WebSocketApi();

        // Receive WebSocket API Response
        websocket.OnMessageReceived(
            async (data) =>
            {
                logger.LogInformation(data);
                await Task.CompletedTask;
            }, CancellationToken.None);

        await websocket.ConnectAsync(CancellationToken.None);

        await websocket.Market.CurrentAveragePriceAsync(symbol: "BNBUSDT", requestId: 123, cancellationToken: CancellationToken.None);

        // wait for 5s before disconnected
        await Task.Delay(5000);
        logger.LogInformation("Disconnect with WebSocket Server");
        await websocket.DisconnectAsync(CancellationToken.None);
    }

    // HistoricalTrades
    public async Task HistoricalTrades()
    {
        // Create WebSocket API
        var websocket = new WebSocketApi();

        // Receive WebSocket API Response
        websocket.OnMessageReceived(
            async (data) =>
            {
                logger.LogInformation(data);
                await Task.CompletedTask;
            }, CancellationToken.None);

        await websocket.ConnectAsync(CancellationToken.None);

        await websocket.Market.HistoricalTradesAsync(symbol: "BNBUSDT", limit: 10, fromId: 123, cancellationToken: CancellationToken.None);

        // wait for 5s before disconnected
        await Task.Delay(5000);
        logger.LogInformation("Disconnect with WebSocket Server");
        await websocket.DisconnectAsync(CancellationToken.None);
    }


    // Klines
    public async Task Klines()
    {
        // Create WebSocket API
        var websocket = new WebSocketApi();

        // Receive WebSocket API Response
        websocket.OnMessageReceived(
            async (data) =>
            {
                logger.LogInformation(data);
                await Task.CompletedTask;
            }, CancellationToken.None);

        await websocket.ConnectAsync(CancellationToken.None);

        await websocket.Market.KlinesAsync(symbol: "BNBUSDT", interval: Interval.ONE_SECOND, limit: 10, cancellationToken: CancellationToken.None);

        // wait for 5s before disconnected
        await Task.Delay(5000);
        logger.LogInformation("Disconnect with WebSocket Server");
        await websocket.DisconnectAsync(CancellationToken.None);

    }


    // OrderBook
    public async Task OrderBook()
    {
        // Create WebSocket API
        var websocket = new WebSocketApi();

        // Receive WebSocket API Response
        websocket.OnMessageReceived(
            async (data) =>
            {
                logger.LogInformation(data);
                await Task.CompletedTask;
            }, CancellationToken.None);

        await websocket.ConnectAsync(CancellationToken.None);

        await websocket.Market.OrderBookAsync(symbol: "BNBUSDT", limit: 10, cancellationToken: CancellationToken.None);

        // wait for 5s before disconnected
        await Task.Delay(5000);
        logger.LogInformation("Disconnect with WebSocket Server");
        await websocket.DisconnectAsync(CancellationToken.None);
    }

    // RecentTradesList
    public async Task RecentTradesList()
    {
        // Create WebSocket API
        var websocket = new WebSocketApi();

        // Receive WebSocket API Response
        websocket.OnMessageReceived(
            async (data) =>
            {
                logger.LogInformation(data);
                await Task.CompletedTask;
            }, CancellationToken.None);

        await websocket.ConnectAsync(CancellationToken.None);

        await websocket.Market.RecentTradesListAsync(symbol: "BNBUSDT", limit: 10, cancellationToken: CancellationToken.None);

        // wait for 5s before disconnected
        await Task.Delay(5000);
        logger.LogInformation("Disconnect with WebSocket Server");
        await websocket.DisconnectAsync(CancellationToken.None);
    }

    // RollingWindowPriceChangeStatistics
    public async Task RollingWindowPriceChangeStatistics()
    {
        // Create WebSocket API
        var websocket = new WebSocketApi();

        // Receive WebSocket API Response
        websocket.OnMessageReceived(
            async (data) =>
            {
                logger.LogInformation(data);
                await Task.CompletedTask;
            }, CancellationToken.None);

        await websocket.ConnectAsync(CancellationToken.None);

        string[] symbols = new string[] { "BNBUSDT", "BTCUSDT" };
        await websocket.Market.RollingWindowPriceChangeStatisticsAsync(symbol: null, symbols: symbols, type: TickerType.FULL, windowSize: "1d", cancellationToken: CancellationToken.None);

        // wait for 5s before disconnected
        await Task.Delay(5000);
        logger.LogInformation("Disconnect with WebSocket Server");
        await websocket.DisconnectAsync(CancellationToken.None);
    }

    // SymbolOrderBookTicker
    public async Task SymbolOrderBookTicker()
    {
        // Create WebSocket API
        var websocket = new WebSocketApi();

        // Receive WebSocket API Response
        websocket.OnMessageReceived(
            async (data) =>
            {
                logger.LogInformation(data);
                await Task.CompletedTask;
            }, CancellationToken.None);

        await websocket.ConnectAsync(CancellationToken.None);

        string[] symbols = new string[] { "BNBUSDT", "BTCUSDT" };
        await websocket.Market.SymbolOrderBookTickerAsync(symbol: null, symbols: symbols, cancellationToken: CancellationToken.None);

        // wait for 5s before disconnected
        await Task.Delay(5000);
        logger.LogInformation("Disconnect with WebSocket Server");
        await websocket.DisconnectAsync(CancellationToken.None);
    }

    // SymbolPriceTicker
    public async Task SymbolPriceTicker()
    {
        // Create WebSocket API
        var websocket = new WebSocketApi();

        // Receive WebSocket API Response
        websocket.OnMessageReceived(
            async (data) =>
            {
                logger.LogInformation(data);
                await Task.CompletedTask;
            }, CancellationToken.None);

        await websocket.ConnectAsync(CancellationToken.None);

        string[] symbols = new string[] { "BNBUSDT", "BTCUSDT" };
        await websocket.Market.SymbolPriceTickerAsync(symbol: null, symbols: symbols, cancellationToken: CancellationToken.None);

        // wait for 5s before disconnected
        await Task.Delay(5000);
        logger.LogInformation("Disconnect with WebSocket Server");
        await websocket.DisconnectAsync(CancellationToken.None);
    }

    // TwentyFourHrTickerPriceChangeStatistics
    public async Task TwentyFourHrTickerPriceChangeStatistics()
    {
        // Create WebSocket API
        var websocket = new WebSocketApi();

        // Receive WebSocket API Response
        websocket.OnMessageReceived(
            async (data) =>
            {
                logger.LogInformation(data);
                await Task.CompletedTask;
            }, CancellationToken.None);

        await websocket.ConnectAsync(CancellationToken.None);

        string[] symbols = new string[] { "BNBUSDT", "BTCUSDT" };
        await websocket.Market.TwentyFourHrTickerPriceChangeStatisticsAsync(symbol: null, symbols: symbols, cancellationToken: CancellationToken.None);

        // wait for 5s before disconnected
        await Task.Delay(5000);
        logger.LogInformation("Disconnect with WebSocket Server");
        await websocket.DisconnectAsync(CancellationToken.None);
    }

    // UiKlines
    public async Task UiKlines()
    {
        // Create WebSocket API
        var websocket = new WebSocketApi();

        // Receive WebSocket API Response
        websocket.OnMessageReceived(
            async (data) =>
            {
                logger.LogInformation(data);
                await Task.CompletedTask;
            }, CancellationToken.None);

        await websocket.ConnectAsync(CancellationToken.None);

        await websocket.Market.UiKlinesAsync(symbol: "BNBUSDT", interval: Interval.ONE_SECOND, limit: 10, cancellationToken: CancellationToken.None);

        // wait for 5s before disconnected
        await Task.Delay(5000);
        logger.LogInformation("Disconnect with WebSocket Server");
        await websocket.DisconnectAsync(CancellationToken.None);
    }

}