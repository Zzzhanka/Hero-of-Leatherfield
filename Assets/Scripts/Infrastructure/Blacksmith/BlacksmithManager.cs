using System.Collections.Generic;
using UnityEngine;

public class BlacksmithManager : MonoBehaviour
{
    public List<Boost> BoostList = new List<Boost>();

    public void BoostWeapon(WeaponInstance weapon, BoostType boostType, float boostAmount)
    {
        weapon.ApplyEnhancement(boostType, boostAmount);
    }

    public bool CheckBoostAvailability(WeaponInstance weapon)
    {
        return weapon.CanApplyEnhancement();
    }
}
