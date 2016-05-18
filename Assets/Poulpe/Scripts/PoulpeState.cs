using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoulpeState : MonoBehaviour
{
    public GameObject[] targets;
    public float speed = 10.0f;
    public float closeEnoughRange = 1.0f;
    private Vector3 currentTarget;
    private Pathfinding graph;
    public List<Vector3> road = new List<Vector3>();
    
    void Start ()
    {
        //Select your pathfinding
        graph = new Pathfinding();
        graph.Load("PoulpeNavMesh");
        graph.setNeighbors();
        //


        targets = GameObject.FindGameObjectsWithTag("Target");
        //road = PathfindingManager.GetInstance().GetRoad(transform.position, target.transform.position, graph);
        InvokeRepeating("UpdateRoad", 0.5f, 0.5f);
    }
}
