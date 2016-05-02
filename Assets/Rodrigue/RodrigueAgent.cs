using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class RodrigueAgent : MonoBehaviour {

    public float speed = 10.0f;
    public float acceleration = 20.0f;
    public GameObject[] targetPossible;
    public NavMeshAgent navMeshAgent;
    float distance;
    float currentDistance;
    public GameObject currentTarget;

    public List<GameObject> listOfTarget = new List<GameObject>();
    // Use this for initialization
    void Start () {
        targetPossible = GameObject.FindGameObjectsWithTag("Target");
        foreach(GameObject temp in targetPossible)
        {
            if(temp != this.gameObject)
            {
                listOfTarget.Add(temp);
            }
        }
        InvokeRepeating("GetTarget", 0.5f, 0.5f);
        currentTarget = targetPossible[0];
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;
        navMeshAgent.acceleration = acceleration;
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Target")
        {
            for (int i = 0; i < listOfTarget.Count; i++)
            {
                if(listOfTarget[i] == collision.gameObject)
                {
                    listOfTarget.RemoveAt(i);
                } 
            }
            ChangeTarget();
        }
    }

    void GetTarget()
    {
        if(currentTarget == this.gameObject)
        {
            currentTarget = listOfTarget[1];
        }
        currentDistance = Vector3.Distance(transform.position, currentTarget.transform.position);
        for (int i =0; i < listOfTarget.Count; i++)
        {
            distance = Vector3.Distance(transform.position, listOfTarget[i].transform.position);
            if(distance < currentDistance && listOfTarget[i]!=this.gameObject)
            {
                currentTarget = listOfTarget[i];
            }
        }
        navMeshAgent.SetDestination(currentTarget.transform.position);
    }

    void ChangeTarget()
    {
        currentDistance = 99999f;
        for (int i = 0; i < listOfTarget.Count; i++)
        {
            distance = Vector3.Distance(transform.position, listOfTarget[i].transform.position);
            if (distance < currentDistance && listOfTarget[i] != this.gameObject)
            {
                currentTarget = listOfTarget[i];
                
            }
        }
        navMeshAgent.SetDestination(currentTarget.transform.position);
        if(listOfTarget.Count == 0)
        {
            navMeshAgent.Stop();
        }
    }
}
