using UnityEngine;


public class EnemyAttackScript : MonoBehaviour
{

    [SerializeField] private float _attackReload = 1.5f;
    private float _attackReloadTimer;

    private EnemyCharacteristics _enemyChars;
    private bool _isTouchingPlayer = false;
    private GameObject _player;

    private void Awake()
    {
        _enemyChars = GetComponent<EnemyCharacteristics>();
    }

    private void Update()
    {
        if (_isTouchingPlayer)
        {
            _attackReloadTimer -= Time.deltaTime;
            if (_attackReloadTimer <= 0f)
            {
                AttackPlayer();
                _attackReloadTimer = _attackReload;
            }
        }
    }

    private void AttackPlayer()
    {
        if (_player != null)
        {
            var playerChars = _player.GetComponent<PlayerCharacteristics>();
            if (playerChars != null)
            {
                playerChars.PlayerTakesDamage(_enemyChars.EnemyDamage);
                Debug.Log("Enemy attacked player!");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isTouchingPlayer = true;
            _player = collision.gameObject;
            _attackReloadTimer = 0f; // мгновенный первый удар
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isTouchingPlayer = false;
            _player = null;
        }
    }

}
