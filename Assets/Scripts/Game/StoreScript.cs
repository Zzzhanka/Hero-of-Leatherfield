using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreScript : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _storePanel;

    private int _selectedCount = 0;

    public void Interact()
    {
        _storePanel.SetActive(true);
    }

    public void OnPlayerEnter()
    {
        InteractionManager.Instance.ShowButton(this);
    }

    public void OnPlayerExit()
    {
        InteractionManager.Instance.HideButton(this);
        _storePanel.SetActive(false);
    }
}
