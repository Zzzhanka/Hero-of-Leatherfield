using TMPro;
using UnityEngine;

public class BlacksmithScript : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _blacksmithPanel;

    public void Interact()
    {
        _blacksmithPanel.SetActive(true);
    }

    public void OnPlayerEnter()
    {
        InteractionManager.Instance.ShowButton(this);
    }

    public void OnPlayerExit()
    {
        InteractionManager.Instance.HideButton(this);
        _blacksmithPanel.SetActive(false);
    }
}
