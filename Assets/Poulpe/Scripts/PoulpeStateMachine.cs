using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoulpeStateMachine : PoulpeState
{
    List<PoulpeState> states;
    PoulpeState currentState;

    public PoulpeStateMachine(List<PoulpeState> States, PoulpeState CurrentState)
    {
        states = States;
        currentState = CurrentState;
    }
	
	// Update is called once per frame
	void Update ()
    {
	}
}
