using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class StateBenoitV {
    

    public List<TransitionBenoitV> _listOfTransitions = new List<TransitionBenoitV>();

    public abstract void Execute(GameObject parAgent);

    public virtual StateBenoitV step(GameObject parAgent)
    {
        foreach(TransitionBenoitV _tempTransition in _listOfTransitions)
        {
            if(_tempTransition.Check())
            {
                return _tempTransition._nextState;
            }
        }
        Execute(parAgent);
        return this;
    }
}
