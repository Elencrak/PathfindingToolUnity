using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class StateJordan {

    protected GameObject currentTarget;
    protected Transform transform;
    protected List<TransitionJordan> listTransition;

    public StateJordan()
    {
    }

    public void setTransform(Transform trans)
    {
        if (trans != null)
            transform = trans;
    }

    public void setTransition(List<TransitionJordan> list)
    {
        if (list != null)
            listTransition = list;
    }

    public void setTarget(GameObject target)
    {
        if (target != null)
            currentTarget = target;
    }

    public GameObject getTarget()
    {
        return currentTarget;
    }

    public abstract void step();
    public abstract StateJordan check();
}
