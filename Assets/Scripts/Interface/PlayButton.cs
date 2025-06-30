using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public void LoadGameScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(3); 
    }
}