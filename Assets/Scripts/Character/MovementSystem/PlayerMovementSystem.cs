using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerMovementSystem : MonoBehaviour
{

    private PlayerCharacteristics _playerChars;
    private PlayerState _playerState;
    private PlayerMovementInputSystem _playerMovementInputSystem;

    private Vector2 _moveDirection;



    private void Awake()
    {
        
        _playerChars = GetComponent<PlayerCharacteristics>();
        _playerState = GetComponent<PlayerState>();
        _playerMovementInputSystem = GetComponent<PlayerMovementInputSystem>();

    }



    private void Update()
    {

        _moveDirection = new Vector2(_playerMovementInputSystem.InputX, _playerMovementInputSystem.InputY).normalized;

        JustMove();

    }



    private void JustMove()
    {

        if (_playerState.PlayerCanMove)
        {
            transform.position += (Vector3)((_moveDirection * _playerChars.PlayerMoveSpeed) * Time.deltaTime);
        }

    }

}
