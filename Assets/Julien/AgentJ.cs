using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class AgentJ : MonoBehaviour
{
    Vector3 startPosition;

    public List<Transform> playerList;

    public Transform target;
    NavMeshAgent agent;



    // Use this for initialization
   void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        startPosition = this.gameObject.transform.position;

        GameObject[] playerArray;
        playerArray = GameObject.FindGameObjectsWithTag("Target");
        
        foreach(GameObject temp in playerArray)
        {
            if(temp != this.gameObject)
            {
                playerList.Add(temp.transform);
            }
        }

        InvokeRepeating("FindTarget", 0.5f, 0.5f);
        InvokeRepeating("FireBullet", 0.5f, 1.0f);
    }


    // Update is called once per frame
    void Update()
    {
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

        GameObject bullet = Instantiate(Resources.Load("Bullet"), transform.position + transform.forward*2.0f, rotation) as GameObject;
    }

    void Death()
    {
        Debug.Log("mort");
        agent.Warp(startPosition);

    }

}


