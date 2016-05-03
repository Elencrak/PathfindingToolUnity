using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AgentJojoKiller : MonoBehaviour {


    bool doOnce = true;
    public List<Transform> targets;
    public Transform targetPosition;
    bool touch;
    NavMeshAgent currentNavMeshAgent;
    public bool needToChangeTarget;
    float fireRate = 1;
    float nextShoot;
    public Vector3 startPosition;
    private float nextMove = 5;
    private float move;

    // Use this for initialization
    void Start () {
        currentNavMeshAgent = GetComponent<NavMeshAgent>();
        needToChangeTarget = true;
        startPosition = transform.position;    
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

        if (needToChangeTarget || nextMove <= 0) {
            targetPosition = targets[0];
            foreach (Transform g in targets)
            {
                if (g.GetInstanceID() != gameObject.GetInstanceID())
                {
                    Vector3 relativePosition;
                    Vector3 relativePositionTarget;
                    relativePosition  = g.position - transform.position;
                    relativePositionTarget = targetPosition.position - transform.position;                      
                    if (relativePositionTarget.magnitude > relativePosition.magnitude)
                    {
                        targetPosition = g;                     
                    }
                }
            }
            needToChangeTarget = false;
            nextMove = move;

            currentNavMeshAgent.SetDestination(targetPosition.position);
        }
        else
        {
            nextMove -= Time.deltaTime;
        } 

        if (nextShoot <= 0)
        {
            foreach (Transform g in targets)
            {
                if (g.GetInstanceID() != gameObject.GetInstanceID())
                {
                    Vector3 relativePosition;                    
                    relativePosition = g.position - transform.position;
                    RaycastHit hit;
                    if ( Physics.Raycast(transform.position, relativePosition.normalized, out hit, 1000))
                    {
                        if(hit.transform.tag == "Target")
                        {
                            Debug.DrawRay(transform.position, relativePosition.normalized *5, Color.red, 1);
                            GameObject temp = Instantiate(Resources.Load("Bullet"), transform.position + relativePosition.normalized * 3, Quaternion.identity) as GameObject;
                            temp.transform.LookAt(transform.position + relativePosition.normalized*10);
                            temp.GetComponentInParent<bulletScript>().launcherName = transform.parent.GetComponent<TeamNumber>().teamName;
                            nextShoot = fireRate;
                            break;
                        }
                    }
                }
            }
        }
        else
        {
            nextShoot -= Time.deltaTime;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetInstanceID() != transform.GetInstanceID() && collision.transform.tag == "Target")
        {
            needToChangeTarget = true;
            targetPosition.position = new Vector3(100000, 100000, 100000);        
        } else if(collision.transform.tag == "Bullet")
        {
            toto();
        }
    }

    void toto()
    {
        Debug.Log(startPosition);
        Debug.Log(transform.position);
 
        //transform.position = startPosition;
        currentNavMeshAgent.Warp(startPosition);
        needToChangeTarget = true;
    }
}
