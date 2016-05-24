using UnityEngine;
using System.Collections;

public class RodrigueDeath : MonoBehaviour {

    NavMeshAgent navMeshAgent;
    Vector3 spawnPoint;
    GameObject parent;
    RodrigueAgent agent;
	// Use this for initialization
	void Start () {
        spawnPoint = transform.position;
        parent = transform.parent.gameObject;
        agent = parent.GetComponent<RodrigueAgent>();
        navMeshAgent = parent.GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            StartCoroutine(agent.GetComponentInParent<TeamLeader>().isDead());
            navMeshAgent.Warp(spawnPoint);
            navMeshAgent.SetDestination(agent.interestPoints[0].transform.position);
            agent.nbOfDeath++;
        }
    }
}
