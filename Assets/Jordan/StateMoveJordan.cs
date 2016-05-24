using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateMoveJordan : StateJordan {

    public float speed = 10.0f;
    public float closeEnoughRange = 1.0f;
    private Vector3 currentTargetPos;
    private Pathfinding graph;
    public List<Vector3> road;
    private float startTimerUpdateRoad, delayTimerUpdateRoad;

    // Use this for initialization
    public StateMoveJordan()
    {
        //Select your pathfinding
        graph = new Pathfinding();
        graph.Load("Windywyll");
        graph.setNeighbors();
        delayTimerUpdateRoad = 1.0f;
        startTimerUpdateRoad = Time.time;
    }
    
    public override void step()
    {
        if (currentTarget == null)
            return;

        if(road == null)
        {
            road = PathfindingManager.GetInstance().GetRoad(transform.position, currentTarget.transform.position, graph);
        }

        if (road.Count > 0)
        {
            currentTargetPos = road[0];
            if (Vector3.Distance(transform.position, currentTargetPos) < closeEnoughRange)
            {
                road.RemoveAt(0);
                currentTargetPos = road[0];
            }
            else
            {
                //transform.position = Vector3.MoveTowards(transform.position, currentTargetPos, speed * Time.deltaTime);
                
                Vector3 dir = currentTargetPos - transform.position;
                Vector3 movement = dir.normalized * speed * Time.deltaTime;
                if (movement.magnitude > dir.magnitude)
                    movement = dir;
                transform.gameObject.GetComponent<CharacterController>().Move(movement);
            }
        }
        else
        {
            //transform.position = Vector3.MoveTowards(transform.position, currentTarget.transform.position, speed * Time.deltaTime);
            Vector3 dir = currentTargetPos - transform.position;
            Vector3 movement = dir.normalized * speed * Time.deltaTime;
            if (movement.magnitude > dir.magnitude)
                movement = dir;
            transform.gameObject.GetComponent<CharacterController>().Move(movement);
        }

        Vector3 heightUpdate = Vector3.zero;
        heightUpdate.y = 1.0f;
        heightUpdate.x = transform.position.x;
        heightUpdate.z = transform.position.z;
        transform.position = heightUpdate;

        if(startTimerUpdateRoad + delayTimerUpdateRoad < Time.time)
        {
            startTimerUpdateRoad = Time.time;
            UpdateRoad();
        }
    }

    public override StateJordan check()
    {
        if (listTransition != null)
        {
            foreach (TransitionJordan trans in listTransition)
            {
                StateJordan temp = trans.check();

                if (temp != null)
                    return temp;
            }
        }
        return null;
    }

    void UpdateRoad()
    {
        road = PathfindingManager.GetInstance().GetRoad(transform.position, currentTarget.transform.position, graph);
    }
}
