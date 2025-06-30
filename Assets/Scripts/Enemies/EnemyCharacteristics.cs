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
        Snail,
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
        var inventory = FindObjectOfType<InventoryManager>();
        if (inventory == null)
        {
            Debug.LogError("[DropLoot] InventoryManager not found in scene!");
            return;
        }

        foreach (var drop in itemDrops)
        {
            if (Random.value <= drop.dropChance)
            {
                inventory.AddItem(drop.item, drop.amount);
            }
        }

        if (Random.value <= coinDrop.dropChance)
        {
            int coinAmount = Random.Range(coinDrop.minCoins, coinDrop.maxCoins + 1);
            inventory.AddItem(coinDrop.coinItem, coinAmount);
        }
    }

}
