using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class StateAntoine /*: MonoBehaviour*/
{
    public enum type
    {
        PARENT,
        IDLE,
        WALK,
        PATROL,
    }

    public type currentType;
    public List<TransitionAntoine> transitions;

    public StateAntoine Step()
    {
        if (transitions.Count > 0)
        {
            foreach (TransitionAntoine t in transitions)
            {
                if (t.Check() != null)
                {
                    return t.GetNextState();
                }
            }
        }
        return null;
    }

    public bool End()
    {
        /*foreach(TransitionAntoine t in transitions)
        {
            if(t.Check())
            {
                return t.GetNextState();
            }
        }*/
        return false;
    }

    public abstract void Execute(GameObject pl);
    public abstract void Initialise();

    public void AddTransition(TransitionAntoine aTransition)
    {
        transitions.Add(aTransition);
    }

    public void PrintState()
    {
        switch(currentType)
        {
            case type.IDLE:
                Debug.Log("state : IDLE");
                break;

            case type.WALK:
                Debug.Log("state : WALK");
                break;

            case type.PATROL:
                Debug.Log("state : PATROL");
                break;

            case type.PARENT:
                Debug.Log("state : PARENT");
                break;

            default:
                Debug.Log("state : default");
                break;
        }
    }

}
