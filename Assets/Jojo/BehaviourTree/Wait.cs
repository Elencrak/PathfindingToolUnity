using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JojoBehaviourTree
{
    class Wait : AgentNode
    {

        public Wait(BehaviourTreeAgent p_agent)
        {
            agent = p_agent;
        }

        public override bool execute()
        {
            if (!agent.canShoot())
            {
                agent.currentNavMeshAgent.SetDestination(agent.startPosition);
                return true;
            }

            return false;
        }
    }
}
