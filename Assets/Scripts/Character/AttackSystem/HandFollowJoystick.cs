using UnityEngine;


public class HandFollowJoystick : MonoBehaviour
{

    [SerializeField] private Transform _player;
    [SerializeField] private Joystick _joystickInput;
    [SerializeField] private GameObject _handAnimator;
    [SerializeField] private float _handRadius = 1.5f;

    private float _inputX;
    private float _inputY;




    private void Update()
    {

        _inputX = _joystickInput.Horizontal;
        _inputY = _joystickInput.Vertical;

        Vector2 dir = new(_inputX, _inputY);
        dir.Normalize();


        if (dir.magnitude > 0.1f)
        {

            Vector3 offset = new Vector3(dir.x, dir.y, 0) * _handRadius;
            transform.position = _player.position + offset;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);


            if (dir.x < 0)
            {
                _handAnimator.transform.localScale = new Vector3(1, -1, 1);
                _handAnimator.transform.rotation = Quaternion.Euler(0, 0, angle - 45);
            }
            else
            {
                _handAnimator.transform.localScale = new Vector3(1, 1, 1);
                _handAnimator.transform.rotation = Quaternion.Euler(0, 0, angle + 45);
            }
        }
        else
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);

            transform.position = _player.position;
            _handAnimator.transform.rotation = Quaternion.Euler(0, 0, angle - 45);
        }

    }

}
