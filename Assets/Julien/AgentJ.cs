using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class AgentJ : MonoBehaviour
{
    Vector3 startPosition;

    RaycastHit testVisibility;

    bool canShoot = true;

    int reloadDelay;
    int reload;


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
        player = this.gameObject;
        agent = GetComponent<NavMeshAgent>();
        startPosition = this.gameObject.transform.position;

        //GameObject[] playerArray;
        //playerArray = GameObject.FindGameObjectsWithTag("Target");

        /*foreach (GameObject temp in playerArray)
        {
            if(temp != this.gameObject)
            {
                playerList.Add(temp.transform);
            }
        }*/

        getTarget();
        listTargeTransform();

        InvokeRepeating("FindTarget", 0.5f, 3.0f);
        InvokeRepeating("FireBullet", 0.5f, 1.0f);
    }


    // Update is called once per frame
    void Update()
    {
       transform.LookAt(target);
    }


    void FindTarget()
    {
        float distanceDefaultTarget = 1000000;

        foreach (Transform Tr in playerList)
        {
            float distancePlayerToTarget = Vector3.Distance(transform.position, Tr.position);

            if (distancePlayerToTarget  < distanceDefaultTarget)
            {
                target = Tr;
                distanceDefaultTarget = Vector3.Distance(transform.position, target.position);
            }
        }

        agent.SetDestination(target.position);
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
                Debug.Log("AgentJ a terminé");
                agent.Stop();
            }
        }
        if (col.gameObject.tag == "Bullet")
        {
            Death();
        }
    }


    void FireBullet()
    {

            Vector3 relativePos = target.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);

            GameObject bullet = Instantiate(Resources.Load("Bullet"), transform.position + transform.forward * 1.0f, rotation) as GameObject;
            bullet.GetComponent<bulletScript>().launcherName = transform.parent.GetComponent<TeamNumber>().teamName;
            Physics.IgnoreCollision(this.GetComponent<BoxCollider>(), bullet.GetComponent<CapsuleCollider>());

    }

    void Death()
    {
        agent.Warp(startPosition);
    }

    void DodgeMovement()
    {

    }

    void getTarget()
    {
        GameObject[] bite = GameObject.FindGameObjectsWithTag("Target");
        foreach (GameObject go in bite)
        {
            AgentJ chatte = go.GetComponent<AgentJ>();
            if (!chatte)
            {
                possibleTargets.Add(go);

            }

        }

    }

    void listTargeTransform()
    {
        foreach (GameObject temp in possibleTargets)
        {
            playerList.Add(temp.transform);
        }
    }
}


