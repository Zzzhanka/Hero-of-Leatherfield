using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance { get; private set; }

    [Header("All Items in the Game")]
    [SerializeField] private List<Item> allItems = new List<Item>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("[ItemDatabase] Duplicate ItemDatabase detected, destroying extra.");
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        allItems = FindAllItemsInAssets();

        SortItemsByID();
        CheckForDuplicateItemIDs();
    }

    /// <summary>
    /// Find item by ID or name (depending on your design)
    /// </summary>
    public Item FindItemByName(string itemName)
    {
        return allItems.Find(item => item.itemName == itemName);
    }

    public Item FindItemByID(int itemID)
    {
        return allItems.Find(item => item.itemID == itemID);
    }

    public void SortItemsByID()
    {
        allItems.Sort((a, b) => a.itemID.CompareTo(b.itemID));
    }

    public void CheckForDuplicateItemIDs()
    {
        HashSet<int> seenIDs = new HashSet<int>();
        List<int> duplicates = new List<int>();

        foreach (var item in allItems)
        {
            if (item == null) continue;

            if (!seenIDs.Add(item.itemID))
            {
                duplicates.Add(item.itemID);
            }
        }

        if (duplicates.Count > 0)
        {
            Debug.LogWarning($"[ItemDatabase] Found duplicate itemIDs: {string.Join(", ", duplicates)}");
            Debug.LogWarning($"[ItemDatabase] Please, resolve these conflicts!");
        }
        else
        {
            Debug.Log("[ItemDatabase] No duplicate itemIDs found.");
        }
    }

    public List<Item> FindAllItemsInAssets()
    {
        List<Item> items = new List<Item>();
        items.AddRange(Resources.LoadAll<Item>(""));
        return items;
    }

}
