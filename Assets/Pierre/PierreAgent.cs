using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PierreAgent : MonoBehaviour {

    TeamNumber team;

    public List<GameObject> targets;
    public float speed = 10.0f;
    public float closeEnoughRange = 1.0f;
    private Vector3 currentTarget;
    private Vector3 currentTargetFire;
    //private Pathfinding graph;
    public List<Vector3> road = new List<Vector3>();

    public GameObject bullet;

    NavMeshAgent nav;

    Vector3 startPos;

    Vector3 lastTargetPosition;

    // Use this for initialization
    void Start() {

        team = transform.parent.GetComponent<TeamNumber>();

        startPos = transform.position;

        /*Select your pathfinding
        graph = new Pathfinding();
        graph.Load(PlayerPrefs.GetString("Pierre"));
        graph.setNeighbors();
        */
        nav = GetComponent<NavMeshAgent>();
        nav.speed = 10;
        nav.acceleration = 20;
        nav.stoppingDistance = 5;

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Target"))
        {
            targets.Add(go);
        }
        targets.Remove(gameObject);

        //road = PathfindingManager.GetInstance().GetRoad(transform.position, target.transform.position,graph);
        InvokeRepeating("UpdateRoad", 0.1f, 0.1f);
        //Debug.Log(PathfindingManager.GetInstance().test);
        InvokeRepeating("Fire", 0f, 1f);
    } 

    void Fire()
    {
        transform.LookAt(currentTargetFire + (currentTargetFire -lastTargetPosition)*5);

        GameObject b = Instantiate(bullet, transform.position + transform.forward * 1.5f, Quaternion.identity) as GameObject;

        b.transform.LookAt(currentTargetFire + (currentTargetFire - lastTargetPosition)*5);
        b.GetComponent<bulletScript>().launcherName = team.teamName;
    }

	// Update is called once per frame
	void Update () {



        /*Collider[] hitColliders = Physics.OverlapSphere(transform.position, 20);
        
        foreach (Collider col in hitColliders)
        {
            if (col.tag == "Bullet" && col.GetComponent<bulletScript>().launcherName != team.teamName)
            {
                nav.SetDestination(transform.position + col.transform.right*20);
            }
        }*/

        //Debug.Log(currentTarget + " " + transform.position);

        /*if(road.Count > 0)
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
        //road = PathfindingManager.GetInstance().GetRoad(transform.position, target.transform.position, graph);

        if (targets.Count <= 0) return;
        
        lastTargetPosition = currentTargetFire;

        if(targets[0] != gameObject)
        {
            currentTarget = targets[0].transform.position;
            currentTargetFire = targets[0].transform.position;
        }

        foreach(GameObject target in targets)
        {
            if(Vector3.Distance(target.transform.position,transform.position) > Vector3.Distance(currentTarget, transform.position) || (currentTarget == transform.position && transform.position != target.transform.position))
            {
                currentTarget = target.transform.position;
            }
            if (Vector3.Distance(target.transform.position, transform.position) < Vector3.Distance(currentTarget, transform.position) || (currentTarget == transform.position && transform.position != target.transform.position))
            {
                currentTargetFire = target.transform.position;
            }
        }
        
        nav.SetDestination(currentTargetFire);
    }

    void OnCollisionEnter(Collision col)
    {
        if (targets.Contains(col.gameObject))
        {
            //col.gameObject.GetComponent<Collider>().isTrigger = true;
            //targets.Remove(col.gameObject);

            if(targets.Count <= 0)
            {
                Debug.Log("Pierre a gagné !");
            }
        }
        else if(col.transform.tag == "Bullet" && col.transform.GetComponent<bulletScript>().launcherName != team.teamName)
        {
            nav.Warp(startPos);
        }
    }
}
