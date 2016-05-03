using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AgentTripleRobin : MonoBehaviour
{
    GameObject Target;
    BoxCollider TargetCollider;

    [Header("GUN")]

    public float RoF = 1.0f;
    public List<GameObject> bullets;
    public GameObject prefabBullet;


    AgentSimpleRobin tankUnit;

    void Start()
    {
        bullets = new List<GameObject>();
        if (prefabBullet == null)
        {
            prefabBullet = Resources.Load<GameObject>("Bullet");
        }
        tankUnit = transform.parent.parent.GetComponent<AgentSimpleRobin>();
        StartCoroutine(Shoot());

        InvokeRepeating("UpdateTarget", 0.0f, 0.5f);
        InvokeRepeating("UpdateRoad", 0.0f, 0.8f);
    }

    void UpdateTarget()
    {

        GameObject target = tankUnit.targets[Random.Range(0, tankUnit.targets.Count - 1)];

        if (target)
        {
            Target = target;
            TargetCollider = target.GetComponent<BoxCollider>();
        }
        else
        {
            Target = null;
            TargetCollider = null;
        }
    }

    IEnumerator Shoot()
    {
        bulletScript bullet = prefabBullet.GetComponent<bulletScript>();
        while (tankUnit.isShooting)
        {
            if (TargetCollider)
            {
                NavMeshAgent targ = Target.GetComponent<NavMeshAgent>();

                Vector3 positionPredicted = TargetCollider.transform.position;

                float distanceParcourue = 0.0f;

                while (Vector3.Distance(transform.position, positionPredicted) - distanceParcourue > float.Epsilon)
                {
                    positionPredicted += Target.GetComponent<NavMeshAgent>().velocity * Time.fixedDeltaTime;
                    distanceParcourue += Time.fixedDeltaTime * bullet.speed;
                }

                RaycastHit hit;

                if (Physics.Raycast(transform.position, positionPredicted, out hit))
                {
                    if (!hit.collider.gameObject.CompareTag("Target"))
                    {
                        yield return new WaitForFixedUpdate();
                        continue;
                    }
                }

                Vector3 direction = (positionPredicted + TargetCollider.center) - transform.position;

                GameObject go = Instantiate(prefabBullet, transform.position + direction.normalized * 2.0f, Quaternion.LookRotation(direction.normalized)) as GameObject;

                go.GetComponent<bulletScript>().launcherName = AgentRobinMathieu.playerID;

                bullets.Add(go);
            }
            yield return new WaitForSeconds(RoF);
        }
    }
}
