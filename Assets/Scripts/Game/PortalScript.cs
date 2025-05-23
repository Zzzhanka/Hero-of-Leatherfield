using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{

    [SerializeField] private GameObject _button;

    public void VillBack()
    {
        SceneManager.LoadScene("Sample")
    }

    public void LoadDungeonScene()
    {
        string[] dungeonScenes = { "Dungeon1" };

        string randomScene = dungeonScenes[Random.Range(0, dungeonScenes.Length)];

        SceneManager.LoadScene(randomScene);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            _button.SetActive(true);
        }

    }



    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            _button.SetActive(false);
        }

    }

}
