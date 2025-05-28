using UnityEngine;
using UnityEngine.UI;


public class PlayerReloadBar : MonoBehaviour
{

    [SerializeField] private Slider _slider;
    [SerializeField] private CanvasGroup _canvasGroup;




    public void UpdateReloadBar(float maxTime, float currentTime)
    {

        _slider.value = currentTime / maxTime;

        if (_slider.value == 0)
        {
            _canvasGroup.alpha = 0;
        }
        else
        {
            _canvasGroup.alpha = 0.5f;
        }

    }

}