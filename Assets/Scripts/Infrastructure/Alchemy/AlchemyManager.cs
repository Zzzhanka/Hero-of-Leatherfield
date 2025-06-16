using UnityEngine;
using System.Collections.Generic;

public class AlchemyManager : MonoBehaviour
{
    [SerializeField] private List<Receipt> receiptLists;

    public List<Receipt> ReceiptList => receiptLists;

    public bool CheckAvailability(Receipt receipt)
    {
        List<Component> tempList = receipt.GetComponents();
        List<ItemEntry> invList = GameManager.Instance.InventoryManager.GetAllEntries();

        foreach (Component comp in tempList)
        {
            Item itemR = comp.requiredItem;
            int countR = comp.requiredNumber;
            
            ItemEntry entry = invList.Find(e => e.item == itemR);
            
            if (entry == null || entry.quantity < countR) return false;
        }

        return true;
    }

    public void MakeDeal(Receipt receipt)
    {
        List<Component> tempList = receipt.GetComponents();

        foreach (Component comp in tempList) 
        {
            Item item = comp.requiredItem;
            int count = comp.requiredNumber;

            GameManager.Instance.InventoryManager.RemoveItem(item, false, count);
            GameManager.Instance.InventoryManager.AddItem(receipt.receiptItemRef, receipt.receiptNumber);
        }
    }
}
