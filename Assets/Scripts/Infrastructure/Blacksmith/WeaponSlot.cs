using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
{
    [SerializeField] private Image slotIcon;
    [SerializeField] private TMP_Text slotName;
    [SerializeField] private TMP_Text slotLevel;

    private Button button;
    private ItemEntry entry;
    private System.Action<ItemEntry> onItemClickedCallback;

    private void Awake()
    {
        button = transform.parent.GetComponent<Button>();
        button.onClick.RemoveAllListeners();
    }

    public void Setup(ItemEntry entry, System.Action<ItemEntry> callback)
    {
        this.entry = entry;
        onItemClickedCallback = callback;

        if (entry != null && entry.item != null)
        {
            slotIcon.sprite = entry.item.icon;
            slotIcon.enabled = true;
            slotName.text = entry.item.itemName;
            slotName.enabled = true;
            slotLevel.text = (entry.weapon.CurrentBoostLevel >= 3 ? "<color=red>" : "") 
                + entry.weapon.CurrentBoostLevel + "/3" + 
                (entry.weapon.CurrentBoostLevel >= 3 ? "</color>" : "");
        }
        else
        {
            slotIcon.sprite = null;
            slotIcon.enabled = false;
            slotName.text = "NULL";
            slotName.enabled = false;
        }

        button.interactable = true;
        button.onClick.AddListener(() =>
        {
            onItemClickedCallback?.Invoke(entry);
        });
    }

    public void Clear()
    {
        this.entry = null; 
        this.onItemClickedCallback = null;

        slotIcon.sprite = null;
        slotIcon.enabled = false;
        slotName.text = "";
        slotName.enabled = false;

        button.onClick.RemoveAllListeners();
    }

    public ItemEntry GetEntry() => entry;
}
