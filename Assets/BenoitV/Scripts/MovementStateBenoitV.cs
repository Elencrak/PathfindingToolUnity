using UnityEngine;
using System.Collections;
using System;

public class MovementStateBenoitV : StateBenoitV
{
    public override void Execute(GameObject parAgent)
    {
        parAgent.GetComponent<AgentFunctions>().Move();
        //Debug.Log(parAgent + "Move");
    }

}
