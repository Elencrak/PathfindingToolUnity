using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class PoulpeState
{
    public List<PoulpeTransition> transitions;

    public void SetTransitions(List<PoulpeTransition> Transitions)
    {
        transitions = Transitions;
    }

    public abstract void Step();
}
