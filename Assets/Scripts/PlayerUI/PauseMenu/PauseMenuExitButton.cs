using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuExitButton : MonoBehaviour 
{
    public void ReturnToMainMenu()
    {
        
        SceneManager.LoadScene(0); 
    }
}