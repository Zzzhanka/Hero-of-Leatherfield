using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    private string COINS_SAVE_NAME = "HOL_COINS";
    private int totalCoins;

    public int TotalCoins 
    {
        get => totalCoins;
        private set
        {
            totalCoins = value <= 0 ? 0 : value;
        }
    }

    public void Initialize()
    {
        LoadCoins();
    }

    public void AddCoins(int newCoins)
    {
        totalCoins += newCoins;
    }

    public void RemoveCoins(int newCoins) 
    {  
        totalCoins -= newCoins;
    }

    public void SaveCoins()
    {
        PlayerPrefs.SetInt(COINS_SAVE_NAME, totalCoins);
    }

    public void LoadCoins()
    {
        totalCoins = PlayerPrefs.GetInt(COINS_SAVE_NAME, 0);
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Backslash))
        {
            CheatAddCoins();
        }
    }

    private void CheatAddCoins()
    {
        totalCoins += 1000;
    }
}
