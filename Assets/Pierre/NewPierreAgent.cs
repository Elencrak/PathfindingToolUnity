using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewPierreAgent : MonoBehaviour {
    
    TeamNumber team;

    public List<GameObject> targets;
    public float speed = 10.0f;
    public float closeEnoughRange = 1.0f;
    private Vector3 currentTarget;
    private Vector3 currentTargetMove;

    public GameObject bullet;

    NavMeshAgent nav;

    Vector3 startPos;

    Vector3 lastTargetPosition;

    public int nbTimeTouched;

    [HideInInspector]public PierreStateMachine stateMachine;
    
    public PierreStateMachine.Strat basicStrat;

    // Use this for initialization
    void Start() {

        InitStateMachine();

        team = transform.parent.GetComponent<TeamNumber>();

        startPos = transform.position;
        
        nav = GetComponent<NavMeshAgent>();
        nav.speed = 10;
        nav.acceleration = 20;
        nav.stoppingDistance = 5;

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Target"))
        {
            targets.Add(go);
        }
        targets.Remove(gameObject);
        
        InvokeRepeating("UpdateRoad", 0.1f, 0.1f);
        //Debug.Log(PathfindingManager.GetInstance().test);
        InvokeRepeating("Fire", 0f, 1f);
    } 

    void InitStateMachine()
    {
        stateMachine = new PierreStateMachine();

        switch (basicStrat)
        {
            case PierreStateMachine.Strat.Offensive:
                stateMachine.currentState = new PierreOffensif(stateMachine);
                break;
            case PierreStateMachine.Strat.Defensive:
                stateMachine.currentState = new PierreDefensif(stateMachine);
                break;
            case PierreStateMachine.Strat.IDontKnow:
                stateMachine.currentState = new PierreRandom(stateMachine);
                break;
        }
    }

    void Fire()
    {
        transform.LookAt(currentTarget + (currentTarget - lastTargetPosition)*Vector3.Distance(currentTarget, transform.position)/4);

        RaycastHit hit;

        if (!(Physics.Raycast(transform.position, transform.forward, out hit, 200) && hit.transform.GetComponent<NewPierreAgent>()))
        {
            GameObject b = Instantiate(bullet, transform.position + transform.forward * 1.5f, Quaternion.identity) as GameObject;

            b.transform.LookAt(currentTarget + (currentTarget - lastTargetPosition) * Vector3.Distance(currentTarget, transform.position) / 4);
            b.GetComponent<bulletScript>().launcherName = team.teamName;
        }
    }

	// Update is called once per frame
	void Update () {

        stateMachine.Move(this, nav);

        stateMachine.Check();

        //Debug.Log(name + " STATE = " + stateMachine.currentState);

    }

    void UpdateRoad()
    {
        if (targets.Count <= 0) return;
        
        lastTargetPosition = currentTarget;

        currentTarget = stateMachine.UpdateTarget(this, currentTarget, targets);

        currentTargetMove = stateMachine.UpdateTargetMove(this, currentTargetMove, targets);

        nav.SetDestination(currentTargetMove);
    }
    
    void OnCollisionEnter(Collision col)
    {
        if (targets.Contains(col.gameObject))
        {
            if(targets.Count <= 0)
            {
                Debug.Log("Pierre a gagné !");
            }
        }
        else if(col.transform.tag == "Bullet"/* && col.transform.GetComponent<bulletScript>().launcherName != team.teamName*/)
        {
            nav.Warp(startPos);
            nbTimeTouched++;
        }
    }
}
