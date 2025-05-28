using UnityEngine;


public enum WeaponType { Melee, Bow, Staff }


public class HandAttackSystem : MonoBehaviour
{

    [Space(10)]
    [Header("Œ¡Ÿ»≈ Õ¿—“–Œ… »")]

    [Space(5)]
    public WeaponType WeaponInHand;

    [Space(10)]
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private LayerMask _obstacleLayer;

    [Space(5)]
    public float WeaponInHandReload;
    public float AttackReloadTimer;
    [SerializeField] private PlayerReloadBar _reloadBar;

    [Space(10)]
    [Header("ƒÀﬂ ¡À»∆. Œ–”∆.")]

    [Space(10)]
    [SerializeField] private Collider2D _meleeWeaponRange;
    [SerializeField] private Animator _meleeAnimator;

    private PlayerCharacteristics _playerChars;




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

                _meleeAnimator.SetTrigger("SwordAttack1");
            }

            AttackReloadTimer = WeaponInHandReload;
        }

    }



    private void Awake()
    {
        
        _playerChars = GetComponentInParent<PlayerCharacteristics>();

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
