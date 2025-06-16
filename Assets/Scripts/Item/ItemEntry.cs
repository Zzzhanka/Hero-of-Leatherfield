using UnityEngine;

[System.Serializable]
public class ItemEntry
{
    public Item item;
    public WeaponItemData weapon;
    public int quantity;

    public ItemEntry(Item item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;

        if (item.itemType == ItemType.Weapon)
        {
            // weapon = new WeaponInstance(item);
        }
    }
}

