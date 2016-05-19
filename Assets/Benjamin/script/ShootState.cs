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
            Debug.Log("InitShootState");
            agent = AgentLefevre.instance;
            AddTransition(new LostTarget());

            agent.CancelInvoke();
            agent.InvokeRepeating("Fire", 0f, 1f);
        }

        // Update is called once per frame
        public override void StateUpdate()
        {
            Debug.Log("currentState = ShootState");

        }

    }
}
