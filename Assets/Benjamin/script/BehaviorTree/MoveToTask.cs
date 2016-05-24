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
                    _agent.transform.position = Vector3.MoveTowards(_agent.transform.position, _agent.currentTarget + Vector3.up, _agent.speed * Time.deltaTime);
                }
            }
            else
            {
                _agent.transform.position = Vector3.MoveTowards(_agent.transform.position, _agent.target.transform.position, _agent.speed * Time.deltaTime);
            }
            return true;
        }

        
    }
}
