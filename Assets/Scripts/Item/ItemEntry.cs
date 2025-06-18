using UnityEngine;

[System.Serializable]
public class ItemEntry
{
    public Item item;
    public WeaponInstance weapon;
    public int quantity;

    public ItemEntry(Item item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;

        if (item.itemType == ItemType.Weapon && item is WeaponItemData weaponItem)
        {
            weapon = new WeaponInstance(weaponItem);
        }
    }

    
}

