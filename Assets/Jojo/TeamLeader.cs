using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace JojoKiller
{
    public class TeamLeader : MonoBehaviour
    {

        //Target
        [Header("Target")]
        private List<Transform> targets = new List<Transform>();

        [Header("Internal")]
        bool doOnce = true;

        [Header("State Machine")]
        public StateMachine stateTactic;
        public StateMachine statePatrol;
        public List<Member> agents = new List<Member>();

        public bool doTransitionToReform;
        public bool doTransitionToRegroup;
        public bool doTransitionToPatrol;

        [Header("Member")]
        public int teamCount;

        // Use this for initialization
        void Start()
        {          
            
            // initialize the agent
            for(int i = 0; i < transform.childCount; i++)
            {
                agents.Add(transform.GetChild(i).GetComponent<Member>());
            }

            // State machine for member
            statePatrol = new StateMachine();

            Idle monIdle = new Idle(myAgent);
            Search monWalk = new Search(myAgent);
            Chase monFire = new Chase(myAgent);

            //from Idle to...
            Transition transitionIdle = new Transition(myAgent.changeToWalk, monWalk);
            Transition transitionWalk = new Transition(myAgent.canShoot, monFire);
            Transition transitionFire = new Transition(myAgent.changeToIdle, monIdle);

            monIdle.addTransition(transitionIdle);
            monWalk.addTransition(transitionWalk);
            monFire.addTransition(transitionFire);

            statePatrol.currentState = monIdle;

            // State machine for team
            stateTactic = new StateMachine();

            Regroup regroup = new Regroup(this);
            Reform reform = new Reform(this);
            StateMachineWrapper stateWrapper = new StateMachineWrapper(statePatrol);

            Transition transitionRegroup = new Transition(checkNeedToReform, reform);
            Transition transitionReform = new Transition(() => { return doTransitionToPatrol; }, stateWrapper);
            Transition transitionPatrol = new Transition(() => { return doTransitionToRegroup; }, regroup);

            regroup.addTransition(transitionRegroup);
            reform.addTransition(transitionReform);
            stateWrapper.addTransition(transitionPatrol);

            stateTactic.currentState = regroup;
        }

        public bool checkNeedToReform()
        {


            return false;
        }

        public bool patrol()
        {
            if (teamCount < 2)
                Debug.Log(teamCount < 2);
            return teamCount < 2;
        }

        public void reformAction()
        {
            Debug.Log("reformAction");
        }

        public void regroupAction()
        {
            Debug.Log("regroupAction");
        }

        // Update is called once per frame
        void Update()
        {
            //Récupération des target
            if (doOnce)
            {
                doOnce = false;
                GameObject[] tempArray;
                tempArray = GameObject.FindGameObjectsWithTag("Target");
                foreach (GameObject g in tempArray)
                {
                    if (g.GetInstanceID() != gameObject.GetInstanceID())
                    {
                        targets.Add(g.transform);
                    }
                }
            }

            stateTactic.execution();

            doTransitionToPatrol = false;
            doTransitionToReform = false;
            doTransitionToRegroup = false;
        }
    }
}