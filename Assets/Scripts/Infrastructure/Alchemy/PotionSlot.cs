using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PotionSlot : MonoBehaviour
{
    private Button button;

    [SerializeField] private Image slotIcon;
    [SerializeField] private TMP_Text slotName;

    private Receipt receipt;
    private System.Action<Receipt, PotionSlot> onReceiptClickedCallback;

    private void Awake()
    {
        button = transform.parent.GetComponent<Button>();
    }

    public void Setup(Receipt receipt, System.Action<Receipt, PotionSlot> callback)
    {
        this.receipt = receipt;
        onReceiptClickedCallback = callback;

        slotName.enabled = true;

        if (receipt != null)
        {
            slotIcon.sprite = receipt.receiptItemRef.icon;
            slotIcon.enabled = true;
            slotName.text = receipt.receiptItemRef.itemName;
        }
        else
        {
            slotIcon.enabled = false;
            slotName.text = "NULL";
            
        }

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            if (receipt != null)
            {
                onReceiptClickedCallback?.Invoke(receipt, this);
            }
        });
    }

    public Receipt GetReceipt() => receipt;
}
