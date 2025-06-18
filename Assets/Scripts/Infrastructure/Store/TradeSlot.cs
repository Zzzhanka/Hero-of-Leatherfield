using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TradeSlot : MonoBehaviour
{
    private Button button;

    [SerializeField] private Image TradeItemIcon;
    [SerializeField] private TMP_Text TradeItemName;
    [SerializeField] private TMP_Text TradeItemCost;

    private Trade trade;
    private System.Action<Trade, TradeSlot> onItemClickedCallback;

    private void Awake()
    {
        button = transform.parent.GetComponent<Button>();
    }

    public void Setup(Trade trade, System.Action<Trade, TradeSlot> callback)
    {
        this.trade = trade;
        onItemClickedCallback = callback;

        TradeItemIcon.enabled = true;
        TradeItemName.enabled = true;
        TradeItemCost.enabled = true;

        if (trade != null)
        {
            button.interactable = true;
            TradeItemIcon.sprite = trade.tradeItem.icon;
            TradeItemName.text = trade.tradeItem.itemName;
            TradeItemCost.text = trade.tradeCost.ToString();
        }
        else
        {
            button.interactable = false;
            TradeItemIcon.enabled = false;
            TradeItemName.text = "NULL TRADE";
            TradeItemCost.text = "0";
        }

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            if (trade != null)
                onItemClickedCallback?.Invoke(trade, this);
        });
    }

    public Trade GetTrade() => trade;
}
