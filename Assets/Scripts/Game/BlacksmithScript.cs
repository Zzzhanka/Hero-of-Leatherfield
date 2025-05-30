using TMPro;
using UnityEngine;

public class BlacksmithScript : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _blacksmithPanel;
    [SerializeField] private TMP_Text _textMeshPro;

    private int _currentStrength = 10;
    private int _strengthIncrease = 10;

    public void Interact()
    {
        _blacksmithPanel.SetActive(true);
        UpdateStrengthText();
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

    public void StrengthUpdate()
    {
        _currentStrength += _strengthIncrease;
        UpdateStrengthText();
    }

    private void UpdateStrengthText()
    {
        _textMeshPro.text = $"Strength: {_currentStrength} + {_strengthIncrease}";
    }
}
