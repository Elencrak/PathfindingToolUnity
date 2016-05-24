using UnityEngine;
using System.Collections;

public class MyAgentChefBenoitV : MonoBehaviour {

    StateMachineBenoitV myStateMachine = new StateMachineBenoitV();

    IdleStateBenoitV myIdleState;
    PatrolStateBenoitV myPatrolState;
    MovementStateBenoitV myMoveState;

    TransitionBenoitV _idleToMove;
    TransitionBenoitV _moveToIdleDeath;
    TransitionBenoitV _moveToIdleStop;
    TransitionBenoitV _moveToPatrol;
    TransitionBenoitV _patrolToIdle;

    public bool itsSecure;
    public bool patrol;

    NavMeshAgent _agent;
    
    void Start()
    {
        myIdleState = new IdleStateBenoitV();
        myPatrolState = new PatrolStateBenoitV();
        myMoveState = new MovementStateBenoitV();

        myStateMachine._currentState = myIdleState;
        
        _idleToMove = new TransitionBenoitV(CanMove, myMoveState);
        myIdleState._listOfTransitions.Add(_idleToMove);

        _moveToIdleStop = new TransitionBenoitV(Stop, myIdleState);
        _moveToIdleDeath = new TransitionBenoitV(Death, myIdleState);
        myMoveState._listOfTransitions.Add(_moveToIdleStop);
        myMoveState._listOfTransitions.Add(_moveToIdleDeath);

        _moveToPatrol = new TransitionBenoitV(CanPatrol, myPatrolState);
        myMoveState._listOfTransitions.Add(_moveToPatrol);

        _patrolToIdle = new TransitionBenoitV(Death, myIdleState);
        myPatrolState._listOfTransitions.Add(_patrolToIdle);

        GetComponent<AgentFunctions>().InvokeRepeating("FindTarget", 0.1f, 0.1f);
    }

    void Update()
    {
        myStateMachine.Execute(this.gameObject);
    }

    bool CanMove()
    {
        return itsSecure && !GetComponent<AgentFunctions>().death;
    }

    bool Stop()
    {
        return !itsSecure;
    }

    bool Death()
    {
        return GetComponent<AgentFunctions>().death;
    }

    bool CanPatrol()
    {
        return patrol && !GetComponent<AgentFunctions>().death;
    }

    
}
