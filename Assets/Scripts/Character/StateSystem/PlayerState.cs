using UnityEngine;


public class PlayerState : MonoBehaviour
{

    [Space(5)]
    [Header("Bools:")]

    [Space(5)]
    public bool PlayerCanMove;
    public bool PlayerCanRun;
    public bool PlayerCanDash;
    public bool PlayerCanAttack;
    public bool PlayerCanTakeDamage;
    public bool PlayerCanDie;
    public bool PlayerCanTakeHealh;
    public bool PlayerCanRecoverEnergy;

    public bool PlayerIsRunning;
    public bool PlayerIsDashing;

}
