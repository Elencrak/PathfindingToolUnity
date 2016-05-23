using UnityEngine;
using System.Collections;
using System;

namespace JojoBehaviourTree
{
    public class SeeOpponent : AgentNode
    {
        
        public SeeOpponent(BehaviourTreeAgent p_agent)
        {
            agent = p_agent;
        }

        public override bool execute()
        {
            Debug.Log("SeeOpponentTask");
            bool temp = Input.GetKeyDown(KeyCode.A);
            return temp;
        }
    }
}