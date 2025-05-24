using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuWindow : Window
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button optionsGameButton;

    private void StartGameHandler()
    {
        SceneManager.LoadScene("Village");
    }

    private void OpenOptionsHandler()
    {

    }
}
