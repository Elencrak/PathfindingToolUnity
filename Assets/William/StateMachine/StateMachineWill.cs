using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateMachineWill : StateWill {

    public List<StateWill> states;
    StateWill currentState;

    public StateMachineWill(List<StateWill> listState, int indexFirstState =0)
    {
        transition = new List<TransitionWill>();
        states = listState;
        currentState = states[indexFirstState];
    }

    public StateMachineWill(StateWill state)
    {
        currentState = state;
    }


    public override StateWill execute()
    {
        StateWill next = checkTransition();
        if (next!=null)return next;
        
        changeState(currentState.execute());
        
        

        return null;
    }

    public void changeState(StateWill newState)
    {
        if(newState!=null)
        currentState = newState;
    }

    protected override StateWill checkTransition()
    {
        StateWill next = null;
        foreach (TransitionWill trans in transition)
        {
            
            next = trans.check();
            if (next!=null)
            {
                changeState(next);
                return next;
            }
        }
        return null;
    }

}
