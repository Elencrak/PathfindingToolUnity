using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MyAgentGuardBenoitV : MonoBehaviour {

    StateMachineBenoitV myStateMachine = new StateMachineBenoitV();
    StateMachineBenoitV mySecondStateMachine = new StateMachineBenoitV();

    IdleStateBenoitV myIdleState;
    PatrolStateBenoitV myPatrolState;
    MovementStateBenoitV myMoveState;
    CoverStateBenoitV myCoverState;
    GroupStateBenoitV myGroupState;

    TransitionBenoitV _idleToMove;
    TransitionBenoitV _moveToCover;
    TransitionBenoitV _coverToIdle;

    TransitionBenoitV _SM1toGroup;
    TransitionBenoitV _GrouptoSM1;
    TransitionBenoitV _moveToIdleDeath;

    public bool bossDeath;
    public bool cover;

    public List<Transform> _pointsOfInterest;
    public Transform _currentPointOfInterest;

    // Use this for initialization
    void Start () {
        myIdleState = new IdleStateBenoitV();
        myPatrolState = new PatrolStateBenoitV();
        myMoveState = new MovementStateBenoitV();
        myCoverState = new CoverStateBenoitV();
        myGroupState = new GroupStateBenoitV();
        

        myStateMachine._currentState = myIdleState;
        mySecondStateMachine._currentState = myStateMachine;
        

        _currentPointOfInterest = _pointsOfInterest[0];
        GetComponent<AgentFunctions>()._target = _currentPointOfInterest;

        _moveToIdleDeath = new TransitionBenoitV(Death, myIdleState);
        myStateMachine._listOfTransitions.Add(_moveToIdleDeath);

        _SM1toGroup = new TransitionBenoitV(Group, myGroupState);
        myStateMachine._listOfTransitions.Add(_SM1toGroup);
        
        
        _GrouptoSM1 = new TransitionBenoitV(Degroup, myStateMachine);
        myGroupState._listOfTransitions.Add(_GrouptoSM1);

        _idleToMove = new TransitionBenoitV(CanMove, myMoveState);
        myIdleState._listOfTransitions.Add(_idleToMove);

        _moveToCover = new TransitionBenoitV(Cover, myCoverState);
        myMoveState._listOfTransitions.Add(_moveToCover);

        _coverToIdle = new TransitionBenoitV(Stop,myIdleState);
        myCoverState._listOfTransitions.Add(_coverToIdle);

        GetComponent<AgentFunctions>().InvokeRepeating("FindTarget", 0.1f, 0.1f);
    }
	
	// Update is called once per frame
	void Update () {
        mySecondStateMachine.Execute(this.gameObject);
	}

    bool Group()
    {
        return bossDeath;
    }

    bool Degroup()
    {
        return !bossDeath;
    }

    bool CanMove()
    {
        return GetComponent<AgentFunctions>().DistanceWithOtherAgent(GetComponent<AgentFunctions>()._boss, this.transform) < 3.0f && !GetComponent<AgentFunctions>().death;
    }

    bool Death()
    {
        return GetComponent<AgentFunctions>().death;
    }

    bool Cover()
    {
        return Vector3.Distance(GetComponent<AgentFunctions>()._target.position, transform.position) < 3.0f && !cover;
    }

    bool Stop()
    {
        return cover;
    }

    public void CoverBoss()
    {
        StartCoroutine(Alert());
    }


    IEnumerator Alert()
    {
        
        yield return new WaitForSeconds(2.0f);
        GetComponent<AgentFunctions>()._boss.GetComponent<MyAgentChefBenoitV>().itsSecure = true;
        cover = true; ;
    }
}
