using Binance.Common;
using Binance.Spot;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Binance.Common;
using Binance.Spot;
using Binance.Spot.Models;
using Newtonsoft.Json;

namespace QuantumTrader.Services;

public class BinanceSpotAccountTradeService
{
    private readonly ILogger<BinanceSpotAccountTradeService> _logger;
    private readonly string _baseUrl;
    private readonly string _apiKey;
    private readonly string _apiSecret;


    public BinanceSpotAccountTradeService(ILogger<BinanceSpotAccountTradeService> logger, IConfiguration configuration)
    {
        _logger = logger;
        _apiKey = configuration["BinanceTestApi:ApiKey"] ?? throw new ArgumentNullException("ApiKey is missing in the configuration");
        _apiSecret = configuration["BinanceTestApi:ApiSecret"] ?? throw new ArgumentNullException("ApiSecret is missing in the configuration");
        _baseUrl = configuration["BinanceTestApi:BaseUrl"] ?? throw new ArgumentNullException("BaseUrl is missing in the configuration");

    }

    private HttpClient CreateHttpClient()
    {
        HttpMessageHandler loggingHandler = new BinanceLoggingHandler(logger: _logger);
        var httpClient = new HttpClient(handler: loggingHandler);
        return httpClient;
    }


    private SpotAccountTrade SpotAccountTrade()
    {
        var httpClient = CreateHttpClient();
        var spotAccountTrade = new SpotAccountTrade(httpClient,  new BinanceHmac(_apiSecret), apiKey: _apiKey, baseUrl: _baseUrl);
        return spotAccountTrade;
    }

    private long GetTimestamp()
    {
        return ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds();
    }


    // Sign by API secret key
    public async Task<string> GetAccountInformation()
    {
        try
        {
            // Sign by API secret key
            var spotAccountTradeHMAC = SpotAccountTrade();
            var resultHMAC = await spotAccountTradeHMAC.AccountInformation();
            return resultHMAC;

        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to fetch the account information" + e);
            return "Failed to fetch the account information" + e;
        }
    }


    // chcek market time
    public async Task<string> CheckServerTime()
    {
        try
        {
            var market = new Market();

            var serverTime = await market.CheckServerTime();
            return serverTime;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to fetch the server time");
            return "Failed to fetch the server time" + e;
        }
    }


    // AccountTradeList
    public async Task<string> AccountTradeList(string symbol)
    {
        try
        {
            var spotAccountTrade = SpotAccountTrade();
            var result = await spotAccountTrade.AccountTradeList(symbol);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to fetch the account trade list");
            return "Failed to fetch the account trade list" + e;
        }
    }

    // ALl Orders
    public async Task<string> AllOrders(string symbol)
    {
        try
        {
            var spotAccountTrade = SpotAccountTrade();
            var result = await spotAccountTrade.AllOrders(symbol);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to fetch the all orders");
            return "Failed to fetch the all orders" + e;
        }
    }

    // CancelAllOpenOrdersOnASymbol
    public async Task<string> CancelAllOpenOrdersOnASymbol(string symbol)
    {
        try
        {
            var spotAccountTrade = SpotAccountTrade();
            var result = await spotAccountTrade.CancelAllOpenOrdersOnASymbol(symbol);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to cancel all open orders on a symbol");
            return "Failed to cancel all open orders on a symbol" + e;
        }
    }

    // CancelAnExistingOrderAndSendANewOrder
    public async Task<string> CancelAnExistingOrderAndSendANewOrder(string symbol,Side side, OrderType orderType, string cancelReplaceMode, string cancelNewClientOrderId, string cancelOrigClientOrderId, string newClientOrderId )
    {
        try
        {
            var spotAccountTrade = SpotAccountTrade();
            var result = await spotAccountTrade.CancelAnExistingOrderAndSendANewOrder(symbol, side: side, orderType, cancelReplaceMode: cancelReplaceMode, cancelNewClientOrderId: cancelNewClientOrderId, cancelOrigClientOrderId: cancelOrigClientOrderId, newClientOrderId: newClientOrderId  );
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to cancel an existing order and send a new order");
            return "Failed to cancel an existing order and send a new order" + e;
        }
    }


    // CancelOco
    public async Task<string> CancelOCO(string symbol, string listClientOrderId , string newClientOrderId)
    {
        try
        {
            var spotAccountTrade = SpotAccountTrade();
            var result = await spotAccountTrade.CancelOco(symbol, listClientOrderId: listClientOrderId, newClientOrderId: newClientOrderId);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to cancel an existing order and send a new order");
            return "Failed to cancel an existing OCO " + e;
        }
    }


    // CancelOrder
    public async Task<string> CancelOrder(string symbol, string origClientOrderId, string newClientOrderId)
    {
        try
        {
            var spotAccountTrade = SpotAccountTrade();
            var result = await spotAccountTrade.CancelOrder(symbol, origClientOrderId: origClientOrderId, newClientOrderId: newClientOrderId);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to cancel an order");
            return "Failed to cancel an order" + e;
        }
    }

    // CurrentOpenOrders
    public async Task<string> CurrentOpenOrders(string symbol)
    {
        try
        {
            var spotAccountTrade = SpotAccountTrade();
            var result = await spotAccountTrade.CurrentOpenOrders(symbol);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to fetch the current open orders");
            return "Failed to fetch the current open orders" + e;
        }
    }


    // NewOco
    public async Task<string> NewOco(string symbol, Side side, decimal quantity, decimal price, decimal stopPrice)
    {
        try
        {
            var spotAccountTrade = SpotAccountTrade();
            var result = await spotAccountTrade.NewOco(symbol, side, quantity, price, stopPrice);
            return result;
        }
        catch (BinanceClientException bcEx)
        {
            _logger.LogError(bcEx, "BinanceClientException in NewOco: {Message}", bcEx.Message);
            return $"BinanceClientException: {bcEx.Message}";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create a new OCO");
            return $"Exception: {ex.Message}";
        }
    }



    // NewOrder
    public async Task<string> NewOrder(string symbol, Side side, OrderType type, decimal quantity)
    {
        try
        {
            var spotAccountTrade = SpotAccountTrade();
            var result = await spotAccountTrade.NewOrder(symbol, side: side, type, quantity: quantity);
            return result;
        }
        catch (BinanceClientException e)
        {
            _logger.LogError(e, "BinanceClientException occurred while creating a new order: {Message}", e.Message);
            return $"Failed to create a new order: {e.Message}";
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unexpected error occurred while creating a new order");
            return "Failed to create a new order due to an unexpected error";
        }
    }


    // QueryAllOco
    public async Task<string> QueryAllOco()
    {
        try
        {
            var spotAccountTrade = SpotAccountTrade();
            var result = await spotAccountTrade.QueryAllOco();
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to query all OCO");
            return "Failed to query all OCO" + e;
        }
    }

    // QueryCurrentOrderCountUsage
    public async Task<string> QueryCurrentOrderCountUsage()
    {
        try
        {
            var spotAccountTrade = SpotAccountTrade();
            var result = await spotAccountTrade.QueryCurrentOrderCountUsage();
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to query current order count usage");
            return "Failed to query current order count usage" + e;
        }
    }

    // QueryOco
    public async Task<string> QueryOco(string origClientOrderId)
    {
        try
        {
            var spotAccountTrade = SpotAccountTrade();
            // Assuming the SDK method only needs orderListId and origClientOrderId.
            var result = await spotAccountTrade.QueryOco( origClientOrderId: origClientOrderId);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to query OCO: {Message}", e.Message);
            return $"Failed to query OCO: {e.Message}";
        }
    }


    // QueryOpenOco
    public async Task<string> QueryOpenOco()
    {
        try
        {
            var spotAccountTrade = SpotAccountTrade();
            var result = await spotAccountTrade.QueryOpenOco();
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to query open OCO");
            return "Failed to query open OCO" + e;
        }
    }

    // QueryOrder
    public async Task<string> QueryOrder(string symbol, long? orderId = null, string origClientOrderId = null)
    {
        try
        {
            var spotAccountTrade = SpotAccountTrade();
            // Assuming the method allows specifying either orderId or origClientOrderId
            var result = await spotAccountTrade.QueryOrder(symbol, orderId, origClientOrderId);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to query order: {Message}", e.Message);
            return $"Failed to query order: {e.Message}";
        }
    }

    // TestNewOrder
    public async Task<string> TestNewOrder(string symbol, decimal quantity, Side side, OrderType type)
    {
        try
        {
            var spotAccountTrade = SpotAccountTrade();
            var result = await spotAccountTrade.TestNewOrder(symbol, side, type, quantity: quantity);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to query order");
            return "Failed to Test New Order" + e;
        }
    }


}