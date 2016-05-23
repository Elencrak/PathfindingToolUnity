using UnityEngine;
using System.Collections;

public class TransitionValentin {

    public delegate bool MyDelegate();
    MyDelegate myDelegate;
    StateValentin state;

    public TransitionValentin(MyDelegate del, StateValentin nextState)
    {
        myDelegate += del;
        state = nextState;
    }

    public virtual StateValentin check()
    {
        if(myDelegate())
        {
            return state;
        }
        else
        {
            return null;
        }
    }
}
