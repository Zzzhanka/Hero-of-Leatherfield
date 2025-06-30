using System.Collections.Generic;
using UnityEngine;

public class InventorySaveData
{
    public List<ItemEntrySaveData> entries = new List<ItemEntrySaveData>();
}

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    [Header("Inventory")]
    public List<ItemEntry> inventory = new List<ItemEntry>();

    [Header("Storage")]
    public List<ItemEntry> storage = new List<ItemEntry>();

    public delegate void InventoryChanged();
    public event InventoryChanged OnInventoryChanged;

    [Range(30, 100)] public int MinSlots = 30;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Initialize()
    {
        // LoadInventory();
    }

    public bool AddItem(Item itemToAdd, int quantity, WeaponInstance weapon = null)
    {
        if (itemToAdd == null)
        {
            Debug.Log("[Inventory] Item is NULL!");
            return false;
        }

        if (inventory.Count >= MinSlots)
        {
            Debug.Log("[Inventory] Inventory is FULL");
        }

        ItemEntry existingEntry = inventory.Find(entry => entry.item == itemToAdd && itemToAdd.maxStack > 1);

        if (existingEntry != null)
        {
            if (existingEntry.quantity + quantity > itemToAdd.maxStack)
            {
                inventory.Add(new ItemEntry(itemToAdd, quantity, weapon));
                return false;
            }

            existingEntry.quantity += quantity;
        }
        else
        {
            inventory.Add(new ItemEntry(itemToAdd, quantity, weapon));
        }

        Debug.Log($"[Inventory] Added {itemToAdd.itemName} x{quantity}");
        OnInventoryChanged?.Invoke();
        return true;
    }

    public void RemoveItem(Item itemToRemove, bool isDropping, int amount = 1)
    {
        ItemEntry entry = inventory.Find(e => e.item == itemToRemove);
        WeaponInstance weapon = entry.weapon;

        if (entry != null)
        {
            if (isDropping)
            {
                GameManager.Instance.ItemPickupFactory.CreatePickup(itemToRemove, amount, weapon);
            }

            if (entry.quantity - amount <= 0)
            {
                Debug.Log($"[Inventory] Delete {amount} {itemToRemove.itemName} ({entry.quantity} - {amount})");
                inventory.Remove(entry);
            }
            else
            {
                Debug.Log($"[Inventory] Remove {amount} {itemToRemove.itemName} ({entry.quantity} - {amount})");
                entry.quantity -= amount;
            }

            OnInventoryChanged?.Invoke();
        }
        else
        {
            Debug.LogError("[Inventory] Item to Remove is NOT in inventory");
            Debug.LogError(itemToRemove.itemName + " " + itemToRemove.itemID);
        }
    }

    public List<ItemEntry> GetAllEntries() => inventory;

    public int FindItemCount(Item item)
    {
        ItemEntry entry = inventory.Find(e => e.item == item);
        return entry != null ? entry.quantity : 0;
    }

    public List<ItemEntry> GetSpecificEntries(ItemType type)
    {
        return inventory.FindAll(e => e.item.itemType == type);
    }

    public void LoadInventory()
    {
        if (!PlayerPrefs.HasKey("InventorySave"))
        {
            Debug.LogWarning("[Inventory] No saved inventory found.");
            return;
        }

        string json = PlayerPrefs.GetString("InventorySave");
        InventorySaveData saveData = JsonUtility.FromJson<InventorySaveData>(json);

        inventory.Clear();

        foreach (var entryData in saveData.entries)
        {
            Item foundItem = FindItemByID(entryData.itemID);
            if (foundItem != null)
            {
                ItemEntry newEntry = new ItemEntry(foundItem, entryData.quantity, entryData.weaponInstance);
                inventory.Add(newEntry);
            }
            else
            {
                Debug.LogWarning($"[Inventory] Item '{entryData.itemID}' not found in database!");
            }
        }

        OnInventoryChanged?.Invoke();
        Debug.Log("[Inventory] Inventory loaded.");
    }

    public void SaveInventory()
    {
        InventorySaveData saveData = new InventorySaveData();

        foreach (var entry in inventory)
        {
            ItemEntrySaveData entryData = new ItemEntrySaveData
            {
                itemID = entry.item.itemID,
                quantity = entry.quantity,
                weaponInstance = entry.weapon
            };

            saveData.entries.Add(entryData);
        }

        string json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString("InventorySave", json);
        PlayerPrefs.Save();

        Debug.Log("[Inventory] Inventory saved.");
    }

    private Item FindItemByID(int id)
    {
        return ItemDatabase.Instance.FindItemByID(id);
    }
}
