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

        if (receipt != null)
        {
            slotIcon.sprite = receipt.receiptSprite;
            slotIcon.enabled = true;
            slotName.text = receipt.receiptName;
            slotName.enabled = true;
        }
        else
        {
            slotIcon.enabled = false;
            slotName.text = "";
            
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
