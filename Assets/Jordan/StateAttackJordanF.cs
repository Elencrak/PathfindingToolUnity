using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateAttackJordanF : StateJordan {

    private GameObject bullet;

    public StateAttackJordanF()
    {
        bullet = Resources.Load("Bullet") as GameObject;
    }

    public override void step()
    {
        if (currentTarget == null)
            return;

        transform.LookAt(currentTarget.transform.position);
        transform.gameObject.GetComponent<JordanAgentF>().fire();
    }

    public override StateJordan check()
    {
        if(listTransition != null)
        {
            foreach (TransitionJordan trans in listTransition)
            {
                StateJordan temp = trans.check();

                if (temp != null)
                    return temp;
            }
        }
        return null;
    }
}
