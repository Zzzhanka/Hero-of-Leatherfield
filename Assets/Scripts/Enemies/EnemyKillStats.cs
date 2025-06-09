using System.Collections.Generic;
using UnityEngine;
using static EnemyCharacteristics;

public class EnemyKillStats : MonoBehaviour
{
    public static EnemyKillStats Instance;

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
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterKill(EnemyType type)
    {
        if (killCounts.ContainsKey(type))
            killCounts[type]++;
    }

    public void PrintKillStats()
    {
        Debug.Log("=== KILL STATS ===");
        foreach (var entry in killCounts)
        {
            Debug.Log($"{entry.Key}: {entry.Value} killed");
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) 
        {
            PrintKillStats();
        }
    }
}
