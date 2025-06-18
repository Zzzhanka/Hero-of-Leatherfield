using UnityEngine;

public enum BoostType
{
    Damage,
    CritDamage,
    CritChance,
    ReloadTime,
}

[CreateAssetMenu(fileName = "Boost Settings", menuName = "Boost / Boost Settings")]
public class Boost : ScriptableObject
{
    public Sprite boostSprite;
    public string boostName;
    public BoostType boostType;
    public int boostCost;
}
