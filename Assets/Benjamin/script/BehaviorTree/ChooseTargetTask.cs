using UnityEngine;
using System.Collections;

namespace BenjaminBehaviorTree
{

    public class ChooseTargetTask : NodeWithAgent {

        public ChooseTargetTask(AgentLefevreBT agent) : base(agent) { }

        public override bool Execute()
        {
            GameObject tmp = GetClosestTarget();
            if(tmp != null)
            {
                _agent.target = tmp;
                return true;
            }
            return false;
        }

        GameObject GetClosestTarget()
        {
            float dist = Mathf.Infinity;
            GameObject target = null;
            _agent.RefreshTargets();
            foreach (GameObject obj in _agent.targets)
            {
                RaycastHit hit;
                Vector3 direction = (obj.transform.position - _agent.transform.position).normalized;
                if (Physics.Raycast(_agent.transform.position + direction, direction, out hit))
                {
                    if (hit.transform.gameObject == obj)
                    {
                        float tmp = Vector3.Distance(_agent.transform.position, obj.transform.position);
                        if (tmp < dist)
                        {
                            dist = tmp;
                            target = obj;
                        }
                    }
                }
            }
            return target;
        }

    }
}
