using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateMachineJordan : StateJordan {

    protected StateJordan currentState;
    protected List<StateJordan> listState;

    public StateMachineJordan(List<StateJordan> list, StateJordan initState)
    {
        currentState = initState;
        listState = list;
    }

    public override void step()
    {
        if (currentState == null)
            currentState = listState[0];
        
        if (currentState.getTarget() == null)
            currentState.setTarget(currentTarget);

        currentState.step();

        
        StateJordan temp = currentState.check();

        if (temp != null)
            currentState = temp;
    }

    public override StateJordan check()
    {
        if (listTransition != null)
        {
            foreach (TransitionJordan trans in listTransition)
            {
                StateJordan temp = trans.check();

                if (temp != null)
                    return temp;
            }
        }
        return null;
    }
}
