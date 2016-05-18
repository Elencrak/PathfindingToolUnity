using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SW_Walk : StateWill {
    int idPlayer;
    GameObject player;
    GameObject target;
    Vector3 currentTarget;
    public List<Vector3> road = new List<Vector3>();
    float closeEnoughRange;
    float speed;
    Pathfinding graph;
    float timerUpdateRoad = 1f;
    float lastUpdate;

    public SW_Walk(int pIdPlayer, float pSpeed, float pRange, string graphName, List<TransitionWill> pTransitions)
    {
        idPlayer = pIdPlayer;
        target = TeamManagerWill.instance.mainTarget;
        transition = pTransitions;
        closeEnoughRange = pRange;
        speed = pSpeed;
        graph = new Pathfinding();
        graph.Load(graphName);
        graph.setNeighbors();
        player = TeamManagerWill.instance.members[idPlayer].gameObject;
        road = PathfindingManager.GetInstance().GetRoad(player.transform.position, target.transform.position, graph);
        road = PathfindingManager.GetInstance().SmoothRoad(road);
    }

    public override StateWill execute()
    {
        StateWill next = checkTransition();
        if (next != null)
            return next;

        walk();
        return null;
    }

    protected override StateWill checkTransition()
    {
        StateWill next = null;
        foreach (TransitionWill trans in transition)
        {
            
            next = trans.check();
            if (next != null)
            {
                return next;
            }
        }
        return null;
    }

    void walk()
    {
        if (timerUpdateRoad + lastUpdate < Time.time)
        {
            lastUpdate = Time.time;
            road = PathfindingManager.GetInstance().GetRoad(player.transform.position, target.transform.position, graph);
            road = PathfindingManager.GetInstance().SmoothRoad(road);
        }

        target = TeamManagerWill.instance.mainTarget;
        if (road.Count > 0)
        {
            currentTarget = road[0];
            if (Vector3.Distance(player.transform.position, currentTarget) < closeEnoughRange)
            {
                road.RemoveAt(0);
                //currentTarget = road[0];
            }
            else
            {
                Vector3 pos = currentTarget;
                pos.y += 0.5f;
                player.transform.position = Vector3.MoveTowards(player.transform.position, pos, speed * Time.deltaTime);
                player.transform.LookAt(pos);
            }
        }
        else
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, target.transform.position, speed * Time.deltaTime);
            player.transform.LookAt(target.transform.position);
        }


        

    }

}
