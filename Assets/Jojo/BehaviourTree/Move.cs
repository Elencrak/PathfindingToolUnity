using UnityEngine;
using System.Collections;
using System;

namespace JojoBehaviourTree
{
    public class Move : AgentNode
    {
        public Move(BehaviourTreeAgent p_agent)
        {
            agent = p_agent;
        }

        public override bool execute()
        {
            Debug.Log("MoveTask");
            bool temp = Input.GetKeyDown(KeyCode.Z);
            return temp;
        }
    }
}