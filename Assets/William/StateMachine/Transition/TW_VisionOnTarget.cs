using UnityEngine;
using System.Collections;

public class TW_VisionOnTarget : TransitionWill {
    bool isTrue;
    
    public TW_VisionOnTarget(int id,StateWill pState, bool pIsTrue): base(id, pState)
    {
        isTrue = pIsTrue;
    }

    public override StateWill check(StateWill current = null)
    {

        Vector3 posPlayer = TeamManagerWill.instance.members[idAgent].transform.position;
        GameObject target = TeamManagerWill.instance.mainTarget;

        Vector3 dir = target.transform.position - posPlayer;
        RaycastHit hit;
        Debug.DrawRay(posPlayer, dir);
        if (Physics.Raycast(posPlayer, dir, out hit))
        {
            Debug.Log("tag hit: " + hit.collider.tag + " /name: " + hit.collider.name);
            if (hit.collider.name == target.name && isTrue)
            {
                return nextState;
            }
            else if (hit.collider.name != target.name && !isTrue&& hit.collider.tag != "Bullet")
            {
                return nextState;
            }
        }
        
        return null;
    }
}
