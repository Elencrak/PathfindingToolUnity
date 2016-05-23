using UnityEngine;
using System.Collections;

namespace benjamin
{
    public class MoveToState : AbstractState {
        AgentLefevre agent;
        

        public override void Init()
        {
            agent = controller.GetComponent<AgentLefevre>();
            Debug.Log(agent.gameObject.name + " Init ShootState");
            AddTransition(new SeeTarget());

            agent.RefreshTargets();
            agent.target = agent.targets[Random.Range(0, agent.targets.Count)];
            agent.CancelInvoke();
            agent.InvokeRepeating("UpdateRoad", 0f, 0.5f);
        }

        // Update is called once per frame
        public override void StateUpdate ()  {
            
            if (agent.target == null || agent.road == null)
                return;
            if (agent.road.Count > 0)
            {
                agent.currentTarget = agent.road[0];
                if (Vector3.Distance(agent.transform.position, agent.currentTarget) < agent.closeEnoughRange)
                {
                    agent.road.RemoveAt(0);
                    if (agent.road.Count == 0)
                        return;
                    agent.currentTarget = agent.road[0];
                }
                else
                {
                    agent.transform.position = Vector3.MoveTowards(agent.transform.position, agent.currentTarget+Vector3.up, agent.speed * Time.deltaTime);
                }
            }
            else
            {
                agent.transform.position = Vector3.MoveTowards(agent.transform.position, agent.target.transform.position, agent.speed * Time.deltaTime);
            }
        }
        
    }
}
