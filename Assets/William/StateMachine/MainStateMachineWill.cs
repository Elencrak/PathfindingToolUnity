using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainStateMachineWill : StateWill {

    public List<StateWill> states;
    StateWill currentState;

    public MainStateMachineWill(int id, List<StateWill> listState, int indexFirstState =0):base(id)
    {
        transitions = new List<TransitionWill>();
        states = listState;
        currentState = states[indexFirstState];
    }

    public MainStateMachineWill(int id, StateWill state):base(id)
    {
        states = new List<StateWill>();
        transitions = new List<TransitionWill>();
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
    

}
