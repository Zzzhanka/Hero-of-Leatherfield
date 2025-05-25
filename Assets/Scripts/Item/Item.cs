using UnityEngine;

public abstract class Item
{
    public int itemID;
    public string itemName;
    public Sprite itemIcon;
    public ItemType itemType;
    public int itemQuantity = 1;

    public abstract void Use();
    public abstract void Equip();
}
