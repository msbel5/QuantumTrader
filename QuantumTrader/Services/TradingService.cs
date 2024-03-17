using QuantumTrader.Data;
using QuantumTrader.Models;
using System;
using QuantumTrader.Data.Repositories;

namespace QuantumTrader.Services
{
    public class TradingService
    {
        private readonly TradeTransactionRepository _tradeTransactionRepository;


        public TradingService(TradeTransactionRepository tradeTransactionRepository)
        {
            _tradeTransactionRepository = tradeTransactionRepository;
        }


        public void AddTrade(string assetPair, decimal quantity, decimal price, bool isBuy)
        {
            _tradeTransactionRepository.AddTradeTransaction(assetPair, quantity, price, isBuy);
        }

        public async Task<bool> BuyAssetAsync(string asset, decimal quantity)
        {
            throw new NotImplementedException();
            // TODO - Implement buy logic
        }

        public async Task<bool> SellAssetAsync(string asset, decimal quantity)
        {
            throw new NotImplementedException();
            // TODO - Implement sell logic
        }
    }
}