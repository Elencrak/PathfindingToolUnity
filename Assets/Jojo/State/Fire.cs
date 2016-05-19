using UnityEngine;
using System.Collections;
using System;

namespace JojoKiller
{
    // Prendre la décision
    public class Fire : IStateAgent
    {
        public Fire(Member currentAgent)
        {
            member = currentAgent;
        }

        public override IState execution()
        {
            IState temp = base.execution();
            if (temp == this)
            {
                Debug.Log("Fire");
                member.shoot();
            }
            return temp;
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
        }
    }
}
