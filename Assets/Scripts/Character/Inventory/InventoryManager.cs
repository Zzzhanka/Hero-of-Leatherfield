using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory")]
    public List<ItemEntry> inventory = new List<ItemEntry>();

    [Header("Storage")]
    public List<ItemEntry> storage = new List<ItemEntry>();

    public delegate void InventoryChanged();
    public event InventoryChanged OnInventoryChanged;

    public void Initialize()
    {
        // LoadInventory();
    }

    public bool AddItem(Item itemToAdd, int quantity)
    {
        if (itemToAdd == null)
        {
            Debug.Log("[Inventory] Item is NULL!");
            return false;
        }

        ItemEntry existingEntry = inventory.Find(entry => entry.item == itemToAdd && itemToAdd.maxStack > 1);

        if (existingEntry != null)
        {
            if (existingEntry.quantity + quantity > itemToAdd.maxStack)
            {
                Debug.Log($"[Inventory] {itemToAdd.itemName} cannot exceed max stack!");
                return false;
            }

            existingEntry.quantity += quantity;
        }
        else
        {
            inventory.Add(new ItemEntry(itemToAdd, quantity));
        }

        Debug.Log($"[Inventory] Added {itemToAdd.itemName} x{quantity}");
        OnInventoryChanged?.Invoke();
        return true;
    }

    public void RemoveItem(Item itemToRemove, bool isDropping, int amount = 1)
    {
        ItemEntry entry = inventory.Find(e => e.item == itemToRemove);

        if (entry != null)
        {
            if (isDropping)
            {
                GameManager.Instance.ItemPickupFactory.CreatePickup(itemToRemove, amount);
            }

            Debug.Log($"[Inventory] Remove {amount} {itemToRemove.itemName} ({entry.quantity} - {amount})");

            if (entry.quantity - amount <= 0)
            {
                
                inventory.Remove(entry);
            }

            entry.quantity -= amount;

            OnInventoryChanged?.Invoke();
        }
    }

    public List<ItemEntry> GetAllEntries()
    {
        return inventory;
    }

    public int FindItemCount(Item item)
    {
        ItemEntry entry = inventory.Find(e => e.item == item);

        if(entry != null)
        {
            return entry.quantity;
        }
        else
        {
            return 0;
        }
    }
}
