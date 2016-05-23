using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PierreStateMachine : PierreState
{
    public PierreState currentState;

    //Pathfinding myPathfinding;

    Vector3 currentTarget;
    
    List<Vector3> road;

    NavMeshAgent nav;

    public enum Strat
    {
        Offensive,
        Defensive,
        IDontKnow
    }
    
    public override void Check()
    {
        base.Check();

        currentState.Check();
    }

    public override void Move(NewPierreAgent agent, NavMeshAgent nav) 
    {
        currentState.Move(agent, nav);
    }

    public override void Fire()
    {
        currentState.Fire();
    }

    public override Vector3 UpdateTarget(NewPierreAgent agent, Vector3 myTarget, List<GameObject> targets)
    {
        return currentState.UpdateTarget(agent, myTarget,targets);
    }

    public override Vector3 UpdateTargetMove(NewPierreAgent agent, Vector3 myTargetMove, List<GameObject> targets)
    {
        return currentState.UpdateTargetMove(agent, myTargetMove, targets);
    }
}
