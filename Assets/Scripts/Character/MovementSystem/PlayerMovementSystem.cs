using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;


public class PlayerMovementSystem : MonoBehaviour
{

    [Space(5)]
    [Header("������������:")]

    [Space(5)]
    public Vector2 PlayerVelocity;
    [SerializeField] private int _dashCost = 10;
    [SerializeField] private float _dashSpeedMultiplier = 1.5f;
    [SerializeField] private float _dashDuration = 0.2f;
    [SerializeField] private float _dashCooldown = 2f;
    [SerializeField] private float _dashCooldownTimer;
    [SerializeField] private PlayerDashReload _dashReloadSlider;



    private PlayerMovementInputSystem _playerMovementInputSystem;
    private PlayerSensorsSystem _playerSensorsSystem;
    private PlayerCharacteristics _playerChars;
    private PlayerState _playerState;

    private Vector2 _heroPreviousPosition;
    private Vector2 _moveDirection;




    public IEnumerator Dash()
    {

        if (_playerState.PlayerCanDash && _playerChars.PlayerCurrentEnergy >= _dashCost && _dashCooldownTimer <= 0 && (_playerMovementInputSystem.InputX != 0 || _playerMovementInputSystem.InputY != 0))
        {
            _playerState.PlayerCanRun = false;
            _playerState.PlayerCanDash = false;

            float speedBeforeDashing = _playerChars.PlayerMoveSpeed;

            _playerState.PlayerIsDashing = true;

            _playerChars.PlayerCurrentEnergy -= _dashCost;
            _playerChars.UpdatePlayerEnergyBar();

            float HeroWithDashSpeed = speedBeforeDashing * _dashSpeedMultiplier;

            float startTime = Time.time;

            while (Time.time < startTime + _dashDuration)
            {
                _playerChars.PlayerMoveSpeed = HeroWithDashSpeed;

                yield return null;
            }

            _playerChars.PlayerMoveSpeed = speedBeforeDashing;

            _playerState.PlayerIsDashing = false;

            _playerState.PlayerCanRun = true;
            _playerState.PlayerCanDash = true;

            _dashCooldownTimer = _dashCooldown;
        }
        
    }



    private void Awake()
    {

        _playerMovementInputSystem = GetComponent<PlayerMovementInputSystem>();
        _playerSensorsSystem = GetComponent<PlayerSensorsSystem>();
        _playerChars = GetComponent<PlayerCharacteristics>();
        _playerState = GetComponent<PlayerState>();

    }



    private void Update()
    {

        HeroInputMove();
        UpdateDashCooldown();

    }



    private void LateUpdate()
    {

        CalculateHeroVelocity();

    }



    private void HeroInputMove()
    {

        _moveDirection = new Vector2(_playerMovementInputSystem.InputX, _playerMovementInputSystem.InputY).normalized;

        if (_playerState.PlayerCanMove)
        {

            Vector3 newPos = transform.position + (Vector3)((_moveDirection * _playerChars.PlayerMoveSpeed) * Time.deltaTime);

            newPos.x = Mathf.Clamp(newPos.x, _playerSensorsSystem.LeftBlockBorderX, _playerSensorsSystem.RightBlockBorderX);
            newPos.y = Mathf.Clamp(newPos.y, _playerSensorsSystem.BottomBlockBorderY, _playerSensorsSystem.TopBlockBorderY);

            transform.position = newPos;

        }

    }



    private void CalculateHeroVelocity()
    {
        Vector2 currentPosition = transform.position;
        PlayerVelocity = (currentPosition - _heroPreviousPosition) / Time.deltaTime;
        _heroPreviousPosition = currentPosition;
    }



    private void UpdateDashCooldown()
    {

        if (_dashCooldownTimer > 0)
        {
            _dashCooldownTimer -= Time.deltaTime;

            if (_dashCooldownTimer < 0)
                _dashCooldownTimer = 0;

            _dashReloadSlider.UpdateDashReloadBar(_dashCooldown, _dashCooldownTimer);
        }

    }

}
