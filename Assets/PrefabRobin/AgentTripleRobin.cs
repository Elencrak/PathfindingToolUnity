using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace IARobin
{
    public class AgentTripleRobin : Entity
    {
        GameObject Target;

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

        private void ForceUpdateTarget()
        {
            GameObject target = tankUnit._bullets[Random.Range(0, tankUnit._bullets.Count - 1)];

            if (target)
            {
                Target = target;
            }
            else
            {
                Target = null;
            }
        }

        void UpdateTarget()
        {
            if (tankUnit._bullets.Count > 0 && !Target && !tankUnit._bullets.Contains(Target))
            {
                ForceUpdateTarget();
            }
        }

        IEnumerator Shoot()
        {
            //transform.position + transform.forward * Time.fixedDeltaTime * speed; direction && speed for bullet

            bulletScript bullet = prefabBullet.GetComponent<bulletScript>();

            while (tankUnit.isShooting)
            {
                if (Target)
                {
                    bulletScript targ = Target.GetComponent<bulletScript>();

                    Vector3 positionPredicted = Target.transform.position + Vector3.up * 0.5f;

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
                            GameObject go = Instantiate(prefabBullet, transform.position + direction.normalized * 2.0f, Quaternion.LookRotation(direction.normalized)) as GameObject;

                            go.GetComponent<bulletScript>().launcherName = AgentRobinMathieu.playerID;

                            bullets.Add(go);
                            yield return new WaitForSeconds(RoF - RoF / 10.0f);
                        }
                        else
                        {
                            ForceUpdateTarget();
                        }
                    }
                }
                yield return new WaitForSeconds(RoF / 10.0f);
            }

            //bulletScript bullet = prefabBullet.GetComponent<bulletScript>();
            //while (tankUnit.isShooting)
            //{
            //    if (TargetCollider)
            //    {
            //        NavMeshAgent targ = Target.GetComponent<NavMeshAgent>();
            //        Agent targAgent = Target.GetComponent<Agent>();

            //        Vector3 positionPredicted = TargetCollider.transform.position + Vector3.up * 0.5f;

            //        float distanceParcourue = 0.0f;
            //        if (targ)
            //        {
            //            while (Vector3.Distance(transform.position, positionPredicted) - distanceParcourue > float.Epsilon)
            //            {
            //                positionPredicted += targ.velocity * Time.fixedDeltaTime;
            //                distanceParcourue += Time.fixedDeltaTime * bullet.speed;
            //            }
            //        }
            //        else if (targAgent)
            //        {
            //            while (Vector3.Distance(transform.position, positionPredicted) - distanceParcourue > float.Epsilon)
            //            {
            //                positionPredicted += (targAgent.target.transform.position - targAgent.transform.position) * Time.fixedDeltaTime;
            //                distanceParcourue += Time.fixedDeltaTime * bullet.speed;
            //            }
            //        }

            //        RaycastHit hit;

            //        predictionZone.transform.position = positionPredicted;

            //        Vector3 direction = (positionPredicted + TargetCollider.center) - transform.position;

            //        if (Physics.Raycast(transform.position, direction.normalized, out hit, Vector3.Distance(positionPredicted, transform.position)))
            //        {
            //            if (hit.collider.gameObject.CompareTag("Prediction") || hit.collider.gameObject.CompareTag("Target"))
            //            {

            //                GameObject go = Instantiate(prefabBullet, transform.position + direction.normalized * 2.0f, Quaternion.LookRotation(direction.normalized)) as GameObject;

            //                go.GetComponent<bulletScript>().launcherName = AgentRobinMathieu.playerID;

            //                bullets.Add(go);
            //                yield return new WaitForSeconds(RoF - RoF / 10.0f);
            //            }
            //        }
            //    }
            //    yield return new WaitForSeconds(RoF / 10.0f);
            //}
        }
    }

}