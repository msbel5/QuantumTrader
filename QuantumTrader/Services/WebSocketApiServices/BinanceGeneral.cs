using Binance.Common;
using Binance.Spot;

namespace QuantumTrader.Services;

public class BinanceGeneral
{

    private readonly ILogger<BinanceGeneral> logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string baseUrl;
    private readonly string apiKey;
    private readonly string apiSecret;


    public BinanceGeneral(ILogger<BinanceGeneral> _logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        logger = _logger;
        _httpClientFactory = httpClientFactory;
        apiKey = configuration["BinanceTestApi:ApiKey"] ?? throw new ArgumentNullException("ApiKey is missing in the configuration");
        apiSecret = configuration["BinanceTestApi:ApiSecret"] ?? throw new ArgumentNullException("ApiSecret is missing in the configuration");
        baseUrl = configuration["BinanceTestApi:BaseUrl"] ?? throw new ArgumentNullException("BaseUrl is missing in the configuration");

    }



    // Time
    public async Task Time()
    {

        // Create WebSocket API
        var websocket = new WebSocketApi();

        // Receive WebSocket API Response
        websocket.OnMessageReceived(
            async (data) =>
            {
                logger.LogInformation(data);
                await Task.CompletedTask;
            });

        await websocket.ConnectAsync();

        await websocket.General.TimeAsync();
        await websocket.General.TimeAsync(requestId: 123, CancellationToken.None);
        await websocket.General.TimeAsync(requestId: "requestId123", CancellationToken.None);

        // wait for 5s before disconnected
        await Task.Delay(5000);
        logger.LogInformation("Disconnect with WebSocket Server");
        await websocket.DisconnectAsync();

    }

    // Ping
    public async Task Ping()
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

        await websocket.General.PingAsync();
        await Task.Delay(1000);
        await websocket.General.PingAsync(requestId: 123, CancellationToken.None);
        await Task.Delay(1000);
        await websocket.General.PingAsync(requestId: null, CancellationToken.None);
        await Task.Delay(1000);
        await websocket.General.PingAsync(requestId: string.Empty, CancellationToken.None);
        await Task.Delay(1000);
        await websocket.General.PingAsync(requestId: "request123", CancellationToken.None);

        // wait for 5s before disconnected
        await Task.Delay(2000);
        logger.LogInformation("Disconnect with WebSocket Server");
        await websocket.DisconnectAsync(CancellationToken.None);
    }

    // ExchangeInfo
    public async Task ExchangeInfo()
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

        await websocket.General.ExchangeInfoAsync();
        Task.Delay(1000).Wait();
        await websocket.General.ExchangeInfoAsync(symbol: "BNBBTC", cancellationToken: CancellationToken.None);
        Task.Delay(1000).Wait();
        string[] symbols = { "BNBBTC", "BTCUSDT" };
        await websocket.General.ExchangeInfoAsync(symbols: symbols, requestId: 123);
        Task.Delay(1000).Wait();
        string[] permissions = { "LEVERAGED" };
        await websocket.General.ExchangeInfoAsync(permissions: permissions, requestId: 123);

        // Wait for 5s before disconnected
        await Task.Delay(5000);
        logger.LogInformation("Disconnect with WebSocket Server");
        await websocket.DisconnectAsync(CancellationToken.None);
    }
}