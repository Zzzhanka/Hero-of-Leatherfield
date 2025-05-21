using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreScript : MonoBehaviour
{
    [SerializeField] private GameObject _button;
    [SerializeField] private GameObject _storePanel;

    [Header("UI Elements")]
    [SerializeField] private TMP_Text _maxText;
    [SerializeField] private TMP_Text _selectedText;
    [SerializeField] private Slider _slider;

    [Header("Inventory")]
    [SerializeField] private int _itemCount = 20; 

    private int _selectedCount = 0;

    private void Start()
    {
        _slider.maxValue = 30;
        _slider.minValue = 1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            _button.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            _button.SetActive(false);
    }

    public void StoreEntry()
    {
        _storePanel.SetActive(true);
        UpdateUI();
    }

    public void StoreExit()
    {
        _storePanel.SetActive(false);
    }

    public void OnSliderValueChanged()
    {
        _selectedCount = Mathf.RoundToInt(_slider.value);
        _selectedText.text = $"Selected: {_selectedCount}";
    }

    public void SellItem()
    {
        if (_selectedCount <= 0 || _itemCount <= 0) return;

        _itemCount -= _selectedCount;

        _selectedCount = 0;
        _slider.value = _slider.minValue;

        UpdateUI();
    }

    private void UpdateUI()
    {
        _maxText.text = $"Max: {_itemCount}";

        _slider.maxValue = _itemCount;

        if (_slider.value > _itemCount)
        {
            _slider.value = _itemCount;
        }
        _selectedText.text = $"Selected: {_selectedCount}";
    }
}
