using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using System;

namespace IARobin
{
    using StateMachineRobin;
    using System.Linq;
    namespace StateMachineRobin
    {
        public class State
        {
            public string name;

            public delegate void Action();

            public Action _action;

            public delegate void Init();

            public Init _init;

            public delegate void Finish();

            public Finish _finish;

            StateMachine _mother;

            List<Transition> _transitions;

            public State()
            {
                _action = null;
                _mother = null;
                _transitions = new List<Transition>();
            }

            public State(StateMachine stateMachine, Action action = null, Init init = null, Finish finish = null)
            {
                _action = action;
                _init = init;
                _finish = finish;
                _mother = stateMachine;
                _transitions = new List<Transition>();
            }

            public void AddTransition(Transition t)
            {
                if (t != null)
                {
                    _transitions.Add(t);
                }
            }

            public virtual void Step()
            {
                State chckd = null;
                for (int i = 0; i < _transitions.Count; i++)
                {
                    chckd = _transitions[i].Check();
                    if (chckd != null)
                    {
                        if (_finish != null)
                        {
                            _finish();
                        }
                        _mother._currentState = chckd;
                        if (chckd._init != null)
                        {
                            chckd._init();
                        }
                        chckd.Step();
                        break;
                    }
                }
                if (chckd == null)
                {
                    if (_action != null)
                    {
                        _action();
                    }
                }
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

            public delegate bool IsTransitioning();

            public IsTransitioning _check;

            public Transition()
            {
                _toState = null;
            }

            public Transition(State state)
            {
                _toState = state;
            }

            public Transition(State state, IsTransitioning check = null)
            {
                _toState = state;
                _check = check;
            }

            public virtual State Check()
            {
                if (_check())
                    return _toState;
                return null;
            }
        }
    }

    public class AgentRobinMathieu : MonoBehaviour
    {

        [Header("IA")]

        public Vector3 startPoint;
        public GameObject nearestTarget;
        public BoxCollider nearTargetCollider;
        public List<GameObject> targets;
        public NavMeshAgent agent;
        public AgentRobin agent2;

        SphereCollider detect;

        public float errorMargin = 0.2f;

        bool hasWin = false;

        public List<GameObject> _bullets;

        [Header("Values")]

        public bool isShooting = true;
        public static string playerID = "Squad Robin";
        TeamNumber parentNumber;

        StateMachine _stateMachine;

        protected virtual void Start()
        {
            InitStateMachine();

            detect = transform.FindChild("Trigger").GetComponent<SphereCollider>();
            _bullets = new List<GameObject>();
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
            InvokeRepeating("UpdateSM", 0.1f, 0.2f);
        }

        void UpdateSM()
        {
            _stateMachine.Step();
            UpdateTarget();
        }

        private void InitStateMachine()
        {
            _stateMachine = new StateMachine();
            _stateMachine.name = "Machine";

            State.Action randomAction = new State.Action(() => 
            {
                UpdateRoadRandom();
            });
            State.Action esquiveAction = new State.Action(() => 
            {
                UpdateRoadEsquive();
            });

            State random = new State(_stateMachine, randomAction);
            random.name = "Random";
            State esquive = new State(_stateMachine, esquiveAction);
            random.name = "Esquive";

            Transition.IsTransitioning transitionBullet = new Transition.IsTransitioning(() =>
            {
                return _bullets.Count > 0;
            });
            Transition.IsTransitioning transitionNoBullet = new Transition.IsTransitioning(() =>
            {
                return _bullets.Count == 0;
            });

            Transition bulletDetected = new Transition(esquive, transitionBullet);
            Transition bulletNotDetected = new Transition(random, transitionNoBullet);

            random.AddTransition(bulletDetected);
            esquive.AddTransition(bulletNotDetected);

            _stateMachine._currentState = random;
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

        protected virtual void UpdateRoadRandom()
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

        protected virtual void UpdateRoadEsquive()
        {
            if (targets.Count > 0)
            {
                if (agent.enabled)
                {
                    agent.SetDestination(transform.position + new Vector3(Random.Range(-detect.bounds.size.x, detect.bounds.size.x), 0.0f, Random.Range(-detect.bounds.size.z, detect.bounds.size.z)));
                }
                if (agent2.enabled)
                {
                    // ???
                }
            }
        }

        void Respawn()
        {
            agent.Warp(startPoint);
        }

        void OnCollisionEnter(Collision coll)
        {
            if (coll.gameObject.CompareTag("Bullet") && coll.gameObject.GetComponent<bulletScript>().launcherName != playerID)
            {
                Respawn();
            }
        }

        void OnTriggerEnter(Collider coll)
        {
            if (coll.CompareTag("Bullet") && coll.GetComponent<bulletScript>().launcherName != playerID)
            {
                _bullets.Add(coll.gameObject);
                //coll.GetComponent<bulletScript>().launcherName = AgentRobinMathieu.playerID;
            }
        }

        void OnTriggerExit(Collider coll)
        {
            //_bullets = _bullets.Where(bull => bull != null).ToList();
            if (coll.CompareTag("Bullet") && coll.GetComponent<bulletScript>().launcherName != playerID)
            {
                _bullets.Remove(coll.gameObject);
                //coll.GetComponent<bulletScript>().launcherName = AgentRobinMathieu.playerID;
            }
        }
    }

}