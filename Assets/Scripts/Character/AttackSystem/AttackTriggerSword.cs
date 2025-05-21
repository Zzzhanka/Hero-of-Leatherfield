using UnityEngine;


public class AttackTriggerSword : MonoBehaviour
{

    [SerializeField] private HandFollowJoystick _handFollowJoystick;
    [SerializeField] private Collider2D _damageCollider;
    [SerializeField] private LayerMask _enemyLayer;

    private PlayerCharacteristics _playerChars;




    public void EndAttackTrigger()
    {

        _handFollowJoystick.IsAttacking = false;
        _handFollowJoystick.WeaponSprite.SetActive(false);

    }



    public void DealDamageBySword()
    {

        _damageCollider.enabled = true;

        Collider2D[] results = new Collider2D[10];

        var filter = new ContactFilter2D
        {
            useLayerMask = true,
            layerMask = _enemyLayer,
            useTriggers = true
        };

        int count = Physics2D.OverlapCollider(_damageCollider, filter, results);

        for (int i = 0; i < count; i++)
        {
            if (results[i].TryGetComponent<EnemyCharacteristics>(out EnemyCharacteristics enemy))
            {
                enemy.EnemyTakesDamage(_playerChars.PlayerDamage);
            }
        }

        _damageCollider.enabled = false;

    }



    private void Awake()
    {

        _playerChars = GetComponentInParent<PlayerCharacteristics>();

    }

}
