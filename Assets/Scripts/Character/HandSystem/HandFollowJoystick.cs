using Unity.VisualScripting;
using UnityEngine;


public class HandFollowJoystick : MonoBehaviour
{

    [Space(10)]
    [Header("ксвье ме рпнцюрэ")]

    [Space(5)]
    [SerializeField] private Transform _player;
    [SerializeField] private Joystick _joystickInput;
    [SerializeField] private GameObject _content;
    [SerializeField] private GameObject _weaponInHandSprite;
    [SerializeField] private GameObject _meleeRange;
    [SerializeField] private float _handRadius = 1.5f;

    [Space(5)]
    public float InputX, InputY;

    private HandAttackSystem _attackSystem;
    private bool _wasJoystickHeld = false;




    public void ReturnHandToPlayer()
    {
        transform.position = _player.position;
        _content.transform.localRotation = Quaternion.identity;
        _weaponInHandSprite.SetActive(false);
        _meleeRange.SetActive(false);
    }



    private void Start()
    {

        _attackSystem = GetComponent<HandAttackSystem>();

    }



    private void Update()
    {

        InputX = _joystickInput.Horizontal;
        InputY = _joystickInput.Vertical;

        Vector2 dir = new(InputX, InputY);
        float magnitude = dir.magnitude;

        if (magnitude > 0.1f)
        {

            _wasJoystickHeld = true;

            if (_attackSystem.AttackReloadTimer <= 0)
            {
                _weaponInHandSprite.SetActive(true);
                _meleeRange.SetActive(true);
            }
            else
            {
                _weaponInHandSprite.SetActive(false);
                _meleeRange.SetActive(false);
            }

            dir.Normalize();
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            Vector3 offset = dir * _handRadius;
            Vector3 newPos = _player.position + offset;

            transform.SetPositionAndRotation(newPos, Quaternion.Euler(0, 0, angle));

            bool isLeft = dir.x < 0;

            _content.transform.localScale = new Vector3(1, isLeft ? -1 : 1, 1);

            RotateWeaponInHand(isLeft);

        }
        else
        {

            if (_wasJoystickHeld)
            {
                _wasJoystickHeld = false;

                _attackSystem?.Attack();
            }

        }

    }



    private void RotateWeaponInHand(bool dirIsLeft)
    {
        float extraAngle = 0f;

        switch (_attackSystem.WeaponInHand)
        {
            case WeaponType.Melee:
                extraAngle = dirIsLeft ? -45 : 45;
                break;

            case WeaponType.Bow:
                extraAngle = dirIsLeft ? -90f : 90f;
                break;

            case WeaponType.Staff:
                extraAngle = dirIsLeft ? 0f : 0f;
                break;
        }

        _content.transform.localRotation = Quaternion.Euler(0, 0, extraAngle);
    }

}