using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PotionComponentSlot : MonoBehaviour
{
    [SerializeField] private Image slotIcon;
    [SerializeField] private TMP_Text slotName;
    [SerializeField] private TMP_Text slotCounter;

    public void Setup(Component component)
    {
        slotName.enabled = true;
        slotCounter.enabled = true;

        if (component != null)
        {
            int numItem = GameManager.Instance.InventoryManager.FindItemCount(component.requiredItem);

            slotIcon.sprite = component.requiredItem.icon;
            slotIcon.enabled = true;
            slotName.text = component.requiredItem.itemName;
            slotCounter.text = numItem + "/" + component.requiredNumber;
        }
        else
        {
            slotIcon.enabled = false;
            slotName.text = "NULL ITEM";
            slotCounter.text = "0/NULL";
        }
    }
}
