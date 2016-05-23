using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Agent : MonoBehaviour {

    public GameObject target;
    public float speed = 10.0f;
    public float closeEnoughRange = 1.0f;
    private Vector3 currentTarget;
    private Pathfinding graph;
    public List<Vector3> road = new List<Vector3>();

	// Use this for initialization
	void Start () {

        //Select your pathfinding
        graph = new Pathfinding();
        graph.Load("PoulpeNavMesh");
        graph.setNeighbors();
        //


        target = GameObject.FindGameObjectWithTag("Target");
        road = PathfindingManager.GetInstance().GetRoad(transform.position, target.transform.position,graph);
        InvokeRepeating("UpdateRoad", 0.5f, 0.5f);
        Debug.Log(PathfindingManager.GetInstance().test);
    }
	
	// Update is called once per frame
	void Update () {
        
        if(road.Count > 0)
        {
            currentTarget = road[0];
            if (Vector3.Distance(transform.position, currentTarget) < closeEnoughRange)
            {
                road.RemoveAt(0);
                currentTarget = road[0];
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
	}

    void UpdateRoad()
    {
        //road = PathfindingManager.GetInstance().GetRoad(transform.position, target.transform.position, graph);
    }
}
