using UnityEngine;
using UnityEngine.UI;

public class JoystickSettings : MonoBehaviour
{
    [SerializeField] private Slider joystickSlider;
    [SerializeField] private Image joystickHandle;
    [SerializeField] private Image joystickCircle;

    private void Awake()
    {
        joystickSlider.minValue = 0;
        joystickSlider.maxValue = 1;
        joystickSlider.value = joystickCircle.color.a;

        joystickSlider.onValueChanged.RemoveAllListeners();
        joystickSlider.onValueChanged.AddListener(ChangeTransparency);
    }

    private void ChangeTransparency(float value)
    {
        Color color = joystickCircle.color;
        color.a = value;

        joystickCircle.color = color;
        joystickHandle.color = color;
    }
}
