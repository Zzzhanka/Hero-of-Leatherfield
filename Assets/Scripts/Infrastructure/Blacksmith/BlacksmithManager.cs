using System.Collections.Generic;
using UnityEngine;

public class BlacksmithManager : MonoBehaviour
{
    public List<Boost> BoostList = new List<Boost>();

    public void BoostWeapon(WeaponInstance weapon, Boost boost)
    {
        weapon.ApplyEnhancement(boost);
        GameManager.Instance.ScoreSystem.RemoveCoins(boost.boostCost);
    }

    public bool CheckBoostAvailability(WeaponInstance weapon)
    {
        return weapon.CanApplyEnhancement();
    }
}
