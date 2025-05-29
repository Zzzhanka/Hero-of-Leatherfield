using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private int slotIndex;
    private Button button;
    [SerializeField] private Image slotIcon;
    [SerializeField] private TMP_Text slotQuantity;
    private Item item = null;

    public delegate void OnItemClicked(Item item);

    private OnItemClicked onItemClickedCallback;

    private void Awake()
    {
        button = transform.parent.GetComponent<Button>();
    }

    public void Setup(Item item, OnItemClicked callback)
    {
        SetItem(item);
        onItemClickedCallback = callback;

        button.onClick.RemoveAllListeners(); // Prevent duplicates
        button.onClick.AddListener(() => onItemClickedCallback?.Invoke(item));
    }

    private void SetItem(Item newItem)
    { 
        if (newItem != null)
        {
            item = newItem;
            slotIcon.sprite = item.icon;
            slotIcon.enabled = true;
            slotQuantity.text = item.quantity > 1 ? item.quantity.ToString() : "";
        }
        else
        {
            slotIcon.enabled = false;
            slotQuantity.text = "";
        }
    }

    public Item GetItem()
    {
        return item;
    }
}
