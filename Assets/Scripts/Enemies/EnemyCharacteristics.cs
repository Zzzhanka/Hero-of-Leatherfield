using System.Collections.Generic;
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

    public List<LootEntry> itemDrops = new();
    public CoinLootEntry coinDrop = new();

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
        DropLoot();
    }

    public void DropLoot()
    {
        foreach (var drop in itemDrops)
        {
            if (Random.value <= drop.dropChance)
            {
                GameManager.Instance.ItemPickupFactory.CreatePickup(gameObject.transform, drop.item, drop.amount);
            }
        }

        if (Random.value <= coinDrop.dropChance)
        {
            int coinAmount = Random.Range(coinDrop.minCoins, coinDrop.maxCoins + 1);
            GameManager.Instance.ItemPickupFactory.CreatePickup(gameObject.transform, coinDrop.coinItem, coinAmount);
        }
    }
}
