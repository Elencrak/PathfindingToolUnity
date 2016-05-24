using UnityEngine;
using System.Collections;
using System;

namespace JojoBehaviourTree
{
    public class Move : AgentNode
    {
        public Move(BehaviourTreeAgent p_agent)
        {
            agent = p_agent;
        }

        public override bool execute()
        {           
            agent.currentTarget = agent.targets[0];
            foreach (Transform g in agent.targets)
            {
                if (agent.GetInstanceID() != agent.currentTarget.GetInstanceID())
                {
                    Vector3 relativePosition;
                    Vector3 relativePositionTarget;
                    relativePosition = g.position - agent.transform.position;
                    relativePositionTarget = agent.currentTarget.position - agent.transform.position;
                    if (relativePositionTarget.magnitude > relativePosition.magnitude)
                    {
                        if (agent.currentTarget != g) { 
                            agent.speedTarget = Vector3.zero;
                            agent.calculSpeed = true;
                        }

                        agent.currentTarget = g;
                    }
                }
            }

            agent.currentNavMeshAgent.SetDestination(agent.currentTarget.position + new Vector3(5, 0, 5));

            return true;
        }
    }
}