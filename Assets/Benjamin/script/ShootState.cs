using UnityEngine;
using System.Collections;
using System;

namespace benjamin
{
    public class ShootState : AbstractState
    {
        AgentLefevre agent;

        
        public override void Init()
        {
            agent = controller.GetComponent<AgentLefevre>();
            Debug.Log(agent.gameObject.name + " Init ShootState");
            AddTransition(new LostTarget());

            agent.CancelInvoke();
        }

        // Update is called once per frame
        public override void StateUpdate()
        {
            if(Time.time > agent.lastFire+1f)
            {
                agent.Fire();
            }

        }

    }
}
