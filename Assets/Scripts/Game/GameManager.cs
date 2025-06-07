using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Space(2), Header("Player Dependent objects")]
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private ScoreSystem scoreSystem;

    [Space(5), Header("Player Independent objects")]
    [SerializeField] private ItemPickupFactory itemPickupFactory;
    [SerializeField] private AlchemyManager alchemyManager;
    [SerializeField] private StoreManager storeManager;

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
        scoreSystem.Initialize();
    }
}
