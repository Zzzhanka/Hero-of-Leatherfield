using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public List<Trade> TradeList = new List<Trade>();

    public void Sell(Item selledItem, int amount = 1)
    {
        int profit = selledItem.cost * amount;
        GameManager.Instance.InventoryManager.RemoveItem(selledItem, false, amount);
        GameManager.Instance.ScoreSystem.AddCoins(profit);
    }

    public void Buy(Trade trade, int amount = 1)
    {
        int totalCost = trade.tradeCost * amount;
        GameManager.Instance.InventoryManager.AddItem(trade.tradeItem, amount);
        GameManager.Instance.ScoreSystem.RemoveCoins(totalCost);
    }
}
