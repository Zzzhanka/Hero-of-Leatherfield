using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuWindow : Window
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button optionsGameButton;

    public void StartGameHandler()
    {
        SceneManager.LoadScene("Village 1");
    }

    private void OpenOptionsHandler()
    {

    }
}
