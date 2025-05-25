using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor.Overlays;

public class InventoryManager : MonoBehaviour
{
    private List<Item> items = new List<Item>();
    private List<Item> store = new List<Item>();

    public void Initialize()
    {
        LoadInventory();
    }

    public void AddItem(Item newItem)
    {
        Item existing = items.Find(i => i.itemID == newItem.itemID);
        if (existing != null)
        {
            existing.itemQuantity += newItem.itemQuantity;
        }
        else
        {
            items.Add(newItem);
        }

        Debug.Log($"Added {newItem.itemName} to inventory.");
    }

    public void DropItem(Item dropItem, int quantity)
    {
        Item existing = items.Find(i => i.itemID == dropItem.itemID);
        if (existing != null)
        {
            if(existing.itemQuantity < quantity)
            {
                existing.itemQuantity -= quantity;
            }
            else 
            {
                items.Remove(existing);
            }

            // Code to send dropped Item to Item Factory

            Debug.Log("The item " + dropItem.itemName + " was dropped out from inventory");
        }
        else
        {
            Debug.LogError("There is no " + dropItem.itemName + " to drop");
        }
    }

    public void StoreItem(Item item) 
    {

    }

    public void TakeItem(Item item)
    {

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
