using System.Threading;
using System.Threading.Tasks;
using Binance.Common;
using Binance.Spot;
using Microsoft.Extensions.Logging;


namespace QuantumTrader.Services;

public class BinanceUserDataStream
{

    private readonly ILogger<BinanceUserDataStream> logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string baseUrl;
    private readonly string apiKey;
    private readonly string apiSecret;


    public BinanceUserDataStream(ILogger<BinanceUserDataStream> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        logger = logger;
        _httpClientFactory = httpClientFactory;
        apiKey = configuration["BinanceTestApi:ApiKey"] ?? throw new ArgumentNullException("ApiKey is missing in the configuration");
        apiSecret = configuration["BinanceTestApi:ApiSecret"] ?? throw new ArgumentNullException("ApiSecret is missing in the configuration");
        baseUrl = configuration["BinanceTestApi:BaseUrl"] ?? throw new ArgumentNullException("BaseUrl is missing in the configuration");

    }



    // PingListenKey
    public async Task PingListenKey()
    {

        // Create WebSocket API
        var websocket = new WebSocketApi("wss://testnet.binance.vision/ws-api/v3", "apiKey", new BinanceHmac(apiSecret));

        // Receive WebSocket API Response
        websocket.OnMessageReceived(
            async (data) =>
            {
                logger.LogInformation(data);
                await Task.CompletedTask;
            }, CancellationToken.None);

        await websocket.ConnectAsync(CancellationToken.None);

        await websocket.UserDataStream.PingListenKeyAsync("pfA3JuofMikuuSqLBlEuhu0iyBaidse16v26pqYQQC0NwHatiXUyTDTt0j5q", cancellationToken: CancellationToken.None);

        // wait for 5s before disconnected
        await Task.Delay(5000);
        logger.LogInformation("Disconnect with WebSocket Server");
        await websocket.DisconnectAsync(CancellationToken.None);
    }

    // CreateListenKey
    public async Task CreateListenKey()
    {
        // Create WebSocket API
        var websocket = new WebSocketApi("wss://testnet.binance.vision/ws-api/v3", "apiKey", new BinanceHmac(apiSecret));

        // Receive WebSocket API Response
        websocket.OnMessageReceived(
            async (data) =>
            {
                logger.LogInformation(data);
                await Task.CompletedTask;
            }, CancellationToken.None);

        await websocket.ConnectAsync(CancellationToken.None);

        await websocket.UserDataStream.CreateListenKeyAsync(cancellationToken: CancellationToken.None);

        // wait for 5s before disconnected
        await Task.Delay(5000);
        logger.LogInformation("Disconnect with WebSocket Server");
        await websocket.DisconnectAsync(CancellationToken.None);
    }

    // CloseListenKey
    public async Task CloseListenKey()
    {
        // Create WebSocket API
        var websocket = new WebSocketApi("wss://testnet.binance.vision/ws-api/v3", "apiKey", new BinanceHmac(apiSecret));

        // Receive WebSocket API Response
        websocket.OnMessageReceived(
            async (data) =>
            {
                logger.LogInformation(data);
                await Task.CompletedTask;
            }, CancellationToken.None);

        await websocket.ConnectAsync(CancellationToken.None);

        await websocket.UserDataStream.CloseListenKeyAsync("pfA3JuofMikuuSqLBlEuhu0iyBaidse16v26pqYQQC0NwHatiXUyTDTt0j5q", cancellationToken: CancellationToken.None);

        // wait for 5s before disconnected
        await Task.Delay(5000);
        logger.LogInformation("Disconnect with WebSocket Server");
        await websocket.DisconnectAsync(CancellationToken.None);
    }

}