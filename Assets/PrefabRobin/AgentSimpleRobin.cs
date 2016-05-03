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
        while (isShooting)
        {
            if (nearTargetCollider)
            {
                Vector3 direction = (nearTargetCollider.transform.position + nearTargetCollider.center) - transform.position;

                GameObject go = Instantiate(prefabBullet, transform.position + direction.normalized * 2.0f, Quaternion.LookRotation(direction.normalized)) as GameObject;
                
                go.GetComponent<bulletScript>().launcherName = playerID;

                bullets.Add(go);
            }
            yield return new WaitForSeconds(RoF);
        }
    }
}
