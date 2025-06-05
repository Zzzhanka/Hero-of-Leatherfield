using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    [SerializeField] private List<Trade> tradeList = new List<Trade>();

    public List<Trade> TradeList => tradeList;

    public void MakeDeal(Trade tradingItem)
    {
        int tradeCost = tradingItem.tradeCost;

        
    }
}
