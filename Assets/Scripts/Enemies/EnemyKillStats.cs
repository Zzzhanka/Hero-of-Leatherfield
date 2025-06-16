using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using static EnemyCharacteristics;

public class EnemyKillStats : MonoBehaviour
{
    public static EnemyKillStats Instance;
    public TextMeshProUGUI killstat; 

    private Dictionary<EnemyType, int> killCounts = new Dictionary<EnemyType, int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            foreach (EnemyType type in System.Enum.GetValues(typeof(EnemyType)))
            {
                killCounts[type] = 0;
            }

            UpdateKillText();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterKill(EnemyType type)
    {
        if (killCounts.ContainsKey(type))
        {
            killCounts[type]++;
            UpdateKillText();
        }
    }

    public void PrintKillStats()
    {
        Debug.Log("=== KILL STATS ===");
        foreach (var entry in killCounts)
        {
            Debug.Log($"{entry.Key}: {entry.Value} killed");
        }
    }

    private void UpdateKillText()
    {
        if (killstat == null) return;

        string text = "Kills:\n";
        foreach (var entry in killCounts)
        {
            text += $"{entry.Key}: {entry.Value}\n";
        }

        killstat.text = text;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            PrintKillStats();
        }
    }
}
