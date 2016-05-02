using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Will_IA_m : MonoBehaviour {
    List<GameObject> targets;
    GameObject currentTarget;
    NavMeshAgent agent;

    void Start () {
        agent = GetComponent<NavMeshAgent>();
        targets = new List<GameObject>(GameObject.FindGameObjectsWithTag("Target"));
        targets.Remove(this.gameObject);
        targetUpdate();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void targetUpdate()
    {
        if (targets.Count == 0)
        {
            win();
            return;
        }
        GameObject tempTarget= targets[0];
        Vector3 myPos = transform.position;
        float distance = Vector3.Distance(myPos, targets[0].transform.position);

        for (int i = 1; i < targets.Count; i++)
        {
            float temp = Vector3.Distance(myPos, targets[i].transform.position);
            if (temp < distance)
            {
                distance = temp;
                tempTarget = targets[i];
            }
        }
        currentTarget = tempTarget;
        // Petite aide :)
        // currentTarget.GetComponent<NavMeshAgent>().SetDestination(transform.position);
        agent.SetDestination(currentTarget.transform.position);
        
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.gameObject == currentTarget)
        {
            targets.Remove(currentTarget);
            targetUpdate();
        }
    }


    void win()
    {
        currentTarget = null;
        agent.SetDestination(new Vector3(1,15,2));
        Debug.Log("Mission completed by " + gameObject.name);
    }
}
