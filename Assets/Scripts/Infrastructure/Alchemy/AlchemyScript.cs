using UnityEngine;
using UnityEngine.UIElements;

public class AlchemyScript : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _alchemyPanel;

    public void Interact()
    {
        _alchemyPanel.SetActive(true);
    }

    public void OnPlayerEnter()
    {
        InteractionManager.Instance.ShowButton(this);
    }

    public void OnPlayerExit()
    {
        InteractionManager.Instance.HideButton(this);
        _alchemyPanel.SetActive(false);
    }
}
