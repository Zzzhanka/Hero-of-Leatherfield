using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private ItemPickupFactory itemPickupFactory;
    [SerializeField] private AlchemyManager alchemyManager;
    [SerializeField] private StoreManager storeManager;
    [SerializeField] private ScoreSystem scoreSystem;

    public ItemPickupFactory ItemPickupFactory => 
        itemPickupFactory;

    public InventoryManager InventoryManager => 
        inventoryManager;

    public AlchemyManager AlchemyManager => 
        alchemyManager;

    public StoreManager StoreManager =>
        storeManager;

    public ScoreSystem ScoreSystem =>
        scoreSystem;

    public static GameManager Instance { get; private set; }
  
    
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
    }
}
