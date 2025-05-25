using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory")]
    public List<Item> inventory = new List<Item>();

    [Header("Max Inventory Stack Size")]
    public int defaultMaxStack = 99;

    [Header("Storage")]
    public List<Item> storage = new List<Item>();

    public delegate void InventoryChanged();
    public event InventoryChanged OnInventoryChanged;

    public void Initialize()
    {
        LoadInventory();
    }

    public void AddItem(Item itemToAdd)
    {
        if (itemToAdd == null || itemToAdd.data == null) return;

        // Check if already exists (and is stackable)
        Item existing = inventory.Find(i => i.data == itemToAdd.data && i.data.maxStack > 1);

        if (existing != null)
        {
            existing.itemQuantity += itemToAdd.itemQuantity;

            // Clamp to maxStack
            if (existing.itemQuantity + itemToAdd.itemQuantity > existing.data.maxStack)
                existing.itemQuantity = existing.data.maxStack;
        }
        else
        {
            inventory.Add(new Item
            {
                data = itemToAdd.data,
                itemQuantity = Mathf.Clamp(itemToAdd.itemQuantity, 1, itemToAdd.data.maxStack)
            });
        }

        Debug.Log($"[Inventory] Added {itemToAdd.data.itemName} x{itemToAdd.itemQuantity}");

        OnInventoryChanged?.Invoke();
    }

    public void RemoveItem(Item itemToRemove, int amount = 1)
    {
        if (inventory.Contains(itemToRemove))
        {
            itemToRemove.itemQuantity -= amount;

            if (itemToRemove.itemQuantity <= 0)
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
