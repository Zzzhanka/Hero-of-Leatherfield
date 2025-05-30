using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour, IInteractable
{
    [SerializeField] private string[] dungeonScenes = { "Dungeon1" };

    public void Interact()
    {
        string randomScene = dungeonScenes[Random.Range(0, dungeonScenes.Length)];
        SceneManager.LoadScene(randomScene);
    }

    public void OnPlayerEnter()
    {
        InteractionManager.Instance.ShowButton(this);
    }

    public void OnPlayerExit()
    {
        InteractionManager.Instance.HideButton(this);
    }
}
