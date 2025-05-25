using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InventoryManager inventoryManager;

    public InventoryManager InventoryManager => 
        inventoryManager;

    public static GameManager Instance { get; private set; }
  
    private bool IsDungeonSession;  
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Initialize();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Initialize()
    {
        inventoryManager.Initialize();
        IsDungeonSession = false;
    }

    private void Update()
    {
        
    }
}
