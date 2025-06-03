using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Aclhemy Receipt", menuName = "Interactable / Alchemy Receipt", order = 22)]
public class Receipt : ScriptableObject
{
    public string receiptName;
    public Sprite receiptSprite;
    public List<Component> components = new List<Component>();
    public Item receiptItemRef; // Reference to item to get it after completing receipt
    public int receiptNumber = 1;

    public List<Component> GetComponents() { return components; }
    public ItemEntry GetPotion() { return new ItemEntry(receiptItemRef, 1); }
}
