using UnityEngine;


public class AttackTriggerSword : MonoBehaviour
{

    [SerializeField] private HandFollowJoystick _handFollowJoystick;




    public void EndAttackTrigger()
    {

        _handFollowJoystick.IsAttacking = false;
        _handFollowJoystick.WeaponSprite.SetActive(false);

    }

}
