using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoulpeStateMachine : PoulpeState
{
    PoulpeState currentState;

    //List<PoulpeState> states;

    public PoulpeStateMachine(/*List<PoulpeState> States,*/ PoulpeState CurrentState)
    {
        //states = States;
        currentState = CurrentState;
    }

    public override void Step()
    {
        foreach (PoulpeTransition transition in transitions)
        {
            transition.Check();
        }
        currentState.Step();
    }

    public void SetCurrentState(PoulpeState state)
    {
        currentState = state;
    }
}
