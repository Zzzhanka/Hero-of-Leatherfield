public class WeaponInstance
{
    public WeaponItemData baseItem;

    public int bonusDamage;
    public float bonusCritDamage;
    public float bonusCritChance;
    public float MinusReloadTime;

    public int TotalDamage => baseItem.baseDamage + bonusDamage;
    public float TotalCritChance => baseItem.baseCritChance + bonusCritChance;

    public WeaponInstance(WeaponItemData item)
    {
        baseItem = item;
        bonusDamage = 0;
        bonusCritDamage = 0f;
        bonusCritChance = 0f;
        MinusReloadTime = 0f;
    }

    public void ApplyEnhancement(int extraDamage, float extraCritChance)
    {
        bonusDamage += extraDamage;
        bonusCritChance += extraCritChance;
    }
}
