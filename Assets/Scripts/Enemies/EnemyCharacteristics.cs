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

<<<<<<< Updated upstream
    private PlayerCharacteristics _playerChars;
    private GameObject _playerGameObject;
=======
>>>>>>> Stashed changes

    public void EnemyTakesDamage(int damage)
    {
        EnemyCurrentHealth -= damage;

        if (EnemyCurrentHealth <= 0)
        {
            EnemyCurrentHealth = 0;
            EnemyDies();
        }
    }

<<<<<<< Updated upstream

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger entered");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player takes damage");
            _playerChars.PlayerTakesDamage(EnemyDamage);
        }
    }
    private void Start()
    {
        EnemyCurrentHealth = EnemyMaxHealth;
        _playerGameObject = GameObject.FindWithTag("Player");
        _playerChars = _playerGameObject.GetComponent<PlayerCharacteristics>();
=======
    private void Start()
    {
        EnemyCurrentHealth = EnemyMaxHealth;
>>>>>>> Stashed changes
    }

    private void EnemyDies()
    {
        gameObject.SetActive(false);


    }

}
