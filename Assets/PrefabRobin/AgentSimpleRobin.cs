using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AgentSimpleRobin : AgentRobinMathieu
{

    [Header("GUN")]

    public float RoF = 1.0f;
    public List<GameObject> bullets;
    public GameObject prefabBullet;

    protected override void Start()
    {
        base.Start();
        bullets = new List<GameObject>();
        if (prefabBullet == null)
        {
            prefabBullet = Resources.Load<GameObject>("Bullet");
        }
        StartCoroutine(Shoot());

        InvokeRepeating("UpdateTarget", 0.0f, 0.5f);
        InvokeRepeating("UpdateRoad", 0.0f, 0.8f);
    }

    protected override void UpdateRoad()
    {
        base.UpdateRoad();
    }

    protected override void UpdateTarget()
    {
        base.UpdateTarget();
    }

    IEnumerator Shoot()
    {
        bulletScript bullet = prefabBullet.GetComponent<bulletScript>();
        while (isShooting)
        {
            if (nearTargetCollider)
            {
                NavMeshAgent targ = nearestTarget.GetComponent<NavMeshAgent>();

                Vector3 positionPredicted = nearTargetCollider.transform.position;

                float distanceParcourue = 0.0f;

                while (Vector3.Distance(transform.position, positionPredicted) - distanceParcourue > float.Epsilon)
                {
                    positionPredicted += nearestTarget.GetComponent<NavMeshAgent>().velocity * Time.fixedDeltaTime;
                    distanceParcourue += Time.fixedDeltaTime * bullet.speed;
                }

                RaycastHit hit;

                if (Physics.Raycast(transform.position, positionPredicted, out hit))
                {
                    if(!hit.collider.gameObject.CompareTag("Target"))
                    {
                        yield return new WaitForFixedUpdate();
                        continue;
                    }
                }

                Vector3 direction = (positionPredicted + nearTargetCollider.center) - transform.position;

                GameObject go = Instantiate(prefabBullet, transform.position + direction.normalized * 2.0f, Quaternion.LookRotation(direction.normalized)) as GameObject;
                
                go.GetComponent<bulletScript>().launcherName = playerID;

                bullets.Add(go);
            }
            yield return new WaitForSeconds(RoF);
        }
    }
}
