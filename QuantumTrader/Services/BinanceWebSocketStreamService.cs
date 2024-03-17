using System.Net.WebSockets;
using System.Text;
using Binance.Spot;
using Newtonsoft.Json;

namespace QuantumTrader.Services;

public class BinanceWebSocketStreamService
{
    private readonly ILogger<BinanceWebSocketStreamService> logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string baseUrl;
    private readonly string apiKey;
    private readonly string apiSecret;
    private ClientWebSocket _webSocket;

    public BinanceWebSocketStreamService(ILogger<BinanceWebSocketStreamService> _logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        logger = _logger;
        _httpClientFactory = httpClientFactory;
        apiKey = configuration["BinanceTestApi:ApiKey"] ?? throw new ArgumentNullException("ApiKey is missing in the configuration");
        apiSecret = configuration["BinanceTestApi:ApiSecret"] ?? throw new ArgumentNullException("ApiSecret is missing in the configuration");
        baseUrl = configuration["BinanceTestApi:BaseUrl"] ?? throw new ArgumentNullException("BaseUrl is missing in the configuration");
        _webSocket = new ClientWebSocket();

    }

    public async Task StartStreamAsync(string streamName, CancellationToken cancellationToken)
    {
        var streamUrl = $"wss://stream.binance.com:9443/ws/{streamName}";
        using (var webSocket = new ClientWebSocket())
        {
            await webSocket.ConnectAsync(new Uri(streamUrl), cancellationToken);
            logger.LogInformation($"Connected to {streamUrl}");

            await ReceiveMessages(webSocket, cancellationToken);
        }
    }

    private async Task ReceiveMessages(ClientWebSocket webSocket, CancellationToken cancellationToken)
    {
        var buffer = new byte[1024 * 4];
        try
        {
            while (webSocket.State == WebSocketState.Open && !cancellationToken.IsCancellationRequested)
            {
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);
                var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                logger.LogInformation($"Message received: {message}");
                // Process the message as needed
            }
        }
        catch (Exception ex)
        {
            logger.LogError($"Error in WebSocket communication: {ex.Message}", ex);
        }
        finally
        {
            if (webSocket.State != WebSocketState.Closed)
            {
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, cancellationToken);
            }
            logger.LogInformation("Disconnected from WebSocket stream.");
        }
    }



    private async Task<string> CreateSpotListenKeyAsync()
    {
        var httpClient = _httpClientFactory.CreateClient();
        httpClient.DefaultRequestHeaders.Add("X-MBX-APIKEY", apiKey);

        // Assuming this is a POST request without a body, adjust if necessary
        var response = await httpClient.PostAsync($"{baseUrl}/api/v3/userDataStream", null);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var listenKey = JsonConvert.DeserializeObject<dynamic>(content).listenKey.ToString();
            return listenKey;
        }
        else
        {
            logger.LogError($"Failed to create a listen key. Status: {response.StatusCode}");
            return null;
        }
    }


    // User WebSocket stream endpoint
    public async Task UserDataWebSocket(CancellationToken cancellationToken)
    {
        string listenKey = await CreateSpotListenKeyAsync();
        if (string.IsNullOrEmpty(listenKey))
        {
            logger.LogError("Failed to obtain listen key for user data stream.");
            return;
        }

        string streamUrl = $"wss://testnet.binance.vision/{listenKey}";
        await StartStreamAsync(streamUrl, cancellationToken);
    }

    public async Task UserDataWebSocket()
    {
        var httpClient = _httpClientFactory.CreateClient();
        var userDataStreams = new UserDataStreams(httpClient, baseUrl, apiKey: apiKey, apiSecret: apiSecret);
        var response = await userDataStreams.CreateSpotListenKey();
        string listenKey = JsonConvert.DeserializeObject<dynamic>(response).listenKey.ToString();

        // Subscribe to user data stream
        var websocket = new UserDataWebSocket(listenKey, "wss://testnet.binance.vision");

        var onlyOneMessage = new TaskCompletionSource<string>();
        websocket.OnMessageReceived(
            async (data) =>
            {
                onlyOneMessage.SetResult(data);
                await Task.CompletedTask;
            }, CancellationToken.None);

        await websocket.ConnectAsync(CancellationToken.None);
        string message = await onlyOneMessage.Task;
        logger.LogInformation(message);
        await websocket.DisconnectAsync(CancellationToken.None);
    }



    // TradeStream
    public async Task TradeStream()
    {
        var websocket = new MarketDataWebSocket("btcusdt@trade");

        websocket.OnMessageReceived(
            async (data) =>
            {
                logger.LogInformation(data);
                await Task.CompletedTask;
            }, CancellationToken.None);

        await websocket.ConnectAsync(CancellationToken.None);

        // wait for 5s before disconnected
        await Task.Delay(5000);
        logger.LogInformation("Disconnect with WebSocket Server");
        await websocket.DisconnectAsync(CancellationToken.None);
    }

    // SymbolTickerStream
    public async Task SymbolTickerStream()
    {
        var websocket = new MarketDataWebSocket("btcusdt@ticker");

        websocket.OnMessageReceived(
            async (data) =>
            {
                logger.LogInformation(data);
                await Task.CompletedTask;
            }, CancellationToken.None);

        await websocket.ConnectAsync(CancellationToken.None);

        // wait for 5s before disconnected
        await Task.Delay(5000);
        logger.LogInformation("Disconnect with WebSocket Server");
        await websocket.DisconnectAsync(CancellationToken.None);
    }

    // SymbolBookTickerStream
    public async Task SymbolBookTickerStream()
    {
        string[] streams = { "btcusdt@bookTicker", "bnbusdt@bookTicker" };
        var websocket = new MarketDataWebSocket(streams);

        websocket.OnMessageReceived(
            async (data) =>
            {
                logger.LogInformation(data);
                await Task.CompletedTask;
            }, CancellationToken.None);

        await websocket.ConnectAsync(CancellationToken.None);

        // wait for 5s before disconnected
        await Task.Delay(5000);
        logger.LogInformation("Disconnect with WebSocket Server");
        await websocket.DisconnectAsync(CancellationToken.None);
    }

    // MiniTickerStream
    public async Task MiniTickerStream()
    {
        var websocket = new MarketDataWebSocket("btcusdt@miniTicker");

        websocket.OnMessageReceived(
            async (data) =>
            {
                logger.LogInformation(data);
                await Task.CompletedTask;
            }, CancellationToken.None);

        await websocket.ConnectAsync(CancellationToken.None);

        // wait for 5s before disconnected
        await Task.Delay(5000);
        logger.LogInformation("Disconnect with WebSocket Server");
        await websocket.DisconnectAsync(CancellationToken.None);
    }

    // KlineCandlestickStream
    public async Task KlineCandlestickStream()
    {
        var websocket = new MarketDataWebSocket("btcusdt@kline_1m");

        websocket.OnMessageReceived(
            async (data) =>
            {
                logger.LogInformation(data);
                await Task.CompletedTask;
            }, CancellationToken.None);

        await websocket.ConnectAsync(CancellationToken.None);

        // wait for 5s before disconnected
        await Task.Delay(5000);
        logger.LogInformation("Disconnect with WebSocket Server");
        await websocket.DisconnectAsync(CancellationToken.None);
    }

    // DiffDepthStream
    public async Task DiffDepthStream()
    {
        var websocket = new MarketDataWebSocket("btcusdt@depth");

        websocket.OnMessageReceived(
            async (data) =>
            {
                logger.LogInformation(data);
                await Task.CompletedTask;
            }, CancellationToken.None);

        await websocket.ConnectAsync(CancellationToken.None);

        // wait for 5s before disconnected
        await Task.Delay(5000);
        logger.LogInformation("Disconnect with WebSocket Server");
        await websocket.DisconnectAsync(CancellationToken.None);
    }

    // DepthStream
    public async Task DepthStream()
    {
        var websocket = new MarketDataWebSocket("btcusdt@depth5");

        websocket.OnMessageReceived(
            async (data) =>
            {
                logger.LogInformation(data);
                await Task.CompletedTask;
            }, CancellationToken.None);

        await websocket.ConnectAsync(CancellationToken.None);

        // wait for 5s before disconnected
        await Task.Delay(5000);
        logger.LogInformation("Disconnect with WebSocket Server");
        await websocket.DisconnectAsync(CancellationToken.None);
    }

    // AggregateTradeStream
    public async Task AggregateTradeStream()
    {
        var websocket = new MarketDataWebSocket("btcusdt@aggTrade");

        websocket.OnMessageReceived(
            async (data) =>
            {
                logger.LogInformation(data);
                await Task.CompletedTask;
            }, CancellationToken.None);

        await websocket.ConnectAsync(CancellationToken.None);

        // wait for 5s before disconnected
        await Task.Delay(5000);
        logger.LogInformation("Disconnect with WebSocket Server");
        await websocket.DisconnectAsync(CancellationToken.None);
    }

}