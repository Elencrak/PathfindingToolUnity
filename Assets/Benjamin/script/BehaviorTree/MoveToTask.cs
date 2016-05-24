using UnityEngine;
using System.Collections;

namespace BenjaminBehaviorTree
{

    public class MoveToTask : NodeWithAgent
    {

        public MoveToTask(AgentLefevreBT agent) : base(agent) { }

        public override bool Execute()
        {
            if (_agent.target == null || _agent.road == null)
                return false;
            if (_agent.road.Count > 0)
            {
                _agent.currentTarget = _agent.road[0];
                if (Vector3.Distance(_agent.transform.position, _agent.currentTarget) < _agent.closeEnoughRange)
                {
                    _agent.road.RemoveAt(0);
                    if (_agent.road.Count == 0)
                        return false;
                    _agent.currentTarget = _agent.road[0];
                }
                else
                {
                    _agent.agent.Resume();
                    _agent.agent.SetDestination(_agent.currentTarget);
                }
            }
            else 
            {
                if (Vector3.Distance(_agent.transform.position, _agent.currentTarget) < 2f)
                {
                    _agent.agent.Resume();
                    _agent.agent.SetDestination(_agent.currentTarget);
                }
                else
                    _agent.agent.Stop();
            }
            return true;
        }

        
    }
}
