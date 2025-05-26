using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public int slotIndex;
    public Image icon;
    public TMP_Text quantityText;
    private Item item;

    public void SetItem(Item newItem)
    {
        item = newItem;

        if (item != null && item.data != null)
        {
            icon.sprite = item.data.icon;
            icon.enabled = true;
            quantityText.text = item.itemQuantity > 1 ? item.itemQuantity.ToString() : "";
        }
        else
        {
            icon.enabled = false;
            quantityText.text = "";
        }
    }

    public Item GetItem()
    {
        return item;
    }

    public void OnClick()
    {
        // Call to inventory details UI or game manager
        // InventoryUI.ShowItemDetails(item);
    }
}
