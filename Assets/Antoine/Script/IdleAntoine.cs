using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IdleAntoine : StateAntoine
{
    public override void Initialise()
    {
        currentType = type.IDLE;
        transitions = new List<TransitionAntoine>();
    }

    public override void Execute(GameObject pl)
    {
        //pl.GetComponent<AgentAntoine>().ChangeMovement(false);
    }
}
