using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory")]
    public List<Item> inventory = new List<Item>();

    //[Header("Max Inventory Stack Size")]
    //public int defaultMaxStack = 99;

    [Header("Storage")]
    public List<Item> storage = new List<Item>();

    public delegate void InventoryChanged();
    public event InventoryChanged OnInventoryChanged;

    public void Initialize()
    {
        LoadInventory();
    }
    
    // Return boolean to pickup to or not to destroy
    public bool AddItem(Item itemToAdd)
    {
        if (itemToAdd == null)
        {
            Debug.Log($"[Inventory] Item is NULL!");
            return false;
        }

        // Check if already exists (and is stackable)
        Item existing = inventory.Find(i => i == itemToAdd && i.maxStack > 1);

        if (existing != null)
        {
            if (existing.quantity + itemToAdd.quantity > existing.maxStack)
            {
                Debug.Log($"[Inventory] {itemToAdd.itemName} cannot added to max stacked item!");
                return false;
            }

            existing.quantity += itemToAdd.quantity;
        }
        else
        {
            inventory.Add(itemToAdd);
        }

        Debug.Log($"[Inventory] Added {itemToAdd.itemName} x {itemToAdd.quantity}");
        OnInventoryChanged?.Invoke();
        return true;
    }

    public void RemoveItem(Item itemToRemove, int amount = 1)
    {
        if (inventory.Contains(itemToRemove))
        {
            itemToRemove.quantity -= amount;

            if (itemToRemove.quantity <= 0)
                inventory.Remove(itemToRemove);

            OnInventoryChanged?.Invoke();
        }
    }

    public List<Item> GetAllItems()
    {
        return inventory;
    }

    public void SaveInventory()
    {
        // Some code to save inventory

        Debug.Log("Inventory saved.");
    }

    public void LoadInventory()
    {
        // Some code to save inventory

        Debug.Log("Inventory loaded.");
    }
}
