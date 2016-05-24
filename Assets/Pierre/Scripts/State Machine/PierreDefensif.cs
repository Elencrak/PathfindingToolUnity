using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PierreDefensif : PierreState {

    public PierreDefensif(PierreStateMachine psm)
    {
        stateMachine = psm;
    }

    public override void Move(NewPierreAgent agent, NavMeshAgent nav)
    {
        Collider[] hitColliders = Physics.OverlapSphere(agent.transform.position, 20);

        foreach (Collider col in hitColliders)
        {
            if (col.tag == "Bullet")
            {
                nav.SetDestination(agent.transform.position + col.transform.right * 20);
            }
        }
    }

    public override void Fire()
    {

    }

    public override Vector3 UpdateTarget(NewPierreAgent agent, Vector3 myTarget, List<GameObject> targets)
    {
        foreach (NewPierreAgent a in GameObject.FindObjectsOfType<NewPierreAgent>())
        {
            targets.Remove(a.gameObject);
        }

        Vector3 newTarget = myTarget;

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
        Vector3 newTarget = myTargetMove;

        if (targets[0] != agent)
        {
            newTarget = targets[0].transform.position;
        }

        foreach (GameObject target in targets)
        {
            if (target.GetComponent<NewPierreAgent>() && target.GetComponent<NewPierreAgent>().basicStrat == PierreStateMachine.Strat.Offensive)
            {
                newTarget = target.transform.position;
            }
        }

        return newTarget;
    }
    
}
