using UnityEngine;
using System.Collections;
using System;

namespace BenjaminBehaviorTree
{

    public class CanShootCondition : NodeWithAgent
    {
        public CanShootCondition(AgentLefevreBT agent):
        base(agent)
        {}

        public override bool Execute()
        {
            if(Time.time> _agent.lastFire+ _agent.fireRate)
            {
                RaycastHit hit;
                Vector3 direction = (_agent.target.transform.position - _agent.transform.position).normalized;
                if (Physics.Raycast(_agent.transform.position+direction,direction,out hit))
                {
                    if(hit.transform.gameObject == _agent.target)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
