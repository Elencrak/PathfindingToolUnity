using UnityEngine;
using System.Collections;
using System;

namespace JojoBehaviourTree
{
    public class SeeOpponent : AgentNode
    {

        public SeeOpponent(BehaviourTreeAgent p_agent)
        {
            agent = p_agent;
        }

        public override bool execute()
        {
            foreach (Transform g in agent.targets)
            {
                if (g.GetInstanceID() != agent.gameObject.GetInstanceID())
                {
                    Vector3 relativePosition;
                    relativePosition = g.position - agent.transform.position;
                    RaycastHit hit;
                    Debug.DrawRay(agent.transform.position, relativePosition.normalized *10,Color.red);
                    if (Physics.Raycast(agent.transform.position, relativePosition.normalized, out hit, 1000))
                    {
                        if (hit.transform.tag == "Target")
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}