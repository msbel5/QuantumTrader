using System;

namespace QuantumTrader.Models
{
    public class TradeTransaction
    {
        public Guid Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public string AssetPair { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public bool IsBuy { get; set; }
    }
}