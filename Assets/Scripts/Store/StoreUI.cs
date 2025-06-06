using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour
{
    [Space(5), Header("Operation Side")]
    [SerializeField] private TMP_Text ValueText;
    [SerializeField] private Slider ValueSlider;
    [SerializeField] private Button OperationButton;

    [Space(5), Header("Inventory Side")]
    [SerializeField] private Transform inventoryGrid;
    [SerializeField] private GameObject inventorySlotPrefab;

    [Space(5), Header("Trader Side")]
    [SerializeField] private Transform traderGrid;
    [SerializeField] private GameObject traderSlotPrefab;

    private Button TradeButton;
    private int MinSlots = 30;

    private List<GameObject> inventorySlots = new List<GameObject>();
    private List<GameObject> traderSlots = new List<GameObject>();

    private Trade chosenTrade;

    private void Awake()
    {
        TradeButton.onClick.RemoveAllListeners();
        TradeButton.onClick.AddListener(MakeDeal);

        ValueSlider.onValueChanged.RemoveAllListeners();
        ValueSlider.onValueChanged.AddListener(ChangeValueText);
    }

    private void OnEnable() 
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        ClearInventoryList();
        ClearTradeList();

        List<ItemEntry> inventory = GameManager.Instance.InventoryManager.GetAllEntries();
        CreateInventoryList(inventory);

        List<Trade> tradeList = GameManager.Instance.StoreManager.TradeList;
        CreateTradeList(tradeList);
    }

    private void MakeDeal()
    {
        GameManager.Instance.StoreManager.MakeDeal(chosenTrade);
        ClearTradeList();
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
