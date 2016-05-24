using UnityEngine;
using System.Collections;
using System;

namespace BenjaminBehaviorTree
{

    public class NodeWithAgent : Node {
        protected AgentLefevreBT _agent;

        public NodeWithAgent(AgentLefevreBT agent)
        {
            _agent = agent;
        }

        public override bool Execute()
        {
            throw new NotImplementedException();
        }
    }
}
