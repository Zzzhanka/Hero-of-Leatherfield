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


    private List<GameObject> slotInstances = new List<GameObject>();

    private void OnEnable()
    {
        GameManager.Instance.InventoryManager.OnInventoryChanged += RefreshUI;
        RefreshUI();
        ChooseFirstSlot();
    }

    private void OnDisable()
    {
        GameManager.Instance.InventoryManager.OnInventoryChanged -= RefreshUI;
    }

    public void RefreshUI()
    {
        ClearUI();

        List<Item> items = GameManager.Instance.InventoryManager.GetAllItems();
        int requiredSlots = Mathf.Max(MinSlots, items.Count);

        for (int i = 0; i < requiredSlots; i++)
        {
            GameObject slotInstance = Instantiate(slotPrefab, gridParent);
            InventorySlot slot = slotInstance.GetComponentInChildren<InventorySlot>();
            slotInstances.Add(slotInstance);

            if (i < items.Count)
            {
                slot.Setup(items[i], ShowItemDetails);                
            }
            else
            {
                slot.Setup(null, ShowItemDetails);
            }
        }
    }

    private void ShowItemDetails(Item slotItem)
    {
        if (slotItem == null) return;
        ItemName.text = slotItem.itemName;
        ItemDescription.text = slotItem.description;
        ItemIcon.sprite = slotItem.icon;
    }

    private void ChooseFirstSlot()
    {
        GameObject firstSlotInstance = slotInstances[0];
        
        if(firstSlotInstance == null) 
        {
            DetailsPanel.SetActive(false);
            return;
        }

        InventorySlot slot = firstSlotInstance.GetComponentInChildren<InventorySlot>();
        if(slot.GetItem() == null)
        {
            DetailsPanel.SetActive(false);
            return;
        }

        DetailsPanel.SetActive(true);
        ShowItemDetails(slot.GetItem());
    }

    private void ClearUI()
    {
        foreach (GameObject slot in slotInstances)
        {
            Destroy(slot);
        }
        slotInstances.Clear();
    }
}
