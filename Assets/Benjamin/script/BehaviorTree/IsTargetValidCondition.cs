using UnityEngine;
using System.Collections;

namespace BenjaminBehaviorTree
{

    public class IsTargetValidCondition : NodeWithAgent
    {

        public IsTargetValidCondition(AgentLefevreBT agent) : base(agent) { }

        public override bool Execute()
        {
            if(_agent.target == null)
            {
                _agent.RefreshTargets();
                _agent.target = _agent.targets[Random.Range(0, _agent.targets.Count)];
                if (_agent.target == null)
                    return false;
                else
                    return true;
            }
            return true;
        }
    }
}
