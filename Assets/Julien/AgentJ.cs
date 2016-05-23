using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AgentJ : MonoBehaviour
{
    Vector3 startPosition;

    RaycastHit testVisibility;

    bool canShoot = true;



    float fireRateMax = 1.0f;
    float fireRate;

    public Transform target;

    NavMeshAgent agent;

    GameObject player;
    GameObject player2;
    GameObject player3;

    public List<Transform> playerList;

    public List<GameObject> possibleTargets;


    public List<GameObject> mySquad;


    // Use this for initialization
   void Start()
    {
        fireRate = 0.0f; 

        player = this.gameObject;
        agent = GetComponent<NavMeshAgent>();
        startPosition = this.gameObject.transform.position;

        getTarget();
        listTargeTransform();

        InvokeRepeating("FindTarget", 0.5f, 0.1f);
        InvokeRepeating("checkForFire", 0.1f, 0.1f);
    }


    // Update is called once per frame
    void Update()
    {
       fireRate -= Time.deltaTime;
       transform.LookAt(target);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Target")
        {
            playerList.Remove(col.gameObject.transform);

            if (playerList.Count > 0)
            {
                FindTarget();
            }
            else
            {
                //Debug.Log("AgentJ a terminé");
                agent.Stop();
            }
        }
        if (col.gameObject.tag == "Bullet")
        {
            Death();
        }
    }

    void checkForFire()
    {
        if (target)
        {
            RaycastHit rayCharles;
            Physics.Raycast(this.gameObject.transform.position, this.gameObject.transform.forward, out rayCharles);

            if (rayCharles.collider.gameObject == target.gameObject)
            {
                if (fireRate < 0)
                {
                    FireBullet();
                    fireRate = fireRateMax;
                }
            }
            else
            {
                FindTarget();
            }
        }
    }


    void FireBullet()
    {
        
            Vector3 relativePos = target.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);

            GameObject bullet = Instantiate(Resources.Load("Bullet"), transform.position + transform.forward * 1.0f, rotation) as GameObject;
            //bullet.GetComponent<bulletScript>().launcherName = transform.parent.GetComponent<TeamNumber>().teamName;
            Physics.IgnoreCollision(this.GetComponent<BoxCollider>(), bullet.GetComponent<CapsuleCollider>());   
    }

   /* IEnumerator setTarget()
    {
       
        yield return null;
    }*/

    void getTarget()
    {
        GameObject[] ennemis = GameObject.FindGameObjectsWithTag("Target");
        foreach (GameObject go in ennemis)
        {
            AgentJ mesAgents = go.GetComponent<AgentJ>();
            if (!mesAgents)
            {
                possibleTargets.Add(go);
            }
        }
    }


    void listTargeTransform()
    {
        foreach (GameObject go in possibleTargets)
        {
            playerList.Add(go.transform);
        }
    }


    void FindTarget()
    {
        float distanceDefaultTarget = 1000000;
        GameObject betterChoice = null;

        foreach (Transform Tr in playerList)
        {
            float distancePlayerToTarget = Vector3.Distance(transform.position, Tr.position);

            if (distancePlayerToTarget < distanceDefaultTarget)
            {
                target = Tr;
                RaycastHit rayCharles;

                distanceDefaultTarget = Vector3.Distance(transform.position, target.position);
                Physics.Raycast(this.gameObject.transform.position, this.gameObject.transform.forward, out rayCharles);

                if (rayCharles.collider.gameObject == target.gameObject)
                {
                    betterChoice = target.gameObject;
                }                
            }

        }

        if (betterChoice != null)
        {
            target = betterChoice.transform;
        }

        agent.SetDestination(target.position);
    }

    void Death()
    {
        agent.Warp(startPosition);
    }
}
    

