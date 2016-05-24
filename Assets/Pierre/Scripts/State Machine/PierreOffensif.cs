using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PierreOffensif : PierreState {
    
    public PierreOffensif(PierreStateMachine psm)
    {
        stateMachine = psm;

        PierreTransition a = new PierreTransition(this, new PierreDefensif(psm)) ;
        a.condition = InputKey;

        transitions.Add(a);
    }
    
    public override void Move(NewPierreAgent agent, NavMeshAgent nav)
    {
        
    }
    
    bool InputKey()
    {
        return Input.GetKeyDown(KeyCode.A);
    }

    public override void Fire()
    {
        
    }

    public override Vector3 UpdateTarget(NewPierreAgent agent, Vector3 myTarget, List<GameObject> targets)
    {
        Vector3 newTarget = myTarget;

        foreach(NewPierreAgent a in GameObject.FindObjectsOfType<NewPierreAgent>())
        {
            targets.Remove(a.gameObject);
        }


        if (targets[0] != agent)
        {
            newTarget = targets[0].transform.position;
        }

        foreach (GameObject target in targets)
        {
            if ((Vector3.Distance(target.transform.position, agent.transform.position) < Vector3.Distance(newTarget, agent.transform.position) || (newTarget == agent.transform.position && agent.transform.position != target.transform.position)) && !target.GetComponent<NewPierreAgent>())
            {
                newTarget = target.transform.position;
            }
        }

        return newTarget;

    }

    public override Vector3 UpdateTargetMove(NewPierreAgent agent, Vector3 myTargetMove, List<GameObject> targets)
    {
        return UpdateTarget(agent, myTargetMove,targets);
    }
    
}
