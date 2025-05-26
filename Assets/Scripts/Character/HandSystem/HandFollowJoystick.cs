using Unity.VisualScripting;
using UnityEngine;


public class HandFollowJoystick : MonoBehaviour
{

    [SerializeField] private Transform _player;
    [SerializeField] private Joystick _joystickInput;
    [SerializeField] private GameObject _content;
    [SerializeField] private float _handRadius = 1.5f;

    private HandAttackSystem _attackSystem;
    private bool _wasJoystickHeld = false;




    private void Start()
    {

        _attackSystem = GetComponent<HandAttackSystem>();

    }



    private void Update()
    {

        float inputX = _joystickInput.Horizontal;
        float inputY = _joystickInput.Vertical;

        Vector2 dir = new(inputX, inputY);
        float magnitude = dir.magnitude;

        if (magnitude > 0.1f)
        {

            _wasJoystickHeld = true;

            dir.Normalize();
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            Vector3 offset = dir * _handRadius;
            Vector3 newPos = _player.position + offset;
            Quaternion newRot = Quaternion.Euler(0, 0, angle - 90);
            transform.SetPositionAndRotation(newPos, newRot);

            if (dir.x < 0)
            {
                _content.transform.localScale = new Vector3(1, -1, 1);
            }
            else
            {
                _content.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else
        {
            transform.position = _player.position;
            _content.transform.rotation = Quaternion.identity;

            if (_wasJoystickHeld)
            {
                _wasJoystickHeld = false;

                _attackSystem?.Attack();
            }
        }

    }

}