using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PierreDefensif : PierreState {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void Move(NavMeshAgent nav)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 20);

        foreach (Collider col in hitColliders)
        {
            if (col.tag == "Bullet")
            {
                nav.SetDestination(transform.position + col.transform.right * 20);
            }
        }
    }

    public override void Fire()
    {

    }

    public override Vector3 UpdateTarget(Vector3 myTarget, List<GameObject> targets)
    {
        Vector3 newTarget = myTarget;

        if (targets[0] != gameObject)
        {
            newTarget = targets[0].transform.position;
        }

        foreach (GameObject target in targets)
        {
            if ((Vector3.Distance(target.transform.position, transform.position) < Vector3.Distance(newTarget, transform.position) || (newTarget == transform.position && transform.position != target.transform.position)) && !target.GetComponent<NewPierreAgent>())
            {
                newTarget = target.transform.position;
            }
        }

        return newTarget;

    }

    public override Vector3 UpdateTargetMove(Vector3 myTargetMove, List<GameObject> targets)
    {
        Vector3 newTarget = myTargetMove;

        if (targets[0] != gameObject)
        {
            newTarget = targets[0].transform.position;
        }

        foreach (GameObject target in targets)
        {
            if ((Vector3.Distance(target.transform.position, transform.position) < Vector3.Distance(newTarget, transform.position) || (newTarget == transform.position && transform.position != target.transform.position)) && target.GetComponent<NewPierreAgent>() && target.GetComponent<PierreStateMachine>().basicStrat == PierreStateMachine.Strat.Offensive)
            {
                newTarget = target.transform.position;
            }
        }

        return newTarget;
    }
}
