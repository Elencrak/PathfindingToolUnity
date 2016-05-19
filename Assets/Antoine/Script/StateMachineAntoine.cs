using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class StateMachineAntoine : StateAntoine
{
    public List<StateAntoine> states;
    public List<StateAntoine.type> initStates;
    public StateAntoine currentState;

    public GameObject player;

    public StateMachineAntoine(GameObject pl)
    {
        player = pl;
        states = new List<StateAntoine>();
        InitStateMachine();
        InitTransitions();
        currentState = states[0];
    }

    public StateAntoine GetCurrentState()
    {
        return currentState;
    }

    public override void Execute(GameObject pl)
    {
    
    }

    public override void Initialise()
    {
        currentType = type.PARENT;
    }

    public void Update()
    {
        StateAntoine s = currentState.Step();
        if (s != null)
        {
            currentState = s;
        }
        else
        {
            currentState.Execute(player);
        }
    }

    public void InitStateMachine()
    {
        states.Add(new IdleAntoine());
        states.Add(new WalkAntoine());
        states.Add(new PatrolAntoine());

        foreach (StateAntoine s in states)
        {
            s.Initialise();
        }
    }
    
    public void InitTransitions()
    {
        TransitionAntoine idleToChase = new TransitionAntoine(player.GetComponent<AgentAntoine>().MustChase, states[1], 0);
        TransitionAntoine idleToPatrol = new TransitionAntoine(player.GetComponent<AgentAntoine>().NoTarget, states[2], 1);
        states[0].AddTransition(idleToChase);
        states[0].AddTransition(idleToPatrol);
        TransitionAntoine chaseToIdle = new TransitionAntoine(player.GetComponent<AgentAntoine>().HaveShoot, states[0], 6);
        TransitionAntoine chaseToPatrol = new TransitionAntoine(player.GetComponent<AgentAntoine>().NoTarget, states[2], 1);
        states[1].AddTransition(chaseToIdle);
        states[1].AddTransition(chaseToPatrol);
        TransitionAntoine patrolToIdle = new TransitionAntoine(player.GetComponent<AgentAntoine>().HaveShoot, states[0], 6);
        TransitionAntoine patrolToChase = new TransitionAntoine(player.GetComponent<AgentAntoine>().MustChase, states[1], 0);
        states[2].AddTransition(patrolToChase);
        states[2].AddTransition(patrolToIdle);
    }
}
