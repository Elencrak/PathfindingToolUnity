using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AgentBenoitV : MonoBehaviour {

    public List<GameObject> targets;
    NavMeshAgent myAgent;
    public GameObject myTarget;
    float distanceMin;
    float currentDistance;
    

	// Use this for initialization
	void Start () {
        distanceMin = Mathf.Infinity;
        myAgent = GetComponent<NavMeshAgent>();
        FindTargets();
        FindTarget();
        InvokeRepeating("MoveToTarget", 0.1f, 0.5f);
        InvokeRepeating("ChangeTarget", 0.1f, 0.5f);
    }

    void FindTargets()
    {
        GameObject[] tempTargets;
        tempTargets = GameObject.FindGameObjectsWithTag("Target");
        foreach (GameObject target in tempTargets)
        {
            if (target.gameObject != this.gameObject)
            {
                targets.Add(target);
            }
        }
        
    }

    void OnCollisionEnter(Collision otherCollider)
    {
        if (otherCollider.gameObject.tag == "Target")
        {
            targets.Remove(otherCollider.gameObject);
            FindTarget();
        }
    }

    void MoveToTarget()
    {
        myAgent.SetDestination(myTarget.transform.position);
    }

    void FindTarget()
    {
        if (targets.Count > 0)
        {
            distanceMin = Mathf.Infinity;
            for (int i = 0; i < targets.Count; ++i)
            {
                currentDistance = Vector3.Distance(transform.position, targets[i].transform.position);
                if (currentDistance < distanceMin)
                {
                    myTarget = targets[i];
                    distanceMin = currentDistance;
                }
            }
        }else
        {
            myAgent.Stop();
        }
    }

    void ChangeTarget()
    {
        if (targets.Count > 0)
        {
            for (int i = 0; i < targets.Count; ++i)
            {
                currentDistance = Vector3.Distance(transform.position, targets[i].transform.position);
                if (currentDistance < distanceMin && currentDistance < 10f)
                {
                    myTarget = targets[i];
                    distanceMin = currentDistance;
                }
            }
        }else
        {
            Debug.Log("J'ai tout mangé, je peux repartir jouer ?");
            myAgent.Stop();
        }
    }
}
