using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AgentJojoKiller : MonoBehaviour {


    bool doOnce = true;
    public List<Transform> targets;
    public Vector3 targetPosition;
    bool touch;
    NavMeshAgent currentNavMeshAgent;
    bool needToChangeTarget;

    // Use this for initialization
    void Start () {
        currentNavMeshAgent = GetComponent<NavMeshAgent>();
        needToChangeTarget = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (doOnce)
        {
            doOnce = false;
            GameObject[] tempArray;
            tempArray = GameObject.FindGameObjectsWithTag("Target");
            foreach(GameObject g in tempArray)
            {
                if(g.GetInstanceID() != gameObject.GetInstanceID())
                {
                    targets.Add(g.transform);
                }
            }
        }

        if (needToChangeTarget) { 
            foreach (Transform g in targets)
            {
                if (g.GetInstanceID() != gameObject.GetInstanceID())
                {
                    Vector3 relativePosition;
                    Vector3 relativePositionTarget;
                    relativePosition  = g.position - transform.position;
                    relativePositionTarget = targetPosition - transform.position;
                    if (relativePositionTarget.magnitude > relativePosition.magnitude)
                    {
                        targetPosition = g.position;
                    }
                }
            }
            Debug.Log("test");
            needToChangeTarget = false;
        }
        currentNavMeshAgent.SetDestination(targetPosition);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetInstanceID() != transform.GetInstanceID() && collision.transform.tag == "Target")
        {
            Debug.Log("toto");
            targets.Remove(collision.transform);
            needToChangeTarget = true;
            targetPosition = new Vector3(100000, 100000, 100000);
            collision.transform.GetComponent<Rigidbody>().AddForce(new Vector3(0,1000,0));
        }
    }
}
