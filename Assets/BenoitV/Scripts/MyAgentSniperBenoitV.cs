using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MyAgentSniperBenoitV : MonoBehaviour {

    StateMachineBenoitV myStateMachine = new StateMachineBenoitV();
    
    MovementStateBenoitV myMoveState;
    GroupStateBenoitV myGroupState;
    
    TransitionBenoitV _moveToGroup;
    TransitionBenoitV _groupToCover;

    public bool death;
    public bool bossDeath;
    

    void Start () {
        myMoveState = new MovementStateBenoitV();
        myGroupState = new GroupStateBenoitV();

        myStateMachine._currentState = myMoveState;

        _moveToGroup = new TransitionBenoitV(Group, myGroupState);
        myMoveState._listOfTransitions.Add(_moveToGroup);

        _groupToCover = new TransitionBenoitV(Degroup, myMoveState);
        myGroupState._listOfTransitions.Add(_groupToCover);

        
        GetComponent<AgentFunctions>().InvokeRepeating("FindTarget", 0.1f, 0.1f);
    }
	

	void Update () {
        myStateMachine.Execute(this.gameObject);
    }

    bool Group()
    {
        return bossDeath;
    }

    bool Degroup()
    {
        return !bossDeath;
    }
}
