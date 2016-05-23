using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace JojoBehaviourTree
{
    public class AgentNode : Node
    {
        protected BehaviourTreeAgent agent;

        public override bool execute()
        {
            return false;
        }
    }
}
