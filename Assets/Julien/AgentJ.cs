using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AgentJ : MonoBehaviour
{

    public List<Transform> playerList;

    public Transform target;
    NavMeshAgent agent;

    // Use this for initialization
   void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        GameObject[] playerArray;
        playerArray = GameObject.FindGameObjectsWithTag("Target");

        
        foreach(GameObject temp in playerArray)
        {
            if(temp != this.gameObject)
            {
                playerList.Add(temp.transform);
            }
        }

        InvokeRepeating("findTarget", 0.5f, 0.5f);
    }

    // Update is called once per frame
   void Update()
    {
    }

    void findTarget()
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
                findTarget();
            }
            else
            {
                Debug.Log("AgentJ a terminé");
                agent.Stop();
            }

        }

    }


}


