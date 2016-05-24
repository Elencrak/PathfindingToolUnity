using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate bool Check();

public class TransitionAntoine /*: MonoBehaviour*/
{
    public StateAntoine nextState;
    public Check theDelegate;

    public int theIndex;
    public bool theState = false;

    public TransitionAntoine(Check adelegate, StateAntoine theNextState, int ind)
    {
        nextState = theNextState;
        theDelegate += adelegate;
        theIndex = ind;
    }

    static public bool CheckCoolDown()
    {
        return false;
    }

    public StateAntoine GetNextState()
    {
        return nextState;
    }

    public StateAntoine Check()
    {
        if (theDelegate() == true)
        {
            return nextState;
        }
        return null;
    }

}
