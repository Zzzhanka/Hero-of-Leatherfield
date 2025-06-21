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

    [Space(8)]
    [SerializeField] private Transform ItemStatsGrid;
    [SerializeField] private GameObject ItemStatsPrefab;

    [Space(5), Header("Inventory Side")]
    [SerializeField] private Transform inventoryGrid;
    [SerializeField] private GameObject inventorySlotPrefab;

    [Space(5), Header("Trader Side")]
    [SerializeField] private Transform traderGrid;
    [SerializeField] private GameObject traderSlotPrefab;
    [SerializeField] private Button TradeButton;

    private int MinSlots = 56;

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
        this.gameObject.SetActive(false);

        OperationButton.onClick.RemoveAllListeners();

        ValueSlider.onValueChanged.RemoveAllListeners();
        ValueSlider.onValueChanged.AddListener(ChangeValueText);
    }

    private void OnEnable() 
    {
        RefreshUI();
        ChooseFirstInventorySlot();
    }

    //private void OnDisable()
    //{
    //    ClearInventoryList();
    //    ClearTradeList();
    //}

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
        chosenTrade = null;

        if (chosenTradeSlot != null)
        {
            chosenTradeSlot.transform.parent.GetComponent<Button>().interactable = true;
        }

        UpdateValueSlider(entry);

        chosenItemFrom.text = "From: Inventory";
        chosenItemIcon.sprite = chosenItem.icon;
        chosenItemName.text = chosenItem.itemName;

        OperationButton.GetComponentInChildren<TMP_Text>().text = "Sell";
        OperationButton.interactable = true;

        OperationButton.onClick.RemoveAllListeners();
        OperationButton.onClick.AddListener(Sell);

        ShowItemStats(entry);

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
        chosenItem = null;
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

        ItemEntry entry = new ItemEntry(trade.tradeItem, trade.tradeItemNumber);
        ShowItemStats(entry);

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

    private void ShowItemStats(ItemEntry entry)
    {
        foreach (Transform child in ItemStatsGrid)
        {
            Destroy(child.gameObject);
        }

        Item item = entry.item;

        switch (item.itemType)
        {
            case ItemType.Weapon:
                {
                    WeaponInstance weapon = entry.weapon;

                    CreateStatRow("<color=#FF9349>Damage</color>", weapon.TotalDamage.ToString("F0") + "HP");
                    CreateStatRow("<color=#FF4628>Crit. HP/%</color>", weapon.TotalCritDamage + "HP/" + (weapon.TotalCritChance * 100).ToString("F0"));
                    CreateStatRow("<color=#51C328>Reload Time</color>", weapon.TotalReloadTime.ToString("F1"));

                    break;
                }

            case ItemType.Potion:
                {
                    PotionItemData potion = item as PotionItemData;
                    string colorStart = "";
                    string colorEnd = "</color>";

                    switch (potion.potionType)
                    {
                        case PotionType.Health:
                            colorStart = "<color=green>";
                            break;

                        case PotionType.Mana:
                            colorStart = "<color=blue>";
                            break;

                        case PotionType.Speed:
                            colorStart = "<color=yellow>";
                            break;

                        case PotionType.Damage:
                            colorStart = "<color=red>";
                            break;

                        default:
                            colorEnd = "";
                            break;
                    }

                    CreateStatRow("Type", colorStart + potion.potionType.ToString() + colorEnd);
                    CreateStatRow("Instant", potion.isInstant ? "Yes" : "No");
                    CreateStatRow("Increase Unit", potion.potionIncreaseAmount + " PU / " +
                        (!potion.isInstant ? potion.potionIncreaseInterval : "1") + " s");

                    if (!potion.isInstant)
                    {
                        CreateStatRow("Action Type", potion.potionActionType.ToString());
                        CreateStatRow("Duration", potion.potionDuration + " s");
                    }

                    break;
                }

            default: break;
        }
    }


    // Creating New List functions
    private void CreateInventoryList(List<ItemEntry> inventory)
    {
        int requiredSlots = Mathf.Max(inventory.Count, MinSlots);

        // Reuse or instantiate slots
        for (int i = 0; i < requiredSlots; i++)
        {
            GameObject slotInstance;

            if (i < inventorySlots.Count)
            {
                slotInstance = inventorySlots[i];
                slotInstance.SetActive(true);
            }
            else
            {
                slotInstance = Instantiate(inventorySlotPrefab, inventoryGrid);
                inventorySlots.Add(slotInstance);
            }

            InventorySlot slot = slotInstance.GetComponentInChildren<InventorySlot>();

            if (i < inventory.Count)
                slot.Setup(inventory[i], ChooseInventoryItem);
            else
                slot.Setup(null, ChooseInventoryItem);
        }

        // Deactivate excess slots
        for (int i = requiredSlots; i < inventorySlots.Count; i++)
        {
            inventorySlots[i].SetActive(false);
        }
    }

    private void CreateTradeList(List<Trade> tradeList)
    {
        for (int i = 0; i < tradeList.Count; i++)
        {
            GameObject slotInstance;

            if (i < traderSlots.Count)
            {
                slotInstance = traderSlots[i];
                slotInstance.SetActive(true);
            }
            else
            {
                slotInstance = Instantiate(traderSlotPrefab, traderGrid);
                traderSlots.Add(slotInstance);
            }

            TradeSlot slot = slotInstance.GetComponentInChildren<TradeSlot>();
            slot.Setup(tradeList[i], ChooseTradeItem);
        }

        // Deactivate excess trade slots
        for (int i = tradeList.Count; i < traderSlots.Count; i++)
        {
            traderSlots[i].SetActive(false);
        }
    }

    private void CreateStatRow(string label, string value)
    {
        GameObject statsObj = Instantiate(ItemStatsPrefab, ItemStatsGrid);
        StatsScript stat = statsObj.GetComponentInChildren<StatsScript>();
        stat.Setup(label, value);
    }

    // Clearing List functions
    private void ClearInventoryList()
    {
        foreach (GameObject slot in inventorySlots)
            slot.SetActive(false);
    }

    private void ClearTradeList()
    {
        foreach (GameObject slot in traderSlots)
            slot.SetActive(false);
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
        CoinsText.text = ((int) number).ToString();
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
