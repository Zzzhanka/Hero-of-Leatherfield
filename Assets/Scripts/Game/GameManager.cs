using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private WindowService windowService;

    public WindowService WindowService =>
        windowService;

    public static GameManager Instance { get; private set; }

    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            //Initialize();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    //private void Initialize()
    //{
    //    windowService.Initialize();
    //}
}
