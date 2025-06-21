using UnityEngine;

[System.Serializable]
public class ItemEntry
{
    public Item item;
    public WeaponInstance weapon;
    public int quantity;

    public ItemEntry(Item item, int quantity, WeaponInstance weapon = null)
    {
        this.item = item;
        this.quantity = quantity;
        this.weapon = weapon;

        if(item is WeaponItemData weaponData && this.weapon == null)
        {
            this.weapon = new WeaponInstance(weaponData);
        }
    }
}

[System.Serializable]
public class ItemEntrySaveData
{
    public int itemID;
    public int quantity;
    public WeaponInstance weaponInstance;
}

