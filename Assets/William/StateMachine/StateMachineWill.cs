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
        checkTransition();
        currentState.execute();
    }

    public void changeState(StateWill newState)
    {
        currentState = newState;
    }

    protected override void checkTransition()
    {
        StateWill next = null;
        foreach (TransitionWill trans in transition)
        {
            next = trans.check();
            if (next!=null)
            {
                changeState(next);
                return;
            }
        }
    }

}
