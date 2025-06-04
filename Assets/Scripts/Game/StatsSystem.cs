using UnityEngine;

public enum StatsType
{
    Enemy = 0,
    Coins = 1,
}

public class StatsSystem : MonoBehaviour
{
    private int killedEnemies;
    private int collectedCoins;

    public int KilledEnemies => killedEnemies;
    public int CollectedCoins => collectedCoins;

    public void AddStats(StatsType statsType, int statsNumber)
    {
        switch(statsType) 
        {
            case StatsType.Enemy:
                killedEnemies += statsNumber;
                break;

            case StatsType.Coins:
                collectedCoins += statsNumber;
                break;
        }
    }
}
