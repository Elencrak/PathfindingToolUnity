using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AgentLefevre : MonoBehaviour
{

    public GameObject target;
    public float speed = 10.0f;
    public float closeEnoughRange = 1.0f;
    private Vector3 currentTarget;
    private Pathfinding graph;
    public List<Vector3> road = new List<Vector3>();
    public List<GameObject> targets = new List<GameObject>();
    NavMeshAgent agent;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        float dist = Mathf.Infinity;
        GameObject[] test = GameObject.FindGameObjectsWithTag("Target");
        foreach (GameObject obj in test)
        {
            if (obj == gameObject)
                continue;
            float tmp = Vector3.Distance(transform.position, obj.transform.position);
            if (tmp < dist)
            {
                dist = tmp;
                target = obj;
            }
            targets.Add(obj);
        }

        /*
        //Select your pathfinding
        graph = new Pathfinding();
        graph.Load(PlayerPrefs.GetString("Pierre"));
        graph.setNeighbors();
        //


        target = GameObject.FindGameObjectWithTag("Target");
        road = PathfindingManager.GetInstance().GetRoad(transform.position, target.transform.position, graph);
        Debug.Log(PathfindingManager.GetInstance().test);

        */
        InvokeRepeating("UpdateRoad", 0.5f, 0.5f);

    }

    // Update is called once per frame
    void Update()
    {

        if (targets.Count > 0)
            agent.SetDestination(target.transform.position);
        else
        {
            Debug.Log("Benjamin à fini !");
            Destroy(this);
        }
        /*
        if (road.Count > 0)
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
        }*/
    }

    void UpdateRoad()
    {
        float dist = Mathf.Infinity;
        foreach (GameObject obj in targets)
        {
            float tmp = Vector3.Distance(transform.position, obj.transform.position);
            if (tmp < dist)
            {
                dist = tmp;
                target = obj;
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.transform.tag == "Target" && targets.Contains(col.gameObject))
        {
            targets.Remove(col.gameObject);
            UpdateRoad();
        }
    }
}
