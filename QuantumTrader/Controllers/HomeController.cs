using System.Dynamic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using QuantumTrader.Services;
using QuantumTrader.ViewModels;
using Binance.Spot.Models;
using Newtonsoft.Json;
using QuantumTrader.Models;
using TimeInForce = Binance.Spot.Models.TimeInForce;

namespace QuantumTrader.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly BinanceSpotAccountTradeService _binanceSpotAccountTradeService;
    private string _accountUid = "G3ZptCOeiVEXlxmRmTwA0o";
    private string _Uid = "1710615495884876000";
    private string _symbol = "BTCUSDT";
    private readonly long _orderId = 1360372;


    public HomeController(ILogger<HomeController> logger, BinanceSpotAccountTradeService binanceSpotAccountTradeService)
    {
        _logger = logger;
        _binanceSpotAccountTradeService = binanceSpotAccountTradeService;
    }


    private  async Task<string> SpotAccountTradeServicesAsJson()
    {
        dynamic data = new ExpandoObject();

        //string builder
        var checkServerTime = await _binanceSpotAccountTradeService.CheckServerTime();
        var accountInfo = await _binanceSpotAccountTradeService.GetAccountInformation();
        var newOco = await _binanceSpotAccountTradeService.NewOco(_symbol, Side.SELL, (decimal)0.1, 400.15m, 390.3m);
        var newOrder = await _binanceSpotAccountTradeService.NewOrder(_symbol,  Side.BUY, OrderType.MARKET,0.1m);
        var testNewOrder = await _binanceSpotAccountTradeService.TestNewOrder(_symbol, 0.1m, Side.BUY, OrderType.MARKET);
        var queryOco = await _binanceSpotAccountTradeService.QueryOco(_accountUid);
        var queryAllOco = await _binanceSpotAccountTradeService.QueryAllOco();
        var queryOpenOco = await _binanceSpotAccountTradeService.QueryOpenOco();
        var queryOrder = await _binanceSpotAccountTradeService.QueryOrder(_symbol , _orderId);
        var queryCurrentOrderCountUsage = await _binanceSpotAccountTradeService.QueryCurrentOrderCountUsage();
        var currentAllOpenOrders = await _binanceSpotAccountTradeService.CurrentOpenOrders(_symbol);
        var trades = await _binanceSpotAccountTradeService.AccountTradeList(_symbol);
        var cancelAnExistingOrderAndSendANewOrder = await _binanceSpotAccountTradeService.CancelAnExistingOrderAndSendANewOrder(_symbol , Side.SELL,  OrderType.MARKET, "STOP_ON_FAILURE" , _accountUid, _accountUid , _accountUid );
        var cancelOrder = await _binanceSpotAccountTradeService.CancelOrder(_symbol, _accountUid, _accountUid);
        var cancelOco = await _binanceSpotAccountTradeService.CancelOCO(_symbol, _accountUid, _accountUid);
        var cancelAllOpenOrdersOnASymbol = await _binanceSpotAccountTradeService.CancelAllOpenOrdersOnASymbol(_symbol);
        var allOrders = await _binanceSpotAccountTradeService.AllOrders(_symbol);

        data.checkServerTime = checkServerTime;
        data.accountInfo = accountInfo;
        data.newOco = newOco;
        data.newOrder = newOrder;
        data.testNewOrder = testNewOrder;
        data.queryOco = queryOco;
        data.queryAllOco = queryAllOco;
        data.queryOpenOco = queryOpenOco;
        data.queryOrder = queryOrder;
        data.queryCurrentOrderCountUsage = queryCurrentOrderCountUsage;
        data.currentAllOpenOrders = currentAllOpenOrders;
        data.trades = trades;
        data.cancelAnExistingOrderAndSendANewOrder = cancelAnExistingOrderAndSendANewOrder;
        data.cancelOrder = cancelOrder;
        data.cancelOco = cancelOco;
        data.cancelAllOpenOrdersOnASymbol = cancelAllOpenOrdersOnASymbol;
        data.allOrders = allOrders;

        return JsonConvert.SerializeObject(data, Formatting.Indented);

    }




    public async Task<IActionResult> Index()
    {
        try
        {
            // Get the latest price from Binance service. Replace your real symbol here.
            var result = await SpotAccountTradeServicesAsJson();
            //pass result to temp data to view
            TempData["result"] = result;
            // add temp data to view
            return View();
        }
        catch (Exception e)
        {
            // Log the caught exception
            _logger.LogError(e, "Failed to fetch the latest price");

            // Redirect to error page or return a friendly error message
            return View("Error",
                new ErrorViewModel { Message = "Failed to fetch the latest price. Please try again." });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Buy(string symbol, decimal quantity)
    {
        try
        {


            return View("Success"); // A hypothetical view to represent a successful trade
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while trying to execute buy operation");
            return View("Error",
                new ErrorViewModel { Message = "Error while trying to execute buy operation. Please try again." });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Sell(string symbol, decimal quantity)
    {
        try
        {


            return View("Success"); // A hypothetical view to represent a successful trade
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while trying to execute sell operation");
            return View("Error",
                new ErrorViewModel { Message = "Error while trying to execute sell operation. Please try again." });
        }
    }

    // Controller actions follow...
}
