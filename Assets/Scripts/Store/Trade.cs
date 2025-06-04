using UnityEngine;

public class Trade
{
    public Item tradeItem;
    public int tradeItemNumber;
    public int tradeCost;

    public Trade(Item tradeItem, int tradeCost, int tradeItemNumber = 1)
    {
        this.tradeItem = tradeItem;
        this.tradeItemNumber = tradeItemNumber;
        this.tradeCost = tradeCost;
    }
}
