using Unity.VisualScripting;
using UnityEngine;


public class HandFollowJoystick : MonoBehaviour
{

    [SerializeField] private Transform _player;
    [SerializeField] private Joystick _joystickInput;
    [SerializeField] private GameObject _content;
    [SerializeField] private Animator _weaponAnims;
    [SerializeField] private float _handRadius = 1.5f;

    public bool IsAttacking;
    public GameObject WeaponSprite;

    private bool _wasAimingLastFrame = false;




    public void EndAttacking()
    {

        _wasAimingLastFrame = false;

    }



    private void Update()
    {

        float inputX = _joystickInput.Horizontal;
        float inputY = _joystickInput.Vertical;

        Vector2 dir = new(inputX, inputY);
        float magnitude = dir.magnitude;
        dir.Normalize();

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;


        if (magnitude > 0.1f)
        {
            _wasAimingLastFrame = true;

            Vector3 offset = dir * _handRadius;
            Vector3 newPos = _player.position + offset;
            Quaternion newRot = Quaternion.Euler(0, 0, angle - 90);
            transform.SetPositionAndRotation(newPos, newRot);

            if (dir.x < 0)
            {
                _content.transform.localScale = new Vector3(1, -1, 1);
                _content.transform.rotation = Quaternion.Euler(0, 0, angle - 45);
            }
            else
            {
                _content.transform.localScale = new Vector3(1, 1, 1);
                _content.transform.rotation = Quaternion.Euler(0, 0, angle + 45);
            }
        }
        else
        {
            if (_wasAimingLastFrame)
            {
                SetAttackTrigger();
            }

            _wasAimingLastFrame = false;

            if (!IsAttacking)
            {
                Vector3 newPos = _player.position;
                Quaternion newRot = Quaternion.Euler(0, 0, angle - 90);
                transform.SetPositionAndRotation(newPos, newRot);
                _content.transform.rotation = Quaternion.Euler(0, 0, angle - 45);
            }
        }

    }



    private void SetAttackTrigger()
    {

        IsAttacking = true;
        WeaponSprite.SetActive(true);
        _weaponAnims.SetTrigger("SwordAttack1");


        // Сюда короче потом можно пихнуть код который будет наносить урон врагам которые попали под коллайдер удара.

    }

}