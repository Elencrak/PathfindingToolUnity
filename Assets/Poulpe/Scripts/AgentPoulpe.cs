using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AgentPoulpe : MonoBehaviour
{

    public GameObject target;
    public float speed = 10.0f;
    public float closeEnoughRange = 1.0f;
    private Vector3 currentTarget;
    private Pathfinding graph;
    public List<Vector3> road = new List<Vector3>();

    private List<GameObject> players;
    private int score;

    public GameObject[] temp;

	// Use this for initialization
	void Start ()
    {
        /*
        //Select your pathfinding
        graph = new Pathfinding();
        graph.Load("poulpe");
        graph.setNeighbors();
        //


        target = GameObject.FindGameObjectWithTag("Target");
        road = PathfindingManager.GetInstance().GetRoad(transform.position, target.transform.position,graph);
        InvokeRepeating("UpdateRoad", 0.5f, 0.5f);
        Debug.Log(PathfindingManager.GetInstance().test);*/
        players = new List<GameObject>();
        temp = GameObject.FindGameObjectsWithTag("Target");
        score = 0;
        foreach(GameObject pla in temp)
        {
            if(pla != this.gameObject)
            {
                players.Add(pla);
            }
        }
    }
	
	// Update is called once per frame
	/*void Update ()
    {
        
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
	}*/

    void Update()
    {
        bool first = false;
        GameObject nearest = this.gameObject;
        float dist = 0;
        float tempdist;
        //road = PathfindingManager.GetInstance().GetRoad(transform.position, target.transform.position, graph);
        foreach(GameObject pla in players)
        {
            tempdist = Mathf.Abs(transform.position.x - pla.transform.position.x) + Mathf.Abs(transform.position.y - pla.transform.position.y) + Mathf.Abs(transform.position.z - pla.transform.position.z);
            if (!first)
            {
                first = true;
                nearest = pla;
                dist = tempdist;
            }
            else
            {
                if(tempdist < dist && pla.transform.position.y <= transform.position.y)
                {
                    dist = tempdist;
                    nearest = pla;
                }
            }
        }
        gameObject.GetComponent<NavMeshAgent>().SetDestination(nearest.transform.position);
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach(GameObject pla in players)
        {
            if(pla == collision.gameObject)
            {
                players.Remove(pla);
                score++;
                if(score == 14)
                {
                    Victory();
                }
                return;
            }
        }
    }

    void Victory()
    {
        Debug.Log("Poulpe a gagné bande de naze !");
    }
}
