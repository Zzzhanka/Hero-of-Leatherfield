using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour
{
    [Space(5), Header("Operation Side")]
    [SerializeField] private TMP_Text CoinsText;
    [SerializeField] private TMP_Text ValueText;
    [SerializeField] private Slider ValueSlider;
    [SerializeField] private Button OperationButton;

    [Space(5), Header("Inventory Side")]
    [SerializeField] private Transform inventoryGrid;
    [SerializeField] private GameObject inventorySlotPrefab;

    [Space(5), Header("Trader Side")]
    [SerializeField] private Transform traderGrid;
    [SerializeField] private GameObject traderSlotPrefab;
    [SerializeField] private Button TradeButton;

    private int MinSlots = 50;

    private List<GameObject> inventorySlots = new List<GameObject>();
    private List<GameObject> traderSlots = new List<GameObject>();

    private Trade chosenTrade;
    private TradeSlot chosenTradeSlot;

    private void Awake()
    {
        TradeButton.onClick.RemoveAllListeners();
        TradeButton.onClick.AddListener(MakeDeal);

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

        ClearInventoryList();
        ClearTradeList();

        List<ItemEntry> inventory = GameManager.Instance.InventoryManager.GetAllEntries();
        CreateInventoryList(inventory);

        List<Trade> tradeList = GameManager.Instance.StoreManager.TradeList;
        CreateTradeList(tradeList);
    }

    private void MakeDeal()
    {
        GameManager.Instance.StoreManager.MakeDeal();
        ClearTradeList();
        ChangeCoinsText(GameManager.Instance.ScoreSystem.TotalCoins);
    }

    private void ChangeValueText(float value)
    {
        ValueText.text = value.ToString();
    }

    private void ChooseInventoryItem(ItemEntry entry)
    {
        ValueSlider.minValue = 1;
        ValueSlider.maxValue = entry.quantity;
        ValueSlider.value = 1;

        OperationButton.GetComponentInChildren<TMP_Text>().text = "Sell";
    }

    private void ChooseTradeItem(Trade trade, TradeSlot slot)
    {
        OperationButton.GetComponentInChildren<TMP_Text>().text = "Buy";

        if(chosenTrade != null)
        {
            chosenTradeSlot.transform.parent.GetComponent<Button>().interactable = true;
        }

        chosenTradeSlot = slot;
        chosenTradeSlot.transform.parent.GetComponent<Button>().interactable = false;

        chosenTrade = trade;

        ValueSlider.minValue = 1;
        ValueSlider.maxValue = 20;
        ValueSlider.value = 1;
        ValueText.text = "1";
    }

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

    private void ChangeCoinsText(float number)
    {
        CoinsText.text = ((int) number).ToString();
    }

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
}
