using UnityEngine;

[System.Serializable]
public class CoinLootEntry
{
    [Range(0f, 1f)]
    public float dropChance = 1f;
    public int minCoins = 1;
    public int maxCoins = 10;
    public Item coinItem; // Assign your "Coin" Item asset here
}