using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    [Header("Generic Data")]
    public string itemName;
    public int itemID;
    public Sprite icon;
    public ItemType itemType = ItemType.Generic;
    public int maxStack = 1;

    [Space(10), TextArea]
    public string description;
}
