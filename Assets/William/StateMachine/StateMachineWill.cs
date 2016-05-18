using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class StateMachineWill : StateWill {

    List<StateWill> states;
    StateWill currentState;

	void Start () {
	
	}
	

	
	void Update () {
	
	}

    public override void execute()
    {
        check();
        currentState.execute();
    }

    public void changeState(StateWill newState)
    {
        currentState = newState;
    }

    protected override void check()
    {
        foreach (TransitionWill trans in transition)
        {

        }
    }

}
