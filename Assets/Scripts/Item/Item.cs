using UnityEngine;

[System.Serializable]
public class Item
{
    public ItemData data;
    public int itemQuantity = 1;

    public string Name => data.itemName;
    public Sprite Icon => data.icon;
    public int ID => data.itemID;
}