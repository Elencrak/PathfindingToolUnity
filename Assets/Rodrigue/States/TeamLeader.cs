using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Rodrigue;
public class TeamLeader : MonoBehaviour {

    public List<RodrigueAgent> _agentList;
    StateMachine myStateMachine = new StateMachine();
    StateMachine StateMachineRegroup = new StateMachine();

    State idleState;
    State patrolState;
    State searchState;
    State groupState;

    Transition idleTransition;
    Transition patrolTransition;
    Transition searchTransition;
    Transition searchToPatrolTransition;

    Transition groupTransition;
    Transition groupTransition2;

    public bool Death;
    public bool Patrol;
    public bool Search;
    // Use this for initialization
    void Start () {
        _agentList = new List<RodrigueAgent>();
        foreach(Transform child in transform)
        {
            _agentList.Add(child.gameObject.GetComponent<RodrigueAgent>());
        }

        //idleState = new IdleState(this.gameObject.GetComponent<TeamLeader>());
        patrolState = new PatrolState(this.gameObject.GetComponent<TeamLeader>());
        searchState = new SearchState(this.gameObject.GetComponent<TeamLeader>());
        groupState = new GroupState(this.gameObject.GetComponent<TeamLeader>());

        Patrol = true;

        myStateMachine.currentState = patrolState;
        StateMachineRegroup.currentState = myStateMachine;

        //idleTransition = new Transition(CheckDeath, groupState);
        //patrolState._listTransition.Add(groupTransition2);
        //searchState._listTransition.Add(groupTransition2);

        //patrolTransition = new Transition(CheckPatrol, patrolState);
        //idleState._listTransition.Add(patrolTransition);

        //searchTransition = new Transition(PatrolF, searchState);
        //patrolState._listTransition.Add(searchTransition);

        //groupTransition = new Transition(RegroupF, patrolState); //changer CheckDeath;
        //groupTransition2 = new Transition(CheckDeathF, groupState); //changer CheckDeath;
        //groupState._listTransition.Add(groupTransition);

        searchTransition = new Transition(PatrolF, searchState);
        patrolState._listTransition.Add(searchTransition);

        groupTransition = new Transition(RegroupF, myStateMachine);
        groupTransition2 = new Transition(CheckDeathF, groupState);

        myStateMachine._listTransition.Add(groupTransition2);

        groupState._listTransition.Add(groupTransition);

        searchToPatrolTransition = new Transition(CheckKillF, patrolState);
        searchState._listTransition.Add(searchToPatrolTransition);

    }
	
	// Update is called once per frame
	void Update () {
        StateMachineRegroup.Execute();
    }

   

    public bool PatrolF()
    {
        foreach(RodrigueAgent parAgent in _agentList)
        {
            parAgent.Patrol();
            if(parAgent.timeSinLastShot > 10)
            {
                
                return true;
            }
        }
        return false;
    }

    public void SearchF()
    {
        foreach (RodrigueAgent parAgent in _agentList)
        {
            parAgent.SearchAndDestroy();
        }
    }

    public void IdleF()
    {
        foreach (RodrigueAgent parAgent in _agentList)
        {
            parAgent.Idle();
        }
    }


    public bool RegroupF()
    {
        foreach(RodrigueAgent parAgent in _agentList)
        {
            parAgent.Regroup();
            if(parAgent.GetComponentInChildren<DodgeRodrigue>().listOfFriends.Count > 2)
            {
                return true;
            }
        }
        return false;
    }

    public bool CheckDeathF()
    {
        if (Death)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public IEnumerator isDead()
    {
        Death = true;
        yield return new WaitForSeconds(1);
        Death = false;
    }

    public bool CheckKillF()
    {
        int nb = 0;
        foreach (RodrigueAgent parAgent in _agentList)
        {
            nb += parAgent.nbOfDeath;
            
        }
        if (nb > 5)
        {
            foreach (RodrigueAgent parAgent in _agentList)
            {
                parAgent.nbOfDeath = 0;
                parAgent.navMeshAgent.SetDestination(parAgent.interestPoints[0].transform.position);
            } 
            return true;
        }
        return false;
    }
}
