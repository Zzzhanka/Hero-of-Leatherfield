using UnityEngine;


public class AnimatorEndAttackTrigger : MonoBehaviour
{

    private HandFollowJoystick _handFollowJoystcik;




    public void EndAttack()
    {

        _handFollowJoystcik.ReturnHandToPlayer();

    }



    private void Awake()
    {
        
        _handFollowJoystcik = GetComponentInParent<HandFollowJoystick>();

    }

}
