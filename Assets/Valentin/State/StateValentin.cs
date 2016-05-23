using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class StateValentin {

    protected List<TransitionValentin> mytransition;

    public StateValentin()
    {
    }

    public virtual StateValentin Step()
    {
        StateValentin test = checkTransition();
        if (test == null)
        {
            execute();
        }
        return test;
    }

    protected abstract void execute();

    protected StateValentin checkTransition()
    {

        foreach(TransitionValentin transi in mytransition)
        {
            if(transi.check()!=null)
            {
                return transi.check();
            }
        }

        return null;
    }

    public void addTransition(List<TransitionValentin> trans)
    {
        mytransition = trans;
    }



}
