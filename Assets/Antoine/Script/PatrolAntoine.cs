using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatrolAntoine : StateAntoine
{
    public override void Initialise()
    {
        currentType = type.PATROL;
        transitions = new List<TransitionAntoine>();
    }

    public override void Execute(GameObject pl)
    {
        pl.GetComponent<AgentAntoine>().Patrol();
    }
}
