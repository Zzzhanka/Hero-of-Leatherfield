using UnityEngine;

[System.Serializable]
public class ItemEntry
{
    public Item item;
    public int quantity;

    public ItemEntry(Item item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }
}

