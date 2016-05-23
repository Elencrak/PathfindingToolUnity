using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace IARobin
{

    public class AgentSimpleRobin : AgentRobinMathieu
    {

        [Header("GUN")]

        public float RoF = 1.0f;
        public List<GameObject> bullets;
        public GameObject prefabBullet;
        public GameObject predictionZone;

        protected override void Start()
        {
            base.Start();
            bullets = new List<GameObject>();
            if (prefabBullet == null)
            {
                prefabBullet = Resources.Load<GameObject>("Bullet");
            }

            predictionZone = transform.parent.Find("PredictionZone").gameObject;

            StartCoroutine(Shoot());
        }

        protected override void UpdateRoadEsquive()
        {
            base.UpdateRoadEsquive();
        }

        protected override void UpdateRoadRandom()
        {
            base.UpdateRoadRandom();
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