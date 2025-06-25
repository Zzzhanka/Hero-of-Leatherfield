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
    [SerializeField] private BlacksmithManager blacksmithManager;
    [SerializeField] private AudioManager audioManager;

    private GameObject player;

    public AudioManager AudioManager => audioManager;

    public ItemPickupFactory ItemPickupFactory => 
        itemPickupFactory;

    public InventoryManager InventoryManager => 
        inventoryManager;

    public AlchemyManager AlchemyManager => 
        alchemyManager;

    public StoreManager StoreManager =>
        storeManager;

    public BlacksmithManager BlacksmithManager => 
        blacksmithManager;

    public ScoreSystem ScoreSystem =>
        scoreSystem;

    public static GameManager Instance { get; private set; }
    public GameObject Player => player;

    private float checkPlayerTimeAmount = 5f;
    private float checkPlayerTimer = 5f;

   

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
        checkPlayerTimer = 5f;
        FindPlayer();

        inventoryManager.Initialize();
        scoreSystem.Initialize();
        
    }

    private void Update()
    {
        if(checkPlayerTimer <= 0)
        {
            FindPlayer();
            checkPlayerTimer = checkPlayerTimeAmount;
            return;
        }

        checkPlayerTimer -= Time.deltaTime; 
    }

    private void FindPlayer()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }
}
