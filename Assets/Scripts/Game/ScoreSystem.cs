using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    private int totalCoins;

    public int TotalCoins => totalCoins;

    public void AddCoins(int newCoins)
    {
        totalCoins += newCoins;
    }

    public void RemoveCoins(int newCoins) 
    {  
        totalCoins -= newCoins;
    }
}
