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

        [Header("Navigation")]
        private NavMeshAgent currentNavMeshAgent;

        [Header("Internal")]
        private Vector3 startPosition;
        bool doOnce = true;
        bool touch;

        [Header("State Machine")]
        [SerializeField]
        public StateMachine stateTactic;
        public StateMachine statePatrol;
        public Member myAgent;

        public float regroupeTime;
        public float reformeTime;

        public float timerReform;
        public float timerRegroup;

        public bool doTransitionToReform;
        public bool doTransitionToRegroup;
        public bool doTransitionToPatrol;

        public int teamCount;

        // Use this for initialization
        void Start()
        {
            currentNavMeshAgent = GetComponent<NavMeshAgent>();           
            startPosition = transform.position;
            timerReform = reformeTime;
            timerRegroup = regroupeTime;

            // Decorator
            //(SSM) statePatrol = new SSM(new StateMachine(states, transitions1), transitions2)

            myAgent = transform.GetChild(0).GetComponent<Member>();

            statePatrol = new StateMachine();

            Idle monIdle = new Idle(myAgent);
            Walk monWalk = new Walk(myAgent);
            Fire monFire = new Fire(myAgent);

            //from Idle to...
            Transition transitionIdle = new Transition(myAgent.changeToWalk, monWalk);
            Transition transitionWalk = new Transition(myAgent.canShoot, monFire);
            Transition transitionFire = new Transition(myAgent.changeToIdle, monIdle);

            monIdle.addTransition(transitionIdle);
            monWalk.addTransition(transitionWalk);
            monFire.addTransition(transitionFire);

            statePatrol.currentState = monIdle;

            stateTactic = new StateMachine();

            Regroup regroup = new Regroup(this);
            Reform reform = new Reform(this);
            StateMachineWrapper stateWrapper = new StateMachineWrapper(statePatrol);

            Transition transitionRegroup = new Transition(() => { return doTransitionToReform; }, reform);
            Transition transitionReform = new Transition(() => { return doTransitionToPatrol; }, stateWrapper);
            Transition transitionPatrol = new Transition(() => { return doTransitionToRegroup; }, regroup);

            regroup.addTransition(transitionRegroup);
            reform.addTransition(transitionReform);
            stateWrapper.addTransition(transitionPatrol);

            stateTactic.currentState = regroup;
        }

        public bool transitionTimerReform()
        {
            return timerReform <= 0;
        }

        public bool transitionTimerPatrol()
        {
            return timerRegroup <= 0;
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
            timerReform = reformeTime;
        }

        public void regroupAction()
        {
            Debug.Log("regroupAction");
            timerRegroup = regroupeTime;
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


        void OnCollisionEnter(Collision collision)
        {
            /*if (collision.transform.GetInstanceID() != transform.GetInstanceID() && collision.transform.tag == "Target")
            {
                needToChangeTarget = true;
                targetPosition.position = new Vector3(100000, 100000, 100000);        
            } else if(collision.transform.tag == "Bullet")
            {
                toto();
            }*/
        }

        void deadPosition()
        {
            currentNavMeshAgent.Warp(startPosition);
        }
    }
}