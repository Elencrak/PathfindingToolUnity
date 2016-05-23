using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Poulpe : MonoBehaviour
{
    public List<GameObject> targets;
    public GameObject target;

    public Vector3 lastTargetPos;
    public Vector3 targetPos;

    float startShoot;
    float delayShoot = 1.0f;
    float startDogge;
    float delayDogge = 0.3f;

    List<PoulpeState> states;

    PoulpeStateMachine stateMachine;

    List<PoulpeTransition> moveTransitions;
    List<PoulpeTransition> shootTransitions;
    List<PoulpeTransition> doggeTransitions;
    List<PoulpeTransition> idleTransitions;
    List<PoulpeTransition> secondTransitions;
    List<PoulpeTransition> firstTransitions;

    PoulpeMove move;
    PoulpeShoot shoot;
    PoulpeDogge dogge;
    PoulpeIdle idle;

    Vector3 begin;

    PoulpeStateMachine firstStateMachine;
    PoulpeStateMachine secondStateMachine;

    void Start()
    {
        targets = new List<GameObject>();
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Target");
        foreach(GameObject targ in temp)
        {
            if(targ.name != "Poulpe")
            {
                targets.Add(targ);
            }
        }

        #region WhitoutHierarchie
        /*
        states = new List<PoulpeState>();
        moveTransitions = new List<PoulpeTransition>();
        shootTransitions = new List<PoulpeTransition>();
        doggeTransitions = new List<PoulpeTransition>();
        idleTransitions = new List<PoulpeTransition>();
        
        startShoot = 0;
        startDogge = 0;
        begin = transform.position;
        move = new PoulpeMove(this.gameObject ,this.GetComponent<NavMeshAgent>());
        shoot = new PoulpeShoot(this.gameObject);
        dogge = new PoulpeDogge(this.gameObject);
        idle = new PoulpeIdle(this.gameObject, this.GetComponent<NavMeshAgent>());
        states.Add(move);
        states.Add(shoot);
        states.Add(dogge);
        states.Add(idle);
        stateMachine = new PoulpeStateMachine(move);

        PoulpeTransition toDogge = new PoulpeTransition(CanDogge, dogge, stateMachine);
        PoulpeTransition toIdle = new PoulpeTransition(EnemySpotted, idle, stateMachine);
        moveTransitions.Add(toDogge);
        moveTransitions.Add(toIdle);
        move.SetTransitions(moveTransitions);

        PoulpeTransition toChase = new PoulpeTransition(HasDogge, move, stateMachine);
        doggeTransitions.Add(toChase);
        dogge.SetTransitions(doggeTransitions);

        PoulpeTransition toIdle2 = new PoulpeTransition(CoolDownDelay, idle, stateMachine);
        shootTransitions.Add(toIdle2);
        shoot.SetTransitions(shootTransitions);

        PoulpeTransition toShoot = new PoulpeTransition(CanShoot, shoot, stateMachine);
        PoulpeTransition toMove = new PoulpeTransition(EnemyNotSpotted, move, stateMachine);
        idleTransitions.Add(toDogge);
        idleTransitions.Add(toShoot);
        idleTransitions.Add(toMove);
        idle.SetTransitions(idleTransitions);*/
        #endregion

        #region WhitHierarchie
        states = new List<PoulpeState>();
        moveTransitions = new List<PoulpeTransition>();
        shootTransitions = new List<PoulpeTransition>();
        doggeTransitions = new List<PoulpeTransition>();
        idleTransitions = new List<PoulpeTransition>();
        secondTransitions = new List<PoulpeTransition>();
        firstTransitions = new List<PoulpeTransition>();

        startShoot = 0;
        startDogge = 0;
        begin = transform.position;
        move = new PoulpeMove(this.gameObject, this.GetComponent<NavMeshAgent>());
        shoot = new PoulpeShoot(this.gameObject);
        dogge = new PoulpeDogge(this.gameObject);
        idle = new PoulpeIdle(this.gameObject, this.GetComponent<NavMeshAgent>());
        states.Add(move);
        states.Add(idle);
        secondStateMachine = new PoulpeStateMachine(move);

        states = new List<PoulpeState>();
        states.Add(secondStateMachine);
        states.Add(dogge);
        states.Add(shoot);
        firstStateMachine = new PoulpeStateMachine(secondStateMachine);
        firstStateMachine.SetTransitions(firstTransitions);


        PoulpeTransition toDogge = new PoulpeTransition(CanDogge, dogge, firstStateMachine);
        PoulpeTransition toShoot = new PoulpeTransition(CanShoot, shoot, firstStateMachine);
        secondTransitions.Add(toShoot);
        secondTransitions.Add(toDogge);
        secondStateMachine.SetTransitions(secondTransitions);

        PoulpeTransition toChase = new PoulpeTransition(HasDogge, secondStateMachine, firstStateMachine);
        doggeTransitions.Add(toChase);
        dogge.SetTransitions(doggeTransitions);

        PoulpeTransition toIdle2 = new PoulpeTransition(CoolDownDelay, secondStateMachine, firstStateMachine);
        shootTransitions.Add(toIdle2);
        shoot.SetTransitions(shootTransitions);
        
        PoulpeTransition toIdle = new PoulpeTransition(EnemySpotted, idle, secondStateMachine);
        moveTransitions.Add(toIdle);
        move.SetTransitions(moveTransitions);

        PoulpeTransition toMove = new PoulpeTransition(EnemyNotSpotted, move, secondStateMachine);
        idleTransitions.Add(toMove);
        idle.SetTransitions(idleTransitions);
        #endregion
    }

    void Update ()
    {
        if(target != null)
        {
            lastTargetPos = targetPos;
            targetPos = target.transform.position;
        }
        startShoot -= Time.deltaTime;
        startDogge -= Time.deltaTime;
        firstStateMachine.Step();
    }

    bool EnemyNotSpotted()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, targets[i].transform.position - transform.position, out hit);
            if (hit.collider.tag == "Target" && hit.collider.name != "Poulpe")
            {
                return false;
            }
        }
        return true;
    }

    bool EnemySpotted()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, targets[i].transform.position - transform.position, out hit);
            if (hit.collider.tag == "Target" && hit.collider.name != "Poulpe")
            {
                idle.destination = this.transform.position;
                return true;
            }
        }
        return false;
    }

    bool HasDogge()
    {
        return startDogge <= 0;
    }

    bool CoolDownDelay()
    {
        if (startShoot > 0)
        {
            idle.destination = this.transform.position;
            return true;
        }
        return false;
    }

    bool CanDogge()
    {
        return startDogge > 0;
    }

    bool CanShoot()
    {
        if (startShoot <= 0)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                RaycastHit hit;
                Physics.Raycast(transform.position, targets[i].transform.position - transform.position, out hit);
                if (hit.collider.tag == "Target" && hit.collider.name != "Poulpe")
                {
                    startShoot = delayShoot;
                    return true;
                }
            }
        }
        return false;
    }
    
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Bullet":
                GetComponent<NavMeshAgent>().Warp(begin);
                break;
            case "Target":
                break;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Bullet")
        {
            dogge.bullet = collider.gameObject.transform.right;
            startDogge = delayDogge;
        }
    }

    public GameObject Instantiation()
    {
        return Instantiate(Resources.Load("Bullet"), transform.position + transform.forward * 2, Quaternion.Euler(transform.eulerAngles)) as GameObject;
    }
}