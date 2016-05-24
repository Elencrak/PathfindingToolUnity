using UnityEngine;
using System.Collections;
using System;

namespace BenjaminBehaviorTree
{

    public class GetRandomNavigationPointTask : NodeWithAgent
    {
        public GetRandomNavigationPointTask(AgentLefevreBT agent) : base(agent)
        {
        }
    }
}
