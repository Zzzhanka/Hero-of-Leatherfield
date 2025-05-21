using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlacksmithScript : MonoBehaviour
{
    [SerializeField] private GameObject _button;
    [SerializeField] private GameObject _BlacksmithPanel;


    [SerializeField] private TMP_Text _textMeshPro;


    private int _currentStrength = 10; 
    private int _strengthIncrease = 10; 
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

    public void BlacksmithEntry()
    {
        _BlacksmithPanel.SetActive(true);
    }
    public void BlacksmithExit()
    {
        _BlacksmithPanel.SetActive(false);
    }

    public void StrengthUpdate()
    {
        _currentStrength += _strengthIncrease;

        UpdateStrengthText();
    }

    public void UpdateStrengthText()
    {
        _textMeshPro.text = $"Strength: {_currentStrength} + {_strengthIncrease}";
    }
}
