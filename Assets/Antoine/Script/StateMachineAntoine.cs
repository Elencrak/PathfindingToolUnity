using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class StateMachineAntoine : StateAntoine
{
    public List<StateAntoine> states;
    public List<StateAntoine.type> initStates;
    public StateAntoine currentState;

    void Start()
    {
        InitStateMachine();
        InitTransitions();
        currentState = states[0];
    }

    public override StateAntoine Execute()
    {
        return null;
    }

    public override void Initialise()
    {
        currentType = type.PARENT;
    }

    void Update()
    {
        StateAntoine s = currentState.Execute();
        if (s != null)
        {
            currentState = s;
        }
    }

    public void InitStateMachine()
    {
        states.Add(new IdleAntoine());
        states.Add(new WalkAntoine());

        foreach(StateAntoine s in states)
        {
            s.Initialise();
        }
    }
    
    public void InitTransitions()
    {
        TransitionAntoine t1 = new TransitionAntoine();
        t1.Init(states[1]);
        states[0].AddTransition(t1);
    }
}
