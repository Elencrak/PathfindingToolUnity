using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AgentBQY : MonoBehaviour {

    GameObject[] destinations;
    //Vrai si la destination a été touchée, faux sinon.
    Dictionary<GameObject, bool> hitDestinations;
    NavMeshAgent navMeshAgent;
    int hitNumber;
    int numberToHit;

    GameObject currentDestination;
    Transform tTransform;

	// Use this for initialization
	void Start () {
        currentDestination = null;
        tTransform = this.transform;
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        hitDestinations = new Dictionary<GameObject, bool>();
        InvokeRepeating("UpdatePath", 0.1f, 0.1f);
        destinations = GameObject.FindGameObjectsWithTag("Target");
        numberToHit = destinations.Length - 1;
        hitNumber = 0;
        foreach (GameObject d in destinations) {
            if (d.GetInstanceID() != this.gameObject.GetInstanceID()) {
                
                hitDestinations.Add(d, false);
            } else {
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	    
	}
    
    void OnCollisionEnter(Collision c) {
        if (hitDestinations.ContainsKey(c.gameObject)) {
            if (!hitDestinations[c.gameObject]) {
                hitDestinations[c.gameObject] = true;
                hitNumber++;
                if (hitNumber >= numberToHit) {
                    Debug.Log("AgentBQY finished!");
                }
                UpdatePath();
            }
        }
    }

    void UpdatePath() {

        if (hitNumber < numberToHit) {
            foreach (GameObject d in hitDestinations.Keys) {
                if (!hitDestinations[d]) {
                    if(currentDestination == null) {
                        currentDestination = d;
                        return;
                    } else if(Vector3.Distance(tTransform.position, currentDestination.transform.position) <= Vector3.Distance(tTransform.position, d.transform.position)) {
                        currentDestination = d;
                    }
                    Vector3 predictedDestination;
                    /*On récupère la direction prévue par l'agent*/
                    if (currentDestination.GetComponent<NavMeshAgent>().hasPath) {
                        predictedDestination = currentDestination.GetComponent<NavMeshAgent>().path.corners[0];
                    } else {
                        predictedDestination = tTransform.position;
                    }
                    /*Si la direction prévue est plus proche que la position actuelle.*/
                    if ((Vector3.Distance(tTransform.position, predictedDestination)) <= Vector3.Distance(tTransform.position, currentDestination.transform.position)) {
                        navMeshAgent.SetDestination(currentDestination.transform.position);
                    } else {
                        navMeshAgent.SetDestination(predictedDestination);
                    }
                }
            }
        } else {
            navMeshAgent.Stop();
        }
    }
}
