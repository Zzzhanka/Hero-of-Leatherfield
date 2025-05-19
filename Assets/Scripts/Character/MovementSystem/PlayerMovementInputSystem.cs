using UnityEngine;


public class PlayerMovementInputSystem : MonoBehaviour
{

    [Space(5)]
    [Header("Джойстик:")]

    [Space(5)]
    [SerializeField] private Joystick _joystickInput;

    [Space(5)]
    public float InputX;
    public float InputY;

    private PlayerMovementSystem _playerMovementSystem;




    public void DashInput()
    {

        StartCoroutine(_playerMovementSystem.Dash());

    }



    private void Awake()
    {

        _playerMovementSystem = GetComponent<PlayerMovementSystem>();

    }



    private void Update()
    {

        JoystickInput();

    }



    private void JoystickInput()
    {

        float rawX;
        float rawY;

        if (_joystickInput != null)
        {
            rawX = _joystickInput.Horizontal;
            rawY = _joystickInput.Vertical;
        }
        else
        {
            rawX = Input.GetAxisRaw("Horizontal");
            rawY = Input.GetAxisRaw("Vertical");
        }

        Vector2 input = new(rawX, rawY);

        if (input.magnitude < 0.2f)
        {
            InputX = 0;
            InputY = 0;
            return;
        }

        input.Normalize();

        InputX = Mathf.Round(input.x);
        InputY = Mathf.Round(input.y);

    }

}
