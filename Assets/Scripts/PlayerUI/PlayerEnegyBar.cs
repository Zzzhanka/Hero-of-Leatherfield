using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerEnegyBar : MonoBehaviour
{

    [SerializeField] private Slider _slider;

    [SerializeField] private TextMeshProUGUI _maxEnergyText;
    [SerializeField] private TextMeshProUGUI _currentEnergyText;




    public void UpdateEnergyBar(float maxEnergy, float currentEnergy)
    {

        _maxEnergyText.text = maxEnergy.ToString();
        _currentEnergyText.text = currentEnergy.ToString();

        _slider.value = currentEnergy / maxEnergy;

    }

}
