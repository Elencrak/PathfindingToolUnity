using UnityEngine;
using System.Collections;
using System;

namespace JojoBehaviourTree
{
    public class Shoot : AgentNode
    {

        public Shoot(BehaviourTreeAgent p_agent)
        {
            agent = p_agent;
        }

        public override bool execute()
        {
            if (agent.canShoot()) {
                Debug.Log("Shoot");
                Vector3 relativePosition;
                relativePosition = agent.currentTarget.position - agent.transform.position;
                // Debug.DrawRay(agent.transform.position, agent.relativePosition.normalized * 5, Color.red, 1);
                GameObject temp = MonoBehaviour.Instantiate(Resources.Load("Bullet"), agent.transform.position + relativePosition.normalized * 2, Quaternion.identity) as GameObject;

                //Anticipation
                if(agent.currentTarget.GetComponent<NavMeshAgent>() != null)
                {
                    float t = Vector3.Distance(agent.currentTarget.position, agent.transform.position) / temp.GetComponent<bulletScript>().speed;
                    Vector3 tempPosition = agent.currentTarget.position + (agent.currentTarget.GetComponent<NavMeshAgent>().velocity * t);
                    temp.transform.LookAt(tempPosition);
                }
                else
                {
                    float t = Vector3.Distance(agent.currentTarget.position, agent.transform.position) / temp.GetComponent<bulletScript>().speed;
                    Vector3 tempPosition = agent.currentTarget.position + (agent.speedTarget * t);
                    temp.transform.LookAt(tempPosition);
                }

                temp.GetComponentInParent<bulletScript>().launcherName = agent.transform.parent.GetComponent<TeamNumber>().teamName;

                agent.resetShoot();

                return true;
            }
            return false;
        }
    }
}
