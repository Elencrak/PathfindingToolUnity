using UnityEngine;
using System.Collections;
using System;

namespace BenjaminBehaviorTree
{

    public class ShootTask : NodeWithAgent
    {

        public ShootTask(AgentLefevreBT agent):base(agent){}

        public override bool Execute()
        {
            return _agent.Fire();
        }
    }
}
