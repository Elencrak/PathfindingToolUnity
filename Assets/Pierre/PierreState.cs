using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PierreState{

    protected List<PierreTransition> transitions = new List<PierreTransition>();

    [HideInInspector]public PierreStateMachine stateMachine;

    public PierreState()
    {
        
    }

    public PierreState(PierreStateMachine psm)
    {
        stateMachine = psm;
    }

    public virtual void Check()
    {
        foreach(PierreTransition transition in transitions)
        {
            transition.Check();
        }
    }

    public virtual void Move(NewPierreAgent agent, NavMeshAgent nav)
    {

    }

    public virtual void Fire()
    {

    }

    public virtual Vector3 UpdateTarget(NewPierreAgent agent, Vector3 myTarget, List<GameObject> targets)
    {
        return new Vector3();
    }

    public virtual Vector3 UpdateTargetMove(NewPierreAgent agent, Vector3 myTargetMove, List<GameObject> targets)
    {
        return new Vector3();
    }

    public virtual void StateStart()
    {
        stateMachine.currentState = this;
    }

    public virtual void StateEnd()
    {

    }

}
