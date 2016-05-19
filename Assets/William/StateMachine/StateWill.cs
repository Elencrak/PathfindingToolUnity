using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class StateWill  {
    public int idAgent;
    public List<TransitionWill> transitions;
    public StateWill(int id)
    {
        idAgent = id;
        transitions = new List<TransitionWill>();
    }

    public StateWill(int id , List<TransitionWill> pTransitions)
    {
        transitions = pTransitions;
    }
    public abstract StateWill execute();

    protected virtual StateWill checkTransition()
    {
        StateWill next = null;
        foreach (TransitionWill trans in transitions)
        {
            next = trans.check();
            if (next != null)
            {
                return next;
            }
        }
        return null;
    }
    
}

