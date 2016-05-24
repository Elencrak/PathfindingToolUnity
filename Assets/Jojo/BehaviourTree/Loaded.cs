using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JojoBehaviourTree
{
    class Loaded : AgentNode
    {

        public Loaded(BehaviourTreeAgent p_agent)
        {
            agent = p_agent;
        }

        public override bool execute()
        {
            if (agent.canShoot())
                return true;

            return false;
        }
    }
}
