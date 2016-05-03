using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AgentDoubleRobin : MonoBehaviour
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

        GameObject target = null;

        for (int i = tankUnit.targets.Count - 1; i >= 0; --i)
        {
            if (target == null || (target && Vector3.Distance(target.transform.position, transform.position) < Vector3.Distance(tankUnit.targets[i].transform.position, transform.position)))
            {
                if (!tankUnit.hitTargets.Contains(tankUnit.targets[i]))
                {
                    target = tankUnit.targets[i];
                }
            }
        }
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
        while (tankUnit.isShooting)
        {
            if (TargetCollider)
            {
                Vector3 direction = (TargetCollider.transform.position + TargetCollider.center) - transform.position;

                GameObject go = Instantiate(prefabBullet, transform.position + direction.normalized * 2.0f, Quaternion.LookRotation(direction.normalized)) as GameObject;
                
                go.GetComponent<bulletScript>().launcherName = AgentRobinMathieu.playerID;

                bullets.Add(go);
            }
            yield return new WaitForSeconds(RoF);
        }
    }
}
