using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [Space(5), Header("Inventory Slots Part")]
    [Range(30, 100)] public int MinSlots = 30;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform gridParent;

    [Space(5), Header("Item Details Part")]
    [SerializeField] private GameObject DetailsPanel;
    [SerializeField] private TMP_Text ItemName;
    [SerializeField] private TMP_Text ItemDescription;
    [SerializeField] private Image ItemIcon;
    [SerializeField] private TMP_Text ItemStats;

    [SerializeField] private Button UseButton;
    [SerializeField] private Button DropButton;

    [Space(5), Header("Item Popup Part")]
    [SerializeField] private GameObject DropPopup;
    [SerializeField] private Slider itemCountSlider;
    [SerializeField] private TMP_Text itemCountText;
    [SerializeField] private Button DropConfirmButton;
    [SerializeField] private Button DropCancelButton;

    private ItemEntry chosenEntry;
    private int chosenItemCount;

    private List<GameObject> slotInstances = new List<GameObject>();

    private void Awake()
    {
        DropPopup.SetActive(false);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        GameManager.Instance.InventoryManager.OnInventoryChanged += RefreshUI;
        RefreshUI();
        ChooseFirstSlot();
        itemCountSlider.onValueChanged.AddListener(UpdateCount);
        DropConfirmButton.onClick.AddListener(DropItem);
    }

    private void OnDisable()
    {
        GameManager.Instance.InventoryManager.OnInventoryChanged -= RefreshUI;
    }

    public void RefreshUI()
    {
        ClearUI();

        List<ItemEntry> entries = GameManager.Instance.InventoryManager.GetAllEntries();
        int requiredSlots = Mathf.Max(MinSlots, entries.Count);

        for (int i = 0; i < requiredSlots; i++)
        {
            GameObject slotInstance = Instantiate(slotPrefab, gridParent);
            InventorySlot slot = slotInstance.GetComponentInChildren<InventorySlot>();
            slotInstances.Add(slotInstance);

            if (i < entries.Count)
            {
                slot.Setup(entries[i], ShowItemDetails);
            }
            else
            {
                slot.Setup(null, ShowItemDetails);
            }
        }
    }

    private void ShowItemDetails(ItemEntry entry)
    {
        if (entry == null || entry.item == null) return;

        chosenEntry = entry;

        ItemName.text = entry.item.itemName;
        ItemDescription.text = entry.item.description;
        ItemIcon.sprite = entry.item.icon;


        ItemStats.text = $"Quantity: {entry.quantity}";
    }

    private void ChooseFirstSlot()
    {
        InventorySlot slot = slotInstances[0].GetComponentInChildren<InventorySlot>();

        if (slot.GetEntry() == null)
        {
            DetailsPanel.SetActive(false);
            return;
        }

        if (slot.GetEntry().item == null)
        {
            DetailsPanel.SetActive(false);
            return;
        }

        DetailsPanel.SetActive(true);
        ShowItemDetails(slot.GetEntry());
    }

    private void ClearUI()
    {
        foreach (GameObject slot in slotInstances)
        {
            Destroy(slot);
        }
        slotInstances.Clear();
    }

    private void DropItem()
    {
        GameManager.Instance.InventoryManager.RemoveItem(chosenEntry.item, true, chosenItemCount);
        RefreshUI();
        DropPopup.SetActive(false);
    }

    private void UseItem(Item item)
    {
        switch (item.itemType)
        {
            case ItemType.Weapon:
                Debug.Log($"Used Weapon {item.itemName}");
                break;
            case ItemType.Armor:
                Debug.Log($"Weared {item.itemName}");
                break;
            case ItemType.Potion:
                Debug.Log($"{item.itemName} was used");
                break;
        }
    }

    public void TooglePopup()
    {
        if (chosenEntry.quantity <= 1)
        {
            chosenItemCount = 1;
            DropItem();
            return;
        }

        if (!DropPopup.activeSelf)
        {
            itemCountSlider.minValue = 1;
            itemCountSlider.maxValue = chosenEntry.quantity;
            itemCountSlider.value = 1;
            itemCountText.text = "1";
            DropPopup.SetActive(true);
        }
        else
        {
            DropPopup.SetActive(false);
        }
    }

    private void UpdateCount(float value)
    {
        chosenItemCount = Mathf.RoundToInt(value);
        itemCountText.text = chosenItemCount.ToString();
    }
}
