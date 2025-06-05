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
    [SerializeField] private GameObject inventroySlotPrefab;

    [Space(5), Header("Trader Side")]
    [SerializeField] private Transform traderGrid;
    [SerializeField] private GameObject traderSlotPrefab;

    private Button TradeButton;

    private List<GameObject> inventorySlots = new List<GameObject>();
    private List<GameObject> traderSlots = new List<GameObject>();

    private Trade chosenTrade;

    private void Awake()
    {
        TradeButton.onClick.RemoveAllListeners();
        TradeButton.onClick.AddListener(MakeDeal);
    }

    private void OnEnable() 
    {
        RefreshUI();
        ChooseFirstSlot();
    }

    public void RefreshUI()
    {
        ClearInventoryList();
        ClearTradeList();


    }

    private void ChooseFirstSlot(int chosenSlotPosition = 0)
    {

    }

    private void MakeDeal()
    {
        GameManager.Instance.StoreManager.MakeDeal(chosenTrade);
        ClearTradeList();

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
