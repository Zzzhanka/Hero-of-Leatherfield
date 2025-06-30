using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoystickTransparencyController : MonoBehaviour
{
    [SerializeField] private Slider joystickSlider;
    [SerializeField] private List<GameObject> joysticks;

    private void Awake()
    {
        joystickSlider.minValue = 0f;
        joystickSlider.maxValue = 1f;

        if (joysticks.Count > 0)
        {
            var image = joysticks[0].GetComponentInChildren<Image>();
            if (image != null)
                joystickSlider.value = image.color.a;
        }

        joystickSlider.onValueChanged.RemoveAllListeners();
        joystickSlider.onValueChanged.AddListener(ChangeTransparency);
    }

    private void ChangeTransparency(float value)
    {
        foreach (GameObject joystick in joysticks)
        {
            // Get all Image components in the joystick GameObject and its children
            Image[] images = joystick.GetComponentsInChildren<Image>(includeInactive: true);
            foreach (Image img in images)
            {
                Color newColor = img.color;
                newColor.a = value;
                img.color = newColor;
            }
        }
    }
}
