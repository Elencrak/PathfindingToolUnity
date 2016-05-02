using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AgentRobinMathieu : MonoBehaviour {
    
    public GameObject nearestTarget;
    BoxCollider nearTargetCollider;
    public List<GameObject> targets;
    public List<GameObject> hitTargets;
    NavMeshAgent agent;
    bool hasWin = false;

    float timeFreezeEnemy = 1.0f;

	// Use this for initialization
	void Start () {
        targets = new List<GameObject>(GameObject.FindGameObjectsWithTag("Target"));
        hitTargets = new List<GameObject>();
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("UpdateTarget", 0.0f, 0.5f);
        InvokeRepeating("UpdateRoad", 0.0f, 0.5f);
        InvokeRepeating("Gagne", 0.0f, 1.5f);
    }
	
	// Update is called once per frame
	void Update ()
    {

	}

	void Gagne ()
    {
        bool isWin = true;
        foreach (GameObject target in targets)
        {
            isWin = isWin && hitTargets.Contains(target);
        }
        if(isWin && !hasWin)
        {
            hasWin = true;
            Debug.Log("J'ai gagné : Robin MATHIEU");
        }
	}

    void UpdateTarget()
    {

        GameObject target = null;

        for (int i = targets.Count - 1; i >= 0 ; --i)
        {
            if (targets[i] == gameObject)
            {
                targets.Remove(targets[i]);
            }
            if (target == null || (target && Vector3.Distance(target.transform.position, transform.position) > Vector3.Distance(targets[i].transform.position, transform.position)))
            {
                if(!hitTargets.Contains(targets[i]))
                {
                    target = targets[i];
                }
            }
        }
        if(target)
        {
            nearestTarget = target;
            nearTargetCollider = target.GetComponent<BoxCollider>();
        }
        else
        {
            nearestTarget = null;
            nearTargetCollider = null;
        }
    }

    void UpdateRoad()
    {
        if(nearestTarget)
        {
            agent.SetDestination(nearestTarget.transform.position + nearTargetCollider.center);
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.CompareTag("Target"))
        {
            StartCoroutine(FreezeEnemy(coll.gameObject));
            hitTargets.Add(coll.gameObject);
        }
    }

    IEnumerator FreezeEnemy(GameObject enemy)
    {
        enemy.GetComponent<NavMeshAgent>().Stop();

        yield return new WaitForSeconds(timeFreezeEnemy);

        enemy.GetComponent<NavMeshAgent>().Resume();
    }
}
