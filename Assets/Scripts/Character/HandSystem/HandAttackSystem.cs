using UnityEngine;
using UnityEngine.InputSystem;


public enum WeaponType { Melee, Bow, Staff }


public class HandAttackSystem : MonoBehaviour
{

    [Space(10)]
    [Header("Œ¡Ÿ»≈ Õ¿—“–Œ… »")]

    [Space(5)]
    public WeaponType WeaponInHand;

    [Space(5)]
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private LayerMask _obstacleLayer;

    [Space(5)]
    public float WeaponInHandReload;
    public float AttackReloadTimer;
    [SerializeField] private PlayerReloadBar _reloadBar;
    [SerializeField] private Animator _animator;

    [Space(10)]
    [Header("ƒÀﬂ ¡À»∆. Œ–”∆.")]

    [Space(5)]
    [SerializeField] private Collider2D _meleeWeaponRange;

    [Space(10)]
    [Header("ƒÀﬂ œŒ—Œ’¿")]

    [Space(5)]
    [SerializeField] private GameObject _staffProjectile;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _staffProjectileSpeed = 8f;

    private PlayerCharacteristics _playerChars;
    private HandFollowJoystick _handFollowJoystick;




    public void Attack()
    {
        if (AttackReloadTimer <= 0)
        {
            if (WeaponInHand == WeaponType.Melee)
            {
                Collider2D[] results = new Collider2D[20];

                ContactFilter2D filter = new()
                {
                    useLayerMask = true,
                    layerMask = _enemyLayer,
                    useTriggers = true
                };

                int count = _meleeWeaponRange.Overlap(filter, results);

                for (int i = 0; i < count; i++)
                {
                    if (results[i] != null && results[i].TryGetComponent<EnemyCharacteristics>(out var enemy))
                    {
                        enemy.EnemyTakesDamage(_playerChars.PlayerDamage);
                    }
                }

                _animator.SetTrigger("SwordAttack1");
            }

            else if (WeaponInHand == WeaponType.Staff)
            {
                Vector2 direction = new Vector2(_handFollowJoystick.InputX, _handFollowJoystick.InputY).normalized;

                if (direction != Vector2.zero)
                {
                    GameObject projectile = Instantiate(_staffProjectile, _firePoint.position, Quaternion.identity);

                    if (projectile.TryGetComponent<PlayerProjectile>(out var projectileScript))
                    {
                        int damage = _playerChars.PlayerDamage;
                        projectileScript.Setup(direction, damage);
                    }

                    _animator.SetTrigger("StaffAttack1");
                }
            }

            AttackReloadTimer = WeaponInHandReload;
        }
    }

    private void Awake()
    {
        _playerChars = GetComponentInParent<PlayerCharacteristics>();
        _handFollowJoystick = GetComponent<HandFollowJoystick>();
    }

    private void Update()
    {
        if (AttackReloadTimer > 0)
        {
            AttackReloadTimer -= Time.deltaTime;
            _reloadBar.UpdateReloadBar(WeaponInHandReload, AttackReloadTimer);
        }
    }
}
