using UnityEngine;
using System.Collections;

public class patrouilleWill : MonoBehaviour {
    NavMeshAgent agent;
    public GameObject[] waypoints;
    int cmpt = 0;
	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(waypoints[cmpt].transform.position);
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(waypoints[cmpt].transform.position, transform.position)<1)
        {
            cmpt++;
            if (cmpt > 1)
                cmpt = 0;
            agent.SetDestination(waypoints[cmpt].transform.position);
        }
	}
}
