using UnityEngine;

public enum ItemType
{
    Generic = 0,
    Weapon = 1,
    Potion = 2,
    Armor = 3,
}

public abstract class Item : ScriptableObject
{
    [Header("Generic Data")]
    public string itemName;
    public int itemID;
    public Sprite icon;
    public virtual ItemType itemType => ItemType.Generic;
    public int maxStack = 1;

    [Space(10), TextArea]
    public string description;
}
