using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace IARobin
{
    public class AgentDoubleRobin : Entity
    {
        GameObject Target;
        BoxCollider TargetCollider;

        [Header("GUN")]

        public float RoF = 1.0f;
        public List<GameObject> bullets;
        public GameObject prefabBullet;
        public GameObject predictionZone;


        AgentSimpleRobin tankUnit;

        protected override void Start()
        {
            bullets = new List<GameObject>();
            if (prefabBullet == null)
            {
                prefabBullet = Resources.Load<GameObject>("Bullet");
            }
            tankUnit = transform.parent.parent.GetComponent<AgentSimpleRobin>();
            predictionZone = GameObject.FindGameObjectWithTag("Prediction");
            StartCoroutine(Shoot());

            InvokeRepeating("UpdateTarget", 0.0f, 0.3f);
            base.Start();
        }

        void UpdateTarget()
        {

            GameObject target = null;

            for (int i = tankUnit.targets.Count - 1; i >= 0; --i)
            {
                if (target == null || (target && Vector3.Distance(target.transform.position, transform.position) < Vector3.Distance(tankUnit.targets[i].transform.position, transform.position)))
                {
                    target = tankUnit.targets[i];
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
            bulletScript bullet = prefabBullet.GetComponent<bulletScript>();
            while (tankUnit.isShooting)
            {
                if (TargetCollider)
                {
                    NavMeshAgent targ = Target.GetComponent<NavMeshAgent>();
                    Agent targAgent = Target.GetComponent<Agent>();

                    Vector3 positionPredicted = TargetCollider.transform.position + Vector3.up * 0.5f;

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

                    Vector3 direction = (positionPredicted + TargetCollider.center) - transform.position;

                    if (Physics.Raycast(transform.position, direction.normalized, out hit, Vector3.Distance(positionPredicted, transform.position)))
                    {
                        if (hit.collider.gameObject.CompareTag("Prediction") || hit.collider.gameObject.CompareTag("Target"))
                        {

                            GameObject go = Instantiate(prefabBullet, transform.position + direction.normalized * 2.0f, Quaternion.LookRotation(direction.normalized)) as GameObject;

                            go.GetComponent<bulletScript>().launcherName = AgentRobinMathieu.playerID;

                            bullets.Add(go);
                            yield return new WaitForSeconds(RoF - RoF / 10.0f);
                        }
                    }
                }
                yield return new WaitForSeconds(RoF / 10.0f);
            }
        }
    }

}