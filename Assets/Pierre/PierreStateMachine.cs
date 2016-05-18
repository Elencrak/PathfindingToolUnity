using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PierreStateMachine : PierreState
{
    List<PierreState> states;

    public PierreState currentState;

    Pathfinding myPathfinding;

    Vector3 currentTarget;

    public Transform target;

    List<Vector3> road;

    // Use this for initialization
    void Start ()
    {
        myPathfinding = new Pathfinding();
        myPathfinding.Load("PierrePathFinding2");
        myPathfinding.setNeighbors();



        road = PathfindingManager.GetInstance().GetRoad(transform.position, target.position, myPathfinding);

        road = PathfindingManager.GetInstance().SmoothRoad(road);
    }
	
	// Update is called once per frame
	void Update ()
    {

        Move();

        Fire();

	}

    public override void Move() 
    {
        
        if (road.Count > 0)
        {
            currentTarget = road[0];
            if (Vector3.Distance(transform.position, currentTarget) < .1f)
            {
                road.RemoveAt(0);
                currentTarget = road[0];
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, currentTarget, 10 * Time.deltaTime);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, 10 * Time.deltaTime);
        }

        //currentState.Move();
    }

    public override void Fire()
    {
        //currentState.Fire();
    }
}
