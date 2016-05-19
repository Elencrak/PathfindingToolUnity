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
        currentState.Step();
    }

    public void SetCurrentState(PoulpeState state)
    {
        currentState = state;
    }
}
