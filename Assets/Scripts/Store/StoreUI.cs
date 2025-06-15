using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class StoreUI : MonoBehaviour
{
    [Space(5), Header("Operation Side")]
    [SerializeField] private TMP_Text chosenItemName;
    [SerializeField] private TMP_Text chosenItemFrom;
    [SerializeField] private Image chosenItemIcon;
    [SerializeField] private TMP_Text CoinsText;
    [SerializeField] private TMP_Text ValueText;
    [SerializeField] private Slider ValueSlider;
    [SerializeField] private GameObject InfoPart;
    [SerializeField] private Button OperationButton;

    [Space(5), Header("Inventory Side")]
    [SerializeField] private Transform inventoryGrid;
    [SerializeField] private GameObject inventorySlotPrefab;

    [Space(5), Header("Trader Side")]
    [SerializeField] private Transform traderGrid;
    [SerializeField] private GameObject traderSlotPrefab;
    [SerializeField] private Button TradeButton;

    private int MinSlots = 30;

    private List<GameObject> inventorySlots = new List<GameObject>();
    private List<GameObject> traderSlots = new List<GameObject>();

    private Item chosenItem;
    private Trade chosenTrade;
    private TradeSlot chosenTradeSlot;

    private int chosenAmount = 1;
    private int chosenMaxAmount = 1;
    private bool onSellOwnItems = false;

    private void Awake()
    {
        OperationButton.onClick.RemoveAllListeners();

        ValueSlider.onValueChanged.RemoveAllListeners();
        ValueSlider.onValueChanged.AddListener(ChangeValueText);

        ChangeCoinsText(GameManager.Instance.ScoreSystem.TotalCoins);
    }

    private void OnEnable() 
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        ChangeCoinsText(GameManager.Instance.ScoreSystem.TotalCoins);

        RefreshInventoryList();
        RefreshTradeList();

        
    }

    // Trade operations
    private void Sell()
    {
        GameManager.Instance.StoreManager.Sell(chosenItem, chosenAmount);
        RefreshInventoryList();
        ChangeCoinsText(GameManager.Instance.ScoreSystem.TotalCoins);

        chosenItem = null;
        ChooseFirstInventorySlot();
    }

    private void Buy()
    {
        GameManager.Instance.StoreManager.Buy(chosenTrade, chosenAmount);
        RefreshInventoryList();
        ChangeCoinsText(GameManager.Instance.ScoreSystem.TotalCoins);
        ChooseTradeItem(chosenTrade, chosenTradeSlot);
    }


    // Refresh UI functions
    private void RefreshInventoryList()
    {
        ClearInventoryList();

        List<ItemEntry> inventory = GameManager.Instance.InventoryManager.GetAllEntries();
        CreateInventoryList(inventory);
    }

    private void RefreshTradeList()
    {
        ClearTradeList();

        List<Trade> tradeList = GameManager.Instance.StoreManager.TradeList;
        CreateTradeList(tradeList);
    }

    
    // Choosing Item from List
    private void ChooseInventoryItem(ItemEntry entry)
    {
        onSellOwnItems = true;

        if(entry == null)
        {
            InfoPart.SetActive(false);
            return;
        }

        chosenItem = entry.item;

        UpdateValueSlider(entry);

        chosenItemFrom.text = "From: Inventory";
        chosenItemIcon.sprite = chosenItem.icon;
        chosenItemName.text = chosenItem.itemName;

        OperationButton.GetComponentInChildren<TMP_Text>().text = "Sell";
        OperationButton.interactable = true;

        OperationButton.onClick.RemoveAllListeners();
        OperationButton.onClick.AddListener(Sell);

        InfoPart.SetActive(true);
    }

    private void ChooseTradeItem(Trade trade, TradeSlot slot)
    {
        // Operation Side
        onSellOwnItems = false;

        chosenItemFrom.text = "From: Trader";
        chosenItemIcon.sprite = trade.tradeItem.icon;
        chosenItemName.text = trade.tradeItem.itemName;

        // Trader Side
        chosenTrade = trade;

        if (chosenTradeSlot != null)
        {
            chosenTradeSlot.transform.parent.GetComponent<Button>().interactable = true;
        }

        chosenTradeSlot = slot;
        chosenTradeSlot.transform.parent.GetComponent<Button>().interactable = false;

        UpdateValueSlider();
        CheckBuyOperation();
        OperationButton.GetComponentInChildren<TMP_Text>().text = "Buy";

        OperationButton.onClick.RemoveAllListeners();
        OperationButton.onClick.AddListener(Buy);

        InfoPart.SetActive(true);
    }

    private void ChooseFirstInventorySlot()
    {
        InventorySlot slot = inventorySlots[0].GetComponentInChildren<InventorySlot>();

        if (slot.GetEntry() == null)
        {
            InfoPart.SetActive(false);
            return;
        }

        if (slot.GetEntry().item == null)
        {
            InfoPart.SetActive(false);
            return;
        }

        InfoPart.SetActive(true);
        ChooseInventoryItem(slot.GetEntry());
    }


    // Creating New List functions
    private void CreateInventoryList(List<ItemEntry> inventory)
    {
        int requiredSlots = Mathf.Max(inventory.Count, MinSlots);

        for (int i = 0; i < requiredSlots; i++)
        {
            GameObject slotInstance = Instantiate(inventorySlotPrefab, inventoryGrid);
            InventorySlot slot = slotInstance.GetComponentInChildren<InventorySlot>();
            inventorySlots.Add(slotInstance);

            if (i < inventory.Count)
            {
                slot.Setup(inventory[i], ChooseInventoryItem);
            }
            else
            {
                slot.Setup(null, ChooseInventoryItem);
            }
        }
    }

    private void CreateTradeList(List<Trade> tradeList)
    {
        foreach (Trade trade in tradeList)
        {
            GameObject tradeSlot = Instantiate(traderSlotPrefab, traderGrid);
            TradeSlot slot = tradeSlot.GetComponentInChildren<TradeSlot>();
            traderSlots.Add(tradeSlot);

            slot.Setup(trade, ChooseTradeItem);
        }
    }

    
    // Clearing List functions
    private void ClearInventoryList()
    {
        foreach (GameObject slot in inventorySlots)
        {
            Destroy(slot);
        }

        inventorySlots.Clear();
    }

    private void ClearTradeList()
    {
        foreach (GameObject slot in traderSlots)
        {
            Destroy(slot);
        }

        traderSlots.Clear();
    }


    // Change Text Value Functions
    private void ChangeValueText(float value)
    {
        chosenAmount = (int)value;
        CheckBuyOperation();

        ValueText.text = ((int) value) + "/" + chosenMaxAmount;
    }

    private void ChangeCoinsText(float number)
    {
        CoinsText.text = ((int)number).ToString();
    }

    private void CheckBuyOperation()
    {
        if (chosenTrade != null)
        {
            int cost = chosenTrade.tradeCost * chosenAmount;
            int coins = GameManager.Instance.ScoreSystem.TotalCoins;
            bool temp = OperationButton.interactable;
            OperationButton.interactable = (coins >= cost) || onSellOwnItems;

            if(OperationButton.interactable == temp)
                OperationButton.interactable = (coins >= cost) || onSellOwnItems;
        }
    }


    // Update Slider Functions
    private void UpdateValueSlider(ItemEntry entry)
    {
        chosenMaxAmount = entry.quantity;

        ValueSlider.minValue = 1;
        ValueSlider.maxValue = chosenMaxAmount;
        ValueSlider.value = 1;
        ChangeValueText(1);
    }

    private void UpdateValueSlider()
    {
        chosenMaxAmount = 20;

        ValueSlider.minValue = 1;
        ValueSlider.maxValue = chosenMaxAmount;
        ValueSlider.value = 1;
        ChangeValueText(1);
    }
}
