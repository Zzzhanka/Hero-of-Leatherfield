using UnityEngine;


public enum WeaponType { Melee, Bow, Staff }


public class HandAttackSystem : MonoBehaviour
{

    public WeaponType WeaponInHand;

    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private LayerMask _obstacleLayer;
    [SerializeField] private Collider2D _meleeWeaponRange;

    private PlayerCharacteristics _playerChars;




    public void Attack()
    {

        Debug.Log("Атака выполнена!");

    }



    private void Start()
    {
        
        _playerChars = GetComponentInParent<PlayerCharacteristics>();

    }

}
