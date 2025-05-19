using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerMovementSystem : MonoBehaviour
{

    [Space(5)]
    [Header("Передвижение:")]

    [Space(5)]
    public Vector2 PlayerVelocity;



    private PlayerMovementInputSystem _playerMovementInputSystem;
    private PlayerSensorsSystem _playerSensorsSystem;
    private PlayerCharacteristics _playerChars;
    private PlayerState _playerState;

    private Vector2 _heroPreviousPosition;
    private Vector2 _moveDirection;




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



    //public IEnumerator Dash()
    //{
    //    _heroStatus.HeroCanRun = false;
    //    _heroStatus.HeroCanRecoverEnergy = false;

    //    _heroChars.HeroCurrentEnergy -= _heroChars.HeroDashEnergyCost;
    //    _heroChars.UpdateEnergyBarAfterDash();

    //    float HeroBaseMoveSpeed = _heroChars.HeroMovementSpeedPoints / 100f;
    //    float HeroWithDashSpeed = HeroBaseMoveSpeed * _heroChars.HeroDashSpeedMultiplier;

    //    float startTime = Time.time;

    //    while (Time.time < startTime + _heroChars.HeroDashDuration)
    //    {
    //        _heroChars.HeroMovementSpeed = HeroWithDashSpeed;

    //        yield return null;
    //    }

    //    _heroChars.HeroMovementSpeed = HeroBaseMoveSpeed;

    //    _heroStatus.HeroCanRun = true;
    //    _heroStatus.HeroCanRecoverEnergy = true;

    //    _heroChars.RestartDashCooldown();
    //}



    private void CalculateHeroVelocity()
    {
        Vector2 currentPosition = transform.position;
        PlayerVelocity = (currentPosition - _heroPreviousPosition) / Time.deltaTime;
        _heroPreviousPosition = currentPosition;
    }


}
