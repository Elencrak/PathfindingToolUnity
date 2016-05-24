using UnityEngine;
using System.Collections;
using System;

namespace JojoBehaviourTree
{
    public class Shoot : AgentNode
    {

        public Shoot(BehaviourTreeAgent p_agent)
        {
            agent = p_agent;
        }

        public override bool execute()
        {
            Debug.Log("ShootTask");
            bool temp = Input.GetKeyDown(KeyCode.E);
            return temp;
        }
    }
}
