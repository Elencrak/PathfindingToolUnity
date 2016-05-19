using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TransitionAntoine : MonoBehaviour
{
    public StateAntoine nextState;

    public void Init(StateAntoine next)
    {
        nextState = next;
    }

    public StateAntoine GetNextState()
    {
        return nextState;
    }

    public bool Check()
    {
        Debug.Log("je suis checke !");
        return true;
    }

}
