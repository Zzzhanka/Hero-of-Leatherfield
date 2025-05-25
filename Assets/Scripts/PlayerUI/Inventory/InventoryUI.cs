using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot
{
    public Item Item;
    public string ItemDescription;
    public string ItemStats;
}

public class InventoryUI : MonoBehaviour
{
    public GameObject slotPrefab;
    public Transform gridParent;

    private List<GameObject> slotInstances = new List<GameObject>();

    private void OnEnable()
    {
        GameManager.Instance.InventoryManager.OnInventoryChanged += RefreshUI;
        RefreshUI();
    }

    private void OnDisable()
    {
        GameManager.Instance.InventoryManager.OnInventoryChanged -= RefreshUI;
    }

    public void RefreshUI()
    {
        ClearUI();

        List<Item> items = GameManager.Instance.InventoryManager.GetAllItems();
        int requiredSlots = Mathf.Max(21, items.Count);

        for (int i = 0; i < requiredSlots; i++)
        {
            GameObject slot = Instantiate(slotPrefab, gridParent);
            slotInstances.Add(slot);

            if (i < items.Count)
            {
                Item item = items[i];
                Image icon = slot.transform.GetChild(0).GetComponent<Image>();
                Debug.Log(item.data.itemName);
                icon.sprite = item.data.icon;
                icon.enabled = true;

                TMP_Text quantityText = slot.GetComponentInChildren<TMP_Text>();
                quantityText.text = item.itemQuantity > 1 ? item.itemQuantity.ToString() : "";
            }
            else
            {
                // Empty slot
                slot.transform.GetChild(0).GetComponent<Image>().enabled = false;
                slot.GetComponentInChildren<TMP_Text>().text = "";
            }
        }
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
