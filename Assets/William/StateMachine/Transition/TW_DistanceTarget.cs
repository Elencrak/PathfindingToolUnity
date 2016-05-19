using UnityEngine;
using System.Collections;

public class TW_DistanceTarget : TransitionWill {

    float range;
    bool isTargetFurther;


    public TW_DistanceTarget(int id,StateWill pState, bool pIsTargetFurther, float pRange): base(id, pState)
    {
        
        range = pRange;
        isTargetFurther = pIsTargetFurther;
    }

    public override StateWill check(StateWill current = null)
    {
        float dist = Vector3.Distance(TeamManagerWill.instance.members[0].gameObject.transform.position, TeamManagerWill.instance.mainTarget.transform.position);
        if (isTargetFurther)
        {
            if (dist > range)
                return nextState;
        }
        else
        {
            if (dist < range)
            {
                return nextState;
            }
                
        }
        
        return null;
    }
}
