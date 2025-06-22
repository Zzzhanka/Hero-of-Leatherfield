using Unity.VisualScripting;
using UnityEngine;


public class EnemyCharacteristics : MonoBehaviour
{
    public int EnemyMaxHealth;
    public int EnemyCurrentHealth;

    public int EnemyDamage;
    public int EnemyDefense;

    public float EnemyMoveSpeed;
    public float EnemyAttackReload;



    public enum EnemyType
    {
        Slime,
        Mandragora,
        Mushroom,
        Cyclops,
        Kust,
        Boloto
    }

    public EnemyType Type;

    public void EnemyTakesDamage(int damage)
    {
        EnemyCurrentHealth -= damage;

        if (EnemyCurrentHealth <= 0)
        {
            EnemyCurrentHealth = 0;
            EnemyDies();
        }
    }



    private void Start()
    {
        EnemyCurrentHealth = EnemyMaxHealth;
    }

    private void EnemyDies()
    {
        EnemyKillStats.Instance.RegisterKill(Type);
        gameObject.SetActive(false);

    }

}
