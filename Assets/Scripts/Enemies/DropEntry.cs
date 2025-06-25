using UnityEngine;

[System.Serializable]
public class DropEntry
{
    public enum DropType { Item, Coins }

    public DropType dropType;

    [Tooltip("For Item drop")]
    public GameObject itemPrefab;
    [Range(0f, 1f)] public float dropChance = 1f; // 1 = 100%
    public int amount = 1;

    [Tooltip("For Coin drop")]
    public int minCoins = 0;
    public int maxCoins = 0;

    public int GetDropAmount()
    {
        if (dropType == DropType.Coins)
        {
            return Random.Range(minCoins, maxCoins + 1);
        }
        else
        {
            return amount;
        }
    }

    public bool ShouldDrop()
    {
        if (dropType == DropType.Item)
            return Random.value <= dropChance;
        else
            return true;
    }
}
