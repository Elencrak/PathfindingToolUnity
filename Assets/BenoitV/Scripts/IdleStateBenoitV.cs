using UnityEngine;
using System.Collections;
using System;

public class IdleStateBenoitV : StateBenoitV
{
    
    public override void Execute(GameObject parAgent)
    {
        parAgent.GetComponent<AgentFunctions>().StandBy();
        Debug.Log(parAgent + "Idle");
    }

}
