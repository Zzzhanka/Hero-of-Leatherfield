using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerHealthBar : MonoBehaviour
{

    [SerializeField] private Slider _slider;

    [SerializeField] private TextMeshProUGUI _maxHealthText;
    [SerializeField] private TextMeshProUGUI _currentHealthText;




    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {

        _maxHealthText.text = maxHealth.ToString();
        _currentHealthText.text = currentHealth.ToString();

        _slider.value = currentHealth / maxHealth;

    }

}
