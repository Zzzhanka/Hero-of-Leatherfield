using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public List<Trade> TradeList = new List<Trade>();
    private List<Trade> tempSaleList = new List<Trade>();
    private List<Trade> tempBuyList = new List<Trade>();

    public List<Trade> TempSale => tempSaleList;
    public List<Trade> TempBuy => tempBuyList;

    public void MakeDeal()
    {
        int saleCost = 0, buyCost = 0;
        foreach (Trade saleTrade in tempSaleList)
        {
            saleCost += saleTrade.tradeCost;
        }

        foreach (Trade buyTrade in tempBuyList)
        {
            buyCost += buyTrade.tradeCost;
        }

        
    }
}
