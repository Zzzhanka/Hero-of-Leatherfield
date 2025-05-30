using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private Button button;

    [SerializeField] private Image slotIcon;
    [SerializeField] private TMP_Text slotQuantity;

    private ItemEntry entry;
    private System.Action<ItemEntry> onItemClickedCallback;

    private void Awake()
    {
        button = transform.parent.GetComponent<Button>();
    }

    public void Setup(ItemEntry entry, System.Action<ItemEntry> callback)
    {
        this.entry = entry;
        onItemClickedCallback = callback;

        if (entry != null && entry.item != null)
        {
            slotIcon.sprite = entry.item.icon;
            slotIcon.enabled = true;
            slotQuantity.text = entry.quantity > 1 ? entry.quantity.ToString() : "";
            slotQuantity.enabled = entry.quantity > 1;
        }
        else
        {
            slotIcon.enabled = false;
            slotQuantity.text = "";
            slotQuantity.enabled = false;
        }

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            if (entry != null && entry.item != null)
                onItemClickedCallback?.Invoke(entry);
        });
    }

    public ItemEntry GetEntry() => entry;

    public void SetQuantity(int newQuantity)
    {
        if (entry != null)
        {
            entry.quantity = newQuantity;
            slotQuantity.text = newQuantity > 1 ? newQuantity.ToString() : "";
            slotQuantity.enabled = newQuantity > 1;
        }
    }
}
