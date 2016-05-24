using UnityEngine;
using System.Collections;

public delegate bool check();

public class TransitionJordan {

    private StateJordan nextState;
    private check condition;

    public TransitionJordan(check newCond)
    {
        condition = newCond;
    }

    public StateJordan check()
    {
        if (condition())
            return nextState;
        return null;
    }

    public void setNextState(StateJordan state)
    {
        if (state != null)
            nextState = state;
    }
}
