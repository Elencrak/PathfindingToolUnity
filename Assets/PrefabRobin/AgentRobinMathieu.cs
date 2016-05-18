﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace StateMachineRobin
{
    public class State
    {
        StateMachine _mother;

        List<Transition> _transitions;

        public State()
        {
            _transitions = new List<Transition>();
        }

        public State(StateMachine stateMachine)
        {
            _mother = stateMachine;
            _transitions = new List<Transition>();
        }

        public virtual void Step()
        {
            State chckd = null;
            for (int i = 0; i < _transitions.Count; i++)
            {
                chckd = _transitions[i].Check();
                if (chckd != null)
                {
                    Finish();
                    chckd.Init();
                    _mother._currentState = chckd;
                    chckd.Step();
                    break;
                }
            }
            if (chckd == null)
            {
                
            }
        }

        public virtual void Init()
        {
         
        }

        public virtual void Finish()
        {

        }
    }

    public class StateMachine : State
    {
        public State _currentState;
        
        public override void Step()
        {
            base.Step();
            _currentState.Step();
        }
    }

    public class Transition
    {
        State _toState;

        public Transition()
        {
            _toState = null;
        }

        public Transition(State state)
        {
            _toState = state;
        }

        public virtual State Check()
        {
            return null;
        }
    }
}

public class AgentRobinMathieu : Entity
{

    [Header("IA")]

    public Vector3 startPoint;
    public GameObject nearestTarget;
    public BoxCollider nearTargetCollider;
    public List<GameObject> targets;
    public NavMeshAgent agent;
    public AgentRobin agent2;
    bool hasWin = false;

    [Header("Values")]

    float timeFreezeEnemy = 1.0f;
    public bool isShooting = true;
    public static string playerID = "Squad Robin";
    TeamNumber parentNumber;

    protected override void Start()
    {

        parentNumber = transform.parent.parent.GetComponent<TeamNumber>();
        parentNumber.teamName = playerID;
        startPoint = transform.position;
        targets = new List<GameObject>(GameObject.FindGameObjectsWithTag("Target"));
        agent = GetComponent<NavMeshAgent>();
        agent2 = GetComponent<AgentRobin>();

        for (int i = targets.Count - 1; i >= 0; --i)
        {
            if (targets[i].name == "Robin")
            {
                targets.Remove(targets[i]);
            }
        }
        //InvokeRepeating("Gagne", 0.0f, 1.5f);
        base.Start();
    }

    void Gagne()
    {
        bool isWin = true;
        if (isWin && !hasWin)
        {
            hasWin = true;
            Debug.Log("J'ai gagné : Robin MATHIEU");
        }
    }

    protected virtual void UpdateTarget()
    {

        GameObject target = null;

        for (int i = targets.Count - 1; i >= 0; --i)
        {
            if (target == null || (target && Vector3.Distance(target.transform.position, transform.position) > Vector3.Distance(targets[i].transform.position, transform.position)))
            {
                target = targets[i];
            }
        }
        if (target)
        {
            nearestTarget = target;
            nearTargetCollider = target.GetComponent<BoxCollider>();
        }
        else
        {
            nearestTarget = null;
            nearTargetCollider = null;
        }
    }

    protected virtual void UpdateRoad()
    {
        if (targets.Count > 0)
        {
            if (agent.enabled)
            {
                agent.SetDestination(targets[Random.Range(0, targets.Count - 1)].transform.position);
            }
            if (agent2.enabled)
            {
                agent2.target = targets[Random.Range(0, targets.Count - 1)];
            }
        }
    }
}
