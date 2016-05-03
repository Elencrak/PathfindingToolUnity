using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AgentRobinMathieu : MonoBehaviour {

    [Header("IA")]

    public Vector3 startPoint;
    public GameObject nearestTarget;
    BoxCollider nearTargetCollider;
    public List<GameObject> targets;
    public List<GameObject> hitTargets;
    NavMeshAgent agent;
    bool hasWin = false;

    [Header("GUN")]

    public float RoF = 1.0f;
    public List<GameObject> bullets;
    public GameObject prefabBullet;
    GameObject bulletList;

    [Header("Values")]

    float timeFreezeEnemy = 1.0f;
    public bool isShooting = true;

    // Use this for initialization
    void Start () {

        bullets = new List<GameObject>();
        if(prefabBullet == null)
        {
            prefabBullet = Resources.Load<GameObject>("Bullet");
        }
        bulletList = transform.parent.GetChild(1).gameObject;
        startPoint = transform.position;
        targets = new List<GameObject>(GameObject.FindGameObjectsWithTag("Target"));
        hitTargets = new List<GameObject>();
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("UpdateTarget", 0.0f, 0.5f);
        InvokeRepeating("UpdateRoad", 0.0f, 0.8f);
        InvokeRepeating("Gagne", 0.0f, 1.5f);
        StartCoroutine(Shoot());
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

    /*void UpdateRoad()
    {
        if(nearestTarget)
        {
            agent.SetDestination(nearTargetCollider.transform.position + nearTargetCollider.center);
        }
    }*/

    void UpdateRoad()
    {
        agent.SetDestination(targets[Random.Range(0, targets.Count - 1)].transform.position);
    }

    void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.CompareTag("Target"))
        {
            StartCoroutine(FreezeEnemy(coll.gameObject));
            hitTargets.Add(coll.gameObject);
        }
        if(coll.gameObject.CompareTag("Bullet") && !bullets.Contains(coll.gameObject))
        {
            Restart();
        }
    }

    IEnumerator Shoot()
    {
        while(isShooting)
        {
            if(nearTargetCollider)
            {
                Vector3 direction = (nearTargetCollider.transform.position + nearTargetCollider.center) - transform.position;

                GameObject go = Instantiate(prefabBullet, transform.position + direction.normalized * 2.0f, Quaternion.LookRotation(direction.normalized)) as GameObject;

                go.transform.parent = bulletList.transform;

                bullets.Add(go);
            }
            yield return new WaitForSeconds(RoF);
        }
    }

    void Restart()
    {
        agent.Warp(startPoint);
    }

    IEnumerator FreezeEnemy(GameObject enemy)
    {
        enemy.GetComponent<NavMeshAgent>().Stop();

        yield return new WaitForSeconds(timeFreezeEnemy);

        enemy.GetComponent<NavMeshAgent>().Resume();
    }
}
