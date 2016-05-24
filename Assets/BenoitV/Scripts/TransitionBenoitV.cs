using UnityEngine;
using System.Collections;

public class TransitionBenoitV  {

    public delegate bool check();
    check _myDelegate;
    

    public StateBenoitV _nextState;

    public TransitionBenoitV(check parDelegate, StateBenoitV parState)
    {
        _myDelegate += parDelegate;
        _nextState = parState;
    }

    public bool Check()
    {
        return _myDelegate();
    }
}
