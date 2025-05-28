using UnityEngine;
using UnityEngine.UI;


public class PlayerDashReload : MonoBehaviour
{

    [SerializeField] private Slider _slider;




    public void UpdateDashReloadBar(float maxTime, float currentTime)
    {

        _slider.value = currentTime / maxTime;

    }

}
