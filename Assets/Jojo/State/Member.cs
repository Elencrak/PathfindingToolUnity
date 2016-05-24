using UnityEngine;
using System.Collections;

namespace JojoKiller { 
    public class Member : MonoBehaviour
    {
        public float fireRate;
        public float idleTime;
        public float walkTime;

        public float timerShoot;
        public float timerIdle;
        public float timerWalk;

        private NavMeshAgent currentNavMeshAgent;
        private Vector3 startPosition;

        // Use this for initialization
        void Start()
        {
            timerShoot = fireRate;
            timerIdle = idleTime;
            timerWalk = walkTime;

            startPosition = transform.position;

            currentNavMeshAgent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
            if (timerShoot > 0)
            {
                timerShoot -= Time.deltaTime;
            }

            if (timerIdle > 0)
            {
                timerIdle -= Time.deltaTime;
            }

            if (timerWalk > 0)
            {
                timerWalk -= Time.deltaTime;
            }
        }

        public bool canShoot()
        {
            return timerShoot <= 0;
        }

        public  bool changeToIdle()
        {
            return timerIdle <= 0;
        }

        public  bool changeToWalk()
        {
            return timerWalk <= 0;
        }

        // Execute les actions
        public void chase()
        {
            /*if (nextShoot <= 0)
            {
                foreach (Transform g in stateTargets)
                {
                    if (g.GetInstanceID() != gameObject.GetInstanceID())
                    {
                        Vector3 relativePosition;
                        relativePosition = g.position - transform.position;
                        RaycastHit hit;
                        if (Physics.Raycast(transform.position, relativePosition.normalized, out hit, 1000))
                        {
                            if (hit.transform.tag == "Target")
                            {
                                Debug.DrawRay(transform.position, relativePosition.normalized * 5, Color.red, 1);
                                GameObject temp = Instantiate(Resources.Load("Bullet"), transform.position + relativePosition.normalized * 10, Quaternion.identity) as GameObject;

                                //Anticipation
                                float t = Vector3.Distance(g.position, transform.position) / temp.GetComponent<bulletScript>().speed;
                                Vector3 tempPosition = g.position + (g.GetComponent<NavMeshAgent>().velocity * t);
                                temp.transform.LookAt(tempPosition);

                                temp.GetComponentInParent<bulletScript>().launcherName = transform.parent.GetComponent<TeamNumber>().teamName;
                                nextShoot = fireRate;
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                nextShoot -= Time.deltaTime;
            }*/
            Debug.Log("Tire bang!");
            timerShoot = fireRate;
        }

        public void search()
        {
            Debug.Log("Move Walk!");
            timerWalk = walkTime;
        }

        public void idle()
        {
            Debug.Log("gogole");
            timerIdle = idleTime;
        }

        void deadPosition()
        {
            currentNavMeshAgent.Warp(startPosition);
        }

        void OnCollisionEnter(Collision collision)
        {
            /*if (collision.transform.GetInstanceID() != transform.GetInstanceID() && collision.transform.tag == "Target")
            {
                needToChangeTarget = true;
                targetPosition.position = new Vector3(100000, 100000, 100000);        
            } else if(collision.transform.tag == "Bullet")
            {
                toto();
            }*/
        }
    }
}
