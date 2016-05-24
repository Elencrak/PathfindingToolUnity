using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace IARobin
{
    public class AgentDoubleRobin : AgentRobinMathieu
    {
        GameObject Target;
        BoxCollider TargetCollider;

        [Header("GUN")]

        public float RoF = 1.0f;
        public GameObject prefabBullet;
        public GameObject predictionZone;

        protected override void Start()
        {
            if (prefabBullet == null)
            {
                prefabBullet = Resources.Load<GameObject>("Bullet");
            }
            base.Start();
            predictionZone = GameObject.FindGameObjectWithTag("Prediction");
            
        }

        protected override void UpdateTarget()
        {

            GameObject target = null;

            for (int i = targets.Count - 1; i >= 0; --i)
            {
                if (target == null || (target && Vector3.Distance(target.transform.position, transform.position) < Vector3.Distance(targets[i].transform.position, transform.position)))
                {
                    target = targets[i];
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

        protected override void UpdateTargetBullet()
        {
            if (targets.Count > 0 && (!TargetBullet || !targets.Contains(TargetBullet)))
            {
                ForceUpdateTargetBullet();
            }
        }

        protected override IEnumerator ShootEnemy()
        {
            bulletScript bullet = prefabBullet.GetComponent<bulletScript>();
            if (nearTargetCollider)
            {
                NavMeshAgent targ = nearestTarget.GetComponent<NavMeshAgent>();
                Agent targAgent = nearestTarget.GetComponent<Agent>();

                Vector3 positionPredicted = nearTargetCollider.transform.position + Vector3.up * 0.5f;

                float distanceParcourue = 0.0f;
                if (targ)
                {
                    while (Vector3.Distance(transform.position, positionPredicted) - distanceParcourue > float.Epsilon)
                    {
                        positionPredicted += targ.velocity * Time.fixedDeltaTime;
                        distanceParcourue += Time.fixedDeltaTime * bullet.speed;
                    }
                }
                else if (targAgent)
                {
                    while (Vector3.Distance(transform.position, positionPredicted) - distanceParcourue > float.Epsilon)
                    {
                        positionPredicted += (targAgent.target.transform.position - targAgent.transform.position) * Time.fixedDeltaTime;
                        distanceParcourue += Time.fixedDeltaTime * bullet.speed;
                    }
                }

                RaycastHit hit;

                predictionZone.transform.position = positionPredicted;

                Vector3 direction = (positionPredicted + nearTargetCollider.center) - transform.position;

                if (Physics.Raycast(transform.position, direction.normalized, out hit, Vector3.Distance(positionPredicted, transform.position)))
                {
                    if (hit.collider.gameObject.CompareTag("Prediction") || (hit.collider.gameObject.CompareTag("Target") && !hit.collider.GetComponent<AgentRobinMathieu>()))
                    {
                        Debug.Log("Shoot " + 2);

                        GameObject go = Instantiate(prefabBullet, transform.position + direction.normalized * 2.0f, Quaternion.LookRotation(direction.normalized)) as GameObject;

                        go.GetComponent<bulletScript>().launcherName = AgentRobinMathieu.playerID;
                        yield return new WaitForSeconds(RoF - RoF / 10.0f);
                    }
                }
            }
            yield return new WaitForSeconds(RoF / 10.0f);
        }

        protected override IEnumerator ShootBullet()
        {
            bulletScript bullet = prefabBullet.GetComponent<bulletScript>();

            if (TargetBullet)
            {
                bulletScript targ = TargetBullet.GetComponent<bulletScript>();

                Vector3 positionPredicted = TargetBullet.transform.position + Vector3.up * 0.5f;

                float distanceParcourue = 0.0f;
                while (Vector3.Distance(transform.position, positionPredicted) - distanceParcourue > float.Epsilon)
                {
                    positionPredicted += (targ.transform.position + targ.transform.forward) * Time.fixedDeltaTime * targ.speed;
                    distanceParcourue += Time.fixedDeltaTime * bullet.speed;
                }

                RaycastHit hit;

                predictionZone.transform.position = positionPredicted;

                Vector3 direction = positionPredicted - transform.position;

                if (Physics.Raycast(transform.position, direction.normalized, out hit, Vector3.Distance(positionPredicted, transform.position)))
                {
                    if (hit.collider.gameObject.CompareTag("Prediction") || hit.collider.gameObject.CompareTag("Bullet"))
                    {
                        Debug.Log("Shoot Bullet " + 2);
                        GameObject go = Instantiate(prefabBullet, transform.position + direction.normalized * 2.0f, Quaternion.LookRotation(direction.normalized)) as GameObject;

                        go.GetComponent<bulletScript>().launcherName = AgentRobinMathieu.playerID;
                        yield return new WaitForSeconds(RoF - RoF / 10.0f);
                    }
                    else
                    {
                        UpdateTargetBullet();
                    }
                }
            }
            yield return new WaitForSeconds(RoF / 10.0f);
        }
    }

}