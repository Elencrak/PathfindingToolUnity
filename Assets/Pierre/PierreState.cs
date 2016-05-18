using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PierreState : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public virtual void Move(NavMeshAgent nav)
    {

    }

    public virtual void Fire()
    {

    }

    public virtual Vector3 UpdateTarget(Vector3 myTarget, List<GameObject> targets)
    {
        return new Vector3();
    }

    public virtual Vector3 UpdateTargetMove(Vector3 myTargetMove, List<GameObject> targets)
    {
        return new Vector3();
    }

}
