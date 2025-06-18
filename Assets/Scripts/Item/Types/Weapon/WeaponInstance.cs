using System.Collections.Generic;
using UnityEngine;

public class WeaponInstance
{
    public WeaponItemData BaseItem { get; private set; }

    // Bonus fields (private)
    private int bonusDamage;
    private float bonusCritDamage;
    private float bonusCritChance;
    private float minusReloadTime;

    // Public getters
    public int BonusDamage => bonusDamage;
    public float BonusCritDamage => bonusCritDamage;
    public float BonusCritChance => bonusCritChance;
    public float MinusReloadTime => minusReloadTime;

    // Total calculated values
    public int TotalDamage => BaseItem.BaseDamage + bonusDamage;
    public float TotalCritDamage => BaseItem.BaseCritDamage + bonusCritDamage;
    public float TotalCritChance => BaseItem.BaseCritChance + bonusCritChance;
    public float TotalReloadTime => BaseItem.BaseReloadTime - minusReloadTime;

    // Limits
    //private readonly Dictionary<BoostType, float> limitTable = new()
    //{
    //    { BoostType.Damage, 50 },            // Max bonus +50 damage
    //    { BoostType.CritDamage, 2.0f },       // Max +200% crit damage
    //    { BoostType.CritChance, 0.5f },       // Max +50% crit chance
    //    { BoostType.ReloadTime, 1.5f }        // Max -1.5 seconds
    //};

    public WeaponInstance(WeaponItemData item)
    {
        BaseItem = item;
        bonusDamage = 0;
        bonusCritDamage = 0f;
        bonusCritChance = 0f;
        minusReloadTime = 0f;
    }

    public void ApplyEnhancement(BoostType boostType, float amount)
    {
        switch (boostType)
        {
            case BoostType.Damage:
                // if (bonusDamage + amount <= BaseItem.limitTable[boostType])
                if (bonusDamage + amount <= BaseItem.DamageLimit)
                    bonusDamage += (int) amount;
                    
                break;

            case BoostType.CritDamage:
                // if (bonusCritDamage + amount <= limitTable[boostType])
                if (bonusCritDamage + amount <= BaseItem.CritDamageLimit)
                    bonusCritDamage += amount;

                break;

            case BoostType.CritChance:
                // if (bonusCritChance + amount <= limitTable[boostType])
                if (bonusCritChance + amount <= BaseItem.CritChanceLimit)
                    bonusCritChance += amount;

                break;

            case BoostType.ReloadTime:
                // if (minusReloadTime + amount <= limitTable[boostType])
                if (minusReloadTime + amount <= BaseItem.MinusReloadTimeLimit)
                    minusReloadTime += amount;
                    
                break;
        }
    }

    public bool CanApplyEnhancement(BoostType boostType, float amount)
    {
        switch (boostType)
        {
            case BoostType.Damage:
                // return bonusDamage + amount <= limitTable[boostType];
                return bonusDamage + amount <= BaseItem.DamageLimit;

            case BoostType.CritDamage:
                // return bonusCritDamage + amount <= limitTable[boostType];
                return bonusCritDamage + amount <= BaseItem.CritDamageLimit;

            case BoostType.CritChance:
                // return bonusCritChance + amount <= limitTable[boostType];
                return bonusCritChance + amount <= BaseItem.CritChanceLimit;

            case BoostType.ReloadTime:
                // return minusReloadTime + amount <= limitTable[boostType];
                return minusReloadTime + amount <= BaseItem.MinusReloadTimeLimit;

            default:
                return false;
        }
    }

    private float CalculateBoost(float baseStat, float statWeight, int level, float limit)
    {
        float boost = baseStat * statWeight * Mathf.Log(level + 1, 2);
        return Mathf.Min(boost, limit);
    }
}

