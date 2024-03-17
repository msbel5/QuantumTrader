using QuantumTrader.Models;

namespace QuantumTrader.Data.Repositories;

public class TradeTransactionRepository()
{

    private readonly ApplicationDbContext _context;

    public TradeTransactionRepository(ApplicationDbContext context) : this()
    {
        _context = context;
    }



    public void AddTradeTransaction(string assetPair, decimal quantity, decimal price, bool isBuy)
    {
        var transaction = new TradeTransaction
        {
            AssetPair = assetPair,
            Quantity = quantity,
            Price = price,
            IsBuy = isBuy,
            TransactionDate = DateTime.UtcNow
        };

        _context.TradeTransactions.Add(transaction);
        _context.SaveChanges();
    }
}