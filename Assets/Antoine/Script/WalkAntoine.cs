using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WalkAntoine : StateAntoine
{
    public override void Initialise()
    {
        currentType = type.WALK;
        transitions = new List<TransitionAntoine>();
    }

    public override void Execute(GameObject pl)
    {
       // pl.GetComponent<AgentAntoine>().Chase();
    }
}
