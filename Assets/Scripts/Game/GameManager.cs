using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private WindowService windowService;

    public WindowService WindowService =>
        windowService;

    public static GameManager Instance { get; private set; }

    public void LoadDungeonScene()
    {
        string[] dungeonScenes = { "Dungeon1", "Dungeon2", "Dungeon3" };

        string randomScene = dungeonScenes[Random.Range(0, dungeonScenes.Length)];

        SceneManager.LoadScene(randomScene);
    }
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
        windowService.Initialize();
    }
}
