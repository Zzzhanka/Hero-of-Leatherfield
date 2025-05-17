using UnityEngine;


public class PlayerMovementInputSystem : MonoBehaviour
{

    [SerializeField] private Joystick _joystickInput;

    public float InputX;
    public float InputY;



    private void Update()
    {

        InputX = _joystickInput.Horizontal;
        InputY = _joystickInput.Vertical;

    }

}
