using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PierreStateMachine : PierreState
{
    public PierreState currentState;

    //Pathfinding myPathfinding;

    Vector3 currentTarget;
    
    List<Vector3> road;

    NavMeshAgent nav;

    public enum Strat
    {
        Offensive,
        Defensive,
        IDontKnow
    }

    public Strat basicStrat;

    // Use this for initialization
    void Start ()
    {
        /* myPathfinding = new Pathfinding();
         myPathfinding.Load("PierrePathFinding2");
         myPathfinding.setNeighbors();

         road = PathfindingManager.GetInstance().GetRoad(transform.position, target.position, myPathfinding);

         road = PathfindingManager.GetInstance().SmoothRoad(road);*/

        switch (basicStrat)
        {
            case Strat.Offensive:
                currentState = gameObject.AddComponent<PierreOffensif>();
                break;
            case Strat.Defensive:
                currentState = gameObject.AddComponent<PierreDefensif>();
                break;
            case Strat.IDontKnow:
                currentState = gameObject.AddComponent<PierreRandom>();
                break;
        }

        nav = GetComponent<NavMeshAgent>();
        nav.speed = 10;
        nav.acceleration = 20;
        nav.stoppingDistance = 5;
    }
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    public override void Move(NavMeshAgent nav) 
    {
        currentState.Move(nav);
    }

    public override void Fire()
    {
        currentState.Fire();
    }

    public override Vector3 UpdateTarget(Vector3 myTarget, List<GameObject> targets)
    {
        return currentState.UpdateTarget(myTarget,targets);
    }

    public override Vector3 UpdateTargetMove(Vector3 myTargetMove, List<GameObject> targets)
    {
        return currentState.UpdateTargetMove(myTargetMove, targets);
    }
}
