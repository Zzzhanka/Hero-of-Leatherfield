using UnityEngine;

[System.Serializable]
public class LootEntry
{
    public Item item;
    [Range(0f, 1f)]
    public float dropChance = 1f;
    public int amount = 1;
}
